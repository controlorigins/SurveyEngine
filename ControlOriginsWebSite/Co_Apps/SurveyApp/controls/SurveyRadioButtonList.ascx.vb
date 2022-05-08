
Public Class SurveyRadioButtonList
    Inherits SurveyQuestionControl
    Implements ISurveyQuestionControl

    Public Sub SetQuestion(ByRef myQuestion As CODataCon.com.controlorigins.ws.QuestionItem, ByVal CurAnswers As List(Of CODataCon.com.controlorigins.ws.SurveyResponseAnswerItem)) Implements ISurveyQuestionControl.SetQuestion
        labQuestion.Text = GetQuestionDisplay(myQuestion)
        Dim mylistItem As New ListItem

        For Each myAnswer In myQuestion.QuestionAnswerItemList
            mylistItem = New ListItem
            With mylistItem
                .Attributes.Add("data-toggle", "tooltip")
                .Attributes.Add("data-placement", "left")
                .Attributes.Add("title", myAnswer.QuestionAnswerDS)
                .Attributes.Add("data-comment-required", myAnswer.QuestionAnswerCommentFL)
                .Attributes.Add("data-question-id", myQuestion.QuestionID)
                .Attributes.Add("data-description", myAnswer.QuestionAnswerDS)
                .Attributes.Add("class", "SurveyRadioListItem")
                .Value = myAnswer.QuestionAnswerID
                .Text = CODataCon.AppUtility.RemoveHtml(myAnswer.QuestionAnswerNM)
            End With
            rblAnswers.Items.Add(mylistItem)
        Next
        If CurAnswers.Count > 0 Then
            tbComment.Text = CurAnswers.Item(0).AnswerComment
            tbComment.Attributes.Add("class", String.Format("SQ{0}-Q{0}", "1", myQuestion.QuestionID))
            rblAnswers.SelectedValue = CurAnswers.Item(0).QuestionAnswerID
            SurveyAnswerID.Value = CurAnswers.Item(0).SurveyAnswerID

            Dim bShowComment = (From i In myQuestion.QuestionAnswerItemList Where i.QuestionAnswerID = rblAnswers.SelectedValue Select i.QuestionAnswerCommentFL).Single

            If bShowComment Then
                tbComment.Visible = True
                tbComment.Enabled = True
            Else
                tbComment.Visible = True
                tbComment.Enabled = True
            End If

        Else
            SurveyAnswerID.Value = "-1"
            tbComment.Visible = True
            tbComment.Enabled = True
        End If

    End Sub

    Public Function GetAnswer(ByVal SurveyResponseID As Integer, ByVal SequenceNumber As Integer, ByVal QuestionID As Integer) As CODataCon.com.controlorigins.ws.SurveyResponseAnswerItem Implements ISurveyQuestionControl.GetAnswer
        Dim newAnswer As New CODataCon.com.controlorigins.ws.SurveyResponseAnswerItem
        If IsNumeric(rblAnswers.SelectedValue) AndAlso CInt(rblAnswers.SelectedValue) > 0 Then
            With newAnswer
                .SurveyResponseID = SurveyResponseID
                .SurveyAnswerID = SurveyAnswerID.Value
                .SequenceNumber = SequenceNumber
                .QuestionID = QuestionID
                .QuestionAnswerID = rblAnswers.SelectedValue
                .AnswerComment = String.Empty
                .ModifiedComment = String.Empty
                .ModifiedDT = Now
                .ModifiedID = UserInfo.ApplicationUserID
                .ResponseList = GetResponseList()
                If tbComment.Visible Then
                    .AnswerComment = tbComment.Text
                End If
            End With
        End If
        Return newAnswer
    End Function

    Private Function GetResponseList() As String()
        Dim myReturn As New List(Of String)
        myReturn.Add(rblAnswers.SelectedValue)
        Return myReturn.ToArray()
    End Function

    Public Sub SetControlID(myID As String) Implements ISurveyQuestionControl.SetControlID
        Me.ID = myID
    End Sub

    Protected Sub rblAnswers_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim myLI As RadioButtonList = TryCast(sender, RadioButtonList)
        Dim bShowComment As Boolean = True
        If Not myLI Is Nothing Then
            bShowComment = CBool(myLI.SelectedItem.Attributes("data-comment-required"))
            If bShowComment Then
                LabeltbComment.Visible=True
                tbComment.Enabled = True
                tbComment.Visible = True
            Else
                LabeltbComment.Visible=True
                tbComment.Enabled = True
                tbComment.Visible = True
            End If
            For Each myItem As ListItem In myLI.Items
                If myItem.Selected Then
                    myItem.Attributes("class") = "SurveyRadioListItem SurveyRadioListItemSelected"
                Else
                    myItem.Attributes("class") = "SurveyRadioListItem"
                End If
            Next
        End If
    End Sub
End Class
