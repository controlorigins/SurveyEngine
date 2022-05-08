Public Class QuestionGroupItem
    Property QuestionGroupID As Integer
    Property SurveyID As Integer
    Property QuestionGroupNM As String
    Property QuestionGroupShortNM As String
    Property QuestionGroupDS As String
    Property QuestionGroupOrder As Integer
    Property QuestionGroupWeight As Decimal
    Property QuestionGroupHeader As String
    Property QuestionGroupFooter As String
    Property DependentMaxScore As Decimal?
    Property DependentMinScore As Decimal?
    Property DependentQuestionGroupID As Integer?
    Property QuestionMembership As New List(Of QuestionGroupMemberItem)
    Property ModifiedID As Integer
    Public Property MarkedForDeletion As Boolean = False

End Class