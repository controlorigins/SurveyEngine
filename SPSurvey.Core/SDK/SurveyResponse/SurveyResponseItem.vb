Public Class SurveyResponseItem
    Implements ISurveyResponse

    Public Property SurveyResponseID As Integer Implements ISurveyResponse.SurveyResponseID
    Public Property SurveyResponseNM As String Implements ISurveyResponse.SurveyResponseNM
    Public Property ApplicationID As Integer Implements ISurveyResponse.ApplicationID
    Public Property AssignedUserID As Nullable(Of Integer) Implements ISurveyResponse.AssignedUserID
    Public Property AssignedSupervisorUserID As Integer? Implements ISurveyResponse.AssignedSupervisorUserID
    Public Property DataSource As String Implements ISurveyResponse.DataSource
    Public Property ModifiedID As Integer Implements ISurveyResponse.ModifiedID
    Public Property AccountNM As String Implements ISurveyResponse.AccountNM
    Public Property ShowQuestionDescription As Boolean = False Implements ISurveyResponse.ShowQuestionDescription
    Public Property Survey As New SurveyItem Implements ISurveyResponse.Survey
    Public Property StatusNM As String Implements ISurveyResponse.StatusNM
    Public Property StatusID As Integer Implements ISurveyResponse.StatusID
    Public Property SupervisorAccountNM As String Implements ISurveyResponse.SupervisorAccountNM
    Public Property AnswerCount As Integer Implements ISurveyResponse.AnswerCount
    Public Property ComplianceReviewCount As Integer Implements ISurveyResponse.ComplianceReviewCount
    Public Property Manager_Name As String Implements ISurveyResponse.Manager_Name
    Public Property Employee_FName As String Implements ISurveyResponse.Employee_FName
    Public Property Employee_LName As String Implements ISurveyResponse.Employee_LName
    Public Property ManagerUserID As String Implements ISurveyResponse.ManagerUserID
    Public Property PercentComplete As Integer Implements ISurveyResponse.PercentComplete
    Public Property QuestionCount As Integer Implements ISurveyResponse.QuestionCount
    Public Property VariantAnswersCount As Integer Implements ISurveyResponse.VariantAnswersCount
    Public Property NewAnswerList As New List(Of SurveyResponseAnswerItem) Implements ISurveyResponse.NewAnswerList
    Public Property NewAnswerReviewList As New List(Of SurveyResponseAnswerReviewItem) Implements ISurveyResponse.NewAnswerReviewList
    Public Property SurveyResponseHistory As New List(Of SurveyResponseHistoryItem) Implements ISurveyResponse.SurveyResponseHistory
    Public Property SequenceList As New List(Of SurveyResponseSequenceItem) Implements ISurveyResponse.SequenceList
    Public Property AnswerList As New List(Of SurveyResponseAnswerItem) Implements ISurveyResponse.AnswerList
    Public Property StateList As New List(Of SurveyResponseStateItem) Implements ISurveyResponse.StateList
    Public Property CurrentQuestionGroupID As Integer
    Public Property DaysSinceModified As Integer
    Public Property SurveyResponseScore As Decimal
    Public Property ModifiedDT As Date


End Class
