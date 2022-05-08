
Public Class Co_Apps_SurveyAdmin_navigator
    Inherits PortalPageBase

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ClearPageArguments()

        For Each i In Request.QueryString
            SetPageArgument(i.ToString.ToLower, Request.QueryString(i.ToString).ToLower)
        Next
        Dim myAction As String = GetPageArgument("action").Second.ToString.ToLower
        Select Case myAction
            Case "questionbank"
                SetPage("QuestionBank.ascx")
            Case "applicationuserview"
                SetPage("UserAdmin.ascx")
            Case "companyview"
                SetPage("CompanyAdmin.ascx")
            Case "projectview"
                SetPage("ProjectAdmin.ascx")
            Case Else
                SetPage("SurveyAdminDashboard.ascx")
        End Select
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
