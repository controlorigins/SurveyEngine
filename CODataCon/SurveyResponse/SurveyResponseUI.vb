Imports CODataCon.com.controlorigins.ws
Imports ControlOrigins.COUtility

Public Class SurveyResponseUI
    Inherits UIPresenterBase

    Private SurveyResponse As ISurveyResponse
    Private SurveyResponseList As ISurveyResponseList
    Private ManagerList As IManagerList
    Private SurveyResponseStatusList As ISurveyStatusLookupList
    Private LookupItemList As ILookupItemList
    Public Sub New()
        MyBase.New()
    End Sub

    Sub SetSurveyResponseListUI(yourUI As Object)
        SurveyResponseList = CType(yourUI, ISurveyResponseList)
    End Sub

    Sub SetSurveyResponseUI(yourUI As Object)
        SurveyResponse = CType(yourUI, ISurveyResponse)
    End Sub

    Sub SetMangerListUI(yourUI As Object)
        ManagerList = CType(yourUI, IManagerList)
        SurveyResponseStatusList = CType(yourUI, ISurveyStatusLookupList)
    End Sub

    Sub GetSurveyResponseStatusList()
        SurveyResponseStatusList.LookupList = myController.GetSurveyResponseStatusList()
    End Sub
    Function GetSurveyResponse(ByVal SurveyResponseID As Integer) As SurveyResponseItem
        Return myController.GetApplicationSurveyResponse_SelectBySurveyResponseID(SurveyResponseID)
    End Function
    Public Function GetSurveyResponseItemBySurveyResponseID(ByVal SurveyResponseID As Integer) As SurveyResponseItem
        Return myController.GetApplicationSurveyResponse_SelectBySurveyResponseID(SurveyResponseID)
    End Function
    Public Function GetSurveyResponsesByApplicationUserID(ByVal ApplicationID As Integer, ByVal ApplicationUserID As Integer) As List(Of SurveyResponseItem)
        Return myController.GetSurveyResponseListByApplication(ApplicationID, ApplicationUserID).ToList()
    End Function
    Sub GetSurveyResponsesByApplicationUserID(ByVal ApplicationUserID As Integer, ByVal SurveyID As Integer, ByVal ApplicationID As Integer)
        If SurveyID > 0 Then
            SurveyResponseList.SurveyResponseList = myController.GetSurveyResponsesByApplicationUserForInput(ApplicationUserID, SurveyID, ApplicationID)
        Else
            SurveyResponseList.SurveyResponseList = myController.GetSurveyResponsesByApplicationUserForInput(ApplicationUserID, ApplicationID)
        End If
    End Sub
    Sub GetSurveyResponseBySurveyResponseID()
        If SurveyResponse.SurveyResponseID <> -1 Then
            Dim dbSurveyResponse As SurveyResponseItem = myController.GetApplicationSurveyResponse_SelectBySurveyResponseID(SurveyResponse.SurveyResponseID)
            With SurveyResponse
                .Survey = dbSurveyResponse.Survey
                .AnswerList = dbSurveyResponse.AnswerList.ToList()
                .SequenceList = dbSurveyResponse.SequenceList.ToList()
                .SurveyResponseHistory = dbSurveyResponse.SurveyResponseHistory.ToList()
                .StateList = dbSurveyResponse.StateList.ToList()
                .SurveyResponseID = dbSurveyResponse.SurveyResponseID
                .SurveyResponseNM = dbSurveyResponse.SurveyResponseNM
                .DataSource = dbSurveyResponse.DataSource
                .ApplicationID = dbSurveyResponse.ApplicationID
                .AssignedUserID = dbSurveyResponse.AssignedUserID
                .AssignedSupervisorUserID = dbSurveyResponse.AssignedSupervisorUserID
                .StatusID = dbSurveyResponse.StatusID
                .StatusNM = dbSurveyResponse.StatusNM
                .ModifiedID = dbSurveyResponse.ModifiedID
                .Manager_Name = dbSurveyResponse.Manager_Name
                .Employee_FName = dbSurveyResponse.Employee_FName
                .Employee_LName = dbSurveyResponse.Employee_LName
                .AccountNM = dbSurveyResponse.AccountNM
                .SupervisorAccountNM = dbSurveyResponse.SupervisorAccountNM
                .ManagerUserID = dbSurveyResponse.ManagerUserID
                .QuestionCount = dbSurveyResponse.QuestionCount
                .AnswerCount = dbSurveyResponse.AnswerCount
                .VariantAnswersCount = dbSurveyResponse.VariantAnswersCount
                .ComplianceReviewCount = dbSurveyResponse.ComplianceReviewCount
                .PercentComplete = dbSurveyResponse.PercentComplete
            End With
        End If
    End Sub
    Sub DeleteSurveyResponseItem()
        myController.DeleteSurveyResponse(myController.GetApplicationSurveyResponse_SelectBySurveyResponseID(SurveyResponse.SurveyResponseID))
    End Sub
    Public Sub ProcessNewAnswers(ByVal QuestionGroupID As String)
        Dim mySurveyResponseBL As New SurveyResponseItem() With {.NewAnswerList = SurveyResponse.NewAnswerList.ToArray,
                                                                   .SurveyResponseID = SurveyResponse.SurveyResponseID,
                                                                   .CurrentQuestionGroupID = QuestionGroupID,
                                                                   .DataSource = SurveyResponse.DataSource,
                                                                   .ModifiedID = SurveyResponse.ModifiedID,
                                                                   .ModifiedDT = Now}
        If Not mySurveyResponseBL Is Nothing Then
            myController.PutSurveyResponseItem(mySurveyResponseBL)
        End If
    End Sub
    Public Function IsGroupComplete(ByVal myGroup As QuestionGroupItem) As Boolean
        Dim bReturn As Boolean = True
        Dim bAnswersRequired As Boolean = False
        Dim GroupAnswerList As New List(Of SurveyResponseAnswerItem)

        If (myGroup.DependentQuestionGroupID Is Nothing) Then
            bAnswersRequired = True
        Else
            '
            ' LINQ Query to get the Group Question Answers for the Dependant Group of the current Group
            '
            Dim dScore As Decimal = CType((From gqi In
                                           (From ai In SurveyResponse.AnswerList
                                            Join q In SurveyResponse.Survey.QuestionList
                                            On ai.QuestionID Equals q.QuestionID
                                            Where q.QuestionGroupMember.QuestionGroupID = myGroup.DependentQuestionGroupID
                                            Select ai).ToList()
                                        Select gqi).Sum(Function(i) i.QuestionAnswerValue * i.QuestionValue), Decimal)
            If dScore >= myGroup.DependentMinScore And dScore <= myGroup.DependentMaxScore Then
                bAnswersRequired = True
            End If
        End If

        If bAnswersRequired Then
            GroupAnswerList = (From ai In SurveyResponse.AnswerList
                      Join q In SurveyResponse.Survey.QuestionList On ai.QuestionID Equals q.QuestionID
                      Where q.QuestionGroupMember.QuestionGroupID = myGroup.QuestionGroupID
                      Select ai).ToList()

            For Each myQuestion As QuestionItem In SurveyResponse.Survey.QuestionList
                If myQuestion.QuestionGroupMember.QuestionGroupID = myGroup.QuestionGroupID Then
                    Dim myQuestionAnswerList As List(Of SurveyResponseAnswerItem) = (From i In GroupAnswerList
                                    Where i.QuestionID = myQuestion.QuestionID
                                    Select i).ToList()
                    If myQuestionAnswerList.Count() < 1 Then
                        bReturn = False
                        Exit For
                    Else
                        For Each QuestionAnswer As SurveyResponseAnswerItem In myQuestionAnswerList
                            For Each qaReview In QuestionAnswer.AnswerReviewList
                                If Not qaReview.ApprovedFL Then
                                    bReturn = False
                                    Exit For
                                End If
                            Next
                            If Not bReturn Then
                                Exit For
                            End If
                        Next
                    End If
                End If
            Next
            If bReturn Or (myGroup.DependentQuestionGroupID Is Nothing) Then
                For Each myAnswer As SurveyResponseAnswerItem In GroupAnswerList
                    For Each myQ As QuestionItem In SurveyResponse.Survey.QuestionList
                        If myQ.QuestionGroupMember.QuestionGroupID = myGroup.QuestionGroupID Then
                            For Each myQA As QuestionAnswerItem In myQ.QuestionAnswerItemList
                                If myAnswer.QuestionAnswerID = myQA.QuestionAnswerID _
                                    AndAlso myQA.QuestionAnswerCommentFL _
                                    AndAlso String.IsNullOrEmpty(myAnswer.AnswerComment) Then
                                    bReturn = False
                                    Exit For
                                End If
                            Next
                            If Not bReturn Then
                                Exit For
                            End If
                        End If
                    Next
                    If Not bReturn Then
                        Exit For
                    End If
                Next
            End If
        End If
        Return bReturn
    End Function
