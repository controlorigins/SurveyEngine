Imports System.Web.Services
Imports SPSurvey.Core
Imports ControlOrigins.COUtility
Imports System.Collections.Generic
Imports LINQHelper.System.Linq.Dynamic

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://ws.controlorigins.com/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Service
    Inherits WebService
    Implements ISurveyResponse
    Private mySurveyResponse As SurveyResponseItem = New SurveyResponseItem
    Public ReadOnly Property str_ConnectionString As String
        Get
            Return ConfigurationManager.ConnectionStrings("SPSurveyConnectionString").ConnectionString
        End Get
    End Property

#Region "SiteUsers"
    <WebMethod()> _
    Public Function GetSiteUserList(ByVal myGUID As Guid) As List(Of ApplicationUserItem)
        Dim mySiteUserList As New List(Of ApplicationUserItem)
        If ValidateGUID(myGUID) Then
            ApplicationLogging.AuditLog("Service.GetSiteUserList", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)

                    mySiteUserList.AddRange(myCON.GetApplicationUserList())
                End Using
            Catch ex As Exception
                ApplicationLogging.ErrorLog(ex.ToString, "Service.GetSiteUserList")
            End Try
        End If
        Return mySiteUserList
    End Function

    <WebMethod()> _
    Public Function GetSiteUser(ByVal myGUID As Guid, ByVal login As String, ByVal pass As String) As ApplicationUserItem
        Dim myUser As New ApplicationUserItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetSiteUser", login)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    myUser = myCON.GetSiteUser(login, pass)
                End Using
                Return myUser
            Catch ex As Exception
                Return New ApplicationUserItem
                AppLog.ErrorLog(ex.ToString, "Service.GetSiteUser")
            End Try
        Else
            Return myUser
        End If
        Return myUser
    End Function

    <WebMethod()> _
    Public Function PutOnlineUserInfo(ByVal myGUID As Guid, userinfo As ApplicationUserItem) As ApplicationUserItem
        If ValidateGUID(myGUID) Then
            Dim sReturn As String = String.Empty
            AppLog.AuditLog("Service.PutOnlineUserInfo", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    Return myCON.UpdateApplicationUser(userinfo, sReturn)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.PutOnlineUserInfo")
                Return New ApplicationUserItem
            End Try
        End If
        Return New ApplicationUserItem
    End Function

    <WebMethod()> _
    Public Function CreateNewUser(ByVal myGUID As Guid, firstname As String, lastname As String, email As String, password As String) As ApplicationUserItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.CreateNewUser", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    Return myCON.CreateNewUser(firstname, lastname, email, password)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.CreateNewUser")
                Return New ApplicationUserItem
            End Try
        End If
        Return New ApplicationUserItem
    End Function

    <WebMethod()> _
    Public Function Checkpassword(ByVal myGUID As Guid, userid As Integer, pass As String) As Boolean
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.Checkpassword", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    Return myCON.Checkpassword(userid, pass)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.Checkpassword")
                Return False
            End Try
        End If
        Return False
    End Function


    <WebMethod()> _
    Public Function updatepassword(ByVal myGUID As Guid, userid As Integer, newpass As String) As Boolean
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.updatepassword", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    Return myCON.updatepassword(userid, newpass)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.updatepassword")
                Return False
            End Try
        End If
        Return False
    End Function


    <WebMethod()> _
    Public Function RemoveUserFromApp(ByVal myGUID As Guid, ByVal UserID As Integer, ByVal appID As Integer) As Boolean
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.RemoveUserFromApp", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    Return myCON.DeleteApplicationUserRole(UserID, appID)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.RemoveUserFromApp")
                Return False
            End Try
        End If
        Return False
    End Function

    <WebMethod()> _
    Public Function UnRegisterUserFromApp(ByVal myGUID As Guid, UserID As Integer, appid As Integer) As Boolean
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.UnRegisterUserFromApp", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    Return myCON.UnRegisterUserFromApp(UserID, appid)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.UnRegisterUserFromApp")
                Return False
            End Try
        End If
        Return False
    End Function

#End Region

#Region "Site Message"
    <WebMethod()> _
    Public Function GetSiteMessageList(ByVal myGUID As Guid) As List(Of SiteMessageItem)
        Dim mySiteMessageList As New List(Of SiteMessageItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetSiteMessageList", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    mySiteMessageList.AddRange(myCON.GetSiteMessages())
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSiteMessageList")
            End Try
        End If
        Return mySiteMessageList
    End Function
    <WebMethod()> _
    Public Function GetSiteMessageByMessageID(ByVal myGUID As Guid, ByVal myMessageID As Integer) As SiteMessageItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetSiteMessageByMessageID", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    Return myCON.GetSiteMessageById(myMessageID)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSiteMessageByMessageID")
            End Try
        End If
        Return New SiteMessageItem With {.Id = -1}
    End Function
    <WebMethod()> _
    Public Function PutSiteMessage(ByVal myGUID As Guid, ByVal myMessage As SiteMessageItem) As SiteMessageItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.PutSiteMessage", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    Return myCON.PutSiteMessage(myMessage)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.PutSiteMessage")
            End Try
        End If
        Return New SiteMessageItem With {.Id = -1}
    End Function
    <WebMethod()> _
    Public Function GetUserSentMessages(ByVal myGUID As Guid, ByVal UserId As Integer) As List(Of SiteMessageItem)
        Dim mySiteMessageList As New List(Of SiteMessageItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetUserSentMessages", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    mySiteMessageList.AddRange(myCON.GetUserSentMessages(UserId))
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetUserSentMessages")
            End Try
        End If
        Return mySiteMessageList
    End Function
    <WebMethod()> _
    Public Function UserMessageOpened(ByVal myGUID As Guid, ByVal myMessage As SiteMessageItem) As SiteMessageItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.UserMessageOpened", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    Return myCON.UserMessageOpened(myMessage)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.UserMessageOpened")
            End Try
        End If
        Return New SiteMessageItem With {.Id = -1}
    End Function
    <WebMethod()> _
    Public Function DeleteMessage(ByVal myGUID As Guid, ByVal myMessage As SiteMessageItem) As Boolean
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.DeleteMessage", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    Return myCON.DeleteMessage(myMessage)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.DeleteMessage")
            End Try
        End If
        Return False
    End Function
    <WebMethod()> _
    Public Function GetRelatedUsers(ByVal myGUID As Guid, ByVal UserId As Integer) As List(Of ApplicationUserItem)
        Dim mySiteUserList As New List(Of ApplicationUserItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetRelatedUsers", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    mySiteUserList.AddRange(myCON.GetRelatedUsers(UserId))
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetRelatedUsers")
            End Try
        End If
        Return mySiteUserList
    End Function
#End Region

#Region "Company Admin"
    <WebMethod()> _
    Public Function GetCompanyList(ByVal myGUID As Guid) As List(Of CompanyItem)
        Dim myList As New List(Of CompanyItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetCompanyList", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myList = mycon.GetCompanyList()
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetCompanyList")
                myList = New List(Of CompanyItem)
            End Try
        End If
        Return myList
    End Function
    <WebMethod()> _
    Public Function GetCompany(ByVal myCompanyID As Integer, ByVal myGUID As Guid) As CompanyItem
        Dim myCompany As New CompanyItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetCompany", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myCompany = mycon.GetCompanyByCompanyID(myCompanyID)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetCompany")
                myCompany = New CompanyItem
            End Try
        End If
        Return myCompany
    End Function
    <WebMethod()> _
    Public Function PutCompany(ByVal myCompany As CompanyItem, ByVal myGUID As Guid) As CompanyItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.PutCompany", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myCompany = mycon.PutCompany(myCompany)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.PutCompany")
                myCompany = New CompanyItem
            End Try
        End If
        Return myCompany
    End Function
    <WebMethod()> _
    Public Function DeleteCompany(ByVal myCompany As CompanyItem, ByVal myGUID As Guid) As Boolean
        Dim bReturn As Boolean = False
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.DeleteCompany", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    bReturn = mycon.DeleteCompany(myCompany)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.DeleteCompany")
                bReturn = False
            End Try
        End If
        Return bReturn
    End Function

