Public Class CompanyItem
    Public Property CompanyID As Integer
    Public Property CompanyNM As String
    Public Property CompanyCD As String
    Public Property CompanyDS As String
    Public Property Title As String
    Public Property SiteTheme As String
    Public Property DefaultSiteTheme As String
    Public Property GalleryFolder As String
    Public Property Address1 As String
    Public Property Address2 As String
    Public Property City As String
    Public Property State As String
    Public Property Country As String
    Public Property PostalCode As String
    Public Property SiteURL As String
    Public Property FromEmail As String
    Public Property SMTP As String
    Public Property Component As String
    Public Property ModifiedID As Integer 
    Public Property ModifiedDT As DateTime
    Public Property Active As Boolean
    Public Property ProjectCount As Integer 
    Public Property UserCount As Integer 
    Public Property SurveyResponseCount As Integer 
    Public Property UserList As New List(Of ApplicationUserItem)
    Public Property ProjectList As New List(Of ApplicationItem)
End Class
