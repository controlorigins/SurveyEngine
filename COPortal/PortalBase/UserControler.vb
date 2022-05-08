Imports System

Public Module UserControler

    Dim mydb As New DataClassesDataContext
    Public Function UserLogin(UserName As String, UserPass As String) As OnlineUserInfo

        Dim myusers = (From i In mydb.SiteUsers Where i.UserLogin = UserName And i.Password = UserPass).ToList
        If myusers.Count = 1 Then
            Dim thisuser = myusers(0)
            thisuser.lastLoginDate = Now
            Try
                mydb.SubmitChanges()
            Catch ex As Exception
                WebProjectMechanics.ApplicationLogging.ErrorLog(String.Format("UserControler.UserLogin UserName={0}, UserID={1}",UserName,thisuser.Id), ex.ToString)
            End Try
            Return GetOnlineUserFromSiteUser(thisuser)
        Else
            Return UserControler.GetGuestUser
        End If

    End Function

    Public Function IsUserInSystem(userlogin As String) As Boolean

        Dim myusercount = (From i In mydb.SiteUsers Where i.UserLogin.ToLower = userlogin.ToLower).Count
        If myusercount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function UpdateUser(userinfo As OnlineUserInfo) As OnlineUserInfo


        Dim myuser = (From i In mydb.SiteUsers Where i.Id = userinfo.UserID).Single
        myuser.FirstName = userinfo.FirstName
        myuser.LastName = userinfo.LastName
        myuser.EmailAddress = userinfo.EmailAddress
        myuser.RoleID = userinfo.UserRoleID
        myuser.AssAccount = userinfo.CoAccountID
        myuser.DisplayName = String.Format("{0} {1}", userinfo.FirstName, userinfo.LastName)
        mydb.SubmitChanges()
        Return userinfo
    End Function

    Public Function CreateNewUser(firstname As String, lastname As String, email As String, password As String) As OnlineUserInfo
        Dim newuser As New SiteUser() With {.AssAccount = "", .DateCreated = Now, .lastLoginDate = Now, .DisplayName = String.Format("{0} {1}", firstname, lastname), .EmailAddress = email, .EmailVerified = False, .FirstName = firstname, .LastName = lastname, .UserLogin = email, .Password = password, .RoleID = 2, .UserKey = Guid.NewGuid, .VerifyCode = Guid.NewGuid.ToString}
        mydb.SiteUsers.InsertOnSubmit(newuser)
        mydb.SubmitChanges()
        Return UserLogin(newuser.UserLogin, newuser.Password)
    End Function


    Public Function Checkpassword(userid As Integer, pass As String) As Boolean
        If (From i In mydb.SiteUsers Where i.Id = userid And i.Password = pass).Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function updatepassword(userid As Integer, newpass As String) As Boolean
        Try
            Dim myuser = (From i In mydb.SiteUsers Where i.Id = userid).Single
            myuser.Password = newpass
            mydb.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function GetRoles() As List(Of SiteRole)
        Return (mydb.SiteRoles).ToList
    End Function

    ' Note Need to move this to message controller only
    Public Function GetMessages(UserID As Integer) As List(Of UserMessage)
        Return CType((From i In mydb.UserMessages Where i.ToUserID = UserID), Global.System.Collections.Generic.List(Of Global.COPortal.UserMessage))
    End Function


    Public Function GetGuestUser() As OnlineUserInfo
        Return New OnlineUserInfo With {.DisplayName = "Guest", .UserID = -1, .UserRoleID = 1}
    End Function


    Public Function GetUserDisplayName(userID As Integer) As String
        Try
            Return (From i In mydb.SiteUsers Where i.Id = userID).Single.DisplayName
        Catch ex As Exception
            Return "User Error"
        End Try

    End Function

    Public Function GetUserByID(userid As Integer) As OnlineUserInfo
        Dim thisuser = (From i In mydb.SiteUsers Where i.Id = userid).Single
        Return GetOnlineUserFromSiteUser(thisuser)
    End Function
    Public Function GetAllUsers() As List(Of OnlineUserInfo)
        Dim mycoll As New List(Of OnlineUserInfo)
        For Each i In mydb.SiteUsers
            mycoll.Add(GetOnlineUserFromSiteUser(i))
        Next
        Return mycoll
    End Function


    Public Function GetPagedUsers(Take As Integer, skip As Integer) As List(Of OnlineUserInfo)
        Dim mycoll As New List(Of OnlineUserInfo)
        For Each i In mydb.SiteUsers.Skip(skip).Take(Take)
            mycoll.Add(GetOnlineUserFromSiteUser(i))
        Next
        Return mycoll
    End Function


    Public Function GetUsersbyAppID(AppID As Integer) As List(Of OnlineUserInfo)
        Return (From i In mydb.UserAppRelations Where i.SiteAppID = AppID Select GetUserByID(i.UserID)).ToList
    End Function


    Function GetOnlineUserFromSiteUser(youruser As SiteUser) As OnlineUserInfo
        Dim websiteuser As New OnlineUserInfo() With {.DisplayName = youruser.DisplayName, .CoAccountID = youruser.AssAccount, .MessageCount = (From i In mydb.UserMessages _
            Where i.ToUserID = youruser.Id).Count, .HasMessages = (From i In mydb.UserMessages _
            Where i.ToUserID = youruser.Id).Count > 0, .UserRoleID = youruser.RoleID, .UserRoleName = youruser.SiteRole.RoleName, .UserID = youruser.Id, .FirstName = youruser.FirstName, .LastName = youruser.LastName, .EmailAddress = youruser.EmailAddress, .LastloginDate = youruser.lastLoginDate, .CreatedDate = youruser.DateCreated}
        Return websiteuser
    End Function

#Region "SessionUserInfo"



    Public Function SetSessionUserAppSetting(UserID As Integer, AppID As Integer, Key As String) As UserAppProperty
        Dim mysetting = New UserAppProperty With {.AppID = AppID, .UserID = UserID, .Key = Key}
        Return SetSessionUserAppSetting(mysetting)
    End Function

    Public Function SetSessionUserAppSetting(UserSetting As UserAppProperty) As UserAppProperty
        Dim mysetting = (From i In mydb.UserAppProperties Where i.UserID = UserSetting.UserID And i.AppID = UserSetting.AppID And i.Key = UserSetting.Key)
        If mysetting.Count > 0 Then
            ' We have existing setting
            mysetting(0).Value = UserSetting.Value
            mydb.SubmitChanges()
            Return CType(mysetting, UserAppProperty)
        Else
            mydb.UserAppProperties.InsertOnSubmit(UserSetting)
            mydb.SubmitChanges()
            Return UserSetting
        End If
    End Function

    Public Function GetSessionUserAppSetting(UserID As Integer, AppID As Integer, Key As String) As UserAppProperty
        Try
            Dim mysetting = (From i In mydb.UserAppProperties Where i.UserID = UserID And i.AppID = AppID And i.Key = Key).Single
            Return mysetting
        Catch ex As Exception
            Return New UserAppProperty
        End Try
    End Function

#End Region





End Module
