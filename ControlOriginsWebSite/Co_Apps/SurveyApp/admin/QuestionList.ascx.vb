Partial Class Co_Apps_SurveyApp_admin_QuestionList
    Inherits SurveyUserControlBase

    Protected Sub ddlQuestionCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim myCon As New DataGridVisualization.DataController()
        Dim myGrid = myCon.GetQuestions(ddlQuestionCategory.SelectedValue)
        Dim myDisplayTableHeader As New DataGridVisualization.DisplayTableHeader
        myDisplayTableHeader.DetailFieldName = "QuestionNM"
        myDisplayTableHeader.DetailKeyName = "QuestionID"
        myDisplayTableHeader.DetailPath = String.Format("/navigator.aspx?applicationid={0}&applicationuserid={1}", AppInfo.ApplicationID, CurUser.ApplicationUserID) & "&surveyresponseid={0}"
        For Each myCol In myGrid.GridColumns
            If myCol.DataType = "string" Or myCol.DataType = "integer" Then
                myDisplayTableHeader.AddHeaderItem(myCol.DisplayName, myCol.SourceName, False)
            End If
        Next
        dtList.BuildTableFromGrid(myDisplayTableHeader, myGrid)
    End Sub
End Class
