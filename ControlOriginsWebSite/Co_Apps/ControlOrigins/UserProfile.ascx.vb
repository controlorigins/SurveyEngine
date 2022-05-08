
Partial Class UserProfile
    Inherits ApplicationControlBase
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            firstname.Text = UserInfo.FirstNM
            lastname.Text = UserInfo.LastNM
            emailaddress.Text = UserInfo.eMailAddress
            tbAccountNM.Text = UserInfo.AccountNM
        End If
    End Sub

    Protected Sub cmd_RegNewUser_Click(sender As Object, e As EventArgs)
        UserInfo.FirstNM = firstname.Text.Trim
        UserInfo.LastNM = lastname.Text.Trim
        UserInfo.eMailAddress = emailaddress.Text.Trim
        UserInfo.AccountNM = tbAccountNM.Text.Trim
        UserInfo.UserLogin = tbAccountNM.Text.Trim
        UserControler.UpdateUser(UserInfo)
        AlertBox1.message = UserInfo.DisplayName & " has been updated."
        AlertBox1.Visible = True
    End Sub

    Protected Sub cmd_editpass_Click(sender As Object, e As EventArgs)
        PassChange.Visible = True
    End Sub

    Protected Sub cmd_cancelSave_Click(sender As Object, e As EventArgs)
        PassChange.Visible = False
        tbNEWPASS.Text = ""
        tbNEWPASSVER.Text = ""
        tbOLDPASS.Text = ""
    End Sub

    Protected Sub cmd_saveNewPass_Click(sender As Object, e As EventArgs) Handles cmd_saveNewPass.Click

        If UserControler.Checkpassword(UserInfo.ApplicationUserID, tbOLDPASS.Text.Trim) Then
            UserControler.updatepassword(UserInfo.ApplicationUserID, tbNEWPASS.Text.Trim)
        End If
        AlertBox1.message = UserInfo.DisplayName & " , your password has been updated."
        AlertBox1.Visible = True
        PassChange.Visible = False

    End Sub
End Class
