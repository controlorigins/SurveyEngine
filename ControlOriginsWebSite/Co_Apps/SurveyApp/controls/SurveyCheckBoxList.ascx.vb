
Public Class SurveyCheckBoxList
    Inherits SurveyQuestionControl
    Implements ISurveyQuestionControl

    Public Sub SetQuestion(ByRef myQuestion As CODataCon.com.controlorigins.ws.QuestionItem, ByVal CurAnswers As List(Of CODataCon.com.controlorigins.ws.SurveyResponseAnswerItem)) Implements ISurveyQuestionControl.SetQuestion
        litQuestion.Text = GetQuestionDisplay(myQuestion)

        Dim mylistItem As New ListItem

        For Each myAnswer In myQuestion.QuestionAnswerItemList
            mylistItem = New ListItem
            With mylistItem
                .Attributes.Add("data-comment-required", myAnswer.QuestionAnswerCommentFL)
                .Attributes.Add("data-question-id", myQuestion.QuestionID)
                .Attributes.Add("class", "SurveyCheckBoxListItem")
                .Value = myAnswer.QuestionAnswerID
                .Text = CODataCon.AppUtility.RemoveHtml(myAnswer.QuestionAnswerNM)
            End With
            cblAnswers.Items.Add(mylistItem)
        Next
        If CurAnswers.Count > 0 Then
            For Each myAnswer In CurAnswers
                cblAnswers.Items.FindByValue(myAnswer.QuestionAnswerID).Selected = True
            Next
        End If
    End Sub

    Public Function GetAnswer(ByVal SurveyResponseID As Integer, ByVal SequenceNumber As Integer, ByVal QuestionID As Integer) As CODataCon.com.controlorigins.ws.SurveyResponseAnswerItem Implements ISurveyQuestionControl.GetAnswer
        Dim newAnswer As New CODataCon.com.controlorigins.ws.SurveyResponseAnswerItem
        If IsNumeric(cblAnswers.SelectedValue) AndAlso CInt(cblAnswers.SelectedValue) > 0 Then
            With newAnswer
                .SurveyResponseID = SurveyResponseID
                .SurveyAnswerID = "-1"
                .SequenceNumber = SequenceNumber
                .QuestionID = QuestionID
                .QuestionAnswerID = Nothing
                .AnswerComment = String.Empty
                .ModifiedComment = String.Empty
                .ModifiedDT = Now
                .ModifiedID = UserInfo.ApplicationUserID
                .ResponseList = GetResponseList()
            End With
        End If
        Return newAnswer
    End Function

    Private Function GetResponseList() As String()
        Dim myReturn As New List(Of String)
        For Each cbItem As ListItem In cblAnswers.Items
            If cbItem.Selected Then
                myReturn.Add(cbItem.Value)
            End If
        Next
        Return myReturn.ToArray()
    End Function

    Public Sub SetControlID(myID As String) Implements ISurveyQuestionControl.SetControlID
        Me.ID = myID
    End Sub
End Class
