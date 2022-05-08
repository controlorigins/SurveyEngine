Imports System
Imports WebProjectMechanics
Public Module AppControler

    Dim mydb As New DataClassesDataContext




#Region " Application Level "

    ''' <summary>
    ''' Get Application by its ID
    ''' </summary>
    ''' <param name="appID"></param>
    ''' <returns>SiteApp</returns>
    ''' <remarks>if no app is found a blank App with id = -1 is returned</remarks>
    Public Function GetAppByID(appID As Integer) As SiteApp
        Try
            Return (From i In mydb.SiteApps Where i.Id = appID).Single
        Catch ex As Exception
            Return New SiteApp With {.Id = -1}
        End Try
    End Function
    Public Function GetAllApps() As List(Of SiteApp)
        Return (From i In mydb.SiteApps Order By i.AppName).ToList
    End Function
    Public Function SaveNewApp(App As SiteApp) As SiteApp
        mydb.SiteApps.InsertOnSubmit(App)
        mydb.SubmitChanges()
        Return App
    End Function

    Public Sub MakeAppavailable(userID As Integer, appid As Integer)
        Dim myapprelation As New UserAppRelation() With {.IsDemo = True, .isMonthlyPrice = True, .Price = 0, .SiteAppID = appid, .UserID = userID, .StartupDate = Now, .UserInroled = False}
        mydb.UserAppRelations.InsertOnSubmit(myapprelation)
        mydb.SubmitChanges()
    End Sub

    Public Sub SubscribeMeToApp(userid As Integer, appid As Integer)
        Dim myapprelation = (From i In mydb.UserAppRelations Where i.UserID = userid And i.SiteAppID = appid).Single
        With myapprelation
            .IsDemo = True
            .isMonthlyPrice = True
            .Price = 0
            .SiteAppID = appid
            .UserID = userid
            .StartupDate = Now
            .UserInroled = True
        End With
        mydb.SubmitChanges()
    End Sub


    Public Function GetMyApps(UserId As Integer) As List(Of SiteApp)
        Dim mycoll = New List(Of SiteApp)
        Dim myapps = (From i In mydb.UserAppRelations Where i.UserID = UserId And i.UserInroled = True).ToList
        For Each i In myapps
            mycoll.Add((From ii In mydb.SiteApps Where ii.Id = i.SiteAppID).Single)
        Next
        Return mycoll
    End Function

    Public Function GetProperty(AppID As Integer, PropertyKey As String) As String
        Dim myprop = (From i In mydb.AppProperties Where i.SiteAppID = AppID And i.Key = PropertyKey).ToList
        If myprop.Count = 1 Then
            Return myprop(0).Value
        Else
            Return Nothing
        End If
    End Function

    Public Function GetProperties(appid As Integer) As Dictionary(Of String, String)
        Dim mydic As New Dictionary(Of String, String)
        Dim myprops = (From i In mydb.AppProperties Where i.SiteAppID = appid).ToList
        For Each i In myprops
            mydic.Add(i.Key, i.Value)
        Next
        Return mydic
    End Function

    Public Function SetProperty(AppID As Integer, PropertyKey As String, value As String) As Boolean
        Try
            Dim myprop = (From i In mydb.AppProperties Where i.SiteAppID = AppID And i.Key = PropertyKey).ToList()
            If myprop.Count = 1 Then
                myprop(0).Value = (value)
            Else
                Dim newprop As New AppProperty() With {.Key = PropertyKey, .Value = value, .SiteAppID = AppID}
                mydb.AppProperties.InsertOnSubmit(newprop)
            End If
            mydb.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeleteProperty(AppID As Integer, propertyKey As String) As Boolean
        Try
            Dim myprop = (From i In mydb.AppProperties Where i.SiteAppID = AppID And i.Key = propertyKey).ToList
            If myprop.Count = 1 Then
                mydb.AppProperties.DeleteOnSubmit(myprop(0))
            End If
            mydb.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region


#Region "Page Definition Level"
    Public Function GetAppPageDefinitions(AppID As Integer) As List(Of SiteAppMenu)
        Return (From i In mydb.SiteAppMenus Where i.SiteAppID = AppID Order By i.MenuOrder).ToList
    End Function
    Public Function GetAppPageDefinition(AppPageID As Integer) As SiteAppMenu
        Try
            Return (From i In mydb.SiteAppMenus Where i.Id = AppPageID).Single
        Catch ex As Exception
            WebProjectMechanics.ApplicationLogging.ErrorLog(String.Format("AppControler.GetAppPageDefinition(""{0}"")", AppPageID), ex.Message)
            Return New SiteAppMenu
        End Try
    End Function
    Public Function SaveAppPageDefinition(newPageDef As SiteAppMenu) As SiteAppMenu
        mydb.SiteAppMenus.InsertOnSubmit(newPageDef)
        mydb.SubmitChanges()
        Return newPageDef
    End Function
    Public Function DeleteAppPageDefinition(AppPageDefID As Integer) As Boolean
        Try
            Dim mypagedef = (From i In mydb.SiteAppMenus Where i.Id = AppPageDefID).Single
            mydb.SiteAppMenus.DeleteOnSubmit(mypagedef)
            mydb.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Sub Updatedata()
        mydb.SubmitChanges()
    End Sub
    Public Sub UpdateApp()
        mydb.SubmitChanges()
    End Sub
    Public Sub UpdateAppPageDefinition()
        mydb.SubmitChanges()
    End Sub
#End Region

    Public Function RemoveUserFromApp(UserID As String, appID As Integer) As Boolean
        Try
            Dim myapprelation = (From i In mydb.UserAppRelations Where i.UserID = wpm_GetDBInteger(UserID, 0) And i.SiteAppID = appID).Single
            mydb.UserAppRelations.DeleteOnSubmit(myapprelation)
            mydb.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function UnRegisterUserFromApp(UserID As String, appid As Integer) As Boolean
        Try
            Dim myapprelation = (From i In mydb.UserAppRelations Where i.UserID = wpm_GetDBInteger(UserID, 0) And i.SiteAppID = appid).Single
            myapprelation.UserInroled = False
            mydb.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function getMyavailableApps(UserID As Integer) As List(Of SiteApp)
        Dim mycoll = New List(Of SiteApp)
        Dim myapps = (From i In mydb.UserAppRelations Where i.UserID = UserID And i.UserInroled = False)
        For Each i In myapps
            mycoll.Add((From ii In mydb.SiteApps Where ii.Id = i.SiteAppID).Single)
        Next
        Return mycoll
    End Function


    Function GetUserapprelationbyuserandapp(userid As Integer, appid As Integer) As UserAppRelation
        Try
            Return (From i In mydb.UserAppRelations Where i.UserID = userid And i.SiteAppID = appid).Single
        Catch ex As Exception
            Return Nothing
        End Try

    End Function


    Public Function IsUserAppAdmin(userid As Integer, appid As Integer) As Boolean
        Dim myrelation = (From i In mydb.UserAppRelations Where i.UserID = userid And i.SiteAppID = appid).ToList
        If myrelation.Count > 0 Then
            Return myrelation(0).IsUserAdmin
        Else
            Return False
        End If
    End Function

    Public Function IsUserInroled(userid As Integer, appid As Integer) As Boolean
        Dim myrelation = (From i In mydb.UserAppRelations Where i.UserID = userid And i.SiteAppID = appid).ToList

        If myrelation.Count > 0 Then
            Return myrelation(0).UserInroled
        Else
            Return False
        End If
        Return True
    End Function



End Module

