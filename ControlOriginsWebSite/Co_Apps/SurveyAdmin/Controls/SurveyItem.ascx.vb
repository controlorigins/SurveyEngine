Imports CODataCon.com.controlorigins.ws
Imports CODataCon


Partial Class Co_Apps_SurveyAdmin_Controls_SurveyItem
    Inherits SurveyUserControlBase

    Public mySurvey As New  SurveyItem
    Public myGroup As New QuestionGroupItem With {.QuestionGroupID = -1}

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        hfSurveyID.Value = AppUtility.GetDBInteger(GetPageArgument("SurveyID").Second, 0)
        hfQuestionGroupID.Value = AppUtility.GetDBInteger(GetPageArgument("questiongroupid").Second, 0)
        hfQuestionGroupMemberID.Value = AppUtility.GetDBInteger(GetPageArgument("questiongroupmemberid").Second, 0)

        If Not IsPostBack Then
            Select Case GetPageArgument("action").Second
                Case "surveyview"
                    ddlSurveyType.Items.Clear()
                    For Each mySType In myCon.GetSurveyCategoryList()
                        ddlSurveyType.Items.Add(New ListItem With {.Value = mySType.SurveyTypeID, .Text = mySType.SurveyTypeNM})
                    Next
                    If AppUtility.GetDBInteger(hfSurveyID.Value) > 0 Then
                        Try
                            mySurvey = myCon.GetSurveyBySurveyID(hfSurveyID.Value)
                        Catch ex As Exception
                            mySurvey = New SurveyItem With {.SurveyID = -1}
                        End Try
                    Else
                        ' NEW SURVEY
                        mySurvey = New SurveyItem With {.SurveyID = -1}
                        dtList.Visible = False
                        dtQuestions.Visible = False
                        dtApplications.Visible = False
                        dtSurveyResponses.Visible = False
                    End If

                    Select Case GetPageArgument("subaction").Second
                        Case "questiongroupid"
                            dtApplications.Visible = False
                            dtList.Visible = False
                            dtQuestions.Visible = False
                            dtSurveyResponses.Visible = False
                            dtSurveyStatus.Visible = False
                            dtSurveyReviewStatus.Visible = False
                            pnlApplicatonDetail.Visible = False
                            pnlQuestionGroup.Visible = True

                            SetQuestionGroup((From i In mySurvey.QuestionGroupList Where i.QuestionGroupID = hfQuestionGroupID.Value Select i).SingleOrDefault)
                        Case "questiongroupmember"
                            dtApplications.Visible = False
                            dtList.Visible = False
                            dtQuestions.Visible = False
                            dtSurveyResponses.Visible = False
                            dtSurveyStatus.Visible = False
                            dtSurveyReviewStatus.Visible = False
                            pnlApplicatonDetail.Visible = False
                            pnlQuestionGroup.Visible = False
                            pnlGroupMember.Visible = True


                            Dim FullList = myCon.GetQuestionList()
                            Dim possibleQuestions As New List(Of LookupItem)


                            If hfQuestionGroupMemberID.Value = "-1" Then
                                possibleQuestions = (From y In FullList.Except((From i In FullList Where (From x In mySurvey.QuestionList Select x.QuestionID).Contains(i.QuestionID)).ToList()) Select New LookupItem With {.Value = y.QuestionID, .Name = String.Format("{0}:  {1}", y.QuestionShortNM, y.QuestionNM)}).ToList()
                                ddlMemberQuestion.Enabled = True
                            Else
                                possibleQuestions = (From i In FullList Where (From x In mySurvey.QuestionList Select x.QuestionID).Contains(i.QuestionID) Select New LookupItem With {.Value = i.QuestionID, .Name = String.Format("{0}:  {1}", i.QuestionShortNM, i.QuestionNM)}).ToList()
                                ddlMemberQuestion.Enabled = False
                            End If


                            ddlMemberQuestion.DataSource = possibleQuestions
                            ddlMemberQuestion.DataTextField = "Name"
                            ddlMemberQuestion.DataValueField = "Value"
                            ddlMemberQuestion.DataBind()

                            If hfQuestionGroupID.Value = String.Empty Or hfQuestionGroupID.Value = "-1" Then
                                hfQuestionGroupID.Value = (From i In mySurvey.QuestionList Where i.QuestionGroupMember.QuestionGroupMemberID = hfQuestionGroupMemberID.Value Select i.QuestionGroupMember.QuestionGroupID).SingleOrDefault
                            End If


                            Dim myGroup = (From i In mySurvey.QuestionGroupList Where i.QuestionGroupID = hfQuestionGroupID.Value Select i).SingleOrDefault
                            If Not myGroup Is Nothing Then
                                SetQuestionGroupMember((From i In myGroup.QuestionMembership Where i.QuestionGroupMemberID = hfQuestionGroupMemberID.Value Select i).SingleOrDefault)
                            End If

                        Case Else
                            dtApplications.Visible = True
                            dtList.Visible = True
                            dtQuestions.Visible = True
                            dtSurveyResponses.Visible = True
                            dtSurveyStatus.Visible = True
                            dtSurveyReviewStatus.Visible = True
                            pnlApplicatonDetail.Visible = True
                            pnlQuestionGroup.Visible = False
                            With mySurvey
                                hfSurveyID.Value = .SurveyID
                                If .SurveyID > 0 Then
                                    tbSurveyNM.Text = .SurveyNM
                                    tbSurveyDS.Text = .SurveyDS
                                    tbSurveyShortNM.Text = .SurveyShortNM
                                    tbCompletionMessage.Text = .CompletionMessage
                                    tbEndDate.Text = .EndDT
                                    tbStartDate.Text = .StartDT
                                    ddlSurveyType.SelectedValue = .SurveyType.SurveyTypeID


                                    tbTotalGroupWeight.Text = Math.Round(CDbl((From g In .QuestionGroupList Select g.QuestionGroupWeight).Sum()), 4).ToString("P")

                                    dtSurveyStatus.TableHeader.TableTitle = "Survey Status"
                                    dtSurveyStatus.TableHeader.DetailFieldName = "StatusNM"
                                    dtSurveyStatus.TableHeader.DetailKeyName = "SurveyStatusID"
                                    dtSurveyStatus.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyview&surveyid=" & .SurveyID & "&subaction=surveystatusid&surveystatusid={0}"
                                    dtSurveyStatus.BuildTable(dtSurveyStatus.TableHeader, .StatusList)

                                    dtSurveyReviewStatus.TableHeader.TableTitle = "Survey Review Status"
                                    dtSurveyReviewStatus.TableHeader.DetailFieldName = "ReviewStatusNM"
                                    dtSurveyReviewStatus.TableHeader.DetailKeyName = "ReviewStatusID"
                                    dtSurveyReviewStatus.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyview&surveyid=" & .SurveyID & "&subaction=reviewstatusid&reviewstatusid={0}"
                                    dtSurveyReviewStatus.BuildTable(dtSurveyReviewStatus.TableHeader, .ReviewStatusList)

                                    dtList.TableHeader.TableTitle = "Question Groups (<a href='/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyview&surveyid=" & .SurveyID & "&subaction=questiongroupid&questiongroupid=-1'> Add New Group</a>)"
                                    dtList.TableHeader.DetailFieldName = "QuestionGroupOrder"
                                    dtList.TableHeader.DetailDisplayName = "Order"
                                    dtList.TableHeader.DetailKeyName = "QuestionGroupID"
                                    dtList.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyview&surveyid=" & .SurveyID & "&subaction=questiongroupid&questiongroupid={0}"
                                    dtList.TableHeader.AddLinkHeaderItem("Group", "QuestionGroupNM", "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyview&surveyid=" & hfSurveyID.Value & "&subaction=questiongroupid&questiongroupid={0}", "QuestionGroupID")
                                    dtList.TableHeader.AddHeaderItem("Weight", "QuestionGroupWeight", False, DataGridVisualization.ControlOriginsWS.DisplayFormat.Percent)
                                    dtList.BuildTable(dtList.TableHeader, .QuestionGroupList)


                                    dtQuestions.TableHeader.TableTitle = "Questions"
                                    dtQuestions.TableHeader.DetailFieldName = "QuestionNM"
                                    dtQuestions.TableHeader.DetailKeyName = "QuestionGroupMember.QuestionGroupMemberID"
                                    dtQuestions.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyview&surveyid=" & .SurveyID & "&subaction=questiongroupmember&QuestionGroupMemberID={0}"
                                    dtQuestions.TableHeader.AddLinkHeaderItem("Group", "QuestionGroupMember.QuestionGroupNM", "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyview&surveyid=" & hfSurveyID.Value & "&subaction=questiongroupid&questiongroupid={0}", "QuestionGroupMember.QuestionGroupID")
                                    dtQuestions.TableHeader.AddHeaderItem("Member Weight", "QuestionGroupMember.QuestionWeight", True, DataGridVisualization.ControlOriginsWS.DisplayFormat.Percent)
                                    dtQuestions.TableHeader.AddHeaderItem("Display Order", "QuestionGroupMember.DisplayOrder", True, DataGridVisualization.ControlOriginsWS.DisplayFormat.Number)
                                    dtQuestions.BuildTable(dtQuestions.TableHeader, .QuestionList)

                                    dtApplications.TableHeader.TableTitle = "Applications"
                                    dtApplications.TableHeader.DetailFieldName = "Name"
                                    dtApplications.TableHeader.DetailKeyName = "Value"
                                    dtApplications.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=applicationview&applicationid={0}"
                                    dtApplications.BuildTable(dtApplications.TableHeader, .ApplicationLookup)

                                    dtSurveyResponses.TableHeader.TableTitle = "Survey Response"
                                    dtSurveyResponses.TableHeader.DetailFieldName = "SurveyResponseNM"
                                    dtSurveyResponses.TableHeader.DetailKeyName = "SurveyResponseID"
                                    dtSurveyResponses.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyresponseview&surveyresponseid={0}"
                                    dtSurveyResponses.TableHeader.AddHeaderItem("AnswerCount", "AnswerCount",True,DataGridVisualization.ControlOriginsWS.DisplayFormat.Number)
                                    dtSurveyResponses.TableHeader.AddHeaderItem("StatusNM", "StatusNM",False,DataGridVisualization.ControlOriginsWS.DisplayFormat.Text)
                                    dtSurveyResponses.TableHeader.AddHeaderItem("ApplicationNM", "ApplicationNM",False,DataGridVisualization.ControlOriginsWS.DisplayFormat.Text)
                                    dtSurveyResponses.TableHeader.AddHeaderItem("SurveyResponseScore", "SurveyResponseScore",True,DataGridVisualization.ControlOriginsWS.DisplayFormat.Float)
                                    dtSurveyResponses.BuildTableFromGrid(dtSurveyResponses.TableHeader, myDACon.GetSurveyResponseSummaryBySurveyID(.SurveyID))
                                End If
                            End With
                    End Select
                Case Else
                    mySurvey = New SurveyItem With {.SurveyID = -1}
            End Select
        End If

    End Sub

    Protected Sub cmd_SaveSurvey_Click(sender As Object, e As EventArgs)
        With mySurvey
            .SurveyID = hfSurveyID.Value
            .SurveyNM = tbSurveyNM.Text
            .SurveyShortNM = tbSurveyShortNM.Text
            .SurveyDS = tbSurveyDS.Text
            .UseSurveyGroupsFL = False
            .AutoAssignFilter = String.Empty
            .CompletionMessage = tbCompletionMessage.Text
            .StartDT = GetDBDate(tbStartDate.Text)
            .EndDT = GetDBDate(tbEndDate.Text)
            .ParentSurveyID = New Integer?
            .SurveyType = New SurveyTypeItem With {.SurveyTypeID = ddlSurveyType.SelectedValue}
            .ModifiedID = 9999
            .ResponseNMTemplate = String.Empty
            .ReviewerAccountNM = String.Empty
        End With
        mySurvey = myCon.UpdateSurvey(mySurvey)
        ClearPageArguments()
        SetPageArgument("surveyid", mySurvey.SurveyID)
        SetPageArgument("action", "surveyview")
        LoadPage()

    End Sub

    Protected Sub cmd_CancelSurvey_Click(sender As Object, e As EventArgs)
        ClearPageArguments()
        SetPageArgument("surveyid", String.Empty)
        SetPageArgument("action", "surveyview")
        LoadPage()
    End Sub

    Protected Sub cmd_DeleteSurvey_Click(sender As Object, e As EventArgs)
        With mySurvey
            .SurveyID = hfSurveyID.Value
            .SurveyNM = tbSurveyNM.Text
            .SurveyShortNM = tbSurveyShortNM.Text
            .SurveyDS = tbSurveyDS.Text
            .UseSurveyGroupsFL = False
            .AutoAssignFilter = String.Empty
            .CompletionMessage = tbCompletionMessage.Text
            .StartDT = GetDBDate(tbStartDate.Text)
            .EndDT = GetDBDate(tbEndDate.Text)
            .ParentSurveyID = New Integer?
            .SurveyType = New SurveyTypeItem With {.SurveyTypeID = ddlSurveyType.SelectedValue}
            .ModifiedID = 9999
            .ResponseNMTemplate = String.Empty
            .ReviewerAccountNM = String.Empty
        End With
        myCon.DeleteSurvey(mySurvey)
        ClearPageArguments()
        SetPageArgument("surveyid", 0)
        SetPageArgument("action", "surveyview")
        LoadPage()
    End Sub


