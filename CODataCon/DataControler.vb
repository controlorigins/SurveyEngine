
Imports CODataCon.com.controlorigins.ws
Imports System.Collections.ObjectModel

Public Class DataControler
    Implements IDisposable

    Dim WithEvents myWS As Service = New Service
    Public ReadOnly Property myGUID As Guid
        Get
            Return Guid.Parse("85AAA903-3C57-4FB0-B91D-B46633C7C637")
        End Get
    End Property

    Public Sub Dispose() Implements IDisposable.Dispose
        If myWS IsNot Nothing Then
            myWS.Dispose()
            myWS = Nothing
        End If
    End Sub

#Region "ApplicationUser / SiteUser"
    Public Function GetSiteUserList() As List(Of ApplicationUserItem)
        Return myWS.GetSiteUserList(myGUID).ToList()
    End Function
    Function GetApplicationUserList() As List(Of ApplicationUserItem)
        Return myWS.GetApplicationUserList(myGUID).ToList
    End Function

    Event UserLoginResults(status As Boolean)
    Public Function UserLogin(UserName As String, UserPass As String) As ApplicationUserItem
        Return myWS.GetSiteUser(myGUID, UserName, UserPass)
    End Function
    Public Function CreateNewUser(firstname As String, lastname As String, email As String, password As String) As ApplicationUserItem
        Return myWS.CreateNewUser(myGUID, firstname, lastname, email, password)
    End Function
    Public Function Checkpassword(userid As Integer, pass As String) As Boolean
        Return myWS.Checkpassword(myGUID, userid, pass)
    End Function
    Public Function updatepassword(userid As Integer, newpass As String) As Boolean
        Return myWS.updatepassword(myGUID, userid, newpass)
    End Function
    Public Function RemoveUserFromApp(ByVal UserID As Integer, ByVal appID As Integer) As Boolean
        Return myWS.RemoveUserFromApp(myGUID, UserID, appID)
    End Function
    Public Function UnRegisterUserFromApp(ByVal UserID As Integer, ByVal appid As Integer) As Boolean
        Return myWS.UnRegisterUserFromApp(myGUID, UserID, appid)
    End Function
    Function GetApplicationUserByApplicationUserID(reqApplicationUserID As Integer) As ApplicationUserItem
        Return myWS.GetApplicationUserByApplicationUserID(myGUID, reqApplicationUserID)
    End Function
    Sub DeleteApplicationUser(applicationUserItem As ApplicationUserItem)
        myWS.DeleteApplicationUser(myGUID, applicationUserItem)
    End Sub
    Function UpdateApplicationUser(ByVal Applicationuser As ApplicationUserItem) As ApplicationUserItem
        Return myWS.PutApplicationUser(myGUID, Applicationuser)
    End Function
    Function UpdateApplicationUserRole(ByVal myApplicationUserRole As ApplicationUserRoleItem, ByRef sReturn As String) As ApplicationUserRoleItem
        Return myWS.PutApplicationUserRole(myApplicationUserRole, myGUID)
    End Function

#End Region

#Region "SiteRole"
    Public Function GetSiteRoleList() As List(Of SiteRoleItem)
        Return myWS.GetSiteRoleList(myGUID).ToList()
    End Function
#End Region