End Class
'Function GetSurveyResponseCount(ByVal sWhere As String) As Integer
'    Return myController.GetSurveyResponseCount(sWhere)
'End Function

'Function SaveAssignedSurveyResponseItem(ByVal dbSurveyResponse As SurveyResponseItem, ByVal AssignmentType As String) As SurveyResponseItem
'    Dim EmailList As New EmailItemList
'    Try
'        dbSurveyResponse = myController.PutSurveyResponseItem(dbSurveyResponse)
'        ' EmailList.Add(SendViaEmail(dbSurveyResponse.GetEmailItem(AppEmailConfig, SurveyEmailTemplate), AppEmailConfig))
'        dbSurveyResponse.StateList.Add(SaveSurveyResponseState(dbSurveyResponse, String.Format("{1} Assigned Response - StatusID={0}", dbSurveyResponse.StatusID, AssignmentType), True))
'    Catch ex As Exception
'        AppLog.ErrorLog(ex.ToString, "SurveyResponseUI.SaveAssignedSurveyResponseItem")
'    End Try
'    EmailList.SaveXML(dbSurveyResponse.SurveyResponseNM)
'    Return dbSurveyResponse
'End Function
'Function SaveSurveyResponseState(ByVal dbSurveyResponse As SurveyResponseItem, ByVal ActivityDescription As String, ByVal bEmailSent As Boolean) As SurveyResponseStateItem
'    Return myController.UpdateSurveyResponseState(dbSurveyResponse, ActivityDescription, bEmailSent)
'End Function
'Sub SaveSurveyResponseItem(ByVal initStatusID As Integer, ByVal AppEmailConfig As ApplicationEmailConfiguration)
'    Dim EmailList As New EmailItemList
'    Dim bEmailSent As Boolean = False
'    Dim mySR As SurveyResponseItem
'    If SurveyResponse.SurveyResponseID < 1 Then
'        mySR = New SurveyResponseItem
'    Else
'        mySR = myController.GetApplicationSurveyResponse_SelectBySurveyResponseID(SurveyResponse.SurveyResponseID)
'    End If
'    With SurveyResponse
'        mySR.ApplicationID = .ApplicationID
'        mySR.SurveyResponseNM = .SurveyResponseNM
'        mySR.SurveyResponseID = .SurveyResponseID
'        mySR.ModifiedID = .ModifiedID
'        mySR.Survey = .Survey
'        mySR.AssignedUserID = .AssignedUserID
'        mySR.StatusID = .StatusID
'        mySR.DataSource = .DataSource
'    End With
'    myController.UpdateSurveyResponse(mySR)
'    'Dim EmailTemplate As SurveyEmailTemplateItem = mySR.GetSurveyStatusEmailTemplate()
'    ' ###################################################################################################################
'    ' ## Process the Survey Response Item checking the new Status ID vs the old Status ID and sending email if the 
'    ' ## Status has changed
'    ' ###################################################################################################################
'    If initStatusID <> mySR.StatusID Then
'        If (mySR.StatusID < 5) Then
'            'EmailList.Add(SendViaEmail(mySR.GetEmailItem(AppEmailConfig, EmailTemplate), AppEmailConfig))
'            mySR.StateList.Add(SaveSurveyResponseState(mySR, String.Format("Save Response from StatusID={0} To:{1}", initStatusID, SurveyResponse.StatusID), True))
'            bEmailSent = True
'        End If
'        If (mySR.StatusID = 5 And initStatusID > 1) Then
'            ' ###################################################################################################################
'            ' ## Send Completion Message ONLY if initial status is not Assigned
'            ' ## This is to avoid sending 2 email messages when a user goes from Assigned to Complete (i.e. no variant answers)
'            ' ###################################################################################################################
'            'EmailList.Add(SendViaEmail(mySR.GetEmailItem(AppEmailConfig, EmailTemplate), AppEmailConfig))
'            mySR.StateList.Add(SaveSurveyResponseState(mySR, "Save Response - Complete", True))
'            bEmailSent = True
'        End If
'        If (initStatusID = 1 AndAlso mySR.StatusID > 2) Then
'            ' ###################################################################################################################
'            ' ## Send Email the first time the Assignment is completed to the Assigned User 
'            ' ## This is a message from the Survey configuration not the status configuration
'            ' ###################################################################################################################
'            'EmailList.Add(SendViaEmail(mySR.GetCompletionEmailItem(AppEmailConfig, EmailTemplate), AppEmailConfig))
'            mySR.StateList.Add(SaveSurveyResponseState(mySR, "Save Response - Initial Data Entry Complete", True))
'            bEmailSent = True
'        End If
'    End If
'    If Not (bEmailSent) Then
'        mySR.StateList.Add(SaveSurveyResponseState(mySR, String.Format("Save Survey Response from StatusID={0} To:{1}", initStatusID, SurveyResponse.StatusID), False))
'    End If
'    If EmailList.Count > 0 Then
'        EmailList.SaveXML(mySR.SurveyResponseNM)
'    End If
'End Sub