#End Region

#Region "Application Item Methods"
    <WebMethod()> _
    Public Function GetSiteAppListByUserID(ByVal myGUID As Guid, ByVal UserID As Integer) As List(Of ApplicationItem)
        Dim mySiteAppList As New List(Of ApplicationItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetSiteAppListByUserID", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    mySiteAppList.AddRange(myCON.GetApplicationListByApplicationUserID(UserID))
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSiteAppListByUserID")
            End Try
        End If
        Return mySiteAppList
    End Function
    <WebMethod()> _
    Public Function SubscribeMeToApp(ByVal myGUID As Guid, userid As Integer, appid As Integer) As Boolean
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.SubscribeMeToApp", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    Return myCON.SubscribeMeToApp(userid, appid)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.SubscribeMeToApp")
                Return False
            End Try
        End If
        Return False
    End Function
    <WebMethod()> _
    Public Function SetDefaultNavigationItem(ByVal myGUID As Guid, ByVal reqSiteApp As ApplicationItem, ByVal NavMenuItemID As Integer) As ApplicationItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.SetDefaultNavigationItem", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    Return myCON.SetDefaultNavigationItem(reqSiteApp, NavMenuItemID)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.SetDefaultNavigationItem")
                Return New ApplicationItem
            End Try
        End If
        Return New ApplicationItem
    End Function
    <WebMethod()> _
    Public Function CloneSiteApp(ByVal myGuid As Guid, ByVal curAppID As Integer, ByVal newAppName As String) As ApplicationItem
        Dim mySiteApp As New ApplicationItem
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.CloneSiteApp", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    mySiteApp = myCON.CloneSiteApp(curAppID, newAppName)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.CloneSiteApp")
            End Try
        End If
        Return mySiteApp
    End Function
    <WebMethod()> _
    Public Function GetApplicationList(ByVal myGUID As Guid) As List(Of ApplicationItem)
        Dim myApplicationList As New List(Of ApplicationItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetApplications", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myApplicationList.AddRange(mycon.GetApplicationList())
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetApplications")
            End Try
        Else
            Return myApplicationList
        End If
        Return myApplicationList
    End Function
    <WebMethod()> _
    Public Function GetSurveyApplicationList(ByVal myGUID As Guid) As List(Of ApplicationItem)
        Dim myApplicationList As New List(Of ApplicationItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetApplications", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myApplicationList.AddRange((From i In mycon.GetApplicationList() Where i.ApplicationID > 20).ToArray)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetApplications")
            End Try
        Else
            Return myApplicationList
        End If
        Return myApplicationList
    End Function
    <WebMethod()> _
    Public Function GetApplicationByApplicationID(ByVal reqApplicaitonID As Integer, ByVal myGUID As Guid) As ApplicationItem
        Dim myApplication As New ApplicationItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetApplicationByApplicationID", reqApplicaitonID.ToString)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    myApplication = myCON.GetApplicationByApplicationID(reqApplicaitonID)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetApplicationByApplicationID")
            End Try
        End If
        Return myApplication
    End Function
    <WebMethod()> _
    Public Function PutApplicationItem(ByVal myApplication As ApplicationItem, ByVal myGUID As Guid) As ApplicationItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.PutApplicationItem", myApplication.ApplicationID.ToString)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myApplication = mycon.PutApplicationItem(myApplication)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.PutApplicationItem")
            End Try
            Return myApplication
        Else
            Return New ApplicationItem
        End If
    End Function
    <WebMethod()> _
    Public Function DeleteApplication(ByVal myApplication As ApplicationItem, ByVal myGUID As Guid) As Boolean
        Dim bReturn As Boolean = False
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.DeleteApplication", myApplication.ApplicationID.ToString)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    bReturn = mycon.DeleteApplication(myApplication)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.DeleteApplication")
            End Try
        End If
        Return bReturn
    End Function
    <WebMethod()> _
    Public Function PutApplicationSurveyItem(ByVal myApplicationSurvey As ApplicationSurveyItem, ByVal myGUID As Guid) As ApplicationSurveyItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.PutApplicationSurveyItem", myApplicationSurvey.ApplicationSurveyID.ToString)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myApplicationSurvey = mycon.UpateApplicationSurvey(myApplicationSurvey)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.PutApplicationSurveyItem")
            End Try
            Return myApplicationSurvey
        Else
            Return New ApplicationSurveyItem
        End If
    End Function
    <WebMethod()> _
    Public Function DeleteApplicationSurveyItem(ByVal myApplicationSurvey As ApplicationSurveyItem, ByVal myGUID As Guid) As Boolean
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.DeleteApplicationSurveyItem", myApplicationSurvey.ApplicationSurveyID.ToString)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    Return mycon.DeleteApplicationSurvey(myApplicationSurvey)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.DeleteApplicationSurveyItem")
                Return False
            End Try
            Return False
        Else
            Return False
        End If
    End Function

#End Region

