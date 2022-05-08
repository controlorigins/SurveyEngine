Public Class SurveyResponseAnswerItem
    Public Property DisplayAnswerNM As String 
    Public Property DisplayAnswerComment As String 
    Public Property SurveyAnswerID As Integer 
    Public Property SurveyResponseID As Integer
    Public Property SequenceNumber As Integer 
    Public Property QuestionID As Integer 
    Public Property QuestionAnswerID As Integer 
    Public Property AnswerType As String 
    Public Property AnswerQuantity As Double 
    Public Property AnswerDate As Nullable(Of DateTime) 
    Public Property AnswerComment As String 
    Public Property ModifiedComment As String 
    Public Property ModifiedID As Integer 
    Public Property ModifiedDT As Date 
    Public Property QuestionAnswerNM As String 
    Public Property QuestionValue As Decimal 
    Public Property QuestionAnswerValue As Decimal 
    Public Property ResponseList As New List(Of String) 
    Public Property AnswerReviewList As New List(Of SurveyResponseAnswerReviewItem) 

End Class
