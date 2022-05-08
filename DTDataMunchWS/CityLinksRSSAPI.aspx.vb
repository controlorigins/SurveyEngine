Imports LINQHelper.System.Linq.Dynamic

Partial Class CityLinksRSSAPI
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Context.Response.ContentType = "text/xml"
        Context.Response.Write(CityTagRSSBuilder.GetXML(HttpContext.Current.Request.QueryString("Tags")))
    End Sub

End Class
