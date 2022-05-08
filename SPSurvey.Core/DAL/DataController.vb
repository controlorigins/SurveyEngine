Imports System.Data.Linq
Imports System.Reflection
Imports System.Data
Imports LINQHelper.System.Linq.Dynamic
Imports ControlOrigins.COUtility

Public Class DataController
    Inherits SurveyAppLINQDataContext

#Region "Constructors"
    Private bReturn As Boolean
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal connection As String)
        MyBase.New(connection)
    End Sub
    Public Sub New(ByVal connection As IDbConnection)
        MyBase.New(connection)
    End Sub
    Public Sub New(ByVal connection As String, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
        MyBase.New(connection, mappingSource)
    End Sub
    Public Sub New(ByVal connection As IDbConnection, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
        MyBase.New(connection, mappingSource)
    End Sub
#End Region
#Region "Lookups - Read Only"

    Function GetQuestionLibrary() As List(Of vwQuestionLibrary)
        Return (From i In vwQuestionLibraries Select i).ToList()
    End Function
    Function GetUnitOfMeasures() As List(Of LookupItem)
        Return (From i In gsp_lu_UnitOfMeasure_Select(Nothing) Order By i.UnitOfMeasureNM Ascending Select New LookupItem With {.Name = i.UnitOfMeasureNM, .Value = i.UnitOfMeasureID.ToString}).ToList()
    End Function
    Function GetQuestionTypes() As List(Of LookupItem)
        Return (From i In gsp_lu_QuestionType_Select(Nothing) Order By i.QuestionTypeCD Ascending Select New LookupItem With {.Name = String.Format("{0}-{1}", i.QuestionTypeCD, i.QuestionTypeDS), .Value = i.QuestionTypeID.ToString}).ToList()
    End Function
    Function GetSurveyTypes() As List(Of LookupItem)
        Return (From I In lu_SurveyType_SelectAll() Order By I.SurveyTypeNM Ascending Select New LookupItem With {.Name = I.SurveyTypeNM, .Value = I.SurveyTypeID.ToString}).ToList()
    End Function
    Function GetApplicationTypes() As List(Of LookupItem)
        Return (From i In lu_ApplicationType_SelectAll() Order By i.ApplicationTypeNM Ascending Select New LookupItem With {.Name = i.ApplicationTypeNM, .Value = CStr(i.ApplicationTypeID)}).ToList()
    End Function
    Function GetQuestionLookupList() As List(Of LookupItem)
        Return (From i In Question_SelectAll() Order By i.QuestionShortNM Ascending Select New LookupItem With {.Name = i.QuestionShortNM, .Value = CStr(i.QuestionID)}).ToList()
    End Function
    Function GetQuestionGroupLookupList() As List(Of LookupItem)
        Return (From i In gsp_QuestionGroup_Select(Nothing) Order By i.QuestionGroupShortNM Ascending Select New LookupItem With {.Name = i.QuestionGroupNM, .Value = CStr(i.QuestionGroupID)}).ToList()
    End Function
    Function GetQuestionGroupLookupList(ByVal SurveyID As Integer) As List(Of LookupItem)
        Return (From i In usp_QuestionGroup_SelectBySurveyID(SurveyID) Order By i.QuestionGroupShortNM Ascending Select New LookupItem With {.Name = i.QuestionGroupNM, .Value = CStr(i.QuestionGroupID)}).ToList()
    End Function
    Function GetSurveyLookupList() As List(Of LookupItem)
        Return (From i In gsp_Survey_Select(Nothing) Order By i.SurveyNM Ascending Select New LookupItem With {.Name = i.SurveyNM, .Value = CStr(i.SurveyID)}).ToList()
    End Function
    Function GetSurveyLookupList(ByVal ApplicationID As Integer) As List(Of LookupItem)
        Return (From i In usp_Survey_SelectByApplicationID(ApplicationID) Order By i.SurveyNM Ascending Select New LookupItem With {.Name = i.SurveyNM, .Value = CStr(i.SurveyID)}).ToList()
    End Function
    Function GetRolesLookupList() As List(Of LookupItem)
        Return (From i In gsp_Role_Select(Nothing) Order By i.RoleNM Ascending Select New LookupItem With {.Name = i.RoleNM, .Value = CStr(i.RoleID)}).ToList()
    End Function
    Function GetUserLookupList() As List(Of LookupItem)
        Return (From i In ApplicationUser_SelectAll() Order By i.LastNM, i.FirstNM Select New LookupItem With {.Name = String.Format("{0} {1} ({2})", i.FirstNM, i.LastNM, i.AccountNM), .Value = CStr(i.ApplicationUserID)}).ToList()
    End Function
    Function GetSurveyResponseStatusList() As List(Of LookupItem)
        Return (From i In gsp_lu_SurveyResponseStatus_Select(Nothing) Order By i.StatusNM Select New LookupItem With {.Name = i.StatusNM, .Value = CStr(i.StatusID)}).ToList()
    End Function
    Function GetReviewSatusLists() As List(Of LookupItem)
        Return (From i In gsp_lu_ReviewStatus_Select(Nothing) Order By i.ReviewStatusNM Select New LookupItem With {.Name = i.ReviewStatusNM, .Value = CStr(i.ReviewStatusID)}).ToList()
    End Function
    Function GetReviewLevelList() As List(Of LookupItem)
        Return (From I In gsp_Role_Select(Nothing) Order By I.ReviewLevel Select New LookupItem With {.Name = String.Format("{0} - {1}", I.ReviewLevel, I.RoleCD), .Value = CStr(I.ReviewLevel)}).ToList()
    End Function
#End Region
#Region "Application User Methods"
    Function GetApplicationUserList() As List(Of ApplicationUserItem)
        Dim myList As List(Of ApplicationUserItem) = (From i In ApplicationUser_SelectSummary() Order By i.FirstNM, i.LastNM
                Select New ApplicationUserItem With {.ApplicationUserID = i.ApplicationUserID,
                                                     .AccountNM = i.AccountNM,
                                                     .SupervisorAccountNM = i.SupervisorAccountNM,
                                                     .CompanyID = AppUtility.GetDBInteger(i.CompanyID),
                                                     .CompanyNM = i.CompanyNM,
                                                     .EmailVerified = AppUtility.GetDBBoolean(i.EmailVerified),
                                                     .CommentDS = i.CommentDS,
                                                     .eMailAddress = i.eMailAddress,
                                                     .FirstNM = i.FirstNM,
                                                     .LastNM = i.LastNM,
                                                     .LastLoginDT = AppUtility.GetDBDate(i.LastLoginDT),
                                                     .LastLoginLocation = i.LastLoginLocation,
                                                     .ApplicatonUserRoleCount = AppUtility.GetDBInteger(i.URCount),
                                                     .SurveyResponseCount = AppUtility.GetDBInteger(i.SRCount),
                                                     .ModifiedDT = i.ModifiedDT,
                                                     .ModifiedID = i.ModifiedID,
                                                     .DisplayName = i.DisplayName,
                                                     .UserKey = i.UserKey,
                                                     .UserLogin = i.UserLogin,
                                                     .UserRoleID = i.RoleID,
                                                     .UserRoleName = i.RoleName,
                                                     .VerifyCode = i.VerifyCode
                                                    }).ToList()

        For Each myUser In myList
            myUser.ApplicationUserRoleList.AddRange(GetApplicationUserRolesByApplicationUserID(myUser.ApplicationUserID))
            myUser.ApplicatonUserRoleCount = myUser.ApplicationUserRoleList.Count()
        Next

        Return myList
    End Function
    Function GetApplicationUserByApplicationUserID(ByVal reqApplicationUserID As Integer) As ApplicationUserItem
        Dim myApplicationuser = (From i In ApplicationUser_SelectRow(reqApplicationUserID)
                                 Select New ApplicationUserItem With {.ApplicationUserID = i.ApplicationUserID,
                                                                      .AccountNM = i.AccountNM,
                                                                      .SupervisorAccountNM = i.SupervisorAccountNM,
                                                                      .CompanyID = AppUtility.GetDBInteger(i.CompanyID),
                                                                      .CompanyNM = i.CompanyNM,
                                                                      .EmailVerified = AppUtility.GetDBBoolean(i.EmailVerified),
                                                                      .CommentDS = i.CommentDS,
                                                                      .eMailAddress = i.eMailAddress,
                                                                      .FirstNM = i.FirstNM,
                                                                      .DisplayName = i.DisplayName,
                                                                      .UserLogin = i.UserLogin,
                                                                      .UserRoleID = i.RoleID,
                                                                      .UserRoleName = i.RoleName,
                                                                      .LastLoginDT = AppUtility.GetDBDate(i.LastLoginDT),
                                                                      .LastLoginLocation = i.LastLoginLocation,
                                                                      .LastNM = i.LastNM,
                                                                      .VerifyCode = i.VerifyCode,
                                                                      .ModifiedID = i.ModifiedID,
                                                                      .ModifiedDT = i.ModifiedDT}).SingleOrDefault()





        myApplicationuser.ApplicationUserRoleList.AddRange(GetApplicationUserRolesByApplicationUserID(myApplicationuser.ApplicationUserID))
        myApplicationuser.ApplicatonUserRoleCount = myApplicationuser.ApplicationUserRoleList.Count

        myApplicationuser.SurveyResponseList.AddRange(GetSurveyResponseListByApplicationUserID(myApplicationuser.ApplicationUserID))
        myApplicationuser.SurveyResponseCount = myApplicationuser.SurveyResponseList.Count

        myApplicationuser.Messages.AddRange((From x In UserMessages
                                             Where x.ToUserID = myApplicationuser.ApplicationUserID
                                             Select New SiteMessageItem With {.Id = x.Id,
                                                                              .AppID = x.AppID,
                                                                              .CratedDateTime = x.CratedDateTime,
                                                                              .Deleted = AppUtility.GetDBBoolean(x.Deleted, False),
                                                                              .FromApp = x.FromApp,
                                                                              .FromUserID = x.FromUserID,
                                                                              .Message = x.Message,
                                                                              .Opened = AppUtility.GetDBBoolean(x.Opened, False),
                                                                              .ShowonPage = x.ShowonPage,
                                                                              .Subject = x.Subject,
                                                                              .ToUserID = x.ToUserID}).ToArray())
        myApplicationuser.MessageCount = myApplicationuser.Messages.Count
        If myApplicationuser.MessageCount > 0 Then
            myApplicationuser.HasMessages = True
        End If
        myApplicationuser.Properties.AddRange((From x In UserAppProperties Where x.UserID = myApplicationuser.ApplicationUserID
                        Select New UserAppPropertyItem With {.Id = x.Id,
                                                             .AppID = x.AppID,
                                                             .Key = x.Key,
                                                             .UserID = myApplicationuser.ApplicationUserID,
                                                             .Value = x.Value}).ToList())
        Return myApplicationuser

    End Function
    Function DeleteApplicationUser(ByVal thisApplicationUser As ApplicationUserItem) As Boolean
        If ApplicationUser_DeleteRow(thisApplicationUser.ApplicationUserID) = 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function
    Function UpdateApplicationUser(ByVal thisApplicationUser As ApplicationUserItem, ByRef sReturn As String) As ApplicationUserItem
        sReturn = String.Empty
        If thisApplicationUser.ApplicationUserID < 1 Then
            Try
                With thisApplicationUser
                    Dim myResult = ApplicationUser_Insert(.FirstNM,
                                                          .LastNM,
                                                          .eMailAddress,
                                                          .CommentDS,
                                                          .AccountNM,
                                                          .SupervisorAccountNM,
                                                          .CompanyID,
                                                          .ModifiedID,
                                                          Now(),
                                                          .DisplayName,
                                                          String.Empty,
                                                          .UserRoleID,
                                                          New Guid,
                                                          .AccountNM,
                                                          .EmailVerified,
                                                          (New Guid).ToString).SingleOrDefault
                    .ApplicationUserID = myResult.ApplicationUserID
                End With
            Catch ex As Exception
                sReturn = String.Format("DataController.UpdateApplicationUser-Insert - {0} - {1}", thisApplicationUser.AccountNM, ex.ToString)
            End Try
        Else
            Try
                With thisApplicationUser
                    ApplicationUser_Update(.ApplicationUserID,
                                           .FirstNM,
                                           .LastNM,
                                           .eMailAddress,
                                           .CommentDS,
                                           .AccountNM,
                                           .SupervisorAccountNM,
                                           .CompanyID,
                                           .ModifiedID,
                                           .ModifiedDT,
                                           .DisplayName,
                                           .UserRoleID,
                                           .AccountNM,
                                           .EmailVerified,
                                           .VerifyCode)
                End With
            Catch ex As Exception
                sReturn = String.Format("DataController.UpdateApplicationUser-Update - {0} - {1}", thisApplicationUser.AccountNM, ex.ToString)
            End Try
        End If
        Return GetApplicationUserByApplicationUserID(thisApplicationUser.ApplicationUserID)
    End Function
    Public Function CreateNewUser(firstname As String, lastname As String, email As String, password As String) As ApplicationUserItem
        Dim newuser As New ApplicationUser With {.AccountNM = email,
                                            .ModifiedDT = Now,
                                            .LastLoginDT = Now,
                                            .DisplayName = String.Format("{0} {1}", firstname, lastname),
                                            .eMailAddress = email,
                                            .EmailVerified = False,
                                            .FirstNM = firstname,
                                            .LastNM = lastname,
                                            .UserLogin = email,
                                            .Password = password,
                                            .RoleID = 2,
                                            .UserKey = Guid.NewGuid,
                                            .VerifyCode = Guid.NewGuid.ToString}
        ApplicationUsers.InsertOnSubmit(newuser)
        SubmitChanges()
        Return GetSiteUser(newuser.UserLogin, newuser.Password)
    End Function
    Public Function Checkpassword(userid As Integer, pass As String) As Boolean
        If (From i In ApplicationUsers Where i.ApplicationUserID = userid And i.Password = pass).Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function updatepassword(userid As Integer, newpass As String) As Boolean
        Try
            Dim myuser = (From i In ApplicationUsers Where i.ApplicationUserID = userid).Single
            myuser.Password = newpass
            SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetSiteUser(ByVal Login As String, ByVal Pass As String) As ApplicationUserItem
        Dim myList As New List(Of ApplicationUserItem)
        For Each i In (From x In ApplicationUsers Where x.UserLogin = Login And x.Password = Pass Select x).ToList()
            ApplicationUser_UpdateLastLogin(i.ApplicationUserID, Now, "ws.controlorigins.com", 1, Now)
            myList.Add(GetApplicationUserByApplicationUserID(i.ApplicationUserID))
        Next
        If myList.Count = 1 Then
            Return myList(0)
        Else
            Return New ApplicationUserItem
        End If
    End Function
    Public Function GetApplicationUserListByApplicationID(ByVal reqSiteAppID As Integer) As List(Of ApplicationUserItem)
        Dim myList As New List(Of ApplicationUserItem)
        For Each i In (From x In ApplicationUserRoles Where x.ApplicationID = reqSiteAppID).ToList()
            myList.Add(GetApplicationUserByApplicationUserID(i.ApplicationUserID))
        Next
        Return myList
    End Function
    Public Function DeleteApplicationUserRole(ByVal UserID As Integer, ByVal appID As Integer) As Boolean
        Try
            Dim myapprelation = (From i In ApplicationUserRoles Where i.ApplicationUserID = UserID And i.ApplicationID = appID).Single
            ApplicationUserRoles.DeleteOnSubmit(myapprelation)
            SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function UnRegisterUserFromApp(UserID As Integer, appid As Integer) As Boolean
        Try
            Dim myapprelation = (From i In ApplicationUserRoles Where i.ApplicationUserID = UserID And i.ApplicationID = appid).Single
            myapprelation.UserInRolled = False
            SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region
#Region "Application Methods"
    Function GetApplicationList() As List(Of ApplicationItem)
        Return (From i In Application_SelectSummary() Order By i.ApplicationNM
                        Select New ApplicationItem With {.ApplicationCD = i.ApplicationCD,
                                                         .ApplicationDS = i.ApplicationDS,
                                                         .ApplicationNM = i.ApplicationNM,
                                                         .ApplicationShortNM = i.ApplicationShortNM,
                                                         .ApplicationTypeNM = i.ApplicationTypeNM,
                                                         .ApplicationTypeID = AppUtility.GetDBInteger(i.ApplicationTypeID),
                                                         .ApplicationFolder = i.ApplicationFolder,
                                                         .CompanyID = AppUtility.GetDBInteger(i.CompanyID),
                                                         .CompanyNM = AppUtility.GetDBString(i.CompanyNM),
                                                         .DefaultAppPage = i.DefaultPageID,
                                                         .MenuOrder = i.MenuOrder,
                                                         .ApplicationID = i.ApplicationID,
                                                         .SurveyCount = AppUtility.GetDBInteger(i.SurveyCount),
                                                         .UserCount = AppUtility.GetDBInteger(i.UserCount),
                                                         .SurveyResponseCount = AppUtility.GetDBInteger(i.SurveyResponseCount),
                                                         .ModifiedDT = i.ModifiedDT,
                                                         .ModifiedID = i.ModifiedID
                                                         }).ToList
    End Function
    Function GetApplications() As List(Of ApplicationItem)
        Dim myApplications As List(Of ApplicationItem)
        myApplications = (From i In Application_SelectSummary() Order By i.ApplicationNM
                        Select New ApplicationItem With {.ApplicationCD = i.ApplicationCD,
                                                         .ApplicationDS = i.ApplicationDS,
                                                         .ApplicationNM = i.ApplicationNM,
                                                         .ApplicationShortNM = i.ApplicationShortNM,
                                                         .ApplicationTypeID = AppUtility.GetDBInteger(i.ApplicationTypeID),
                                                         .ApplicationTypeNM = i.ApplicationTypeNM,
                                                         .CompanyID = AppUtility.GetDBInteger(i.CompanyID),
                                                         .CompanyNM = AppUtility.GetDBString(i.CompanyNM),
                                                         .SurveyCount = AppUtility.GetDBInteger(i.SurveyCount),
                                                         .UserCount = AppUtility.GetDBInteger(i.UserCount),
                                                         .SurveyResponseCount = AppUtility.GetDBInteger(i.SurveyResponseCount),
                                                         .MenuOrder = i.MenuOrder,
                                                         .ApplicationID = i.ApplicationID,
                                                         .ModifiedDT = i.ModifiedDT,
                                                         .ModifiedID = i.ModifiedID
                                                        }).ToList
        For Each myApp In myApplications
            myApp.ApplicationSurveyList.Clear()
            myApp.ApplicationSurveyList.AddRange(GetApplicationSurveysByApplicationID(myApp.ApplicationID))
            For i = 0 To myApp.ApplicationSurveyList.Count - 1 Step 1
                myApp.ApplicationSurveyList(i).SurveyResponseList.AddRange(GetApplicationSurveyResponseSummary(0, 10000, String.Format("where SurveyID={0} and ApplicationID={1}", myApp.ApplicationSurveyList(i).Survey.SurveyID, myApp.ApplicationID)))
            Next
            myApp.ApplicationUserList.Clear()
            myApp.ApplicationUserList.AddRange(GetApplicationUserRolesByApplicationID(myApp.ApplicationID))
        Next

        Return myApplications
    End Function
    Function GetApplicationByApplicationID(reqApplicaitonID As Integer) As ApplicationItem
        Try
            Dim myApplication = (From i In Application_SelectRow(reqApplicaitonID)
                                 Select New ApplicationItem With {.ApplicationCD = i.ApplicationCD,
                                                                  .ApplicationDS = i.ApplicationDS,
                                                                  .ApplicationNM = i.ApplicationNM,
                                                                  .ApplicationShortNM = i.ApplicationShortNM,
                                                                  .ApplicationTypeID = AppUtility.GetDBInteger(i.ApplicationTypeID),
                                                                  .MenuOrder = i.MenuOrder,
                                                                  .CompanyID = AppUtility.GetDBInteger(i.CompanyID),
                                                                  .CompanyNM = AppUtility.GetDBString(i.CompanyNM),
                                                                  .ApplicationID = i.ApplicationID,
                                                                  .ApplicationFolder = i.ApplicationFolder,
                                                                  .DefaultAppPage = AppUtility.GetDBInteger(i.DefaultPageID),
                                                                  .ModifiedDT = i.ModifiedDT,
                                                                  .ModifiedID = i.ModifiedID
                                                                 }).Single
            With myApplication
                .ApplicationSurveyList = GetApplicationSurveysByApplicationID(reqApplicaitonID)
                For i = 0 To .ApplicationSurveyList.Count - 1 Step 1
                    .ApplicationSurveyList(i).SurveyResponseList.AddRange(GetApplicationSurveyResponseSummary(0, 10000, String.Format("where SurveyID={0} and ApplicationID={1}", .ApplicationSurveyList(i).Survey.SurveyID, .ApplicationID)))
                Next
                .SurveyCount = .ApplicationSurveyList.Count
                .ApplicationUserList = GetApplicationUserRolesByApplicationID(reqApplicaitonID)
                .UserCount = .ApplicationUserList.Count

                .Navigation.AddRange((From x In SiteAppMenus Where x.SiteAppID = .ApplicationID
                                           Select New NavigationMenuItem With {.Id = x.Id,
                                                                        .GlyphName = x.GlyphName,
                                                                        .MenuOrder = x.MenuOrder,
                                                                        .MenuText = x.MenuText,
                                                                        .SiteAppID = x.SiteAppID,
                                                                        .SiteRoleID = x.SiteRoleID,
                                                                        .TartgetPage = x.TartgetPage,
                                                                        .ViewInMenu = x.ViewInMenu
                                                                       }).ToArray)
                .Properties.AddRange((From x In AppProperties Where x.SiteAppID = .ApplicationID
                          Select New PropertyItem With {.Id = x.Id,
                                                        .SiteAppID = x.SiteAppID,
                                                        .Key = x.Key,
                                                        .Value = x.Value}).ToArray)
            End With



            Return myApplication
        Catch ex As Exception
            Return New ApplicationItem With {.ApplicationID = -1}
        End Try
    End Function
    Function DeleteApplication(thisApplicationas As ApplicationItem) As Boolean
        Dim delAppliation = GetApplicationByApplicationID(thisApplicationas.ApplicationID)
        bReturn = False

        If delAppliation.ApplicationSurveyList.Count = 0 AndAlso delAppliation.ApplicationUserList.Count = 0 Then
            For Each myNavItem In delAppliation.Navigation
                DeleteNavigationMenuItem(myNavItem)
            Next

            For Each myAppProperety In delAppliation.Properties
                DeleteProperty(myAppProperety.SiteAppID, myAppProperety.Key)
            Next
            Dim iResult As Integer = Application_DeleteRow(thisApplicationas.ApplicationID)
            If iResult = 0 Then
                bReturn = True
            Else
                bReturn = False
                ApplicationLogging.SQLDeleteError(String.Format("DataController.DeleteApplication - ApplicationID={1} ({0})", thisApplicationas.ApplicationID, thisApplicationas.ApplicationNM), String.Format("gsp_Application_Delete did not return 1 as expected return: {0}", iResult))
            End If
        End If
        Return bReturn
    End Function
    Public Function PutApplicationItem(ByVal thisApplication As ApplicationItem) As ApplicationItem
        Try
            If thisApplication.ApplicationID > 0 Then
                With thisApplication
                    Dim myReturn = Application_Update(.ApplicationID,
                                               .ApplicationNM,
                                               .ApplicationCD,
                                               .ApplicationShortNM,
                                               .ApplicationTypeID,
                                               .ApplicationDS,
                                               .MenuOrder,
                                               .ModifiedID,
                                               Now(),
                                               .ApplicationFolder,
                                               .DefaultAppPage,
                                               .CompanyID).SingleOrDefault
                End With
            Else
                With thisApplication
                    Dim myReturn = Application_Insert(.ApplicationNM,
                                                      .ApplicationCD,
                                                      .ApplicationShortNM,
                                                      .ApplicationTypeID,
                                                      .ApplicationDS,
                                                      .MenuOrder,
                                                      .ModifiedID,
                                                      Now(),
                                                      .ApplicationFolder,
                                                      .DefaultAppPage,
                                                      .CompanyID).SingleOrDefault
                    thisApplication.ApplicationID = myReturn.ApplicationID
                End With
            End If
        Catch ex As Data.SqlClient.SqlException
            AppLog.SQLExceptionLog("DataController.UpdateApplicationItem", ex)
        End Try
        Return GetApplicationByApplicationID(thisApplication.ApplicationID)
    End Function
    Public Function GetApplicationListByApplicationUserID(ByVal reqApplicationUserID As Integer) As List(Of ApplicationItem)
        Dim myList As New List(Of ApplicationItem)
        For Each i In (From z In ApplicationUserRoles Where z.ApplicationUserID = reqApplicationUserID Select z.Application).ToList()
            Dim myApp As New ApplicationItem() With {.ApplicationID = i.ApplicationID,
                                                     .ApplicationDS = i.ApplicationDS,
                                                     .ApplicationFolder = i.ApplicationFolder,
                                                     .ApplicationNM = i.ApplicationNM,
                                                     .DefaultAppPage = i.DefaultPageID}
            myApp.ApplicationUserList.AddRange((From x In i.ApplicationUserRoles.ToList()
                                            Select New ApplicationUserRoleItem With {.ApplicationUserRoleID = x.ApplicationUserRoleID,
                                                                                     .ApplicationID = x.ApplicationID,
                                                                                     .ApplicationUserID = x.ApplicationUserID,
                                                                                     .IsDemo = AppUtility.GetDBBoolean(x.IsDemo),
                                                                                     .isMonthlyPrice = AppUtility.GetDBBoolean(x.IsMonthlyPrice),
                                                                                     .Price = AppUtility.GetDBDecimal(x.Price),
                                                                                     .StartupDate = AppUtility.GetDBDate(x.StartUpDate),
                                                                                     .ApplicationNM = i.ApplicationNM,
                                                                                     .ApplicationDS = i.ApplicationDS,
                                                                                     .IsUserAdmin = AppUtility.GetDBBoolean(x.IsUserAdmin),
                                                                                     .UserInroled = AppUtility.GetDBBoolean(x.UserInRolled)}).ToList())
            myApp.Navigation.AddRange((From x In i.SiteAppMenus.ToList()
                                       Select New NavigationMenuItem With {.Id = x.Id,
                                                                    .GlyphName = x.GlyphName,
                                                                    .MenuOrder = x.MenuOrder,
                                                                    .MenuText = x.MenuText,
                                                                    .SiteAppID = x.SiteAppID,
                                                                    .SiteRoleID = x.SiteRoleID,
                                                                    .TartgetPage = x.TartgetPage,
                                                                    .ViewInMenu = x.ViewInMenu
                                                                   }).ToList())
            myApp.Properties.AddRange((From x In i.AppProperties.ToList() Select New PropertyItem With {.Id = x.Id,
                                                                                                        .SiteAppID = x.SiteAppID,
                                                                                                        .Key = x.Key,
                                                                                                        .Value = x.Value}).ToList())
            myList.Add(myApp)
        Next
        Return myList
    End Function
    Public Function SetDefaultNavigationItem(ByVal reqSiteApp As ApplicationItem, ByVal NavMenuItemID As Integer) As ApplicationItem
        Try
            Dim myApp = (From i In Applications Where i.ApplicationID = reqSiteApp.ApplicationID).SingleOrDefault
            If (From i In myApp.SiteAppMenus Where i.Id = NavMenuItemID Select i).ToList().Count > 0 Then
                myApp.DefaultPageID = NavMenuItemID
                SubmitChanges()
            End If
            reqSiteApp = GetApplicationByApplicationID(reqSiteApp.ApplicationID)
        Catch ex As Exception
            bReturn = False
        End Try
        Return reqSiteApp
    End Function
    'Public Function MakeAppavailable(userID As Integer, appid As Integer) As Boolean
    '    bReturn = False
    '    Try
    '        Dim myapprelation As New ApplicationUserRole() With {.IsDemo = True,
    '                                                         .IsMonthlyPrice = True,
    '                                                         .Price = 0,
    '                                                         .ApplicationID = appid,
    '                                                         .ApplicationUserID = userID,
    '                                                         .StartUpDate = Now,
    '                                                         .UserInRolled = False}
    '        ApplicationUserRoles.InsertOnSubmit(myapprelation)
    '        SubmitChanges()
    '        bReturn = True
    '    Catch ex As Exception
    '        bReturn = False
    '    End Try
    '    Return bReturn
    'End Function
    Public Function SubscribeMeToApp(userid As Integer, appid As Integer) As Boolean
        bReturn = False
        Try
            Dim myapprelation = (From i In ApplicationUserRoles Where i.ApplicationUserID = userid And i.ApplicationID = appid).Single
            With myapprelation
                .IsDemo = True
                .IsMonthlyPrice = True
                .Price = 0
                .ApplicationID = appid
                .ApplicationUserID = userid
                .StartUpDate = Now
                .UserInRolled = True
            End With
            SubmitChanges()
            bReturn = True
        Catch ex As Exception
            bReturn = False
        End Try
        Return bReturn
    End Function
    Public Function CloneSiteApp(ByVal curAppID As Integer, ByVal newAppName As String) As ApplicationItem
        Dim NewOnlineSiteApp As New ApplicationItem
        If curAppID > 0 Then
            Dim fromapp = (From i In Applications Where i.ApplicationID = curAppID).Single
            ' Generate new SiteApp , populate , and save 
            Dim toapp As New Application
            With toapp
                .ApplicationNM = newAppName.Trim
                .ApplicationDS = fromapp.ApplicationDS
                .ApplicationFolder = fromapp.ApplicationFolder
            End With
            ' submit siteApp
            Applications.InsertOnSubmit(toapp)
            Try
                SubmitChanges()
            Catch ex As Exception
                ' Log Error
            End Try
            ' clone menu items and set app default page  
            For Each i In fromapp.SiteAppMenus
                Dim newmenuitem As New SiteAppMenu
                With newmenuitem
                    .SiteAppID = toapp.ApplicationID
                    .MenuText = i.MenuText
                    .TartgetPage = i.TartgetPage
                    .GlyphName = i.GlyphName
                    .MenuOrder = i.MenuOrder
                    .SiteRoleID = i.SiteRoleID
                    .ViewInMenu = i.ViewInMenu
                End With
                SiteAppMenus.InsertOnSubmit(newmenuitem)
                SubmitChanges()
                If i.Id = fromapp.DefaultPageID Then
                    toapp.DefaultPageID = newmenuitem.Id
                    SubmitChanges()
                End If
            Next
            ' Clone appProperty
            For Each i In fromapp.AppProperties
                Dim newproperty As New AppProperty
                With newproperty
                    .SiteAppID = toapp.ApplicationID
                    .Key = i.Key
                    .Value = i.Value
                End With
                AppProperties.InsertOnSubmit(newproperty)
                SubmitChanges()
            Next
            NewOnlineSiteApp = GetApplicationByApplicationID(toapp.ApplicationID)
        End If
        Return NewOnlineSiteApp
    End Function

#End Region
#Region "ApplicationChart Methods"
    Function GetApplicationChartList() As List(Of ApplicationChartItem)
        Return (From i In ChartSetting_SelectAll()
                Select New ApplicationChartItem With {.ApplicationChartId = i.Id,
                                                  .DateCreated = AppUtility.GetDBDate(i.DateCreated),
                                                  .LastUpdated = AppUtility.GetDBDate(i.LastUpdated),
                                                  .SettingName = i.SettingName,
                                                  .SettingType = i.SettingType,
                                                  .SettingValue = i.SettingValue,
                                                  .SettingValueEnhanced = i.SettingValueEnhanced,
                                                  .SiteAppID = AppUtility.GetDBInteger(i.SiteAppID),
                                                  .SiteUserID = AppUtility.GetDBInteger(i.SiteUserID)}).ToList()
    End Function
    Function GetApplicaitonChartByApplicationChartID(ByVal ApplicationChartID As Integer) As ApplicationChartItem
        Return (From i In ChartSetting_SelectRow(ApplicationChartID)
                Select New ApplicationChartItem With {.ApplicationChartId = i.Id,
                                                  .DateCreated = AppUtility.GetDBDate(i.DateCreated),
                                                  .LastUpdated = AppUtility.GetDBDate(i.LastUpdated),
                                                  .SettingName = i.SettingName,
                                                  .SettingType = i.SettingType,
                                                  .SettingValue = i.SettingValue,
                                                  .SettingValueEnhanced = i.SettingValueEnhanced,
                                                  .SiteAppID = AppUtility.GetDBInteger(i.SiteAppID),
                                                  .SiteUserID = AppUtility.GetDBInteger(i.SiteUserID)}).SingleOrDefault

    End Function
    Function PutApplicationChart(ByVal Chart As ApplicationChartItem) As ApplicationChartItem
        With Chart
            If .ApplicationChartId > 0 Then
                Try
                    Dim Result = ChartSetting_Update(.ApplicationChartId,
                                                     .SiteUserID,
                                                     .SiteAppID,
                                                     .SettingType,
                                                     .SettingName,
                                                     .SettingValue,
                                                     .SettingValueEnhanced,
                                                     .DateCreated,
                                                     Now()).SingleOrDefault
                Catch ex As Exception
                    AppLog.SQLError(ex.ToString, "DataController.PutApplicationChart-Update")
                    bReturn = False
                End Try
            Else
                Try
                    Dim Result = ChartSetting_Insert(.SiteUserID,
                                                     .SiteAppID,
                                                     .SettingType,
                                                     .SettingName,
                                                     .SettingValue,
                                                     .SettingValueEnhanced,
                                                     Now(),
                                                     Now()).SingleOrDefault
                    .ApplicationChartId = Result.Id
                Catch ex As Exception
                    AppLog.SQLError(ex.ToString, "DataController.PutApplicationChart-Update")
                    bReturn = False
                End Try
            End If
            Return GetApplicaitonChartByApplicationChartID(.ApplicationChartId)
        End With
    End Function
    Public Function DeleteApplicationChart(ByVal Chart As ApplicationChartItem) As Boolean
        If ChartSetting_DeleteRow(Chart.ApplicationChartId) = 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function
#End Region
#Region "Site App Menu"
    Public Function GetNavigationMenuList() As List(Of NavigationMenuItem)
        Dim myList As New List(Of NavigationMenuItem)
        For Each i In SiteAppMenus.ToList()
            Dim newMenuItem As New NavigationMenuItem With {
                .Id = i.Id,
                .GlyphName = i.GlyphName,
                .MenuOrder = i.MenuOrder,
                .MenuText = i.MenuText,
                .SiteAppID = i.SiteAppID,
                .SiteRoleID = i.SiteRoleID,
                .TartgetPage = i.TartgetPage,
                .IsSelected = False,
                .Css = " "}
            myList.Add(newMenuItem)
        Next
        Return myList
    End Function
    Public Function GetNavigationMenuItem(ByVal MenuID As Integer) As NavigationMenuItem
        Dim myList As New List(Of NavigationMenuItem)
        Dim newMenuItem As New NavigationMenuItem
        For Each i In (From x In SiteAppMenus Where x.Id = MenuID).ToList()
            newMenuItem = New NavigationMenuItem With {
                .Id = i.Id,
                .GlyphName = i.GlyphName,
                .MenuOrder = i.MenuOrder,
                .MenuText = i.MenuText,
                .SiteAppID = i.SiteAppID,
                .SiteRoleID = i.SiteRoleID,
                .TartgetPage = i.TartgetPage,
                .IsSelected = False,
                .Css = " "}
            Exit For
        Next
        Return newMenuItem
    End Function
    Public Function PutNavigationMenuItem(ByVal thisMenuItem As NavigationMenuItem) As NavigationMenuItem
        Dim NewPageDef As New SiteAppMenu

        With NewPageDef
            .MenuOrder = CInt(thisMenuItem.MenuOrder)
            .MenuText = thisMenuItem.MenuText
            .SiteAppID = CInt(thisMenuItem.SiteAppID)
            .TartgetPage = thisMenuItem.TartgetPage
            .SiteRoleID = CInt(thisMenuItem.SiteRoleID)
            .GlyphName = thisMenuItem.GlyphName
            .ViewInMenu = CBool(thisMenuItem.ViewInMenu)
        End With

        If thisMenuItem.Id < 1 Then
            SiteAppMenus.InsertOnSubmit(NewPageDef)
            SubmitChanges()
            thisMenuItem.Id = NewPageDef.Id
        Else
            SubmitChanges()
        End If
        SiteAppMenus.InsertOnSubmit(NewPageDef)
        SubmitChanges()
        thisMenuItem.Id = NewPageDef.Id
        Return thisMenuItem
    End Function
    Public Function DeleteNavigationMenuItem(ByVal thisMenuItem As NavigationMenuItem) As Boolean
        bReturn = False
        Try
            Dim mypagedef = (From i In SiteAppMenus Where i.Id = thisMenuItem.Id).Single
            SiteAppMenus.DeleteOnSubmit(mypagedef)
            SubmitChanges()
            bReturn = True
        Catch ex As Exception
            bReturn = False
        End Try
        Return bReturn
    End Function

#End Region
#Region "Site User Roles"
    Public Function GetSiteRoles() As List(Of SiteRoleItem)
        Dim myList As New List(Of SiteRoleItem)

        For Each mySiteRole In SiteRoles
            ' .SiteUsers = New List(Of ApplicationUserItem)(),
            Dim myRole As New SiteRoleItem() With {.Id = mySiteRole.Id, .Active = mySiteRole.Active, .RoleName = mySiteRole.RoleName}
            'myRole.SiteUsers.AddRange((From i In mySiteRole.ApplicationUsers.ToList()
            '                           Select New ApplicationUserItem With {.ApplicationUserID = i.ApplicationUserID,
            '                                          .UserLogin = i.UserLogin,
            '                                          .DisplayName = i.DisplayName,
            '                                          .FirstNM = i.FirstNM,
            '                                          .LastNM = i.LastNM,
            '                                          .eMailAddress = i.eMailAddress,
            '                                          .LastLoginDT = AppUtility.GetDBDate(i.LastLoginDT),
            '                                          .AccountNM = i.AccountNM,
            '                                          .ModifiedDT = AppUtility.GetDBDate(i.ModifiedDT),
            '                                          .HasMessages = False,
            '                                          .MessageCount = i.UserMessages.Count(),
            '                                          .UserRoleID = i.RoleID,
            '                                          .UserRoleName = i.SiteRole.RoleName}).ToList())
            myList.Add(myRole)
        Next
        Return myList
    End Function
#End Region
#Region "Site App Properties"
    Public Function GetProperty(ByVal AppID As Integer, ByVal PropertyKey As String) As PropertyItem
        Dim myPropertyItem As New PropertyItem()
        myPropertyItem = (From i In AppProperty_SelectByAppIDKey(AppID, PropertyKey)
                          Where i.SiteAppID = AppID And i.Key = PropertyKey
                          Select New PropertyItem With {.Id = i.Id,
                                                        .Key = i.Key,
                                                        .SiteAppID = i.SiteAppID,
                                                        .Value = i.Value}).SingleOrDefault()
        If IsNothing(myPropertyItem) Then
            myPropertyItem = New PropertyItem with {.Id=-1,.Key = PropertyKey, .SiteAppID=AppID,.Value=String.Empty}
        End If
        Return myPropertyItem
    End Function
    Public Function PutProperty(ByVal myProperty As PropertyItem) As PropertyItem
        Dim ReturnProperty = GetProperty(myProperty.SiteAppID, myProperty.Key)
        With myProperty
            If ReturnProperty.Id > 0 Then
                Dim ReturnVal = AppProperty_Update(ReturnProperty.Id,
                                                   ReturnProperty.SiteAppID,
                                                   ReturnProperty.Key,
                                                   .Value).SingleOrDefault()
            Else
                Dim ReturnVal = AppProperty_Insert(.SiteAppID,
                                                  .Key,
                                                  .Value).SingleOrDefault()
                .Id = ReturnVal.Id
            End If
        End With
        Return myProperty
    End Function
    Public Function SetProperty(AppID As Integer, PropertyKey As String, value As String) As Boolean
        Try
            Dim myprop = (From i In AppProperties Where i.SiteAppID = AppID And i.Key = PropertyKey).ToList()
            If myprop.Count = 1 Then
                myprop(0).Value = (value)
            Else
                Dim newprop As New AppProperty() With {.Key = PropertyKey, .Value = value, .SiteAppID = AppID}
                AppProperties.InsertOnSubmit(newprop)
            End If
            SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function DeleteProperty(AppID As Integer, propertyKey As String) As Boolean
        Try
            Dim myprop = (From i In AppProperties Where i.SiteAppID = AppID And i.Key = propertyKey).ToList
            If myprop.Count = 1 Then
                AppProperties.DeleteOnSubmit(myprop(0))
            End If
            SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region
#Region "Site Message "
    Public Function GetSiteMessages() As List(Of SiteMessageItem)
        Dim myList As New List(Of SiteMessageItem)
        For Each i In UserMessages.ToList()
            Dim myApp As New SiteMessageItem() With {.Id = i.Id,
                                                     .AppID = i.AppID,
                                                     .CratedDateTime = i.CratedDateTime,
                                                     .Deleted = i.Deleted,
                                                     .FromUserID = i.FromUserID,
                                                     .Message = i.Message,
                                                     .Opened = i.Opened,
                                                     .ShowonPage = i.ShowonPage,
                                                     .Subject = i.Subject,
                                                     .ToUserID = i.ToUserID
                                                     }
            myList.Add(myApp)
        Next
        Return myList
    End Function
    Public Function GetSiteMessageById(ByVal SiteMessageID As Integer) As SiteMessageItem
        Dim mySiteMessage As SiteMessageItem = New SiteMessageItem With {.Id = -1}
        Dim myUserMessage As UserMessage = (From i In UserMessages Where i.Id = SiteMessageID).SingleOrDefault
        If myUserMessage.Id > 0 Then
            With mySiteMessage
                .Id = myUserMessage.Id
                .AppID = myUserMessage.AppID
                .CratedDateTime = myUserMessage.CratedDateTime
                .Deleted = myUserMessage.Deleted
                .FromUserID = myUserMessage.FromUserID
                .Message = myUserMessage.Message
                .Opened = myUserMessage.Opened
                .ShowonPage = myUserMessage.ShowonPage
                .Subject = myUserMessage.Subject
                .ToUserID = myUserMessage.ToUserID
            End With
        End If
        Return mySiteMessage
    End Function
    Public Function PutSiteMessage(ByVal myMessage As SiteMessageItem) As SiteMessageItem
        If myMessage.Id > 0 Then
            'Update
            Dim myUserMessage = (From i In UserMessages Where i.Id = myMessage.Id).SingleOrDefault
            With myUserMessage
                .AppID = myMessage.AppID
                .CratedDateTime = myMessage.CratedDateTime
                .Deleted = myMessage.Deleted
                .FromUserID = myMessage.FromUserID
                .Message = myMessage.Message
                .Opened = myMessage.Opened
                .ShowonPage = myMessage.ShowonPage
                .Subject = myMessage.Subject
                .ToUserID = myMessage.ToUserID
            End With
            SubmitChanges()
            Return GetSiteMessageById(myMessage.Id)
        Else
            'Insert
            With myMessage
                Dim myReturn = UserMessages_Insert(.ToUserID,
                                                   .FromUserID,
                                                   .Message,
                                                   .Opened,
                                                   .CratedDateTime,
                                                   .Subject,
                                                   .Deleted,
                                                   .AppID,
                                                   .ShowonPage,
                                                   .FromApp).Single
                .Id = myReturn.Id
            End With
            Return GetSiteMessageById(myMessage.Id)
        End If
    End Function
    Public Function GetUserSentMessages(userID As Integer) As List(Of SiteMessageItem)
        Dim myList As New List(Of SiteMessageItem)
        For Each i In (From x In UserMessages Where x.FromUserID = userID).ToList()
            Dim myApp As New SiteMessageItem() With {.Id = i.Id,
                                                     .AppID = i.AppID,
                                                     .CratedDateTime = i.CratedDateTime,
                                                     .Deleted = i.Deleted,
                                                     .FromUserID = i.FromUserID,
                                                     .Message = i.Message,
                                                     .Opened = i.Opened,
                                                     .ShowonPage = i.ShowonPage,
                                                     .Subject = i.Subject,
                                                     .ToUserID = i.ToUserID
                                                     }
            myList.Add(myApp)
        Next
        Return myList
    End Function
    Public Function UserMessageOpened(myMessage As SiteMessageItem) As SiteMessageItem
        Dim myUserMessage = (From i In UserMessages Where i.Id = myMessage.Id).SingleOrDefault
        With myUserMessage
            .Opened = True
        End With
        SubmitChanges()
        Return GetSiteMessageById(myMessage.Id)
    End Function
    Public Function DeleteMessage(MyMessage As SiteMessageItem) As Boolean
        Try
            Dim myUserMessage = (From i In UserMessages Where i.Id = MyMessage.Id).SingleOrDefault
            UserMessages.DeleteOnSubmit(myUserMessage)
            SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetRelatedUsers(userid As Integer) As List(Of ApplicationUserItem)
        Dim mycoll As New List(Of ApplicationUserItem)
        Dim myUser = GetApplicationUserByApplicationUserID(userid)
        Dim templist As New List(Of ApplicationUserItem)
        For Each App In myUser.ApplicationUserRoleList
            templist.AddRange(GetApplicationUserListByApplicationID(App.ApplicationID).ToArray())
        Next
        mycoll.AddRange(From i In templist Distinct)
        Return mycoll
    End Function
#End Region
#Region "Company Methods"
    Function GetCompanyList() As List(Of CompanyItem)
        Return (From i In Company_SelectAll()
                Select New CompanyItem With {.CompanyID = i.CompanyID,
                                             .CompanyCD = i.CompanyCD,
                                             .CompanyNM = i.CompanyNM,
                                             .CompanyDS = i.CompanyDS,
                                             .Title = i.Title,
                                             .SiteURL = i.SiteURL,
                                             .DefaultSiteTheme = i.DefaultTheme,
                                             .SiteTheme = i.Theme,
                                             .Address1 = i.Address1,
                                             .Address2 = i.Address2,
                                             .City = i.City,
                                             .State = i.State,
                                             .Country = i.Country,
                                             .PostalCode = i.PostalCode,
                                             .Active = i.ActiveFL,
                                             .Component = i.Component,
                                             .SMTP = i.SMTP,
                                             .FromEmail = i.FromEmail,
                                             .GalleryFolder = i.GalleryFolder,
                                             .ModifiedID = i.ModifiedID,
                                             .ModifiedDT = i.ModifiedDT,
                                             .UserCount = AppUtility.GetDBInteger(i.UserCount),
                                             .SurveyResponseCount = AppUtility.GetDBInteger(i.SurveyResponseCount),
                                             .ProjectCount = AppUtility.GetDBInteger(i.ApplicationCount)}).ToList()
    End Function
    Function GetCompanyByCompanyID(ByVal CompanyID As Integer) As CompanyItem
        Dim myCompany = (From i In Company_SelectRow(CompanyID)
                Select New CompanyItem With {.CompanyID = i.CompanyID,
                                             .CompanyCD = i.CompanyCD,
                                             .CompanyNM = i.CompanyNM,
                                             .CompanyDS = i.CompanyDS,
                                             .Title = i.Title,
                                             .SiteURL = i.SiteURL,
                                             .DefaultSiteTheme = i.DefaultTheme,
                                             .SiteTheme = i.Theme,
                                             .Address1 = i.Address1,
                                             .Address2 = i.Address2,
                                             .City = i.City,
                                             .State = i.State,
                                             .Country = i.Country,
                                             .PostalCode = i.PostalCode,
                                             .Active = i.ActiveFL,
                                             .Component = i.Component,
                                             .SMTP = i.SMTP,
                                             .FromEmail = i.FromEmail,
                                             .GalleryFolder = i.GalleryFolder,
                                             .ModifiedID = i.ModifiedID,
                                             .ModifiedDT = i.ModifiedDT,
                                             .UserCount = AppUtility.GetDBInteger(i.UserCount),
                                             .SurveyResponseCount = AppUtility.GetDBInteger(i.SurveyResponseCount),
                                             .ProjectCount = AppUtility.GetDBInteger(i.ApplicationCount)}).SingleOrDefault

        myCompany.UserList.AddRange((From i In ApplicationUser_SelectByCompanyID(myCompany.CompanyID)
                            Select New ApplicationUserItem With {.AccountNM = i.AccountNM,
                                                                 .ApplicationUserID = i.ApplicationUserID,
                                                                 .CommentDS = i.CommentDS,
                                                                 .CompanyID = Utility.GetDBInteger(i.CompanyID),
                                                                 .CompanyNM = i.CompanyNM,
                                                                 .DisplayName = i.DisplayName,
                                                                 .eMailAddress = i.eMailAddress,
                                                                 .EmailVerified = Utility.GetDBBoolean(i.EmailVerified),
                                                                 .FirstNM = i.FirstNM,
                                                                 .LastLoginDT = Utility.GetDBDate(i.LastLoginDT),
                                                                 .LastLoginLocation = i.LastLoginLocation,
                                                                 .LastNM = i.LastNM,
                                                                 .ModifiedDT = i.ModifiedDT,
                                                                 .ModifiedID = i.ModifiedID,
                                                                 .SupervisorAccountNM = i.SupervisorAccountNM}).ToList())


        myCompany.ProjectList.AddRange((From i In Application_SelectByCompanyID(myCompany.CompanyID)
                                        Select New ApplicationItem With {.ApplicationID = i.ApplicationID,
                                                                        .ApplicationCD = i.ApplicationCD,
                                                                        .ApplicationDS = i.ApplicationDS,
                                                                        .ApplicationFolder = i.ApplicationFolder,
                                                                        .ApplicationNM = i.ApplicationNM,
                                                                        .ApplicationShortNM = i.ApplicationShortNM,
                                                                        .ApplicationTypeID = i.ApplicationTypeID,
                                                                        .ApplicationTypeNM = i.ApplicationTypeNM,
                                                                        .CompanyID = Utility.GetDBInteger(i.CompanyID),
                                                                        .CompanyNM = i.CompanyNM,
                                                                        .DefaultAppPage = i.DefaultPageID,
                                                                        .MenuOrder = i.MenuOrder,
                                                                        .SurveyCount = Utility.GetDBInteger(i.SurveyCount),
                                                                        .SurveyResponseCount = Utility.GetDBInteger(i.SurveyResponseCount),
                                                                        .UserCount = Utility.GetDBInteger(i.UserCount)}).ToList())

        Return myCompany

    End Function
    Function PutCompany(ByVal Company As CompanyItem) As CompanyItem

        With Company
            If .CompanyID > 0 Then
                Try
                    Dim Result = Company_Update(.CompanyID,
                                                .CompanyNM,
                                                .CompanyCD,
                                                .CompanyDS,
                                                .Title,
                                                .SiteTheme,
                                                .DefaultSiteTheme,
                                                .GalleryFolder,
                                                .SiteURL,
                                                .Address1,
                                                .Address2,
                                                .City,
                                                .State,
                                                .Country,
                                                .PostalCode,
                                                String.Empty,
                                                String.Empty,
                                                String.Empty,
                                                String.Empty,
                                                .Active,
                                                .Component,
                                                .FromEmail,
                                                .SMTP,
                                                .ModifiedDT,
                                                .ModifiedID).SingleOrDefault
                Catch ex As Exception
                    AppLog.SQLError(ex.ToString, "DataController.PutCompany-Update")
                    bReturn = False
                End Try
            Else
                Try
                    Dim Result = Company_Insert(.CompanyNM,
                                                .CompanyCD,
                                                .CompanyDS,
                                                .Title,
                                                .SiteTheme,
                                                .DefaultSiteTheme,
                                                .GalleryFolder,
                                                .SiteURL,
                                                .Address1,
                                                .Address2,
                                                .City,
                                                .State,
                                                .Country,
                                                .PostalCode,
                                                String.Empty,
                                                String.Empty,
                                                String.Empty,
                                                String.Empty,
                                                .Active,
                                                .Component,
                                                .FromEmail,
                                                .SMTP,
                                                .ModifiedDT,
                                                .ModifiedID).SingleOrDefault

                    .CompanyID = Result.CompanyID
                Catch ex As Exception
                    AppLog.SQLError(ex.ToString, "DataController.PutCompany-Insert")
                    bReturn = False
                End Try
            End If
        End With
        Return GetCompanyByCompanyID(Company.CompanyID)
    End Function
    Function DeleteCompany(ByVal Company As CompanyItem) As Boolean
        If Company_DeleteRow(Company.CompanyID) = 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function

#End Region
#Region "Application Survey Methods"
    Function GetApplicationSurveysByApplicationID(ByVal ApplicationID As Integer) As List(Of ApplicationSurveyItem)
        Dim myAppSurveyList = (From i In usp_Survey_SelectByApplicationID(ApplicationID).ToList()
                Select New ApplicationSurveyItem With {.ApplicationID = i.ApplicationID,
                                                       .ApplicationSurveyID = i.ApplicationSurveyID,
                                                       .DefaultRoleID = i.DefaultRoleID,
                                                       .Survey = New SurveyItem With {.SurveyID = i.SurveyID,
                                                                                      .SurveyType = New SurveyTypeItem With {.SurveyTypeID = i.SurveyTypeID},
                                                                                      .UseSurveyGroupsFL = i.UseQuestionGroupsFL,
                                                                                      .SurveyNM = i.SurveyNM,
                                                                                      .SurveyShortNM = i.SurveyShortNM,
                                                                                      .SurveyDS = i.SurveyDS,
                                                                                      .StartDT = i.StartDT,
                                                                                      .EndDT = i.EndDT,
                                                                                      .ParentSurveyID = i.ParentSurveyID,
                                                                                      .CompletionMessage = i.CompletionMessage,
                                                                                      .AutoAssignFilter = i.AutoAssignFilter,
                                                                                      .ReviewerAccountNM = i.ReviewerAccountNM,
                                                                                      .ResponseNMTemplate = i.ResponseNMTemplate}}).ToList()


        For Each myAppSurvey In myAppSurveyList
            If (myAppSurvey.Survey.QuestionGroupList Is Nothing) Then
                myAppSurvey.Survey.QuestionGroupList = New List(Of QuestionGroupItem)
            End If
            myAppSurvey.Survey.QuestionGroupList = GetQuestionGroupsBySurveyID(myAppSurvey.Survey.SurveyID)

            If (myAppSurvey.Survey.QuestionList Is Nothing) Then
                myAppSurvey.Survey.QuestionList = New List(Of QuestionItem)
            End If
            myAppSurvey.Survey.QuestionList = GetQuestionsBySurveyID(myAppSurvey.Survey.SurveyID)

            For Each myQuestion As QuestionItem In myAppSurvey.Survey.QuestionList
                If myQuestion.QuestionAnswerItemList Is Nothing Then
                    myQuestion.QuestionAnswerItemList = New List(Of QuestionAnswerItem)
                End If
                myQuestion.QuestionAnswerItemList.AddRange(GetQuestionAnswersByQuestionID(myQuestion.QuestionID))
            Next
            myAppSurvey.Survey.StatusList.Clear()
            myAppSurvey.Survey.StatusList = GetSurveyStatusItemList(myAppSurvey.Survey.SurveyID)

            myAppSurvey.Survey.ReviewStatusList.Clear()
            myAppSurvey.Survey.ReviewStatusList = GetReviewStatuses(myAppSurvey.Survey.SurveyID)

            myAppSurvey.Survey.EmailTemplateList.Clear()
            myAppSurvey.Survey.EmailTemplateList = GetSurveyEmailTemplates(myAppSurvey.Survey.SurveyID)
        Next
        Return myAppSurveyList
    End Function
    Function GetApplicationSurveys() As List(Of ApplicationSurveyItem)
        Return (From i In gsp_ApplicationSurvey_Select(Nothing)
                Select New ApplicationSurveyItem With {.ApplicationID = i.ApplicationID,
                                                       .ApplicationSurveyID = i.ApplicationSurveyID,
                                                       .Survey = New SurveyItem With {.SurveyID = i.SurveyID},
                                                       .DefaultRoleID = i.DefaultRoleID}).ToList()
    End Function
    Function DeleteApplicationSurvey(ByVal thisApplicationSurvey As ApplicationSurveyItem) As Boolean
        If gsp_ApplicationSurvey_Delete(thisApplicationSurvey.ApplicationSurveyID) = 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function
    Function UpateApplicationSurvey(ByVal thisApplicationSurvey As ApplicationSurveyItem) As ApplicationSurveyItem

        If thisApplicationSurvey.ApplicationSurveyID < 1 Then
            Dim checkApplicationSurvey = (From i In GetApplicationSurveysByApplicationID(thisApplicationSurvey.ApplicationID) Where i.Survey.SurveyID = thisApplicationSurvey.Survey.SurveyID).SingleOrDefault
            If Not IsNothing(checkApplicationSurvey) Then
                thisApplicationSurvey.ApplicationSurveyID = checkApplicationSurvey.ApplicationSurveyID
            End If
        End If

        If thisApplicationSurvey.ApplicationSurveyID < 1 Then
            Try
                Dim myResult = gsp_ApplicationSurvey_Insert(thisApplicationSurvey.ApplicationID,
                                             thisApplicationSurvey.Survey.SurveyID,
                                             thisApplicationSurvey.DefaultRoleID,
                                             9999,
                                             Now).Single()

                thisApplicationSurvey.ApplicationSurveyID = myResult.ApplicationSurveyID
                bReturn = True
            Catch ex As Exception
                bReturn = False
            End Try
        Else
            Try
                gsp_ApplicationSurvey_Update(thisApplicationSurvey.ApplicationSurveyID,
                                             thisApplicationSurvey.ApplicationID,
                                             thisApplicationSurvey.Survey.SurveyID,
                                             thisApplicationSurvey.DefaultRoleID,
                                             9999,
                                             Now)
                bReturn = True
            Catch ex As Exception
                bReturn = False
            End Try
        End If
        Return thisApplicationSurvey
    End Function
#End Region
#Region "Application User Role Methods"
    Function GetApplicationUserRolesByApplicationUserID(ByVal ApplicationUserID As Integer) As List(Of ApplicationUserRoleItem)
        Return (From i In ApplicationUserRole_SelectByApplicationUserID(ApplicationUserID)
                Select New ApplicationUserRoleItem With {.ApplicationUserRoleID = i.ApplicationUserRoleID,
                                                         .ApplicationID = i.ApplicationID,
                                                         .ApplicationUserID = i.ApplicationUserID,
                                                         .AccountNM = i.AccountNM,
                                                         .RoleID = i.RoleID,
                                                         .RoleCD = i.RoleCD,
                                                         .RoleNM = i.RoleNM,
                                                         .ReviewLevel = AppUtility.GetDBInteger(i.ReviewLevel),
                                                         .CanManage = AppUtility.GetDBBoolean(i.UpdateFL),
                                                         .CanRead = AppUtility.GetDBBoolean(i.ReadFL),
                                                         .CanCreate = AppUtility.GetDBBoolean(i.UpdateFL),
                                                         .CanDelete = AppUtility.GetDBBoolean(i.UpdateFL),
                                                         .FirstNM = i.FirstNM,
                                                         .LastNM = i.LastNM,
                                                         .eMailAddress = i.eMailAddress,
                                                         .CommentDS = i.CommentDS,
                                                         .LastLoginDT = AppUtility.GetDBDate(i.LastLoginDT),
                                                         .LastLoginLocation = i.LastLoginLocation,
                                                         .IsDemo = AppUtility.GetDBBoolean(i.IsDemo),
                                                         .isMonthlyPrice = AppUtility.GetDBBoolean(i.IsMonthlyPrice),
                                                         .Price = AppUtility.GetDBDecimal(i.Price),
                                                         .StartupDate = AppUtility.GetDBDate(i.StartUpDate),
                                                         .ApplicationNM = i.ApplicationNM,
                                                         .ApplicationDS = i.ApplicationDS,
                                                         .IsUserAdmin = AppUtility.GetDBBoolean(i.IsUserAdmin),
                                                         .UserInroled = AppUtility.GetDBBoolean(i.UserInRolled)
                                                        }).ToList()
    End Function
    Function GetApplicationUserRolesByApplicationID(ByVal ApplicationID As Integer) As List(Of ApplicationUserRoleItem)
        Return (From i In ApplicationUserRole_SelectByApplicationID(ApplicationID)
                Select New ApplicationUserRoleItem With {.ApplicationUserRoleID = i.ApplicationUserRoleID,
                                                         .ApplicationID = i.ApplicationID,
                                                         .ApplicationUserID = i.ApplicationUserID,
                                                         .AccountNM = i.AccountNM,
                                                         .RoleID = i.RoleID,
                                                         .RoleCD = i.RoleCD,
                                                         .RoleNM = i.RoleNM,
                                                         .ReviewLevel = AppUtility.GetDBInteger(i.ReviewLevel),
                                                         .CanManage = AppUtility.GetDBBoolean(i.UpdateFL),
                                                         .CanRead = AppUtility.GetDBBoolean(i.ReadFL),
                                                         .CanCreate = AppUtility.GetDBBoolean(i.UpdateFL),
                                                         .CanDelete = AppUtility.GetDBBoolean(i.UpdateFL),
                                                         .FirstNM = i.FirstNM,
                                                         .LastNM = i.LastNM,
                                                         .eMailAddress = i.eMailAddress,
                                                         .CommentDS = i.CommentDS,
                                                         .LastLoginDT = AppUtility.GetDBDate(i.LastLoginDT),
                                                         .LastLoginLocation = i.LastLoginLocation,
                                                         .IsDemo = AppUtility.GetDBBoolean(i.IsDemo),
                                                         .isMonthlyPrice = AppUtility.GetDBBoolean(i.IsMonthlyPrice),
                                                         .Price = AppUtility.GetDBDecimal(i.Price),
                                                         .StartupDate = AppUtility.GetDBDate(i.StartUpDate),
                                                         .ApplicationNM = .ApplicationNM,
                                                         .ApplicationDS = .ApplicationDS,
                                                         .IsUserAdmin = AppUtility.GetDBBoolean(i.IsUserAdmin),
                                                         .UserInroled = AppUtility.GetDBBoolean(i.UserInRolled)
                                                        }).ToList()
    End Function
    Function GetApplicationUserRoles() As List(Of ApplicationUserRoleItem)
        Return (From i In ApplicationUserRole_SelectAll()
                Select New ApplicationUserRoleItem With {.ApplicationUserRoleID = i.ApplicationUserRoleID,
                                                         .ApplicationID = i.ApplicationID,
                                                         .RoleID = i.RoleID,
                                                         .ApplicationUserID = i.ApplicationUserID,
                                                        .ModifiedID = i.ModifiedID}).ToList()
    End Function
    Function DeleteApplicationUserRole(ByVal thisApplicationUserRole As ApplicationUserRoleItem) As Boolean
        '
        ' Lookup ApplicationUserROleID from existing records
        '
        For Each myRole In ApplicationUserRole_SelectByApplicationID(thisApplicationUserRole.ApplicationID)
            If myRole.ApplicationUserID = thisApplicationUserRole.ApplicationUserID Then
                thisApplicationUserRole.ApplicationUserRoleID = myRole.ApplicationUserRoleID
                Exit For
            End If
        Next

        If ApplicationUserRole_DeleteRow(thisApplicationUserRole.ApplicationUserRoleID) = 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function
    Function UpdateApplicationUserRole(ByVal thisApplicationuserRole As ApplicationUserRoleItem, ByRef sReturn As String) As ApplicationUserRoleItem
        sReturn = String.Empty
        '
        ' Force and UPDATE if this relationship exists already
        '
        For Each myRole In ApplicationUserRole_SelectByApplicationID(thisApplicationuserRole.ApplicationID)
            If myRole.ApplicationUserID = thisApplicationuserRole.ApplicationUserID Then
                thisApplicationuserRole.ApplicationUserRoleID = myRole.ApplicationUserRoleID
                Exit For
            End If
        Next
        If thisApplicationuserRole.ApplicationUserRoleID < 1 Then
            sReturn = ""
            Try
                With thisApplicationuserRole
                    Dim myResult = ApplicationUserRole_Insert(.ApplicationID,
                                               .ApplicationUserID,
                                               .RoleID,
                                               .ModifiedID,
                                               Now,
                                               .IsDemo,
                                               .StartupDate,
                                               .isMonthlyPrice,
                                               .Price,
                                               .UserInroled,
                                               .IsUserAdmin).SingleOrDefault
                    .ApplicationUserRoleID = myResult.ApplicationUserRoleID
                End With
            Catch ex As Exception
                sReturn = String.Format("DataController.UpdateApplicationUserRole-Insert - {0} - {1}", thisApplicationuserRole.ApplicationUserID, ex.ToString)
                AppLog.ErrorLog("Insert Failed:" & sReturn, "Service.PutApplicationUserRole")
            End Try
        Else
            Try
                With thisApplicationuserRole
                    ApplicationUserRole_Update(.ApplicationUserRoleID,
                                               .ApplicationID,
                                               .ApplicationUserID,
                                               .RoleID,
                                               .ModifiedID,
                                               Now,
                                               .IsDemo,
                                               .StartupDate,
                                               .isMonthlyPrice,
                                               .Price,
                                               .UserInroled,
                                               .IsUserAdmin)
                End With
            Catch ex As Exception
                sReturn = String.Format("DataController.UpdateApplicationUserRole-Update - {0} - {1}", thisApplicationuserRole.ApplicationUserID, ex.ToString)
                AppLog.ErrorLog("Update Failed:" & sReturn, "Service.PutApplicationUserRole")
            End Try
        End If
        Return thisApplicationuserRole
    End Function
#End Region
#Region "Question Methods"
    Function GetQuestions() As List(Of QuestionItem)
        Return (From i In Question_SelectAll() Select New QuestionItem With {.QuestionID = i.QuestionID,
                                                                             .SurveyTypeID = i.SurveyTypeID,
                                                                             .QuestionShortNM = i.QuestionShortNM,
                                                                             .QuestionNM = i.QuestionNM,
                                                                             .QuestionDS = i.QuestionDS,
                                                                             .Keywords = AppUtility.GetDBString(i.Keywords),
                                                                             .QuestionSort = i.QuestionSort,
                                                                             .ReviewRoleLevel = i.ReviewRoleLevel,
                                                                             .QuestionTypeID = i.QuestionTypeID,
                                                                             .CommentFL = i.CommentFL,
                                                                             .QuestionValue = AppUtility.GetDBDecimal(i.QuestionValue),
                                                                             .UnitOfMeasureID = AppUtility.GetDBInteger(i.UnitOfMeasureID, 0),
                                                                             .FileData = AppUtility.GetDBByte(i.FileData)}).ToList()
    End Function
    Function GetQuestionsBySurveyID(ByVal SurveyID As Integer) As List(Of QuestionItem)
        Return (From i In Question_SelectBySurveyID(SurveyID)
                Select New QuestionItem With {.QuestionID = i.QuestionID,
                                              .SurveyTypeID = i.SurveyTypeID,
                                              .QuestionShortNM = i.QuestionShortNM,
                                              .QuestionNM = i.QuestionNM,
                                              .QuestionDS = i.QuestionDS,
                                              .SurveyDisplayOrder = AppUtility.GetDBInteger(i.SurveyQuestionOrder),
                                              .QuestionSort = i.QuestionSort,
                                              .ReviewRoleLevel = i.ReviewRoleLevel,
                                              .QuestionTypeID = i.QuestionTypeID,
                                              .FileData = AppUtility.GetDBByte(i.FileData),
                                              .Keywords = AppUtility.GetDBString(i.Keywords),
                                              .CommentFL = i.CommentFL,
                                              .QuestionValue = AppUtility.GetDBDecimal(i.QuestionValue),
                                              .UnitOfMeasureID = AppUtility.GetDBInteger(i.UnitOfMeasureID),
                                              .QuestionGroupMember = New QuestionGroupMemberItem With {.QuestionGroupID = i.QuestionGroupID,
                                                                                                       .QuestionGroupMemberID = i.QuestionGroupMemberID,
                                                                                                       .DisplayOrder = i.DisplayOrder,
                                                                                                       .QuestionID = i.QuestionID,
                                                                                                       .QuestionGroupNM = i.QuestionGroupNM,
                                                                                                       .QuestionGroupShortNM = i.QuestionGroupShortNM,
                                                                                                       .QuestionWeight = i.QuestionWeight},
                                              .AnswerDataType = i.AnswerDataType,
                                              .MaxQuestionValue = CType(i.MaxQuestionValue, Integer),
                                              .QuestionTypeCD = i.QuestionTypeCD}).ToList()
    End Function
    Function GetQuestionByQuestionID(ByVal QuestionID As Integer) As QuestionItem
        Dim myQuestionItem As New QuestionItem
        Try
            myQuestionItem = (From i In Question_SelectRow(QuestionID)
                              Select New QuestionItem With {.QuestionID = i.QuestionID,
                                                            .QuestionShortNM = i.QuestionShortNM,
                                                            .SurveyTypeID = i.SurveyTypeID,
                                                            .QuestionNM = i.QuestionNM,
                                                            .QuestionDS = i.QuestionDS,
                                                            .QuestionSort = i.QuestionSort,
                                                            .ReviewRoleLevel = i.ReviewRoleLevel,
                                                            .QuestionTypeID = i.QuestionTypeID,
                                                            .FileData = AppUtility.GetDBByte(i.FileData),
                                                            .Keywords = AppUtility.GetDBString(i.Keywords),
                                                            .CommentFL = i.CommentFL,
                                                            .QuestionGroupMember = New QuestionGroupMemberItem With {.QuestionID = i.QuestionID,
                                                                                                                     .QuestionNM = i.QuestionNM,
                                                                                                                     .DisplayOrder = i.QuestionSort},
                                                            .QuestionValue = CType(i.QuestionValue, Decimal),
                                                            .UnitOfMeasureID = CType(i.UnitOfMeasureID, Integer)}).Single

            myQuestionItem.QuestionAnswerItemList = (From i In usp_QuestionAnswer_SelectByQuestionID(myQuestionItem.QuestionID)
                                                     Order By i.QuestionAnswerSort Ascending
                                                               Select New QuestionAnswerItem With {.QuestionAnswerActiveFL = i.ActiveFL,
                                                                                                   .QuestionAnswerCommentFL = i.CommentFL,
                                                                                                   .QuestionAnswerDS = i.QuestionAnswerDS,
                                                                                                   .QuestionAnswerID = i.QuestionAnswerID,
                                                                                                   .QuestionAnswerNM = i.QuestionAnswerNM,
                                                                                                   .QuestionAnswerShortNM = i.QuestionAnswerShortNM,
                                                                                                   .QuestionAnswerSort = i.QuestionAnswerSort,
                                                                                                   .QuestionAnswerValue = i.QuestionAnswerValue,
                                                                                                   .QuestionID = i.QuestionID}).ToList()

            For Each mySurvey In usp_Survey_SelectByQuestionID(myQuestionItem.QuestionID)
                myQuestionItem.SurveyLookupList.Add(New LookupItem With {.Value = mySurvey.SurveyID.ToString, .Name = mySurvey.SurveyNM})
            Next

        Catch ex As Exception
            AppLog.SQLError(ex.ToString, String.Format("DataController.GetQuestionByQuestionID QuestionID={0}", QuestionID))
            myQuestionItem.QuestionID = -1
        End Try

        Return myQuestionItem
    End Function
    Function GetQuestionByQuestionShortNM(ByVal QuestionShortNM As String) As QuestionItem
        Dim myQuestionItem As New QuestionItem With {.QuestionID = -1}
        Try
            myQuestionItem = (From i In Question_SelectByQuestionShortNM(QuestionShortNM)
                              Select New QuestionItem With {.QuestionID = i.QuestionID,
                                                            .QuestionShortNM = i.QuestionShortNM,
                                                            .SurveyTypeID = i.SurveyTypeID,
                                                            .QuestionNM = i.QuestionNM,
                                                            .QuestionDS = i.QuestionDS,
                                                            .QuestionSort = i.QuestionSort,
                                                            .ReviewRoleLevel = i.ReviewRoleLevel,
                                                            .QuestionTypeID = i.QuestionTypeID,
                                              .FileData = AppUtility.GetDBByte(i.FileData),
                                              .Keywords = AppUtility.GetDBString(i.Keywords),
                                                            .CommentFL = i.CommentFL,
                                                            .QuestionGroupMember = New QuestionGroupMemberItem With {.QuestionID = i.QuestionID,
                                                                                                                     .QuestionNM = i.QuestionNM,
                                                                                                                     .DisplayOrder = i.QuestionSort},
                                                            .QuestionValue = CType(i.QuestionValue, Decimal),
                                                            .UnitOfMeasureID = CType(i.UnitOfMeasureID, Integer)}).SingleOrDefault

            If myQuestionItem Is Nothing Then
                myQuestionItem = New QuestionItem With {.QuestionID = -1}
            Else
                myQuestionItem.QuestionAnswerItemList = (From i In usp_QuestionAnswer_SelectByQuestionID(myQuestionItem.QuestionID)
                                                         Order By i.QuestionAnswerSort Ascending
                                                         Select New QuestionAnswerItem With {.QuestionAnswerActiveFL = i.ActiveFL,
                                                                                             .QuestionAnswerCommentFL = i.CommentFL,
                                                                                             .QuestionAnswerDS = i.QuestionAnswerDS,
                                                                                             .QuestionAnswerID = i.QuestionAnswerID,
                                                                                             .QuestionAnswerNM = i.QuestionAnswerNM,
                                                                                             .QuestionAnswerShortNM = i.QuestionAnswerShortNM,
                                                                                             .QuestionAnswerSort = i.QuestionAnswerSort,
                                                                                             .QuestionAnswerValue = i.QuestionAnswerValue,
                                                                                             .QuestionID = i.QuestionID}).ToList()
            End If
        Catch ex As Exception
            AppLog.SQLError(ex.ToString, String.Format("DataController.GetQuestionByQuestionShortNM QuestionShortNM={0}", QuestionShortNM))
            myQuestionItem.QuestionID = -1
        End Try
        Return myQuestionItem
    End Function
    Function DeleteQuestion(ByVal thisQuestionItem As QuestionItem) As Boolean
        bReturn = False
        Dim Filters As New SQLFilterList
        Filters.Add(New SQLFilterClause With {.Argument = thisQuestionItem.QuestionID.ToString(),
                                              .Conjunction = SQLFilterConjunction.andConjunction,
                                              .Field = "QuestionID",
                                              .FieldOperator = SQLFilterOperator.Equal,
                                              .FieldType = "Question"})
        Dim TestQuestion = (From i In vwQuestionLibraries.Where(Filters.GetLINQWhere) Select i).ToList()
        If TestQuestion.Count = 1 Then
            If TestQuestion(0).ResponseAnswerCount = 0 And TestQuestion(0).SurveyCount = 0 Then
                Try
                    For Each answer In GetQuestionByQuestionID(thisQuestionItem.QuestionID).QuestionAnswerItemList
                        DeleteQuestionAnswer(answer)
                    Next
                    If Question_DeleteRow(thisQuestionItem.QuestionID) = 0 Then
                        bReturn = True
                    Else
                        bReturn = False
                    End If
                Catch ex As Exception
                    bReturn = False
                    AppLog.SQLError(ex.ToString, String.Format("DataController.DeleteQuestion QuestionID={0}", thisQuestionItem.QuestionID))
                End Try
            End If
        End If
        Return bReturn
    End Function
    Function PutQuestion(ByVal thisQuestionItem As QuestionItem) As QuestionItem
        If thisQuestionItem.UnitOfMeasureID < 1 Then
            thisQuestionItem.UnitOfMeasureID = 1
        End If

        If thisQuestionItem.QuestionID < 1 Then
            '
            ' Calculate Short Name and Order based on SurveyTypeID
            '
            Dim mySurveyType As SurveyTypeItem = GetSurveyTypeBySurveyTypeID(thisQuestionItem.SurveyTypeID)
            thisQuestionItem.QuestionSort = mySurveyType.QuestionCount + 1
            thisQuestionItem.QuestionShortNM = mySurveyType.SurveyTypeShortNM & (mySurveyType.QuestionCount + 1).ToString

            ' Check for duplicate QuestionShortNM which is the unique business key for the Question Table
            ' If the question already exists, just return that question from the database as is
            Dim checkQuestion As QuestionItem = GetQuestionByQuestionShortNM(thisQuestionItem.QuestionShortNM)
            If checkQuestion.QuestionID > 0 Then
                With thisQuestionItem
                    .QuestionID = checkQuestion.QuestionID
                    .QuestionNM = checkQuestion.QuestionNM
                    .QuestionDS = checkQuestion.QuestionDS
                    .QuestionShortNM = checkQuestion.QuestionShortNM
                    .SurveyTypeID = checkQuestion.SurveyTypeID
                    .QuestionSort = checkQuestion.QuestionSort
                    .ReviewRoleLevel = checkQuestion.ReviewRoleLevel
                    .QuestionTypeCD = checkQuestion.QuestionTypeCD
                    .CommentFL = checkQuestion.CommentFL
                    .QuestionValue = checkQuestion.QuestionValue
                    .UnitOfMeasureID = checkQuestion.UnitOfMeasureID
                    .QuestionGroupMember.QuestionID = checkQuestion.QuestionID
                    .QuestionGroupMember.QuestionNM = checkQuestion.QuestionNM
                    .FileData = AppUtility.GetDBByte(checkQuestion.FileData)
                    .QuestionAnswerItemList.Clear()
                    For Each QA As QuestionAnswerItem In checkQuestion.QuestionAnswerItemList
                        .QuestionAnswerItemList.Add(QA)
                    Next
                End With
            Else
                Try
                    Dim myInsertReturn = Question_Insert(
                        thisQuestionItem.SurveyTypeID,
                        thisQuestionItem.QuestionShortNM,
                        thisQuestionItem.QuestionNM,
                        thisQuestionItem.QuestionDS,
                        thisQuestionItem.Keywords,
                        thisQuestionItem.QuestionSort,
                        thisQuestionItem.ReviewRoleLevel,
                        thisQuestionItem.QuestionTypeID,
                        thisQuestionItem.CommentFL,
                        AppUtility.GetDBInteger(thisQuestionItem.QuestionValue),
                        thisQuestionItem.UnitOfMeasureID,
                        thisQuestionItem.ModifiedID,
                        Now,
                        AppUtility.GetDBByte(thisQuestionItem.FileData)).SingleOrDefault
                    thisQuestionItem.QuestionID = myInsertReturn.QuestionID
                    If thisQuestionItem.QuestionAnswerItemList.Count > 0 Then
                        For Each QA As QuestionAnswerItem In thisQuestionItem.QuestionAnswerItemList
                            QA.QuestionID = thisQuestionItem.QuestionID
                            QA.QuestionAnswerID = UpdateQuestionAnswer(QA).QuestionAnswerID
                        Next
                    End If
                Catch ex As Exception
                    AppLog.SQLError(ex.ToString, String.Format("DataController.UpdateQuestion-Insert QuestionShortNM={0}", thisQuestionItem.QuestionShortNM))
                End Try
            End If
        Else
            Try
                Dim myUpdateReturn = Question_Update(thisQuestionItem.QuestionID,
                                      thisQuestionItem.SurveyTypeID,
                                      thisQuestionItem.QuestionShortNM,
                                      thisQuestionItem.QuestionNM,
                                      thisQuestionItem.QuestionDS,
                                      thisQuestionItem.Keywords,
                                      thisQuestionItem.QuestionSort,
                                      thisQuestionItem.ReviewRoleLevel,
                                      thisQuestionItem.QuestionTypeID,
                                      thisQuestionItem.CommentFL,
                                      AppUtility.GetDBInteger(thisQuestionItem.QuestionValue),
                                      thisQuestionItem.UnitOfMeasureID,
                                      thisQuestionItem.ModifiedID,
                                      Now,
                                      AppUtility.GetDBByte(thisQuestionItem.FileData)).SingleOrDefault

                If thisQuestionItem.QuestionAnswerItemList.Count > 0 Then
                    For Each QA As QuestionAnswerItem In thisQuestionItem.QuestionAnswerItemList
                        QA.QuestionID = thisQuestionItem.QuestionID
                        If QA.MarkedForDeletion Then
                            DeleteQuestionAnswer(QA)
                        Else
                            QA.QuestionAnswerID = UpdateQuestionAnswer(QA).QuestionAnswerID
                        End If
                    Next
                End If
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, String.Format("DataController.UpdateQuestion-Update QuestionShortNM={0}", thisQuestionItem.QuestionShortNM))
            End Try
        End If
        Return GetQuestionByQuestionID(thisQuestionItem.QuestionID)
    End Function
#End Region
#Region "Question Answer Methods"
    Function GetQuestionAnswers() As List(Of QuestionAnswerItem)
        Return (From i In gsp_QuestionAnswer_Select(Nothing) Select New QuestionAnswerItem With {.QuestionAnswerID = i.QuestionAnswerID,
                                                                              .QuestionID = i.QuestionID,
                                                                              .QuestionAnswerNM = i.QuestionAnswerNM,
                                                                              .QuestionAnswerShortNM = i.QuestionAnswerShortNM,
                                                                              .QuestionAnswerValue = i.QuestionAnswerValue,
                                                                              .QuestionAnswerDS = i.QuestionAnswerDS,
                                                                              .QuestionAnswerSort = i.QuestionAnswerSort,
                                                                              .QuestionAnswerCommentFL = i.CommentFL,
                                                                              .QuestionAnswerActiveFL = i.ActiveFL}).ToList()
    End Function
    Function GetQuestionAnswersByQuestionID(ByVal QuestionID As Integer) As List(Of QuestionAnswerItem)
        Return (From i In usp_QuestionAnswer_SelectByQuestionID(QuestionID) Order By i.QuestionAnswerSort Ascending
            Select New QuestionAnswerItem With {.QuestionAnswerID = i.QuestionAnswerID,
                                                .QuestionID = i.QuestionID,
                                                .QuestionAnswerNM = i.QuestionAnswerNM,
                                                .QuestionAnswerShortNM = i.QuestionAnswerShortNM,
                                                .QuestionAnswerValue = i.QuestionAnswerValue,
                                                .QuestionAnswerDS = i.QuestionAnswerDS,
                                                .QuestionAnswerSort = i.QuestionAnswerSort,
                                                .QuestionAnswerCommentFL = i.CommentFL,
                                                .QuestionAnswerActiveFL = i.ActiveFL}).ToList()
    End Function
    Function GetQuestionAnswerByQuestionAnswerID(ByVal QuestionAnswerID As Integer) As QuestionAnswerItem
        Return (From i In gsp_QuestionAnswer_Select(QuestionAnswerID) Select New QuestionAnswerItem With {.QuestionAnswerID = i.QuestionAnswerID,
                                                                                                          .QuestionID = i.QuestionID,
                                                                                                          .QuestionAnswerNM = i.QuestionAnswerNM,
                                                                                                          .QuestionAnswerShortNM = i.QuestionAnswerShortNM,
                                                                                                          .QuestionAnswerValue = i.QuestionAnswerValue,
                                                                                                          .QuestionAnswerDS = i.QuestionAnswerDS,
                                                                                                          .QuestionAnswerSort = i.QuestionAnswerSort,
                                                                                                          .QuestionAnswerCommentFL = i.CommentFL,
                                                                                                          .QuestionAnswerActiveFL = i.ActiveFL}).Single
    End Function
    Function DeleteQuestionAnswer(ByVal thisQuestionAnswerItem As QuestionAnswerItem) As Boolean
        If gsp_QuestionAnswer_Delete(thisQuestionAnswerItem.QuestionAnswerID) = 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function
    Function UpdateQuestionAnswer(ByVal thisQuestionAnswerItem As QuestionAnswerItem) As QuestionAnswerItem
        If String.IsNullOrEmpty(thisQuestionAnswerItem.QuestionAnswerDS) Then
            thisQuestionAnswerItem.QuestionAnswerDS = thisQuestionAnswerItem.QuestionAnswerNM
        End If
        If thisQuestionAnswerItem.QuestionAnswerID < 1 Then
            Try
                Dim InsertResults = gsp_QuestionAnswer_Insert(thisQuestionAnswerItem.QuestionID,
                                          thisQuestionAnswerItem.QuestionAnswerShortNM,
                                          thisQuestionAnswerItem.QuestionAnswerNM,
                                          CType(thisQuestionAnswerItem.QuestionAnswerValue, Integer?),
                                          thisQuestionAnswerItem.QuestionAnswerDS,
                                          thisQuestionAnswerItem.QuestionAnswerSort,
                                          thisQuestionAnswerItem.QuestionAnswerCommentFL,
                                          thisQuestionAnswerItem.QuestionAnswerActiveFL,
                                          1,
                                          Now).Single
                thisQuestionAnswerItem.QuestionAnswerID = InsertResults.QuestionAnswerID
                bReturn = True
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, "DataController.UpdateQuestionAnswer-Insert")
                bReturn = False
            End Try
        Else
            Try
                gsp_QuestionAnswer_Update(thisQuestionAnswerItem.QuestionAnswerID,
                                          thisQuestionAnswerItem.QuestionID,
                                          thisQuestionAnswerItem.QuestionAnswerShortNM,
                                          thisQuestionAnswerItem.QuestionAnswerNM,
                                          CType(thisQuestionAnswerItem.QuestionAnswerValue, Integer?),
                                          thisQuestionAnswerItem.QuestionAnswerDS,
                                          thisQuestionAnswerItem.QuestionAnswerSort,
                                          thisQuestionAnswerItem.QuestionAnswerCommentFL,
                                          thisQuestionAnswerItem.QuestionAnswerActiveFL,
                                          1,
                                          Now)
                bReturn = True
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, "DataController.UpdateQuestionAnswer-Update")
                bReturn = False
            End Try
        End If
        Return thisQuestionAnswerItem
    End Function
