
Public Class Co_Apps_ControlOrigins_COContentPage
    Inherits ApplicationControlBase

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        COContentPage.AppPropID = "COContentPage-" & (From i In GetPageArguments() Where i.Key.ToLower = "pid" Select i.Value).FirstOrDefault
        UserInfo = UserControler.GetUserByID(UserInfo.ApplicationUserID)
        AppInfo = AppControler.GetAppByID(AppInfo.ApplicationID)
    End Sub
End Class
