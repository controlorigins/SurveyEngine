Imports CODataCon.com.controlorigins.ws

<Serializable()> Public Class QuestionGroupListUtility
    Public Shared Function FindQuestionGroupByQuestionGroupID(ByVal QuestionGroupID As Integer, ByRef ql As List(Of QuestionGroupItem)) As QuestionGroupItem
        Try
            Return (From i In ql Where i.QuestionGroupID = QuestionGroupID).Single
        Catch ex As Exception
            Return New QuestionGroupItem With {.QuestionGroupID = QuestionGroupID}
        End Try
    End Function
End Class