#End Region
#Region "Question Group Methods"
    Function GetQuestionGroups() As List(Of QuestionGroupItem)
        Try
            Return (From i In gsp_QuestionGroup_Select(Nothing) Select New QuestionGroupItem With {.QuestionGroupID = i.QuestionGroupID,
                                                                                                   .SurveyID = CType(i.SurveyID, Integer),
                                                                                                   .QuestionGroupOrder = i.GroupOrder,
                                                                                                   .QuestionGroupShortNM = i.QuestionGroupShortNM,
                                                                                                   .QuestionGroupNM = i.QuestionGroupNM,
                                                                                                   .QuestionGroupDS = i.QuestionGroupDS,
                                                                                                   .QuestionGroupWeight = i.QuestionGroupWeight,
                                                                                                   .QuestionGroupHeader = i.GroupHeader,
                                                                                                   .QuestionGroupFooter = i.GroupFooter,
                                                                                                   .DependentQuestionGroupID = i.DependentQuestionGroupID,
                                                                                                   .DependentMinScore = i.DependentMinScore,
                                                                                                   .DependentMaxScore = i.DependentMaxScore}).ToList()
        Catch ex As Exception
            AppLog.SQLError(ex.ToString, "DataController.GetQuestionGroupsBySurveyID")
            Return New List(Of QuestionGroupItem)
        End Try
    End Function
    Function GetQuestionGroupsBySurveyID(ByVal SurveyID As Integer) As List(Of QuestionGroupItem)
        Dim myList As New List(Of QuestionGroupItem)
        Try
            For Each myGroup As QuestionGroupItem In (From i In usp_QuestionGroup_SelectBySurveyID(SurveyID) Order By i.GroupOrder Ascending
                Select New QuestionGroupItem With {.QuestionGroupID = i.QuestionGroupID,
                                                   .SurveyID = CType(i.SurveyID, Integer),
                                                   .QuestionGroupOrder = i.GroupOrder,
                                                   .QuestionGroupShortNM = i.QuestionGroupShortNM,
                                                   .QuestionGroupNM = i.QuestionGroupNM,
                                                   .QuestionGroupDS = i.QuestionGroupDS,
                                                   .QuestionGroupWeight = i.QuestionGroupWeight,
                                                   .QuestionGroupHeader = i.GroupHeader,
                                                   .QuestionGroupFooter = i.GroupFooter,
                                                   .DependentQuestionGroupID = i.DependentQuestionGroupID,
                                                   .DependentMinScore = i.DependentMinScore,
                                                   .DependentMaxScore = i.DependentMaxScore}).ToList()
                myGroup.QuestionMembership.AddRange((From i In usp_QuestionGroupMember_SelectByQuestionGroupID(myGroup.QuestionGroupID)
                        Select New QuestionGroupMemberItem With {.DisplayOrder = i.DisplayOrder,
                                                                 .QuestionGroupID = i.QuestionGroupID,
                                                                 .QuestionGroupMemberID = i.QuestionGroupMemberID,
                                                                 .QuestionGroupNM = i.QuestionGroupNM,
                                                                 .QuestionGroupShortNM = i.QuestionGroupShortNM,
                                                                 .QuestionID = i.QuestionID,
                                                                 .QuestionNM = i.QuestionNM,
                                                                 .QuestionShortNM = i.QuestionShortNM,
                                                                 .QuestionWeight = i.QuestionWeight}).ToList())

                myList.Add(myGroup)
            Next
        Catch ex As Exception
            AppLog.SQLError(ex.ToString, "DataController.GetQuestionGroupsBySurveyID")
            Return New List(Of QuestionGroupItem)
        End Try
        Return myList
    End Function
    Function GetQuestionGroupByQuestionGroupID(ByVal QuestionGroupID As Integer) As QuestionGroupItem
        Try
            Dim myGroup As QuestionGroupItem = (From i In gsp_QuestionGroup_Select(QuestionGroupID) Order By i.GroupOrder Ascending
                Select New QuestionGroupItem With {.QuestionGroupID = i.QuestionGroupID,
                                                   .SurveyID = CType(i.SurveyID, Integer),
                                                   .QuestionGroupOrder = i.GroupOrder,
                                                   .QuestionGroupShortNM = i.QuestionGroupShortNM,
                                                   .QuestionGroupNM = i.QuestionGroupNM,
                                                   .QuestionGroupDS = i.QuestionGroupDS,
                                                   .QuestionGroupWeight = i.QuestionGroupWeight,
                                                   .QuestionGroupHeader = i.GroupHeader,
                                                   .QuestionGroupFooter = i.GroupFooter,
                                                   .DependentQuestionGroupID = i.DependentQuestionGroupID,
                                                   .DependentMinScore = i.DependentMinScore,
                                                   .DependentMaxScore = i.DependentMaxScore}).Single
            myGroup.QuestionMembership.AddRange((From i In usp_QuestionGroupMember_SelectByQuestionGroupID(QuestionGroupID)
                    Select New QuestionGroupMemberItem With {.DisplayOrder = i.DisplayOrder,
                                                             .QuestionGroupID = i.QuestionGroupID,
                                                             .QuestionGroupMemberID = i.QuestionGroupMemberID,
                                                             .QuestionGroupNM = i.QuestionGroupNM,
                                                             .QuestionGroupShortNM = i.QuestionGroupShortNM,
                                                             .QuestionID = i.QuestionID,
                                                             .QuestionNM = i.QuestionNM,
                                                             .QuestionShortNM = i.QuestionShortNM,
                                                             .QuestionWeight = i.QuestionWeight}).ToList())

            Return myGroup
        Catch ex As Exception
            AppLog.SQLError(ex.ToString, "DataController.GetQuestionGroupByQuestionGroupID")
            Return New QuestionGroupItem
        End Try

    End Function
    Function DeleteQuestionGroup(ByVal thisQuestionGroupItem As QuestionGroupItem) As Boolean
        If gsp_QuestionGroup_Delete(thisQuestionGroupItem.QuestionGroupID) = 0 Then
            AppLog.AuditLog("DataController.DeleteQuestionGroup - Success", thisQuestionGroupItem.QuestionGroupID.ToString())
            bReturn = True
        Else
            AppLog.AuditLog("DataController.DeleteQuestionGroup - Failed", thisQuestionGroupItem.QuestionGroupID.ToString())
            bReturn = False
        End If
        Return bReturn
    End Function
    Function UpdateQuestionGroup(ByVal thisQuestionGroupItem As QuestionGroupItem) As QuestionGroupItem
        If thisQuestionGroupItem.QuestionGroupID < 1 Then
            Try
                Dim InsertResult = gsp_QuestionGroup_Insert(thisQuestionGroupItem.SurveyID,
                                         thisQuestionGroupItem.QuestionGroupOrder,
                                         thisQuestionGroupItem.QuestionGroupShortNM,
                                         thisQuestionGroupItem.QuestionGroupNM,
                                         thisQuestionGroupItem.QuestionGroupDS,
                                         thisQuestionGroupItem.QuestionGroupWeight,
                                         thisQuestionGroupItem.QuestionGroupHeader,
                                         thisQuestionGroupItem.QuestionGroupFooter,
                                         thisQuestionGroupItem.ModifiedID,
                                         Now,
                                         thisQuestionGroupItem.DependentQuestionGroupID,
                                         thisQuestionGroupItem.DependentMinScore,
                                         thisQuestionGroupItem.DependentMaxScore).Single

                thisQuestionGroupItem.QuestionGroupID = InsertResult.QuestionGroupID
                AppLog.AuditLog("DataController.thisQuestionGroupItem - Insert Survey Group", thisQuestionGroupItem.QuestionGroupID.ToString())
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, "DataController.UpdateQuestionGroup Insert")
            End Try
        Else
            Try
                gsp_QuestionGroup_Update(thisQuestionGroupItem.QuestionGroupID,
                                         thisQuestionGroupItem.SurveyID,
                                         thisQuestionGroupItem.QuestionGroupOrder,
                                         thisQuestionGroupItem.QuestionGroupShortNM,
                                         thisQuestionGroupItem.QuestionGroupNM,
                                         thisQuestionGroupItem.QuestionGroupDS,
                                         thisQuestionGroupItem.QuestionGroupWeight,
                                         thisQuestionGroupItem.QuestionGroupHeader,
                                         thisQuestionGroupItem.QuestionGroupFooter,
                                         thisQuestionGroupItem.ModifiedID,
                                         Now,
                                         thisQuestionGroupItem.DependentQuestionGroupID,
                                         thisQuestionGroupItem.DependentMinScore,
                                         thisQuestionGroupItem.DependentMaxScore)
                AppLog.AuditLog("DataController.thisQuestionGroupItem - Update Survey Group", thisQuestionGroupItem.QuestionGroupID.ToString())
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, "DataController.UpdateQuestionGroup Update")
            End Try
        End If

        For Each myMember In thisQuestionGroupItem.QuestionMembership
            myMember.QuestionGroupID = thisQuestionGroupItem.QuestionGroupID
            UpdateQuestionGroupMember(myMember)
        Next


        Return thisQuestionGroupItem
    End Function
