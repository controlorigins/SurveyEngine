
Public Class navigator
    Inherits PortalPageBase

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim Feilds As New List(Of String)
        Feilds.Add("AID") ' app id 
        Feilds.Add("PID") ' Page id to go to 
        Feilds.Add("UUID") ' UserUnique id

        Response.Write(Request.RawUrl & "<br />")

        ClearPageArguments()

        For Each i In Request.QueryString
            SetPageArgument(i.ToString, Request.QueryString(i.ToString))
            Response.Write(i.ToString & " = " & Request.QueryString(i.ToString) & "<br />")
        Next

        SetPage("SurveyResponseScore.ascx")
        LoadPage()

    End Sub

    Public Sub SetPage(ByVal myControlName As String)
        Try
            SetPageArgument("pid", (From i In AppInfo.Navigation Where i.TartgetPage.ToLower = myControlName.ToLower() Select i.Id).Single())
        Catch ex As Exception
            ClearPageArguments()
        End Try
    End Sub

End Class
