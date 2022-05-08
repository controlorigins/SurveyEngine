Imports CODataCon.com.controlorigins.ws

Public Interface ISurveyResponse
    Property SurveyResponseID As Integer
    Property SurveyResponseNM As String
    Property StatusID As Integer
    Property ApplicationID As Integer
    Property AssignedUserID As Nullable(Of Integer)
    Property AssignedSupervisorUserID As Nullable(Of Integer)
    Property DataSource As String
    Property ModifiedID As Integer
    Property SupervisorAccountNM As String
    Property AccountNM As String
    Property StatusNM As String

    Property ShowQuestionDescription As Boolean

    Property PercentComplete As Integer
    Property AnswerCount As Integer
    Property QuestionCount As Integer
    Property VariantAnswersCount As Integer
    Property ComplianceReviewCount As Integer

    Property ManagerUserID As String
    Property Manager_Name As String
    Property Employee_FName As String
    Property Employee_LName As String

    Property Survey As SurveyItem
    Property SequenceList As List(Of SurveyResponseSequenceItem)
    Property AnswerList As List(Of SurveyResponseAnswerItem)
    Property NewAnswerList As List(Of SurveyResponseAnswerItem)
    Property NewAnswerReviewList As List(Of SurveyResponseAnswerReviewItem)
    Property SurveyResponseHistory As List(Of SurveyResponseHistoryItem)
    Property StateList As List(Of SurveyResponseStateItem)

End Interface