#End Region
#Region "Question Group Memember Methods"
    Function GetQuestionGroupMembers() As List(Of QuestionGroupMemberItem)
        Return (From i In gsp_QuestionGroupMember_Select(Nothing)
                Select New QuestionGroupMemberItem With {.QuestionGroupMemberID = i.QuestionGroupMemberID,
                                                         .QuestionGroupID = i.QuestionGroupID,
                                                         .DisplayOrder = i.DisplayOrder,
                                                         .QuestionID = i.QuestionID,
                                                         .QuestionWeight = i.QuestionWeight}).ToList()
    End Function
    Function GetQuestionGroupMembers(ByVal QuestionGroupID As Integer) As List(Of QuestionGroupMemberItem)
        Return (From i In usp_QuestionGroupMember_SelectByQuestionGroupID(QuestionGroupID)
                Select New QuestionGroupMemberItem With {.QuestionGroupMemberID = i.QuestionGroupMemberID,
                                                         .QuestionGroupID = i.QuestionGroupID,
                                                         .DisplayOrder = i.DisplayOrder,
                                                         .QuestionID = i.QuestionID,
                                                         .QuestionNM = i.QuestionNM,
                                                         .QuestionShortNM = i.QuestionShortNM,
                                                         .QuestionWeight = i.QuestionWeight}).ToList()
    End Function

    Function DeleteQuestionGroupMember(ByVal QuestionGroupMember As QuestionGroupMemberItem) As Boolean
        If gsp_QuestionGroupMember_Delete(QuestionGroupMember.QuestionGroupMemberID) = 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function
    Function UpdateQuestionGroupMember(ByVal QuestionGroupMember As QuestionGroupMemberItem) As QuestionGroupMemberItem
        If QuestionGroupMember.QuestionGroupMemberID < 1 Then
            Try
                Dim InsertResults = gsp_QuestionGroupMember_Insert(QuestionGroupMember.QuestionGroupID,
                                               QuestionGroupMember.QuestionID,
                                               CType(QuestionGroupMember.QuestionWeight, Decimal?),
                                               QuestionGroupMember.DisplayOrder,
                                               QuestionGroupMember.ModifiedID,
                                               Now).Single
                QuestionGroupMember.QuestionGroupMemberID = InsertResults.QuestionGroupMemberID
                AppLog.AuditLog("DataController.UpdateQuestionGroupMember - Insert Survey Group Member", QuestionGroupMember.QuestionGroupMemberID.ToString())
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, "DataController.UpdateQuestionGroupMember-Insert")
            End Try
        Else
            Try
                If QuestionGroupMember.MarkedForDeletion = True Then
                    gsp_QuestionGroupMember_Delete(QuestionGroupMember.QuestionGroupMemberID)
                    QuestionGroupMember = New QuestionGroupMemberItem With {.QuestionGroupMemberID = -1}
                Else
                    gsp_QuestionGroupMember_Update(QuestionGroupMember.QuestionGroupMemberID,
                                                   QuestionGroupMember.QuestionGroupID,
                                                   QuestionGroupMember.QuestionID,
                                                   CType(QuestionGroupMember.QuestionWeight, Decimal?),
                                                   QuestionGroupMember.DisplayOrder,
                                                   QuestionGroupMember.ModifiedID,
                                                   Now)
                End If

                AppLog.AuditLog("DataController.UpdateQuestionGroupMember - Update Survey Group Member", QuestionGroupMember.QuestionGroupMemberID.ToString())
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, "DataController.UpdateQuestionGroupMember-Update")
            End Try
        End If
        Return QuestionGroupMember
    End Function
