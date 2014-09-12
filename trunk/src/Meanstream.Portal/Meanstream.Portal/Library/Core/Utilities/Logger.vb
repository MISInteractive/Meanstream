Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Configuration

Namespace Meanstream.Portal.Core.Utilities

    Public Class Logger
        Implements IDisposable

        Public Sub New()

        End Sub

        Public Enum LoggingType
            LOGGING_TYPE_NONE = 0
            LOGGING_TYPE_ERROR = 1
            LOGGING_TYPE_WARNING = 2
            LOGGING_TYPE_INFORMATION = 3
            LOGGING_TYPE_DEBUG = 4
        End Enum

        Private Function LogType() As Integer
            Try
                Integer.Parse(ConfigurationManager.AppSettings("Meanstream.Logging"))
            Catch ex As Exception
                Return LoggingType.LOGGING_TYPE_NONE
            End Try
            Return ConfigurationManager.AppSettings("Meanstream.Logging")
        End Function

        Public Sub Warning(ByVal Message As String)
            Write(Message, LoggingType.LOGGING_TYPE_WARNING)
        End Sub

        Public Sub OnError(ByVal Message As String)
            Write(Message, LoggingType.LOGGING_TYPE_ERROR)
        End Sub

        Public Sub Information(ByVal Message As String)
            Write(Message, LoggingType.LOGGING_TYPE_INFORMATION)
        End Sub

        Public Sub Add(ByVal Message As String, ByVal Type As LoggingType)
            Write(Message, Type)
        End Sub

        Public Sub Debug(ByVal Message As String)
            Write(Message, LoggingType.LOGGING_TYPE_DEBUG)
        End Sub

        Public Sub Write(ByVal Message As String, ByVal Type As Meanstream.Portal.Core.Utilities.Logger.LoggingType)
            If Me.LogType() = 0 Or Me.LogType < Type Then
                Exit Sub
            End If

            Dim Dir As String = ConfigurationManager.AppSettings("Meanstream.LogRoot") & "/Events/"
            If (Not Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(Dir))) Then
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(Dir))
            End If

            Dim path As String = Dir & DateTime.Today.ToString("yyyy-MM-dd") & ".txt"

            If (Not File.Exists(System.Web.HttpContext.Current.Server.MapPath(path))) Then
                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close()
            End If

            Using w As StreamWriter = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path))
                Dim Line As String = ""
                If Type = LoggingType.LOGGING_TYPE_ERROR Then
                    Line = "Error: "
                End If
                If Type = LoggingType.LOGGING_TYPE_INFORMATION Then
                    Line = "Information: "
                End If
                If Type = LoggingType.LOGGING_TYPE_WARNING Then
                    Line = "Warning: "
                End If
                If Type = LoggingType.LOGGING_TYPE_DEBUG Then
                    Line = "Debug: "
                End If
                'w.WriteLine(Constants.vbCrLf & "Log Entry : ")
                'w.WriteLine("{0}", DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture))
                Line = Line & DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture) & ": " & System.Web.HttpContext.Current.Request.Url.ToString() & " Message:" & Message
                w.WriteLine(Line)
                'w.WriteLine("__________________________")
                w.Flush()
                w.Close()
            End Using
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Namespace

