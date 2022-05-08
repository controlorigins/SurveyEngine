Imports CODataCon


Public Class SurveyResponseAnswers
    Inherits SurveyUserControlBase
'    Implements ISurveyResponseDetail

    Dim mypresenter As New SurveyResponseUI()
    Dim writeCSV As Boolean = False

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        mypresenter.SetSurveyResponseListUI(Me)
    End Sub
    'Public Sub GetData(ByVal sWhere As String, ByVal OutputCSV As Boolean, ByVal RowCount As Integer, ByVal StartRow As Integer) Implements ISurveyResponseDetail.GetData
    '    tbFilter.Text = sWhere
    '    Dim mySRAnswerList = mypresenter.GetSurveyResponseDetail(StartRow, RowCount, sWhere)
    '    writeCSV = OutputCSV
    '    tbFilter.Text = sWhere
    '    Dim myTableHeader As New DisplayTableHeader
    '    ' myHeaders.AddHeaderItem("Application ID", "ApplicationID")
    '    ' myHeaders.AddHeaderItem("Application", "ApplicationNM")
    '    ' myHeaders.AddHeaderItem("Assigned User ID", "AssignedUserID")
    '    ' myHeaders.AddHeaderItem("Assigned User", "AssignedUserNM")
    '    ' myHeaders.AddHeaderItem("SurveyID", "SurveyID")
    '    myTableHeader.AddHeaderItem("Survey", "SurveyNM")
    '    myTableHeader.AddHeaderItem("Survey Short NM", "SurveyShortNM")
    '    ' myHeaders.AddHeaderItem("SurveyResponseID", "SurveyResponseID")
    '    myTableHeader.AddHeaderItem("Survey Response", "SurveyResponseNM")
    '    ' myHeaders.AddHeaderItem("SurveyResponseSequenceID", "SurveyResponseSequenceID")
    '    myTableHeader.AddHeaderItem("Sequence Number", "SequenceNumber")
    '    myTableHeader.AddHeaderItem("Sequence Text", "SequenceText")
    '    'myHeaders.AddHeaderItem("QuestionGroupID", "QuestionGroupID")
    '    myTableHeader.AddHeaderItem("Question Group", "QuestionGroupNM")
    '    'myHeaders.AddHeaderItem("Question Group Short", "QuestionGroupShortNM")
    '    myTableHeader.AddHeaderItem("Question Group Weight", "QuestionGroupWeight")
    '    ' myHeaders.AddHeaderItem("QuestionID", "QuestionID")
    '    myTableHeader.AddHeaderItem("Question", "QuestionNM")
    '    'myHeaders.AddHeaderItem("QuestionShortNM", "QuestionShortNM")
    '    myTableHeader.AddHeaderItem("Question Value", "QuestionValue")
    '    myTableHeader.AddHeaderItem("Question Weight", "QuestionWeight")
    '    ' myHeaders.AddHeaderItem("QuestionAnswerID", "QuestionAnswerID")
    '    myTableHeader.AddHeaderItem("Question Answer", "QuestionAnswerNM")
    '    ' myHeaders.AddHeaderItem("Question Answer Short", "QuestionAnswerShortNM")
    '    myTableHeader.AddHeaderItem("Question Answer Value", "QuestionAnswerValue")
    '    myTableHeader.AddHeaderItem("Answer Comment", "AnswerComment")
    '    myTableHeader.AddHeaderItem("Answer Date", "AnswerDate")
    '    myTableHeader.AddHeaderItem("Answer Quantity", "AnswerQuantity")
    '    myTableHeader.AddHeaderItem("Answer Weighted Score", "AnswerWeightedScore")
    '    myTableHeader.TableTitle = "All Responses"
    '    myTableHeader.TableTitle = "Survey Response Answers"

    '    If writeCSV Then
    '        Dim returnSB As New StringBuilder(String.Empty)
    '        returnSB.Append(TryCast(DisplayTable1, Icontrols_DisplayTable).GetCSV(myTableHeader, mySRAnswerList))

    '        HttpContext.Current.Response.Clear()
    '        HttpContext.Current.Response.ClearHeaders()
    '        HttpContext.Current.Response.ClearContent()
    '        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=datasets.csv")
    '        HttpContext.Current.Response.ContentType = "text/csv"
    '        HttpContext.Current.Response.AddHeader("Pragma", "public")
    '        HttpContext.Current.Response.Write(returnSB.ToString())

    '        Response.Flush()
    '        Response.End()
    '    Else
    '        TryCast(DisplayTable1, Icontrols_DisplayTable).BuildTable(myTableHeader, mySRAnswerList)
    '    End If
    'End Sub
End Class
