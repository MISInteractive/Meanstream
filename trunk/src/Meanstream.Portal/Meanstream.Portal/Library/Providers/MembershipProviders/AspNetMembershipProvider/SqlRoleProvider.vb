'------------------------------------------------------------------------------
' <copyright file="SqlRoleProvider.cs" company="Microsoft">
' Copyright (c) Microsoft Corporation. All rights reserved.
' </copyright>
'------------------------------------------------------------------------------

Imports System
Imports System.Web.Security
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
Imports System.Text
Imports System.Configuration.Provider
Imports System.Configuration
Imports System.Web.Hosting
Imports System.Web.Util
Namespace Meanstream.Portal.Providers.AspNetMembershipProvider
    'using Meanstream.Portal.Core.Entities;
    'using Meanstream.Portal.Core.Data;


    ' Remove CAS from sample: [AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    ' Remove CAS from sample: [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    Public Class SqlRoleProvider
        Inherits RoleProvider
        Private _AppName As String
        Private _SchemaVersionCheck As Integer
        Private _sqlConnectionString As String
        Private _CommandTimeout As Integer

        ' Public properties
        Private ReadOnly Property CommandTimeout() As Integer
            Get
                Return _CommandTimeout
            End Get
        End Property


        Public Overloads Overrides Sub Initialize(ByVal name As String, ByVal config As NameValueCollection)
            ' Remove CAS from sample: HttpRuntime.CheckAspNetHostingPermission (AspNetHostingPermissionLevel.Low, SR.Feature_not_supported_at_this_level);
            If config Is Nothing Then
                Throw New ArgumentNullException("config")
            End If

            If [String].IsNullOrEmpty(name) Then
                name = "SqlRoleProvider"
            End If
            If String.IsNullOrEmpty(config("description")) Then
                config.Remove("description")
                config.Add("description", SR.GetString(SR.RoleSqlProvider_description))
            End If
            MyBase.Initialize(name, config)

            _SchemaVersionCheck = 0

            _CommandTimeout = SecUtility.GetIntValue(config, "commandTimeout", 30, True, 0)

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

            config.Remove("connectionStringName")
            config.Remove("applicationName")
            config.Remove("commandTimeout")
            If config.Count > 0 Then
                Dim attribUnrecognized As String = config.GetKey(0)
                If Not [String].IsNullOrEmpty(attribUnrecognized) Then
                    Throw New ProviderException(SR.GetString(SR.Provider_unrecognized_attribute, attribUnrecognized))
                End If
            End If
        End Sub

        Private Sub CheckSchemaVersion(ByVal connection As SqlConnection)
            Dim features As String() = {"Role Manager"}
            Dim version As String = "1"

            SecUtility.CheckSchemaVersion(Me, connection, features, version, _SchemaVersionCheck)
        End Sub

        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////

        Public Overloads Overrides Function IsUserInRole(ByVal username As String, ByVal roleName As String) As Boolean
            SecUtility.CheckParameter(roleName, True, True, True, 256, "roleName")
            SecUtility.CheckParameter(username, True, False, True, 256, "username")
            If username.Length < 1 Then
                Return False
            End If

            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_UsersInRoles_IsUserInRole", holder.Connection)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = CommandTimeout

                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, username))
                    cmd.Parameters.Add(CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName))
                    cmd.ExecuteNonQuery()
                    Dim iStatus As Integer = GetReturnValue(cmd)

                    Select Case iStatus
                        Case 0
                            Return False
                        Case 1
                            Return True
                        Case 2
                            Return False
                            ' throw new ProviderException(SR.GetString(SR.Provider_user_not_found));
                        Case 3
                            Return False
                            ' throw new ProviderException(SR.GetString(SR.Provider_role_not_found, roleName));
                    End Select
                    Throw New ProviderException(SR.GetString(SR.Provider_unknown_failure))
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

        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////

        Public Overloads Overrides Function GetRolesForUser(ByVal username As String) As String()
            SecUtility.CheckParameter(username, True, False, True, 256, "username")
            If username.Length < 1 Then
                Return New String(-1) {}
            End If
            Try
                Dim holder As SqlConnectionHolder = Nothing

                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_UsersInRoles_GetRolesForUser", holder.Connection)
                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    Dim reader As SqlDataReader = Nothing
                    Dim sc As New StringCollection()

                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = CommandTimeout

                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, username))
                    Try
                        reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
                        While reader.Read()
                            sc.Add(reader.GetString(0))
                        End While
                    Catch
                        Throw
                    Finally
                        If reader IsNot Nothing Then
                            reader.Close()
                        End If
                    End Try
                    If sc.Count > 0 Then
                        Dim strReturn As [String]() = New [String](sc.Count - 1) {}
                        sc.CopyTo(strReturn, 0)
                        Return strReturn
                    End If

                    Select Case GetReturnValue(cmd)
                        Case 0
                            Return New String(-1) {}
                        Case 1
                            Return New String(-1) {}
                        Case Else
                            'throw new ProviderException(SR.GetString(SR.Provider_user_not_found));
                            Throw New ProviderException(SR.GetString(SR.Provider_unknown_failure))
                    End Select
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

        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////

        Public Overloads Overrides Sub CreateRole(ByVal roleName As String)
            SecUtility.CheckParameter(roleName, True, True, True, 256, "roleName")
            Try
                Dim holder As SqlConnectionHolder = Nothing

                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)
                    Dim cmd As New SqlCommand("dbo.aspnet_Roles_CreateRole", holder.Connection)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = CommandTimeout

                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)

                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName))
                    cmd.ExecuteNonQuery()


                    'Add the role to our custom roles table

                    'Meanstream.Portal.Core.Entities.TList<Meanstream.Portal.Core.Entities.AspnetRoles> RoleList = Meanstream.Portal.Core.Data.DataRepository.AspnetRolesProvider.Find("RoleName=" + roleName);
                    'Meanstream.Portal.Core.Entities.Roles RoleInfo = new Meanstream.Portal.Core.Entities.Roles();
                    'RoleInfo.RoleId = RoleList[0].RoleId;
                    'RoleInfo.PortalId = 0;
                    'RoleInfo.AutoAssignment = false;
                    'RoleInfo.Description = "";
                    'RoleInfo.IsPublic = false;
                    'RoleInfo.RoleName = roleName;
                    'Meanstream.Portal.Core.Data.DataRepository.RolesProvider.Insert(RoleInfo);


                    Dim returnValue As Integer = GetReturnValue(cmd)

                    Select Case returnValue
                        Case 0
                            Exit Sub

                        Case 1
                            Throw New ProviderException(SR.GetString(SR.Provider_role_already_exists, roleName))
                        Case Else

                            Throw New ProviderException(SR.GetString(SR.Provider_unknown_failure))
                    End Select
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





        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////

        Public Overloads Overrides Function DeleteRole(ByVal roleName As String, ByVal throwOnPopulatedRole As Boolean) As Boolean
            SecUtility.CheckParameter(roleName, True, True, True, 256, "roleName")
            Try
                Dim holder As SqlConnectionHolder = Nothing

                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Roles_DeleteRole", holder.Connection)

                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = CommandTimeout

                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName))
                    cmd.Parameters.Add(CreateInputParam("@DeleteOnlyIfRoleIsEmpty", SqlDbType.Bit, If(throwOnPopulatedRole, 1, 0)))
                    cmd.ExecuteNonQuery()
                    Dim returnValue As Integer = GetReturnValue(cmd)

                    If returnValue = 2 Then
                        Throw New ProviderException(SR.GetString(SR.Role_is_not_empty))
                    End If

                    Return (returnValue = 0)
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

        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////

        Public Overloads Overrides Function RoleExists(ByVal roleName As String) As Boolean
            SecUtility.CheckParameter(roleName, True, True, True, 256, "roleName")

            Try
                Dim holder As SqlConnectionHolder = Nothing

                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Roles_RoleExists", holder.Connection)

                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = CommandTimeout

                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName))
                    cmd.ExecuteNonQuery()
                    Dim returnValue As Integer = GetReturnValue(cmd)

                    Select Case returnValue
                        Case 0
                            Return False
                        Case 1
                            Return True
                    End Select
                    Throw New ProviderException(SR.GetString(SR.Provider_unknown_failure))
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

        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////

        Public Overloads Overrides Sub AddUsersToRoles(ByVal usernames As String(), ByVal roleNames As String())
            SecUtility.CheckArrayParameter(roleNames, True, True, True, 256, "roleNames")
            SecUtility.CheckArrayParameter(usernames, True, True, True, 256, "usernames")

            Dim beginTranCalled As Boolean = False
            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)
                    Dim numUsersRemaing As Integer = usernames.Length
                    While numUsersRemaing > 0
                        Dim iter As Integer
                        Dim allUsers As String = usernames(usernames.Length - numUsersRemaing)
                        numUsersRemaing -= 1
                        For iter = usernames.Length - numUsersRemaing To usernames.Length - 1
                            If allUsers.Length + usernames(iter).Length + 1 >= 4000 Then
                                Exit For
                            End If
                            allUsers += "," & usernames(iter)
                            numUsersRemaing -= 1
                        Next

                        Dim numRolesRemaining As Integer = roleNames.Length
                        While numRolesRemaining > 0
                            Dim allRoles As String = roleNames(roleNames.Length - numRolesRemaining)
                            numRolesRemaining -= 1
                            For iter = roleNames.Length - numRolesRemaining To roleNames.Length - 1
                                If allRoles.Length + roleNames(iter).Length + 1 >= 4000 Then
                                    Exit For
                                End If
                                allRoles += "," & roleNames(iter)
                                numRolesRemaining -= 1
                            Next
                            '
                            ' Note: ADO.NET 2.0 introduced the TransactionScope class - in your own code you should use TransactionScope
                            ' rather than explicitly managing transactions with the TSQL BEGIN/COMMIT/ROLLBACK statements.
                            '
                            If Not beginTranCalled AndAlso (numUsersRemaing > 0 OrElse numRolesRemaining > 0) Then
                                Dim SqlCommand As SqlCommand = New SqlCommand("BEGIN TRANSACTION", holder.Connection)
                                SqlCommand.ExecuteNonQuery()
                                beginTranCalled = True
                            End If
                            AddUsersToRolesCore(holder.Connection, allUsers, allRoles)
                        End While
                    End While
                    If beginTranCalled Then
                        Dim SqlCommand As SqlCommand = New SqlCommand("COMMIT TRANSACTION", holder.Connection)
                        SqlCommand.ExecuteNonQuery()
                        beginTranCalled = False
                    End If
                Catch
                    If beginTranCalled Then
                        Try
                            Dim SqlCommand As SqlCommand = New SqlCommand("ROLLBACK TRANSACTION", holder.Connection)
                            SqlCommand.ExecuteNonQuery()
                        Catch
                        End Try
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
        End Sub

        Private Sub AddUsersToRolesCore(ByVal conn As SqlConnection, ByVal usernames As String, ByVal roleNames As String)
            Dim cmd As New SqlCommand("dbo.aspnet_UsersInRoles_AddUsersToRoles", conn)
            Dim reader As SqlDataReader = Nothing
            Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
            Dim s1 As String = [String].Empty, s2 As String = [String].Empty

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = CommandTimeout

            p.Direction = ParameterDirection.ReturnValue
            cmd.Parameters.Add(p)
            cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
            cmd.Parameters.Add(CreateInputParam("@RoleNames", SqlDbType.NVarChar, roleNames))
            cmd.Parameters.Add(CreateInputParam("@UserNames", SqlDbType.NVarChar, usernames))
            cmd.Parameters.Add(CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))
            Try
                reader = cmd.ExecuteReader(CommandBehavior.SingleRow)
                If reader.Read() Then
                    If reader.FieldCount > 0 Then
                        s1 = reader.GetString(0)
                    End If
                    If reader.FieldCount > 1 Then
                        s2 = reader.GetString(1)
                    End If
                End If
            Finally
                If reader IsNot Nothing Then
                    reader.Close()
                End If
            End Try
            Select Case GetReturnValue(cmd)
                Case 0
                    Exit Sub
                Case 1
                    Throw New ProviderException(SR.GetString(SR.Provider_this_user_not_found, s1))
                Case 2
                    Throw New ProviderException(SR.GetString(SR.Provider_role_not_found, s1))
                Case 3
                    Throw New ProviderException(SR.GetString(SR.Provider_this_user_already_in_role, s1, s2))
            End Select
            Throw New ProviderException(SR.GetString(SR.Provider_unknown_failure))
        End Sub

        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////

        Public Overloads Overrides Sub RemoveUsersFromRoles(ByVal usernames As String(), ByVal roleNames As String())
            SecUtility.CheckArrayParameter(roleNames, True, True, True, 256, "roleNames")
            SecUtility.CheckArrayParameter(usernames, True, True, True, 256, "usernames")

            Dim beginTranCalled As Boolean = False
            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)
                    Dim numUsersRemaing As Integer = usernames.Length
                    While numUsersRemaing > 0
                        Dim iter As Integer
                        Dim allUsers As String = usernames(usernames.Length - numUsersRemaing)
                        numUsersRemaing -= 1
                        For iter = usernames.Length - numUsersRemaing To usernames.Length - 1
                            If allUsers.Length + usernames(iter).Length + 1 >= 4000 Then
                                Exit For
                            End If
                            allUsers += "," & usernames(iter)
                            numUsersRemaing -= 1
                        Next

                        Dim numRolesRemaining As Integer = roleNames.Length
                        While numRolesRemaining > 0
                            Dim allRoles As String = roleNames(roleNames.Length - numRolesRemaining)
                            numRolesRemaining -= 1
                            For iter = roleNames.Length - numRolesRemaining To roleNames.Length - 1
                                If allRoles.Length + roleNames(iter).Length + 1 >= 4000 Then
                                    Exit For
                                End If
                                allRoles += "," & roleNames(iter)
                                numRolesRemaining -= 1
                            Next
                            '
                            ' Note: ADO.NET 2.0 introduced the TransactionScope class - in your own code you should use TransactionScope
                            ' rather than explicitly managing transactions with the TSQL BEGIN/COMMIT/ROLLBACK statements.
                            '
                            If Not beginTranCalled AndAlso (numUsersRemaing > 0 OrElse numRolesRemaining > 0) Then
                                Dim SqlCommand As SqlCommand = New SqlCommand("BEGIN TRANSACTION", holder.Connection)
                                SqlCommand.ExecuteNonQuery()
                                beginTranCalled = True
                            End If
                            RemoveUsersFromRolesCore(holder.Connection, allUsers, allRoles)
                        End While
                    End While
                    If beginTranCalled Then
                        Dim SQLCommand As SqlCommand = New SqlCommand("COMMIT TRANSACTION", holder.Connection)
                        SQLCommand.ExecuteNonQuery()
                        beginTranCalled = False
                    End If
                Catch
                    If beginTranCalled Then
                        Dim SQLCommand As SqlCommand = New SqlCommand("ROLLBACK TRANSACTION", holder.Connection)
                        SQLCommand.ExecuteNonQuery()
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
        End Sub

        Private Sub RemoveUsersFromRolesCore(ByVal conn As SqlConnection, ByVal usernames As String, ByVal roleNames As String)
            Dim cmd As New SqlCommand("dbo.aspnet_UsersInRoles_RemoveUsersFromRoles", conn)
            Dim reader As SqlDataReader = Nothing
            Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
            Dim s1 As String = [String].Empty, s2 As String = [String].Empty

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = CommandTimeout

            p.Direction = ParameterDirection.ReturnValue
            cmd.Parameters.Add(p)
            cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
            cmd.Parameters.Add(CreateInputParam("@UserNames", SqlDbType.NVarChar, usernames))
            cmd.Parameters.Add(CreateInputParam("@RoleNames", SqlDbType.NVarChar, roleNames))
            Try
                reader = cmd.ExecuteReader(CommandBehavior.SingleRow)
                If reader.Read() Then
                    If reader.FieldCount > 0 Then
                        s1 = reader.GetString(0)
                    End If
                    If reader.FieldCount > 1 Then
                        s2 = reader.GetString(1)
                    End If
                End If
            Finally
                If reader IsNot Nothing Then
                    reader.Close()
                End If
            End Try
            Select Case GetReturnValue(cmd)
                Case 0
                    Exit Sub
                Case 1
                    Throw New ProviderException(SR.GetString(SR.Provider_this_user_not_found, s1))
                Case 2
                    Throw New ProviderException(SR.GetString(SR.Provider_role_not_found, s2))
                Case 3
                    Throw New ProviderException(SR.GetString(SR.Provider_this_user_already_not_in_role, s1, s2))
            End Select
            Throw New ProviderException(SR.GetString(SR.Provider_unknown_failure))
        End Sub

        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////

        Public Overloads Overrides Function GetUsersInRole(ByVal roleName As String) As String()
            SecUtility.CheckParameter(roleName, True, True, True, 256, "roleName")

            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_UsersInRoles_GetUsersInRoles", holder.Connection)
                    Dim reader As SqlDataReader = Nothing
                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    Dim sc As New StringCollection()

                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = CommandTimeout

                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName))
                    Try
                        reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
                        While reader.Read()
                            sc.Add(reader.GetString(0))
                        End While
                    Catch
                        Throw
                    Finally
                        If reader IsNot Nothing Then
                            reader.Close()
                        End If
                    End Try
                    If sc.Count < 1 Then
                        Select Case GetReturnValue(cmd)
                            Case 0
                                Return New String(-1) {}
                            Case 1
                                Throw New ProviderException(SR.GetString(SR.Provider_role_not_found, roleName))
                        End Select
                        Throw New ProviderException(SR.GetString(SR.Provider_unknown_failure))
                    End If

                    Dim strReturn As [String]() = New [String](sc.Count - 1) {}
                    sc.CopyTo(strReturn, 0)
                    Return strReturn
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

        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////

        Public Overloads Overrides Function GetAllRoles() As String()
            Try
                Dim holder As SqlConnectionHolder = Nothing

                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Roles_GetAllRoles", holder.Connection)
                    Dim sc As New StringCollection()
                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    Dim reader As SqlDataReader = Nothing

                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = CommandTimeout

                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    Try
                        reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
                        While reader.Read()
                            sc.Add(reader.GetString(0))
                        End While
                    Catch
                        Throw
                    Finally
                        If reader IsNot Nothing Then
                            reader.Close()
                        End If
                    End Try

                    Dim strReturn As [String]() = New [String](sc.Count - 1) {}
                    sc.CopyTo(strReturn, 0)
                    Return strReturn
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

        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        Public Overloads Overrides Function FindUsersInRole(ByVal roleName As String, ByVal usernameToMatch As String) As String()
            SecUtility.CheckParameter(roleName, True, True, True, 256, "roleName")
            SecUtility.CheckParameter(usernameToMatch, True, True, False, 256, "usernameToMatch")

            Try
                Dim holder As SqlConnectionHolder = Nothing

                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_UsersInRoles_FindUsersInRole", holder.Connection)
                    Dim reader As SqlDataReader = Nothing
                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    Dim sc As New StringCollection()

                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = CommandTimeout

                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName))
                    cmd.Parameters.Add(CreateInputParam("@UserNameToMatch", SqlDbType.NVarChar, usernameToMatch))
                    Try
                        reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
                        While reader.Read()
                            sc.Add(reader.GetString(0))
                        End While
                    Catch
                        Throw
                    Finally
                        If reader IsNot Nothing Then
                            reader.Close()
                        End If
                    End Try
                    If sc.Count < 1 Then
                        Select Case GetReturnValue(cmd)
                            Case 0
                                Return New String(-1) {}

                            Case 1
                                Throw New ProviderException(SR.GetString(SR.Provider_role_not_found, roleName))
                            Case Else

                                Throw New ProviderException(SR.GetString(SR.Provider_unknown_failure))
                        End Select
                    End If
                    Dim strReturn As [String]() = New [String](sc.Count - 1) {}
                    sc.CopyTo(strReturn, 0)
                    Return strReturn
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
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////

        Public Overloads Overrides Property ApplicationName() As String
            Get
                Return _AppName
            End Get
            Set(ByVal value As String)
                _AppName = value

                If _AppName.Length > 256 Then
                    Throw New ProviderException(SR.GetString(SR.Provider_application_name_too_long))
                End If
            End Set
        End Property

        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        Private Function CreateInputParam(ByVal paramName As String, ByVal dbType As SqlDbType, ByVal objValue As Object) As SqlParameter
            Dim param As New SqlParameter(paramName, dbType)
            If objValue Is Nothing Then
                objValue = [String].Empty
            End If
            param.Value = objValue
            Return param
        End Function

        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        Private Function GetReturnValue(ByVal cmd As SqlCommand) As Integer
            For Each param As SqlParameter In cmd.Parameters
                If param.Direction = ParameterDirection.ReturnValue AndAlso param.Value IsNot Nothing AndAlso TypeOf param.Value Is Integer Then
                    Return CInt(param.Value)
                End If
            Next
            Return -1
        End Function
    End Class
End Namespace