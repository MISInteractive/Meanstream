
Partial Class Meanstream_Administration_UserControls_CreateUser
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Me.btnSave.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-save.png"
            If Not Membership.RequiresQuestionAndAnswer Then
                Me.pSecurityQuestionRequired.Visible = False
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim result As String = ""
        Dim Password As String = txtPassword.Text.Trim()
        Dim securityQuestion As String = Me.comboQuestion.selectedValue
        Dim securityAnswer As String = Me.txtAnswer.Text.Trim

        If Not Me.chkGeneratePassword.Checked Then
            If Password <> Me.txtConfirmPassword.Text Then
                Me.btnSave.ThrowFailure = True
                Me.btnSave.FailMessage = "Passwords do not match"
                Return
            End If
        End If

        If Membership.RequiresQuestionAndAnswer Then
            If securityQuestion = Nothing Then
                Me.btnSave.ThrowFailure = True
                Me.btnSave.FailMessage = "Please select a valid security question"
                Return
            End If
            If securityAnswer = "" Then
                Me.btnSave.ThrowFailure = True
                Me.btnSave.FailMessage = "Please enter a valid security answer"
                Return
            End If
        End If

        Try
            Dim roles() As String = {}
            Dim status As System.Web.Security.MembershipCreateStatus = Nothing

            If Membership.RequiresQuestionAndAnswer Then
                status = Meanstream.Portal.Core.Membership.MembershipService.Current.CreateUser(Me.txtUsername.Text, Me.txtEmail.Text, Password, securityQuestion, securityAnswer, roles, Me.chkAuthorize.Checked, Me.chkGeneratePassword.Checked, Me.chkNotify.Checked)
            Else
                status = Meanstream.Portal.Core.Membership.MembershipService.Current.CreateUser(Me.txtUsername.Text, Me.txtEmail.Text, Password, roles, Me.chkAuthorize.Checked, Me.chkGeneratePassword.Checked, Me.chkNotify.Checked)
            End If

            If status = MembershipCreateStatus.Success Then
                Dim ProfileCommon As ProfileCommon = Profile.GetProfile(Me.txtUsername.Text)
                ProfileCommon.FirstName = Me.txtFirstName.Text.Trim
                ProfileCommon.LastName = Me.txtLastName.Text.Trim
                ProfileCommon.DisplayName = Me.txtDisplayName.Text
                ProfileCommon.Save()
                Me.btnSave.SuccessMessage = "The user has been created"
                Me.litUserName.Value = Me.txtUsername.Text
            ElseIf status = MembershipCreateStatus.DuplicateEmail Then
                Me.btnSave.ThrowFailure = True
                Me.btnSave.FailMessage = "Email exists in the system"
            ElseIf status = MembershipCreateStatus.DuplicateUserName Then
                Me.btnSave.ThrowFailure = True
                Me.btnSave.FailMessage = "Username exists in the system"
            ElseIf status = MembershipCreateStatus.InvalidUserName Then
                Me.btnSave.ThrowFailure = True
                Me.btnSave.FailMessage = "Please enter a valid username (6-20 characters)"
            ElseIf status = MembershipCreateStatus.InvalidQuestion Then
                Me.btnSave.ThrowFailure = True
                Me.btnSave.FailMessage = "Please enter a valid security question"
            ElseIf status = MembershipCreateStatus.InvalidAnswer Then
                Me.btnSave.ThrowFailure = True
                Me.btnSave.FailMessage = "Please enter a valid security answer"
            ElseIf status = MembershipCreateStatus.InvalidPassword Then
                Me.btnSave.ThrowFailure = True
                Me.btnSave.FailMessage = "Please enter a valid password"
            ElseIf status = MembershipCreateStatus.InvalidEmail Then
                Me.btnSave.ThrowFailure = True
                Me.btnSave.FailMessage = "Please enter a valid email address"
            End If
        Catch ex As Exception
            Meanstream.Portal.Core.Instrumentation.PortalTrace.Fail(ex.Message, Meanstream.Portal.Core.Instrumentation.DisplayMethodInfo.DoNotDisplay)
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub
End Class