#Region "Site App Menu"

    <WebMethod()> _
    Public Function GetNavigationMenuList(ByVal myGUID As Guid) As List(Of NavigationMenuItem)
        Dim myList As New List(Of NavigationMenuItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetSiteAppList", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    myList.AddRange(myCON.GetNavigationMenuList())
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSiteAppList")
            End Try
        End If
        Return myList
    End Function
    <WebMethod()> _
    Public Function PutNavigationMenuItem(ByVal myGUID As Guid, ByVal thisMenuItem As NavigationMenuItem) As NavigationMenuItem
        Dim myList As New List(Of NavigationMenuItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.PutNavigationMenuItem", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    myList.AddRange(myCON.PutNavigationMenuItem(thisMenuItem))
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.PutNavigationMenuItem")
            End Try
        End If
        Return thisMenuItem
    End Function
    <WebMethod()> _
    Public Function DeleteNavigationMenuItem(ByVal myGUID As Guid, ByVal thisMenuItem As NavigationMenuItem) As Boolean
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.DeleteNavigationMenuItem", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    Return myCON.DeleteNavigationMenuItem(thisMenuItem)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.DeleteNavigationMenuItem")
            End Try
        End If
        Return False
    End Function

#End Region

#Region "Site User Roles"
    <WebMethod()> _
    Public Function GetSiteRoleList(ByVal myGUID As Guid) As List(Of SiteRoleItem)
        Dim myList As New List(Of SiteRoleItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetSiteRoleList", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    myList.AddRange(myCON.GetSiteRoles())
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSiteRoleList")
            End Try
        End If
        Return myList
    End Function
#End Region

#Region "Site App Properties"
    <WebMethod()> _
    Public Function GetProperty(ByVal AppID As Integer, ByVal PropertyKey As String, ByVal myGUID As Guid) As PropertyItem
        Dim myProperty As New PropertyItem With {.Id = -1, .SiteAppID = AppID, .Key = PropertyKey, .Value = String.Empty}
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetProperty", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myProperty = mycon.GetProperty(AppID, PropertyKey)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetProperty")
            End Try
        End If
        Return myProperty
    End Function
    <WebMethod()> _
    Public Function PutProperty(ByVal myProperty As PropertyItem, ByVal myGUID As Guid) As PropertyItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.PutProperty", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myProperty = mycon.PutProperty(myProperty)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.PutProperty")
                myProperty = New PropertyItem
            End Try
        End If
        Return myProperty
    End Function
    <WebMethod()> _
    Public Function SetProperty(ByVal myGUID As Guid, AppID As Integer, PropertyKey As String, value As String) As Boolean
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.SetProperty", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    Return myCON.SetProperty(AppID, PropertyKey, value)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.SetProperty")
                Return False
            End Try
        End If
        Return False
    End Function
    <WebMethod()> _
    Public Function DeleteProperty(ByVal myGUID As Guid, AppID As Integer, propertyKey As String) As Boolean
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.DeleteProperty", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    Return myCON.DeleteProperty(AppID, propertyKey)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.DeleteProperty")
                Return False
            End Try
        End If
        Return False
    End Function
#End Region

#Region "Lookup Lists - Common Tables"
    <WebMethod()> _
    Public Function GetLookupList(ByVal reqLookupType As LookupType, ByVal myGuid As Guid) As List(Of LookupItem)
        Dim myLookupList As New List(Of LookupItem)

        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.GetLookupList", reqLookupType)

            Try
                If reqLookupType = LookupType.LookupTypes Then
                    myLookupList = (From x In [Enum].GetNames(GetType(LookupType)).ToArray Select New LookupItem With {.Name = x, .Value = Array.IndexOf([Enum].GetNames(GetType(LookupType)).ToArray, x)}).ToList()
                Else
                    Using myController As New DataController(str_ConnectionString)
                        Select Case reqLookupType
                            Case LookupType.SurveyResponseStatusList
                                myLookupList = myController.GetSurveyResponseStatusList()
                            Case LookupType.ApplicationTypeList
                                myLookupList = myController.GetApplicationTypes()
                            Case LookupType.QuestionTypeList
                                myLookupList = myController.GetQuestionTypes()
                            Case LookupType.SurveyList
                                myLookupList = myController.GetSurveyLookupList()
                            Case LookupType.RoleList
                                myLookupList = myController.GetRolesLookupList()
                            Case LookupType.SurveyTypeList
                                myLookupList = myController.GetSurveyTypes()
                            Case LookupType.UnitOfMeasureList
                                myLookupList = myController.GetUnitOfMeasures()
                            Case LookupType.ReviewRoleLevelList
                                myLookupList.Add(New LookupItem With {.Value = 1, .Name = "User"})
                                myLookupList.Add(New LookupItem With {.Value = 2, .Name = "Manager"})
                                myLookupList.Add(New LookupItem With {.Value = 3, .Name = "Administrator"})
                            Case Else
                                myLookupList = New List(Of LookupItem)
                        End Select
                    End Using
                End If
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetLookupList")
            End Try
        End If
        Return myLookupList
    End Function

#End Region