#End Region
#Region "Survey Type Methods"
    Function GetSurveyTypeList() As List(Of SurveyTypeItem)
        Return (From i In lu_SurveyType_SelectAll() Order By i.SurveyTypeNM
                Select New SurveyTypeItem With {.SurveyTypeID = i.SurveyTypeID,
                                                .SurveyTypeNM = i.SurveyTypeNM,
                                                .SurveyTypeShortNM = i.SurveyTypeShortNM,
                                                .SurveyTypeDS = i.SurveyTypeDS,
                                                .SurveyTypeComment = i.SurveyTypeComment,
                                                .ApplicationTypeID = AppUtility.GetDBInteger(i.ApplicationTypeID),
                                                .ApplicationTypeNM = AppUtility.GetDBString(i.ApplicationTypeNM),
                                                .ParentSurveyTypeID = AppUtility.GetDBInteger(i.ParentSurveyTypeID),
                                                .MultiSequence = i.MutiSequenceFL,
                                                .ModifiedDT = i.ModifiedDT,
                                                .ModifiedID = i.ModifiedID,
                                                .ParentSurveyTypeNM = AppUtility.GetDBString(i.ParentSurveyTypeNM),
                                                .LevelNumber = AppUtility.GetDBInteger(i.level),
                                                .TreeSort = AppUtility.GetDBString(i.TreeSort),
                                                .QuestionCount = AppUtility.GetDBInteger(i.QuestionCount),
                                                .SurveyCount = AppUtility.GetDBInteger(i.SurveyCount),
                                                .ChildCount = AppUtility.GetDBInteger(i.ChildCount)
                                               }).ToList
    End Function
    Function GetSurveyCategoryList() As List(Of SurveyTypeItem)
        Return (From i In lu_SurveyType_SelectAll() Where i.ParentSurveyTypeID = 0 Order By i.SurveyTypeNM
                Select New SurveyTypeItem With {.SurveyTypeID = i.SurveyTypeID,
                                                .SurveyTypeNM = i.SurveyTypeNM,
                                                .SurveyTypeShortNM = i.SurveyTypeShortNM,
                                                .SurveyTypeDS = i.SurveyTypeDS,
                                                .SurveyTypeComment = i.SurveyTypeComment,
                                                .ApplicationTypeID = AppUtility.GetDBInteger(i.ApplicationTypeID),
                                                .ApplicationTypeNM = AppUtility.GetDBString(i.ApplicationTypeNM),
                                                .ParentSurveyTypeID = AppUtility.GetDBInteger(i.ParentSurveyTypeID),
                                                .MultiSequence = i.MutiSequenceFL,
                                                .ModifiedDT = i.ModifiedDT,
                                                .ModifiedID = i.ModifiedID,
                                                .ParentSurveyTypeNM = AppUtility.GetDBString(i.ParentSurveyTypeNM),
                                                .LevelNumber = AppUtility.GetDBInteger(i.level),
                                                .TreeSort = AppUtility.GetDBString(i.TreeSort),
                                                .QuestionCount = AppUtility.GetDBInteger(i.QuestionCount),
                                                .SurveyCount = AppUtility.GetDBInteger(i.SurveyCount),
                                                .ChildCount = AppUtility.GetDBInteger(i.ChildCount)
                                               }).ToList
    End Function
    Function GetQuestionCategoryList() As List(Of SurveyTypeItem)
        Return (From i In lu_SurveyType_SelectAll() Where i.ParentSurveyTypeID > 0 Order By i.SurveyTypeNM
                Select New SurveyTypeItem With {.SurveyTypeID = i.SurveyTypeID,
                                                .SurveyTypeNM = i.SurveyTypeNM,
                                                .SurveyTypeShortNM = i.SurveyTypeShortNM,
                                                .SurveyTypeDS = i.SurveyTypeDS,
                                                .SurveyTypeComment = i.SurveyTypeComment,
                                                .ApplicationTypeID = AppUtility.GetDBInteger(i.ApplicationTypeID),
                                                .ApplicationTypeNM = AppUtility.GetDBString(i.ApplicationTypeNM),
                                                .ParentSurveyTypeID = AppUtility.GetDBInteger(i.ParentSurveyTypeID),
                                                .MultiSequence = i.MutiSequenceFL,
                                                .ModifiedDT = i.ModifiedDT,
                                                .ModifiedID = i.ModifiedID,
                                                .ParentSurveyTypeNM = AppUtility.GetDBString(i.ParentSurveyTypeNM),
                                                .LevelNumber = AppUtility.GetDBInteger(i.level),
                                                .TreeSort = AppUtility.GetDBString(i.TreeSort),
                                                .QuestionCount = AppUtility.GetDBInteger(i.QuestionCount),
                                                .SurveyCount = AppUtility.GetDBInteger(i.SurveyCount),
                                                .ChildCount = AppUtility.GetDBInteger(i.ChildCount)
                                               }).ToList
    End Function

    Function GetSurveyTypeBySurveyTypeID(ByVal pSurveyTypeID As Integer) As SurveyTypeItem
        Return (From i In lu_SurveyType_SelectRow(pSurveyTypeID)
                Select New SurveyTypeItem With {.SurveyTypeID = i.SurveyTypeID,
                                                .SurveyTypeNM = i.SurveyTypeNM,
                                                .SurveyTypeShortNM = i.SurveyTypeShortNM,
                                                .SurveyTypeDS = i.SurveyTypeDS,
                                                .SurveyTypeComment = i.SurveyTypeComment,
                                                .ApplicationTypeID = AppUtility.GetDBInteger(i.ApplicationTypeID),
                                                .ApplicationTypeNM = AppUtility.GetDBString(i.ApplicationTypeNM),
                                                .ParentSurveyTypeID = AppUtility.GetDBInteger(i.ParentSurveyTypeID),
                                                .MultiSequence = i.MutiSequenceFL,
                                                .ModifiedDT = i.ModifiedDT,
                                                .ModifiedID = i.ModifiedID,
                                                .ParentSurveyTypeNM = AppUtility.GetDBString(i.ParentSurveyTypeNM),
                                                .LevelNumber = AppUtility.GetDBInteger(i.level),
                                                .TreeSort = AppUtility.GetDBString(i.TreeSort),
                                                .QuestionCount = AppUtility.GetDBInteger(i.QuestionCount),
                                                .SurveyCount = AppUtility.GetDBInteger(i.SurveyCount),
                                               .ChildCount = AppUtility.GetDBInteger(i.ChildCount)
                                               }).SingleOrDefault

    End Function
    Function DeleteSurveyType(ByVal thisSurveyTypeItem As SurveyTypeItem) As Boolean
        If lu_SurveyType_DeleteRow(thisSurveyTypeItem.SurveyTypeID) = 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function
    Function UpdateSurveyType(ByVal thisSurveyTypeItem As SurveyTypeItem) As SurveyTypeItem
        With thisSurveyTypeItem
            If .SurveyTypeID < 1 Then
                Try
                    Dim ReturnVal = lu_SurveyType_Insert(.SurveyTypeShortNM,
                                           .SurveyTypeNM,
                                           .SurveyTypeDS,
                                           .SurveyTypeComment,
                                           .ApplicationTypeID,
                                           .ParentSurveyTypeID,
                                           .MultiSequence,
                                           .ModifiedID,
                                           Now()).SingleOrDefault
                    .SurveyTypeID = ReturnVal.SurveyTypeID
                    bReturn = True
                Catch ex As Exception
                    bReturn = False
                End Try
            Else
                Try
                    Dim ReturnVal = lu_SurveyType_Update(.SurveyTypeID,
                                                         .SurveyTypeShortNM,
                                                         .SurveyTypeNM,
                                                         .SurveyTypeDS,
                                                         .SurveyTypeComment,
                                                         .ApplicationTypeID,
                                                         .ParentSurveyTypeID,
                                                         .MultiSequence,
                                                         .ModifiedID,
                                                         Now()).SingleOrDefault
                    bReturn = True
                Catch ex As Exception
                    bReturn = False
                End Try
            End If
        End With
        Return GetSurveyTypeBySurveyTypeID(thisSurveyTypeItem.SurveyTypeID)
    End Function