'Sub SaveSurveyResponseItem(ByVal initStatusID As Integer)
'    Dim EmailList As New EmailItemList
'    Dim bEmailSent As Boolean = False
'    Dim mySR As SurveyResponseItem
'    If SurveyResponse.SurveyResponseID < 1 Then
'        mySR = New SurveyResponseItem
'    Else
'        mySR = myController.GetApplicationSurveyResponse_SelectBySurveyResponseID(SurveyResponse.SurveyResponseID)
'    End If
'    With SurveyResponse
'        mySR.ApplicationID = .ApplicationID
'        mySR.SurveyResponseNM = .SurveyResponseNM
'        mySR.SurveyResponseID = .SurveyResponseID
'        mySR.ModifiedID = .ModifiedID
'        mySR.Survey = .Survey
'        mySR.AssignedUserID = .AssignedUserID
'        mySR.StatusID = .StatusID
'        mySR.DataSource = .DataSource
'    End With
'    myController.UpdateSurveyResponse(mySR)
'    ' Dim EmailTemplate As SurveyEmailTemplateItem = mySR.GetSurveyStatusEmailTemplate()
'    ' ###################################################################################################################
'    ' ## Process the Survey Response Item checking the new Status ID vs the old Status ID and sending email if the 
'    ' ## Status has changed
'    ' ###################################################################################################################
'    If initStatusID <> mySR.StatusID Then
'        If (mySR.StatusID < 5) Then
'            ' EmailList.Add(SendViaEmail(oWeb, mySR.GetEmailItem(AppEmailConfig, EmailTemplate), AppEmailConfig))
'            mySR.StateList.Add(SaveSurveyResponseState(mySR, String.Format("Save Response from StatusID={0} To:{1}", initStatusID, SurveyResponse.StatusID), True))
'            bEmailSent = True
'        End If
'        If (mySR.StatusID = 5 And initStatusID > 1) Then
'            ' ###################################################################################################################
'            ' ## Send Completion Message ONLY if initial status is not Assigned
'            ' ## This is to avoid sending 2 email messages when a user goes from Assigned to Complete (i.e. no variant answers)
'            ' ###################################################################################################################
'            ' EmailList.Add(SendViaEmail(oWeb, mySR.GetEmailItem(AppEmailConfig, EmailTemplate), AppEmailConfig))
'            mySR.StateList.Add(SaveSurveyResponseState(mySR, "Save Response - Complete", True))
'            bEmailSent = True
'        End If
'        If (initStatusID = 1 AndAlso mySR.StatusID > 2) Then
'            ' ###################################################################################################################
'            ' ## Send Email the first time the Assignment is completed to the Assigned User 
'            ' ## This is a message from the Survey configuration not the status configuration
'            ' ###################################################################################################################
'            'EmailList.Add(SendViaEmail(oWeb, mySR.GetCompletionEmailItem(AppEmailConfig, EmailTemplate), AppEmailConfig))
'            mySR.StateList.Add(SaveSurveyResponseState(mySR, "Save Response - Initial Data Entry Complete", True))
'            bEmailSent = True
'        End If
'    End If
'    If Not (bEmailSent) Then
'        mySR.StateList.Add(SaveSurveyResponseState(mySR, String.Format("Save Survey Response from StatusID={0} To:{1}", initStatusID, SurveyResponse.StatusID), False))
'    End If
'    If EmailList.Count > 0 Then
'        EmailList.SaveXML(mySR.SurveyResponseNM)
'    End If
'End Sub      'Public Sub ProcessReviewAnswers(ByVal QuestionGroupID As String)
'    If SurveyResponse.NewAnswerReviewList Is Nothing Then
'        AppLog.SurveyLog(String.Format("No Review Answers - SurveyResponseID={0}",
'                                       SurveyResponse.SurveyResponseID),
'                                   "CompleteSurveyResponse.ProcessReviewAnswers")
'    Else
'        For Each myReviewAnswerItem As SurveyResponseAnswerReviewItem In SurveyResponse.NewAnswerReviewList
'            Select Case SurveyResponse.StatusID
'                Case 3
'                    myReviewAnswerItem.ReviewLevel = 1
'                Case 4
'                    myReviewAnswerItem.ReviewLevel = 2
'                Case Else
'                    myReviewAnswerItem.ReviewLevel = 0
'            End Select
'            myController.UpdateSurveyResponseAnswerReview(myReviewAnswerItem)
'        Next
'    End If

