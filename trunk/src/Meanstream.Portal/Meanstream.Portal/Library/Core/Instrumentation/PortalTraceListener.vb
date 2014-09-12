Imports System.Diagnostics
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Specialized 

Namespace Meanstream.Portal.Core.Instrumentation
    ''' <summary>
    ''' Summary description for PortalTraceListener.
    ''' </summary>
    Public Class PortalTraceListener
        Inherits TraceListener
        Private Const COLUMN_SEPARATOR As String = "|"
        Private _connectionString As String
        Private _maximumRequests As Integer
        Private _objCollection As StringCollection
        Private _traceSwitch As TraceSwitch
        Private _booleanSwitch As BooleanSwitch

        Public Sub New()
            InitializeListener()
        End Sub

        Public Sub New(ByVal r_strListenerName As String)
            MyBase.New(r_strListenerName)
            InitializeListener()
        End Sub

        Private Sub InitializeListener()
            _connectionString = Meanstream.Portal.Core.Data.DataRepository.ConnectionStrings("Meanstream").ConnectionString
            _maximumRequests = Convert.ToInt32(ConfigurationManager.AppSettings("Meanstream.Tracing.MaximumRequests"))
            _objCollection = New StringCollection()
            _traceSwitch = New TraceSwitch("PortalTraceLevel", "Tracing Level")
            _booleanSwitch = New BooleanSwitch("PortalSwitch", "On/Off Switch")
        End Sub

        Private Sub SaveTrace()
            Dim objConnection As New SqlConnection(_connectionString)
            Dim objCommand As New SqlCommand()
            Try
                objCommand.Connection = objConnection
                objCommand.CommandText = "meanstream_AddTrace"
                objCommand.CommandType = CommandType.StoredProcedure
                objConnection.Open()

                For Each message As String In _objCollection
                    CreateParameters(objCommand, message)
                    objCommand.ExecuteNonQuery()
                Next
                _objCollection.Clear()

            Catch e As Exception
            Finally
                If objConnection IsNot Nothing Then
                    If objConnection.State = ConnectionState.Open Then
                        objConnection.Close()
                    End If
                End If
                objConnection = Nothing
                objCommand = Nothing
            End Try
        End Sub

        Private Sub AddToCollection(ByVal traceDateTime As String, ByVal traceCategory As String, ByVal traceDescription As String, ByVal stackTrace As String, ByVal detailedErrorDescription As String)
            If _booleanSwitch.Enabled Then
                If Not [Enum].GetNames(GetType(TraceLevel)).Contains(traceCategory) Then
                    traceCategory = [Enum].GetName(GetType(TraceLevel), TraceLevel.Verbose)
                End If
                Dim category As TraceLevelAttribute = New TraceLevelAttribute(traceCategory)
                If _traceSwitch.Level > 0 And category.Value <= _traceSwitch.Level Then               
                    Dim strError As String = traceDateTime & COLUMN_SEPARATOR & category.Name & COLUMN_SEPARATOR & traceDescription & COLUMN_SEPARATOR & stackTrace & COLUMN_SEPARATOR & detailedErrorDescription
                    _objCollection.Add(strError)
                    If _objCollection.Count = _maximumRequests Then
                        SaveTrace()
                    End If
                End If
            End If
        End Sub

        Private Sub CreateParameters(ByVal r_objCommand As SqlCommand, ByVal r_strError As String)
            If (r_objCommand IsNot Nothing) AndAlso (Not r_strError.Equals("")) Then
                Dim strColumns As String()
                Dim objParameters As SqlParameterCollection = r_objCommand.Parameters

                strColumns = r_strError.Split(COLUMN_SEPARATOR.ToCharArray())
                objParameters.Clear()

                objParameters.Add(New SqlParameter("@m_TraceDateTime", SqlDbType.DateTime, 8))
                objParameters.Add(New SqlParameter("@m_TraceCategory", SqlDbType.NVarChar, 50))
                objParameters.Add(New SqlParameter("@m_TraceDescription", SqlDbType.NVarChar, 1024))
                objParameters.Add(New SqlParameter("@m_StackTrace", SqlDbType.NVarChar))
                objParameters.Add(New SqlParameter("@m_DetailedErrorDescription", SqlDbType.NVarChar))

                Dim iCount As Integer = strColumns.GetLength(0)
                For i As Integer = 0 To iCount - 1
                    objParameters(i).IsNullable = True
                    objParameters(i).Direction = ParameterDirection.Input
                    objParameters(i).Value = strColumns.GetValue(i).ToString().Trim()
                Next
            End If
        End Sub


        Public Overrides Sub Write(ByVal message As String)
            Dim objTrace As New StackTrace(True)
            AddToCollection(DateTime.Now.ToString(), "", message, objTrace.ToString(), "")
        End Sub

        Public Overrides Sub Write(ByVal o As Object)
            Dim objTrace As New StackTrace(True)
            AddToCollection(DateTime.Now.ToString(), "", o.ToString(), objTrace.ToString(), "")
        End Sub

        Public Overrides Sub Write(ByVal message As String, ByVal category As String)
            Dim objTrace As New StackTrace(True)
            AddToCollection(DateTime.Now.ToString(), category, message, objTrace.ToString(), "")
        End Sub

        Public Overrides Sub Write(ByVal o As Object, ByVal category As String)
            Dim objTrace As New StackTrace(True)
            AddToCollection(DateTime.Now.ToString(), category, o.ToString(), objTrace.ToString(), "")
        End Sub


        Public Overrides Sub WriteLine(ByVal message As String)
            Write(message & vbLf)
        End Sub

        Public Overrides Sub WriteLine(ByVal o As Object)
            Write(o.ToString() & vbLf)
        End Sub

        Public Overrides Sub WriteLine(ByVal message As String, ByVal category As String)
            Write((message & vbLf), category)
        End Sub

        Public Overrides Sub WriteLine(ByVal o As Object, ByVal category As String)
            Write((o.ToString() & vbLf), category)
        End Sub


        Public Overrides Sub Fail(ByVal message As String)
            Dim objTrace As New StackTrace(True)
            AddToCollection(DateTime.Now.ToString(), "Fail", message, objTrace.ToString(), "")
        End Sub

        Public Overrides Sub Fail(ByVal message As String, ByVal detailMessage As String)
            Dim objTrace As New StackTrace(True)
            AddToCollection(DateTime.Now.ToString(), "Fail", message, objTrace.ToString(), detailMessage)
        End Sub

        Public Overrides Sub Close()
            If _booleanSwitch.Enabled Then
                SaveTrace()
            End If
        End Sub

        Public Overrides Sub Flush()
            If _booleanSwitch.Enabled Then
                SaveTrace()
            End If
        End Sub

    End Class
End Namespace