#Region "Users"
    <WebMethod()> _
    Public Function GetApplicationUserList(ByVal myGUID As Guid) As List(Of ApplicationUserItem)
        Dim myApplicationUserList As New List(Of ApplicationUserItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetApplicationUserList", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    myApplicationUserList.AddRange(myCON.GetApplicationUserList())
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetApplicationUserList")
            End Try
        End If
        Return myApplicationUserList
    End Function
    <WebMethod()> _
    Public Function GetApplicationUserByApplicationUserID(ByVal myGUID As Guid, ByVal reqApplicationUserID As Integer) As ApplicationUserItem
        Dim myApplicationUser As New ApplicationUserItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetApplicationUserByApplicationUserID", String.Format("ApplicationUserID:{0}", reqApplicationUserID))
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myApplicationUser = mycon.GetApplicationUserByApplicationUserID(reqApplicationUserID)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetApplicationUserByApplicationUserID")
            End Try
        End If
        Return myApplicationUser
    End Function
    <WebMethod()> _
    Public Function PutApplicationUser(ByVal myGUID As Guid, ByVal reqApplicationUser As ApplicationUserItem) As ApplicationUserItem
        Dim sReturn As String = String.Empty

        If ValidateGUID(myGUID) Then
            Try
                Using mycon As New DataController(str_ConnectionString)
                    AppLog.AuditLog("Service.PutApplicationUser", String.Format("ApplicationUserID:{0}, AccountNM:{1}", reqApplicationUser.ApplicationUserID, reqApplicationUser.AccountNM))
                    Dim myApplicationUser = mycon.GetApplicationUserByApplicationUserID(reqApplicationUser.ApplicationUserID)
                    With myApplicationUser
                        .FirstNM = reqApplicationUser.FirstNM
                        .LastNM = reqApplicationUser.LastNM
                        .eMailAddress = reqApplicationUser.eMailAddress
                        .AccountNM = reqApplicationUser.AccountNM
                        .CommentDS = reqApplicationUser.CommentDS
                        .DisplayName = reqApplicationUser.DisplayName
                        .EmailVerified = reqApplicationUser.EmailVerified
                        .ModifiedDT = Now()
                        .ModifiedID = reqApplicationUser.ModifiedID
                        .SupervisorAccountNM = reqApplicationUser.SupervisorAccountNM
                        .UserRoleID = reqApplicationUser.UserRoleID
                        If reqApplicationUser.CompanyID > 0 Then
                            .CompanyID = reqApplicationUser.CompanyID
                        End If
                        If String.IsNullOrEmpty(.SupervisorAccountNM) Then
                            .SupervisorAccountNM = "admin@controlorigins.com"
                        End If
                        If .ModifiedID < 1 Then
                            .ModifiedID = 9999
                        End If
                        If String.IsNullOrWhiteSpace(.DisplayName) Then
                            .DisplayName = String.Format("{0} {1}", .FirstNM, .LastNM)
                        End If
                        If String.IsNullOrWhiteSpace(.UserLogin) Then
                            .UserLogin = .eMailAddress
                        End If
                        If String.IsNullOrWhiteSpace(.AccountNM) Then
                            .AccountNM = .eMailAddress
                        End If
                        If .UserRoleID < 1 Then
                            .UserRoleID = 2
                        End If
                    End With
                    Return mycon.UpdateApplicationUser(myApplicationUser, sReturn)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog("Service.PutApplicationUser", sReturn & "-" & ex.ToString)
                Return New ApplicationUserItem With {.ApplicationUserID = -1}
            End Try
        Else
            Return New ApplicationUserItem With {.ApplicationUserID = -1}
        End If
    End Function
    <WebMethod()> _
    Public Function DeleteApplicationUser(ByVal myGUID As Guid, ByVal reqApplicationuser As ApplicationUserItem) As Boolean
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.DeleteApplicationUser", String.Format("ApplicationUserID:{0}, AccountNM:{1}", reqApplicationuser.ApplicationUserID, reqApplicationuser.AccountNM))
            Try
                Using mycon As New DataController(str_ConnectionString)
                    Return mycon.DeleteApplicationUser(reqApplicationuser)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog("Service.DeleteApplicationUser", ex.ToString)
                Return False
            End Try
        Else
            Return False
        End If
    End Function
#End Region

#Region "User Application Assignment"
    <WebMethod()> _
    Public Function GetRoles(ByVal myGUID As Guid) As List(Of RoleItem)
        Dim myRoleList As New List(Of RoleItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetRoles", DataSource)
            Try
                Using myCON As New DataController(str_ConnectionString)
                    myRoleList.AddRange(myCON.GetRoles())
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetRoles")
            End Try
        End If
        Return myRoleList
    End Function
    <WebMethod()> _
    Public Function GetApplicationUserRoleListByApplicationID(ByVal reqApplicationID As Integer, ByVal myGUID As Guid) As List(Of ApplicationUserRoleItem)
        Dim myApplicationUserRoleList As New List(Of ApplicationUserRoleItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetApplicationUserRoleListByApplicationID", String.Format("ApplicationID: {0}", reqApplicationID))
            Try
                Using mycon As New DataController(str_ConnectionString)
                    If reqApplicationID <= 0 Then
                        ' Do nothing, must have ApplicationID 
                    Else
                        myApplicationUserRoleList.AddRange(mycon.GetApplicationUserRolesByApplicationID(reqApplicationID))
                    End If
                End Using
            Catch ex As Exception
                myApplicationUserRoleList(0).ApplicationUserID = -1
                AppLog.ErrorLog(ex.ToString, String.Format("ApplicationUserRoleUI.GetApplicationUserRoleListByApplicationID By ApplicationID={0}", reqApplicationID))
            End Try
        End If
        Return myApplicationUserRoleList
    End Function
    <WebMethod()> _
    Public Function DeleteApplicationUserRole(ByVal myApplicationUserRole As ApplicationUserRoleItem, ByVal myGuid As Guid) As Integer
        Dim iReturn As Integer = -1
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.DeleteApplicationUserRole", String.Format("ApplicationUserRoleID: {0}", myApplicationUserRole.ApplicationUserRoleID))
            Try
                Using mycon As New DataController(str_ConnectionString)
                    For Each myRole In mycon.GetApplicationUserRolesByApplicationID(myApplicationUserRole.ApplicationID)
                        If myRole.ApplicationUserID = myApplicationUserRole.ApplicationUserID Then
                            myApplicationUserRole.ApplicationUserRoleID = myRole.ApplicationUserRoleID
                            Exit For
                        End If
                    Next
                    iReturn = mycon.ApplicationUserRole_DeleteRow(myApplicationUserRole.ApplicationUserRoleID)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.DeleteApplicationUserRole")
                iReturn = -1
            End Try
        End If
        Return iReturn
    End Function
    <WebMethod()> _
    Public Function PutApplicationUserRole(ByVal myApplicationUserRole As ApplicationUserRoleItem, ByVal myGuid As Guid) As ApplicationUserRoleItem
        Dim sReturn As String = String.Empty
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.PutApplicationUserRole", String.Format("ApplicationUserID: {0}", myApplicationUserRole.ApplicationUserID))
            Try
                If myApplicationUserRole.ModifiedID < 1 Then
                    myApplicationUserRole.ModifiedID = 9999
                End If

                Using mycon As New DataController(str_ConnectionString)
                    For Each myRole In mycon.GetApplicationUserRolesByApplicationID(myApplicationUserRole.ApplicationID)
                        If myRole.ApplicationUserID = myApplicationUserRole.ApplicationUserID Then
                            myApplicationUserRole.ApplicationUserRoleID = myRole.ApplicationUserRoleID
                            Exit For
                        End If
                    Next
                    myApplicationUserRole = mycon.UpdateApplicationUserRole(myApplicationUserRole, sReturn)
                    If myApplicationUserRole.ApplicationUserRoleID < 1 Or Not String.IsNullOrEmpty(sReturn) Then
                        AppLog.ErrorLog(String.Format("Insert Failed, AppliationUserRoleID:{1} Message:{0}", sReturn, myApplicationUserRole.ApplicationUserRoleID), "Service.PutApplicationUserRole")
                    End If
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.PutApplicationUserRole")
            End Try
        Else
            myApplicationUserRole = New ApplicationUserRoleItem
        End If
        Return myApplicationUserRole
    End Function
#End Region

#Region "Application Chart"
    <WebMethod()> _
    Public Function GetApplicationChartList(ByVal myGUID As Guid) As List(Of ApplicationChartItem)
        Dim myList As New List(Of ApplicationChartItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetApplicationChartList", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myList = mycon.GetApplicationChartList()
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetApplicationChartList")
                myList = New List(Of ApplicationChartItem)
            End Try
        End If
        Return myList
    End Function
    <WebMethod()> _
    Public Function GetApplicationChart(ByVal myApplicationChartID As Integer, ByVal myGUID As Guid) As ApplicationChartItem
        Dim myApplicationChart As New ApplicationChartItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetApplicationChart", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myApplicationChart = mycon.GetApplicaitonChartByApplicationChartID(myApplicationChartID)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetApplicationChart")
                myApplicationChart = New ApplicationChartItem
            End Try
        End If
        Return myApplicationChart
    End Function
    <WebMethod()> _
    Public Function PutApplicationChart(ByVal myApplicationChart As ApplicationChartItem, ByVal myGUID As Guid) As ApplicationChartItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetApplicationChartList", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myApplicationChart = mycon.PutApplicationChart(myApplicationChart)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetApplicationChartList")
                myApplicationChart = New ApplicationChartItem
            End Try
        End If
        Return myApplicationChart
    End Function
    <WebMethod()> _
    Public Function DeleteApplicationChart(ByVal myApplicationChart As ApplicationChartItem, ByVal myGUID As Guid) As Boolean
        Dim bReturn As Boolean = False
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.DeleteApplicationChart", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    bReturn = mycon.DeleteApplicationChart(myApplicationChart)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.DeleteApplicationChart")
                bReturn = False
            End Try
        End If
        Return bReturn
    End Function

#End Region

#Region "Application Type Admin"
    <WebMethod()> _
    Public Function GetApplicationTypeList(ByVal myGUID As Guid) As List(Of ApplicationTypeItem)
        Dim myList As New List(Of ApplicationTypeItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetApplicationTypeList", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myList = mycon.GetApplicationTypeList()
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetApplicationTypeList")
                myList = New List(Of ApplicationTypeItem)
            End Try
        End If
        Return myList
    End Function
    <WebMethod()> _
    Public Function GetApplicationType(ByVal myApplicationTypeID As Integer, ByVal myGUID As Guid) As ApplicationTypeItem
        Dim myApplicationType As New ApplicationTypeItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetApplicationType", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myApplicationType = mycon.GetApplicationTypeByApplicationTypeID(myApplicationTypeID)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetApplicationType")
                myApplicationType = New ApplicationTypeItem
            End Try
        End If
        Return myApplicationType
    End Function
    <WebMethod()> _
    Public Function PutApplicationType(ByVal myApplicationType As ApplicationTypeItem, ByVal myGUID As Guid) As ApplicationTypeItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetApplicationTypeList", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myApplicationType = mycon.UpdateApplicationType(myApplicationType)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetApplicationTypeList")
                myApplicationType = New ApplicationTypeItem
            End Try
        End If
        Return myApplicationType
    End Function
    <WebMethod()> _
    Public Function DeleteApplicationType(ByVal myApplicationType As ApplicationTypeItem, ByVal myGUID As Guid) As Boolean
        Dim bReturn As Boolean = False
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.DeleteApplicationType", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    bReturn = mycon.DeleteApplicationType(myApplicationType)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.DeleteApplicationType")
                bReturn = False
            End Try
        End If
        Return bReturn
    End Function