#End Region
#Region "Application Type Methods"
    Function GetApplicationTypeList() As List(Of ApplicationTypeItem)
        Return (From i In lu_ApplicationType_SelectAll() Order By i.ApplicationTypeNM
                Select New ApplicationTypeItem With {.ApplicationTypeID = i.ApplicationTypeID,
                                                     .ApplicationTypeNM = i.ApplicationTypeNM,
                                                     .ApplicationTypeDS = i.ApplicationTypeDS,
                                                     .ModifiedID = i.ModifiedID,
                                                     .ModifiedDT = i.ModifiedDT,
                                                     .ApplicationCount = AppUtility.GetDBInteger(i.ApplicationCount),
                                                     .SurveyTypeCount = AppUtility.GetDBInteger(i.SurveyTypeCount)}).ToList

    End Function
    Function GetApplicationTypeByApplicationTypeID(ByVal pApplicationTypeID As Integer) As ApplicationTypeItem
        Return (From i In lu_ApplicationType_SelectRow(pApplicationTypeID)
                Select New ApplicationTypeItem With {.ApplicationTypeID = i.ApplicationTypeID,
                                                     .ApplicationTypeNM = i.ApplicationTypeNM,
                                                     .ApplicationTypeDS = i.ApplicationTypeDS,
                                                     .ModifiedID = i.ModifiedID,
                                                     .ModifiedDT = i.ModifiedDT,
                                                     .ApplicationCount = AppUtility.GetDBInteger(i.ApplicationCount),
                                                     .SurveyTypeCount = AppUtility.GetDBInteger(i.SurveyTypeCount)}).SingleOrDefault
    End Function
    Function DeleteApplicationType(ByVal thisApplicationType As ApplicationTypeItem) As Boolean
        If lu_ApplicationType_DeleteRow(thisApplicationType.ApplicationTypeID) = 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function
    Function UpdateApplicationType(ByVal thisApplicationType As ApplicationTypeItem) As ApplicationTypeItem
        With thisApplicationType
            If .ApplicationTypeID > 0 Then
                Try
                    Dim Result = lu_ApplicationType_Update(.ApplicationTypeID, .ApplicationTypeNM, .ApplicationTypeDS, .ModifiedID, Now).SingleOrDefault
                Catch ex As Exception
                    AppLog.SQLError(ex.ToString, "DataController.UpdateApplicationType-Update")
                    bReturn = False
                End Try
            Else
                Try
                    Dim Result = lu_ApplicationType_Insert(.ApplicationTypeNM, .ApplicationTypeDS, .ModifiedID, Now).SingleOrDefault
                    .ApplicationTypeID = Result.ApplicationTypeID
                Catch ex As Exception
                    AppLog.SQLError(ex.ToString, "DataController.UpdateApplicationType-Insert")
                    bReturn = False
                End Try
            End If
        End With
        Return GetApplicationTypeByApplicationTypeID(thisApplicationType.ApplicationTypeID)
    End Function
#End Region
#Region "Role Methods"
    Function GetRoles() As List(Of RoleItem)
        Return (From i In gsp_Role_Select(Nothing) Order By i.RoleNM Select New RoleItem With {.RoleID = i.RoleID,
                                                                                               .RoleNM = i.RoleNM,
                                                                                               .RoleCD = i.RoleCD,
                                                                                               .ReviewLevel = i.ReviewLevel,
                                                                                               .RoleDS = i.RoleDS}).ToList
    End Function
    Function GetRoleByRoleID(pRoleID As Integer) As RoleItem
        Return (From i In gsp_Role_Select(pRoleID)
                Select New RoleItem With {.RoleID = i.RoleID,
                                          .RoleCD = i.RoleCD,
                                          .RoleDS = i.RoleDS,
                                          .ReviewLevel = i.ReviewLevel,
                                          .RoleNM = i.RoleNM}).Single
    End Function
    Function DeleteRole(ByVal thisRoleItem As RoleItem) As Boolean
        If gsp_Role_Delete(thisRoleItem.RoleID) = 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function
    Function UpdateRole(ByVal thisRoleItem As RoleItem) As Boolean
        If thisRoleItem.RoleID < 1 Then
            Try
                gsp_Role_Insert(thisRoleItem.RoleCD,
                                thisRoleItem.RoleNM,
                                thisRoleItem.RoleDS,
                                thisRoleItem.ReviewLevel,
                                True,
                                True,
                                1, Now)
                bReturn = True
            Catch ex As Exception
                bReturn = False
            End Try
        Else
            Try
                gsp_Role_Update(thisRoleItem.RoleID,
                                thisRoleItem.RoleCD,
                                thisRoleItem.RoleNM,
                                thisRoleItem.RoleDS,
                                thisRoleItem.ReviewLevel,
                                True,
                                True,
                                1, Now)
                bReturn = True
            Catch ex As Exception
                'Debug.Write(ex.Message.ToString)
                bReturn = False
            End Try
        End If

        Return bReturn
    End Function
