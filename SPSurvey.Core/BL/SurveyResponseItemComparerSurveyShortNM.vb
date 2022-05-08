
Public Class SurveyResponseItemComparerSurveyShortNM
    Implements IComparer(Of SurveyResponseItem)

    Public Function Compare(ByVal p1 As SurveyResponseItem, _
                            ByVal p2 As SurveyResponseItem) As Integer Implements IComparer(Of SurveyResponseItem).Compare
        Return p1.Survey.SurveyShortNM.CompareTo(p2.Survey.SurveyShortNM)
    End Function
End Class
