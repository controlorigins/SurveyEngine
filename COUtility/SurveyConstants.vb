Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Web
Imports System.IO
Imports System.Web.UI
Imports System.Xml.Serialization
Namespace COUtility

Public Module SurveyConstants
    Public Const MsgConfirmDeleteSitePropertyFormatString As String = "return confirm('{0}');"
    Public Const PageNameManageSiteProperties As String = "default.aspx"
    Public Const QueryKeyParamProperty As String = "property"
    Public Const QueryKeyParamSource As String = "Source"
    Public Const STR_DefaultResponse As String = "<p>{0}</p>"
    Public Const STR_DefaultResponseComment As String = "<p>{0}</p><blockquote>""{1}""</blockquote>"
    Public Const STR_DefaultReview As String = "<p>{0} by {1}</p>"
    Public Const STR_DefaultReviewComment As String = "<p>{0} by {1}</p><blockquote>""{4}""</blockquote>"
    Public Const STR_ResponseDIV As String = "<div class=""QuestionResponse""><h3>{3} ({1}-{2})</h3>{0}</div>"
    Public Const STR_SurveyResponseID As String = "CurrentSurveyResponseID"
    Public Const STR_CurrentStatusID As String = "CurrentStatusID"
    Public Const STR_CurrentQuestionGroupID As String = "CurrentQuestionGroupID"
    Public Const STR_NextQuestionGroupID As String = "NextQuestionGroupID"
    Public Const STR_PreviousQuestionGroupID As String = "PreviousQuestionGroupID"
    Public Const STR_QuestionAnswerName As String = "QAN-{0}-{1}-{2}"
    Public Const STR_QuestionAnswerComment As String = "QAC-{0}-{1}-{2}"
    Public Const STR_QuestionAnswerItem As String = "QAI-{0}-{1}-{2}-{3}"
    Public Const STR_QuestionAnswerReviewItem As String = "QARI-{0}-{1}-{2}-{3}"
    Public Const STR_SurveyQuestionAnswerID As String = "SQAI-{0}-{1}-{2}"
    Public Const STR_SurveyQuestionAnswerReviewID As String = "SQARI-{0}-{1}-{2}"
End Module
End Namespace