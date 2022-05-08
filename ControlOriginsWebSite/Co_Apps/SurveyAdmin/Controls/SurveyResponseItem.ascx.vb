Imports CODataCon.com.controlorigins.ws
Imports CODataCon

Partial Class Co_Apps_SurveyAdmin_Controls_SurveyResponseItem
    Inherits SurveyUserControlBase
    Public mySurveyResponse As SurveyResponseItem
    Public myApplication As ApplicationItem

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim reqSurveyResponseID = AppUtility.GetDBInteger(GetPageArgument("SurveyResponseID").Second, 0)
        Dim Action = GetPageArgument("action").Second.ToString.ToLower
        If Not IsPostBack Then
            hfSurveyResponseID.Value = reqSurveyResponseID
            Select Case Action
                Case "surveyresponseview"
                    Try
                        mySurveyResponse = myCon.GetSurveyResponse(reqSurveyResponseID)
                        If mySurveyResponse.ApplicationID > 0 Then
                            myApplication = myCon.GetApplicationByApplicationID(mySurveyResponse.ApplicationID)
                            ddlApplicationUser.Items.Clear()
                            ddlApplicationUser.Items.Add(New ListItem With {.Value = String.Empty, .Text = "All Users"})
                            For Each myUser In myApplication.ApplicationUserList
                                If mySurveyResponse.AssignedUserID = myUser.ApplicationUserID Then
                                    ddlApplicationUser.Items.Add(New ListItem With {.Value = myUser.ApplicationUserID, .Text = myUser.AccountNM, .Selected = True})
                                Else
                                    ddlApplicationUser.Items.Add(New ListItem With {.Value = myUser.ApplicationUserID, .Text = myUser.AccountNM})
                                End If
                            Next
                            ddlStatus.Items.Clear()
                            For Each myStat In mySurveyResponse.Survey.StatusList
                                If myStat.StatusID = mySurveyResponse.StatusID Then
                                    ddlStatus.Items.Add(New ListItem With {.Value = myStat.StatusID, .Text = myStat.StatusNM, .Selected = True})
                                Else
                                    ddlStatus.Items.Add(New ListItem With {.Value = myStat.StatusID, .Text = myStat.StatusNM, .Selected = False})
                                End If
                            Next
                        End If
                    Catch ex As Exception
                        mySurveyResponse = New SurveyResponseItem
                    End Try
                Case Else
                    mySurveyResponse = New SurveyResponseItem
            End Select
            Dim myAnswers As New List(Of QuestionAnswerDisplay)
            With mySurveyResponse
                tbSurveyResponseNM.Text = .SurveyResponseNM
                If Not mySurveyResponse.Survey Is Nothing Then
                    lbSurveyNM.Text = .Survey.SurveyNM
                    lbSurveyNM.PostBackUrl = String.Format("/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyview&surveyid={0}", .Survey.SurveyID)
                    lbApplicationNM.Text = myApplication.ApplicationNM
                    lbApplicationNM.PostBackUrl = String.Format("/CO_Apps/SurveyAdmin/navigator.aspx?action=applicationview&applicationid={0}", .ApplicationID)
                    tbSurveyResponseDS.Text = .PercentComplete
                    tbSurveyResponseScore.Text = .SurveyResponseScore
                    tbResponseHistory.Text = String.Empty
                    tbDataSource.Text = .DataSource

                End If
                For Each myQ In .Survey.QuestionList
                    Dim QA = ((From answer In .AnswerList Where answer.QuestionID = myQ.QuestionID Select answer).ToList())
                    If QA.Count > 0 Then
                        For Each myQA In QA
                            myAnswers.Add(New QuestionAnswerDisplay With
                             {.QuestionAnswerID = myQA.QuestionAnswerID,
                              .QuestionAnswerNM = myQA.DisplayAnswerNM,
                              .QuestionID = myQA.QuestionID,
                              .QuestionNM = myQ.QuestionNM,
                              .QuestionValue = myQ.QuestionValue,
                              .QuestionAnswerValue = myQA.QuestionAnswerValue,
                              .ModifiedDT = myQA.ModifiedDT,
                              .ModifiedComment = myQA.ModifiedComment,
                              .AnswerComment =myQA.AnswerComment,
                              .QuestionScore = (myQA.QuestionAnswerValue * myQ.QuestionValue)})
                        Next
                    Else
                        myAnswers.Add(New QuestionAnswerDisplay With
                         {.QuestionAnswerID = 0,
                          .QuestionAnswerNM = String.Empty,
                          .QuestionID = myQ.QuestionID,
                          .QuestionNM = myQ.QuestionNM,
                          .QuestionValue = myQ.QuestionValue,
                          .QuestionAnswerValue = 0,
                          .ModifiedDT = Nothing,
                          .ModifiedComment = String.Empty,
                          .AnswerComment = String.Empty,
                          .QuestionScore = 0})
                    End If
                Next
            End With

            With dtSurveyResponseAnswer.TableHeader
                .TableTitle = "Survey Response Answers"
                .DetailFieldName = "QuestionNM"
                .DetailKeyName = "QuestionID"
                .DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=questionview&questionid={0}"
                .AddHeaderItem("Response Answer", "QuestionAnswerNM")
                .AddHeaderItem("Question Value", "QuestionValue")
                .AddHeaderItem("Answer Value", "QuestionAnswerValue")
                .AddHeaderItem("Response Score", "QuestionScore")
                .AddHeaderItem("Comment", "AnswerComment")
                .AddHeaderItem("Modified Date", "ModifiedDT")
            End With

            dtSurveyResponseAnswer.BuildTable(dtSurveyResponseAnswer.TableHeader, myAnswers)
            
            With dtHistory.TableHeader
                .TableTitle = "History"
                .DetailFieldName = "ModifiedDT"
                .DetailKeyName = "SurveyResponseHistoryID"
                .DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyresponseview&surveyresponseid=" & hfSurveyResponseID.Value & "&subaction=surveyresponsestate&surveyresponsestateid={0}"
                .AddHeaderItem("StatusNM", "StatusNM")
                .AddHeaderItem("Answers", "Answers")
            End With
            dtHistory.BuildTable(dtHistory.TableHeader, mySurveyResponse.SurveyResponseHistory)

            With dtStateHistory.TableHeader
                .TableTitle = "State History"
                .DetailFieldName = "EmailBody"
                .DetailKeyName = "SurveyResponseStateID"
                .DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyresponseview&surveyresponseid=" & hfSurveyResponseID.Value & "&subaction=surveyresponsestate&surveyresponsestateid={0}"
                .AddHeaderItem("ModifiedDT", "ModifiedDT")
                .AddHeaderItem("AssignedUserID", "AssignedUserID")
                .AddHeaderItem("EmailSent", "EmailSent")
            End With
            dtStateHistory.BuildTable(dtStateHistory.TableHeader, mySurveyResponse.StateList)

            
        End If

    End Sub

    Public Class QuestionAnswerDisplay
        Public Property QuestionID As Integer
        Public Property QuestionNM As String
        Public Property QuestionAnswerID As Integer
        Public Property QuestionAnswerNM As String
        Public Property QuestionValue As Decimal
        Public Property QuestionAnswerValue As Decimal
        Public Property QuestionScore As Decimal
        Public Property ModifiedDT As DateTime
        Public Property ModifiedComment As String
        Public Property AnswerComment As String 
    End Class

    Protected Sub cmd_Save_Click(sender As Object, e As EventArgs)
        mySurveyResponse = myCon.GetSurveyResponse(hfSurveyResponseID.Value)
        If mySurveyResponse.ApplicationID > 0 Then
            myApplication = myCon.GetApplicationByApplicationID(mySurveyResponse.ApplicationID)
            Dim mySR As New SurveyResponseItem
            With mySR
                .ApplicationID = mySurveyResponse.ApplicationID
                .Survey = New SurveyItem With {.SurveyID = mySurveyResponse.Survey.SurveyID}
                .SurveyResponseNM = tbSurveyResponseNM.Text
                .SurveyResponseID = hfSurveyResponseID.Value
                If ddlApplicationUser.SelectedValue <> String.Empty Then
                    .AssignedUserID = AppUtility.GetDBInteger(ddlApplicationUser.SelectedValue)
                End If
                .ModifiedID = UserInfo.ApplicationUserID
                .ModifiedDT = Now()
                .DataSource = "SurveyAdmin.SurveyResponseItem.ascx"
            End With
            mySR = myCon.PutSurveyResponseItem(mySR)
        End If

        ClearPageArguments()
        SetPageArgument("surveyresponseid", hfSurveyResponseID.Value)
        SetPageArgument("action", "surveyresponseview")
        LoadPage()
    End Sub

    Protected Sub cmd_Cancel_Click(sender As Object, e As EventArgs)

        mySurveyResponse = myCon.GetSurveyResponse(hfSurveyResponseID.Value)
        ClearPageArguments()
        SetPageArgument("applicationid", mySurveyResponse.ApplicationID)
        SetPageArgument("action", "applicationview")
        LoadPage()
    End Sub

    Protected Sub cmd_Reset_Click(sender As Object, e As EventArgs)

        mySurveyResponse = myCon.GetSurveyResponse(hfSurveyResponseID.Value)
        myCon.ResetSurveyResponse(mySurveyResponse)

        ClearPageArguments()
        SetPageArgument("applicationid", mySurveyResponse.ApplicationID)
        SetPageArgument("action", "applicationview")
        LoadPage()
    End Sub

    Protected Sub cmd_Delete_Click(sender As Object, e As EventArgs)
        mySurveyResponse = myCon.GetSurveyResponse(hfSurveyResponseID.Value)
        myCon.DeleteSurveyResponse(mySurveyResponse)

        ClearPageArguments()
        SetPageArgument("applicationid", mySurveyResponse.ApplicationID)
        SetPageArgument("action", "applicationview")
        LoadPage()
    End Sub
End Class