#Region "Application Item / SiteApp / ApplicationUserRole"
    Public Function GetApplicationUserByApplicationID(ByVal AppID As Integer) As List(Of ApplicationUserRoleItem)
        Dim myApp = GetApplicationByApplicationID(AppID)
        Return myApp.ApplicationUserList.ToList()
    End Function
    Public Function GetSiteAppListByUserID(ByVal UserID As Integer) As List(Of ApplicationItem)
        Return myWS.GetSiteAppListByUserID(myGUID, UserID).ToList()
    End Function
    Public Function PutApplicationItem(ByVal myOnlineSiteApp As ApplicationItem) As ApplicationItem
        Return myWS.PutApplicationItem(myOnlineSiteApp, myGUID)
    End Function
    Public Function SubscribeMeToApp(userid As Integer, appid As Integer) As Boolean
        Return myWS.SubscribeMeToApp(myGUID, userid, appid)
    End Function
    Public Function CloneSiteApp(ByVal curAppID As Integer, ByVal newAppName As String) As ApplicationItem
        Return myWS.CloneSiteApp(myGUID, curAppID, newAppName)
    End Function
    Sub DeleteApplication(delApplicationItem As ApplicationItem)
        myWS.DeleteApplication(delApplicationItem, myGUID)
    End Sub
    Function UpateApplicationSurvey(myApplicationSurvey As ApplicationSurveyItem) As ApplicationSurveyItem
        Return myWS.PutApplicationSurveyItem(myApplicationSurvey, myGUID)
    End Function
    Function GetApplicationByApplicationID(reqApplicationID As Integer) As ApplicationItem
        Return myWS.GetApplicationByApplicationID(reqApplicationID, myGUID)
    End Function
    Function UpdateApplication(myApplication As ApplicationItem) As ApplicationItem
        Return myWS.PutApplicationItem(myApplication, myGUID)
    End Function
    Function GetApplicationList() As List(Of ApplicationItem)
        Return myWS.GetApplicationList(myGUID).ToList()
    End Function
    Sub DeleteApplicationSurvey(applicationSurveyItem As ApplicationSurveyItem)
        myWS.DeleteApplicationSurveyItem(applicationSurveyItem, myGUID)
    End Sub
    Function DeleteApplicationUserRole(myApplicationUserRole As ApplicationUserRoleItem) As Integer
        Return myWS.DeleteApplicationUserRole(myApplicationUserRole, myGUID)
    End Function
#End Region

#Region "Site Property"
    Public Function SetProperty(AppID As Integer, PropertyKey As String, value As String) As Boolean
        Return myWS.SetProperty(myGUID, AppID, PropertyKey, value)
    End Function
    Public Function GetPropertyValue(AppID As Integer, PropertyKey As String) As String
        Return myWS.GetProperty(AppID,PropertyKey,myGUID).Value
    End Function
    Public Function GetProperty(AppID As Integer, PropertyKey As String) As PropertyItem
        Return myWS.GetProperty(AppID,PropertyKey,myGUID)
    End Function
    Public Function PutProperty(ByVal myProperty As PropertyItem) As PropertyItem
        Return myWS.PutProperty(myProperty,myGUID)
    End Function
    Public Function DeleteProperty(AppID As Integer, propertyKey As String) As Boolean
        Return myWS.DeleteProperty(myGUID, AppID, propertyKey)
    End Function
#End Region

#Region "Site Messages"
    Public Function GetSiteMessageList() As List(Of SiteMessageItem)
        Return myWS.GetSiteMessageList(myGUID).ToList()
    End Function
    Public Function GetSiteMessageByMessageID(ByVal MessageId As Integer) As SiteMessageItem
        Return myWS.GetSiteMessageByMessageID(myGUID, MessageId)
    End Function
    Public Function PutSiteMessage(ByVal myMessage As SiteMessageItem) As SiteMessageItem
        Return myWS.PutSiteMessage(myGUID, myMessage)
    End Function
    Public Function GetUserSentMessages(ByVal UserId As Integer) As List(Of SiteMessageItem)
        Return myWS.GetUserSentMessages(myGUID, UserId).ToList()
    End Function
    Public Function UserMessageOpened(ByVal myMessage As SiteMessageItem) As SiteMessageItem
        Return myWS.UserMessageOpened(myGUID, myMessage)
    End Function
    Public Function DeleteMessage(ByVal myMessage As SiteMessageItem) As Boolean
        Return myWS.DeleteMessage(myGUID, myMessage)
    End Function
    Public Function GetRelatedUsers(ByVal UserId As Integer) As List(Of ApplicationUserItem)
        Return myWS.GetRelatedUsers(myGUID, UserId).ToList()
    End Function