#End Region

#Region "Survey Type Admin"
    <WebMethod()> _
    Public Function GetSurveyTypeList(ByVal myGUID As Guid) As List(Of SurveyTypeItem)
        Dim myList As New List(Of SurveyTypeItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetSurveyTypeList", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myList = mycon.GetSurveyTypeList()
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSurveyTypeList")
                myList = New List(Of SurveyTypeItem)
            End Try
        End If
        Return myList
    End Function
    <WebMethod()> _
    Public Function GetSurveyCategoryList(ByVal myGUID As Guid) As List(Of SurveyTypeItem)
        Dim myList As New List(Of SurveyTypeItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetSurveyCategoryList", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myList = mycon.GetSurveyCategoryList()
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSurveyCategoryList")
                myList = New List(Of SurveyTypeItem)
            End Try
        End If
        Return myList
    End Function
    <WebMethod()> _
    Public Function GetQuestionCategoryList(ByVal myGUID As Guid) As List(Of SurveyTypeItem)
        Dim myList As New List(Of SurveyTypeItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetQuestionCategoryList", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myList = mycon.GetQuestionCategoryList()
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetQuestionCategoryList")
                myList = New List(Of SurveyTypeItem)
            End Try
        End If
        Return myList
    End Function
    <WebMethod()> _
    Public Function GetSurveyCategoryListByApplicationTypeID(ByVal reqApplicationTypeID As Integer, ByVal myGUID As Guid) As List(Of SurveyTypeItem)
        Dim myList As New List(Of SurveyTypeItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetSurveyTypeList", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myList = (From i In mycon.GetSurveyCategoryList() Where i.ApplicationTypeID = reqApplicationTypeID).ToList
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSurveyTypeList")
                myList = New List(Of SurveyTypeItem)
            End Try
        End If
        Return myList
    End Function
    <WebMethod()> _
    Public Function GetSurveyType(ByVal mySurveyTypeID As Integer, ByVal myGUID As Guid) As SurveyTypeItem
        Dim mySurveyType As New SurveyTypeItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetSurveyType", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    mySurveyType = mycon.GetSurveyTypeBySurveyTypeID(mySurveyTypeID)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSurveyType")
                mySurveyType = New SurveyTypeItem
            End Try
        End If
        Return mySurveyType
    End Function
    <WebMethod()> _
    Public Function PutSurveyType(ByVal mySurveyType As SurveyTypeItem, ByVal myGUID As Guid) As SurveyTypeItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetSurveyTypeList", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    mySurveyType = mycon.UpdateSurveyType(mySurveyType)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSurveyTypeList")
                mySurveyType = New SurveyTypeItem
            End Try
        End If
        Return mySurveyType
    End Function
    <WebMethod()> _
    Public Function DeleteSurveyType(ByVal mySurveyType As SurveyTypeItem, ByVal myGUID As Guid) As Boolean
        Dim bReturn As Boolean = False
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.DeleteSurveyType", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    bReturn = mycon.DeleteSurveyType(mySurveyType)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.DeleteSurveyType")
                bReturn = False
            End Try
        End If
        Return bReturn
    End Function
#End Region

#Region "Survey Items"
    <WebMethod()> _
    Public Function GetSurvey(ByVal reqSurveyID As Integer, ByVal myGUID As Guid) As SurveyItem
        Dim mySurveyItem As New SurveyItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetSurvey", reqSurveyID)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    mySurveyItem = mycon.GetSurveyBySurveyID(reqSurveyID)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSurvey")
            End Try
        End If
        Return mySurveyItem
    End Function
    <WebMethod()> _
    Public Function PutSurveyItem(ByVal mySurveyItem As SurveyItem, ByVal myGUID As Guid) As SurveyItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.PutSurveyItem", mySurveyItem.SurveyID)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    mySurveyItem = mycon.UpdateSurvey(mySurveyItem, -1, -1)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.PutSurveyItem")
                mySurveyItem = New SurveyItem
            End Try
        Else
            Return New SurveyItem
        End If
        Return mySurveyItem
    End Function
    <WebMethod()> _
    Public Function DeleteSurveyItem(ByVal mySurveyItem As SurveyItem, ByVal myGUID As Guid) As Boolean
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.DeleteSurveyItem", mySurveyItem.SurveyID)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    mycon.DeleteSurvey(mySurveyItem)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.DeleteSurveyItem")
            End Try
            Return True
        Else
            Return False
        End If
    End Function
    <WebMethod()> _
    Public Function PutSurveyItemWithApplication(ByVal mySurveyItem As SurveyItem, ByVal ApplicationID As Integer, ByVal RoleID As Integer, ByVal myGUID As Guid) As SurveyItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.PutSurveyItemWithApplication", mySurveyItem.SurveyID)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    mySurveyItem = mycon.UpdateSurvey(mySurveyItem, ApplicationID, RoleID)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.PutSurveyItemWithApplication")
            End Try
            Return mySurveyItem
        Else
            Return New SurveyItem
        End If
    End Function
    <WebMethod()> _
    Public Function GetSurveys(ByVal Filters As SQLFilterList, ByVal myGuid As Guid) As List(Of SurveyItem)
        Dim mySurveyList As New List(Of SurveyItem)
        Dim myDic As New Dictionary(Of String, String)
        Dim curSRID As Integer = 0
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.GetSurveys", DataSource)
            Try
                Using myController As New DataController(str_ConnectionString)
                    If Filters.Count > 0 Then
                        mySurveyList.AddRange(From i In myController.GetSurveyList().ToList())
                    Else
                        mySurveyList.AddRange(From i In myController.GetSurveyList().ToList())
                    End If
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSurveys")
            End Try
        End If
        Return mySurveyList
    End Function
    <WebMethod()> _
    Public Function GetSurveySummaries(ByVal myGuid As Guid) As List(Of SurveyItem)
        Dim myList As New List(Of SurveyItem)
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.GetSurveySummaries", DataSource)
            Try
                Using myController As New DataController(str_ConnectionString)
                    myList.AddRange(myController.GetSurveySummary())
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSurveySummaries")
            End Try
        End If
        Return myList
    End Function
#End Region

