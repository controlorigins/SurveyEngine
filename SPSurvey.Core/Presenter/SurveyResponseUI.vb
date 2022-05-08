Imports System.Text
Imports ControlOrigins.COUtility

Public Class SurveyResponseUI
    Inherits UIPresenterBase

    Private SurveyResponse As ISurveyResponse
    Private SurveyResponseList As ISurveyResponseList
    Private SurveyResponseStatusList As ISurveyStatusLookupList
    Private LookupItemList As ILookupItemList

    Public Sub New(ByVal connection As String)
        MyBase.New(connection)
    End Sub
    Sub SetSurveyResponseListUI(yourUI As Object)
        SurveyResponseList = CType(yourUI, ISurveyResponseList)
    End Sub

    Sub SetSurveyResponseUI(yourUI As Object)
        SurveyResponse = CType(yourUI, ISurveyResponse)
    End Sub

    Sub SetLookupItemListUI(yourUI As Object)
        LookupItemList = CType(yourUI, ILookupItemList)
    End Sub
    Sub GetSurveyResponseStatusList()
        SurveyResponseStatusList.LookupList = myController.GetSurveyResponseStatusList()
    End Sub
    Sub GetSurveyResponseSummary(ByVal StartRow As Integer, ByVal PageSize As Integer, ByVal WhereClause As String)
        SurveyResponseList.SurveyResponseList = myController.GetApplicationSurveyResponseSummary(StartRow, PageSize, WhereClause)
    End Sub
    Sub GetSurveyResponseDetail(ByVal StartRow As Integer, ByVal PageSize As Integer, ByVal WhereClause As String)
        SurveyResponseList.SurveyResponseList = myController.GetApplicationSurveyResponseSummary(StartRow, PageSize, WhereClause)
    End Sub
    Sub GetSurveyResponse(ByVal SurveyResponseID As Integer)
        SurveyResponse = myController.GetApplicationSurveyResponse_SelectBySurveyResponseID(SurveyResponseID)
    End Sub
    Public Function GetSurveyResponseItemBySurveyResponseID(ByVal SurveyResponseID As Integer) As SurveyResponseItem
        Return CType(myController.GetApplicationSurveyResponse_SelectBySurveyResponseID(SurveyResponseID), SurveyResponseItem)
    End Function

    Sub GetSurveyResponseListByApplication(ByVal AppliationID As Integer, ByVal GetDetails As Boolean, ByVal SurveyResponseID As Integer)
        SurveyResponseList.SurveyResponseList = myController.GetSurveyResponseListByApplication(AppliationID, GetDetails, SurveyResponseID)
    End Sub

    Sub GetCompleteSurveyResponseList(ByVal ApplicationUserID As Integer, ByVal ApplicationID As Integer)
        SurveyResponseList.SurveyResponseList = myController.GetSurveyResponseListByUser(ApplicationUserID, ApplicationID)
    End Sub

    Sub GetSurveyResponsesByApplicationUserID(ByVal ApplicationID As Integer, ByVal ApplicationUserID As Integer)
        SurveyResponseList.SurveyResponseList = myController.GetSurveyResponsesByApplicationUserForInput(ApplicationID, ApplicationUserID)
    End Sub

    Sub GetSurveyResponsesByApplicationUserID(ByVal ApplicationUserID As Integer, ByVal SurveyID As Integer, ByVal ApplicationID As Integer)
        If SurveyID > 0 Then
            SurveyResponseList.SurveyResponseList = myController.GetSurveyResponsesByApplicationUserForInput(ApplicationUserID, SurveyID, ApplicationID)
        Else
            SurveyResponseList.SurveyResponseList = myController.GetSurveyResponsesByApplicationUserForInput(ApplicationUserID, ApplicationID)
        End If
    End Sub

    Function GetSurveyResponseCount(ByVal sWhere As String) As Integer
        Return myController.GetSurveyResponseCount(sWhere)
    End Function

    Function SaveAssignedSurveyResponseItem(ByVal dbSurveyResponse As SurveyResponseItem, ByVal SurveyEmailTemplate As SurveyEmailTemplateItem, ByVal AppEmailConfig As ApplicationEmailConfiguration, ByVal AssignmentType As String) As SurveyResponseItem
        Dim EmailList As New EmailItemList
        Try
            dbSurveyResponse = myController.UpdateSurveyResponse(dbSurveyResponse)
            '            EmailList.Add(SendViaEmail(dbSurveyResponse.GetEmailItem(AppEmailConfig, SurveyEmailTemplate), AppEmailConfig))
            dbSurveyResponse.StateList.Add(SaveSurveyResponseState(dbSurveyResponse, String.Format("{1} Assigned Response - StatusID={0}", dbSurveyResponse.StatusID, AssignmentType), True))
        Catch ex As Exception
            ApplicationLogging.ErrorLog(ex.ToString, "SurveyResponseUI.SaveAssignedSurveyResponseItem")
        End Try
        EmailList.SaveXML(dbSurveyResponse.SurveyResponseNM)
        Return dbSurveyResponse
    End Function
    Function SaveSurveyResponseState(ByVal dbSurveyResponse As SurveyResponseItem, ByVal ActivityDescription As String, ByVal bEmailSent As Boolean) As SurveyResponseStateItem
        Return myController.UpdateSurveyResponseState(dbSurveyResponse, ActivityDescription, bEmailSent)
    End Function

    Sub SaveSurveyResponseItem(ByVal initStatusID As Integer, ByVal AppEmailConfig As ApplicationEmailConfiguration)
        Dim EmailList As New EmailItemList
        Dim bEmailSent As Boolean = False
        Dim mySR As SurveyResponseItem
        If SurveyResponse.SurveyResponseID < 1 Then
            mySR = New SurveyResponseItem
            mySR.ApplicationID = SurveyResponse.ApplicationID
            mySR.Survey.SurveyID = SurveyResponse.Survey.SurveyID
            mySR.AssignedUserID = SurveyResponse.AssignedUserID
            mySR.ModifiedID = SurveyResponse.ModifiedID
            mySR.ModifiedDT = Now()
        Else
            mySR = myController.GetApplicationSurveyResponse_SelectBySurveyResponseID(SurveyResponse.SurveyResponseID)
        End If
        With SurveyResponse

            ' Changing these values requires a higher administration permission (There needs to be a separate function that check permissions to change these things)
            ' mySR.ApplicationID = .ApplicationID
            ' mySR.SurveyResponseID = .SurveyResponseID
            mySR.Survey = .Survey
            mySR.AssignedUserID = .AssignedUserID
            '
            '
            '
            If initStatusID <> .StatusID Then
                mySR.StatusID = .StatusID
            End If
            If Not String.IsNullOrEmpty(.SurveyResponseNM) Then
                mySR.SurveyResponseNM = .SurveyResponseNM
            End If
            mySR.ModifiedID = .ModifiedID
            mySR.ModifiedDT = Now()
            If String.IsNullOrEmpty(.DataSource) Then
                mySR.DataSource = "WS.CONTROLORIGINS.COM"
            Else
                mySR.DataSource = .DataSource
            End If
        End With
        myController.UpdateSurveyResponse(mySR)
        ' Dim EmailTemplate As SurveyEmailTemplateItem = mySR.GetSurveyStatusEmailTemplate()
        ' ###################################################################################################################
        ' ## Process the Survey Response Item checking the new Status ID vs the old Status ID and sending email if the 
        ' ## Status has changed
        ' ###################################################################################################################
        If initStatusID <> mySR.StatusID Then
            If (mySR.StatusID < 5) Then
                '  EmailList.Add(SendViaEmail(mySR.GetEmailItem(AppEmailConfig, EmailTemplate), AppEmailConfig))
                mySR.StateList.Add(SaveSurveyResponseState(mySR, String.Format("Save Response from StatusID={0} To:{1}", initStatusID, SurveyResponse.StatusID), True))
                bEmailSent = True
            End If
            If (mySR.StatusID = 5 And initStatusID > 1) Then
                ' ###################################################################################################################
                ' ## Send Completion Message ONLY if initial status is not Assigned
                ' ## This is to avoid sending 2 email messages when a user goes from Assigned to Complete (i.e. no variant answers)
                ' ###################################################################################################################
                ' EmailList.Add(SendViaEmail(mySR.GetEmailItem(AppEmailConfig, EmailTemplate), AppEmailConfig))
                mySR.StateList.Add(SaveSurveyResponseState(mySR, "Save Response - Complete", True))
                bEmailSent = True
            End If
            If (initStatusID = 1 AndAlso mySR.StatusID > 2) Then
                ' ###################################################################################################################
                ' ## Send Email the first time the Assignment is completed to the Assigned User 
                ' ## This is a message from the Survey configuration not the status configuration
                ' ###################################################################################################################
                ' EmailList.Add(SendViaEmail(mySR.GetCompletionEmailItem(AppEmailConfig, EmailTemplate), AppEmailConfig))
                mySR.StateList.Add(SaveSurveyResponseState(mySR, "Save Response - Initial Data Entry Complete", True))
                bEmailSent = True
            End If
        End If
        If Not (bEmailSent) Then
            mySR.StateList.Add(SaveSurveyResponseState(mySR, String.Format("Save Survey Response from StatusID={0} To:{1}", initStatusID, SurveyResponse.StatusID), False))
        End If
        'If EmailList.Count > 0 Then
        '    EmailList.SaveXML(mySR.SurveyResponseNM)
        'End If
    End Sub

    Sub GetSurveyResponseBySurveyResponseID()
        If SurveyResponse.SurveyResponseID <> -1 Then
            Dim dbSurveyResponse As SurveyResponseItemBL = CType(myController.GetApplicationSurveyResponse_SelectBySurveyResponseID(SurveyResponse.SurveyResponseID), SurveyResponseItemBL)
            With SurveyResponse
                .Survey = dbSurveyResponse.Survey
                .AnswerList = dbSurveyResponse.AnswerList
                .SequenceList = dbSurveyResponse.SequenceList
                .SurveyResponseHistory = dbSurveyResponse.SurveyResponseHistory
                .StateList = dbSurveyResponse.StateList
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
        myController.DeleteSurveyResponse(CType(myController.GetApplicationSurveyResponse_SelectBySurveyResponseID(SurveyResponse.SurveyResponseID), SurveyResponseItemBL))
    End Sub

    Public Sub ProcessReviewAnswers(ByVal QuestionGroupID As String, ByVal AppEmail As ApplicationEmailConfiguration)
        If SurveyResponse.NewAnswerReviewList Is Nothing Then
            AppLog.SurveyLog(String.Format("No Review Answers - SurveyResponseID={0}",
                                           SurveyResponse.SurveyResponseID),
                                       "CompleteSurveyResponse.ProcessReviewAnswers")
        Else
            For Each myReviewAnswerItem As SurveyResponseAnswerReviewItem In SurveyResponse.NewAnswerReviewList
                Select Case SurveyResponse.StatusID
                    Case 3
                        myReviewAnswerItem.ReviewLevel = 1
                    Case 4
                        myReviewAnswerItem.ReviewLevel = 2
                    Case Else
                        myReviewAnswerItem.ReviewLevel = 0
                End Select
                myController.UpdateSurveyResponseAnswerReview(myReviewAnswerItem)
            Next
        End If

        CreateSurveyReviewHistory(QuestionGroupID)
        SetSurveyResponseStatus(AppEmail)
    End Sub

    Public Sub SetNewAnswers(ByVal myNewAnswers As List(Of SurveyResponseAnswerItem))
        SurveyResponse.NewAnswerList = myNewAnswers
    End Sub


    Public Sub ProcessNewAnswers(ByVal QuestionGroupID As String, ByVal AppEmail As ApplicationEmailConfiguration)
        Dim myQuestion As QuestionItem
        Dim myCurrentAnswers As SurveyResponseAnswerListBL
        If SurveyResponse.NewAnswerList Is Nothing Then
            AppLog.SurveyLog(String.Format("No New Answers - SurveyResponseID={0}", SurveyResponse.SurveyResponseID), "SurveyResponseUI.ProcessNewAnswers")
        Else
            If SurveyResponse.Survey.QuestionList Is Nothing Then
                AppLog.SurveyLog(String.Format("No Questions -  SurveyResponseID={0}", SurveyResponse.SurveyResponseID), "SurveyResponseUI.ProcessNewAnswers")
            Else
                ' Loop over all Responses in the NewAnswers
                For Each myAnswerItem As SurveyResponseAnswerItem In SurveyResponse.NewAnswerList
                    myQuestion = QuestionListUtility.FindQuestionByQuestionID(myAnswerItem.QuestionID, SurveyResponse.Survey.QuestionList)
                    If myQuestion Is Nothing Then
                        AppLog.SurveyLog(String.Format("FindQuestionByQuestionID({0}) did not find question", myAnswerItem.QuestionID), "SurveyResponseUI.ProcessNewAnswers")
                    ElseIf myQuestion.QuestionTypeCD = "CALC" Then
                        ' Do Nothing
                    Else
                        myAnswerItem.AnswerType = myQuestion.AnswerDataType
                        myAnswerItem.SurveyResponseID = SurveyResponse.SurveyResponseID
                        myAnswerItem.ModifiedID = SurveyResponse.ModifiedID

                        If myQuestion.QuestionTypeCD = "CBL" Then
                            myCurrentAnswers = New SurveyResponseAnswerListBL(SurveyResponse.AnswerList)
                            For Each myanswer In myCurrentAnswers.FindAnswersByQuestionID(myAnswerItem.QuestionID)
                                myController.DeleteSurveyResponseAnswer(myanswer)
                            Next
                            myAnswerItem.SurveyAnswerID = -1
                        End If

                        For Each strAnswer As String In myAnswerItem.ResponseList
                            myAnswerItem.QuestionAnswerID = -1
                            Select Case myQuestion.AnswerDataType
                                Case "QuestionAnswerID"
                                    ' Date/Quanitity null
                                    For Each Answer As QuestionAnswerItem In myQuestion.QuestionAnswerItemList
                                        If Answer.QuestionAnswerID = AppUtility.GetDBInteger(strAnswer) Then
                                            If Answer.QuestionAnswerCommentFL Then
                                                myAnswerItem.QuestionAnswerID = Answer.QuestionAnswerID
                                                ' Use this logic to prevent saving of a response when the answer requires a comment
                                                If String.IsNullOrEmpty(myAnswerItem.AnswerComment) Then
                                                    AppLog.SurveyLog(String.Format("A QuestionAnswer ({2}-{0} for SurveyResponseID {1}) that requires a comment, did NOT have a comment", myAnswerItem.QuestionAnswerID, myAnswerItem.SurveyResponseID, myAnswerItem.QuestionID), "SurveyResponseUI.ProcessNewAnswers")
                                                    myAnswerItem.QuestionAnswerID = Answer.QuestionAnswerID
                                                Else
                                                    myAnswerItem.QuestionAnswerID = Answer.QuestionAnswerID
                                                End If
                                            Else
                                                myAnswerItem.QuestionAnswerID = Answer.QuestionAnswerID
                                            End If
                                        End If
                                    Next
                                Case "Quantity"
                                    ' Date/comment null, only Quantity
                                    If strAnswer.Contains("%") Then
                                        strAnswer = strAnswer.Replace("%", "")
                                        strAnswer = CStr(CDec(strAnswer) / 100)
                                    End If
                                    If Trim(strAnswer) = String.Empty Then
                                        myAnswerItem.AnswerQuantity = 0
                                    Else
                                        If IsNumeric(strAnswer) Then
                                            myAnswerItem.AnswerQuantity = CType(strAnswer, Double)
                                        Else
                                            myAnswerItem.AnswerQuantity = 0
                                        End If
                                    End If
                                    myAnswerItem.QuestionAnswerID = myQuestion.QuestionAnswerItemList.Item(0).QuestionAnswerID
                                    Try
                                        myAnswerItem.AnswerQuantity = CType(myAnswerItem.ResponseList(0), Double)
                                    Catch ex As Exception
                                        myAnswerItem.AnswerQuantity = Nothing
                                    End Try
                                Case "Date"
                                    ' Quanity/Comment null, only date
                                    If Not (Trim(strAnswer) = String.Empty) Then
                                        myAnswerItem.AnswerComment = strAnswer
                                        If IsDate(strAnswer) Then
                                            myAnswerItem.AnswerDate = CType(strAnswer, DateTime)
                                        End If
                                    End If
                                    myAnswerItem.QuestionAnswerID = myQuestion.QuestionAnswerItemList.Item(0).QuestionAnswerID
                                    Try
                                        myAnswerItem.AnswerDate = CType(myAnswerItem.ResponseList(0), DateTime)
                                    Catch ex As Exception
                                        myAnswerItem.AnswerDate = Nothing
                                    End Try
                                Case "Comment"
                                    myAnswerItem.QuestionAnswerID = myQuestion.QuestionAnswerItemList.Item(0).QuestionAnswerID
                                    If myAnswerItem.ResponseList.Count = 1 Then
                                        myAnswerItem.AnswerComment = myAnswerItem.ResponseList(0).ToString()
                                    End If

                                Case Else
                                    AppLog.SurveyLog(String.Format("Unknown Question.AnswerDataType-{0}  SurveyResponseID={1}",
                                                                   myAnswerItem.AnswerType,
                                                                   SurveyResponse.SurveyResponseID),
                                                               "SurveyResponseUI.ProcessNewAnswers")
                            End Select
                            myAnswerItem.SurveyAnswerID = -1


                            If myAnswerItem.QuestionAnswerID > 0 Then
                                If myQuestion.QuestionTypeCD <> "CBL" Then
                                    For Each curAnswerItem In SurveyResponse.AnswerList
                                        If curAnswerItem.QuestionID = myAnswerItem.QuestionID And curAnswerItem.SequenceNumber = myAnswerItem.SequenceNumber Then
                                            myAnswerItem.SurveyAnswerID = curAnswerItem.SurveyAnswerID
                                        End If
                                    Next
                                End If
                                SurveyResponseAnswer_Update(myAnswerItem)
                            Else
                                If strAnswer <> String.Empty Then
                                    AppLog.SurveyLog(String.Format("QuestionAnswerID not found for ~{2}~ on QuestionID={0} on SurveyResponseID={1}",
                                                                   myAnswerItem.QuestionID,
                                                                   SurveyResponse.SurveyResponseID,
                                                                   strAnswer),
                                                               "SurveyResponseUI.ProcessNewAnswers")
                                End If
                            End If
                        Next
                    End If
                Next
                CreateSurveyResponseHistory(QuestionGroupID)
                SetSurveyResponseStatus(AppEmail)
            End If
        End If

    End Sub

    Public Sub SetSurveyResponseStatus(ByVal AppEmail As ApplicationEmailConfiguration)
        '  Check if All Questions have been answered / reviewed
        '  Update SurveyResponse StatusID 
        Dim oldStatusID As Integer = SurveyResponse.StatusID

        If SurveyResponse.SequenceList.Count > 1 Then
            SurveyResponse.StatusID = 1
        Else
            SurveyResponse.AnswerList.Clear()
            SurveyResponse.AnswerList = myController.GetSurveyResponseAnswersBySurveyResponseID(SurveyResponse.SurveyResponseID)
            If IsComplete() Then
                If NeedsReview() Then
                    If oldStatusID = 3 Then
                        SurveyResponse.StatusID = 2
                    Else
                        SurveyResponse.StatusID = 3
                    End If
                Else
                    If NeedsComplianceReview() Then
                        If oldStatusID = 4 Then
                            SurveyResponse.StatusID = 2
                        Else
                            SurveyResponse.StatusID = 4
                        End If
                    Else
                        SurveyResponse.StatusID = 5
                    End If
                End If
            Else
                If oldStatusID = 1 Then
                    SurveyResponse.StatusID = 1
                Else
                    SurveyResponse.StatusID = 2
                End If
            End If
        End If
        '  Set StatusNM If the current status is not available in this Survey, choose the next highest available status
        SurveyResponse.StatusNM = String.Empty
        For Each myStatus As SurveyStatusItem In (From i In SurveyResponse.Survey.StatusList Order By i.StatusID Select i)
            If myStatus.StatusID >= SurveyResponse.StatusID Then
                If myStatus.StatusID = SurveyResponse.StatusID Then
                    SurveyResponse.StatusNM = myStatus.StatusNM
                    Exit For
                Else
                    If SurveyResponse.StatusNM = String.Empty Then
                        SurveyResponse.StatusID = myStatus.StatusID
                        SurveyResponse.StatusNM = myStatus.StatusNM
                        Exit For
                    End If
                End If

            End If
        Next
        SaveSurveyResponseItem(oldStatusID, AppEmail)
    End Sub
    Public Function SurveyResponseAnsser_Delete(ByRef ThisSurveyResponseAnswerItem As SurveyResponseAnswerItem) As Boolean
        Return myController.DeleteSurveyResponseAnswer(ThisSurveyResponseAnswerItem)
    End Function
    Public Function SurveyResponseAnswer_Update(ByRef ThisSurveyResponseAnswerItem As SurveyResponseAnswerItem) As SurveyResponseAnswerItem
        If IsValidAnswerItem(ThisSurveyResponseAnswerItem) Then
            Return myController.UpdateSurveyResponseAnswer(ThisSurveyResponseAnswerItem)
        End If
        Return ThisSurveyResponseAnswerItem
    End Function

    Public Function IsValidAnswerItem(ByRef ThisSurveyResponseAnswerItem As SurveyResponseAnswerItem) As Boolean
        Dim bReturn As Boolean = False
        If ThisSurveyResponseAnswerItem.SequenceNumber = 0 Then
            AppLog.SurveyLog("SequenceNUmber is 0", "SurveyResponseAnswer.IsValidResponse")
        ElseIf ThisSurveyResponseAnswerItem.SurveyResponseID = 0 Then
            AppLog.SurveyLog("SurveyResponseID is 0", "SurveyResponseAnswer.IsValidResponse")
        ElseIf ThisSurveyResponseAnswerItem.QuestionAnswerID = 0 Then
            AppLog.SurveyLog("QuestionAnswerID is 0", "SurveyResponseAnswer.IsValidResponse")
        ElseIf ThisSurveyResponseAnswerItem.QuestionID = 0 Then
            AppLog.SurveyLog("QuestionID is 0", "SurveyResponseAnswer.IsValidResponse")
        Else
            bReturn = True
        End If
        Return bReturn
    End Function

    Public Sub CreateSurveyResponseHistory(ByVal thisQuestionGroupID As String)
        Dim GroupID As Integer = -1
        If IsNumeric(thisQuestionGroupID) Then
            GroupID = AppUtility.GetDBInteger(thisQuestionGroupID)
        End If
        If IsNumeric(SurveyResponse.ModifiedID) Then
            myController.gsp_SurveyResponseHistory_Insert(SurveyResponse.ModifiedID,
                                                 SurveyResponse.SurveyResponseID,
                                                 SurveyResponse.SurveyResponseNM,
                                                 SurveyResponse.StatusID,
                                                 GroupID,
                                                 SurveyResponse.AccountNM,
                                                 GetXML(SurveyResponse.NewAnswerList),
                                                 SurveyResponse.ModifiedID,
                                                 Now)
        End If
    End Sub

    Public Sub CreateSurveyReviewHistory(ByVal thisQuestionGroupID As String)
        Dim GroupID As Integer = -1
        If IsNumeric(thisQuestionGroupID) Then
            GroupID = AppUtility.GetDBInteger(thisQuestionGroupID)
        End If
        If IsNumeric(SurveyResponse.ModifiedID) Then
            myController.gsp_SurveyResponseHistory_Insert(SurveyResponse.ModifiedID,
                                                 SurveyResponse.SurveyResponseID,
                                                 SurveyResponse.SurveyResponseNM,
                                                 SurveyResponse.StatusID,
                                                 GroupID,
                                                 SurveyResponse.AccountNM,
                                                 GetXML(SurveyResponse.NewAnswerReviewList),
                                                 SurveyResponse.ModifiedID,
                                                 Now)
        End If
    End Sub

    Public Function NeedsComplianceReview() As Boolean
        Dim bReturn As Boolean = False
        Dim iMaxLevel As Integer = 0
        If SurveyResponse.Survey.ReviewStatusList.Count > 1 Then
            For Each myAnswer As SurveyResponseAnswerItem In SurveyResponse.AnswerList
                For Each myQ As QuestionItem In SurveyResponse.Survey.QuestionList
                    For Each myQA As QuestionAnswerItem In myQ.QuestionAnswerItemList
                        If myAnswer.QuestionAnswerID = myQA.QuestionAnswerID Then
                            If myQA.QuestionAnswerCommentFL Then
                                iMaxLevel = 0
                                If myAnswer.AnswerReviewList.Count = 0 Then
                                    bReturn = True
                                    Exit For
                                Else
                                    For Each myReview In myAnswer.AnswerReviewList
                                        If myReview.ReviewLevel > iMaxLevel Then
                                            iMaxLevel = myReview.ReviewLevel
                                        End If

                                        If Not myReview.ApprovedFL AndAlso myReview.ReviewLevel = 2 Then
                                            bReturn = True
                                            Exit For
                                        End If
                                    Next
                                End If
                                If iMaxLevel < 2 Then
                                    bReturn = True
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    If bReturn Then
                        Exit For
                    End If
                Next
                If bReturn Then
                    Exit For
                End If
            Next
        End If

        Return bReturn
    End Function

    Public Function NeedsReview() As Boolean
        Dim bReturn As Boolean = False
        If SurveyResponse.Survey.ReviewStatusList.Count > 0 Then
            For Each myAnswer As SurveyResponseAnswerItem In SurveyResponse.AnswerList
                For Each myQ As QuestionItem In SurveyResponse.Survey.QuestionList
                    For Each myQA As QuestionAnswerItem In myQ.QuestionAnswerItemList
                        If myAnswer.QuestionAnswerID = myQA.QuestionAnswerID Then
                            If myQA.QuestionAnswerCommentFL And myQ.ReviewRoleLevel = 1 Then
                                If myAnswer.AnswerReviewList.Count = 0 Then
                                    bReturn = True
                                    Exit For
                                Else
                                    For Each myReview As SurveyResponseAnswerReviewItem In myAnswer.AnswerReviewList
                                        If myReview.ReviewLevel = 1 AndAlso Not myReview.ApprovedFL Then
                                            bReturn = True
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    Next
                    If bReturn Then
                        Exit For
                    End If
                Next
                If bReturn Then
                    Exit For
                End If
            Next
        End If
        Return bReturn
    End Function

    Public Function IsComplete() As Boolean
        Dim bReturn As Boolean = True
        For Each myGroup As QuestionGroupItem In SurveyResponse.Survey.QuestionGroupList
            If Not IsGroupComplete(myGroup) Then
                bReturn = False
                Exit For
            End If
        Next
        Return bReturn
    End Function

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

    Public Sub EmailSurveyResponseItem(ByVal mySR As SurveyResponseItemBL, ByVal myTemplate As SurveyEmailTemplateItem, ByVal AppEmail As ApplicationEmailConfiguration)
        Try
            SendViaEmail(mySR.GetEmailItem(AppEmail, myTemplate), AppEmail)
            mySR.StateList.Add(SaveSurveyResponseState(mySR, "Admin - Send Email", True))
        Catch ex As Exception
            AppLog.ErrorLog("Error with EmailSurveyResponseItem", ex.ToString)
        End Try
    End Sub

    Public Function CreateSurveyResponse(ByRef SRList As List(Of SurveyResponseItem), ByRef reqUser As ApplicationUserRoleItem, ByRef reqSurvey As SurveyItem, ByVal SurveyResponseName As String) As SurveyResponseItem
        Dim newSR As New SurveyResponseItem
        Dim bActive As Boolean = False
        Dim iCount As Integer = 1
        For Each mySR As SurveyResponseItem In SRList
            If mySR.Survey.SurveyID = reqSurvey.SurveyID AndAlso mySR.AssignedUserID = reqUser.ApplicationUserID Then
                iCount = iCount + 1
                If mySR.StatusID < 5 Then
                    bActive = True
                    newSR = mySR
                End If
            End If
        Next

        For Each mySR As SurveyResponseItemBL In SRList
            If mySR.SurveyResponseNM = GetSurveyResponseNM(reqSurvey, reqUser, iCount) Then
                bActive = True
                newSR = mySR
                Exit For
            End If
        Next

        If Not bActive Then
            newSR.DataSource = "SurveyResponseUI."
            newSR.ApplicationID = reqUser.ApplicationID
            newSR.AssignedUserID = reqUser.ApplicationUserID

            If String.IsNullOrEmpty(SurveyResponseName) Then
                newSR.SurveyResponseNM = GetSurveyResponseNM(reqSurvey, reqUser, iCount)
            Else
                newSR.SurveyResponseNM = SurveyResponseName
            End If

            newSR.Survey.SurveyID = reqSurvey.SurveyID
            newSR.StatusID = 1
            newSR.ModifiedID = reqUser.ApplicationUserID
            newSR.ModifiedDT = Now()
            newSR = myController.UpdateSurveyResponse(newSR)
        End If
        Return newSR
    End Function


End Class