#End Region

#Region "Navigational Menu"
    Public Function GetNavigationMenuList() As List(Of NavigationMenuItem)
        Return myWS.GetNavigationMenuList(myGUID).ToList()
    End Function
    Public Function PutNavigationMenuItem(ByVal thisMenuItem As NavigationMenuItem) As NavigationMenuItem
        Return myWS.PutNavigationMenuItem(myGUID, thisMenuItem)
    End Function

    Public Function SetDefaultNavigationItem(ByVal reqSiteApp As ApplicationItem, ByVal NavMenuItemID As Integer) As ApplicationItem
        Return myWS.SetDefaultNavigationItem(myGUID, reqSiteApp, NavMenuItemID)
    End Function

    Public Function DeleteNavigationMenuItem(ByVal NavMenuItem As NavigationMenuItem) As Boolean
        Return myWS.DeleteNavigationMenuItem(myGUID, NavMenuItem)
    End Function

#End Region

#Region "Lookups"
    Function GetQuestionTypeList() As List(Of LookupItem)
        Return myWS.GetLookupList(LookupType.QuestionTypeList, myGUID).ToList
    End Function
    Function GetUnitOfMeasureList() As List(Of LookupItem)
        Return myWS.GetLookupList(LookupType.UnitOfMeasureList, myGUID).ToList
    End Function
    Function GetReviewRoleLevelList() As List(Of LookupItem)
        Return myWS.GetLookupList(LookupType.ReviewRoleLevelList, myGUID).ToList
    End Function
    Function GetSurveyResponseStatusList() As List(Of LookupItem)
        Return myWS.GetLookupList(com.controlorigins.ws.LookupType.SurveyResponseStatusList, myGUID).ToList
    End Function
    'Function GetSurveyTypes() As List(Of LookupItem)
    '    Return myWS.GetLookupList(com.controlorigins.ws.LookupType.SurveyTypeList, myGUID).ToList
    'End Function
    Function GetApplicationTypes() As List(Of LookupItem)
        Return myWS.GetLookupList(com.controlorigins.ws.LookupType.ApplicationTypeList, myGUID).ToList
    End Function
    Function GetSurveyLookupList() As List(Of LookupItem)
        Return myWS.GetLookupList(com.controlorigins.ws.LookupType.SurveyList, myGUID).ToList
    End Function
#End Region

#Region "Survey Response"
    Public Function GetSurveyResponseListByApplication(appid As Integer, appuserid As Integer) As SurveyResponseItem()
        Return myWS.GetSuveyResponseListByApplicationUserID(appid, appuserid, myGUID)
    End Function
    Public Function GetSurveyResponse(SRID As Integer) As SurveyResponseItem
        Return myWS.GetSurveyResponseItem(SRID, myGUID)
    End Function
    Function GetApplicationSurveyResponse_SelectBySurveyResponseID(SurveyResponseID As Integer) As SurveyResponseItem
        Return TryCast(myWS.GetSurveyResponseItem(SurveyResponseID, myGUID), SurveyResponseItem)
    End Function

    Function GetSurveyResponsesByApplicationUserForInput(ApplicationUserID As Integer, ApplicationID As Integer) As List(Of SurveyResponseItem)
        Throw New NotImplementedException
    End Function

    Function GetSurveyResponsesByApplicationUserForInput(ApplicationUserID As Integer, SurveyID As Integer, ApplicationID As Integer) As List(Of SurveyResponseItem)
        Throw New NotImplementedException
    End Function
    Function UpdateSurveyResponseState(dbSurveyResponse As SurveyResponseItem, ActivityDescription As String, bEmailSent As Boolean) As SurveyResponseStateItem
        Throw New NotImplementedException
    End Function
    Function DeleteSurveyResponse(ByVal mySurveyResponse As SurveyResponseItem) As Integer
        Return myWS.DeleteSurveyResponseItem(mySurveyResponse, myGUID)
    End Function

    Function PutSurveyResponseItem(ByVal thisSurveyResponse As SurveyResponseItem) As SurveyResponseItem
        Return myWS.PutSurveyResponseItem(thisSurveyResponse, myGUID)
    End Function
    'Function GetSurveyResponseCount(sWhere As String) As Integer
    '    Throw New NotImplementedException
    'End Function
    Function ResetSurveyResponse(thisSurveyResponse As SurveyResponseItem) As Integer
        Return myWS.ResetSurveyResponseItem(thisSurveyResponse, myGUID)
    End Function