#Region "Survey Question Group Item"

    <WebMethod()> _
    Public Function DeleteSurveyQuestionGroupItem(ByVal myGroupItem As QuestionGroupItem, ByVal myGUID As Guid) As Boolean
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.DeleteSurveyQuestionGroupItem", myGroupItem.QuestionGroupID)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    mycon.DeleteQuestionGroup(myGroupItem)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.DeleteSurveyQuestionGroupItem")
            End Try
            Return True
        Else
            Return False
        End If
    End Function


#End Region

#Region "Question Items"
    <WebMethod()> _
    Public Function GetQuestionItem(ByVal reqQuestionID As Integer, ByVal myGuid As Guid) As QuestionItem
        Dim myQuestionItem As New QuestionItem
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.GetQuestionItem", String.Format("QuestionID:{0}", reqQuestionID))
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myQuestionItem = mycon.GetQuestionByQuestionID(reqQuestionID)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetQuestionItem")
            End Try
        End If
        Return myQuestionItem
    End Function

    <WebMethod()> _
    Public Function GetQuestionByQuestionShortNM(ByVal QuestionShortNM As String, ByVal myGuid As Guid) As QuestionItem
        Dim myQuestionItem As New QuestionItem
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.GetQuestionByQuestionShortNM", String.Format("QuestionShortNM:{0}", QuestionShortNM))
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myQuestionItem = mycon.GetQuestionByQuestionShortNM(QuestionShortNM)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetQuestionByQuestionShortNM")
            End Try
        End If
        Return myQuestionItem
    End Function

    <WebMethod()> _
    Public Function PutQuestionItem(ByVal reqQuestionItem As QuestionItem, ByVal myGuid As Guid) As QuestionItem
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.PutQuestionItem", String.Format("QustionID:{0};QuestionShortNM:{1};QuestionNM:{2}", reqQuestionItem.QuestionID, reqQuestionItem.QuestionShortNM, reqQuestionItem.QuestionNM))
            Try
                Using mycon As New DataController(str_ConnectionString)
                    reqQuestionItem = mycon.PutQuestion(reqQuestionItem)
                End Using
                AppLog.AuditLog("Service.PutQuestionItem", reqQuestionItem.QuestionNM)
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.PutQuestionItem")
            End Try
            Return reqQuestionItem
        Else
            Return New QuestionItem
        End If
    End Function

    <WebMethod()> _
    Public Function DeleteQuestionItem(ByVal myQuestion As QuestionItem, ByVal myGUID As Guid) As Boolean
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.DeleteQuestionItem", String.Format("QustionID:{0};QuestionShortNM:{1};QuestionNM:{2}", myQuestion.QuestionID, myQuestion.QuestionShortNM, myQuestion.QuestionNM))
            Try
                Using mycon As New DataController(str_ConnectionString)
                    mycon.DeleteQuestion(myQuestion)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.DeleteQuestionItem")
            End Try
            Return True
        Else
            Return False
        End If
    End Function



    <WebMethod()> _
    Public Function GetQuestions(ByVal Filters As SQLFilterList, ByVal myGuid As Guid) As List(Of QuestionItem)
        Dim myQuestionList As New List(Of QuestionItem)
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.GetQuestions", DataSource)
            Try
                Using myController As New DataController(str_ConnectionString)
                    If Filters.Count > 0 Then
                        For Each myQ In (From i In myController.vwQuestionLibraries.Where(Filters.GetLINQWhere) Select i).ToList
                            myQuestionList.Add(New QuestionItem With {
                                               .QuestionID = myQ.QuestionID,
                                               .QuestionNM = myQ.QuestionNM,
                                               .QuestionShortNM = myQ.QuestionShortNM,
                                               .QuestionSort = myQ.QuestionSort,
                                               .QuestionTypeCD = myQ.QuestionTypeCD,
                                               .QuestionTypeID = myQ.QuestionTypeID,
                                               .QuestionValue = myQ.QuestionValue,
                                               .ReviewRoleLevel = myQ.ReviewRoleLevel,
                                               .CommentFL = myQ.CommentFL,
                                               .MaxQuestionValue = myQ.MaxScore,
                                               .SurveyTypeID = myQ.SurveyTypeID,
                                               .UnitOfMeasureID = myQ.UnitOfMeasureID})
                        Next
                    Else
                        myQuestionList = (From i In myController.GetQuestions Select i).ToList
                    End If
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetQuestions")
            End Try
        End If
        Return myQuestionList
    End Function

#End Region

