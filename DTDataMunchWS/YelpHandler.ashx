<%@ WebHandler Language="VB" Class="YelpHandler" %>

Imports System
Imports System.Web

Public Class YelpHandler : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim CityName = GetProperty("city", "Dallas")
        Dim StateName = GetProperty("state","TX")
        Dim PageKeywords = GetProperty("keywords", "best restaraunts")

        'Dim mycon As New DTYelpAPI.DTYelpMuncherTools
        'context.Response.ContentType = "text/html"
        'context.Response.Write(mycon.GetHTML(mycon.OAuth(PageKeywords, String.Format("{0} , {1} ", CityName, StateName)), My.Computer.FileSystem.ReadAllText(context.Server.MapPath("\yelptemplate.html"))))

    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property


    Public Function GetProperty(ByVal myProperty As String, ByVal curValue As String) As String
        Dim myValue As String = ""
        If Len(HttpContext.Current.Request.QueryString(myProperty)) > 0 Then
            myValue = HttpContext.Current.Request.QueryString(myProperty).ToString
        ElseIf Len(HttpContext.Current.Request.Form.Item(myProperty)) > 0 Then
            myValue = HttpContext.Current.Request.Form.Item(myProperty).ToString
        Else
            If curValue Is Nothing Then
                myValue = String.Empty
            Else
                myValue = curValue
            End If
        End If
        Return myValue
    End Function



End Class