'    CreateSurveyReviewHistory(QuestionGroupID)
'    ' SetSurveyResponseStatus(AppEmail)
'End Sub   'Private Sub CreateSurveyReviewHistory(ByVal thisQuestionGroupID As String)
'    Dim GroupID As Integer = -1
'    If IsNumeric(thisQuestionGroupID) Then
'        GroupID = CInt(thisQuestionGroupID)
'    End If
'    If IsNumeric(SurveyResponse.ModifiedID) Then
'        myController.gsp_SurveyResponseHistory_Insert(SurveyResponse.ModifiedID,
'                                             SurveyResponse.SurveyResponseID,
'                                             SurveyResponse.SurveyResponseNM,
'                                             SurveyResponse.StatusID,
'                                             GroupID,
'                                             SurveyResponse.AccountNM,
'                                             GetXML(SurveyResponse.NewAnswerReviewList),
'                                             SurveyResponse.ModifiedID,
'                                             Now)
'    End If
'End Sub


'Public Function NeedsComplianceReview() As Boolean
'    Dim bReturn As Boolean = False
'    Dim iMaxLevel As Integer = 0
'    For Each myAnswer As SurveyResponseAnswerItem In SurveyResponse.AnswerList
'        For Each myQ As QuestionItem In SurveyResponse.Survey.QuestionList
'            For Each myQA As QuestionAnswerItem In myQ.QuestionAnswerItemList
'                If myAnswer.QuestionAnswerID = myQA.QuestionAnswerID Then
'                    If myQA.QuestionAnswerCommentFL Then
'                        iMaxLevel = 0
'                        If myAnswer.AnswerReviewList.Count = 0 Then
'                            bReturn = True
'                            Exit For
'                        Else
'                            For Each myReview In myAnswer.AnswerReviewList
'                                If myReview.ReviewLevel > iMaxLevel Then
'                                    iMaxLevel = myReview.ReviewLevel
'                                End If

