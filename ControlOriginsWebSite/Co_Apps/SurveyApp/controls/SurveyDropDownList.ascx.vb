
Public Class SurveyDropDownList
    Inherits SurveyQuestionControl
    Implements ISurveyQuestionControl

    Private TextBoxID As String


    Public Sub SetQuestion(ByRef myQuestion As CODataCon.com.controlorigins.ws.QuestionItem, ByVal CurAnswers As List(Of CODataCon.com.controlorigins.ws.SurveyResponseAnswerItem)) Implements ISurveyQuestionControl.SetQuestion
        litQuestion.Text = GetQuestionDisplay(myQuestion)
        Dim mylistItem As New ListItem

        For Each myAnswer In myQuestion.QuestionAnswerItemList
            mylistItem = New ListItem
            With mylistItem
                .Attributes.Add("data-comment-required", myAnswer.QuestionAnswerCommentFL)
                .Attributes.Add("data-question-id", myQuestion.QuestionID)
                If myAnswer.QuestionAnswerCommentFL Then
                    .Attributes.Add("onclick", String.Format("javascript:enableTxt('{0}')", String.Format("SQ{0}-Q{0}", "1", myQuestion.QuestionID)))
                Else
                    .Attributes.Add("onclick", String.Format("javascript:disableTxt('{0}')", String.Format("SQ{0}-Q{0}", "1", myQuestion.QuestionID)))
                End If
                .Attributes.Add("class", "SurveyRadioListItem")
                .Value = myAnswer.QuestionAnswerID
                .Text = myAnswer.QuestionAnswerNM
            End With
            ddlAnswers.Items.Add(mylistItem)
        Next
        If CurAnswers.Count > 0 Then
            tbComment.Text = CurAnswers.Item(0).ModifiedComment
            tbComment.Attributes.Add("data-question-id", myQuestion.QuestionID)
            ddlAnswers.SelectedValue = CurAnswers.Item(0).QuestionAnswerID
            SurveyAnswerID.Value = CurAnswers.Item(0).SurveyAnswerID
        Else
            SurveyAnswerID.Value = "-1"
        End If

        If myQuestion.CommentFL Then
            tbComment.Visible = True
            tbComment.Enabled = True
        Else
            tbComment.Visible = False
            tbComment.Enabled = False

        End If


    End Sub

    Public Function GetAnswer(ByVal SurveyResponseID As Integer, ByVal SequenceNumber As Integer, ByVal QuestionID As Integer) As CODataCon.com.controlorigins.ws.SurveyResponseAnswerItem Implements ISurveyQuestionControl.GetAnswer

        Dim newAnswer As New CODataCon.com.controlorigins.ws.SurveyResponseAnswerItem
        With newAnswer
            .SurveyResponseID = SurveyResponseID
            .SequenceNumber = SequenceNumber
            .SurveyAnswerID = SurveyAnswerID.Value
            .QuestionID = QuestionID
            .QuestionAnswerID = ddlAnswers.SelectedValue
            If tbComment.Enabled Then
                .AnswerComment = tbComment.Text
            Else
                .AnswerComment = tbComment.Text
            End If
            .AnswerComment = String.Empty
            .ModifiedDT = Now
            .ModifiedID = UserInfo.ApplicationUserID
            .ResponseList = GetResponseList(QuestionID)
        End With
        Return newAnswer
    End Function

    Private Function GetResponseList(ByVal QuestionID As Integer) As String()
        Dim myReturn As New List(Of String)
        myReturn.Add(ddlAnswers.SelectedValue)
        Return myReturn.ToArray()
    End Function

    Public Sub SetControlID(myID As String) Implements ISurveyQuestionControl.SetControlID
        Me.ID = myID
    End Sub
End Class
