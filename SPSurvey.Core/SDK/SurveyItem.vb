Imports ControlOrigins.COUtility

Public Class SurveyItem

    Public Property SurveyID As Integer
    Public Property SurveyNM As String
    Public Property SurveyDS As String
    Public Property SurveyShortNM As String
    Public Property CompletionMessage As String
    Public Property AutoAssignFilter As String
    Public Property ResponseNMTemplate As String
    Public Property ReviewerAccountNM As String
    Public Property UseSurveyGroupsFL As Boolean
    Public Property EndDT As Nullable(Of Date)
    Public Property StartDT As Nullable(Of Date)
    Public Property ModifiedID As Integer
    Public Property ParentSurveyID As Integer?
    Public Property QuestionGroupList As New List(Of QuestionGroupItem)
    Public Property QuestionList As New List(Of QuestionItem)
    Public Property EmailTemplateList As New List(Of SurveyEmailTemplateItem)
    Public Property ReviewStatusList As New List(Of SurveyReviewStatusItem)
    Public Property StatusList As New List(Of SurveyStatusItem)
    Public Property SurveyType As New SurveyTypeItem
    Public Property ApplicationLookup As New List(Of LookupItem)
    Public Property ApplicationCount As Integer
    Public Property SurveyResponseCount As Integer
    Public Property QuestionCount As Integer
    Public Property QuestionGroupCount As Integer


End Class
