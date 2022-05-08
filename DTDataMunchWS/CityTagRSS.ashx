<%@ WebHandler Language="VB" Class="CityTagRSS"  %>

Public Class CityTagRSS : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/xml"
        context.Response.Write(CityTagRSSBuilder.GetXML(HttpContext.Current.Request.QueryString("Tags")))
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class