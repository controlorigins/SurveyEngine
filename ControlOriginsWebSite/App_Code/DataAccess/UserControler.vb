Imports System
Imports CODataCon.com.controlorigins.ws

Public Module UserControler
    Dim mycon As New CODataCon.DataControler()
    Public Function UserLogin(UserName As String, UserPass As String) As ApplicationuserItem
        If IsUserInSystem(UserName) Then
            Dim myUser = mycon.UserLogin(UserName, UserPass)
            If myUser.ApplicationUserID > 0 Then
                Return myUser
            Else
                Return UserControler.GetGuestUser()
            End If
        Else
            Return UserControler.GetGuestUser()
        End If
    End Function
    Public Function GetGuestUser() As ApplicationuserItem
        Return New ApplicationuserItem With {.DisplayName = "Guest", .ApplicationUserID = -1,.UserRoleID = 1}
    End Function

    Public Function IsUserInSystem(userlogin As String) As Boolean
        If 0 < (From i In mycon.GetSiteUserList() Where i.UserLogin.ToLower = userlogin.ToLower).Count Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function UpdateUser(userinfo As ApplicationuserItem) As ApplicationuserItem
        Return mycon.UpdateApplicationUser(userinfo)
    End Function
    Public Function CreateNewUser(firstname As String, lastname As String, email As String, password As String) As ApplicationuserItem
        Return mycon.CreateNewUser(firstname, lastname, email, password)
    End Function
    Public Function Checkpassword(userid As Integer, pass As String) As Boolean
        Return mycon.Checkpassword(userid, pass)
    End Function
    Public Function updatepassword(userid As Integer, newpass As String) As Boolean
        Return mycon.updatepassword(userid, newpass)
    End Function
    Public Function GetRoles() As List(Of SiteRoleItem)
        Return mycon.GetSiteRoleList()
    End Function

    ' Note Need to move this to message controller only
    Public Function GetMessages(UserID As Integer) As List(Of SiteMessageItem)
        Return mycon.GetApplicationUserByApplicationUserID(UserID).Messages.ToList()
    End Function
    Public Function GetUserDisplayName(userID As Integer) As String
        Try
            Return mycon.GetApplicationUserByApplicationUserID(userID).DisplayName
        Catch ex As Exception
            Return "User Error"
        End Try
    End Function
    Public Function GetUserByID(userid As Integer) As ApplicationuserItem
        Return mycon.GetApplicationUserByApplicationUserID(userid)
    End Function
    Public Function GetAllUsers() As List(Of ApplicationuserItem)
        Return mycon.GetSiteUserList()
    End Function
    Public Function GetPagedUsers(Take As Integer, skip As Integer) As List(Of ApplicationuserItem)
        Return (From i In mycon.GetSiteUserList.Skip(skip).Take(Take)).ToList()

    End Function
    Public Function GetUsersbyAppID(AppID As Integer) As List(Of ApplicationUserRoleItem)
        Return mycon.GetApplicationUserByApplicationID(AppID)
    End Function

End Module
