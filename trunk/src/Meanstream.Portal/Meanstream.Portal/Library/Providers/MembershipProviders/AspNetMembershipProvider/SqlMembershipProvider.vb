'------------------------------------------------------------------------------
' <copyright file="SqlMembershipProvider.cs" company="Microsoft">
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
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Configuration.Provider
Imports System.Configuration
Imports System.Web.Management
Imports System.Web.Util
Namespace Meanstream.Portal.Providers.AspNetMembershipProvider

    ' Remove CAS from sample: [AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    ' Remove CAS from sample: [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    Public Class SqlMembershipProvider
        Inherits MembershipProvider

        ' Public properties

        Public Overloads Overrides ReadOnly Property EnablePasswordRetrieval() As Boolean
            Get
                Return _EnablePasswordRetrieval
            End Get
        End Property

        Public Overloads Overrides ReadOnly Property EnablePasswordReset() As Boolean
            Get
                Return _EnablePasswordReset
            End Get
        End Property

        Public Overloads Overrides ReadOnly Property RequiresQuestionAndAnswer() As Boolean
            Get
                Return _RequiresQuestionAndAnswer
            End Get
        End Property

        Public Overloads Overrides ReadOnly Property RequiresUniqueEmail() As Boolean
            Get
                Return _RequiresUniqueEmail
            End Get
        End Property

        Public Overloads Overrides ReadOnly Property PasswordFormat() As MembershipPasswordFormat
            Get
                Return _PasswordFormat
            End Get
        End Property
        Public Overloads Overrides ReadOnly Property MaxInvalidPasswordAttempts() As Integer
            Get
                Return _MaxInvalidPasswordAttempts
            End Get
        End Property

        Public Overloads Overrides ReadOnly Property PasswordAttemptWindow() As Integer
            Get
                Return _PasswordAttemptWindow
            End Get
        End Property

        Public Overloads Overrides ReadOnly Property MinRequiredPasswordLength() As Integer
            Get
                Return _MinRequiredPasswordLength
            End Get
        End Property

        Public Overloads Overrides ReadOnly Property MinRequiredNonAlphanumericCharacters() As Integer
            Get
                Return _MinRequiredNonalphanumericCharacters
            End Get
        End Property

        Public Overloads Overrides ReadOnly Property PasswordStrengthRegularExpression() As String
            Get
                Return _PasswordStrengthRegularExpression
            End Get
        End Property

        Public Overloads Overrides Property ApplicationName() As String
            Get
                Return _AppName
            End Get
            Set(ByVal value As String)
                If [String].IsNullOrEmpty(value) Then
                    Throw New ArgumentNullException("value")
                End If

                If value.Length > 256 Then
                    Throw New ProviderException(SR.GetString(SR.Provider_application_name_too_long))
                End If
                _AppName = value
            End Set
        End Property

        Private _sqlConnectionString As String
        Private _EnablePasswordRetrieval As Boolean
        Private _EnablePasswordReset As Boolean
        Private _RequiresQuestionAndAnswer As Boolean
        Private _AppName As String
        Private _RequiresUniqueEmail As Boolean
        Private _MaxInvalidPasswordAttempts As Integer
        Private _CommandTimeout As Integer
        Private _PasswordAttemptWindow As Integer
        Private _MinRequiredPasswordLength As Integer
        Private _MinRequiredNonalphanumericCharacters As Integer
        Private _PasswordStrengthRegularExpression As String
        Private _SchemaVersionCheck As Integer
        Private _PasswordFormat As MembershipPasswordFormat

        Private Const PASSWORD_SIZE As Integer = 14

        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////

        Public Overloads Overrides Sub Initialize(ByVal name As String, ByVal config As NameValueCollection)
            ' Remove CAS from sample: HttpRuntime.CheckAspNetHostingPermission (AspNetHostingPermissionLevel.Low, SR.Feature_not_supported_at_this_level);
            If config Is Nothing Then
                Throw New ArgumentNullException("config")
            End If
            If [String].IsNullOrEmpty(name) Then
                name = "SqlMembershipProvider"
            End If
            If String.IsNullOrEmpty(config("description")) Then
                config.Remove("description")
                config.Add("description", SR.GetString(SR.MembershipSqlProvider_description))
            End If
            MyBase.Initialize(name, config)

            _SchemaVersionCheck = 0

            _EnablePasswordRetrieval = SecUtility.GetBooleanValue(config, "enablePasswordRetrieval", False)
            _EnablePasswordReset = SecUtility.GetBooleanValue(config, "enablePasswordReset", True)
            _RequiresQuestionAndAnswer = SecUtility.GetBooleanValue(config, "requiresQuestionAndAnswer", True)
            _RequiresUniqueEmail = SecUtility.GetBooleanValue(config, "requiresUniqueEmail", True)
            _MaxInvalidPasswordAttempts = SecUtility.GetIntValue(config, "maxInvalidPasswordAttempts", 5, False, 0)
            _PasswordAttemptWindow = SecUtility.GetIntValue(config, "passwordAttemptWindow", 10, False, 0)
            _MinRequiredPasswordLength = SecUtility.GetIntValue(config, "minRequiredPasswordLength", 7, False, 128)
            _MinRequiredNonalphanumericCharacters = SecUtility.GetIntValue(config, "minRequiredNonalphanumericCharacters", 1, True, 128)

            _PasswordStrengthRegularExpression = config("passwordStrengthRegularExpression")
            If _PasswordStrengthRegularExpression IsNot Nothing Then
                _PasswordStrengthRegularExpression = _PasswordStrengthRegularExpression.Trim()
                If _PasswordStrengthRegularExpression.Length <> 0 Then
                    Try
                        Dim regex As New Regex(_PasswordStrengthRegularExpression)
                    Catch e As ArgumentException
                        Throw New ProviderException(e.Message, e)
                    End Try
                End If
            Else
                _PasswordStrengthRegularExpression = String.Empty
            End If
            If _MinRequiredNonalphanumericCharacters > _MinRequiredPasswordLength Then
                Throw New HttpException(SR.GetString(SR.MinRequiredNonalphanumericCharacters_can_not_be_more_than_MinRequiredPasswordLength))
            End If

            _CommandTimeout = SecUtility.GetIntValue(config, "commandTimeout", 30, True, 0)
            _AppName = config("applicationName")
            If String.IsNullOrEmpty(_AppName) Then
                _AppName = SecUtility.GetDefaultAppName()
            End If

            If _AppName.Length > 256 Then
                Throw New ProviderException(SR.GetString(SR.Provider_application_name_too_long))
            End If

            Dim strTemp As String = config("passwordFormat")
            If strTemp Is Nothing Then
                strTemp = "Hashed"
            End If

            Select Case strTemp
                Case "Clear"
                    _PasswordFormat = MembershipPasswordFormat.Clear
                    Exit Select
                Case "Encrypted"
                    _PasswordFormat = MembershipPasswordFormat.Encrypted
                    Exit Select
                Case "Hashed"
                    _PasswordFormat = MembershipPasswordFormat.Hashed
                    Exit Select
                Case Else
                    Throw New ProviderException(SR.GetString(SR.Provider_bad_password_format))
            End Select

            If PasswordFormat = MembershipPasswordFormat.Hashed AndAlso EnablePasswordRetrieval Then
                Throw New ProviderException(SR.GetString(SR.Provider_can_not_retrieve_hashed_password))
            End If
            'if (_PasswordFormat == MembershipPasswordFormat.Encrypted && MachineKeySection.IsDecryptionKeyAutogenerated)
            ' throw new ProviderException(SR.GetString(SR.Can_not_use_encrypted_passwords_with_autogen_keys));

            Dim temp As String = config("connectionStringName")
            If temp Is Nothing OrElse temp.Length < 1 Then
                Throw New ProviderException(SR.GetString(SR.Connection_name_not_specified))
            End If
            _sqlConnectionString = SqlConnectionHelper.GetConnectionString(temp, True, True)
            If _sqlConnectionString Is Nothing OrElse _sqlConnectionString.Length < 1 Then
                Throw New ProviderException(SR.GetString(SR.Connection_string_not_found, temp))
            End If

            config.Remove("connectionStringName")
            config.Remove("enablePasswordRetrieval")
            config.Remove("enablePasswordReset")
            config.Remove("requiresQuestionAndAnswer")
            config.Remove("applicationName")
            config.Remove("requiresUniqueEmail")
            config.Remove("maxInvalidPasswordAttempts")
            config.Remove("passwordAttemptWindow")
            config.Remove("commandTimeout")
            config.Remove("passwordFormat")
            config.Remove("name")
            config.Remove("minRequiredPasswordLength")
            config.Remove("minRequiredNonalphanumericCharacters")
            config.Remove("passwordStrengthRegularExpression")
            If config.Count > 0 Then
                Dim attribUnrecognized As String = config.GetKey(0)
                If Not [String].IsNullOrEmpty(attribUnrecognized) Then
                    Throw New ProviderException(SR.GetString(SR.Provider_unrecognized_attribute, attribUnrecognized))
                End If
            End If
        End Sub

        Private Sub CheckSchemaVersion(ByVal connection As SqlConnection)
            Dim features As String() = {"Common", "Membership"}
            Dim version As String = "1"

            SecUtility.CheckSchemaVersion(Me, connection, features, version, _SchemaVersionCheck)
        End Sub

        Private ReadOnly Property CommandTimeout() As Integer
            Get
                Return _CommandTimeout
            End Get
        End Property

        '''/////////////////////////////////////////////////////////
        '''/////////////////////////////////////////////////////////
        '''/////////////////////////////////////////////////////////

        Public Overloads Overrides Function CreateUser(ByVal username As String, ByVal password As String, ByVal email As String, ByVal passwordQuestion As String, ByVal passwordAnswer As String, ByVal isApproved As Boolean, _
        ByVal providerUserKey As Object, ByRef status As MembershipCreateStatus) As MembershipUser
            If Not SecUtility.ValidateParameter(password, True, True, False, 128) Then
                status = MembershipCreateStatus.InvalidPassword
                Return Nothing
            End If

            Dim salt As String = GenerateSalt()
            Dim pass As String = EncodePassword(password, CInt(_PasswordFormat), salt)
            If pass.Length > 128 Then
                status = MembershipCreateStatus.InvalidPassword
                Return Nothing
            End If

            Dim encodedPasswordAnswer As String
            If passwordAnswer IsNot Nothing Then
                passwordAnswer = passwordAnswer.Trim()
            End If

            If Not String.IsNullOrEmpty(passwordAnswer) Then
                If passwordAnswer.Length > 128 Then
                    status = MembershipCreateStatus.InvalidAnswer
                    Return Nothing
                End If
                encodedPasswordAnswer = EncodePassword(passwordAnswer.ToLower(CultureInfo.InvariantCulture), CInt(_PasswordFormat), salt)
            Else
                encodedPasswordAnswer = passwordAnswer
            End If
            If Not SecUtility.ValidateParameter(encodedPasswordAnswer, RequiresQuestionAndAnswer, True, False, 128) Then
                status = MembershipCreateStatus.InvalidAnswer
                Return Nothing
            End If

            If Not SecUtility.ValidateParameter(username, True, True, True, 256) Then
                status = MembershipCreateStatus.InvalidUserName
                Return Nothing
            End If

            If Not SecUtility.ValidateParameter(email, RequiresUniqueEmail, RequiresUniqueEmail, False, 256) Then
                status = MembershipCreateStatus.InvalidEmail
                Return Nothing
            End If

            If Not SecUtility.ValidateParameter(passwordQuestion, RequiresQuestionAndAnswer, True, False, 256) Then
                status = MembershipCreateStatus.InvalidQuestion
                Return Nothing
            End If

            If providerUserKey IsNot Nothing Then
                If Not (TypeOf providerUserKey Is Guid) Then
                    status = MembershipCreateStatus.InvalidProviderUserKey
                    Return Nothing
                End If
            End If

            If password.Length < MinRequiredPasswordLength Then
                status = MembershipCreateStatus.InvalidPassword
                Return Nothing
            End If

            Dim count As Integer = 0

            For i As Integer = 0 To password.Length - 1
                If Not Char.IsLetterOrDigit(password, i) Then
                    count += 1
                End If
            Next

            If count < MinRequiredNonAlphanumericCharacters Then
                status = MembershipCreateStatus.InvalidPassword
                Return Nothing
            End If

            If PasswordStrengthRegularExpression.Length > 0 Then
                If Not Regex.IsMatch(password, PasswordStrengthRegularExpression) Then
                    status = MembershipCreateStatus.InvalidPassword
                    Return Nothing
                End If
            End If

            Dim e As New ValidatePasswordEventArgs(username, password, True)
            OnValidatingPassword(e)

            If e.Cancel Then
                status = MembershipCreateStatus.InvalidPassword
                Return Nothing
            End If

            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim dt As DateTime = RoundToSeconds(DateTime.UtcNow)
                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_CreateUser", holder.Connection)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, username))
                    cmd.Parameters.Add(CreateInputParam("@Password", SqlDbType.NVarChar, pass))
                    cmd.Parameters.Add(CreateInputParam("@PasswordSalt", SqlDbType.NVarChar, salt))
                    cmd.Parameters.Add(CreateInputParam("@Email", SqlDbType.NVarChar, email))
                    cmd.Parameters.Add(CreateInputParam("@PasswordQuestion", SqlDbType.NVarChar, passwordQuestion))
                    cmd.Parameters.Add(CreateInputParam("@PasswordAnswer", SqlDbType.NVarChar, encodedPasswordAnswer))
                    cmd.Parameters.Add(CreateInputParam("@IsApproved", SqlDbType.Bit, isApproved))
                    cmd.Parameters.Add(CreateInputParam("@UniqueEmail", SqlDbType.Int, If(RequiresUniqueEmail, 1, 0)))
                    cmd.Parameters.Add(CreateInputParam("@PasswordFormat", SqlDbType.Int, CInt(PasswordFormat)))
                    cmd.Parameters.Add(CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, dt))
                    Dim p As SqlParameter = CreateInputParam("@UserId", SqlDbType.UniqueIdentifier, providerUserKey)
                    p.Direction = ParameterDirection.InputOutput
                    cmd.Parameters.Add(p)

                    p = New SqlParameter("@ReturnValue", SqlDbType.Int)
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)

                    cmd.ExecuteNonQuery()
                    Dim iStatus As Integer = (If((p.Value IsNot Nothing), CInt(p.Value), -1))
                    If iStatus < 0 OrElse iStatus > CInt(MembershipCreateStatus.ProviderError) Then
                        iStatus = CInt(MembershipCreateStatus.ProviderError)
                    End If
                    status = DirectCast(iStatus, MembershipCreateStatus)
                    If iStatus <> 0 Then
                        ' !success
                        Return Nothing
                    End If

                    providerUserKey = New Guid(cmd.Parameters("@UserId").Value.ToString())
                    dt = dt.ToLocalTime()
                    Return New MembershipUser(Me.Name, username, providerUserKey, email, passwordQuestion, Nothing, _
                    isApproved, False, dt, dt, dt, dt, _
                    New DateTime(1754, 1, 1))
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

        Public Overloads Overrides Function ChangePasswordQuestionAndAnswer(ByVal username As String, ByVal password As String, ByVal newPasswordQuestion As String, ByVal newPasswordAnswer As String) As Boolean
            SecUtility.CheckParameter(username, True, True, True, 256, "username")
            SecUtility.CheckParameter(password, True, True, False, 128, "password")

            Dim salt As String = ""
            Dim passwordFormat As Integer = 0
            If Not CheckPassword(username, password, False, False, salt, passwordFormat) Then
                Return False
            End If
            SecUtility.CheckParameter(newPasswordQuestion, RequiresQuestionAndAnswer, RequiresQuestionAndAnswer, False, 256, "newPasswordQuestion")
            Dim encodedPasswordAnswer As String
            If newPasswordAnswer IsNot Nothing Then
                newPasswordAnswer = newPasswordAnswer.Trim()
            End If

            SecUtility.CheckParameter(newPasswordAnswer, RequiresQuestionAndAnswer, RequiresQuestionAndAnswer, False, 128, "newPasswordAnswer")
            If Not String.IsNullOrEmpty(newPasswordAnswer) Then
                encodedPasswordAnswer = EncodePassword(newPasswordAnswer.ToLower(CultureInfo.InvariantCulture), CInt(passwordFormat), salt)
            Else
                encodedPasswordAnswer = newPasswordAnswer
            End If
            SecUtility.CheckParameter(encodedPasswordAnswer, RequiresQuestionAndAnswer, RequiresQuestionAndAnswer, False, 128, "newPasswordAnswer")

            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_ChangePasswordQuestionAndAnswer", holder.Connection)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, username))
                    cmd.Parameters.Add(CreateInputParam("@NewPasswordQuestion", SqlDbType.NVarChar, newPasswordQuestion))
                    cmd.Parameters.Add(CreateInputParam("@NewPasswordAnswer", SqlDbType.NVarChar, encodedPasswordAnswer))

                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)

                    cmd.ExecuteNonQuery()
                    Dim status As Integer = (If((p.Value IsNot Nothing), CInt(p.Value), -1))
                    If status <> 0 Then
                        Throw New ProviderException(GetExceptionText(status))
                    End If

                    Return (status = 0)
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

        Public Overloads Overrides Function GetPassword(ByVal username As String, ByVal passwordAnswer As String) As String
            If Not EnablePasswordRetrieval Then
                Throw New NotSupportedException(SR.GetString(SR.Membership_PasswordRetrieval_not_supported))
            End If

            SecUtility.CheckParameter(username, True, True, True, 256, "username")

            Dim encodedPasswordAnswer As String = GetEncodedPasswordAnswer(username, passwordAnswer)
            SecUtility.CheckParameter(encodedPasswordAnswer, RequiresQuestionAndAnswer, RequiresQuestionAndAnswer, False, 128, "passwordAnswer")

            Dim errText As String
            Dim passwordFormat As Integer = 0
            Dim status As Integer = 0

            Dim pass As String = GetPasswordFromDB(username, encodedPasswordAnswer, RequiresQuestionAndAnswer, passwordFormat, status)

            If pass Is Nothing Then
                errText = GetExceptionText(status)
                If IsStatusDueToBadPassword(status) Then
                    Throw New MembershipPasswordException(errText)
                Else
                    Throw New ProviderException(errText)
                End If
            End If

            Return UnEncodePassword(pass, passwordFormat)
        End Function

        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////

        Public Overloads Overrides Function ChangePassword(ByVal username As String, ByVal oldPassword As String, ByVal newPassword As String) As Boolean
            SecUtility.CheckParameter(username, True, True, True, 256, "username")
            SecUtility.CheckParameter(oldPassword, True, True, False, 128, "oldPassword")
            SecUtility.CheckParameter(newPassword, True, True, False, 128, "newPassword")

            Dim salt As String = Nothing
            Dim passwordFormat As Integer
            Dim status As Integer

            If Not CheckPassword(username, oldPassword, False, False, salt, passwordFormat) Then
                Return False
            End If

            If newPassword.Length < MinRequiredPasswordLength Then
                Throw New ArgumentException(SR.GetString(SR.Password_too_short, "newPassword", MinRequiredPasswordLength.ToString(CultureInfo.InvariantCulture)))
            End If

            Dim count As Integer = 0

            For i As Integer = 0 To newPassword.Length - 1
                If Not Char.IsLetterOrDigit(newPassword, i) Then
                    count += 1
                End If
            Next

            If count < MinRequiredNonAlphanumericCharacters Then
                Throw New ArgumentException(SR.GetString(SR.Password_need_more_non_alpha_numeric_chars, "newPassword", MinRequiredNonAlphanumericCharacters.ToString(CultureInfo.InvariantCulture)))
            End If

            If PasswordStrengthRegularExpression.Length > 0 Then
                If Not Regex.IsMatch(newPassword, PasswordStrengthRegularExpression) Then
                    Throw New ArgumentException(SR.GetString(SR.Password_does_not_match_regular_expression, "newPassword"))
                End If
            End If

            Dim pass As String = EncodePassword(newPassword, CInt(passwordFormat), salt)
            If pass.Length > 128 Then
                Throw New ArgumentException(SR.GetString(SR.Membership_password_too_long), "newPassword")
            End If

            Dim e As New ValidatePasswordEventArgs(username, newPassword, False)
            OnValidatingPassword(e)

            If e.Cancel Then
                If e.FailureInformation IsNot Nothing Then
                    Throw e.FailureInformation
                Else
                    Throw New ArgumentException(SR.GetString(SR.Membership_Custom_Password_Validation_Failure), "newPassword")
                End If
            End If


            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_SetPassword", holder.Connection)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, username))
                    cmd.Parameters.Add(CreateInputParam("@NewPassword", SqlDbType.NVarChar, pass))
                    cmd.Parameters.Add(CreateInputParam("@PasswordSalt", SqlDbType.NVarChar, salt))
                    cmd.Parameters.Add(CreateInputParam("@PasswordFormat", SqlDbType.Int, passwordFormat))
                    cmd.Parameters.Add(CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))

                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)

                    cmd.ExecuteNonQuery()

                    status = (If((p.Value IsNot Nothing), CInt(p.Value), -1))

                    If status <> 0 Then
                        Dim errText As String = GetExceptionText(status)

                        If IsStatusDueToBadPassword(status) Then
                            Throw New MembershipPasswordException(errText)
                        Else
                            Throw New ProviderException(errText)
                        End If
                    End If

                    Return True
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

        Public Overloads Overrides Function ResetPassword(ByVal username As String, ByVal passwordAnswer As String) As String
            If Not EnablePasswordReset Then
                Throw New NotSupportedException(SR.GetString(SR.Not_configured_to_support_password_resets))
            End If

            SecUtility.CheckParameter(username, True, True, True, 256, "username")

            Dim salt As String = ""
            Dim passwordFormat As Integer = 0
            Dim passwdFromDB As String = ""
            Dim status As Integer
            Dim failedPasswordAttemptCount As Integer
            Dim failedPasswordAnswerAttemptCount As Integer
            Dim isApproved As Boolean
            Dim lastLoginDate As DateTime, lastActivityDate As DateTime

            GetPasswordWithFormat(username, False, status, passwdFromDB, passwordFormat, salt, _
            failedPasswordAttemptCount, failedPasswordAnswerAttemptCount, isApproved, lastLoginDate, lastActivityDate)
            If status <> 0 Then
                If IsStatusDueToBadPassword(status) Then
                    Throw New MembershipPasswordException(GetExceptionText(status))
                Else
                    Throw New ProviderException(GetExceptionText(status))
                End If
            End If

            Dim encodedPasswordAnswer As String
            If passwordAnswer IsNot Nothing Then
                passwordAnswer = passwordAnswer.Trim()
            End If
            If Not String.IsNullOrEmpty(passwordAnswer) Then
                encodedPasswordAnswer = EncodePassword(passwordAnswer.ToLower(CultureInfo.InvariantCulture), passwordFormat, salt)
            Else
                encodedPasswordAnswer = passwordAnswer
            End If
            SecUtility.CheckParameter(encodedPasswordAnswer, RequiresQuestionAndAnswer, RequiresQuestionAndAnswer, False, 128, "passwordAnswer")
            Dim newPassword As String = GeneratePassword()

            Dim e As New ValidatePasswordEventArgs(username, newPassword, False)
            OnValidatingPassword(e)

            If e.Cancel Then
                If e.FailureInformation IsNot Nothing Then
                    Throw e.FailureInformation
                Else
                    Throw New ProviderException(SR.GetString(SR.Membership_Custom_Password_Validation_Failure))
                End If
            End If


            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_ResetPassword", holder.Connection)
                    Dim errText As String

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, username))
                    cmd.Parameters.Add(CreateInputParam("@NewPassword", SqlDbType.NVarChar, EncodePassword(newPassword, CInt(passwordFormat), salt)))
                    cmd.Parameters.Add(CreateInputParam("@MaxInvalidPasswordAttempts", SqlDbType.Int, MaxInvalidPasswordAttempts))
                    cmd.Parameters.Add(CreateInputParam("@PasswordAttemptWindow", SqlDbType.Int, PasswordAttemptWindow))
                    cmd.Parameters.Add(CreateInputParam("@PasswordSalt", SqlDbType.NVarChar, salt))
                    cmd.Parameters.Add(CreateInputParam("@PasswordFormat", SqlDbType.Int, CInt(passwordFormat)))
                    cmd.Parameters.Add(CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))
                    If RequiresQuestionAndAnswer Then
                        cmd.Parameters.Add(CreateInputParam("@PasswordAnswer", SqlDbType.NVarChar, encodedPasswordAnswer))
                    End If

                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)

                    cmd.ExecuteNonQuery()

                    status = (If((p.Value IsNot Nothing), CInt(p.Value), -1))

                    If status <> 0 Then
                        errText = GetExceptionText(status)

                        If IsStatusDueToBadPassword(status) Then
                            Throw New MembershipPasswordException(errText)
                        Else
                            Throw New ProviderException(errText)
                        End If
                    End If

                    Return newPassword
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

        Public Overloads Overrides Sub UpdateUser(ByVal user As MembershipUser)
            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            Dim temp As String = user.UserName
            SecUtility.CheckParameter(temp, True, True, True, 256, "UserName")
            temp = user.Email
            SecUtility.CheckParameter(temp, RequiresUniqueEmail, RequiresUniqueEmail, False, 256, "Email")
            user.Email = temp
            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_UpdateUser", holder.Connection)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, user.UserName))
                    cmd.Parameters.Add(CreateInputParam("@Email", SqlDbType.NVarChar, user.Email))
                    cmd.Parameters.Add(CreateInputParam("@Comment", SqlDbType.NText, user.Comment))
                    cmd.Parameters.Add(CreateInputParam("@IsApproved", SqlDbType.Bit, If(user.IsApproved, 1, 0)))
                    cmd.Parameters.Add(CreateInputParam("@LastLoginDate", SqlDbType.DateTime, user.LastLoginDate.ToUniversalTime()))
                    cmd.Parameters.Add(CreateInputParam("@LastActivityDate", SqlDbType.DateTime, user.LastActivityDate.ToUniversalTime()))
                    cmd.Parameters.Add(CreateInputParam("@UniqueEmail", SqlDbType.Int, If(RequiresUniqueEmail, 1, 0)))
                    cmd.Parameters.Add(CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))

                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)
                    cmd.ExecuteNonQuery()
                    Dim status As Integer = (If((p.Value IsNot Nothing), CInt(p.Value), -1))
                    If status <> 0 Then
                        Throw New ProviderException(GetExceptionText(status))
                    End If
                    Exit Sub
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

        Public Overloads Overrides Function ValidateUser(ByVal username As String, ByVal password As String) As Boolean
            If SecUtility.ValidateParameter(username, True, True, True, 256) AndAlso SecUtility.ValidateParameter(password, True, True, False, 128) AndAlso CheckPassword(username, password, True, True) Then
                ' Comment out perf counters in sample: PerfCounters.IncrementCounter(AppPerfCounter.MEMBER_SUCCESS);
                ' Comment out events in sample: WebBaseEvent.RaiseSystemEvent(null, WebEventCodes.AuditMembershipAuthenticationSuccess, username);
                Return True
            Else
                ' Comment out perf counters in sample: PerfCounters.IncrementCounter(AppPerfCounter.MEMBER_FAIL);
                ' Comment out events in sample: WebBaseEvent.RaiseSystemEvent(null, WebEventCodes.AuditMembershipAuthenticationFailure, username);
                Return False
            End If
        End Function

        Public Overloads Overrides Function UnlockUser(ByVal username As String) As Boolean
            SecUtility.CheckParameter(username, True, True, True, 256, "username")
            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_UnlockUser", holder.Connection)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, username))

                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)

                    cmd.ExecuteNonQuery()

                    Dim status As Integer = (If((p.Value IsNot Nothing), CInt(p.Value), -1))
                    If status = 0 Then
                        Return True
                    End If

                    Return False
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

        Public Overloads Overrides Function GetUser(ByVal providerUserKey As Object, ByVal userIsOnline As Boolean) As MembershipUser
            If providerUserKey Is Nothing Then
                Throw New ArgumentNullException("providerUserKey")
            End If

            If Not (TypeOf providerUserKey Is Guid) Then
                Throw New ArgumentException(SR.GetString(SR.Membership_InvalidProviderUserKey), "providerUserKey")
            End If

            Dim reader As SqlDataReader = Nothing

            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_GetUserByUserId", holder.Connection)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@UserId", SqlDbType.UniqueIdentifier, providerUserKey))
                    cmd.Parameters.Add(CreateInputParam("@UpdateLastActivity", SqlDbType.Bit, userIsOnline))
                    cmd.Parameters.Add(CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))
                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)

                    reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim email As String = GetNullableString(reader, 0)
                        Dim passwordQuestion As String = GetNullableString(reader, 1)
                        Dim comment As String = GetNullableString(reader, 2)
                        Dim isApproved As Boolean = reader.GetBoolean(3)
                        Dim dtCreate As DateTime = reader.GetDateTime(4).ToLocalTime()
                        Dim dtLastLogin As DateTime = reader.GetDateTime(5).ToLocalTime()
                        Dim dtLastActivity As DateTime = reader.GetDateTime(6).ToLocalTime()
                        Dim dtLastPassChange As DateTime = reader.GetDateTime(7).ToLocalTime()
                        Dim userName As String = GetNullableString(reader, 8)
                        Dim isLockedOut As Boolean = reader.GetBoolean(9)
                        Dim dtLastLockoutDate As DateTime = reader.GetDateTime(10).ToLocalTime()


                        ' Step 4 : Return the result
                        Return New MembershipUser(Me.Name, userName, providerUserKey, email, passwordQuestion, comment, _
                        isApproved, isLockedOut, dtCreate, dtLastLogin, dtLastActivity, dtLastPassChange, _
                        dtLastLockoutDate)
                    End If

                    Return Nothing
                Finally
                    If reader IsNot Nothing Then
                        reader.Close()
                        reader = Nothing
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

        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////

        Public Overloads Overrides Function GetUser(ByVal username As String, ByVal userIsOnline As Boolean) As MembershipUser
            SecUtility.CheckParameter(username, True, False, True, 256, "username")

            Dim reader As SqlDataReader = Nothing

            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_GetUserByName", holder.Connection)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, username))
                    cmd.Parameters.Add(CreateInputParam("@UpdateLastActivity", SqlDbType.Bit, userIsOnline))
                    cmd.Parameters.Add(CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))
                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)

                    reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim email As String = GetNullableString(reader, 0)
                        Dim passwordQuestion As String = GetNullableString(reader, 1)
                        Dim comment As String = GetNullableString(reader, 2)
                        Dim isApproved As Boolean = reader.GetBoolean(3)
                        Dim dtCreate As DateTime = reader.GetDateTime(4).ToLocalTime()
                        Dim dtLastLogin As DateTime = reader.GetDateTime(5).ToLocalTime()
                        Dim dtLastActivity As DateTime = reader.GetDateTime(6).ToLocalTime()
                        Dim dtLastPassChange As DateTime = reader.GetDateTime(7).ToLocalTime()
                        Dim userId As Guid = reader.GetGuid(8)
                        Dim isLockedOut As Boolean = reader.GetBoolean(9)
                        Dim dtLastLockoutDate As DateTime = reader.GetDateTime(10).ToLocalTime()


                        ' Step 4 : Return the result
                        Return New MembershipUser(Me.Name, username, userId, email, passwordQuestion, comment, _
                        isApproved, isLockedOut, dtCreate, dtLastLogin, dtLastActivity, dtLastPassChange, _
                        dtLastLockoutDate)
                    End If


                    Return Nothing
                Finally
                    If reader IsNot Nothing Then
                        reader.Close()
                        reader = Nothing
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

        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////

        Public Overloads Overrides Function GetUserNameByEmail(ByVal email As String) As String
            SecUtility.CheckParameter(email, False, False, False, 256, "email")


            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_GetUserByEmail", holder.Connection)
                    Dim username As String = Nothing
                    Dim reader As SqlDataReader = Nothing

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@Email", SqlDbType.NVarChar, email))

                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)
                    Try
                        reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
                        If reader.Read() Then
                            username = GetNullableString(reader, 0)
                            If RequiresUniqueEmail AndAlso reader.Read() Then
                                Throw New ProviderException(SR.GetString(SR.Membership_more_than_one_user_with_email))
                            End If
                        End If
                    Finally
                        If reader IsNot Nothing Then
                            reader.Close()
                        End If
                    End Try
                    Return username
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

        Public Overloads Overrides Function DeleteUser(ByVal username As String, ByVal deleteAllRelatedData As Boolean) As Boolean
            SecUtility.CheckParameter(username, True, True, True, 256, "username")

            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)
                    Dim cmd As New SqlCommand("dbo.aspnet_Users_DeleteUser", holder.Connection)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, username))

                    If deleteAllRelatedData Then
                        cmd.Parameters.Add(CreateInputParam("@TablesToDeleteFrom", SqlDbType.Int, &HF))
                    Else
                        cmd.Parameters.Add(CreateInputParam("@TablesToDeleteFrom", SqlDbType.Int, 1))
                    End If

                    Dim p As New SqlParameter("@NumTablesDeletedFrom", SqlDbType.Int)
                    p.Direction = ParameterDirection.Output
                    cmd.Parameters.Add(p)
                    cmd.ExecuteNonQuery()

                    Dim status As Integer = (If((p.Value IsNot Nothing), CInt(p.Value), -1))

                    Return (status > 0)
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


        Public Overloads Overrides Function GetAllUsers(ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As MembershipUserCollection
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

            Dim users As New MembershipUserCollection()
            totalRecords = 0
            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_GetAllUsers", holder.Connection)
                    Dim reader As SqlDataReader = Nothing
                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@PageIndex", SqlDbType.Int, pageIndex))
                    cmd.Parameters.Add(CreateInputParam("@PageSize", SqlDbType.Int, pageSize))
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)
                    Try
                        reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
                        While reader.Read()
                            Dim username As String, email As String, passwordQuestion As String, comment As String
                            Dim isApproved As Boolean
                            Dim dtCreate As DateTime, dtLastLogin As DateTime, dtLastActivity As DateTime, dtLastPassChange As DateTime
                            Dim userId As Guid
                            Dim isLockedOut As Boolean
                            Dim dtLastLockoutDate As DateTime

                            username = GetNullableString(reader, 0)
                            email = GetNullableString(reader, 1)
                            passwordQuestion = GetNullableString(reader, 2)
                            comment = GetNullableString(reader, 3)
                            isApproved = reader.GetBoolean(4)
                            dtCreate = reader.GetDateTime(5).ToLocalTime()
                            dtLastLogin = reader.GetDateTime(6).ToLocalTime()
                            dtLastActivity = reader.GetDateTime(7).ToLocalTime()
                            dtLastPassChange = reader.GetDateTime(8).ToLocalTime()
                            userId = reader.GetGuid(9)
                            isLockedOut = reader.GetBoolean(10)
                            dtLastLockoutDate = reader.GetDateTime(11).ToLocalTime()

                            users.Add(New MembershipUser(Me.Name, username, userId, email, passwordQuestion, comment, _
                            isApproved, isLockedOut, dtCreate, dtLastLogin, dtLastActivity, dtLastPassChange, _
                            dtLastLockoutDate))
                        End While
                    Finally
                        If reader IsNot Nothing Then
                            reader.Close()
                        End If
                        If p.Value IsNot Nothing AndAlso TypeOf p.Value Is Integer Then
                            totalRecords = CInt(p.Value)
                        End If
                    End Try
                Finally
                    If holder IsNot Nothing Then
                        holder.Close()
                        holder = Nothing
                    End If
                End Try
            Catch
                Throw
            End Try
            Return users
        End Function
        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////

        Public Overloads Overrides Function GetNumberOfUsersOnline() As Integer

            Try
                Dim holder As SqlConnectionHolder = Nothing
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_GetNumberOfUsersOnline", holder.Connection)
                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@MinutesSinceLastInActive", SqlDbType.Int, System.Web.Security.Membership.UserIsOnlineTimeWindow))
                    cmd.Parameters.Add(CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)
                    cmd.ExecuteNonQuery()
                    Dim num As Integer = (If((p.Value IsNot Nothing), CInt(p.Value), -1))
                    Return num
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
        '''//////////////////////////////////////////////////////////////////////////

        Public Overloads Overrides Function FindUsersByName(ByVal usernameToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As MembershipUserCollection
            SecUtility.CheckParameter(usernameToMatch, True, True, False, 256, "usernameToMatch")

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
                totalRecords = 0
                Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                p.Direction = ParameterDirection.ReturnValue
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_FindUsersByName", holder.Connection)
                    Dim users As New MembershipUserCollection()
                    Dim reader As SqlDataReader = Nothing

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserNameToMatch", SqlDbType.NVarChar, usernameToMatch))
                    cmd.Parameters.Add(CreateInputParam("@PageIndex", SqlDbType.Int, pageIndex))
                    cmd.Parameters.Add(CreateInputParam("@PageSize", SqlDbType.Int, pageSize))
                    cmd.Parameters.Add(p)
                    Try
                        reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
                        While reader.Read()
                            Dim username As String, email As String, passwordQuestion As String, comment As String
                            Dim isApproved As Boolean
                            Dim dtCreate As DateTime, dtLastLogin As DateTime, dtLastActivity As DateTime, dtLastPassChange As DateTime
                            Dim userId As Guid
                            Dim isLockedOut As Boolean
                            Dim dtLastLockoutDate As DateTime

                            username = GetNullableString(reader, 0)
                            email = GetNullableString(reader, 1)
                            passwordQuestion = GetNullableString(reader, 2)
                            comment = GetNullableString(reader, 3)
                            isApproved = reader.GetBoolean(4)
                            dtCreate = reader.GetDateTime(5).ToLocalTime()
                            dtLastLogin = reader.GetDateTime(6).ToLocalTime()
                            dtLastActivity = reader.GetDateTime(7).ToLocalTime()
                            dtLastPassChange = reader.GetDateTime(8).ToLocalTime()
                            userId = reader.GetGuid(9)
                            isLockedOut = reader.GetBoolean(10)
                            dtLastLockoutDate = reader.GetDateTime(11).ToLocalTime()

                            users.Add(New MembershipUser(Me.Name, username, userId, email, passwordQuestion, comment, _
                            isApproved, isLockedOut, dtCreate, dtLastLogin, dtLastActivity, dtLastPassChange, _
                            dtLastLockoutDate))
                        End While

                        Return users
                    Finally
                        If reader IsNot Nothing Then
                            reader.Close()
                        End If
                        If p.Value IsNot Nothing AndAlso TypeOf p.Value Is Integer Then
                            totalRecords = CInt(p.Value)
                        End If
                    End Try
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
        '''//////////////////////////////////////////////////////////////////////////
        Public Overloads Overrides Function FindUsersByEmail(ByVal emailToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As MembershipUserCollection
            SecUtility.CheckParameter(emailToMatch, False, False, False, 256, "emailToMatch")

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
                totalRecords = 0
                Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                p.Direction = ParameterDirection.ReturnValue
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_FindUsersByEmail", holder.Connection)
                    Dim users As New MembershipUserCollection()
                    Dim reader As SqlDataReader = Nothing

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@EmailToMatch", SqlDbType.NVarChar, emailToMatch))
                    cmd.Parameters.Add(CreateInputParam("@PageIndex", SqlDbType.Int, pageIndex))
                    cmd.Parameters.Add(CreateInputParam("@PageSize", SqlDbType.Int, pageSize))
                    cmd.Parameters.Add(p)
                    Try
                        reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
                        While reader.Read()
                            Dim username As String, email As String, passwordQuestion As String, comment As String
                            Dim isApproved As Boolean
                            Dim dtCreate As DateTime, dtLastLogin As DateTime, dtLastActivity As DateTime, dtLastPassChange As DateTime
                            Dim userId As Guid
                            Dim isLockedOut As Boolean
                            Dim dtLastLockoutDate As DateTime

                            username = GetNullableString(reader, 0)
                            email = GetNullableString(reader, 1)
                            passwordQuestion = GetNullableString(reader, 2)
                            comment = GetNullableString(reader, 3)
                            isApproved = reader.GetBoolean(4)
                            dtCreate = reader.GetDateTime(5).ToLocalTime()
                            dtLastLogin = reader.GetDateTime(6).ToLocalTime()
                            dtLastActivity = reader.GetDateTime(7).ToLocalTime()
                            dtLastPassChange = reader.GetDateTime(8).ToLocalTime()
                            userId = reader.GetGuid(9)
                            isLockedOut = reader.GetBoolean(10)
                            dtLastLockoutDate = reader.GetDateTime(11).ToLocalTime()

                            users.Add(New MembershipUser(Me.Name, username, userId, email, passwordQuestion, comment, _
                            isApproved, isLockedOut, dtCreate, dtLastLogin, dtLastActivity, dtLastPassChange, _
                            dtLastLockoutDate))
                        End While

                        Return users
                    Finally
                        If reader IsNot Nothing Then
                            reader.Close()
                        End If
                        If p.Value IsNot Nothing AndAlso TypeOf p.Value Is Integer Then
                            totalRecords = CInt(p.Value)
                        End If
                    End Try
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
        Private Function CheckPassword(ByVal username As String, ByVal password As String, ByVal updateLastLoginActivityDate As Boolean, ByVal failIfNotApproved As Boolean) As Boolean
            Dim salt As String = ""
            Dim passwordFormat As Integer = 0
            Return CheckPassword(username, password, updateLastLoginActivityDate, failIfNotApproved, salt, passwordFormat)
        End Function
        Private Function CheckPassword(ByVal username As String, ByVal password As String, ByVal updateLastLoginActivityDate As Boolean, ByVal failIfNotApproved As Boolean, ByRef salt As String, ByRef passwordFormat As Integer) As Boolean
            Dim holder As SqlConnectionHolder = Nothing
            Dim passwdFromDB As String = ""
            Dim status As Integer
            Dim failedPasswordAttemptCount As Integer
            Dim failedPasswordAnswerAttemptCount As Integer
            Dim isPasswordCorrect As Boolean
            Dim isApproved As Boolean
            Dim lastLoginDate As DateTime, lastActivityDate As DateTime

            GetPasswordWithFormat(username, updateLastLoginActivityDate, status, passwdFromDB, passwordFormat, salt, _
            failedPasswordAttemptCount, failedPasswordAnswerAttemptCount, isApproved, lastLoginDate, lastActivityDate)
            If status <> 0 Then
                Return False
            End If
            If Not isApproved AndAlso failIfNotApproved Then
                Return False
            End If

            Dim encodedPasswd As String = EncodePassword(password, passwordFormat, salt)

            isPasswordCorrect = passwdFromDB.Equals(encodedPasswd)

            If isPasswordCorrect AndAlso failedPasswordAttemptCount = 0 AndAlso failedPasswordAnswerAttemptCount = 0 Then
                Return True
            End If

            Try
                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_UpdateUserInfo", holder.Connection)
                    Dim dtNow As DateTime = DateTime.UtcNow
                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, username))
                    cmd.Parameters.Add(CreateInputParam("@IsPasswordCorrect", SqlDbType.Bit, isPasswordCorrect))
                    cmd.Parameters.Add(CreateInputParam("@UpdateLastLoginActivityDate", SqlDbType.Bit, updateLastLoginActivityDate))
                    cmd.Parameters.Add(CreateInputParam("@MaxInvalidPasswordAttempts", SqlDbType.Int, MaxInvalidPasswordAttempts))
                    cmd.Parameters.Add(CreateInputParam("@PasswordAttemptWindow", SqlDbType.Int, PasswordAttemptWindow))
                    cmd.Parameters.Add(CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, dtNow))
                    cmd.Parameters.Add(CreateInputParam("@LastLoginDate", SqlDbType.DateTime, If(isPasswordCorrect, dtNow, lastLoginDate)))
                    cmd.Parameters.Add(CreateInputParam("@LastActivityDate", SqlDbType.DateTime, If(isPasswordCorrect, dtNow, lastActivityDate)))
                    Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)

                    cmd.ExecuteNonQuery()

                    status = (If((p.Value IsNot Nothing), CInt(p.Value), -1))
                Finally
                    If holder IsNot Nothing Then
                        holder.Close()
                        holder = Nothing
                    End If
                End Try
            Catch
                Throw
            End Try

            Return isPasswordCorrect
        End Function

        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////
        Private Sub GetPasswordWithFormat(ByVal username As String, ByVal updateLastLoginActivityDate As Boolean, ByRef status As Integer, ByRef password As String, ByRef passwordFormat As Integer, ByRef passwordSalt As String, _
        ByRef failedPasswordAttemptCount As Integer, ByRef failedPasswordAnswerAttemptCount As Integer, ByRef isApproved As Boolean, ByRef lastLoginDate As DateTime, ByRef lastActivityDate As DateTime)
            Try
                Dim holder As SqlConnectionHolder = Nothing
                Dim reader As SqlDataReader = Nothing
                Dim p As SqlParameter = Nothing

                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_GetPasswordWithFormat", holder.Connection)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, username))
                    cmd.Parameters.Add(CreateInputParam("@UpdateLastLoginActivityDate", SqlDbType.Bit, updateLastLoginActivityDate))
                    cmd.Parameters.Add(CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))

                    p = New SqlParameter("@ReturnValue", SqlDbType.Int)
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)

                    reader = cmd.ExecuteReader(CommandBehavior.SingleRow)

                    status = -1

                    If reader.Read() Then
                        password = reader.GetString(0)
                        passwordFormat = reader.GetInt32(1)
                        passwordSalt = reader.GetString(2)
                        failedPasswordAttemptCount = reader.GetInt32(3)
                        failedPasswordAnswerAttemptCount = reader.GetInt32(4)
                        isApproved = reader.GetBoolean(5)
                        lastLoginDate = reader.GetDateTime(6)
                        lastActivityDate = reader.GetDateTime(7)
                    Else
                        password = Nothing
                        passwordFormat = 0
                        passwordSalt = Nothing
                        failedPasswordAttemptCount = 0
                        failedPasswordAnswerAttemptCount = 0
                        isApproved = False
                        lastLoginDate = DateTime.UtcNow
                        lastActivityDate = DateTime.UtcNow
                    End If
                Finally
                    If reader IsNot Nothing Then
                        reader.Close()
                        reader = Nothing

                        status = (If((p.Value IsNot Nothing), CInt(p.Value), -1))
                    End If

                    If holder IsNot Nothing Then
                        holder.Close()
                        holder = Nothing
                    End If
                End Try
            Catch
                Throw

            End Try
        End Sub

        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////
        Private Function GetPasswordFromDB(ByVal username As String, ByVal passwordAnswer As String, ByVal requiresQuestionAndAnswer As Boolean, ByRef passwordFormat As Integer, ByRef status As Integer) As String
            Try
                Dim holder As SqlConnectionHolder = Nothing
                Dim reader As SqlDataReader = Nothing
                Dim p As SqlParameter = Nothing

                Try
                    holder = SqlConnectionHelper.GetConnection(_sqlConnectionString, True)
                    CheckSchemaVersion(holder.Connection)

                    Dim cmd As New SqlCommand("dbo.aspnet_Membership_GetPassword", holder.Connection)

                    cmd.CommandTimeout = CommandTimeout
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, ApplicationName))
                    cmd.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, username))
                    cmd.Parameters.Add(CreateInputParam("@MaxInvalidPasswordAttempts", SqlDbType.Int, MaxInvalidPasswordAttempts))
                    cmd.Parameters.Add(CreateInputParam("@PasswordAttemptWindow", SqlDbType.Int, PasswordAttemptWindow))
                    cmd.Parameters.Add(CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))

                    If requiresQuestionAndAnswer Then
                        cmd.Parameters.Add(CreateInputParam("@PasswordAnswer", SqlDbType.NVarChar, passwordAnswer))
                    End If

                    p = New SqlParameter("@ReturnValue", SqlDbType.Int)
                    p.Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add(p)

                    reader = cmd.ExecuteReader(CommandBehavior.SingleRow)

                    Dim password As String = Nothing

                    status = -1

                    If reader.Read() Then
                        password = reader.GetString(0)
                        passwordFormat = reader.GetInt32(1)
                    Else
                        password = Nothing
                        passwordFormat = 0
                    End If

                    Return password
                Finally
                    If reader IsNot Nothing Then
                        reader.Close()
                        reader = Nothing

                        status = (If((p.Value IsNot Nothing), CInt(p.Value), -1))
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

        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////
        Private Function GetEncodedPasswordAnswer(ByVal username As String, ByVal passwordAnswer As String) As String
            If passwordAnswer IsNot Nothing Then
                passwordAnswer = passwordAnswer.Trim()
            End If
            If String.IsNullOrEmpty(passwordAnswer) Then
                Return passwordAnswer
            End If
            Dim status As Integer, passwordFormat As Integer, failedPasswordAttemptCount As Integer, failedPasswordAnswerAttemptCount As Integer
            Dim password As String = "", passwordSalt As String = ""
            Dim isApproved As Boolean = False
            Dim lastLoginDate As DateTime, lastActivityDate As DateTime
            GetPasswordWithFormat(username, False, status, password, passwordFormat, passwordSalt, _
            failedPasswordAttemptCount, failedPasswordAnswerAttemptCount, isApproved, lastLoginDate, lastActivityDate)
            If status = 0 Then
                Return EncodePassword(passwordAnswer.ToLower(CultureInfo.InvariantCulture), passwordFormat, passwordSalt)
            Else
                Throw New ProviderException(GetExceptionText(status))
            End If
        End Function

        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////

        Public Overridable Function GeneratePassword() As String
            Return System.Web.Security.Membership.GeneratePassword(If(MinRequiredPasswordLength < PASSWORD_SIZE, PASSWORD_SIZE, MinRequiredPasswordLength), MinRequiredNonAlphanumericCharacters)
        End Function

        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////
        Private Function CreateInputParam(ByVal paramName As String, ByVal dbType As SqlDbType, ByVal objValue As Object) As SqlParameter

            Dim param As New SqlParameter(paramName, dbType)

            If objValue Is Nothing Then
                param.IsNullable = True
                param.Value = DBNull.Value
            Else
                param.Value = objValue
            End If

            Return param
        End Function

        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////
        Private Function GetNullableString(ByVal reader As SqlDataReader, ByVal col As Integer) As String
            If reader.IsDBNull(col) = False Then
                Return reader.GetString(col)
            End If

            Return Nothing
        End Function
        '''//////////////////////////////////////////////////////////////////////////
        '''//////////////////////////////////////////////////////////////////////////
        Private Function GetExceptionText(ByVal status As Integer) As String
            Dim key As String
            Select Case status
                Case 0
                    Return [String].Empty
                Case 1
                    key = SR.Membership_UserNotFound
                    Exit Select
                Case 2
                    key = SR.Membership_WrongPassword
                    Exit Select
                Case 3
                    key = SR.Membership_WrongAnswer
                    Exit Select
                Case 4
                    key = SR.Membership_InvalidPassword
                    Exit Select
                Case 5
                    key = SR.Membership_InvalidQuestion
                    Exit Select
                Case 6
                    key = SR.Membership_InvalidAnswer
                    Exit Select
                Case 7
                    key = SR.Membership_InvalidEmail
                    Exit Select
                Case 99
                    key = SR.Membership_AccountLockOut
                    Exit Select
                Case Else
                    key = SR.Provider_Error
                    Exit Select
            End Select
            Return SR.GetString(key)
        End Function

        Private Function IsStatusDueToBadPassword(ByVal status As Integer) As Boolean
            Return (status >= 2 AndAlso status <= 6 OrElse status = 99)
        End Function

        Private Function RoundToSeconds(ByVal dt As DateTime) As DateTime
            Return New DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second)
        End Function
        Friend Function GenerateSalt() As String
            Dim buf As Byte() = New Byte(15) {}
            Dim RNGCryptoServiceProvider As New RNGCryptoServiceProvider()
            RNGCryptoServiceProvider.GetBytes(buf)
            Return Convert.ToBase64String(buf)
        End Function
        Friend Function EncodePassword(ByVal pass As String, ByVal passwordFormat As Integer, ByVal salt As String) As String
            If passwordFormat = 0 Then
                ' MembershipPasswordFormat.Clear
                Return pass
            End If

            Dim bIn As Byte() = Encoding.Unicode.GetBytes(pass)
            Dim bSalt As Byte() = Convert.FromBase64String(salt)
            Dim bAll As Byte() = New Byte(bSalt.Length + (bIn.Length - 1)) {}
            Dim bRet As Byte() = Nothing

            Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length)
            Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length)
            If passwordFormat = 1 Then
                ' MembershipPasswordFormat.Hashed
                'Dim s As HashAlgorithm = HashAlgorithm.Create("SHA1")
                Dim s As HashAlgorithm = HashAlgorithm.Create(System.Web.Security.Membership.HashAlgorithmType)
                bRet = s.ComputeHash(bAll)
            Else
                bRet = EncryptPassword(bAll)
            End If

            Return Convert.ToBase64String(bRet)
        End Function

        Friend Function UnEncodePassword(ByVal pass As String, ByVal passwordFormat As Integer) As String
            Select Case passwordFormat
                Case 0
                    ' MembershipPasswordFormat.Clear:
                    Return pass
                Case 1
                    ' MembershipPasswordFormat.Hashed:
                    Throw New ProviderException(SR.GetString(SR.Provider_can_not_decode_hashed_password))
                Case Else
                    Dim bIn As Byte() = Convert.FromBase64String(pass)
                    Dim bRet As Byte() = DecryptPassword(bIn)
                    If bRet Is Nothing Then
                        Return Nothing
                    End If
                    Return Encoding.Unicode.GetString(bRet, 16, bRet.Length - 16)
            End Select
        End Function
    End Class

End Namespace