#Region "Question Group Administration"
    Public Sub SetQuestionGroup(ByVal QuestionGroup As QuestionGroupItem)
        If QuestionGroup Is Nothing Then
            hfQuestionGroupID.Value = "-1"
        Else
            With QuestionGroup
                tbQuestionGroupNM.Text = .QuestionGroupNM
                tbQuestionGroupShortNM.Text = .QuestionGroupShortNM
                tbQuestionGroupDS.Text = .QuestionGroupDS
                tbGroupFooter.Text = .QuestionGroupFooter
                tbGroupHeader.Text = .QuestionGroupHeader
                tbGroupOrder.Text = .QuestionGroupOrder
                tbGroupWeight.Text = .QuestionGroupWeight
                hfQuestionGroupID.Value = .QuestionGroupID

                tbTotalQuestionWeight.Text = Math.Round(CDbl((From g In .QuestionMembership Select g.QuestionWeight).Sum()), 4).ToString("P")


                dtQuestionGroupMember.TableHeader.TableTitle = "Questions in this Group (<a href='/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyview&subaction=questiongroupmember&surveyid=" & mySurvey.SurveyID & "&questiongroupid=" & .QuestionGroupID & "&questiongroupmemberid=-1'> Add Question</a>)"
                dtQuestionGroupMember.TableHeader.DetailFieldName = "DisplayOrder"
                dtQuestionGroupMember.TableHeader.DetailDisplayName = "Order"
                dtQuestionGroupMember.TableHeader.DetailKeyName = "QuestionGroupMemberID"
                dtQuestionGroupMember.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyview&subaction=questiongroupmember&surveyid=" & mySurvey.SurveyID & "&questiongroupid=" & .QuestionGroupID & "&questiongroupmemberid={0}"
                dtQuestionGroupMember.TableHeader.AddLinkHeaderItem("Question",
                                                                    "QuestionNM",
                                                                     "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyview&subaction=questiongroupmember&surveyid=" & mySurvey.SurveyID & "&questiongroupid=" & .QuestionGroupID & "&questiongroupmemberid={0}", "QuestionGroupMemberID")
                dtQuestionGroupMember.TableHeader.AddHeaderItem("Short Name", "QuestionShortNM")
                dtQuestionGroupMember.TableHeader.AddHeaderItem("Weight", "QuestionWeight", True, DataGridVisualization.ControlOriginsWS.DisplayFormat.Percent)
                dtQuestionGroupMember.BuildTable(dtQuestionGroupMember.TableHeader, .QuestionMembership)

            End With
        End If
    End Sub
    Public Function GetQuestionGroup() As QuestionGroupItem
        Dim questionGroup As New QuestionGroupItem
        With questionGroup
            .SurveyID = mySurvey.SurveyID
            .QuestionGroupNM = tbQuestionGroupNM.Text
            .QuestionGroupShortNM = tbQuestionGroupShortNM.Text
            .QuestionGroupDS = tbQuestionGroupDS.Text
            .QuestionGroupFooter = tbGroupFooter.Text
            .QuestionGroupHeader = tbGroupHeader.Text
            .QuestionGroupOrder = tbGroupOrder.Text
            .QuestionGroupWeight = tbGroupWeight.Text
            .QuestionGroupID = hfQuestionGroupID.Value
        End With
        Return questionGroup
    End Function
    Protected Sub cmd_SaveQuestionGroup_Click(sender As Object, e As EventArgs)
        mySurvey = myCon.GetSurveyBySurveyID(AppUtility.GetDBInteger(hfSurveyID.Value))
        If hfQuestionGroupID.Value = "-1" Then
            mySurvey.QuestionGroupList.Add(GetQuestionGroup)
        Else
            For gIndex = 0 To mySurvey.QuestionGroupList.Count - 1
                If mySurvey.QuestionGroupList(gIndex).QuestionGroupID = hfQuestionGroupID.Value Then
                    With mySurvey.QuestionGroupList(gIndex)
                        .QuestionGroupNM = tbQuestionGroupNM.Text
                        .QuestionGroupShortNM = tbQuestionGroupShortNM.Text
                        .QuestionGroupDS = tbQuestionGroupDS.Text
                        .QuestionGroupFooter = tbGroupFooter.Text
                        .QuestionGroupHeader = tbGroupHeader.Text
                        .QuestionGroupOrder = tbGroupOrder.Text
                        .QuestionGroupWeight = tbGroupWeight.Text
                    End With
                End If
            Next
        End If
        mySurvey = myCon.UpdateSurvey(mySurvey)

        LoadSurveyPage()
    End Sub

    Protected Sub cmd_CancelQuestionGroup_Click(sender As Object, e As EventArgs)
        LoadSurveyPage()
    End Sub
    Protected Sub cmd_DeleteQuestionGroup_Click(sender As Object, e As EventArgs)
        mySurvey = myCon.GetSurveyBySurveyID(AppUtility.GetDBInteger(hfSurveyID.Value))
        If hfQuestionGroupID.Value = "-1" Then
            mySurvey.QuestionGroupList.Add(GetQuestionGroup)
        Else
            For gIndex = 0 To mySurvey.QuestionGroupList.Count - 1
                If mySurvey.QuestionGroupList(gIndex).QuestionGroupID = hfQuestionGroupID.Value Then
                    With mySurvey.QuestionGroupList(gIndex)
                        .MarkedForDeletion =True
                    End With
                End If
            Next
        End If
        mySurvey = myCon.UpdateSurvey(mySurvey)




        LoadSurveyPage()
    End Sub

    Private Sub LoadSurveyPage()
        ClearPageArguments()
        SetPageArgument("surveyid", AppUtility.GetDBInteger(hfSurveyID.Value))
        SetPageArgument("action", "surveyview")
        LoadPage()
    End Sub