#Region "Survey Response Item"
    <WebMethod()> _
    Public Function GetSurveyResponseItem(ByVal reqSurveyResponseID As Integer, ByVal myGuid As Guid) As SurveyResponseItem
        Dim mySurveyResponse As New SurveyResponseItem
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.GetSurveyResponseItem", reqSurveyResponseID.ToString())
            Try
                Using myCon As New DataController(str_ConnectionString)
                    mySurveyResponse = myCon.GetApplicationSurveyResponse_SelectBySurveyResponseID(reqSurveyResponseID)
                End Using
                If Not (mySurveyResponse.Survey.QuestionList Is Nothing) AndAlso mySurveyResponse.Survey.QuestionList.Count > 0 Then
                    For Each myQuestion As QuestionItem In mySurveyResponse.Survey.QuestionList
                        If myQuestion.QuestionDS.Trim <> String.Empty Then
                            Dim newQuestionDS As New StringBuilder(String.Empty)
                            For Each myP In myQuestion.QuestionDS.Split(Environment.NewLine)
                                newQuestionDS.Append(String.Format("<p>{0}</p>", myP))
                            Next
                            myQuestion.QuestionDS = newQuestionDS.ToString
                        End If
                        myQuestion.SurveyResponseAnswerItemList.AddRange((From i In mySurveyResponse.AnswerList Where i.QuestionID = myQuestion.QuestionID).ToList())
                    Next
                End If
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSurveyResponseItem")
            End Try
        End If
        Return mySurveyResponse
    End Function
    <WebMethod()> _
    Public Function PutSurveyResponseItem(ByVal reqSurveyResponseItem As SurveyResponseItem, ByVal myGuid As Guid) As SurveyResponseItem
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.PutSurveyResponseItem", String.Format("SurveyResponseID={0}, SurveyResponseNM={1}, New Answer Count:{2}", reqSurveyResponseItem.SurveyResponseNM, reqSurveyResponseItem.SurveyResponseID, reqSurveyResponseItem.NewAnswerList.Count))
            Dim AppEmail As New ApplicationEmailConfiguration With {.SendEmail = False}
            Try



                Using mypresentor As New SurveyResponseUI(str_ConnectionString)
                    mypresentor.SetSurveyResponseUI(Me)
                    mySurveyResponse = mypresentor.GetSurveyResponseItemBySurveyResponseID(reqSurveyResponseItem.SurveyResponseID)
                    If reqSurveyResponseItem.NewAnswerList.Count > 0 Then
                        With mySurveyResponse
                            If String.IsNullOrEmpty(reqSurveyResponseItem.SurveyResponseNM) Then
                            Else
                                .SurveyResponseNM = reqSurveyResponseItem.SurveyResponseNM
                            End If
                            .DataSource = HttpContext.Current.Request.Url.DnsSafeHost
                        End With
                        mypresentor.SetNewAnswers(reqSurveyResponseItem.NewAnswerList)
                        mypresentor.ProcessNewAnswers(reqSurveyResponseItem.CurrentQuestionGroupID, AppEmail)
                    Else
                        With mySurveyResponse
                            If .SurveyResponseID < 1 Then
                                .ApplicationID = reqSurveyResponseItem.ApplicationID
                                .SurveyResponseID = reqSurveyResponseItem.SurveyResponseID
                                .Survey.SurveyID = reqSurveyResponseItem.Survey.SurveyID
                                .AssignedUserID = reqSurveyResponseItem.AssignedUserID
                                .StatusID = reqSurveyResponseItem.StatusID
                                .ModifiedDT = reqSurveyResponseItem.ModifiedDT
                                .ModifiedID = reqSurveyResponseItem.ModifiedID
                            End If

                            If String.IsNullOrEmpty(reqSurveyResponseItem.SurveyResponseNM) Then
                                AppLog.ErrorLog("Survey Response Name is blank", "Service.PutSurveyResponseItem")
                                .SurveyResponseNM = reqSurveyResponseItem.Survey.SurveyNM & " - " & reqSurveyResponseItem.AccountNM
                            Else
                                .SurveyResponseNM = reqSurveyResponseItem.SurveyResponseNM
                            End If

                            If .AssignedUserID <> reqSurveyResponseItem.AssignedUserID Then
                                .AssignedUserID = reqSurveyResponseItem.AssignedUserID
                                AppLog.ErrorLog("Assigned User Has Changed", "Service.PutSurveyResponseItem")
                            End If

                            If .Survey.SurveyID <> reqSurveyResponseItem.Survey.SurveyID Then
                                .Survey.SurveyID = reqSurveyResponseItem.Survey.SurveyID
                                AppLog.ErrorLog("Survey Has Changed", "Service.PutSurveyResponseItem")
                            End If


                            .DataSource = HttpContext.Current.Request.Url.DnsSafeHost
                        End With
                        mypresentor.SaveSurveyResponseItem(mySurveyResponse.StatusID, AppEmail)
                    End If
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.PutSurveyResponseItem")
            End Try
        End If
        Return mySurveyResponse
    End Function
    <WebMethod()> _
    Public Function DeleteSurveyResponseItem(ByVal mySR As SurveyResponseItem, ByVal myGuid As Guid) As Integer
        Dim iReturn As Integer = -1
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.DeleteSurveyResponseItem", mySR.SurveyResponseID.ToString())
            Try
                Using myCON As New DataController(str_ConnectionString)
                    iReturn = myCON.DeleteSurveyResponse(mySR)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.DeleteSurveyResponseItem")
            End Try
        End If
        Return iReturn
    End Function

    <WebMethod()> _
    Public Function ResetSurveyResponseItem(ByVal mySR As SurveyResponseItem, ByVal myGuid As Guid) As Integer
        Dim iReturn As Integer = -1
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.ResetSurveyResponseItem", mySR.SurveyResponseID.ToString())
            Try
                Using myCON As New DataController(str_ConnectionString)
                    iReturn = myCON.ResetSurveyResponse(mySR)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.ResetSurveyResponseItem")
            End Try
        End If
        Return iReturn
    End Function
    <WebMethod()> _
    Public Function GetSuveyResponseListByApplicationUserID(ByVal reqApplicationID As Integer, ByVal reqApplicationUserID As Integer, ByVal myGuid As Guid) As List(Of SurveyResponseItem)
        Dim mySurveyResponseItemList As New List(Of SurveyResponseItem)
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.GetSuveyResponseListByApplicationUserID", reqApplicationID)
            Try
                Using myCon As New DataController(str_ConnectionString)
                    For Each mySRBL In myCon.GetSurveyResponsesByApplicationUserForInput(reqApplicationID, reqApplicationUserID)
                        mySurveyResponseItemList.Add(CType(mySRBL, SurveyResponseItem))
                    Next
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSuveyResponseListByApplicationUserID")
            End Try
        Else
            Return New List(Of SurveyResponseItem)
        End If
        Return mySurveyResponseItemList
    End Function

#End Region

