Imports System.Diagnostics

Partial Class ApplicationLogout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Session("PageArgs") = New Dictionary(Of String, String)
        HttpContext.Current.Session.RemoveAll()
        HttpContext.Current.Session.Abandon()
        Response.Redirect("/")
    End Sub
End Class

