
Partial Class controls_MessageFormControl
    Inherits ApplicationControlBase

    Event MailSent()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim userlist = GetAllUsers()
        ddtoUsers.DataSource = userlist
        ddtoUsers.DataTextField = "DisplayName"
        ddtoUsers.DataValueField = "ApplicationUserID"
        ddtoUsers.DataBind()

        AdminPanel.Visible = IsUserAppAdmin(UserInfo.ApplicationUserID, AppInfo.ApplicationID)
    End Sub

    Protected Sub cmd_SendMessage_Click(sender As Object, e As EventArgs)
        SendMessage(UserInfo.ApplicationUserID, ddtoUsers.SelectedValue, tbsubject.Text, tbmessage.Text)
        RaiseEvent MailSent()
    End Sub
End Class
