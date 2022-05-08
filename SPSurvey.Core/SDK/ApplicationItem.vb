Public Class ApplicationItem
    Public Property ApplicationID As Integer 
    Public Property ApplicationNM As String 
    Public Property ApplicationCD As String 
    Public Property ApplicationDS As String 
    Public Property ApplicationShortNM As String 
    Public Property ApplicationTypeID As Integer 
    Public Property CompanyID As Integer
    Public Property CompanyNM As String 
    Public Property MenuOrder As Integer 
    Public Property ApplicationTypeNM As String 
    Public Property ApplicationUserList As New List(Of ApplicationUserRoleItem) 
    Public Property ApplicationSurveyList As New List(Of ApplicationSurveyItem) 
    Public Property ModifiedDT As DateTime
    Public Property ModifiedID As Integer
    Public Property SurveyCount As Integer
    Public Property UserCount As Integer
    Public Property SurveyResponseCount As Integer
    Property ApplicationFolder As String
    Property DefaultAppPage As Integer
    Property Properties As New List(Of PropertyItem)
    Property Navigation As New List(Of NavigationMenuItem)
End Class