#End Region
#Region "Survey Methods"
    Function GetSurveySummary() As List(Of SurveyItem)
        Dim mySurveyList As List(Of SurveyItem) = (From i In usp_Survey_SelectSummary().ToList()
                                                   Select New SurveyItem With {.SurveyID = i.SurveyID,
                                                                               .SurveyType = New SurveyTypeItem With {.SurveyTypeID = i.SurveyTypeID,
                                                                                                                      .SurveyTypeNM = i.SurveyTypeNM,
                                                                                                                      .SurveyTypeShortNM = i.SurveyTypeShortNM},
                                                                               .UseSurveyGroupsFL = True,
                                                                               .SurveyNM = i.SurveyNM,
                                                                               .SurveyShortNM = i.SurveyShortNM,
                                                                               .SurveyDS = i.SurveyDS,
                                                                               .StartDT = i.StartDT,
                                                                               .EndDT = i.EndDT,
                                                                               .ParentSurveyID = i.ParentSurveyID,
                                                                               .CompletionMessage = i.CompletionMessage,
                                                                               .AutoAssignFilter = i.AutoAssignFilter,
                                                                               .ReviewerAccountNM = i.ReviewerAccountNM,
                                                                               .ResponseNMTemplate = i.ResponseNMTemplate,
                                                                               .ApplicationCount = CType(i.ApplicationCount, Integer),
                                                                               .QuestionCount = CType(i.QuestionCount, Integer),
                                                                               .QuestionGroupCount = CType(i.QuestionGroupCount, Integer),
                                                                               .SurveyResponseCount = CType(i.SurveyResponseCount, Integer)}).ToList()
        Return mySurveyList
    End Function
    Function GetSurveyList() As List(Of SurveyItem)
        Dim mySurveyList As List(Of SurveyItem) = (From i In gsp_Survey_Select(Nothing).ToList()
                                                   Select New SurveyItem With {.SurveyID = i.SurveyID,
                                                                               .SurveyType = New SurveyTypeItem With {.SurveyTypeID = i.SurveyTypeID},
                                                                               .UseSurveyGroupsFL = i.UseQuestionGroupsFL,
                                                                               .SurveyNM = i.SurveyNM,
                                                                               .SurveyShortNM = i.SurveyShortNM,
                                                                               .SurveyDS = i.SurveyDS,
                                                                               .StartDT = i.StartDT,
                                                                               .EndDT = i.EndDT,
                                                                               .ParentSurveyID = i.ParentSurveyID,
                                                                               .CompletionMessage = i.CompletionMessage,
                                                                               .AutoAssignFilter = i.AutoAssignFilter,
                                                                               .ReviewerAccountNM = i.ReviewerAccountNM,
                                                                               .ResponseNMTemplate = i.ResponseNMTemplate}).ToList()
        For Each mySurvey In mySurveyList
            If (mySurvey.QuestionGroupList Is Nothing) Then
                mySurvey.QuestionGroupList = New List(Of QuestionGroupItem)
            End If
            mySurvey.QuestionGroupList = GetQuestionGroupsBySurveyID(mySurvey.SurveyID)

            If (mySurvey.QuestionList Is Nothing) Then
                mySurvey.QuestionList = New List(Of QuestionItem)
            End If
            mySurvey.QuestionList = GetQuestionsBySurveyID(mySurvey.SurveyID)

            For Each myQuestion As QuestionItem In mySurvey.QuestionList
                If myQuestion.QuestionAnswerItemList Is Nothing Then
                    myQuestion.QuestionAnswerItemList = New List(Of QuestionAnswerItem)
                End If
                myQuestion.QuestionAnswerItemList.AddRange(GetQuestionAnswersByQuestionID(myQuestion.QuestionID))
            Next
            mySurvey.StatusList.Clear()
            mySurvey.StatusList = GetSurveyStatusItemList(mySurvey.SurveyID)

            mySurvey.ReviewStatusList.Clear()
            mySurvey.ReviewStatusList = GetReviewStatuses(mySurvey.SurveyID)

            mySurvey.EmailTemplateList.Clear()
            mySurvey.EmailTemplateList = GetSurveyEmailTemplates(mySurvey.SurveyID)
        Next
        Return mySurveyList
    End Function
    Function GetSurveysByApplicationID(ByVal ApplicationID As Integer) As List(Of SurveyItem)
        Dim mySurveyList As List(Of SurveyItem) = (From i In usp_Survey_SelectByApplicationID(ApplicationID).ToList()
                                                   Select New SurveyItem With {.SurveyID = i.SurveyID,
                                                                               .SurveyType = New SurveyTypeItem With {.SurveyTypeID = i.SurveyTypeID},
                                                                               .UseSurveyGroupsFL = i.UseQuestionGroupsFL,
                                                                               .SurveyNM = i.SurveyNM,
                                                                               .SurveyShortNM = i.SurveyShortNM,
                                                                               .SurveyDS = i.SurveyDS,
                                                                               .StartDT = i.StartDT,
                                                                               .EndDT = i.EndDT,
                                                                               .ParentSurveyID = i.ParentSurveyID,
                                                                               .CompletionMessage = i.CompletionMessage,
                                                                               .AutoAssignFilter = i.AutoAssignFilter,
                                                                               .ReviewerAccountNM = i.ReviewerAccountNM,
                                                                               .ResponseNMTemplate = i.ResponseNMTemplate}).ToList()

        For Each mySurvey In mySurveyList
            If (mySurvey.QuestionGroupList Is Nothing) Then
                mySurvey.QuestionGroupList = New List(Of QuestionGroupItem)
            End If
            mySurvey.QuestionGroupList = GetQuestionGroupsBySurveyID(mySurvey.SurveyID)

            If (mySurvey.QuestionList Is Nothing) Then
                mySurvey.QuestionList = New List(Of QuestionItem)
            End If
            mySurvey.QuestionList = GetQuestionsBySurveyID(mySurvey.SurveyID)

            For Each myQuestion As QuestionItem In mySurvey.QuestionList
                If myQuestion.QuestionAnswerItemList Is Nothing Then
                    myQuestion.QuestionAnswerItemList = New List(Of QuestionAnswerItem)
                End If
                myQuestion.QuestionAnswerItemList.AddRange(GetQuestionAnswersByQuestionID(myQuestion.QuestionID))
            Next
            mySurvey.StatusList.Clear()
            mySurvey.StatusList = GetSurveyStatusItemList(mySurvey.SurveyID)

            mySurvey.ReviewStatusList.Clear()
            mySurvey.ReviewStatusList = GetReviewStatuses(mySurvey.SurveyID)

            mySurvey.EmailTemplateList.Clear()
            mySurvey.EmailTemplateList = GetSurveyEmailTemplates(mySurvey.SurveyID)
        Next

        Return mySurveyList
    End Function
    Function GetSurveyBySurveyShortNM(ByRef Survey As SurveyItem) As SurveyItem
        Dim mySurvey As SurveyItem = New SurveyItem With {.SurveyID = -1}
        Try
            mySurvey = (From i In usp_Survey_SelectBySurveyShortNM(Survey.SurveyShortNM)
                            Select New SurveyItem With {.SurveyID = i.SurveyID,
                                                        .SurveyType = New SurveyTypeItem With {.SurveyTypeID = CType(i.SurveyTypeID, Integer)},
                                                        .UseSurveyGroupsFL = i.UseQuestionGroupsFL,
                                                        .SurveyNM = i.SurveyNM,
                                                        .SurveyShortNM = i.SurveyShortNM,
                                                        .SurveyDS = i.SurveyDS,
                                                        .EndDT = i.EndDT,
                                                        .StartDT = i.StartDT,
                                                        .AutoAssignFilter = i.AutoAssignFilter,
                                                        .ResponseNMTemplate = i.ResponseNMTemplate,
                                                        .ReviewerAccountNM = i.ReviewerAccountNM,
                                                        .CompletionMessage = i.CompletionMessage,
                                                        .ParentSurveyID = i.ParentSurveyID}).Single
            If mySurvey.SurveyID > 0 Then
                With Survey
                    .SurveyID = mySurvey.SurveyID
                    .SurveyType = mySurvey.SurveyType
                    .UseSurveyGroupsFL = mySurvey.UseSurveyGroupsFL
                    .SurveyNM = mySurvey.SurveyNM
                    .SurveyShortNM = mySurvey.SurveyShortNM
                    .SurveyDS = mySurvey.SurveyDS
                    .EndDT = mySurvey.EndDT
                    .StartDT = mySurvey.StartDT
                    .AutoAssignFilter = mySurvey.AutoAssignFilter
                    .ResponseNMTemplate = mySurvey.ResponseNMTemplate
                    .ReviewerAccountNM = mySurvey.ReviewerAccountNM
                    .CompletionMessage = mySurvey.CompletionMessage
                    .ParentSurveyID = mySurvey.ParentSurveyID

                    If (mySurvey.QuestionGroupList Is Nothing) Then
                        mySurvey.QuestionGroupList = New List(Of QuestionGroupItem)
                    End If
                    mySurvey.QuestionGroupList = GetQuestionGroupsBySurveyID(mySurvey.SurveyID)

                    If (mySurvey.QuestionList Is Nothing) Then
                        mySurvey.QuestionList = New List(Of QuestionItem)
                    End If
                    mySurvey.QuestionList = GetQuestionsBySurveyID(mySurvey.SurveyID)

                    For Each myQuestion As QuestionItem In mySurvey.QuestionList
                        If myQuestion.QuestionAnswerItemList Is Nothing Then
                            myQuestion.QuestionAnswerItemList = New List(Of QuestionAnswerItem)
                        End If
                        myQuestion.QuestionAnswerItemList.AddRange(GetQuestionAnswersByQuestionID(myQuestion.QuestionID))
                    Next
                    mySurvey.StatusList.Clear()
                    mySurvey.StatusList = GetSurveyStatusItemList(mySurvey.SurveyID)

                    mySurvey.ReviewStatusList.Clear()
                    mySurvey.ReviewStatusList = GetReviewStatuses(mySurvey.SurveyID)

                    mySurvey.EmailTemplateList.Clear()
                    mySurvey.EmailTemplateList = GetSurveyEmailTemplates(mySurvey.SurveyID)

                    'mySurvey.ApplicationSurveyRoleList.Clear()
                    'mySurvey.ApplicationSurveyRoleList = GetApplicationSurveyRolesBySurveyID(mySurvey.SurveyID)

                End With
            End If
        Catch ex As Exception
            Survey.SurveyID = -1
        End Try
        Return Survey
    End Function
    Function GetSurveyBySurveyID(ByVal SurveyID As Integer) As SurveyItem
        Dim mySurvey As SurveyItem
        Try
            mySurvey = (From i In gsp_Survey_Select(SurveyID)
                        Select New SurveyItem With {.SurveyID = i.SurveyID,
                                                    .SurveyType = New SurveyTypeItem With {.SurveyTypeID = CType(i.SurveyTypeID, Integer)},
                                                    .UseSurveyGroupsFL = i.UseQuestionGroupsFL,
                                                    .SurveyNM = i.SurveyNM,
                                                    .SurveyShortNM = i.SurveyShortNM,
                                                    .SurveyDS = i.SurveyDS,
                                                    .EndDT = i.EndDT,
                                                    .StartDT = i.StartDT,
                                                    .AutoAssignFilter = i.AutoAssignFilter,
                                                    .ResponseNMTemplate = i.ResponseNMTemplate,
                                                    .ReviewerAccountNM = i.ReviewerAccountNM,
                                                    .CompletionMessage = i.CompletionMessage,
                                                    .ParentSurveyID = i.ParentSurveyID}).Single

            If (mySurvey.QuestionGroupList Is Nothing) Then
                mySurvey.QuestionGroupList = New List(Of QuestionGroupItem)
            End If
            mySurvey.QuestionGroupList = GetQuestionGroupsBySurveyID(mySurvey.SurveyID)
            mySurvey.QuestionGroupCount = mySurvey.QuestionGroupList.Count

            If (mySurvey.QuestionList Is Nothing) Then
                mySurvey.QuestionList = New List(Of QuestionItem)
            End If
            mySurvey.QuestionList = GetQuestionsBySurveyID(mySurvey.SurveyID)
            mySurvey.QuestionCount = mySurvey.QuestionList.Count

            For Each myQuestion As QuestionItem In mySurvey.QuestionList
                If myQuestion.QuestionAnswerItemList Is Nothing Then
                    myQuestion.QuestionAnswerItemList = New List(Of QuestionAnswerItem)
                End If
                myQuestion.QuestionAnswerItemList.AddRange(GetQuestionAnswersByQuestionID(myQuestion.QuestionID))
            Next
            mySurvey.StatusList.Clear()
            mySurvey.StatusList = GetSurveyStatusItemList(mySurvey.SurveyID)

            mySurvey.ReviewStatusList.Clear()
            mySurvey.ReviewStatusList = GetReviewStatuses(mySurvey.SurveyID)

            mySurvey.EmailTemplateList.Clear()
            mySurvey.EmailTemplateList = GetSurveyEmailTemplates(mySurvey.SurveyID)

            mySurvey.ApplicationLookup = New List(Of LookupItem)
            For Each myApp In Application_SelectBySurveyID(mySurvey.SurveyID)
                mySurvey.ApplicationLookup.Add(New LookupItem With {.Name = myApp.ApplicationNM, .Value = myApp.ApplicationID.ToString})
            Next
            mySurvey.ApplicationCount = mySurvey.ApplicationLookup.Count
        Catch ex As Exception
            mySurvey = New SurveyItem
        End Try
        Return mySurvey
    End Function
    Function DeleteSurvey(ByVal thisSurveyItem As SurveyItem) As Boolean
        Dim mySurvey = (From i In GetSurveySummary() Where i.SurveyID = thisSurveyItem.SurveyID Select i).SingleOrDefault

        If mySurvey.ApplicationCount > 0 Then
            bReturn = False
        ElseIf mySurvey.SurveyResponseCount > 0 Then
            bReturn = False
        ElseIf mySurvey.QuestionGroupList.Count > 0 Then
            bReturn = False
        Else
            If usp_Survey_DeleteRelated(thisSurveyItem.SurveyID) = 1 Then
                bReturn = True
            Else
                bReturn = False
            End If
        End If

        Return bReturn
    End Function
    Function ImportSurvey(ByVal thisSurveyItem As SurveyItem, ByVal ApplicationID As Integer, ByVal DefaultRoleID As Integer) As SurveyItem
        ' Create a New Survey
        thisSurveyItem.SurveyID = -1
        Dim myResult As gsp_Survey_InsertResult = gsp_Survey_Insert(thisSurveyItem.SurveyType.SurveyTypeID,
                               thisSurveyItem.UseSurveyGroupsFL,
                               thisSurveyItem.SurveyNM,
                               thisSurveyItem.SurveyShortNM,
                               thisSurveyItem.SurveyDS,
                               thisSurveyItem.CompletionMessage,
                               thisSurveyItem.ResponseNMTemplate,
                               thisSurveyItem.ReviewerAccountNM,
                               thisSurveyItem.AutoAssignFilter,
                               thisSurveyItem.StartDT,
                               thisSurveyItem.EndDT,
                               thisSurveyItem.ParentSurveyID,
                               thisSurveyItem.ModifiedID,
                               Now).Single()
        thisSurveyItem.SurveyID = myResult.SurveyID

        ' Add New Survey to current Application
        gsp_ApplicationSurvey_Insert(ApplicationID, thisSurveyItem.SurveyID, DefaultRoleID, thisSurveyItem.ModifiedID, Now)

        If thisSurveyItem.StatusList.Count = 0 Then
            For Each SurveyStatus As SurveyStatusItem In (From i In gsp_lu_SurveyResponseStatus_Select(Nothing) Order By i.StatusNM
                    Select New SurveyStatusItem With {.StatusID = i.StatusID,
                                                      .StatusNM = i.StatusNM,
                                                      .StatusDS = i.StatusDS,
                                                      .NextStatusID = i.NextStatusID,
                                                      .PreviousStatusID = i.PreviousStatusID,
                                                      .BodyTemplate = "NO EMAIL",
                                                      .SubjectTemplate = "NO EMAIL",
                                                      .SurveyID = thisSurveyItem.SurveyID,
                                                      .ModifiedID = thisSurveyItem.ModifiedID,
                                                      .SurveyStatusID = -1}).ToList()
                thisSurveyItem.StatusList.Add(UpdateSurveyResponseStatus(SurveyStatus))
            Next
        Else
            For Each SurveyStatus As SurveyStatusItem In thisSurveyItem.StatusList
                SurveyStatus.SurveyID = thisSurveyItem.SurveyID
                SurveyStatus.SurveyStatusID = -1
                SurveyStatus.SurveyStatusID = UpdateSurveyResponseStatus(SurveyStatus).SurveyStatusID
            Next
        End If

        If thisSurveyItem.ReviewStatusList.Count = 0 Then
            For Each ReviewStatus As SurveyReviewStatusItem In (From i In gsp_lu_ReviewStatus_Select(Nothing) Order By i.ReviewStatusNM
                    Select New SurveyReviewStatusItem With {.ReviewStatusNM = i.ReviewStatusNM,
                                                            .ReviewStatusID = i.ReviewStatusID,
                                                            .ReviewStatusDS = i.ReviewStatusDS,
                                                            .SurveyID = thisSurveyItem.SurveyID,
                                                            .ApprovedFL = i.ApprovedFL,
                                                            .CommentFL = i.CommentFL,
                                                            .ModifiedID = i.ModifiedID,
                                                            .SurveyReviewStatusID = -1}).ToList()
                thisSurveyItem.ReviewStatusList.Add(UpdateReviewStatus(ReviewStatus))
            Next
        Else
            For Each ReviewStatus As SurveyReviewStatusItem In thisSurveyItem.ReviewStatusList
                ReviewStatus.SurveyID = thisSurveyItem.SurveyID
                ReviewStatus.SurveyReviewStatusID = -1
                ReviewStatus.SurveyReviewStatusID = UpdateReviewStatus(ReviewStatus).SurveyReviewStatusID
            Next
        End If


        For Each EmailTemplate As SurveyEmailTemplateItem In thisSurveyItem.EmailTemplateList
            EmailTemplate.SurveyID = thisSurveyItem.SurveyID
            EmailTemplate.SurveyEmailTemplateID = -1
            EmailTemplate.SurveyEmailTemplateID = UpdateSurveyEmailTemplate(EmailTemplate).SurveyEmailTemplateID

        Next

        For Each Group As QuestionGroupItem In thisSurveyItem.QuestionGroupList
            Group.SurveyID = thisSurveyItem.SurveyID
            Group.QuestionGroupID = -1
            Group.QuestionGroupID = UpdateQuestionGroup(Group).QuestionGroupID
        Next

        For Each Question As QuestionItem In thisSurveyItem.QuestionList
            Question.QuestionID = -1
            With PutQuestion(Question)
                Question.QuestionID = .QuestionID
                Question.QuestionNM = .QuestionNM
                Question.QuestionDS = .QuestionDS
                Question.QuestionShortNM = .QuestionShortNM
                Question.SurveyTypeID = .SurveyTypeID
                Question.QuestionSort = .QuestionSort
                Question.ReviewRoleLevel = .ReviewRoleLevel
                Question.QuestionTypeCD = .QuestionTypeCD
                Question.CommentFL = .CommentFL
                Question.QuestionValue = .QuestionValue
                Question.UnitOfMeasureID = .UnitOfMeasureID
                Question.QuestionGroupMember.QuestionID = .QuestionID
                Question.QuestionGroupMember.QuestionNM = .QuestionNM
                Question.QuestionAnswerItemList.Clear()
                For Each QA As QuestionAnswerItem In .QuestionAnswerItemList
                    Question.QuestionAnswerItemList.Add(QA)
                Next
            End With
        Next

        For Each Question As QuestionItem In thisSurveyItem.QuestionList
            For Each Group As QuestionGroupItem In thisSurveyItem.QuestionGroupList
                If Question.QuestionGroupMember.QuestionGroupShortNM = Group.QuestionGroupShortNM Then
                    Question.QuestionGroupMember.QuestionGroupMemberID = -1
                    Question.QuestionGroupMember.QuestionID = Question.QuestionID
                    Question.QuestionGroupMember.QuestionGroupID = Group.QuestionGroupID
                    Question.QuestionGroupMember.QuestionGroupMemberID = UpdateQuestionGroupMember(Question.QuestionGroupMember).QuestionGroupMemberID
                    Exit For
                End If
            Next
        Next

        Return thisSurveyItem
    End Function
    Function UpdateSurvey(ByVal thisSurveyItem As SurveyItem, ByVal ApplicationID As Integer, ByVal DefaultRoleID As Integer) As SurveyItem
        If thisSurveyItem.SurveyID < 1 Then
            Try
                With gsp_Survey_Insert(thisSurveyItem.SurveyType.SurveyTypeID,
                                       thisSurveyItem.UseSurveyGroupsFL,
                                       thisSurveyItem.SurveyNM,
                                       thisSurveyItem.SurveyShortNM,
                                       thisSurveyItem.SurveyDS,
                                       thisSurveyItem.CompletionMessage,
                                       thisSurveyItem.ResponseNMTemplate,
                                       thisSurveyItem.ReviewerAccountNM,
                                       thisSurveyItem.AutoAssignFilter,
                                       thisSurveyItem.StartDT,
                                       thisSurveyItem.EndDT,
                                       thisSurveyItem.ParentSurveyID,
                                       1,
                                       Now).Single()
                    thisSurveyItem.SurveyID = .SurveyID
                End With
                AppLog.AuditLog("DataController.UpdateSurvey - Insert Survey", thisSurveyItem.SurveyID.ToString())
                If ApplicationID > 0 AndAlso DefaultRoleID > 0 Then
                    gsp_ApplicationSurvey_Insert(ApplicationID,
                                                 thisSurveyItem.SurveyID,
                                                 DefaultRoleID,
                                                 1,
                                                 Now())
                End If
                For Each myGroup In thisSurveyItem.QuestionGroupList
                    myGroup.SurveyID = thisSurveyItem.SurveyID
                    UpdateQuestionGroup(myGroup)
                Next

                If thisSurveyItem.QuestionGroupList.Count = 0 Then
                    Dim gCount As Integer = 1
                    Dim gmCount As Integer = 1
                    For Each QuestionCategory In (From i In GetQuestionCategoryList() Where i.ParentSurveyTypeID = thisSurveyItem.SurveyType.SurveyTypeID)
                        Dim myGroup As New QuestionGroupItem With {.SurveyID = thisSurveyItem.SurveyID,
                                                                   .QuestionGroupID = -1,
                                                                   .QuestionGroupNM = QuestionCategory.SurveyTypeNM,
                                                                   .QuestionGroupDS = QuestionCategory.SurveyTypeDS,
                                                                   .QuestionGroupShortNM = QuestionCategory.SurveyTypeShortNM,
                                                                   .QuestionGroupWeight = 0,
                                                                   .QuestionGroupOrder = gCount,
                                                                   .ModifiedID = thisSurveyItem.ModifiedID,
                                                                   .MarkedForDeletion = False}
                        gmCount = 1
                        For Each Question In (From i In GetQuestions() Where i.SurveyTypeID = QuestionCategory.SurveyTypeID)
                            Dim myGroupMember As New QuestionGroupMemberItem With {.QuestionGroupID = -1,
                                                                                   .DisplayOrder = gmCount,
                                                                                   .MarkedForDeletion = False,
                                                                                   .QuestionGroupMemberID = -1,
                                                                                   .QuestionID = Question.QuestionID,
                                                                                   .QuestionWeight = 0}
                            myGroup.QuestionMembership.Add(myGroupMember)
                            gmCount = gmCount + 1
                        Next
                        UpdateQuestionGroup(myGroup)
                        gCount = gCount + 1
                    Next
                End If

                For Each SurveyStatus As SurveyStatusItem In (From i In gsp_lu_SurveyResponseStatus_Select(Nothing) Order By i.StatusNM
                        Select New SurveyStatusItem With {.StatusID = i.StatusID,
                                                          .StatusNM = i.StatusNM,
                                                          .StatusDS = i.StatusDS,
                                                          .NextStatusID = i.NextStatusID,
                                                          .PreviousStatusID = i.PreviousStatusID,
                                                          .BodyTemplate = "NO EMAIL",
                                                          .SubjectTemplate = "NO EMAIL",
                                                          .SurveyID = thisSurveyItem.SurveyID,
                                                          .ModifiedID = 1,
                                                          .SurveyStatusID = -1}).ToList()
                    UpdateSurveyResponseStatus(SurveyStatus)
                Next
                For Each ReviewStatus As SurveyReviewStatusItem In (From i In gsp_lu_ReviewStatus_Select(Nothing) Order By i.ReviewStatusNM
                        Select New SurveyReviewStatusItem With {.ReviewStatusNM = i.ReviewStatusNM,
                                                                .ReviewStatusID = i.ReviewStatusID,
                                                                .ReviewStatusDS = i.ReviewStatusDS,
                                                                .SurveyID = thisSurveyItem.SurveyID,
                                                                .ApprovedFL = i.ApprovedFL,
                                                                .CommentFL = i.CommentFL,
                                                                .ModifiedID = 1,
                                                                .SurveyReviewStatusID = -1}).ToList()
                    UpdateReviewStatus(ReviewStatus)
                Next
                bReturn = True
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, "DataController.UpdateSurvey-Insert")
                bReturn = False
            End Try
        Else
            Try
                gsp_Survey_Update(thisSurveyItem.SurveyID,
                                  thisSurveyItem.SurveyType.SurveyTypeID,
                                  thisSurveyItem.UseSurveyGroupsFL,
                                  thisSurveyItem.SurveyNM,
                                  thisSurveyItem.SurveyShortNM,
                                  thisSurveyItem.SurveyDS,
                                  thisSurveyItem.CompletionMessage,
                                  thisSurveyItem.ResponseNMTemplate,
                                  thisSurveyItem.ReviewerAccountNM,
                                  thisSurveyItem.AutoAssignFilter,
                                  thisSurveyItem.StartDT,
                                  thisSurveyItem.EndDT,
                                  thisSurveyItem.ParentSurveyID,
                                  1,
                                  Now).Single()
                bReturn = True
                AppLog.AuditLog("DataController.UpdateSurvey - Update Survey", thisSurveyItem.SurveyID.ToString())
                For Each myGroup In thisSurveyItem.QuestionGroupList
                    If myGroup.MarkedForDeletion Then
                        DeleteQuestionGroup(myGroup)
                    Else
                        myGroup.SurveyID = thisSurveyItem.SurveyID
                        UpdateQuestionGroup(myGroup)
                    End If
                Next
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, "DataController.UpdateSurvey-Update")
                bReturn = False
            End Try
        End If

        Return GetSurveyBySurveyID(thisSurveyItem.SurveyID)
    End Function
