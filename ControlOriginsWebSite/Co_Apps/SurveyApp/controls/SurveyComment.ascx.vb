Public Class SurveyComment
    Inherits SurveyQuestionControl
    Implements ISurveyQuestionControl
    Public Sub SetQuestion(ByRef myQuestion As CODataCon.com.controlorigins.ws.QuestionItem, ByVal CurAnswers As List(Of CODataCon.com.controlorigins.ws.SurveyResponseAnswerItem)) Implements ISurveyQuestionControl.SetQuestion
        labeltbComment.Text = GetQuestionDisplay(myQuestion)
        hfQAID.Value = myQuestion.QuestionAnswerItemList(0).QuestionAnswerID
        If CurAnswers.Count > 0 Then
            tbComment.Text = CurAnswers.Item(0).AnswerComment
            SurveyAnswerID.Value = CurAnswers.Item(0).SurveyAnswerID
        Else
            SurveyAnswerID.Value = "-1"
        End If
    End Sub

    Public Function GetAnswer(ByVal SurveyResponseID As Integer, ByVal SequenceNumber As Integer, ByVal QuestionID As Integer) As CODataCon.com.controlorigins.ws.SurveyResponseAnswerItem Implements ISurveyQuestionControl.GetAnswer
        Dim newAnswer As New CODataCon.com.controlorigins.ws.SurveyResponseAnswerItem
        With newAnswer
            .SurveyResponseID = SurveyResponseID
            .SurveyAnswerID = CInt(SurveyAnswerID.Value)
            .SequenceNumber = SequenceNumber
            .QuestionID = QuestionID
            .QuestionAnswerID = hfQAID.Value
            .AnswerComment = tbComment.Text
            .ModifiedComment = String.Empty
            .ModifiedDT = Now
            .ModifiedID = UserInfo.ApplicationUserID
            .ResponseList = GetResponseList()
        End With
        Return newAnswer
    End Function

    Private Function GetResponseList() As String()
        Dim myReturn As New List(Of String)
        myReturn.Add(tbComment.Text)
        Return myReturn.ToArray()
    End Function

    Public Sub SetControlID(myID As String) Implements ISurveyQuestionControl.SetControlID
        Me.ID = myID
    End Sub

End Class