#End Region

#Region "Group Membership Administration"
    Protected Sub SetQuestionGroupMember(ByRef myMember As QuestionGroupMemberItem)
        If Not myMember Is Nothing Then
            With myMember
                ddlMemberQuestion.SelectedValue = .QuestionID
                tbMemberWeight.Text = .QuestionWeight
                tbMemberOrder.Text = .DisplayOrder
            End With
        End If
    End Sub
    Protected Function GetQuesionGroupMember() As QuestionGroupMemberItem
        Dim myMember As New QuestionGroupMemberItem
        With myMember
            .QuestionGroupID = AppUtility.GetDBInteger(hfQuestionGroupID.Value)
            .QuestionGroupMemberID = AppUtility.GetDBInteger(hfQuestionGroupMemberID.Value)
            .QuestionID = AppUtility.GetDBInteger(ddlMemberQuestion.SelectedValue)
            .QuestionWeight = AppUtility.GetDBDouble(tbMemberWeight.Text)
            .DisplayOrder = AppUtility.GetDBInteger(tbMemberOrder.Text)
        End With
        Return myMember
    End Function
    Protected Sub cmd_GroupMemberSave_Click(sender As Object, e As EventArgs)
        mySurvey = myCon.GetSurveyBySurveyID(AppUtility.GetDBInteger(hfSurveyID.Value))
        If hfQuestionGroupID.Value = "-1" Then
            ' Can't add Member to new group
        Else
            For gIndex = 0 To mySurvey.QuestionGroupList.Count - 1
                If mySurvey.QuestionGroupList(gIndex).QuestionGroupID = hfQuestionGroupID.Value Then
                    If hfQuestionGroupMemberID.Value = "-1" Then
                        mySurvey.QuestionGroupList(gIndex).QuestionMembership.Add( GetQuesionGroupMember()   )
                    Else
                        For mIndex = 0 To mySurvey.QuestionGroupList(gIndex).QuestionMembership.Count - 1
                            If mySurvey.QuestionGroupList(gIndex).QuestionMembership(mIndex).QuestionGroupMemberID = AppUtility.GetDBInteger(hfQuestionGroupMemberID.Value) Then
                                With mySurvey.QuestionGroupList(gIndex).QuestionMembership(mIndex)
                                    .QuestionWeight = AppUtility.GetDBDouble(tbMemberWeight.Text)
                                    .DisplayOrder = AppUtility.GetDBInteger(tbMemberOrder.Text)
                                End With
                            End If
                        Next
                    End If
                End If
            Next
        End If
        mySurvey = myCon.UpdateSurvey(mySurvey)
        LoadQuestionGroupPage()
    End Sub
    Protected Sub cmd_GroupMemberCancel_Click(sender As Object, e As EventArgs)
        LoadQuestionGroupPage()
    End Sub
    Protected Sub cmd_GroupMemberDelete_Click(sender As Object, e As EventArgs)
        mySurvey = myCon.GetSurveyBySurveyID(AppUtility.GetDBInteger(hfSurveyID.Value))
        If hfQuestionGroupID.Value = "-1" Then
            mySurvey.QuestionGroupList.Add(GetQuestionGroup)
        Else
            For gIndex = 0 To mySurvey.QuestionGroupList.Count - 1
                If mySurvey.QuestionGroupList(gIndex).QuestionGroupID = hfQuestionGroupID.Value Then
                    For mIndex = 0 To mySurvey.QuestionGroupList(gIndex).QuestionMembership.Count - 1
                        If mySurvey.QuestionGroupList(gIndex).QuestionMembership(mIndex).QuestionGroupMemberID = AppUtility.GetDBInteger(hfQuestionGroupMemberID.Value) Then
                            With mySurvey.QuestionGroupList(gIndex).QuestionMembership(mIndex)
                                .MarkedForDeletion = True
                            End With
                        End If
                    Next
                End If
            Next
        End If
        mySurvey = myCon.UpdateSurvey(mySurvey)
        LoadQuestionGroupPage()
    End Sub
    Private Sub LoadQuestionGroupPage()
        ClearPageArguments()
        SetPageArgument("surveyid", AppUtility.GetDBInteger(hfSurveyID.Value))
        SetPageArgument("questiongroupid", AppUtility.GetDBInteger(hfQuestionGroupID.Value))
        SetPageArgument("action", "surveyview")
        SetPageArgument("subaction", "questiongroupid")
        LoadPage()
    End Sub
#End Region

End Class