#End Region
#Region "Survey Response Methods"

    '
    '  Manual Declaration because of Dynamic SQL,  the LINQ to SQL tool does not handle Stored Procedures that return multiple values or that return dynamic values
    '  so I had to create the declaration of the stored procedure manually and used the gsp_SurveyResponse_SelectResult class as a return value. 
    '
    <Global.System.Data.Linq.Mapping.FunctionAttribute(Name:="dbo.usp_SurveyResponse_SelectByRowOrderBy")> _
    Public Function ApplicationSurveyResponseSummary_SelectByWhere(<Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="StartRow", DbType:="Int")> ByVal startRow As Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="PageSize", DbType:="Int")> ByVal pageSize As Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="WhereClase", DbType:="NVarChar(4000)")> ByVal whereClase As String) As ISingleResult(Of usp_SurveyResponse_SelectBySurveyResponseIDResult)
        Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), startRow, pageSize, whereClase)
        Return CType(result.ReturnValue, ISingleResult(Of usp_SurveyResponse_SelectBySurveyResponseIDResult))
    End Function

    Function GetApplicationSurveyResponseSummary(ByVal StartRow As Integer, ByVal PageSize As Integer, ByVal WhereClause As String) As List(Of SurveyResponseItem)
        Return (From i In ApplicationSurveyResponseSummary_SelectByWhere(StartRow, PageSize, WhereClause) Order By i.SurveyNM, i.SurveyResponseNM
                Select New SurveyResponseItem With {.SurveyResponseID = i.SurveyResponseID,
                                                      .SurveyResponseNM = i.SurveyResponseNM,
                                                      .ApplicationID = i.ApplicationID,
                                                      .ModifiedID = i.ModifiedID,
                                                      .ModifiedDT = i.ModifiedDT,
                                                      .AssignedUserID = i.AssignedUserID,
                                                      .StatusID = i.StatusID,
                                                      .StatusNM = i.StatusNM,
                                                      .Survey = New SurveyItem With {.SurveyID = CType(i.SurveyID, Integer),
                                                                                         .SurveyNM = i.SurveyNM,
                                                                                         .SurveyShortNM = i.SurveyShortNM},
                                                      .AccountNM = i.AccountNM,
                                                      .Employee_FName = i.FirstNM,
                                                      .Employee_LName = i.LastNM,
                                                      .AnswerCount = AppUtility.GetDBInteger(i.AnswerCount),
                                                      .QuestionCount = AppUtility.GetDBInteger(i.QuestionCount),
                                                      .SurveyResponseScore = AppUtility.GetDBDecimal(i.SurveyResponseScore),
                                                      .ComplianceReviewCount = AppUtility.GetDBInteger(i.PendingReviewCount),
                                                      .PercentComplete = AppUtility.GetDBInteger(i.PercentComplete),
                                                      .DaysSinceModified = AppUtility.GetDBInteger(i.DaySinceModified),
                                                      .DataSource = i.DataSource}).ToList()

    End Function


    Function GetSurveyResponsesByApplicationUserForInput(ByVal ApplicationUserID As Integer, ByVal SurveyID As Integer, ByVal ApplicationID As Integer) As List(Of SurveyResponseItem)
        Try
            Return (From i In usp_SurveyResponse_SelectByApplicationUserID(ApplicationUserID, ApplicationID) Where i.SurveyID = SurveyID Order By i.SurveyNM, i.SurveyResponseNM
                    Select New SurveyResponseItem With {.SurveyResponseID = i.SurveyResponseID,
                                                          .SurveyResponseNM = i.SurveyResponseNM,
                                                          .ApplicationID = i.ApplicationID,
                                                          .AssignedSupervisorUserID = i.SupervisorApplicationUserID,
                                                          .AssignedUserID = i.AssignedUserID,
                                                          .StatusID = i.StatusID,
                                                          .StatusNM = i.StatusNM,
                                                          .Survey = New SurveyItem With {.SurveyID = CType(i.SurveyID, Integer),
                                                                                         .SurveyNM = i.SurveyNM,
                                                                                         .SurveyShortNM = i.SurveyShortNM},
                                                          .ModifiedID = i.ModifiedID,
                                                          .ModifiedDT = i.ModifiedDT,
                                                          .AccountNM = i.AccountNM,
                                                          .Employee_FName = i.FirstNM,
                                                          .Employee_LName = i.LastNM,
                                                          .DataSource = i.DataSource}).ToList()
        Catch ex As Exception
            AppLog.SQLError(ex.ToString(), "DataController.GetSurveyResponsesByApplicationUserForInput")
            Return New List(Of SurveyResponseItem)
        End Try
    End Function

    Function GetSurveyResponseListByUser(ByVal ApplicationUserID As Integer, ByVal ApplicationID As Integer) As List(Of SurveyResponseItem)
        Dim myReturn As New List(Of SurveyResponseItem)
        Try

            Dim sWhere As String = String.Format(" where ApplicationID={0} and ( AssignedUserID={1} or AssignedUserID is null)  ", ApplicationID, ApplicationUserID)

            myReturn = (From i In ApplicationSurveyResponseSummary_SelectByWhere(0, 500, sWhere) Order By i.SurveyNM, i.SurveyResponseNM
                    Select New SurveyResponseItem With {.SurveyResponseID = i.SurveyResponseID,
                                                          .SurveyResponseNM = i.SurveyResponseNM,
                                                          .ApplicationID = i.ApplicationID,
                                                          .AssignedUserID = i.AssignedUserID,
                                                          .StatusID = i.StatusID,
                                                          .StatusNM = i.StatusNM,
                                                          .Survey = New SurveyItem With {.SurveyID = CType(i.SurveyID, Integer),
                                                                                         .SurveyNM = i.SurveyNM,
                                                                                         .SurveyShortNM = i.SurveyShortNM},
                                                          .ModifiedID = i.ModifiedID,
                                                          .SurveyResponseScore = AppUtility.GetDBDecimal(i.SurveyResponseScore),
                                                          .PercentComplete = AppUtility.GetDBInteger(i.PercentComplete),
                                                          .AnswerCount = AppUtility.GetDBInteger(i.AnswerCount),
                                                          .QuestionCount = AppUtility.GetDBInteger(i.QuestionCount),
                                                          .ComplianceReviewCount = AppUtility.GetDBInteger(i.PendingReviewCount),
                                                          .ModifiedDT = i.ModifiedDT,
                                                          .AccountNM = i.AccountNM,
                                                          .Employee_FName = i.FirstNM,
                                                          .Employee_LName = i.LastNM,
                                                          .DataSource = i.DataSource}).ToList()

            For Each sriReturn In myReturn
                sriReturn.Survey = GetSurveyBySurveyID(sriReturn.Survey.SurveyID)
                sriReturn.SequenceList = GetSurveyResponseSequencesBySurveyResponseID(sriReturn.SurveyResponseID)
                If sriReturn.SequenceList.Count = 0 Then
                    Dim newSequence As New SurveyResponseSequenceItem With {.SequenceNumber = 1,
                                                                            .SequenceText = String.Empty,
                                                                            .SurveyResponseID = sriReturn.SurveyResponseID}
                    UpdateSurveyResponseSequence(newSequence)
                    sriReturn.SequenceList = GetSurveyResponseSequencesBySurveyResponseID(sriReturn.SurveyResponseID)
                End If
                If (sriReturn.AnswerList Is Nothing) Then
                    sriReturn.AnswerList = New List(Of SurveyResponseAnswerItem)
                End If
                sriReturn.AnswerList = GetSurveyResponseAnswersBySurveyResponseID(sriReturn.SurveyResponseID)
                sriReturn.StateList = GetSurveyResponseState(sriReturn.SurveyResponseID)
                sriReturn.SurveyResponseHistory = GetSurveyResponseHistoryBySurveyResponseID(sriReturn.SurveyResponseID)
            Next
        Catch ex As Exception
            AppLog.SQLError(ex.ToString(), "DataController.GetSurveyResponsesByApplicationUserForInput")
            Return New List(Of SurveyResponseItem)
        End Try
        Return myReturn
    End Function

    Function GetSurveyResponseListByApplication(ByVal ApplicationID As Integer, ByVal GetDetails As Boolean, ByVal SurveyResponseID As Integer) As List(Of SurveyResponseItem)
        Dim myReturn As New List(Of SurveyResponseItem)
        Try
            Dim sWhere As String
            If SurveyResponseID > 0 Then
                sWhere = String.Format(" where ApplicationID={0} and SurveyResponseID={1} ", ApplicationID, SurveyResponseID)
            Else
                sWhere = String.Format(" where ApplicationID={0} ", ApplicationID)
            End If
            myReturn = (From i In ApplicationSurveyResponseSummary_SelectByWhere(0, 500, sWhere) Order By i.SurveyNM, i.SurveyResponseNM
                    Select New SurveyResponseItem With {.SurveyResponseID = i.SurveyResponseID,
                                                          .SurveyResponseNM = i.SurveyResponseNM,
                                                          .ApplicationID = i.ApplicationID,
                                                          .AssignedUserID = i.AssignedUserID,
                                                          .StatusID = i.StatusID,
                                                          .StatusNM = i.StatusNM,
                                                          .Survey = New SurveyItem With {.SurveyID = CType(i.SurveyID, Integer),
                                                                                         .SurveyNM = i.SurveyNM,
                                                                                         .SurveyShortNM = i.SurveyShortNM},
                                                          .ModifiedID = i.ModifiedID,
                                                          .SurveyResponseScore = AppUtility.GetDBDecimal(i.SurveyResponseScore),
                                                          .PercentComplete = AppUtility.GetDBInteger(i.PercentComplete),
                                                          .AnswerCount = AppUtility.GetDBInteger(i.AnswerCount),
                                                          .QuestionCount = AppUtility.GetDBInteger(i.QuestionCount),
                                                          .ComplianceReviewCount = AppUtility.GetDBInteger(i.PendingReviewCount),
                                                          .ModifiedDT = i.ModifiedDT,
                                                          .AccountNM = i.AccountNM,
                                                          .Employee_FName = i.FirstNM,
                                                          .Employee_LName = i.LastNM,
                                                          .DataSource = i.DataSource}).ToList()
            If GetDetails Then
                For Each sriReturn In myReturn
                    sriReturn.Survey = GetSurveyBySurveyID(sriReturn.Survey.SurveyID)
                    sriReturn.SequenceList = GetSurveyResponseSequencesBySurveyResponseID(sriReturn.SurveyResponseID)
                    If sriReturn.SequenceList.Count = 0 Then
                        Dim newSequence As New SurveyResponseSequenceItem With {.SequenceNumber = 1,
                                                                                .SequenceText = String.Empty,
                                                                                .SurveyResponseID = sriReturn.SurveyResponseID}
                        UpdateSurveyResponseSequence(newSequence)
                        sriReturn.SequenceList = GetSurveyResponseSequencesBySurveyResponseID(sriReturn.SurveyResponseID)
                    End If
                    If (sriReturn.AnswerList Is Nothing) Then
                        sriReturn.AnswerList = New List(Of SurveyResponseAnswerItem)
                    End If
                    sriReturn.AnswerList = GetSurveyResponseAnswersBySurveyResponseID(sriReturn.SurveyResponseID)
                    sriReturn.StateList = GetSurveyResponseState(sriReturn.SurveyResponseID)
                    sriReturn.SurveyResponseHistory = GetSurveyResponseHistoryBySurveyResponseID(sriReturn.SurveyResponseID)
                Next
            End If
        Catch ex As Exception
            AppLog.SQLError(ex.ToString(), "DataController.GetSurveyResponsesByApplicationUserForInput")
            Return New List(Of SurveyResponseItem)
        End Try
        Return myReturn
    End Function

    Function GetSurveyResponseListByApplicationUserID(ByVal ApplicationUserID As Integer) As List(Of SurveyResponseItem)
        Dim myReturn As New List(Of SurveyResponseItem)
        Try
            Dim sWhere As String
            If ApplicationUserID > 0 Then

                sWhere = String.Format(" where AssignedUserID={0} ", ApplicationUserID)

                For Each i In ApplicationSurveyResponseSummary_SelectByWhere(0, 500, sWhere).ToList()
                    myReturn.Add(New SurveyResponseItem With {.SurveyResponseID = i.SurveyResponseID,
                                                                .SurveyResponseNM = i.SurveyResponseNM,
                                                                .ApplicationID = i.ApplicationID,
                                                                .AssignedUserID = i.AssignedUserID,
                                                                .StatusID = i.StatusID,
                                                                .StatusNM = i.StatusNM,
                                                                .Survey = New SurveyItem With {.SurveyID = AppUtility.GetDBInteger(i.SurveyID, 0),
                                                                                               .SurveyNM = i.SurveyNM,
                                                                                               .SurveyShortNM = i.SurveyShortNM},
                                                                .ModifiedID = i.ModifiedID,
                                                                .SurveyResponseScore = AppUtility.GetDBDecimal(i.SurveyResponseScore),
                                                                .PercentComplete = AppUtility.GetDBInteger(i.PercentComplete, 0),
                                                                .AnswerCount = AppUtility.GetDBInteger(i.AnswerCount, 0),
                                                                .QuestionCount = AppUtility.GetDBInteger(i.QuestionCount, 0),
                                                                .ComplianceReviewCount = AppUtility.GetDBInteger(i.PendingReviewCount, 0),
                                                                .ModifiedDT = i.ModifiedDT,
                                                                .AccountNM = i.AccountNM,
                                                                .Employee_FName = i.FirstNM,
                                                                .Employee_LName = i.LastNM,
                                                                .DataSource = i.DataSource}
                                                            )
                Next
            End If
        Catch ex As Exception
            AppLog.SQLError(ex.ToString(), "DataController.GetSurveyResponsesByApplicationUserForInput")
            Return New List(Of SurveyResponseItem)
        End Try
        Return myReturn
    End Function

    Function GetSurveyResponsesByApplicationUserForInput(ByVal ApplicationID As Integer, ByVal ApplicationUserID As Integer) As List(Of SurveyResponseItem)
        Try
            Return (From i In usp_SurveyResponse_SelectByApplicationUserID(ApplicationUserID, ApplicationID) Order By i.SurveyNM, i.SurveyResponseNM
                    Select New SurveyResponseItem With {.SurveyResponseID = i.SurveyResponseID,
                                                          .SurveyResponseNM = i.SurveyResponseNM,
                                                          .ApplicationID = i.ApplicationID,
                                                          .AssignedSupervisorUserID = i.SupervisorApplicationUserID,
                                                          .AssignedUserID = i.AssignedUserID,
                                                          .StatusID = i.StatusID,
                                                          .StatusNM = i.StatusNM,
                                                          .Survey = New SurveyItem With {.SurveyID = CType(i.SurveyID, Integer),
                                                                                         .SurveyNM = i.SurveyNM,
                                                                                         .SurveyShortNM = i.SurveyShortNM},
                                                          .ModifiedID = i.ModifiedID,
                                                          .ModifiedDT = i.ModifiedDT,
                                                          .AccountNM = i.AccountNM,
                                                          .Employee_FName = i.FirstNM,
                                                          .Employee_LName = i.LastNM,
                                                          .DataSource = i.DataSource}).ToList()
        Catch ex As Exception
            AppLog.SQLError(ex.ToString(), "DataController.GetSurveyResponsesByApplicationUserForInput")
            Return New List(Of SurveyResponseItem)
        End Try
    End Function

    Function GetApplicationSurveyResponse_SelectBySurveyResponseID(ByVal SurveyResponseID As Integer) As SurveyResponseItem
        Dim sriReturn As New SurveyResponseItem
        If SurveyResponseID > 0 Then
            Try
                sriReturn = (From i In usp_SurveyResponse_SelectBySurveyResponseID(SurveyResponseID)
                    Select New SurveyResponseItem With {.SurveyResponseID = i.SurveyResponseID,
                                                          .SurveyResponseNM = i.SurveyResponseNM,
                                                          .DataSource = i.DataSource,
                                                          .ApplicationID = i.ApplicationID,
                                                          .AssignedUserID = i.AssignedUserID,
                                                          .StatusID = i.StatusID,
                                                          .StatusNM = i.StatusNM,
                                                          .Survey = New SurveyItem With {.SurveyID = CType(i.SurveyID, Integer),
                                                                                         .SurveyNM = i.SurveyNM,
                                                                                         .SurveyShortNM = i.SurveyShortNM},
                                                          .ModifiedID = i.ModifiedID,
                                                          .Employee_FName = i.FirstNM,
                                                          .Employee_LName = i.LastNM,
                                                          .AccountNM = i.AccountNM,
                                                          .AnswerCount = AppUtility.GetDBInteger(i.AnswerCount),
                                                          .QuestionCount = AppUtility.GetDBInteger(i.QuestionCount),
                                                          .ComplianceReviewCount = AppUtility.GetDBInteger(i.PendingReviewCount),
                                                          .SurveyResponseScore = AppUtility.GetDBDecimal(i.SurveyResponseScore),
                                                          .PercentComplete = AppUtility.GetDBInteger(i.PercentComplete),
                                                          .DaysSinceModified = AppUtility.GetDBInteger(i.DaySinceModified)
                                                         }).Single()
                sriReturn.Survey = GetSurveyBySurveyID(sriReturn.Survey.SurveyID)
                sriReturn.SequenceList = GetSurveyResponseSequencesBySurveyResponseID(sriReturn.SurveyResponseID)
                If sriReturn.SequenceList.Count = 0 Then
                    Dim newSequence As New SurveyResponseSequenceItem With {.SequenceNumber = 1,
                                                                            .SequenceText = String.Empty,
                                                                            .SurveyResponseID = sriReturn.SurveyResponseID}
                    UpdateSurveyResponseSequence(newSequence)
                    sriReturn.SequenceList = GetSurveyResponseSequencesBySurveyResponseID(sriReturn.SurveyResponseID)
                End If
                If (sriReturn.AnswerList Is Nothing) Then
                    sriReturn.AnswerList = New List(Of SurveyResponseAnswerItem)
                End If
                sriReturn.AnswerList = GetSurveyResponseAnswersBySurveyResponseID(SurveyResponseID)
                sriReturn.StateList = GetSurveyResponseState(sriReturn.SurveyResponseID)
                sriReturn.SurveyResponseHistory = GetSurveyResponseHistoryBySurveyResponseID(SurveyResponseID)
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, String.Format("DataController.GetApplicationSurveyResponse_SelectBySurveyResponseID SurveyResponseID={0}", SurveyResponseID))
            End Try
        End If

        Return sriReturn
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="thisSurveyResponse"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateSurveyResponse(ByVal thisSurveyResponse As SurveyResponseItem) As SurveyResponseItem
        Dim bContinue = True

        If String.IsNullOrEmpty(thisSurveyResponse.SurveyResponseNM) Then
            bContinue = False
            AppLog.ErrorLog(String.Format("SurveyResponseNM is missing - SurveyResponseID={0}", thisSurveyResponse.SurveyResponseID), "DataController.UpdateSurveyResponse")
        End If
        If thisSurveyResponse.Survey.SurveyID < 1 Then
            bContinue = False
            AppLog.ErrorLog(String.Format("thisSurveyResponse.Survey.SurveyID is less than 1 - SurveyResponseID={0}", thisSurveyResponse.SurveyResponseID), "DataController.UpdateSurveyResponse")
        End If
        If String.IsNullOrEmpty(thisSurveyResponse.DataSource) Then
            bContinue = False
            AppLog.ErrorLog(String.Format("DataSource is missing - SurveyResponseID={0}", thisSurveyResponse.SurveyResponseID), "DataController.UpdateSurveyResponse")
        End If
        If thisSurveyResponse.StatusID < 1 Then
            thisSurveyResponse.StatusID = 1
        End If

        If thisSurveyResponse.SurveyResponseID < 1 AndAlso bContinue Then
            Try
                Dim gspResult = gsp_SurveyResponse_Insert(thisSurveyResponse.SurveyResponseNM,
                                          thisSurveyResponse.Survey.SurveyID,
                                          thisSurveyResponse.ApplicationID,
                                          thisSurveyResponse.AssignedUserID,
                                          thisSurveyResponse.StatusID,
                                          thisSurveyResponse.DataSource,
                                          thisSurveyResponse.ModifiedID,
                                          Now()).Single()

                thisSurveyResponse.SurveyResponseID = gspResult.SurveyResponseID
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, String.Format("DataController.UpdateSurveyResponse (Insert) SurveyResponseID={0}", thisSurveyResponse.SurveyResponseID))
            End Try
        Else
            Try
                gsp_SurveyResponse_Update(thisSurveyResponse.SurveyResponseID,
                                          thisSurveyResponse.SurveyResponseNM,
                                          thisSurveyResponse.Survey.SurveyID,
                                          thisSurveyResponse.ApplicationID,
                                          thisSurveyResponse.AssignedUserID,
                                          thisSurveyResponse.StatusID,
                                          thisSurveyResponse.DataSource,
                                          thisSurveyResponse.ModifiedID,
                                          Now())
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, String.Format("DataController.UpdateSurveyResponse (Update) SurveyResponseID={0}", thisSurveyResponse.SurveyResponseID))
            End Try
        End If
        If bReturn Then
            Return GetApplicationSurveyResponse_SelectBySurveyResponseID(thisSurveyResponse.SurveyResponseID)
        Else
            Return thisSurveyResponse
        End If
    End Function

    Public Function DeleteSurveyResponse(ByVal thisSurveyResponse As SurveyResponseItem) As Integer
        Return usp_SurveyResponse_Delete(thisSurveyResponse.SurveyResponseID)
    End Function
    Public Function ResetSurveyResponse(ByVal thisSurveyResponse As SurveyResponseItem) As Integer
        Return usp_SurveyResponse_Reset(thisSurveyResponse.SurveyResponseID)
    End Function

    Public Function GetSurveyResponseCount(ByVal sWhere As String) As Integer
        Return usp_SurveyResponse_SelectCountByWhere(sWhere).Single.ReturnCount.Value
    End Function

#End Region
#Region "Survey Response Data Set Methods"






#End Region
#Region "Survey Response Answer Methods"
    Function GetSurveyResponseAnswersBySurveyResponseID(ByVal SurveyResponseID As Integer) As List(Of SurveyResponseAnswerItem)
        Dim mySRAIList As New List(Of SurveyResponseAnswerItem)
        Try

            mySRAIList.AddRange((From i In usp_SurveyResponseCurrentAnswers_SelectBySurveyResponseID(SurveyResponseID, 99, Nothing).ToList()
                    Select New SurveyResponseAnswerItem With {.SurveyAnswerID = i.SurveyAnswerID,
                                                              .SurveyResponseID = i.SurveyResponseID,
                                                              .AnswerComment = i.AnswerComment,
                                                              .AnswerDate = AppUtility.GetDBDate(i.AnswerDate),
                                                              .AnswerQuantity = AppUtility.GetDBDecimal(i.AnswerQuantity),
                                                              .AnswerType = i.AnswerType,
                                                              .QuestionAnswerID = i.QuestionAnswerID,
                                                              .QuestionAnswerNM = i.QuestionAnswerNM,
                                                              .QuestionID = i.QuestionID,
                                                              .SequenceNumber = i.SequenceNumber,
                                                              .QuestionAnswerValue = i.QuestionAnswerValue,
                                                              .QuestionValue = AppUtility.GetDBDecimal(i.QuestionValue),
                                                              .ModifiedComment = i.ModifiedComment,
                                                              .ModifiedID = i.ModifiedID,
                                                              .ModifiedDT = i.ModifiedDT}).ToList())

            For Each mySRAI In mySRAIList
                Select Case mySRAI.AnswerType.ToLower()
                    Case "questionanswerid"
                        mySRAI.DisplayAnswerNM = mySRAI.QuestionAnswerNM
                        mySRAI.DisplayAnswerComment = mySRAI.AnswerComment
                    Case "quantity"
                        mySRAI.DisplayAnswerNM = CStr(mySRAI.AnswerQuantity)
                        mySRAI.DisplayAnswerComment = mySRAI.AnswerComment
                    Case "date"
                        mySRAI.DisplayAnswerNM = CStr(mySRAI.AnswerDate)
                        mySRAI.DisplayAnswerComment = mySRAI.AnswerComment
                    Case "comment"
                        mySRAI.DisplayAnswerNM = mySRAI.AnswerComment
                        mySRAI.DisplayAnswerComment = mySRAI.AnswerComment
                    Case Else
                        mySRAI.DisplayAnswerNM = mySRAI.QuestionAnswerNM
                        mySRAI.DisplayAnswerComment = mySRAI.AnswerComment
                End Select

                mySRAI.AnswerReviewList.AddRange((From i In usp_SurveyResponseAnswerReview_SelectBySurveyAnswerID(mySRAI.SurveyAnswerID).ToList()
                                                  Select New SurveyResponseAnswerReviewItem With {.ApplicationUserRoleID = i.ApplicationUserRoleID,
                                                                                                  .ModifiedComment = i.ModifiedComment,
                                                                                                  .ModifiedDT = i.ModifiedDT,
                                                                                                  .ModifiedID = i.ModifiedID,
                                                                                                  .ReviewStatusID = i.ReviewStatusID,
                                                                                                  .ReviewLevel = i.ReviewLevel,
                                                                                                  .ReviewerAccountNM = i.ReviewerAccountNM,
                                                                                                  .ReviewerNM = i.ReviewerNM,
                                                                                                  .ApprovedFL = i.ApprovedFL,
                                                                                                  .SurveyAnswerID = i.SurveyAnswerID,
                                                                                                  .SurveyResponseAnswerReviewID = i.SurveyResponseAnswerReviewID}).ToList())
            Next
        Catch ex As Exception
            AppLog.SQLError(ex.ToString(), "DataController.GetSurveyResponseAnswersBySurveyResponseID")
        End Try
        Return mySRAIList
    End Function

    Function UpdateSurveyResponseAnswer(ByVal ThisSurveyResponseAnswerItem As SurveyResponseAnswerItem) As SurveyResponseAnswerItem
        bReturn = False
        Try
            If ThisSurveyResponseAnswerItem.SurveyAnswerID < 1 Then
                gsp_SurveyResponseAnswer_Insert(ThisSurveyResponseAnswerItem.SurveyResponseID,
                                                ThisSurveyResponseAnswerItem.SequenceNumber,
                                                ThisSurveyResponseAnswerItem.QuestionID,
                                                ThisSurveyResponseAnswerItem.QuestionAnswerID,
                                                ThisSurveyResponseAnswerItem.AnswerType,
                                                ThisSurveyResponseAnswerItem.AnswerQuantity,
                                                ThisSurveyResponseAnswerItem.AnswerDate,
                                                ThisSurveyResponseAnswerItem.AnswerComment,
                                                ThisSurveyResponseAnswerItem.ModifiedComment,
                                                ThisSurveyResponseAnswerItem.ModifiedID,
                                                Now)
            Else
                gsp_SurveyResponseAnswer_Update(ThisSurveyResponseAnswerItem.SurveyAnswerID,
                                                ThisSurveyResponseAnswerItem.SurveyResponseID,
                                                ThisSurveyResponseAnswerItem.SequenceNumber,
                                                ThisSurveyResponseAnswerItem.QuestionID,
                                                ThisSurveyResponseAnswerItem.QuestionAnswerID,
                                                ThisSurveyResponseAnswerItem.AnswerType,
                                                ThisSurveyResponseAnswerItem.AnswerQuantity,
                                                ThisSurveyResponseAnswerItem.AnswerDate,
                                                ThisSurveyResponseAnswerItem.AnswerComment,
                                                ThisSurveyResponseAnswerItem.ModifiedComment,
                                                ThisSurveyResponseAnswerItem.ModifiedID,
                                                Now)
                usp_SurveyResponseAnswerReview_DeleteBySurveyAnswerID(ThisSurveyResponseAnswerItem.SurveyAnswerID)
            End If
        Catch ex As Exception
            AppLog.SQLError(ex.ToString(), "DataController.UpdateSurveyResponseAnswer")
        End Try
        Return ThisSurveyResponseAnswerItem
    End Function
    Function DeleteSurveyResponseAnswer(ByVal ThisSurveyResponseAnswerItem As SurveyResponseAnswerItem) As Boolean
        bReturn = False
        Try
            If gsp_SurveyResponseAnswer_Delete(ThisSurveyResponseAnswerItem.SurveyAnswerID) = 0 Then
                bReturn = True
            End If
        Catch ex As Exception
            AppLog.SQLError(ex.ToString(), "DataController.DeleteSurveyResponseAnswer")
        End Try
    End Function
#End Region
#Region "Survey Response History Methods"
    Function GetSurveyResponseHistoryBySurveyResponseID(ByVal SurveyResponseID As Integer) As List(Of SurveyResponseHistoryItem)
        Return (From i In usp_SurveyResponseHistory_SelectBySurveyResponseID(SurveyResponseID) Order By i.ModifiedDT Ascending
                Select New SurveyResponseHistoryItem With {.SurveyResponseHistoryID = i.SurveyResponseHistoryID,
                                                           .SurveyResponseID = i.SurveyResponseID,
                                                           .SurveyResponseNM = i.SurveyResponseNM,
                                                           .UserNM = i.UserNM,
                                                           .StatusID = i.StatusID,
                                                           .StatusNM = i.StatusNM,
                                                           .QuestionGroupID = i.QuestionGroupID,
                                                           .ModifiedID = i.ModifiedID,
                                                           .ModifiedDT = i.ModifiedDT,
                                                           .ModifiedNM = i.ModifiedNM,
                                                           .ApplicationUserID = i.ApplicationUserID,
                                                           .Answers = i.Answers}).ToList
    End Function


#End Region
#Region "Survey Response Answer Review Methods"
    Function GetSurveyResponseAnswerReviews() As List(Of SurveyResponseAnswerReviewItem)
        Return (From i In gsp_SurveyResponseAnswerReview_Select(Nothing)
                Select New SurveyResponseAnswerReviewItem With {.SurveyResponseAnswerReviewID = i.SurveyResponseAnswerReviewID,
                                                                .ApplicationUserRoleID = i.ApplicationUserRoleID,
                                                                .ReviewLevel = i.ReviewLevel,
                                                                .ReviewStatusID = i.ReviewStatusID,
                                                                .ModifiedID = i.ModifiedID,
                                                                .ModifiedDT = i.ModifiedDT}).ToList
    End Function
    Function DeleteSurveyResponseAnswerReview(ByVal thisSurveyResponseAnswerReviewItem As SurveyResponseAnswerReviewItem) As Boolean
        If gsp_SurveyResponseAnswerReview_Delete(thisSurveyResponseAnswerReviewItem.SurveyResponseAnswerReviewID) = 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function
    Function UpdateSurveyResponseAnswerReview(ByVal thisSurveyResponseAnswerReviewItem As SurveyResponseAnswerReviewItem) As SurveyResponseAnswerReviewItem
        If thisSurveyResponseAnswerReviewItem.SurveyResponseAnswerReviewID < 1 Then
            Try
                gsp_SurveyResponseAnswerReview_Insert(thisSurveyResponseAnswerReviewItem.SurveyAnswerID,
                                                      thisSurveyResponseAnswerReviewItem.ApplicationUserRoleID,
                                                      thisSurveyResponseAnswerReviewItem.ReviewLevel,
                                                      thisSurveyResponseAnswerReviewItem.ReviewStatusID,
                                                      thisSurveyResponseAnswerReviewItem.ModifiedID,
                                                      Now,
                                                      thisSurveyResponseAnswerReviewItem.ModifiedComment)
                bReturn = True
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, String.Format("DataController.UpdateSurveyResponseAnswerReview-Insert ID={0}", thisSurveyResponseAnswerReviewItem.SurveyResponseAnswerReviewID))
                bReturn = False
            End Try
        Else
            Try
                gsp_SurveyResponseAnswerReview_Update(thisSurveyResponseAnswerReviewItem.SurveyResponseAnswerReviewID,
                                                      thisSurveyResponseAnswerReviewItem.SurveyAnswerID,
                                                      thisSurveyResponseAnswerReviewItem.ApplicationUserRoleID,
                                                      thisSurveyResponseAnswerReviewItem.ReviewLevel,
                                                      thisSurveyResponseAnswerReviewItem.ReviewStatusID,
                                                      thisSurveyResponseAnswerReviewItem.ModifiedID,
                                                      Now,
                                                      thisSurveyResponseAnswerReviewItem.ModifiedComment)
                bReturn = True
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, String.Format("DataController.UpdateSurveyResponseAnswerReview-Update ID={0}", thisSurveyResponseAnswerReviewItem.SurveyResponseAnswerReviewID))
                bReturn = False
            End Try
        End If

        Return thisSurveyResponseAnswerReviewItem
    End Function
