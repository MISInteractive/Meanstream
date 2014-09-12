Imports System.Text
Imports System.Reflection
Imports System.Diagnostics

Namespace Meanstream.Portal.Core.Instrumentation
    Public Enum DisplayMethodInfo
        DoNotDisplay
        NameOnly
        FullSignature
    End Enum

    Public NotInheritable Class PortalTrace

        Private Sub New()
        End Sub

        Public Shared Sub WriteLine(ByVal message As String, ByVal level As Meanstream.Portal.Core.Instrumentation.TraceLevel, ByVal displayMethodInfo As DisplayMethodInfo)
            If String.IsNullOrEmpty(message) Then
                message = "TRACE MESSAGE NULL"
            End If

            Dim callerSignature As String = GetMethodSignature(displayMethodInfo)

            Trace.WriteLine((String.Concat(message, callerSignature)), [Enum].GetName(GetType(Meanstream.Portal.Core.Instrumentation.TraceLevel), level))
        End Sub

        Public Shared Sub WriteLine(ByVal message As String, ByVal level As Meanstream.Portal.Core.Instrumentation.TraceLevel)
            WriteLine(message, level, DisplayMethodInfo.DoNotDisplay)
        End Sub

        Public Shared Sub WriteLine(ByVal message As String)
            WriteLine(message, Meanstream.Portal.Core.Instrumentation.TraceLevel.Information, DisplayMethodInfo.DoNotDisplay)
        End Sub

        Public Shared Sub Warning(ByVal message As String, ByVal displayMethodInfo As DisplayMethodInfo)
            WriteLine(message, Meanstream.Portal.Core.Instrumentation.TraceLevel.Warning, displayMethodInfo)
        End Sub

        Public Shared Sub Information(ByVal message As String, ByVal displayMethodInfo As DisplayMethodInfo)
            WriteLine(message, Meanstream.Portal.Core.Instrumentation.TraceLevel.Information, displayMethodInfo)
        End Sub

        Public Shared Sub Fail(ByVal message As String, ByVal displayMethodInfo As DisplayMethodInfo)
            WriteLine(message, Meanstream.Portal.Core.Instrumentation.TraceLevel.Fail, displayMethodInfo)
        End Sub

        Private Shared Function GetClassTraceLevel() As String
            ' Get the appropiate Stack Frame - we sholud skip 2 frames: the the first frame is this method, the second is the caller method from this class.
            ' Then get the method info of the caller method.
            Dim method As MethodBase = New StackFrame(2, False).GetMethod()

            Dim categoryName As String = Nothing

            Dim customAttributes As Object() = method.DeclaringType.GetCustomAttributes(GetType(TraceLevelAttribute), False)
            If customAttributes IsNot Nothing AndAlso customAttributes.Length = 1 Then
                Dim traceCategoryAttribute As TraceLevelAttribute = TryCast(customAttributes(0), TraceLevelAttribute)
                If traceCategoryAttribute IsNot Nothing Then
                    categoryName = traceCategoryAttribute.Name
                End If
            End If

            Return categoryName
        End Function

        Public Shared Sub CleanTracing()
            Dim params() As Object = {}
            Meanstream.Portal.Core.Data.DataRepository.Provider.ExecuteNonQuery("meanstream_CleanTracing", params)
        End Sub

        Public Shared Function GetTracing() As DataSet
            Dim objCommand As New System.Data.SqlClient.SqlCommand()
            Dim ds As New DataSet
            Try
                objCommand.CommandText = "select Id, TraceDateTime, TraceCategory, TraceDescription, StackTrace, DetailedErrorDescription from meanstream_tracing"
                objCommand.CommandType = CommandType.Text
                ds = Meanstream.Portal.Core.Data.DataRepository.Provider.ExecuteDataSet(objCommand)
            Catch e As Exception
            Finally
                objCommand = Nothing
            End Try
            Return ds
        End Function

        Private Shared Function GetMethodSignature(ByVal showMethodName__1 As DisplayMethodInfo) As String

            If showMethodName__1 = DisplayMethodInfo.DoNotDisplay Then
                Return String.Empty
            End If

            ' Get the appropiate Stack Frame - we sholud skip 2 frames: the the first frame is this method, the second is the caller method from this class.
            ' Then get the method info of the caller method.
            Dim method As MethodBase = New StackFrame(2, False).GetMethod()

            If showMethodName__1 = DisplayMethodInfo.NameOnly Then
                Return String.Concat(" [Method: ", method.Name, "()]")
            End If

            Dim signatureBuilder As System.Text.StringBuilder = New StringBuilder()

            signatureBuilder.Append(" [Method: ")

            If method.IsPrivate Then
                signatureBuilder.Append("private ")
            End If
            If method.IsPublic Then
                signatureBuilder.Append("public ")
            End If
            If method.IsStatic Then
                signatureBuilder.Append("static ")
            End If
            If method.IsAbstract Then
                signatureBuilder.Append("abstract ")
            End If
            If method.IsVirtual Then
                signatureBuilder.Append("virtual ")
            End If

            Dim mi As MethodInfo = TryCast(method, MethodInfo)
            Dim ci As ConstructorInfo = TryCast(method, ConstructorInfo)

            If mi IsNot Nothing Then
                signatureBuilder.Append(mi.ReturnType.Name)
            End If
            If ci IsNot Nothing Then
                signatureBuilder.Append(ci.DeclaringType.Name)
            End If

            signatureBuilder.Append(" ")
            signatureBuilder.Append(method.Name)

            If mi IsNot Nothing Then
                Dim genericArgumentTypes As Type() = mi.GetGenericArguments()

                If genericArgumentTypes IsNot Nothing AndAlso genericArgumentTypes.Length > 0 Then
                    signatureBuilder.Append("<")
                    Dim firstGenericArgument As Boolean = True
                    For Each genericArgumentType As Type In genericArgumentTypes
                        If Not firstGenericArgument Then
                            signatureBuilder.Append(", ")
                        Else
                            firstGenericArgument = False
                        End If

                        signatureBuilder.Append(genericArgumentType.Name)
                    Next
                    signatureBuilder.Append(">")
                End If
            End If

            signatureBuilder.Append("(")

            Dim firstParameter As Boolean = True
            For Each paremeter As ParameterInfo In method.GetParameters()
                If Not firstParameter Then
                    signatureBuilder.Append(", ")
                Else
                    firstParameter = False
                End If

                signatureBuilder.Append(paremeter.ParameterType.Name)
                signatureBuilder.Append(" ")
                signatureBuilder.Append(paremeter.Name)
            Next

            signatureBuilder.Append(")]")

            Return signatureBuilder.ToString()
        End Function
    End Class
End Namespace