'                                If Not myReview.ApprovedFL AndAlso myReview.ReviewLevel = 2 Then
'                                    bReturn = True
'                                    Exit For
'                                End If
'                            Next
'                        End If
'                        If iMaxLevel < 2 Then
'                            bReturn = True
'                            Exit For
'                        End If
'                    End If
'                End If
'            Next
'            If bReturn Then
'                Exit For
'            End If
'        Next
'        If bReturn Then
'            Exit For
'        End If
'    Next
'    Return bReturn
'End Function

'Public Function NeedsReview() As Boolean
'    Dim bReturn As Boolean = False
'    For Each myAnswer As SurveyResponseAnswerItem In SurveyResponse.AnswerList
'        For Each myQ As QuestionItem In SurveyResponse.Survey.QuestionList
'            For Each myQA As QuestionAnswerItem In myQ.QuestionAnswerItemList
'                If myAnswer.QuestionAnswerID = myQA.QuestionAnswerID Then
'                    If myQA.QuestionAnswerCommentFL And myQ.ReviewRoleLevel = 1 Then
'                        If myAnswer.AnswerReviewList.Count = 0 Then
'                            bReturn = True
'                            Exit For
'                        Else
'                            For Each myReview As SurveyResponseAnswerReviewItem In myAnswer.AnswerReviewList
'                                If myReview.ReviewLevel = 1 AndAlso Not myReview.ApprovedFL Then
'                                    bReturn = True
'                                End If
'                            Next
'                        End If
'                    End If
'                End If
'            Next
'            If bReturn Then
'                Exit For
'            End If
'        Next
'        If bReturn Then
'            Exit For
'        End If
'    Next
'    Return bReturn
'End Function


