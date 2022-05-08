
Imports CODataCon.com.controlorigins.ws
Imports CODataCon
Imports System.IO


Public Class Co_Apps_SurveyAdmin_Controls_QuestionItem
    Inherits SurveyUserControlBase

    Public myQuestion As QuestionItem

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        hfQuestionID.Value = AppUtility.GetDBInteger(GetPageArgument("questionid").Second, 0)
        hfQuestionAnswerID.Value = AppUtility.GetDBInteger(GetPageArgument("questionanswerid").Second, 0)

        cmd_BulkQuestion.Visible = False

        If ddlSurveyType.Items.Count =0  Then
            ddlSurveyType.Items.Clear()
            ddlSurveyType.Items.AddRange((From i In myCon.GetQuestionCategoryList() Where i.ParentSurveyTypeID<>0  Select New ListItem With {.Text = i.SurveyTypeNM, .Value = i.SurveyTypeID}).ToArray)

            ddlUnitOfMeasure.Items.Clear()
            ddlUnitOfMeasure.Items.AddRange((From i In myCon.GetUnitOfMeasureList() Select New ListItem With {.Text = i.Name, .Value = i.Value}).ToArray)

            ddlReviewRoleList.Items.Clear()
            ddlReviewRoleList.Items.AddRange((From i In myCon.GetReviewRoleLevelList() Select New ListItem With {.Text = i.Name, .Value = i.Value}).ToArray)

            If AppUtility.GetDBInteger(hfQuestionID.Value) > 0 Then
                Select Case GetPageArgument("action").Second.ToString.ToLower
                    Case "questionview"
                        Try
                            myQuestion = myCon.GetQuestionByQuestionID(AppUtility.GetDBInteger(hfQuestionID.Value))
                            cmd_BulkQuestion.Visible = True

                        Catch ex As Exception
                            myQuestion = New QuestionItem
                        End Try

                        Select Case GetPageArgument("subaction").Second.ToString.ToLower
                            Case "questionanswerid"
                                pnlQuestionAnswer.Visible = True
                                pnlApplicatonDetail.Visible = False

                                SetQuestionAnswer((From i In myQuestion.QuestionAnswerItemList Where i.QuestionAnswerID = hfQuestionAnswerID.Value Select i).SingleOrDefault)

                            Case Else
                                pnlQuestionAnswer.Visible = False
                                pnlApplicatonDetail.Visible = True
                        End Select
                    Case Else
                        myQuestion = New QuestionItem
                End Select

                With myQuestion
                    hfQuestionID.Value = .QuestionID
                    tbQuestionNM.Text = .QuestionNM
                    tbQuestionShortNM.Text = .QuestionShortNM
                    tbQuestionDS.Text = .QuestionDS
                    tbQuestionSort.Text = .QuestionSort
                    tbKeywords.Text = .Keywords
                    ddlQuestionType.SelectedValue = .QuestionTypeID
                    tbQuestionValue.Text = .QuestionValue
                    ddlReviewRoleList.SelectedValue = .ReviewRoleLevel
                    ddlSurveyType.SelectedValue = .SurveyTypeID
                    ddlUnitOfMeasure.SelectedValue = .UnitOfMeasureID

                    If .FileData.Length > 1 Then
                        QuestionImage.Visible = True
                        QuestionImage.ImageUrl = String.Format("/controls/GetImage.ashx?id={0}", .QuestionID)
                        QuestionImage.CssClass = "img-responsive"
                    Else
                        QuestionImage.Visible = False
                        QuestionImage.ImageUrl = "/images/spacer.gif"
                        QuestionImage.CssClass = "img-responsive"
                    End If
                     ' .QuestionAnswerItemList(0).QuestionAnswerCommentFL
                    dtQuestionAnswer.Visible = True
                    dtQuestionAnswer.TableHeader.TableTitle = "Question Answers (<a href='/CO_Apps/SurveyAdmin/navigator.aspx?action=questionview&questionid=" & .QuestionID & "&subaction=questionanswerid&questionanswerid=-1'> Add Answer</a>)"
                    dtQuestionAnswer.TableHeader.DetailFieldName = "QuestionAnswerSort"
                    dtQuestionAnswer.TableHeader.DetailKeyName = "QuestionAnswerID"
                    dtQuestionAnswer.TableHeader.DetailDisplayName = "Sort"
                    dtQuestionAnswer.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=questionview&questionid=" & .QuestionID & "&subaction=questionanswerid&questionanswerid={0}"
                    dtQuestionAnswer.TableHeader.AddHeaderItem("QuestionAnswerNM", "QuestionAnswerNM")
                    dtQuestionAnswer.TableHeader.AddHeaderItem("QuestionAnswerDS", "QuestionAnswerDS")
                    dtQuestionAnswer.TableHeader.AddHeaderItem("Comment Required", "QuestionAnswerCommentFL")
                    dtQuestionAnswer.TableHeader.AddHeaderItem("Active", "QuestionAnswerActiveFL")
                    dtQuestionAnswer.BuildTable(dtQuestionAnswer.TableHeader, .QuestionAnswerItemList)

                    dtSurvey.Visible = True
                    dtSurvey.TableHeader.TableTitle = "Survey List"
                    dtSurvey.TableHeader.DetailFieldName = "Name"
                    dtSurvey.TableHeader.DetailKeyName = "Value"
                    dtSurvey.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyview&surveyid={0}"
                    dtSurvey.BuildTable(dtSurvey.TableHeader, .SurveyLookupList)

                    dtResponseAnswers.Visible = True
                    dtResponseAnswers.TableHeader.TableTitle = "Survey Response Answers"
                    dtResponseAnswers.TableHeader.DetailFieldName = "SurveyResponseNM"
                    dtResponseAnswers.TableHeader.DetailKeyName = "SurveyResponseID"
                    dtResponseAnswers.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyresponseview&surveyresponseid={0}"
                    dtResponseAnswers.TableHeader.AddHeaderItem("ApplicationNM", "ApplicationNM")
                    dtResponseAnswers.TableHeader.AddHeaderItem("SurveyNM", "SurveyNM")
                    dtResponseAnswers.TableHeader.AddHeaderItem("QuestionScore", "QuestionScore")
                    dtResponseAnswers.TableHeader.AddHeaderItem("Response Comment", "AnswerComment")
                    dtResponseAnswers.BuildTableFromGrid(dtResponseAnswers.TableHeader, myDACon.GetSurveyResponseAnswersByQuestionID(.QuestionID))
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
        With myQuestion
            .QuestionNM = tbQuestionNM.Text
            .QuestionShortNM = tbQuestionShortNM.Text
            .QuestionDS = tbQuestionDS.Text
            .QuestionSort = AppUtility.GetDBInteger(tbQuestionSort.Text)
            .CommentFL = True
            .Keywords = tbKeywords.Text
            .ModifiedID = UserInfo.ApplicationUserID
            .QuestionTypeID = ddlQuestionType.SelectedValue
            .QuestionValue = AppUtility.GetDBInteger(tbQuestionValue.Text)
            .ReviewRoleLevel = AppUtility.GetDBInteger(ddlReviewRoleList.SelectedValue)
            .SurveyTypeID = ddlSurveyType.SelectedValue
            .UnitOfMeasureID = ddlUnitOfMeasure.SelectedValue

            If FileUpload.HasFile Then
                FileUpload.SaveAs(Server.MapPath("/images/client/") & FileUpload.FileName)
                ' Read the file and convert it to Byte Array
                Dim filePath As String = Server.MapPath("/images/client/" & FileUpload.FileName)
                Dim filename As String = Path.GetFileName(filePath)
                Using fs As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
                    Using br As BinaryReader = New BinaryReader(fs)
                        .FileData = br.ReadBytes(Convert.ToInt32(fs.Length))
                    End Using
                End Using
            End If
        End With
        myQuestion = myCon.PutQuestionItem(myQuestion)
        hfQuestionID.Value = myQuestion.QuestionID

        ResetQuestionView()
    End Sub
    Protected Sub cmd_CancelQuestion_Click(sender As Object, e As EventArgs)
        ResetQuestionView()
    End Sub
    Protected Sub cmd_DeleteQuestion_Click(sender As Object, e As EventArgs)
        Dim QuestionID = AppUtility.GetDBInteger(hfQuestionID.Value)
        myCon.DeleteQuestionByQuestionID(QuestionID)
        ResetQuestionView()
    End Sub
    Protected Sub ResetQuestionView()
        ClearPageArguments()
        SetPageArgument("questionid", 0)
        SetPageArgument("action", "questionview")
        LoadPage()

    End Sub

