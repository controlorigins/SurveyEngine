Public Class ApplicationUserItem

    Property ApplicationUserID As Integer 
    Property UserLogin As String
    Property AccountNM As String 
    Property FirstNM As String 
    Property LastNM As String 
    Property eMailAddress As String 
    Property CommentDS As String 
    Property CompanyID As Integer
    Property CompanyNM As String 
    Property SupervisorAccountNM As String 
    Property LastLoginDT As DateTime 
    Property LastLoginLocation As String 
    Property SurveyResponseList As New List(Of SurveyResponseItem) 
    Property ApplicationUserRoleList As New List(Of ApplicationUserRoleItem) 
    Property SurveyResponseCount As Integer
    Property ApplicatonUserRoleCount As Integer
    Property ModifiedDT As DateTime
    Property ModifiedID As Integer
    Property HasMessages As Boolean
    Property MessageCount As Integer
    Property UserRoleID As Integer
    Property UserRoleName As String
    Property Messages As New List(Of SiteMessageItem)
    Property Properties As New List(Of UserAppPropertyItem)
    Property DisplayName As String
    Property UserKey As GUID 
    Property VerifyCode As String 
    Property EmailVerified As Boolean
End Class
