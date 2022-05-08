Imports CODataCon.com.controlorigins.ws
Imports CODataCon

Public Class Co_Apps_SurveyAdmin_SurveyType
    Inherits SurveyUserControlBase
    Public mySurveyType As SurveyTypeItem
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        hfSurveyTypeID.Value = AppUtility.GetDBInteger(GetPageArgument("surveytypeid").Second, 0)
        If Not IsPostBack Then
            ddlParentSurveyType.Items.Clear()
            ddlParentSurveyType.Items.Add(New ListItem With {.Value = 0, .Text = "No Parent", .Selected = True})
            ddlParentSurveyType.AppendDataBoundItems = True
            ddlParentSurveyType.DataSource = myCon.GetSurveyCategoryList
            ddlParentSurveyType.DataValueField = "SurveyTypeID"
            ddlParentSurveyType.DataTextField = "SurveyTypeNM"
            ddlParentSurveyType.DataBind()

            ddlApplicationType.Items.Clear()
            ddlApplicationType.Items.Add(New ListItem With {.Value = 0, .Text = "No Parent", .Selected = True})
            ddlApplicationType.AppendDataBoundItems = True
            ddlApplicationType.DataSource = myCon.GetApplicationTypeList
            ddlApplicationType.DataValueField = "ApplicationTypeID"
            ddlApplicationType.DataTextField = "ApplicationTypeNM"
            ddlApplicationType.DataBind()


            If AppUtility.GetDBInteger(hfSurveyTypeID.Value) > 0 Then
                mySurveyType = myCon.GetSurveyTypeBySurveyTypeID(AppUtility.GetDBInteger(hfSurveyTypeID.Value))
                With mySurveyType
                    tbSurveyTypeNM.Text = .SurveyTypeNM
                    tbSurveyTypeShortNM.Text = .SurveyTypeShortNM
                    tbSurveyTypeDS.Text = .SurveyTypeDS
                    tbSurveyTypeComment.Text = .SurveyTypeComment
                    cbMultipleSequence.Checked = .MultiSequence
                    ddlParentSurveyType.SelectedValue = AppUtility.GetDBInteger(.ParentSurveyTypeID, 0)
                    Dim myGrid = myDACon.GetQuestions(AppUtility.GetDBInteger(hfSurveyTypeID.Value))
                    Dim myDisplayTableHeader As New DataGridVisualization.DisplayTableHeader
                    myDisplayTableHeader.TableTitle = "Question List (<a href='/CO_Apps/SurveyAdmin/navigator.aspx?action=questionview&questionid=-1'>New Question</a>)"
                    myDisplayTableHeader.DetailFieldName = "QuestionNM"
                    myDisplayTableHeader.DetailKeyName = "QuestionID"
                    myDisplayTableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=questionview&questionid={0}"
                    myDisplayTableHeader.AddHeaderItem("QuestionShortNM", "QuestionShortNM")
                    myDisplayTableHeader.AddHeaderItem("QuestionValue", "QuestionValue")
                    myDisplayTableHeader.AddHeaderItem("QuestionTypeCD", "QuestionTypeCD")
                    myDisplayTableHeader.AddHeaderItem("AnswerCount", "AnswerCount")
                    myDisplayTableHeader.AddHeaderItem("SurveyCount", "SurveyCount")
                    myDisplayTableHeader.AddHeaderItem("MinScore", "MinScore")
                    myDisplayTableHeader.AddHeaderItem("MaxScore", "MaxScore")
                    dtQuestion.BuildTableFromGrid(myDisplayTableHeader, myGrid)
                End With
            End If
        End If
    End Sub
    Protected Sub cmd_SaveSurveyType_Click(sender As Object, e As EventArgs)
        If hfSurveyTypeID.Value = "-1" Then
            mySurveyType = New SurveyTypeItem With {.SurveyTypeID = -1}
        Else
            mySurveyType = myCon.GetSurveyTypeBySurveyTypeID(AppUtility.GetDBInteger(hfSurveyTypeID.Value))
        End If
        With mySurveyType
            .SurveyTypeNM = tbSurveyTypeNM.Text
            .SurveyTypeShortNM = tbSurveyTypeShortNM.Text
            .SurveyTypeDS = tbSurveyTypeDS.Text
            .SurveyTypeComment = tbSurveyTypeComment.Text
            .MultiSequence = cbMultipleSequence.Checked
            .ParentSurveyTypeID = AppUtility.GetDBInteger(ddlParentSurveyType.SelectedValue, 0)
            .ApplicationTypeID = AppUtility.GetDBInteger(ddlApplicationType.SelectedValue,0)
        End With
        myCon.PutSurveyType(mySurveyType)
        ReloadPage()
    End Sub
    Protected Sub cmd_CancelSurveyType_Click(sender As Object, e As EventArgs)
        ReloadPage()
    End Sub
    Protected Sub cmd_DeleteSurveyType_Click(sender As Object, e As EventArgs)
        mySurveyType = myCon.GetSurveyTypeBySurveyTypeID(AppUtility.GetDBInteger(hfSurveyTypeID.Value))
        myCon.DeleteSurveyType(mySurveyType)
        ReloadPage()
    End Sub
    Protected Sub ReloadPage()
        ClearPageArguments()
        SetPageArgument("surveytypeid", 0)
        SetPageArgument("action", "surveytypeview")
        LoadPage()
    End Sub


End Class
