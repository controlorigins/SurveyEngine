Imports System
Imports CODataCon.com.controlorigins.ws
Imports CODataCon

Public Module AppControler
    Dim mycon As New DataControler()

#Region " Project / Application Level "
    ''' <summary>
    ''' Get Application by its ID
    ''' </summary>
    ''' <param name="appID"></param>
    ''' <returns>SiteApp</returns>
    ''' <remarks>if no app is found a blank App with id = -1 is returned</remarks>
    Public Function GetAppByID(appID As Integer) As ApplicationItem
        Try
            Return mycon.GetApplicationByApplicationID(appID)
        Catch ex As Exception
            Return New ApplicationItem With {.ApplicationID = -1}
        End Try
    End Function
    Public Function GetAllApps() As List(Of ApplicationItem)
        Return mycon.GetApplicationList()
    End Function
    Public Function PutOnlineSiteApp(App As ApplicationItem) As ApplicationItem
        Return mycon.PutApplicationItem(App)
    End Function
    Public Sub UpdateApplicationUserRole(reqApplicationUserRoleItem As ApplicationUserRoleItem)
        Dim sReturn As String = String.Empty
        mycon.UpdateApplicationUserRole(reqApplicationUserRoleItem, sReturn)
    End Sub
    Public Sub SubscribeMeToApp(ApplicationUserID As Integer, ApplicationID As Integer)
        mycon.SubscribeMeToApp(ApplicationUserID, ApplicationID)
    End Sub
    Public Function GetMyApps(ApplicationUserID As Integer) As List(Of ApplicationItem)
        Return mycon.GetSiteAppListByUserID(ApplicationUserID)
    End Function
    Public Function SetDefaultNavigationItem(ByVal reqSiteApp As ApplicationItem, ByVal NavMenuItemID As Integer) As ApplicationItem
        Return mycon.SetDefaultNavigationItem(reqSiteApp, NavMenuItemID)
    End Function


    Public Function GetProperty(AppID As Integer, PropertyKey As String) As String
        Dim myprop = (From i In GetAppByID(AppID).Properties.ToList Where i.Key = PropertyKey).ToList()
        If myprop.Count = 1 Then
            Return myprop(0).Value
        Else
            Return Nothing
        End If
    End Function

    'Public Function GetProperties(appid As Integer) As Dictionary(Of String, String)
    '    Dim mydic As New Dictionary(Of String, String)
    '    Dim myprops = (From i In GetAppByID(appid).Properties).ToList
    '    For Each i In myprops
    '        mydic.Add(i.Key, i.Value)
    '    Next
    '    Return mydic
    'End Function

    Public Function SetProperty(AppID As Integer, PropertyKey As String, value As String) As Boolean
        Return mycon.SetProperty(AppID, PropertyKey, value)
    End Function
    Public Function DeleteProperty(AppID As Integer, propertyKey As String) As Boolean
        Return mycon.DeleteProperty(AppID, propertyKey)
    End Function
#End Region


#Region "Page Definition Level"
    Public Function GetAppNavigation(AppID As Integer) As List(Of NavigationMenuItem)
        Return GetAppByID(AppID).Navigation.ToList()
    End Function

    Public Function GetNavigationMenuItem(AppInfo As ApplicationItem, AppPageID As Integer) As NavigationMenuItem
        Try
            Return (From i In AppInfo.Navigation.ToList() Where i.Id = AppPageID).SingleOrDefault
        Catch ex As Exception
            Return New NavigationMenuItem
        End Try
    End Function
    Public Function SaveAppPageDefinition(newPageDef As NavigationMenuItem) As NavigationMenuItem
        Return mycon.PutNavigationMenuItem(newPageDef)
    End Function
    Public Function DeleteAppPageDefinition(NavMenuItem As NavigationMenuItem) As Boolean
        Return mycon.DeleteNavigationMenuItem(NavMenuItem)
    End Function

#End Region


#Region "Site App User"

    Public Function RemoveUserFromApp(ApplicationUserID As Integer, ApplicationID As Integer) As Boolean
        Return mycon.RemoveUserFromApp(ApplicationUserID, ApplicationID)
    End Function

    Public Function UnRegisterUserFromApp(ApplicationUserID As String, ApplicationID As Integer) As Boolean
        Return mycon.UnRegisterUserFromApp(ApplicationUserID, ApplicationID)
    End Function

    Function getMyavailableApps(ApplicationUserID As Integer) As List(Of ApplicationItem)
        Return mycon.GetSiteAppListByUserID(ApplicationUserID)
    End Function

    Function GetUserapprelationbyuserandapp(ApplicationUserID As Integer, ApplicationID As Integer) As ApplicationUserRoleItem
        Try
            Dim myUser = mycon.GetApplicationUserByApplicationUserID(ApplicationUserID).ApplicationUserRoleList.ToList
            Return (From i In myUser Where i.ApplicationID = ApplicationID Select i).SingleOrDefault()

        Catch ex As Exception
            Return New ApplicationUserRoleItem With {.ApplicationUserRoleID = -1,
                                         .ApplicationUserID = ApplicationUserID,
                                         .ApplicationID = ApplicationID}
        End Try
    End Function


    Public Function IsUserAppAdmin(IsAdmin As Object) As Boolean
        Return AppUtility.GetDBBoolean(IsAdmin)
    End Function


    Public Function IsUserAppAdmin(ApplicationUserID As Integer, appid As Integer) As Boolean
        Try
            Dim myUser = mycon.GetApplicationUserByApplicationUserID(ApplicationUserID).ApplicationUserRoleList.ToList
            Return (From i In myUser Where i.ApplicationID = appid Select i).SingleOrDefault().IsUserAdmin
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function IsUserInroled(UserInroled As Object) As Boolean
        Return AppUtility.GetDBBoolean(UserInroled)
    End Function

    Sub UpdateSiteAppUser(thisuser As ApplicationUserRoleItem)
        Dim sReturn As String = String.Empty
        mycon.UpdateApplicationUserRole(thisuser, sReturn)
    End Sub


#End Region

    Function GetApplicationTypes() As List(Of LookupItem)
        Return mycon.GetApplicationTypes()
    End Function


End Module

