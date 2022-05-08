Imports CODataCon.com.controlorigins.ws

Partial Class Co_Apps_ControlOrigins_CODashBoard
    Inherits ApplicationControlBase

    Protected Sub Pagestartup() Handles Me.Load
        If Not IsPostBack Then
            updatelist()
        End If
    End Sub

    Protected Sub updatelist()
        Dim mycoll = (From i In UserInfo.ApplicationUserRoleList Where i.UserInroled = True).ToList()
        If mycoll.Count > 0 Then
            RegisteredApps.DataSource = mycoll
            RegisteredApps.DataBind()
        Else
            YourAppAlert.boldnote = "You have not Subscribed to any projects yet."
            YourAppAlert.message = "Please select a project form the 'Available Projects' option."
            YourAppAlert.alertType = AlertBoxType.warning
            YourAppAlert.Visible = True
        End If
    End Sub

    Protected Sub cmd_LoadApp_Click(sender As LinkButton, e As EventArgs)
        AppInfo = AppControler.GetAppByID(CInt(sender.Attributes("data-appid")))
        ' get default page from here
        SetPageArgument("pid", AppInfo.DefaultAppPage)
        Response.Redirect("/")
    End Sub

    Protected Sub cmd_RemoveApp_Click(sender As Object, e As EventArgs)
        Dim AppInfotemp = AppControler.GetAppByID(CInt(sender.Attributes("data-appid")))
        AppControler.UnRegisterUserFromApp(UserInfo.ApplicationUserID, AppInfotemp.ApplicationID)
        UserInfo = UserControler.GetUserByID(UserInfo.ApplicationUserID)
        Response.Redirect("/")
    End Sub

End Class
