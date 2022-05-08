Imports CODataCon.com.controlorigins.ws

Public MustInherit Class SurveyQuestionControl
    Inherits ApplicationControlBase

    Public Function GetQuestionDisplay(ByRef myQuestion As QuestionItem) As String
        If myQuestion.QuestionDS.Trim = String.Empty Then
            Return String.Format("{0}. {1}<br/>", myQuestion.SurveyDisplayOrder, myQuestion.QuestionNM)
        Else
            Return String.Format("{0}. {1}<span class='COSurvey_QuestionDS'>{2}</span>", myQuestion.SurveyDisplayOrder, myQuestion.QuestionNM, myQuestion.QuestionDS)
        End If
    End Function

End Class
