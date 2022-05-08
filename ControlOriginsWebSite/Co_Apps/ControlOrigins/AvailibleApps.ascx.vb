
Partial Class Co_Apps_ControlOrigins_availibleApps_old
    Inherits ApplicationControlBase


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim mycoll = (From i In UserInfo.ApplicationUserRoleList Where i.UserInroled = False).ToList()
        If mycoll.Count > 0 Then
            availableApps.DataSource = mycoll
            availableApps.DataBind()
        Else
            AlertBox.boldnote = "No Projects available."
            AlertBox.message = "Please contact your Sponsor and ask about project availibility for your account."
            AlertBox.alertType = AlertBoxType.warning
            AlertBox.Visible = True
        End If
    End Sub

    Protected Sub cmd_SubscribetoApp_Click(sender As LinkButton, e As EventArgs)
        AppControler.SubscribeMeToApp(UserInfo.ApplicationUserID, CInt(sender.Attributes("data-appid")))
        UserInfo = UserControler.GetUserByID(UserInfo.ApplicationUserID)
        LoadPage()
    End Sub


End Class
