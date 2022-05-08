Public Class SurveyCurrency
    Inherits SurveyQuestionControl
    Implements ISurveyQuestionControl
    Public Sub SetQuestion(ByRef myQuestion As CODataCon.com.controlorigins.ws.QuestionItem, ByVal CurAnswers As List(Of CODataCon.com.controlorigins.ws.SurveyResponseAnswerItem)) Implements ISurveyQuestionControl.SetQuestion
        litQuestion.Text = GetQuestionDisplay(myQuestion)
        hfQAID.Value = myQuestion.QuestionAnswerItemList(0).QuestionAnswerID
        If CurAnswers.Count > 0 Then
            tb_text.Text = CurAnswers.Item(0).AnswerQuantity
            SurveyAnswerID.Value = CurAnswers.Item(0).SurveyAnswerID
        Else
            SurveyAnswerID.Value = "-1"
        End If
    End Sub

    Public Function GetAnswer(ByVal SurveyResponseID As Integer, ByVal SequenceNumber As Integer, ByVal QuestionID As Integer) As CODataCon.com.controlorigins.ws.SurveyResponseAnswerItem Implements ISurveyQuestionControl.GetAnswer
        Dim newAnswer As New CODataCon.com.controlorigins.ws.SurveyResponseAnswerItem
        With newAnswer
            .SurveyResponseID = SurveyResponseID
            .SequenceNumber = SequenceNumber
            .SurveyAnswerID = SurveyAnswerID.Value
            .QuestionID = QuestionID
            .QuestionAnswerID = hfQAID.Value
            .AnswerDate = Nothing
            .AnswerComment = String.Empty
            .AnswerQuantity = CODataCon.AppUtility.GetDBDecimal(tb_text.Text)
            .ModifiedComment = String.Empty
            .ModifiedDT = Now
            .ModifiedID = UserInfo.ApplicationUserID
            .ResponseList = GetResponseList()
        End With
        Return newAnswer
    End Function

    Private Function GetResponseList() As String()
        Dim myReturn As New List(Of String)
        myReturn.Add(tb_text.Text)
        Return myReturn.ToArray()
    End Function

    Public Sub SetControlID(myID As String) Implements ISurveyQuestionControl.SetControlID
        Me.ID = myID
    End Sub

End Class
