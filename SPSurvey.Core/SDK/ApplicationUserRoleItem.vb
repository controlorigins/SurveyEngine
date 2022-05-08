Public Class ApplicationUserRoleItem

    ' Role Fields
    Public Property ApplicationUserRoleID As Integer
    Public Property CanCreate As Boolean
    Public Property CanDelete As Boolean
    Public Property CanManage As Boolean
    Public Property CanRead As Boolean
    Public Property CanReview As Boolean
    Public Property CanUpdate As Boolean
    Public Property ReviewLevel As Integer
    Public Property RoleCD As String
    Public Property RoleDS As String
    Public Property RoleID As Integer
    Public Property RoleNM As String

    ' User Fields
    Public Property AccountNM As String
    Public Property ApplicationUserID As Integer
    Public Property eMailAddress As String
    Public Property FirstNM As String
    Public Property LastLoginDT As Date
    Public Property LastLoginLocation As String
    Public Property LastNM As String

    ' Application Fields
    Public Property ApplicationCD As String
    Public Property ApplicationDS As String
    Public Property ApplicationID As Integer
    Public Property ApplicationNM As String
    Public Property ApplicationShortNM As String
    Public Property ApplicationTypeID As Integer
    Public Property ApplicationTypeNM As String
    Public Property CommentDS As String
    Public Property MenuOrder As Integer
    Public Property ModifiedID As Integer
    ' Company Fields
    Public Property CompanyID As Integer
    Public Property CompanyNM As String
    Public Property CompanyCD As String 

    Property IsDemo As Boolean
    Property StartupDate As Date
    Property isMonthlyPrice As Boolean
    Property Price As Decimal
    Property UserInroled As Boolean
    Property IsUserAdmin As Boolean




End Class