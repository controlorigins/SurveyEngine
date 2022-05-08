
Partial Class Co_Apps_ControlOrigins_COLogin
    Inherits ApplicationControlBase

    Protected Sub cmd_login_Click(sender As Object, e As EventArgs)
        UserInfo = GetGuestUser()
        Dim testuser = UserControler.UserLogin(tbEmail.Text.Trim, tbpass.Text.Trim)
        If testuser.ApplicationUserID > 0 Then
            UserInfo = testuser
            Response.Redirect("/")
        Else ' User faild Login 
            tbpass.Text = ""
            LoginErrorMessage.Visible = True
        End If
        
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(AppInfo.ApplicationNM) then 
            AppInfo = GetAppByID(1)
        End If
        'litAppName.Text = AppInfo.ApplicationNM
    End Sub
End Class
