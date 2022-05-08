Imports CODataCon.com.controlorigins.ws
Partial Class Co_Apps_SurveyApp_controls_SurveyResponseQuickAdd
    Inherits SurveyUserControlBase
    Event NewSurveyResponseAdded(surveyResponse As SurveyResponseItem)
    Public WriteOnly Property BaseSurveyResponse As SurveyResponseItem
        Set(value As SurveyResponseItem)
            SurveyResponseID = value.SurveyResponseID
            If value.SurveyResponseID > 0 Then
                glyphplus.Visible = False
            End If
            SurveyResponseForm1.SetSurveyResponse(value, AppInfo)
        End Set
    End Property
    Public Property SurveyResponseID As Integer
        Get
            If String.IsNullOrEmpty(hfSurveyResponseID.Value) Then
                Return -1
            Else
                Return hfSurveyResponseID.Value
            End If
        End Get
        Set(value As Integer)
            hfSurveyResponseID.Value = value
        End Set
    End Property
    Sub SetSurveryResponseItem(sri As SurveyResponseItem)
        SurveyResponseForm1.SetSurveyResponse(sri, AppInfo)
    End Sub
    Protected Sub CMD_Save_Click(sender As Object, e As EventArgs)
        Dim myCon As New CODataCon.DataControler()
        Dim mySR = SurveyResponseForm1.GetSurveyResponse
        myCon.PutSurveyResponseItem(mySR)
        RaiseEvent NewSurveyResponseAdded(mySR)
    End Sub
End Class