'Public Function IsComplete() As Boolean
'    Dim bReturn As Boolean = True
'    For Each myGroup As QuestionGroupItem In SurveyResponse.Survey.QuestionGroupList
'        If Not IsGroupComplete(myGroup) Then
'            bReturn = False
'            Exit For
'        End If
'    Next
'    Return bReturn
'End Function   'Public Function CreateSurveyResponse(ByRef SRList As List(Of SurveyResponseItem), ByRef reqUser As ApplicationUserRoleItem, ByRef reqSurvey As SurveyItem, ByVal SurveyResponseName As String) As SurveyResponseItem
'    Dim newSR As New SurveyResponseItem
'    Dim bActive As Boolean = False
'    Dim iCount As Integer = 1
'    For Each mySR As SurveyResponseItem In SRList
'        If mySR.Survey.SurveyID = reqSurvey.SurveyID AndAlso mySR.AssignedUserID = reqUser.ApplicationUserID Then
'            iCount = iCount + 1
'            If mySR.StatusID < 5 Then
'                bActive = True
'                newSR = mySR
'            End If
'        End If
'    Next

'    For Each mySR As SurveyResponseItem In SRList
'        If mySR.SurveyResponseNM = GetSurveyResponseNM(reqSurvey, reqUser, iCount) Then
'            bActive = True
'            newSR = mySR
'            Exit For
'        End If
'    Next

'    If Not bActive Then
'        newSR.DataSource = "SurveyResponseUI."
'        newSR.ApplicationID = reqUser.ApplicationID
'        newSR.AssignedUserID = reqUser.ApplicationUserID

'        If String.IsNullOrEmpty(SurveyResponseName) Then
'            newSR.SurveyResponseNM = GetSurveyResponseNM(reqSurvey, reqUser, iCount)
'        Else
'            newSR.SurveyResponseNM = SurveyResponseName
'        End If

'        newSR.Survey.SurveyID = reqSurvey.SurveyID
'        newSR.StatusID = 1
'        newSR.ModifiedID = reqUser.ApplicationUserID
'        newSR.ModifiedDT = Now()
'        newSR = myController.UpdateSurveyResponse(newSR)
'    End If
'    Return newSR
'End Function