#End Region
#Region "Survey Response Sequence Methods"
    Function GetSurveyResponseSequencesBySurveyResponseID(ByVal SurveyResponseID As Integer) As List(Of SurveyResponseSequenceItem)
        Return (From i In usp_SurveyResponseSequence_SelectBySurveyResponseID(SurveyResponseID)
                Select New SurveyResponseSequenceItem With {.SequenceID = i.SurveyResponseSequenceID,
                                                            .SequenceNumber = i.SequenceNumber,
                                                            .SequenceText = i.SequenceText,
                                                            .SurveyResponseID = i.SurveyResponseID}).ToList()
    End Function
    Function UpdateSurveyResponseSequence(ByVal thisSequenceItem As SurveyResponseSequenceItem) As Boolean
        If thisSequenceItem.SequenceID < 1 Then
            Try
                gsp_SurveyResponseSequence_Insert(thisSequenceItem.SurveyResponseID,
                                                   thisSequenceItem.SequenceNumber,
                                                   thisSequenceItem.SequenceText,
                                                   1,
                                                   Now)
                bReturn = True
            Catch ex As Exception
                bReturn = False
            End Try
        Else
            Try
                gsp_SurveyResponseSequence_Update(thisSequenceItem.SequenceID,
                                                  thisSequenceItem.SurveyResponseID,
                                                   thisSequenceItem.SequenceNumber,
                                                   thisSequenceItem.SequenceText,
                                                   1,
                                                   Now)
                bReturn = True
            Catch ex As Exception
                bReturn = False
            End Try
        End If
        Return bReturn
    End Function
#End Region
#Region "Survey Response State"
    Function GetSurveyResponseState(ByVal pSurveyResponseID As Integer) As List(Of SurveyResponseStateItem)
        Dim myStateList As New List(Of SurveyResponseStateItem)
        myStateList.AddRange((From i In usp_SurveyResponseState_SelectBySurveyResponseID(pSurveyResponseID) Order By i.StateModifiedDT Ascending
                Select New SurveyResponseStateItem With {.SurveyResponseStateID = i.SurveyResponseStateID,
                                                         .SurveyResponseID = i.SurveyResponseID,
                                                         .Active = True,
                                                         .AssignedUserID = i.AssignedUserID,
                                                         .EmailBody = i.EmailBody,
                                                         .EmailSent = i.EmailSent,
                                                         .StatusID = i.StatusID,
                                                         .ModifiedID = i.ModifiedID,
                                                         .ModifiedDT = i.StateModifiedDT}).ToList())
        Return myStateList
    End Function
    Function UpdateSurveyResponseState(ByVal thisSurveyResponse As SurveyResponseItem, ByVal ActivityDescription As String, ByVal bEmailSent As Boolean) As SurveyResponseStateItem
        Dim SRSI As New SurveyResponseStateItem

        Try
            If thisSurveyResponse.StatusID = 3 Then
                If thisSurveyResponse.AssignedSupervisorUserID Is Nothing Then
                    SRSI = UpdateSurveyResponseState(New SurveyResponseStateItem() With {.SurveyResponseID = thisSurveyResponse.SurveyResponseID,
                                                                                         .SurveyResponseStateID = -1,
                                                                                         .StatusID = thisSurveyResponse.StatusID,
                                                                                         .Active = False,
                                                                                         .EmailSent = bEmailSent,
                                                                                         .ModifiedID = thisSurveyResponse.ModifiedID,
                                                                                         .ModifiedDT = Now(),
                                                                                         .EmailBody = ActivityDescription,
                                                                                         .AssignedUserID = AppUtility.GetDBInteger(thisSurveyResponse.AssignedUserID)})
                Else
                    SRSI = UpdateSurveyResponseState(New SurveyResponseStateItem() With {.SurveyResponseID = thisSurveyResponse.SurveyResponseID,
                                                                                         .SurveyResponseStateID = -1,
                                                                                         .StatusID = thisSurveyResponse.StatusID,
                                                                                         .Active = False,
                                                                                         .EmailSent = bEmailSent,
                                                                                         .ModifiedID = thisSurveyResponse.ModifiedID,
                                                                                         .ModifiedDT = Now(),
                                                                                         .EmailBody = ActivityDescription,
                                                                                         .AssignedUserID = AppUtility.GetDBInteger(thisSurveyResponse.AssignedSupervisorUserID)})
                End If
            Else
                SRSI = UpdateSurveyResponseState(New SurveyResponseStateItem() With {.SurveyResponseID = thisSurveyResponse.SurveyResponseID,
                                                                                     .SurveyResponseStateID = -1,
                                                                                     .StatusID = thisSurveyResponse.StatusID,
                                                                                     .Active = False,
                                                                                     .EmailSent = bEmailSent,
                                                                                     .ModifiedID = thisSurveyResponse.ModifiedID,
                                                                                     .ModifiedDT = Now(),
                                                                                     .EmailBody = ActivityDescription,
                                                                                     .AssignedUserID = AppUtility.GetDBInteger(thisSurveyResponse.AssignedUserID)})
            End If
        Catch ex As Exception
            AppLog.SQLError(ex.ToString, String.Format("DataController.UpdateSurveyResponseState SurveyResponseID={0}", thisSurveyResponse.SurveyResponseID))
        End Try
        Return SRSI
    End Function
    Function UpdateSurveyResponseState(ByVal State As SurveyResponseStateItem) As SurveyResponseStateItem
        If State.SurveyResponseStateID < 1 Then
            Try
                Dim myResult = gsp_SurveyResponseState_Insert(State.SurveyResponseID,
                                               State.StatusID,
                                               State.AssignedUserID,
                                               State.Active,
                                               State.EmailSent,
                                               State.EmailBody,
                                               State.ModifiedID,
                                               Now()).Single()
                State.SurveyResponseStateID = myResult.SurveyResponseStateID
                State.ModifiedDT = myResult.ModifiedDT
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, String.Format("DataController.UpdateSurveyResponseState-Insert SurveyResponseID={0}", State.SurveyResponseID))
            End Try
        Else
            Try
                Dim myResult = gsp_SurveyResponseState_Update(State.SurveyResponseStateID,
                                               State.SurveyResponseID,
                                               State.StatusID,
                                               State.AssignedUserID,
                                               State.Active,
                                               State.EmailSent,
                                               State.EmailBody,
                                               State.ModifiedID,
                                               Now()).Single()

                State.ModifiedDT = myResult.ModifiedDT
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, String.Format("DataController.UpdateSurveyResponseState-Update SurveyResponseID={0}", State.SurveyResponseID))
            End Try
        End If
        Return State
    End Function
#End Region
#Region "Survey Email Templates"
    Function GetSurveyEmailTemplates(ByVal pSurveyID As Integer) As List(Of SurveyEmailTemplateItem)
        Return (From i In usp_SurveyEmailTemplate_SelectBySurveyID(pSurveyID)
                Select New SurveyEmailTemplateItem With {.SurveyEmailTemplateID = i.SurveyEmailTemplateID,
                                                         .SurveyEmailTemplateNM = i.SurveyEmailTemplateNM,
                                                         .SurveyID = i.SurveyID,
                                                         .StatusID = i.StatusID,
                                                         .SubjectTemplate = i.SubjectTemplate,
                                                         .BodyTemplate = i.EmailTemplate,
                                                         .FromEmailAddress = i.FromEmailAddress,
                                                         .FilterCriteria = i.FilterCriteria,
                                                         .StartDT = i.StartDT,
                                                         .EndDT = i.EndDT,
                                                         .IsActive = i.Active,
                                                         .SendToSupervisor = i.SendToSupervisor,
                                                         .ModifiedID = i.ModifiedID}).ToList
    End Function
    Function GetSurveyEmailTemplateBySurveyEmailTemplateID(ByVal SurveyEmailTemplateID As Integer) As SurveyEmailTemplateItem
        Try
            Return (From i In gsp_SurveyEmailTemplate_Select(SurveyEmailTemplateID)
                Select New SurveyEmailTemplateItem With {.SurveyEmailTemplateID = i.SurveyEmailTemplateID,
                                                         .SurveyEmailTemplateNM = i.SurveyEmailTemplateNM,
                                                         .SurveyID = i.SurveyID,
                                                         .StatusID = i.StatusID,
                                                         .SubjectTemplate = i.SubjectTemplate,
                                                         .BodyTemplate = i.EmailTemplate,
                                                         .FromEmailAddress = i.FromEmailAddress,
                                                         .FilterCriteria = i.FilterCriteria,
                                                         .StartDT = i.StartDT,
                                                         .EndDT = i.EndDT,
                                                         .IsActive = i.Active,
                                                         .SendToSupervisor = i.SendToSupervisor,
                                                         .ModifiedID = i.ModifiedID}).Single
        Catch ex As Exception
            Return New SurveyEmailTemplateItem With {.SurveyEmailTemplateID = -1}
        End Try
    End Function
    Function DeleteSurveyEmailTemplate(ByVal SurveyEmailTemplate As SurveyEmailTemplateItem) As Boolean
        If (gsp_SurveyEmailTemplate_Delete(SurveyEmailTemplate.SurveyEmailTemplateID) = 0) Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function
    Function UpdateSurveyEmailTemplate(ByVal SurveyEmailTemplate As SurveyEmailTemplateItem) As SurveyEmailTemplateItem
        If SurveyEmailTemplate.SurveyEmailTemplateID < 1 Then
            Try
                Dim InsertResult = gsp_SurveyEmailTemplate_Insert(SurveyEmailTemplate.SurveyEmailTemplateNM,
                                               SurveyEmailTemplate.SurveyID,
                                               SurveyEmailTemplate.StatusID,
                                               SurveyEmailTemplate.SubjectTemplate,
                                               SurveyEmailTemplate.BodyTemplate,
                                               SurveyEmailTemplate.FromEmailAddress,
                                               SurveyEmailTemplate.FilterCriteria,
                                               SurveyEmailTemplate.StartDT,
                                               SurveyEmailTemplate.EndDT,
                                               SurveyEmailTemplate.IsActive,
                                               SurveyEmailTemplate.SendToSupervisor,
                                               SurveyEmailTemplate.ModifiedID,
                                               Now()).Single
                SurveyEmailTemplate.SurveyEmailTemplateID = InsertResult.SurveyEmailTemplateID
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, String.Format("DataController.UpdateSurveyEmailTemplate-Insert SurveyEmailTemplateNM={0}", SurveyEmailTemplate.SurveyEmailTemplateNM))
            End Try
        Else
            Try
                gsp_SurveyEmailTemplate_Update(SurveyEmailTemplate.SurveyEmailTemplateID,
                                               SurveyEmailTemplate.SurveyEmailTemplateNM,
                                               SurveyEmailTemplate.SurveyID,
                                               SurveyEmailTemplate.StatusID,
                                               SurveyEmailTemplate.SubjectTemplate,
                                               SurveyEmailTemplate.BodyTemplate,
                                               SurveyEmailTemplate.FromEmailAddress,
                                               SurveyEmailTemplate.FilterCriteria,
                                               SurveyEmailTemplate.StartDT,
                                               SurveyEmailTemplate.EndDT,
                                               SurveyEmailTemplate.IsActive,
                                               SurveyEmailTemplate.SendToSupervisor,
                                               SurveyEmailTemplate.ModifiedID,
                                               Now())
                bReturn = True
            Catch ex As Exception
                AppLog.SQLError(ex.ToString, String.Format("DataController.UpdateSurveyEmailTemplate-Update SurveyEmailTemplateNM={0}", SurveyEmailTemplate.SurveyEmailTemplateNM))
            End Try
        End If
        Return SurveyEmailTemplate
    End Function
#End Region
#Region "Survey Review Status Methods"
    Function GetReviewStatuses(ByVal pSurveyID As Integer) As List(Of SurveyReviewStatusItem)
        Return (From i In usp_SurveyReviewStatus_SelectBySurveyID(pSurveyID)
                Select New SurveyReviewStatusItem With {.SurveyID = i.SurveyID,
                                                        .SurveyReviewStatusID = i.SurveyReviewStatusID,
                                                        .ReviewStatusID = i.ReviewStatusID,
                                                        .ReviewStatusNM = i.ReviewStatusNM,
                                                        .ReviewStatusDS = i.ReviewStatusDS,
                                                        .CommentFL = CBool(i.CommentFL),
                                                        .ApprovedFL = CBool(i.ApprovedFL),
                                                        .ModifiedID = i.ModifiedID}).ToList
    End Function
    Function GetReviewStatusByReviewStatusID(ByVal SurveyReviewStatusID As Integer) As SurveyReviewStatusItem
        Try
            Return (From i In gsp_SurveyReviewStatus_Select(SurveyReviewStatusID)
                    Select New SurveyReviewStatusItem With {.ReviewStatusID = i.ReviewStatusID,
                                                            .SurveyReviewStatusID = i.SurveyReviewStatusID,
                                                            .SurveyID = i.SurveyID,
                                                            .ReviewStatusNM = i.ReviewStatusNM,
                                                            .ReviewStatusDS = i.ReviewStatusDS,
                                                            .CommentFL = CBool(i.CommentFL),
                                                            .ApprovedFL = CBool(i.ApprovedFL),
                                                            .ModifiedID = i.ModifiedID}).Single
        Catch ex As Exception
            Return New SurveyReviewStatusItem With {.SurveyReviewStatusID = -1}
        End Try
    End Function
    Function DeleteReviewStatus(ByVal ReviewStatusItem As SurveyReviewStatusItem) As Boolean
        If (gsp_SurveyReviewStatus_Delete(ReviewStatusItem.SurveyReviewStatusID) = 0) Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function
    Function UpdateReviewStatus(ByVal ReviewStatusItem As SurveyReviewStatusItem) As SurveyReviewStatusItem
        If ReviewStatusItem.SurveyReviewStatusID < 1 Then
            Try
                Dim InsertResult = gsp_SurveyReviewStatus_Insert(ReviewStatusItem.SurveyID,
                                              ReviewStatusItem.ReviewStatusID,
                                              ReviewStatusItem.ReviewStatusNM,
                                              ReviewStatusItem.ReviewStatusDS,
                                              ReviewStatusItem.ApprovedFL,
                                              ReviewStatusItem.CommentFL,
                                              ReviewStatusItem.ModifiedID,
                                              Now()).Single
                ReviewStatusItem.SurveyReviewStatusID = InsertResult.SurveyReviewStatusID
            Catch ex As Exception
                AppLog.SQLError(ex.ToString(), "DataController.UpdateReviewStatus-Insert")

            End Try
        Else
            Try
                gsp_SurveyReviewStatus_Update(ReviewStatusItem.SurveyReviewStatusID,
                                              ReviewStatusItem.SurveyID,
                                              ReviewStatusItem.ReviewStatusID,
                                              ReviewStatusItem.ReviewStatusNM,
                                              ReviewStatusItem.ReviewStatusDS,
                                              ReviewStatusItem.ApprovedFL,
                                              ReviewStatusItem.CommentFL,
                                              ReviewStatusItem.ModifiedID,
                                              Now())
            Catch ex As Exception
                AppLog.SQLError(ex.ToString(), "DataController.UpdateReviewStatus-Insert")
            End Try
        End If
        Return ReviewStatusItem
    End Function


#End Region
#Region "Survey Status Methods"
    Function GetSurveyStatusItemList(pSurveyID As Integer) As List(Of SurveyStatusItem)
        Return (From i In usp_SurveyStatus_SelectBySurveyID(pSurveyID)
                Select New SurveyStatusItem With {.SurveyStatusID = i.SurveyStatusID,
                                                  .SurveyID = i.SurveyID,
                                                  .StatusID = i.StatusID,
                                                  .StatusNM = i.StatusNM,
                                                  .StatusDS = i.StatusDS,
                                                  .BodyTemplate = i.EmailTemplate,
                                                  .SubjectTemplate = i.EmailSubjectTemplate,
                                                  .ModifiedID = i.ModifiedID}).ToList

    End Function
    Function GetSurveyResponseStatusBySurveyResponseStatusID(pSurveyResponseStatusID As Integer) As SurveyStatusItem
        Return (From i In gsp_SurveyStatus_Select(pSurveyResponseStatusID)
                Select New SurveyStatusItem With {.SurveyStatusID = i.SurveyStatusID,
                                                  .SurveyID = i.SurveyID,
                                                  .StatusID = i.StatusID,
                                                  .StatusNM = i.StatusNM,
                                                  .StatusDS = i.StatusDS,
                                                  .BodyTemplate = i.EmailTemplate,
                                                  .SubjectTemplate = i.EmailSubjectTemplate,
                                                  .ModifiedID = i.ModifiedID}).Single
    End Function
    Function DeleteSurveyResponseStatus(ByVal thisSurveyResponseStatusItem As SurveyStatusItem) As Boolean
        If gsp_SurveyStatus_Delete(thisSurveyResponseStatusItem.SurveyStatusID) = 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function
    Function UpdateSurveyResponseStatus(ByVal SurveyStatus As SurveyStatusItem) As SurveyStatusItem
        If SurveyStatus.SurveyStatusID < 1 Then
            Try
                Dim InsertResults = gsp_SurveyStatus_Insert(SurveyStatus.SurveyID,
                                        SurveyStatus.StatusID,
                                        SurveyStatus.StatusNM,
                                        SurveyStatus.StatusDS,
                                        SurveyStatus.BodyTemplate,
                                        SurveyStatus.SubjectTemplate,
                                        SurveyStatus.PreviousStatusID,
                                        SurveyStatus.NextStatusID,
                                        SurveyStatus.ModifiedID,
                                        Now()).Single
                SurveyStatus.SurveyStatusID = InsertResults.SurveyStatusID
            Catch ex As Exception
                AppLog.SQLError(ex.ToString(), "DataController.UpdateSurveyResponseStatus-Insert")
            End Try
        Else
            Try
                gsp_SurveyStatus_Update(SurveyStatus.SurveyStatusID,
                                        SurveyStatus.SurveyID,
                                        SurveyStatus.StatusID,
                                        SurveyStatus.StatusNM,
                                        SurveyStatus.StatusDS,
                                        SurveyStatus.BodyTemplate,
                                        SurveyStatus.SubjectTemplate,
                                        SurveyStatus.PreviousStatusID,
                                        SurveyStatus.NextStatusID,
                                        SurveyStatus.ModifiedID,
                                        Now())
            Catch ex As Exception
                AppLog.SQLError(ex.ToString(), "DataController.UpdateSurveyResponseStatus-Insert")
            End Try
        End If
        Return SurveyStatus
    End Function
#End Region
#Region "Import History"
    Function GetImportHistoryList() As List(Of ImportHistoryItem)
        Return (From i In gsp_ImportHistory_Select(Nothing)
                Select New ImportHistoryItem With {.ImportHistoryID = i.ImportHistoryID,
                                                   .ImportType = i.ImportType,
                                                   .FileName = i.FileName,
                                                   .ImportLog = i.ImportLog,
                                                   .NumberOfRows = i.NumberOfRows,
                                                   .ModifiedID = i.ModifiedID,
                                                   .ModifiedDT = i.ModifiedDT}).ToList

    End Function
    Function GetImportHistory(ByVal pFileName As String) As List(Of ImportHistoryItem)
        Return (From i In usp_ImportHistory_SelectByFileName(pFileName)
                Select New ImportHistoryItem With {.ImportHistoryID = i.ImportHistoryID,
                                                   .ImportType = i.ImportType,
                                                   .FileName = i.FileName,
                                                   .ImportLog = i.ImportLog,
                                                   .NumberOfRows = i.NumberOfRows,
                                                   .ModifiedID = i.ModifiedID,
                                                   .ModifiedDT = i.ModifiedDT}).ToList
    End Function
    Function GetImportHistory(ByVal pImportHistoryID As Integer) As ImportHistoryItem
        Return (From i In gsp_ImportHistory_Select(pImportHistoryID)
                Select New ImportHistoryItem With {.ImportHistoryID = i.ImportHistoryID,
                                                   .ImportType = i.ImportType,
                                                   .FileName = i.FileName,
                                                   .ImportLog = i.ImportLog,
                                                   .NumberOfRows = i.NumberOfRows,
                                                   .ModifiedID = i.ModifiedID,
                                                   .ModifiedDT = i.ModifiedDT}).Single
    End Function
    Function DeleteImportHistory(ByVal ImportHistory As ImportHistoryItem) As Boolean
        If gsp_ImportHistory_Delete(ImportHistory.ImportHistoryID) = 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function
    Function UpdateImportHistory(ByVal ImportHistory As ImportHistoryItem) As Boolean

        bReturn = True
        If ImportHistory.ImportHistoryID > 1 Then
            Try
                gsp_ImportHistory_Update(ImportHistory.ImportHistoryID, ImportHistory.FileName, CStr(ImportHistory.NumberOfRows), ImportHistory.NumberOfRows, ImportHistory.ImportLog, ImportHistory.ModifiedID, Now())
            Catch ex As Exception
                bReturn = False
            End Try
        Else
            Try
                gsp_ImportHistory_Insert(ImportHistory.FileName, CStr(ImportHistory.NumberOfRows), ImportHistory.NumberOfRows, ImportHistory.ImportLog, ImportHistory.ModifiedID, Now())
            Catch ex As Exception
                bReturn = False
            End Try
        End If
        Return bReturn
    End Function
#End Region
#Region "Web Portal"
    Function GetWebPortalList() As List(Of WebPortalItem)
        Return (From i In gsp_WebPortal_Select(Nothing) Select New WebPortalItem With {.WebPortalID = i.WebPortalID,
                                                                                       .WebPortalDS = i.WebPortalDS,
                                                                                       .WebPortalNM = i.WebPortalNM,
                                                                                       .WebPortalURL = i.WebPortalURL,
                                                                                       .WebServiceURL = i.WebServiceURL,
                                                                                       .ActiveFL = i.ActiveFL,
                                                                                       .ModifiedID = i.ModifiedID,
                                                                                       .ModifiedDT = i.ModifiedDT}).ToList()



    End Function


#End Region
#Region "tblFiles Methods"
    Function GettblFilesList() As List(Of tblFilesItem)
        Return (From i In tblFiles_SelectAll() Order By i.Name
                Select New tblFilesItem With {.Id = i.id,
                                              .Name = i.Name,
                                              .ContentType = i.ContentType,
                                              .Data = AppUtility.GetDBByte(i.Data)}).ToList
    End Function
    Function GettblFilesBytblFilesID(ByVal pID As Integer) As tblFilesItem
        Dim myReturn As New tblFilesItem
        Dim myX = tblFiles_SelectRow(pID).SingleOrDefault
        With myX
            myReturn.Name = .Name
            myReturn.ContentType = .ContentType
            myReturn.Id = .id
            myReturn.Data = AppUtility.GetDBByte(.Data)
        End With
        Return myReturn
    End Function
    Function DeletetblFiles(ByVal thistblFiles As tblFilesItem) As Boolean
        If tblFiles_DeleteRow(thistblFiles.Id) = 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function
    Function UpdatetblFiles(ByVal thistblFiles As tblFilesItem) As tblFilesItem
        With thistblFiles
            If .Id > 0 Then
                Try
                    Dim Result = tblFiles_Update(.Id, .Name, .ContentType, .Data).SingleOrDefault
                Catch ex As Exception
                    AppLog.SQLError(ex.ToString, "DataController.UpdatetblFiles-Update")
                    bReturn = False
                End Try
            Else
                Try
                    Dim Result = tblFiles_Insert(.Name, .ContentType, .Data).SingleOrDefault
                    .Id = Result.id
                Catch ex As Exception
                    AppLog.SQLError(ex.ToString, "DataController.UpdatetblFiles-Insert")
                    bReturn = False
                End Try
            End If
        End With
        Return GettblFilesBytblFilesID(thistblFiles.Id)
    End Function
#End Region


End Class