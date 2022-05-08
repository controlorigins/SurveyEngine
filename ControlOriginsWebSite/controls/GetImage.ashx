<%@ WebHandler Language="VB" Class="GetImage" %>

Imports System
Imports System.Web
Imports CODataCon
Imports CODataCon.com.controlorigins.ws

Public Class GetImage : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim myID As Integer = context.Request.QueryString("id")
        If myID > 0 Then
            Using mycon As New DataControler()
                Dim myFile As QuestionItem  = mycon.GetQuestionByQuestionID(myID)
                download(myFile.FileData, "image/jpeg")
            End Using
        End If
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    Protected Sub download(ByVal bytes() As Byte, ByVal ContentType As String)
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.Charset = ""
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.ContentType = ContentType
        HttpContext.Current.Response.BinaryWrite(bytes)
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.End()
    End Sub
End Class