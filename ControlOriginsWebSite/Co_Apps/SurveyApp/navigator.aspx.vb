
Public Class Co_Apps_SurveyApp_navigator
    Inherits PortalPageBase

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ClearPageArguments()
        For Each i In Request.QueryString
            SetPageArgument(i.ToString.ToLower, Request.QueryString(i.ToString).ToLower)
        Next
        SetPage("SurveyDashboard.ascx")
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
