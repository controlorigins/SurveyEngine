<Serializable()> Public Class QuestionListUtility

    Public Shared Function FindQuestionByQuestionID(ByVal QuestionID As Integer, ByRef ql As List(Of QuestionItem)) As QuestionItem
        Try
            Return (From i In ql Where i.QuestionID = QuestionID).Single
        Catch ex As Exception
            Return New QuestionItem
        End Try
    End Function
    Public Shared Function FindQuestionByQuestionGroupID(ByVal QuestionGroupID As Integer, ByRef ql As List(Of QuestionItem)) As List(Of QuestionItem)
        Try
            Return (From i In ql Where i.QuestionGroupMember.QuestionGroupID = QuestionGroupID).ToList
        Catch ex As Exception
            Return New List(Of QuestionItem)
        End Try
    End Function
    Public Shared Function FindQuestionAnswerByQuestionID(ByVal QuestionID As Integer, ByVal QuestionAnswerID As Integer, ByRef ql As List(Of QuestionItem)) As QuestionAnswerItem
        Dim rAnswer As New QuestionAnswerItem With {.QuestionID = QuestionID, .QuestionAnswerID = -1}
        Try
            For Each myAnswer As QuestionAnswerItem In FindQuestionByQuestionID(QuestionID, ql).QuestionAnswerItemList
                If myAnswer.QuestionAnswerID = QuestionAnswerID Then
                    With myAnswer
                        rAnswer.QuestionAnswerID = myAnswer.QuestionAnswerID
                        rAnswer.QuestionAnswerNM = myAnswer.QuestionAnswerNM
                        rAnswer.QuestionAnswerActiveFL = myAnswer.QuestionAnswerActiveFL
                        rAnswer.QuestionAnswerCommentFL = myAnswer.QuestionAnswerCommentFL
                        rAnswer.QuestionAnswerDS = myAnswer.QuestionAnswerDS
                        rAnswer.QuestionAnswerValue = myAnswer.QuestionAnswerValue
                        rAnswer.QuestionAnswerShortNM = myAnswer.QuestionAnswerShortNM
                        rAnswer.QuestionID = myAnswer.QuestionID
                        rAnswer.QuestionAnswerSort = myAnswer.QuestionAnswerSort
                    End With
                    Exit For
                End If
            Next
        Catch ex As Exception
            rAnswer = New QuestionAnswerItem With {.QuestionID = QuestionID, .QuestionAnswerID = -1}
        End Try
        Return rAnswer
    End Function

    Public Shared Function MissingQuestionGroupIDCount(ByRef qList As List(Of QuestionItem)) As Integer
        Dim icount As Integer = 0
        For Each myQuestion As QuestionItem In qList
            If myQuestion.QuestionGroupMember.QuestionGroupID = 0 Then
                icount = icount + 1
            End If
        Next
        Return icount
    End Function
End Class