#End Region

#Region "Survey"
    Function GetSurveyBySurveyID(SurveyID As Integer) As SurveyItem
        Return myWS.GetSurvey(SurveyID, myGUID)
    End Function

    Function GetSurveySummaries() As List(Of SurveyItem)
        Return myWS.GetSurveySummaries(myGUID).ToList()
    End Function
    Function UpdateSurvey(dbSurvey As SurveyItem) As SurveyItem
        Return myWS.PutSurveyItem(dbSurvey, myGUID)
    End Function
    Function DeleteSurvey(surveyItem As SurveyItem) As Boolean
        Return myWS.DeleteSurveyItem(surveyItem, myGUID)
    End Function
    Function ImportSurvey(newSurvey As SurveyItem, ApplicationID As Integer, DefaultRoleID As Integer) As SurveyItem
        Throw New NotImplementedException
    End Function
#End Region

#Region "Company"
    Function GetCompanyList() As List(Of CompanyItem)
        Return myWS.GetCompanyList(myGUID).ToList
    End Function
    Function GetCompanyByCompanyID(ByVal myCompanyID As Integer) As CompanyItem
        Return myWS.GetCompany(myCompanyID, myGUID)
    End Function
    Function PutCompany(ByVal myCompany As CompanyItem) As CompanyItem
        Return myWS.PutCompany(myCompany, myGUID)
    End Function
    Function DeleteCompany(ByVal myCompany As CompanyItem) As Boolean
        Return myWS.DeleteCompany(myCompany, myGUID)
    End Function

#End Region

#Region "Application Chart"
    Function GetApplicationChartList() As List(Of ApplicationChartItem)
        Return myWS.GetApplicationChartList(myGUID).ToList
    End Function
    Function GetApplicationChartByApplicationChartID(ByVal myApplicationChartID As Integer) As ApplicationChartItem
        Return myWS.GetApplicationChart(myApplicationChartID, myGUID)
    End Function
    Function PutApplicationChart(ByVal myApplicationChart As ApplicationChartItem) As ApplicationChartItem
        Return myWS.PutApplicationChart(myApplicationChart, myGUID)
    End Function
    Function DeleteApplicationChart(ByVal myApplicationChart As ApplicationChartItem) As Boolean
        Return myWS.DeleteApplicationChart(myApplicationChart, myGUID)
    End Function


#End Region
#Region "Application Type"
    Function GetApplicationTypeList() As List(Of ApplicationTypeItem)
        Return myWS.GetApplicationTypeList(myGUID).ToList
    End Function
    Function GetApplicationTypeByApplicationTypeID(ByVal myApplicationTypeID As Integer) As ApplicationTypeItem
        Return myWS.GetApplicationType(myApplicationTypeID, myGUID)
    End Function
    Function PutApplicationType(ByVal myApplicationType As ApplicationTypeItem) As ApplicationTypeItem
        Return myWS.PutApplicationType(myApplicationType, myGUID)
    End Function
    Function DeleteApplicationType(ByVal myApplicationType As ApplicationTypeItem) As Boolean
        Return myWS.DeleteApplicationType(myApplicationType, myGUID)
    End Function

#End Region

