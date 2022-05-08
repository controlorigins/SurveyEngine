
Partial Class Co_Apps_ControlOrigins_CORegister
    Inherits ApplicationControlBase


    Protected Sub cmd_RegNewUser_Click(sender As Object, e As EventArgs)

        ' check for user
        If UserControler.IsUserInSystem(emailaddress.Text.Trim) Then
            ' already have this user 
        Else
            ' set up new user 
            UserInfo = UserControler.CreateNewUser(firstname.Text.Trim, lastname.Text.Trim, emailaddress.Text.Trim, password.Text)
            Response.Redirect("/")
        End If


    End Sub
End Class