#End Region

#Region "Quesiton Answer Administration"
    Public Sub SetQuestionAnswer(ByRef myAnswer As QuestionAnswerItem)
        If myAnswer Is Nothing Then
            hfQuestionAnswerID.Value = "-1"
        Else
            With myAnswer
                tbQuestionAnswerNM.Text = .QuestionAnswerNM
                tbQuestionAnswerDS.Text = .QuestionAnswerDS
                tbQuestionAnswerShortNM.Text = .QuestionAnswerShortNM
                tbQuestionAnswerSort.Text = .QuestionAnswerSort
                tbQuestionAnswerValue.Text = .QuestionAnswerValue
                cbQuestionAnswerActiveFL.Checked = .QuestionAnswerActiveFL
                cbQuestionAnswerCommentFL.Checked = .QuestionAnswerCommentFL
            End With
        End If
    End Sub
    Public Function GetQuestionAnswer() As QuestionAnswerItem
        Dim myAnswer As New QuestionAnswerItem
        With myAnswer
            .QuestionID = AppUtility.GetDBInteger(hfQuestionID.Value)
            .QuestionAnswerID = AppUtility.GetDBInteger(hfQuestionAnswerID.Value)
            .QuestionAnswerNM = tbQuestionAnswerNM.Text
            .QuestionAnswerDS = tbQuestionAnswerDS.Text
            .QuestionAnswerShortNM = tbQuestionAnswerShortNM.Text
            .QuestionAnswerSort = tbQuestionAnswerSort.Text
            .QuestionAnswerValue = tbQuestionAnswerValue.Text
            .QuestionAnswerActiveFL = cbQuestionAnswerActiveFL.Checked
            .QuestionAnswerCommentFL = cbQuestionAnswerCommentFL.Checked
        End With
        Return myAnswer
    End Function
    Protected Sub cmd_SaveQuestionAnswer_Click(sender As Object, e As EventArgs)
        myQuestion = myCon.GetQuestionByQuestionID(AppUtility.GetDBInteger(hfQuestionID.Value))
        myQuestion.ModifiedID = UserInfo.ApplicationUserID
        
        If hfQuestionAnswerID.Value = "-1" Then
            ' New Quesiton Answer
            myQuestion.QuestionAnswerItemList.Add(GetQuestionAnswer())
        Else
            ' update Question Answer
            For gIndex = 0 To myQuestion.QuestionAnswerItemList.Count - 1
                If myQuestion.QuestionAnswerItemList(gIndex).QuestionAnswerID = AppUtility.GetDBInteger(hfQuestionAnswerID.Value) Then
                    With myQuestion.QuestionAnswerItemList(gIndex)
                        .QuestionAnswerNM = tbQuestionAnswerNM.Text
                        .QuestionAnswerDS = tbQuestionAnswerDS.Text
                        .QuestionAnswerShortNM = tbQuestionAnswerShortNM.Text
                        .QuestionAnswerSort = tbQuestionAnswerSort.Text
                        .QuestionAnswerValue = tbQuestionAnswerValue.Text
                        .QuestionAnswerActiveFL = cbQuestionAnswerActiveFL.Checked
                        .QuestionAnswerCommentFL = cbQuestionAnswerCommentFL.Checked
                    End With
                    Exit For
                End If
            Next
        End If
        myQuestion = myCon.PutQuestionItem(myQuestion)
        hfQuestionID.Value = myQuestion.QuestionID
        ClearPageArguments()
        SetPageArgument("questionid", myQuestion.QuestionID)
        SetPageArgument("action", "questionview")
        LoadPage()
    End Sub
    Protected Sub cmd_CancelQuestionAnswer_Click(sender As Object, e As EventArgs)
        ClearPageArguments()
        SetPageArgument("questionid", hfQuestionID.Value)
        SetPageArgument("action", "questionview")
        LoadPage()
    End Sub
    Protected Sub cmd_DeleteQuestionAnswer_Click(sender As Object, e As EventArgs)
        myQuestion = myCon.GetQuestionByQuestionID(AppUtility.GetDBInteger(hfQuestionID.Value))
        If hfQuestionAnswerID.Value = "-1" Then
            ' New Quesiton Answer - DO NOTHING
        Else
            ' update Question Answer
            For gIndex = 0 To myQuestion.QuestionAnswerItemList.Count - 1
                If myQuestion.QuestionAnswerItemList(gIndex).QuestionAnswerID = AppUtility.GetDBInteger(hfQuestionAnswerID.Value) Then
                    With myQuestion.QuestionAnswerItemList(gIndex)
                        .QuestionAnswerNM = tbQuestionAnswerNM.Text
                        .QuestionAnswerDS = tbQuestionAnswerDS.Text
                        .QuestionAnswerShortNM = tbQuestionAnswerShortNM.Text
                        .QuestionAnswerSort = tbQuestionAnswerSort.Text
                        .QuestionAnswerValue = tbQuestionAnswerValue.Text
                        .QuestionAnswerActiveFL = cbQuestionAnswerActiveFL.Checked
                        .QuestionAnswerCommentFL = cbQuestionAnswerCommentFL.Checked
                        .MarkedForDeletion = True
                    End With
                    Exit For
                End If
            Next
        End If


        myQuestion = myCon.PutQuestionItem(myQuestion)
        hfQuestionID.Value = myQuestion.QuestionID
        ClearPageArguments()
        SetPageArgument("questionid", myQuestion.QuestionID)
        SetPageArgument("action", "questionview")
        LoadPage()
    End Sub

#End Region

    Protected Sub cmd_BulkQuestion_Click(sender As Object, e As EventArgs)
        ClearPageArguments()
        SetPageArgument("questionid", AppUtility.GetDBInteger(hfQuestionID.Value))
        SetPageArgument("action", "questionclone")
        LoadPage()
    End Sub
End Class