#Region "Implementation of ISurveyResponseItem  "
    Public Property AssignedUserID As Integer? Implements ISurveyResponse.AssignedUserID
        Get
            Return mySurveyResponse.AssignedUserID
        End Get
        Set(value As Integer?)
            mySurveyResponse.AssignedUserID = value
        End Set
    End Property
    Public Property AssignedSupervisorUserID As Integer? Implements ISurveyResponse.AssignedSupervisorUserID
        Get
            Return mySurveyResponse.AssignedSupervisorUserID
        End Get
        Set(value As Integer?)
            mySurveyResponse.AssignedSupervisorUserID = value
        End Set
    End Property
    Public Property DataSource As String Implements ISurveyResponse.DataSource
        Get
            Return mySurveyResponse.DataSource
        End Get
        Set(value As String)
            mySurveyResponse.DataSource = value
        End Set
    End Property
    Public Property SurveyResponseModifiedID As Integer Implements ISurveyResponse.ModifiedID
        Get
            Return mySurveyResponse.ModifiedID
        End Get
        Set(value As Integer)
            mySurveyResponse.ModifiedID = value
        End Set
    End Property
    Public Property StatusID As Integer Implements ISurveyResponse.StatusID
        Get
            Return mySurveyResponse.StatusID
        End Get
        Set(value As Integer)
            mySurveyResponse.StatusID = value
        End Set
    End Property
    Public Property SurveyResponseNM As String Implements ISurveyResponse.SurveyResponseNM
        Get
            Return mySurveyResponse.SurveyResponseNM
        End Get
        Set(value As String)
            mySurveyResponse.SurveyResponseNM = value
        End Set
    End Property
    Public Property SurveyResponseID As Integer Implements ISurveyResponse.SurveyResponseID
        Get
            Return mySurveyResponse.SurveyResponseID
        End Get
        Set(value As Integer)
            mySurveyResponse.SurveyResponseID = value
        End Set
    End Property
    Public Property SurveyResponseAccountNM As String Implements ISurveyResponse.AccountNM
        Get
            Return mySurveyResponse.AccountNM
        End Get
        Set(value As String)
            mySurveyResponse.AccountNM = value
        End Set
    End Property
    Public Property CurrentAnswerList As List(Of SurveyResponseAnswerItem) Implements ISurveyResponse.AnswerList
        Get
            Return mySurveyResponse.AnswerList
        End Get
        Set(value As List(Of SurveyResponseAnswerItem))
            mySurveyResponse.AnswerList = value
        End Set
    End Property
    Public Property SequenceList As List(Of SurveyResponseSequenceItem) Implements ISurveyResponse.SequenceList
        Get
            Return mySurveyResponse.SequenceList
        End Get
        Set(value As List(Of SurveyResponseSequenceItem))
            mySurveyResponse.SequenceList = value
        End Set
    End Property
    Public Property StatusNM As String Implements ISurveyResponse.StatusNM
        Get
            Return mySurveyResponse.StatusNM
        End Get
        Set(value As String)
            mySurveyResponse.StatusNM = value
        End Set
    End Property
    Public Property Survey As SurveyItem Implements ISurveyResponse.Survey
        Get
            Return mySurveyResponse.Survey
        End Get
        Set(value As SurveyItem)
            mySurveyResponse.Survey = value
        End Set
    End Property
    Public Property ShowQuestionDescription As Boolean Implements ISurveyResponse.ShowQuestionDescription
        Get
            Return mySurveyResponse.ShowQuestionDescription
        End Get
        Set(value As Boolean)
            mySurveyResponse.ShowQuestionDescription = value
        End Set
    End Property
    Public Property NewAnswerList As List(Of SurveyResponseAnswerItem) Implements ISurveyResponse.NewAnswerList
        Get
            Return mySurveyResponse.NewAnswerList
        End Get
        Set(value As List(Of SurveyResponseAnswerItem))
            mySurveyResponse.NewAnswerList = value
        End Set
    End Property
    Public Overloads Property ApplicationID As Integer Implements ISurveyResponse.ApplicationID
        Get
            Return mySurveyResponse.ApplicationID
        End Get
        Set(value As Integer)
            mySurveyResponse.ApplicationID = value
        End Set
    End Property
    Public Property NewAnswerReviewList As List(Of SurveyResponseAnswerReviewItem) Implements ISurveyResponse.NewAnswerReviewList
        Get
            Return mySurveyResponse.NewAnswerReviewList
        End Get
        Set(value As List(Of SurveyResponseAnswerReviewItem))
            mySurveyResponse.NewAnswerReviewList = value
        End Set
    End Property
    Public Property SurveyResponseHistory As List(Of SurveyResponseHistoryItem) Implements ISurveyResponse.SurveyResponseHistory
        Get
            Return mySurveyResponse.SurveyResponseHistory
        End Get
        Set(value As List(Of SurveyResponseHistoryItem))
            mySurveyResponse.SurveyResponseHistory = value
        End Set
    End Property
    Public Property ReviewerAccountNM As String Implements ISurveyResponse.SupervisorAccountNM
        Get
            Return mySurveyResponse.SupervisorAccountNM
        End Get
        Set(value As String)
            mySurveyResponse.SupervisorAccountNM = value
        End Set
    End Property
    Public Property AnswerCount As Integer Implements ISurveyResponse.AnswerCount
        Get
            Return mySurveyResponse.AnswerCount
        End Get
        Set(value As Integer)
            mySurveyResponse.AnswerCount = value
        End Set
    End Property
    Public Property ComplianceReviewCount As Integer Implements ISurveyResponse.ComplianceReviewCount
        Get
            Return mySurveyResponse.ComplianceReviewCount
        End Get
        Set(value As Integer)
            mySurveyResponse.ComplianceReviewCount = value
        End Set
    End Property
    Public Property Manager_Name As String Implements ISurveyResponse.Manager_Name
        Get
            Return mySurveyResponse.Manager_Name
        End Get
        Set(value As String)
            mySurveyResponse.Manager_Name = value
        End Set
    End Property
    Public Property Employee_FName As String Implements ISurveyResponse.Employee_FName
        Get
            Return mySurveyResponse.Employee_FName
        End Get
        Set(value As String)
            mySurveyResponse.Employee_FName = value
        End Set
    End Property
    Public Property Employee_LName As String Implements ISurveyResponse.Employee_LName
        Get
            Return mySurveyResponse.Employee_LName
        End Get
        Set(value As String)
            mySurveyResponse.Employee_LName = value
        End Set
    End Property
    Public Property ManagerUserID As String Implements ISurveyResponse.ManagerUserID
        Get
            Return mySurveyResponse.ManagerUserID
        End Get
        Set(value As String)
            mySurveyResponse.ManagerUserID = value
        End Set
    End Property
    Public Property PercentComplete As Integer Implements ISurveyResponse.PercentComplete
        Get
            Return mySurveyResponse.PercentComplete
        End Get
        Set(value As Integer)
            mySurveyResponse.PercentComplete = value
        End Set
    End Property
    Public Property QuestionCount As Integer Implements ISurveyResponse.QuestionCount
        Get
            Return mySurveyResponse.QuestionCount
        End Get
        Set(value As Integer)
            mySurveyResponse.QuestionCount = value
        End Set
    End Property
    Public Property VariantAnswersCount As Integer Implements ISurveyResponse.VariantAnswersCount
        Get
            Return mySurveyResponse.VariantAnswersCount
        End Get
        Set(value As Integer)
            mySurveyResponse.VariantAnswersCount = value
        End Set
    End Property
    Public Property StateList As List(Of SurveyResponseStateItem) Implements ISurveyResponse.StateList
        Get
            Return mySurveyResponse.StateList
        End Get
        Set(value As List(Of SurveyResponseStateItem))
            mySurveyResponse.StateList = value
        End Set
    End Property

#End Region

#Region "Web Portal"
    <WebMethod()> _
    Public Function GetWebPortals(ByVal myGuid As Guid) As List(Of WebPortalItem)
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.GetWebPortals", DataSource)
            Try
                Using myCon As New DataController(str_ConnectionString)
                    Return myCon.GetWebPortalList()
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetWebPortals")
            End Try
        End If
        Return New List(Of WebPortalItem)
    End Function
#End Region

#Region "tblFiles Admin"
    <WebMethod()> _
    Public Function GettblFilesList(ByVal myGUID As Guid) As List(Of tblFilesItem)
        Dim myList As New List(Of tblFilesItem)
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GettblFilesList", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    myList = mycon.GettblFilesList()
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GettblFilesList")
                myList = New List(Of tblFilesItem)
            End Try
        End If
        Return myList
    End Function
    <WebMethod()> _
    Public Function GettblFiles(ByVal mytblFilesID As Integer, ByVal myGUID As Guid) As tblFilesItem
        Dim mytblFiles As New tblFilesItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GettblFiles", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    mytblFiles = mycon.GettblFilesBytblFilesID(mytblFilesID)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GettblFiles")
                mytblFiles = New tblFilesItem
            End Try
        End If
        Return mytblFiles
    End Function
    <WebMethod()> _
    Public Function PuttblFiles(ByVal mytblFiles As tblFilesItem, ByVal myGUID As Guid) As tblFilesItem
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GettblFilesList", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    mytblFiles = mycon.UpdatetblFiles(mytblFiles)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GettblFilesList")
                mytblFiles = New tblFilesItem
            End Try
        End If
        Return mytblFiles
    End Function
    <WebMethod()> _
    Public Function DeletetblFiles(ByVal mytblFiles As tblFilesItem, ByVal myGUID As Guid) As Boolean
        Dim bReturn As Boolean = False
        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.DeletetblFiles", DataSource)
            Try
                Using mycon As New DataController(str_ConnectionString)
                    bReturn = mycon.DeletetblFiles(mytblFiles)
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.DeletetblFiles")
                bReturn = False
            End Try
        End If
        Return bReturn
    End Function

#End Region



    Private Function ValidateGUID(ByRef myGuid As Guid) As Boolean
        If myGuid = Guid.Parse("FFB6791E-39BA-404A-BA86-B2C3210CD259") Then
            DataSource = "ControlOriginsMetro"
            Return True
        ElseIf myGuid = Guid.Parse("85AAA903-3C57-4FB0-B91D-B46633C7C637") Then
            DataSource = "CODataCon"
            Return True
        Else
            AppLog.AuditLog(String.Format("Invalid GUID:{0}", myGuid), "Service.ValidateGUID")
            Return False
        End If
    End Function

End Class