#Region "Survey Type"
    Function GetSurveyCategoryList() As List(Of SurveyTypeItem)
        Return myWS.GetSurveyCategoryList(myGUID).ToList
    End Function
    Function GetQuestionCategoryList() As List(Of SurveyTypeItem)
        Return myWS.GetQuestionCategoryList(myGUID).ToList
    End Function

    Function GetSurveyCategoryListByApplicationTypeID(ByVal reqApplicationTypeID As Integer) As List(Of SurveyTypeItem)
        Return myWS.GetSurveyCategoryListByApplicationTypeID(reqApplicationTypeID, myGUID).ToList()
    End Function
    Function GetQuestionCategoryListByParentCategoryID(ByVal reqSurveyTypeID As Integer) As List(Of SurveyTypeItem)
        If reqSurveyTypeID = 0 Then
            Return New List(Of SurveyTypeItem)()
        Else
            Return (From i In myWS.GetQuestionCategoryList(myGUID) Where i.ParentSurveyTypeID = reqSurveyTypeID).ToList()
        End If
    End Function


    Function GetSurveyTypeBySurveyTypeID(ByVal mySurveyTypeID As Integer) As SurveyTypeItem
        Return myWS.GetSurveyType(mySurveyTypeID, myGUID)
    End Function
    Function PutSurveyType(ByVal mySurveyType As SurveyTypeItem) As SurveyTypeItem
        Return myWS.PutSurveyType(mySurveyType, myGUID)
    End Function
    Function DeleteSurveyType(ByVal mySurveytype As SurveyTypeItem) As Boolean
        Return myWS.DeleteSurveyType(mySurveytype, myGUID)
    End Function

#End Region

    Function GetRoles() As List(Of RoleItem)
        Return myWS.GetRoles(myGUID).ToList
    End Function

#Region "Question Item Web Service Calls"

    Function GetQuestionList() As List(Of QuestionItem)
        Dim Filters(-1) As SQLFilterClause
        Return myWS.GetQuestions(Filters, myGUID).ToList()
    End Function
    Function GetQuestionByQuestionID(ByVal QuestionID) As QuestionItem
        If QuestionID > 0 Then
            Return myWS.GetQuestionItem(QuestionID, myGUID)
        Else
            Return New QuestionItem With {.QuestionID = -1}
        End If
    End Function
    Function DeleteQuestionByQuestionID(ByVal QuestionID) As Boolean
        Dim myQuestion = myWS.GetQuestionItem(QuestionID, myGUID)
        If QuestionID > 0 Then
            Return myWS.DeleteQuestionItem(myQuestion, myGUID)
        Else
            Return False
        End If
    End Function
    Function GetQuestionByQuestionShortNM(ByVal QuestionShortNM As String) As QuestionItem
        If Not String.IsNullOrEmpty(QuestionShortNM) Then
            Return myWS.GetQuestionByQuestionShortNM(QuestionShortNM, myGUID)
        Else
            Return New QuestionItem With {.QuestionID = -1}
        End If
    End Function


    Function PutQuestionItem(ByVal myQuestionItem) As QuestionItem
        Return myWS.PutQuestionItem(myQuestionItem, myGUID)
    End Function
#End Region


#Region "tblFiles"
    Function GetFileList() As List(Of tblFilesItem)
        Return myWS.GettblFilesList(myGUID).ToList
    End Function
    Function GetFileByID(ByVal mytblFilesID As Integer) As tblFilesItem
        Return myWS.GettblFiles(mytblFilesID, myGUID)
    End Function
    Function PutFile(ByVal mytblFiles As tblFilesItem) As tblFilesItem
        Return myWS.PuttblFiles(mytblFiles, myGUID)
    End Function
    Function DeleteFile(ByVal mytblFiles As tblFilesItem) As Boolean
        Return myWS.DeletetblFiles(mytblFiles, myGUID)
    End Function

#End Region

End Class