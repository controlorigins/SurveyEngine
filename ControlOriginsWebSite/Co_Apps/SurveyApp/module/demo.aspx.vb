
Public Class modules_demo
    Inherits System.Web.UI.Page

    Protected Overrides Sub OnPreInit(e As EventArgs)
        MyBase.OnPreInit(e)
        Response.Cookies.Add(New HttpCookie("mycookie", "WebProjectMechanics Cookie"))
    End Sub


End Class
