'------------------------------------------------------------------------------
' <copyright file="SqlProfileProvider.cs" company="Microsoft">
' Copyright (c) Microsoft Corporation. All rights reserved.
' </copyright>
'------------------------------------------------------------------------------

Imports System
Imports System.Web.Profile
Imports System.Web
Imports System.Web.Configuration
Imports System.Security.Principal
Imports System.Security.Permissions
Imports System.Globalization
Imports System.Runtime.Serialization
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports System.Reflection
Imports System.Xml.Serialization
Imports System.Text
Imports System.Configuration.Provider
Imports System.Configuration
Imports System.Web.Hosting
Imports System.Web.Util

Namespace Meanstream.Portal.Providers.WordPressMembershipProvider

    ' Remove CAS from sample: [AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    ' Remove CAS from sample: [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    Public Class SqlProfileProvider
        Inherits ProfileProvider
        Private _AppName As String
        Private _sqlConnectionString As String
        Private _SchemaVersionCheck As Integer
        Private _CommandTimeout As Integer


        Public Overloads Overrides Sub Initialize(ByVal name As String, ByVal config As NameValueCollection)
            ' Remove CAS in sample: HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, SR.Feature_not_supported_at_this_level);
            If config Is Nothing Then
                Throw New ArgumentNullException("config")
            End If
            If name Is Nothing OrElse name.Length < 1 Then
                name = "SqlProfileProvider"
            End If
            If String.IsNullOrEmpty(config("description")) Then
                config.Remove("description")
                config.Add("description", SR.GetString(SR.ProfileSqlProvider_description))
            End If
            MyBase.Initialize(name, config)

            _SchemaVersionCheck = 0

            Dim temp As String = config("connectionStringName")
            If temp Is Nothing OrElse temp.Length < 1 Then
                Throw New ProviderException(SR.GetString(SR.Connection_name_not_specified))
            End If
            _sqlConnectionString = SqlConnectionHelper.GetConnectionString(temp, True, True)
            If _sqlConnectionString Is Nothing OrElse _sqlConnectionString.Length < 1 Then
                Throw New ProviderException(SR.GetString(SR.Connection_string_not_found, temp))
            End If

            _AppName = config("applicationName")
            If String.IsNullOrEmpty(_AppName) Then
                _AppName = SecUtility.GetDefaultAppName()
            End If

            If _AppName.Length > 256 Then
                Throw New ProviderException(SR.GetString(SR.Provider_application_name_too_long))
            End If

            _CommandTimeout = SecUtility.GetIntValue(config, "commandTimeout", 30, True, 0)

            config.Remove("commandTimeout")
            config.Remove("connectionStringName")
            config.Remove("applicationName")
            If config.Count > 0 Then
                Dim attribUnrecognized As String = config.GetKey(0)
                If Not [String].IsNullOrEmpty(attribUnrecognized) Then
                    Throw New ProviderException(SR.GetString(SR.Provider_unrecognized_attribute, attribUnrecognized))
                End If
            End If
        End Sub

        Private Sub CheckSchemaVersion(ByVal connection As SqlConnection)
            Dim features As String() = {"Profile"}
            Dim version As String = "1"

            SecUtility.CheckSchemaVersion(Me, connection, features, version, _SchemaVersionCheck)
        End Sub


        Public Overloads Overrides Property ApplicationName() As String
            Get
                Return _AppName
            End Get
            Set(ByVal value As String)
                If value.Length > 256 Then
                    Throw New ProviderException(SR.GetString(SR.Provider_application_name_too_long))
                End If

                _AppName = value
            End Set
        End Property

        Private ReadOnly Property CommandTimeout() As Integer
            Get
                Return _CommandTimeout
            End Get
        End Property

        '''/////////////////////////////////////////////////////////
        '''/////////////////////////////////////////////////////////
        '''/////////////////////////////////////////////////////////

        Public Overloads Overrides Function GetPropertyValues(ByVal sc As SettingsContext, ByVal properties As SettingsPropertyCollection) As SettingsPropertyValueCollection
            Dim svc As New SettingsPropertyValueCollection()

            If properties.Count < 1 Then
                Return svc
            End If

            Dim username As String = DirectCast(sc("UserName"), String)

            For Each prop As SettingsProperty In properties
                If prop.SerializeAs = SettingsSerializeAs.ProviderSpecific Then
                    If prop.PropertyType.IsPrimitive OrElse prop.PropertyType Is GetType(String) Then
                        prop.SerializeAs = SettingsSerializeAs.[String]
                    Else
                        prop.SerializeAs = SettingsSerializeAs.Xml
                    End If
                End If

                svc.Add(New SettingsPropertyValue(prop))
            Next
            If Not [String].IsNullOrEmpty(username) Then
                GetPropertyValuesFromDatabase(username, svc)
            End If
            Return svc
        End Function

        '''/////////////////////////////////////////////////////////
        '''/////////////////////////////////////////////////////////
        Private Sub GetPropertyValuesFromDatabase(ByVal userName As String, ByVal svc As SettingsPropertyValueCollection)
            ' Comment out events in sample: if (HostingEnvironment.IsHosted && EtwTrace.IsTraceEnabled(EtwTraceLevel.Information, EtwTraceFlags.AppSvc)) EtwTrace.Trace(EtwTraceType.ETW_TYPE_PROFILE_BEGIN, HttpContext.Current.WorkerRequest);

            Dim context As HttpContext = HttpContext.Current
            Dim names As String() = Nothing
            Dim values As String = Nothing
            Dim buf As Byte() = Nothing
            Dim sName As String = Nothing

            If context IsNot Nothing Then
                sName = (If(context.Request.IsAuthenticated, context.User.Identity.Name, context.Request.AnonymousID))
            End If

            Try
                Dim holder As SqlConnectionHolder = Nothing
                Dim reader As SqlDataReader = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)
                    Dim cmd As New SqlCommand("dbo.aspnet_Profile_GetProperties", holder.Connection)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, userName))
                    cmd.Parameters.Add(CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))
                    reader = cmd.ExecuteReader(CommandBehavior.SingleRow)
                    If reader.Read() Then
                        names = reader.GetString(0).Split(":"c)
                        values = reader.GetString(1)

                        Dim size As Integer = CInt(reader.GetBytes(2, 0, Nothing, 0, 0))

                        buf = New Byte(size - 1) {}
                        reader.GetBytes(2, 0, buf, 0, size)
                    End If
                Finally
                    If holder IsNot Nothing Then
                        holder.Close()
                        holder = Nothing
                    End If

                    If reader IsNot Nothing Then
                        reader.Close()
                    End If
                End Try


                ' Comment out events in sample: if (HostingEnvironment.IsHosted && EtwTrace.IsTraceEnabled(EtwTraceLevel.Information, EtwTraceFlags.AppSvc)) EtwTrace.Trace(EtwTraceType.ETW_TYPE_PROFILE_END, HttpContext.Current.WorkerRequest, userName);
                ParseDataFromDB(names, values, buf, svc)
            Catch
                Throw
            End Try
        End Sub


        '''/////////////////////////////////////////////////////////
        '''/////////////////////////////////////////////////////////
        '''/////////////////////////////////////////////////////////

        Public Overloads Overrides Sub SetPropertyValues(ByVal sc As SettingsContext, ByVal properties As SettingsPropertyValueCollection)
            Dim username As String = DirectCast(sc("UserName"), String)
            Dim userIsAuthenticated As Boolean = CBool(sc("IsAuthenticated"))

            If username Is Nothing OrElse username.Length < 1 OrElse properties.Count < 1 Then
                Exit Sub
            End If

            Dim names As String = [String].Empty
            Dim values As String = [String].Empty
            Dim buf As Byte() = Nothing

            PrepareDataForSaving(names, values, buf, True, properties, userIsAuthenticated)
            If names.Length = 0 Then
                Exit Sub
            End If

            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Profile_SetProperties", holder.Connection)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, username))
                    cmd.Parameters.Add(CreateInputParam("@PropertyNames", SqlDbType.NText, names))
                    cmd.Parameters.Add(CreateInputParam("@PropertyValuesString", SqlDbType.NText, values))
                    cmd.Parameters.Add(CreateInputParam("@PropertyValuesBinary", SqlDbType.Image, buf))
                    cmd.Parameters.Add(CreateInputParam("@IsUserAnonymous", SqlDbType.Bit, Not userIsAuthenticated))
                    cmd.Parameters.Add(CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))
                    cmd.ExecuteNonQuery()

                    'update wordpress default properties
                    cmd = New SqlCommand("dbo.meanstream_WordPress_SetUserProperties", holder.Connection)
                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, username))
                    cmd.ExecuteNonQuery()
                    'end wp

                Finally
                    If holder IsNot Nothing Then
                        holder.Close()
                        holder = Nothing
                    End If
                End Try
            Catch
                Throw
            End Try
        End Sub

        '''/////////////////////////////////////////////////////////
        '''/////////////////////////////////////////////////////////
        '''/////////////////////////////////////////////////////////

        Private Function CreateInputParam(ByVal paramName As String, ByVal dbType As SqlDbType, ByVal objValue As Object) As SqlParameter
            Dim param As New SqlParameter(paramName, dbType)
            If objValue Is Nothing Then
                objValue = [String].Empty
            End If
            param.Value = objValue
            Return param
        End Function


        ' Mangement APIs from ProfileProvider class

        Public Overloads Overrides Function DeleteProfiles(ByVal profiles As ProfileInfoCollection) As Integer
            If profiles Is Nothing Then
                Throw New ArgumentNullException("profiles")
            End If

            If profiles.Count < 1 Then
                Throw New ArgumentException(SR.GetString(SR.Parameter_collection_empty, "profiles"), "profiles")
            End If

            Dim usernames As String() = New String(profiles.Count - 1) {}

            Dim iter As Integer = 0
            For Each profile As ProfileInfo In profiles
                usernames(System.Math.Max(System.Threading.Interlocked.Increment(iter), iter - 1)) = profile.UserName
            Next

            Return DeleteProfiles(usernames)
        End Function
        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////
        Public Overloads Overrides Function DeleteProfiles(ByVal usernames As String()) As Integer
            SecUtility.CheckArrayParameter(usernames, True, True, True, 256, "usernames")

            Dim numProfilesDeleted As Integer = 0
            Dim beginTranCalled As Boolean = False
            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As SqlCommand

                    Dim numUsersRemaing As Integer = usernames.Length
                    While numUsersRemaing > 0
                        Dim allUsers As String = usernames(usernames.Length - numUsersRemaing)
                        numUsersRemaing -= 1
                        For iter As Integer = usernames.Length - numUsersRemaing To usernames.Length - 1
                            If allUsers.Length + usernames(iter).Length + 1 >= 4000 Then
                                Exit For
                            End If
                            allUsers += "," & usernames(iter)
                            numUsersRemaing -= 1
                        Next

                        ' We don't need to start a transaction if we can finish this in one sql command
                        '
                        ' Note: ADO.NET 2.0 introduced the TransactionScope class - in your own code you should use TransactionScope
                        ' rather than explicitly managing transactions with the TSQL BEGIN/COMMIT/ROLLBACK statements.
                        '
                        If Not beginTranCalled AndAlso numUsersRemaing > 0 Then
                            cmd = New SqlCommand("BEGIN TRANSACTION", holder.Connection)
                            cmd.ExecuteNonQuery()
                            beginTranCalled = True
                        End If

                        cmd = New SqlCommand("dbo.aspnet_Profile_DeleteProfiles", holder.Connection)

                        cmd.CommandTimeout = CommandTimeout
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                        cmd.Parameters.Add(CreateInputParam("@UserNames", SqlDbType.NVarChar, allUsers))
                        Dim o As Object = cmd.ExecuteScalar()
                        If o IsNot Nothing AndAlso TypeOf o Is Integer Then
                            numProfilesDeleted += CInt(o)

                        End If
                    End While

                    If beginTranCalled Then
                        cmd = New SqlCommand("COMMIT TRANSACTION", holder.Connection)
                        cmd.ExecuteNonQuery()
                        beginTranCalled = False
                    End If
                Catch
                    If beginTranCalled Then
                        Dim cmd As New SqlCommand("ROLLBACK TRANSACTION", holder.Connection)
                        cmd.ExecuteNonQuery()
                        beginTranCalled = False
                    End If
                    Throw
                Finally
                    If holder IsNot Nothing Then
                        holder.Close()
                        holder = Nothing
                    End If
                End Try
            Catch
                Throw
            End Try
            Return numProfilesDeleted
        End Function

        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////
        Public Overloads Overrides Function DeleteInactiveProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal userInactiveSinceDate As DateTime) As Integer
            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Profile_DeleteInactiveProfiles", holder.Connection)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@ProfileAuthOptions", SqlDbType.Int, CInt(authenticationOption)))
                    cmd.Parameters.Add(CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime()))
                    Dim o As Object = cmd.ExecuteScalar()
                    If o Is Nothing OrElse Not (TypeOf o Is Integer) Then
                        Return 0
                    End If
                    Return CInt(o)
                Finally
                    If holder IsNot Nothing Then
                        holder.Close()
                        holder = Nothing
                    End If
                End Try
            Catch
                Throw
            End Try
        End Function
        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////
        Public Overloads Overrides Function GetNumberOfInactiveProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal userInactiveSinceDate As DateTime) As Integer
            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Profile_GetNumberOfInactiveProfiles", holder.Connection)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@ProfileAuthOptions", SqlDbType.Int, CInt(authenticationOption)))
                    cmd.Parameters.Add(CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime()))
                    Dim o As Object = cmd.ExecuteScalar()
                    If o Is Nothing OrElse Not (TypeOf o Is Integer) Then
                        Return 0
                    End If
                    Return CInt(o)
                Finally
                    If holder IsNot Nothing Then
                        holder.Close()
                        holder = Nothing
                    End If
                End Try
            Catch
                Throw
            End Try
        End Function

        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////
        Public Overloads Overrides Function GetAllProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As ProfileInfoCollection
            Return GetProfilesForQuery(New SqlParameter(-1) {}, authenticationOption, pageIndex, pageSize, totalRecords)
        End Function


        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////
        Public Overloads Overrides Function GetAllInactiveProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal userInactiveSinceDate As DateTime, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As ProfileInfoCollection
            Dim args As SqlParameter() = New SqlParameter(0) {}
            args(0) = CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime())
            Return GetProfilesForQuery(args, authenticationOption, pageIndex, pageSize, totalRecords)
        End Function
        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////
        Public Overloads Overrides Function FindProfilesByUserName(ByVal authenticationOption As ProfileAuthenticationOption, ByVal usernameToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As ProfileInfoCollection
            SecUtility.CheckParameter(usernameToMatch, True, True, False, 256, "username")
            Dim args As SqlParameter() = New SqlParameter(0) {}
            args(0) = CreateInputParam("@UserNameToMatch", SqlDbType.NVarChar, usernameToMatch)
            Return GetProfilesForQuery(args, authenticationOption, pageIndex, pageSize, totalRecords)
        End Function

        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////
        Public Overloads Overrides Function FindInactiveProfilesByUserName(ByVal authenticationOption As ProfileAuthenticationOption, ByVal usernameToMatch As String, ByVal userInactiveSinceDate As DateTime, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As ProfileInfoCollection
            SecUtility.CheckParameter(usernameToMatch, True, True, False, 256, "username")
            Dim args As SqlParameter() = New SqlParameter(1) {}
            args(0) = CreateInputParam("@UserNameToMatch", SqlDbType.NVarChar, usernameToMatch)
            args(1) = CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime())
            Return GetProfilesForQuery(args, authenticationOption, pageIndex, pageSize, totalRecords)
        End Function


        ' Remove CAS in sample: [SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
        Private Shared Sub ParseDataFromDB(ByVal names As String(), ByVal values As String, ByVal buf As Byte(), ByVal properties As SettingsPropertyValueCollection)
            If names Is Nothing OrElse values Is Nothing OrElse buf Is Nothing OrElse properties Is Nothing Then
                Exit Sub
            End If
            Try
                For iter As Integer = 0 To names.Length / 4 - 1
                    Dim name As String = names(iter * 4)
                    Dim pp As SettingsPropertyValue = properties(name)

                    If pp Is Nothing Then
                        ' property not found
                        Continue For
                    End If

                    Dim startPos As Integer = Int32.Parse(names(iter * 4 + 2), CultureInfo.InvariantCulture)
                    Dim length As Integer = Int32.Parse(names(iter * 4 + 3), CultureInfo.InvariantCulture)

                    If length = -1 AndAlso Not pp.[Property].PropertyType.IsValueType Then
                        ' Null Value
                        pp.PropertyValue = Nothing
                        pp.IsDirty = False
                        pp.Deserialized = True
                    End If
                    If names(iter * 4 + 1) = "S" AndAlso startPos >= 0 AndAlso length > 0 AndAlso values.Length >= startPos + length Then
                        pp.SerializedValue = values.Substring(startPos, length)
                    End If

                    If names(iter * 4 + 1) = "B" AndAlso startPos >= 0 AndAlso length > 0 AndAlso buf.Length >= startPos + length Then
                        Dim buf2 As Byte() = New Byte(length - 1) {}

                        Buffer.BlockCopy(buf, startPos, buf2, 0, length)
                        pp.SerializedValue = buf2
                    End If
                Next
            Catch
                ' Eat exceptions
            End Try
        End Sub
        ' Remove CAS in sample: [SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
        Private Shared Sub PrepareDataForSaving(ByRef allNames As String, ByRef allValues As String, ByRef buf As Byte(), ByVal binarySupported As Boolean, ByVal properties As SettingsPropertyValueCollection, ByVal userIsAuthenticated As Boolean)
            Dim names As New StringBuilder()
            Dim values As New StringBuilder()

            Dim ms As MemoryStream = (If(binarySupported, New System.IO.MemoryStream(), Nothing))
            Try
                Try
                    Dim anyItemsToSave As Boolean = False

                    For Each pp As SettingsPropertyValue In properties
                        If pp.IsDirty Then
                            If Not userIsAuthenticated Then
                                Dim allowAnonymous As Boolean = CBool(pp.[Property].Attributes("AllowAnonymous"))
                                If Not allowAnonymous Then
                                    Continue For
                                End If
                            End If
                            anyItemsToSave = True
                            Exit For
                        End If
                    Next

                    If Not anyItemsToSave Then
                        Exit Sub
                    End If

                    For Each pp As SettingsPropertyValue In properties
                        If Not userIsAuthenticated Then
                            Dim allowAnonymous As Boolean = CBool(pp.[Property].Attributes("AllowAnonymous"))
                            If Not allowAnonymous Then
                                Continue For
                            End If
                        End If

                        If Not pp.IsDirty AndAlso pp.UsingDefaultValue Then
                            ' Not fetched from DB and not written to
                            Continue For
                        End If

                        Dim len As Integer = 0, startPos As Integer = 0
                        Dim propValue As String = Nothing

                        If pp.Deserialized AndAlso pp.PropertyValue Is Nothing Then
                            ' is value null?
                            len = -1
                        Else
                            Dim sVal As Object = pp.SerializedValue

                            If sVal Is Nothing Then
                                len = -1
                            Else
                                If Not (TypeOf sVal Is String) AndAlso Not binarySupported Then
                                    sVal = Convert.ToBase64String(DirectCast(sVal, Byte()))
                                End If

                                If TypeOf sVal Is String Then
                                    propValue = DirectCast(sVal, String)
                                    len = propValue.Length
                                    startPos = values.Length
                                Else
                                    Dim b2 As Byte() = DirectCast(sVal, Byte())
                                    startPos = CInt(ms.Position)
                                    ms.Write(b2, 0, b2.Length)
                                    ms.Position = startPos + b2.Length
                                    len = b2.Length
                                End If
                            End If
                        End If

                        names.Append((((pp.Name & ":") + (If((propValue IsNot Nothing), "S", "B")) & ":") + startPos.ToString(CultureInfo.InvariantCulture) & ":") + len.ToString(CultureInfo.InvariantCulture) & ":")
                        If propValue IsNot Nothing Then
                            values.Append(propValue)
                        End If
                    Next

                    If binarySupported Then
                        buf = ms.ToArray()
                    End If
                Finally
                    If ms IsNot Nothing Then
                        ms.Close()
                    End If
                End Try
            Catch
                Throw
            End Try
            allNames = names.ToString()
            allValues = values.ToString()
        End Sub

        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////
        Private Function GetProfilesForQuery(ByVal args As SqlParameter(), ByVal authenticationOption As ProfileAuthenticationOption, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As ProfileInfoCollection
            If pageIndex < 0 Then
                Throw New ArgumentException(SR.GetString(SR.PageIndex_bad), "pageIndex")
            End If
            If pageSize < 1 Then
                Throw New ArgumentException(SR.GetString(SR.PageSize_bad), "pageSize")
            End If

            Dim upperBound As Long = CLng(pageIndex) * pageSize + pageSize - 1
            If upperBound > Int32.MaxValue Then
                Throw New ArgumentException(SR.GetString(SR.PageIndex_PageSize_bad), "pageIndex and pageSize")
            End If

            Try
                Dim holder As SqlConnectionHolder = Nothing
                Dim reader As SqlDataReader = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Profile_GetProfiles", holder.Connection)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@ProfileAuthOptions", SqlDbType.Int, CInt(authenticationOption)))
                    cmd.Parameters.Add(CreateInputParam("@PageIndex", SqlDbType.Int, pageIndex))
                    cmd.Parameters.Add(CreateInputParam("@PageSize", SqlDbType.Int, pageSize))
                    For Each arg As SqlParameter In args
                        cmd.Parameters.Add(arg)
                    Next
                    reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
                    Dim profiles As New ProfileInfoCollection()
                    While reader.Read()
                        Dim username As String
                        Dim dtLastActivity As DateTime, dtLastUpdated As DateTime
                        Dim isAnon As Boolean

                        username = reader.GetString(0)
                        isAnon = reader.GetBoolean(1)
                        dtLastActivity = DateTime.SpecifyKind(reader.GetDateTime(2), DateTimeKind.Utc)
                        dtLastUpdated = DateTime.SpecifyKind(reader.GetDateTime(3), DateTimeKind.Utc)
                        Dim size As Integer = reader.GetInt32(4)
                        profiles.Add(New ProfileInfo(username, isAnon, dtLastActivity, dtLastUpdated, size))
                    End While
                    totalRecords = profiles.Count
                    If reader.NextResult() Then
                        If reader.Read() Then
                            totalRecords = reader.GetInt32(0)
                        End If
                    End If
                    Return profiles
                Finally
                    If reader IsNot Nothing Then
                        reader.Close()
                    End If

                    If holder IsNot Nothing Then
                        holder.Close()
                        holder = Nothing
                    End If
                End Try
            Catch
                Throw
            End Try
        End Function
    End Class
End Namespace
