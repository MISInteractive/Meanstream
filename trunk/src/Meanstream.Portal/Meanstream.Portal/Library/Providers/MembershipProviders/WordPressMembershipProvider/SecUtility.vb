'------------------------------------------------------------------------------
' <copyright file="SecurityUtil.cs" company="Microsoft">
' Copyright (c) Microsoft Corporation. All rights reserved.
' </copyright>
'------------------------------------------------------------------------------

'
' * SecurityUtil class
' *
' * Copyright (c) 1999 Microsoft Corporation
' 


Imports System
Imports System.Globalization
Imports System.Web.Hosting
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Configuration.Provider
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports System.Xml
Namespace Meanstream.Portal.Providers.WordPressMembershipProvider

    Friend Module SecUtility
        Sub New()
        End Sub

        Friend Const Infinite As Integer = Int32.MaxValue
        Friend Function GetDefaultAppName() As String
            Try
                Dim appName As String = HostingEnvironment.ApplicationVirtualPath
                If [String].IsNullOrEmpty(appName) Then

                    appName = System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName

                    Dim indexOfDot As Integer = appName.IndexOf("."c)
                    If indexOfDot <> -1 Then
                        appName = appName.Remove(indexOfDot)
                    End If
                End If

                If [String].IsNullOrEmpty(appName) Then
                    Return "/"
                Else
                    Return appName
                End If
            Catch
                Return "/"
            End Try
        End Function

        ' We don't trim the param before checking with password parameters
        Friend Function ValidatePasswordParameter(ByRef param As String, ByVal maxSize As Integer) As Boolean
            If param Is Nothing Then
                Return False
            End If

            If param.Length < 1 Then
                Return False
            End If

            If maxSize > 0 AndAlso (param.Length > maxSize) Then
                Return False
            End If

            Return True
        End Function

        Friend Function ValidateParameter(ByRef param As String, ByVal checkForNull As Boolean, ByVal checkIfEmpty As Boolean, ByVal checkForCommas As Boolean, ByVal maxSize As Integer) As Boolean
            If param Is Nothing Then
                Return Not checkForNull
            End If

            param = param.Trim()
            If (checkIfEmpty AndAlso param.Length < 1) OrElse (maxSize > 0 AndAlso param.Length > maxSize) OrElse (checkForCommas AndAlso param.Contains(",")) Then
                Return False
            End If

            Return True
        End Function

        ' We don't trim the param before checking with password parameters
        Friend Sub CheckPasswordParameter(ByRef param As String, ByVal maxSize As Integer, ByVal paramName As String)
            If param Is Nothing Then
                Throw New ArgumentNullException(paramName)
            End If

            If param.Length < 1 Then
                Throw New ArgumentException(SR.GetString(SR.Parameter_can_not_be_empty, paramName), paramName)
            End If

            If maxSize > 0 AndAlso param.Length > maxSize Then
                Throw New ArgumentException(SR.GetString(SR.Parameter_too_long, paramName, maxSize.ToString(CultureInfo.InvariantCulture)), paramName)
            End If
        End Sub

        Friend Sub CheckParameter(ByRef param As String, ByVal checkForNull As Boolean, ByVal checkIfEmpty As Boolean, ByVal checkForCommas As Boolean, ByVal maxSize As Integer, ByVal paramName As String)
            If param Is Nothing Then
                If checkForNull Then
                    Throw New ArgumentNullException(paramName)
                End If

                Exit Sub
            End If

            param = param.Trim()
            If checkIfEmpty AndAlso param.Length < 1 Then
                Throw New ArgumentException(SR.GetString(SR.Parameter_can_not_be_empty, paramName), paramName)
            End If

            If maxSize > 0 AndAlso param.Length > maxSize Then
                Throw New ArgumentException(SR.GetString(SR.Parameter_too_long, paramName, maxSize.ToString(CultureInfo.InvariantCulture)), paramName)
            End If

            If checkForCommas AndAlso param.Contains(",") Then
                Throw New ArgumentException(SR.GetString(SR.Parameter_can_not_contain_comma, paramName), paramName)
            End If
        End Sub

        Friend Sub CheckArrayParameter(ByRef param As String(), ByVal checkForNull As Boolean, ByVal checkIfEmpty As Boolean, ByVal checkForCommas As Boolean, ByVal maxSize As Integer, ByVal paramName As String)
            If param Is Nothing Then
                Throw New ArgumentNullException(paramName)
            End If

            If param.Length < 1 Then
                Throw New ArgumentException(SR.GetString(SR.Parameter_array_empty, paramName), paramName)
            End If

            Dim values As New Hashtable(param.Length)
            For i As Integer = param.Length - 1 To 0 Step -1
                SecUtility.CheckParameter(param(i), checkForNull, checkIfEmpty, checkForCommas, maxSize, (paramName & "[ ") + i.ToString(CultureInfo.InvariantCulture) & " ]")
                If values.Contains(param(i)) Then
                    Throw New ArgumentException(SR.GetString(SR.Parameter_duplicate_array_element, paramName), paramName)
                Else
                    values.Add(param(i), param(i))
                End If
            Next
        End Sub

        Friend Function GetBooleanValue(ByVal config As NameValueCollection, ByVal valueName As String, ByVal defaultValue As Boolean) As Boolean
            Dim sValue As String = config(valueName)
            If sValue Is Nothing Then
                Return defaultValue
            End If

            Dim result As Boolean
            If Boolean.TryParse(sValue, result) Then
                Return result
            Else
                Throw New ProviderException(SR.GetString(SR.Value_must_be_boolean, valueName))
            End If
        End Function

        Friend Function GetIntValue(ByVal config As NameValueCollection, ByVal valueName As String, ByVal defaultValue As Integer, ByVal zeroAllowed As Boolean, ByVal maxValueAllowed As Integer) As Integer
            Dim sValue As String = config(valueName)

            If sValue Is Nothing Then
                Return defaultValue
            End If

            Dim iValue As Integer
            If Not Int32.TryParse(sValue, iValue) Then
                If zeroAllowed Then
                    Throw New ProviderException(SR.GetString(SR.Value_must_be_non_negative_integer, valueName))
                End If

                Throw New ProviderException(SR.GetString(SR.Value_must_be_positive_integer, valueName))
            End If

            If zeroAllowed AndAlso iValue < 0 Then
                Throw New ProviderException(SR.GetString(SR.Value_must_be_non_negative_integer, valueName))
            End If

            If Not zeroAllowed AndAlso iValue <= 0 Then
                Throw New ProviderException(SR.GetString(SR.Value_must_be_positive_integer, valueName))
            End If

            If maxValueAllowed > 0 AndAlso iValue > maxValueAllowed Then
                Throw New ProviderException(SR.GetString(SR.Value_too_big, valueName, maxValueAllowed.ToString(CultureInfo.InvariantCulture)))
            End If

            Return iValue
        End Function

        Private Function IsDirectorySeparatorChar(ByVal ch As Char) As Boolean
            Return (ch = "\"c OrElse ch = "/"c)
        End Function

        Friend Function IsAbsolutePhysicalPath(ByVal path As String) As Boolean
            If path Is Nothing OrElse path.Length < 3 Then
                Return False
            End If

            ' e.g c:\foo
            If path(1) = ":"c AndAlso IsDirectorySeparatorChar(path(2)) Then
                Return True
            End If

            ' e.g \\server\share\foo or //server/share/foo
            Return IsUncSharePath(path)
        End Function

        Friend Function IsUncSharePath(ByVal path As String) As Boolean
            ' e.g \\server\share\foo or //server/share/foo
            If path.Length > 2 AndAlso IsDirectorySeparatorChar(path(0)) AndAlso IsDirectorySeparatorChar(path(1)) Then
                Return True
            End If

            Return False
        End Function

        Friend Sub CheckSchemaVersion(ByVal provider As ProviderBase, ByVal connection As SqlConnection, ByVal features As String(), ByVal version As String, ByRef schemaVersionCheck As Integer)
            If connection Is Nothing Then
                Throw New ArgumentNullException("connection")
            End If

            If features Is Nothing Then
                Throw New ArgumentNullException("features")
            End If

            If version Is Nothing Then
                Throw New ArgumentNullException("version")
            End If

            If schemaVersionCheck = -1 Then
                Throw New ProviderException(SR.GetString(SR.Provider_Schema_Version_Not_Match, provider.ToString(), version))
            ElseIf schemaVersionCheck = 0 Then
                SyncLock provider
                    If schemaVersionCheck = -1 Then
                        Throw New ProviderException(SR.GetString(SR.Provider_Schema_Version_Not_Match, provider.ToString(), version))
                    ElseIf schemaVersionCheck = 0 Then
                        Dim iStatus As Integer = 0
                        Dim cmd As SqlCommand = Nothing
                        Dim p As SqlParameter = Nothing

                        For Each feature As String In features
                            cmd = New SqlCommand("dbo.aspnet_CheckSchemaVersion", connection)

                            cmd.CommandType = CommandType.StoredProcedure

                            p = New SqlParameter("@Feature", feature)
                            cmd.Parameters.Add(p)

                            p = New SqlParameter("@CompatibleSchemaVersion", version)
                            cmd.Parameters.Add(p)

                            p = New SqlParameter("@ReturnValue", SqlDbType.Int)
                            p.Direction = ParameterDirection.ReturnValue
                            cmd.Parameters.Add(p)

                            cmd.ExecuteNonQuery()

                            iStatus = (If((p.Value IsNot Nothing), CInt(p.Value), -1))
                            If iStatus <> 0 Then
                                schemaVersionCheck = -1

                                Throw New ProviderException(SR.GetString(SR.Provider_Schema_Version_Not_Match, provider.ToString(), version))
                            End If
                        Next

                        schemaVersionCheck = 1
                    End If
                End SyncLock
            End If
        End Sub

        Friend Function GetAndRemoveBooleanAttribute(ByVal node As XmlNode, ByVal attrib As String, ByRef val As Boolean) As XmlNode
            'fRequired
            Return GetAndRemoveBooleanAttributeInternal(node, attrib, False, val)
        End Function

        ' input.Xml cursor must be at a true/false XML attribute
        Private Function GetAndRemoveBooleanAttributeInternal(ByVal node As XmlNode, ByVal attrib As String, ByVal fRequired As Boolean, ByRef val As Boolean) As XmlNode
            Dim a As XmlNode = GetAndRemoveAttribute(node, attrib, fRequired)
            If a IsNot Nothing Then
                If a.Value = "true" Then
                    val = True
                ElseIf a.Value = "false" Then
                    val = False
                Else
                    Throw New ConfigurationErrorsException(SR.GetString(SR.Invalid_boolean_attribute, a.Name), a)
                End If
            End If

            Return a
        End Function

        Private Function GetAndRemoveAttribute(ByVal node As XmlNode, ByVal attrib As String, ByVal fRequired As Boolean) As XmlNode
            Dim a As XmlNode = node.Attributes.RemoveNamedItem(attrib)

            ' If the attribute is required and was not present, throw
            If fRequired AndAlso a Is Nothing Then
                Throw New ConfigurationErrorsException(SR.GetString(SR.Missing_required_attribute, attrib, node.Name), node)
            End If

            Return a
        End Function

        Friend Function GetAndRemoveNonEmptyStringAttribute(ByVal node As XmlNode, ByVal attrib As String, ByRef val As String) As XmlNode
            'fRequired
            Return GetAndRemoveNonEmptyStringAttributeInternal(node, attrib, False, val)
        End Function

        Private Function GetAndRemoveNonEmptyStringAttributeInternal(ByVal node As XmlNode, ByVal attrib As String, ByVal fRequired As Boolean, ByRef val As String) As XmlNode
            Dim a As XmlNode = GetAndRemoveStringAttributeInternal(node, attrib, fRequired, val)
            If a IsNot Nothing AndAlso val.Length = 0 Then
                Throw New ConfigurationErrorsException(SR.GetString(SR.Empty_attribute, attrib), a)
            End If

            Return a
        End Function

        Private Function GetAndRemoveStringAttributeInternal(ByVal node As XmlNode, ByVal attrib As String, ByVal fRequired As Boolean, ByRef val As String) As XmlNode
            Dim a As XmlNode = GetAndRemoveAttribute(node, attrib, fRequired)
            If a IsNot Nothing Then
                val = a.Value
            End If

            Return a
        End Function

        Friend Sub CheckForUnrecognizedAttributes(ByVal node As XmlNode)
            If node.Attributes.Count <> 0 Then
                Throw New ConfigurationErrorsException(SR.GetString(SR.Config_base_unrecognized_attribute, node.Attributes(0).Name), node.Attributes(0))
            End If
        End Sub

        Friend Sub CheckForNonCommentChildNodes(ByVal node As XmlNode)
            For Each childNode As XmlNode In node.ChildNodes
                If childNode.NodeType <> XmlNodeType.Comment Then
                    Throw New ConfigurationErrorsException(SR.GetString(SR.Config_base_no_child_nodes), childNode)
                End If
            Next
        End Sub

        Friend Function GetAndRemoveStringAttribute(ByVal node As XmlNode, ByVal attrib As String, ByRef val As String) As XmlNode
            'fRequired
            Return GetAndRemoveStringAttributeInternal(node, attrib, False, val)
        End Function

        Friend Sub CheckForbiddenAttribute(ByVal node As XmlNode, ByVal attrib As String)
            Dim attr As XmlAttribute = node.Attributes(attrib)
            If attr IsNot Nothing Then
                Throw New ConfigurationErrorsException(SR.GetString(SR.Config_base_unrecognized_attribute, attrib), attr)
            End If
        End Sub

        ' Returns whether the virtual path is relative. Note that this returns true for
        ' app relative paths (e.g. "~/sub/foo.aspx")
        Friend Function IsRelativeUrl(ByVal virtualPath As String) As Boolean
            ' If it has a protocol, it's not relative
            If virtualPath.IndexOf(":", StringComparison.Ordinal) <> -1 Then
                Return False
            End If

            Return Not IsRooted(virtualPath)
        End Function

        Friend Function IsRooted(ByVal basepath As [String]) As Boolean
            Return ([String].IsNullOrEmpty(basepath) OrElse basepath(0) = "/"c OrElse basepath(0) = "\"c)
        End Function

        Friend Sub GetAndRemoveStringAttribute(ByVal config As NameValueCollection, ByVal attrib As String, ByVal providerName As String, ByRef val As String)
            val = config.[Get](attrib)
            config.Remove(attrib)
        End Sub

        Friend Sub CheckUnrecognizedAttributes(ByVal config As NameValueCollection, ByVal providerName As String)
            If config.Count > 0 Then
                Dim attribUnrecognized As String = config.GetKey(0)
                If Not [String].IsNullOrEmpty(attribUnrecognized) Then
                    Throw New ConfigurationErrorsException(SR.GetString(SR.Unexpected_provider_attribute, attribUnrecognized, providerName))
                End If
            End If
        End Sub

        Friend Function GetStringFromBool(ByVal flag As Boolean) As String
            Return If(flag, "true", "false")
        End Function
        Friend Sub GetAndRemovePositiveOrInfiniteAttribute(ByVal config As NameValueCollection, ByVal attrib As String, ByVal providerName As String, ByRef val As Integer)
            GetPositiveOrInfiniteAttribute(config, attrib, providerName, val)
            config.Remove(attrib)
        End Sub

        Friend Sub GetPositiveOrInfiniteAttribute(ByVal config As NameValueCollection, ByVal attrib As String, ByVal providerName As String, ByRef val As Integer)
            Dim s As String = config.[Get](attrib)
            Dim t As Integer

            If s Is Nothing Then
                Exit Sub
            End If

            If s = "Infinite" Then
                t = Infinite
            Else
                Try
                    t = Convert.ToInt32(s, CultureInfo.InvariantCulture)
                Catch e As Exception
                    If TypeOf e Is ArgumentException OrElse TypeOf e Is FormatException OrElse TypeOf e Is OverflowException Then
                        Throw New ConfigurationErrorsException(SR.GetString(SR.Invalid_provider_positive_attributes, attrib, providerName))
                    Else
                        Throw

                    End If
                End Try

                If t < 0 Then

                    Throw New ConfigurationErrorsException(SR.GetString(SR.Invalid_provider_positive_attributes, attrib, providerName))
                End If
            End If

            val = t
        End Sub

        Friend Sub GetAndRemovePositiveAttribute(ByVal config As NameValueCollection, ByVal attrib As String, ByVal providerName As String, ByRef val As Integer)
            GetPositiveAttribute(config, attrib, providerName, val)
            config.Remove(attrib)
        End Sub

        Friend Sub GetPositiveAttribute(ByVal config As NameValueCollection, ByVal attrib As String, ByVal providerName As String, ByRef val As Integer)
            Dim s As String = config.[Get](attrib)
            Dim t As Integer

            If s Is Nothing Then
                Exit Sub
            End If

            Try
                t = Convert.ToInt32(s, CultureInfo.InvariantCulture)
            Catch e As Exception
                If TypeOf e Is ArgumentException OrElse TypeOf e Is FormatException OrElse TypeOf e Is OverflowException Then
                    Throw New ConfigurationErrorsException(SR.GetString(SR.Invalid_provider_positive_attributes, attrib, providerName))
                Else
                    Throw

                End If
            End Try

            If t < 0 Then

                Throw New ConfigurationErrorsException(SR.GetString(SR.Invalid_provider_positive_attributes, attrib, providerName))
            End If

            val = t
        End Sub
    End Module
End Namespace
