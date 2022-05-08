Imports ControlOrigins.COUtility

Public Class QuestionItem
    Public Property QuestionID As Integer
    Public Property QuestionSort As Integer
    Public Property QuestionNM As String
    Public Property QuestionShortNM As String
    Public Property QuestionTypeCD As String
    Public Property QuestionTypeID As Integer
    Public Property ControlNM As String
    Public Property AnswerDataType As String
    Public Property CommentFL As Boolean
    Public Property MaxQuestionValue As Integer
    Public Property QuestionDS As String
    Public Property QuestionValue As Decimal
    Public Property ReviewRoleLevel As Integer
    Public Property SurveyTypeID As Integer
    Public Property UnitOfMeasureID As Integer
    Public Property FileData As Byte()
    Public Property ModifiedID As Integer
    Public Property Keywords As String
    '
    ' Possible Answers for this Question
    '
    Public Property QuestionAnswerItemList As New List(Of QuestionAnswerItem)
    '
    '  Only Implemented when Question is in a QuestionGroup or SurveyQuestionList
    Public Property QuestionGroupMember As New QuestionGroupMemberItem
    '  Only Implemented when Question is in a SurveyResponse.Survey.Question 
    Public Property SurveyResponseAnswerItemList As New List(Of SurveyResponseAnswerItem)
    Public Property SurveyDisplayOrder As Integer
    Public Property SurveyLookupList As New List(Of LookupItem)
End Class
