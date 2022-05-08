Imports CODataCon.com.controlorigins.ws
Imports CODataCon


Public Class Co_Apps_SurveyAdmin_Controls_BulkQuestionItem
    Inherits SurveyUserControlBase

    Public myQuestion As QuestionItem

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        hfQuestionID.Value = AppUtility.GetDBInteger(GetPageArgument("questionid").Second, 0)

        If Not IsPostBack Then
            ddlSurveyType.Items.Clear()
            ddlSurveyType.Items.AddRange((From i In myCon.GetQuestionCategoryList() Select New ListItem With {.Text = i.SurveyTypeNM, .Value = i.SurveyTypeID}).ToArray)

            ddlUnitOfMeasure.Items.Clear()
            ddlUnitOfMeasure.Items.AddRange((From i In myCon.GetUnitOfMeasureList() Select New ListItem With {.Text = i.Name, .Value = i.Value}).ToArray)

            ddlReviewRoleList.Items.Clear()
            ddlReviewRoleList.Items.AddRange((From i In myCon.GetReviewRoleLevelList() Select New ListItem With {.Text = i.Name, .Value = i.Value}).ToArray)

            If AppUtility.GetDBInteger(hfQuestionID.Value) > 0 Then
                Select Case GetPageArgument("action").Second.ToString.ToLower
                    Case "questionclone"
                        Try
                            myQuestion = myCon.GetQuestionByQuestionID(AppUtility.GetDBInteger(hfQuestionID.Value))
                        Catch ex As Exception
                            myQuestion = New QuestionItem
                        End Try

                        pnlApplicatonDetail.Visible = True
                    Case Else
                        myQuestion = New QuestionItem
                End Select

                With myQuestion
                    hfQuestionID.Value = .QuestionID
                    tbQuestionNM.Text = .QuestionNM
                    tbQuestionShortNM.Text = .QuestionShortNM
                    tbQuestionSort.Text = .QuestionSort
                    ddlQuestionType.SelectedValue = .QuestionTypeID
                    tbQuestionValue.Text = .QuestionValue
                    ddlReviewRoleList.SelectedValue = .ReviewRoleLevel
                    ddlSurveyType.SelectedValue = .SurveyTypeID
                    ddlUnitOfMeasure.SelectedValue = .UnitOfMeasureID
                End With
            Else
                myQuestion = New QuestionItem With {.QuestionID = -1}
                hfQuestionID.Value = -1
            End If
        End If

    End Sub


#Region "Question Administration"


    Protected Sub cmd_SaveQuestion_Click(sender As Object, e As EventArgs)
        myQuestion = myCon.GetQuestionByQuestionID(AppUtility.GetDBInteger(hfQuestionID.Value))

        Dim myQAList As New List(Of QuestionAnswerItem)

        Dim qCount As Integer = AppUtility.GetDBInteger(tbQuestionSort.Text) + 1
        Dim qaCount As Integer = AppUtility.GetDBInteger(tbQuestionSort.Text) + 1

        For Each myQ As String In Split(tbQuestionNM.Text, vbCrLf)
            If myQ.Trim.Length > 0 Then
                Dim newQ As New QuestionItem With {.QuestionID = -1,
                                                   .QuestionNM = myQ.Trim,
                                                   .QuestionDS = myQ.Trim,
                                                   .QuestionShortNM = String.Format("{0}-{1}", tbQuestionShortNM.Text, qCount.ToString),
                                                   .QuestionSort = qCount,
                                                   .CommentFL = True,
                                                   .ModifiedID = UserInfo.ApplicationUserID,
                                                   .QuestionTypeID = ddlQuestionType.SelectedValue,
                                                   .QuestionValue = AppUtility.GetDBInteger(tbQuestionValue.Text),
                                                   .ReviewRoleLevel = AppUtility.GetDBInteger(ddlReviewRoleList.SelectedValue),
                                                   .SurveyTypeID = ddlSurveyType.SelectedValue,
                                                   .UnitOfMeasureID = ddlUnitOfMeasure.SelectedValue
                                                   }
                myQAList.Clear()
                qaCount = 1
                For Each myAnswer As QuestionAnswerItem In myQuestion.QuestionAnswerItemList
                    myQAList.Add(New QuestionAnswerItem With {.QuestionAnswerID = -1,
                                                              .QuestionID = -1,
                                                              .QuestionAnswerShortNM = String.Format("{0}-{1}", newQ.QuestionShortNM, qaCount),
                                                              .QuestionAnswerActiveFL = myAnswer.QuestionAnswerActiveFL,
                                                              .QuestionAnswerCommentFL = myAnswer.QuestionAnswerCommentFL,
                                                              .QuestionAnswerDS = myAnswer.QuestionAnswerDS,
                                                              .QuestionAnswerNM = myAnswer.QuestionAnswerNM,
                                                              .QuestionAnswerSort = qaCount,
                                                              .QuestionAnswerValue = myAnswer.QuestionAnswerValue,
                                                              .ModifedID = UserInfo.ApplicationUserID,
                                                              .ModifiedDT = Now()})
                    qaCount = qaCount + 1
                Next
                newQ.QuestionAnswerItemList = myQAList.ToArray
                myCon.PutQuestionItem(newQ)
                qCount = qCount + 1
            End If
        Next

        ResetQuestionView()
    End Sub

    Protected Sub cmd_CancelQuestion_Click(sender As Object, e As EventArgs)
        ResetQuestionView()
    End Sub

    Protected Sub ResetQuestionView()
        ClearPageArguments()
        SetPageArgument("questionid", 0)
        SetPageArgument("action", "questionview")
        LoadPage()
    End Sub

#End Region




End Class
