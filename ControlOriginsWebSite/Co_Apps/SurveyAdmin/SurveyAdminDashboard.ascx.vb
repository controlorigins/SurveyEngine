Imports CODataCon
Imports CODataCon.com.controlorigins.ws
Imports DataGridVisualization.ControlOriginsWS

Public Class Co_Apps_SurveyAdmin_Dashboard
    Inherits SurveyUserControlBase

    Public Enum ViewType
        Application
        ApplicationType
        Company
        User
        Survey
        Question
        SurveyResponse
        SurveyCategory
        QuestionCategory
    End Enum

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        dtList.Visible = False

        Select Case GetPageArgument("action").Second
            Case "applicationview"
                SetActiveMenu(ViewType.Application)
                If AppUtility.GetDBInteger(GetPageArgument("applicationid").Second, 0) = 0 Then
                    cmd_GetApplications_Click(Nothing, New EventArgs)
                Else
                    myControl = DirectCast(Page.LoadControl("~/Co_Apps/SurveyAdmin/Controls/ApplicationItem.ascx"), SurveyUserControlBase)
                    pnlEdit.Controls.Add(myControl)
                End If
            Case "applicationuserview"
                SetActiveMenu(ViewType.User)
                If AppUtility.GetDBInteger(GetPageArgument("applicationuserid").Second, 0) = 0 Then
                    cmd_GetUsers_Click(Nothing, New EventArgs)
                Else
                    myControl = DirectCast(Page.LoadControl("~/Co_Apps/SurveyAdmin/Controls/ApplicationUserItem.ascx"), SurveyUserControlBase)
                    pnlEdit.Controls.Add(myControl)
                End If
            Case "questionview"
                If AppUtility.GetDBInteger(GetPageArgument("questionid").Second, 0) = 0 Then
                    SetActiveMenu(ViewType.Question)
                    pnlEdit.Visible = False
                    cmd_GetQuestions_Click(Nothing, New EventArgs)
                Else
                    SetActiveMenu(ViewType.Question)
                    pnlEdit.Visible = True
                    myControl = DirectCast(Page.LoadControl("~/Co_Apps/SurveyAdmin/Controls/QuestionItem.ascx"), SurveyUserControlBase)
                    pnlEdit.Controls.Add(myControl)
                End If
            Case "questionclone"
                If AppUtility.GetDBInteger(GetPageArgument("questionid").Second, 0) = 0 Then
                    SetActiveMenu(ViewType.Question)
                    pnlEdit.Visible = False
                    cmd_GetQuestions_Click(Nothing, New EventArgs)
                Else
                    SetActiveMenu(ViewType.Question)
                    pnlEdit.Visible = True
                    myControl = DirectCast(Page.LoadControl("~/Co_Apps/SurveyAdmin/Controls/BulkQuestionItem.ascx"), SurveyUserControlBase)
                    pnlEdit.Controls.Add(myControl)
                End If
            Case "surveyview"
                SetActiveMenu(ViewType.Survey)
                If AppUtility.GetDBInteger(GetPageArgument("surveyid").Second, 0) = 0 Then
                    cmd_GetSurveys_Click(Nothing, New EventArgs)
                Else
                    myControl = DirectCast(Page.LoadControl("~/Co_Apps/SurveyAdmin/Controls/SurveyItem.ascx"), SurveyUserControlBase)
                    pnlEdit.Controls.Add(myControl)
                End If
            Case "surveyresponseview"
                SetActiveMenu(ViewType.SurveyResponse)
                If AppUtility.GetDBInteger(GetPageArgument("surveyresponseid").Second, 0) = 0 Then
                    ''
                Else
                    myControl = DirectCast(Page.LoadControl("~/Co_Apps/SurveyAdmin/Controls/SurveyResponseItem.ascx"), SurveyUserControlBase)
                    pnlEdit.Controls.Add(myControl)
                End If
            Case "surveytypeview"
                SetActiveMenu(ViewType.SurveyCategory)
                If AppUtility.GetDBInteger(GetPageArgument("surveytypeid").Second, 0) = 0 Then
                    cmd_GetSurveyType_Click(Nothing, New EventArgs)
                Else
                    myControl = DirectCast(Page.LoadControl("~/Co_Apps/SurveyAdmin/Controls/SurveyTypeItem.ascx"), SurveyUserControlBase)
                    pnlEdit.Controls.Add(myControl)
                End If
            Case "applicationtypeview"
                SetActiveMenu(ViewType.ApplicationType)
                If AppUtility.GetDBInteger(GetPageArgument("applicationtypeid").Second, 0) = 0 Then
                    cmd_GetApplicationType_Click(Nothing, New EventArgs)
                Else
                    myControl = DirectCast(Page.LoadControl("~/Co_Apps/SurveyAdmin/Controls/applicationtypeItem.ascx"), SurveyUserControlBase)
                    pnlEdit.Controls.Add(myControl)
                End If
            Case "companyview"
                SetActiveMenu(ViewType.Company)
                If AppUtility.GetDBInteger(GetPageArgument("companyid").Second, 0) = 0 Then
                    cmd_GetCompany_Click(Nothing, New EventArgs)
                Else
                    myControl = DirectCast(Page.LoadControl("~/Co_Apps/SurveyAdmin/Controls/CompanyItem.ascx"), SurveyUserControlBase)
                    pnlEdit.Controls.Add(myControl)
                End If
            Case Else
        End Select
    End Sub


    Private _myControl As SurveyUserControlBase
    Public Property myControl As SurveyUserControlBase
        Get
            Return _myControl
        End Get
        Set(ByVal value As SurveyUserControlBase)
            _myControl = value
        End Set
    End Property

    Protected Sub cmd_GetApplications_Click(sender As Object, e As EventArgs)
        dtList.Visible = True
        pnlEdit.Controls.Clear()
        Dim myGrid As DataGridVisualization.ControlOriginsWS.CO_DataGrid
        Dim myCon As New CODataCon.DataControler()
        Dim myDACon As New DataGridVisualization.DataController()
        SetActiveMenu(ViewType.Application)

        myGrid = myDACon.GetApplicationGrid()
        Dim myDisplayTableHeader As New DataGridVisualization.DisplayTableHeader
        myDisplayTableHeader.TableTitle = "Project List (<a href='/CO_Apps/SurveyAdmin/navigator.aspx?action=applicationview&applicationid=-1'>New Project</a>)"
        myDisplayTableHeader.DetailFieldName = "ApplicationNM"
        myDisplayTableHeader.DetailKeyName = "ApplicationID"
        myDisplayTableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=applicationview&applicationid={0}"

        myDisplayTableHeader.AddHeaderItem("ApplicationCD", "ApplicationCD", False, DisplayFormat.Text)
        myDisplayTableHeader.AddHeaderItem("ApplicationTypeNM", "ApplicationTypeNM", False, DisplayFormat.Text)
        myDisplayTableHeader.AddHeaderItem("CompanyNM", "CompanyNM")
        myDisplayTableHeader.AddHeaderItem("UserCount", "UserCount", True, DisplayFormat.Number)
        myDisplayTableHeader.AddHeaderItem("SurveyCount", "SurveyCount", True, DisplayFormat.Number)
        myDisplayTableHeader.AddHeaderItem("SurveyResponseCount", "SurveyResponseCount", True, DisplayFormat.Number)
        dtList.BuildTableFromGrid(myDisplayTableHeader, myGrid)
    End Sub

    Protected Sub cmd_GetQuestions_Click(sender As Object, e As EventArgs)
        dtList.Visible = True
        pnlEdit.Controls.Clear()
        SetActiveMenu(ViewType.Question)

        Dim myGrid As CO_DataGrid
        Using myCon As New CODataCon.DataControler()
            myGrid = myDACon.GetQuestions()
        End Using
        Dim x As New QuestionItem
        ' x.QuestionSort

        dtList.TableHeader = New DataGridVisualization.DisplayTableHeader
        dtList.TableHeader.TableTitle = "Question List (<a href='/CO_Apps/SurveyAdmin/navigator.aspx?action=questionview&questionid=-1'>New Question</a>)"
        dtList.TableHeader.DetailFieldName = "QuestionSort"
        dtList.TableHeader.DetailKeyName = "QuestionID"
        dtList.TableHeader.DetailDisplayName = "Default Order"
        dtList.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=questionview&questionid={0}"
        dtList.TableHeader.AddLinkHeaderItem("QuestionNM",
                                                     "QuestionNM",
                                                     "/CO_Apps/SurveyAdmin/navigator.aspx?action=questionview&questionid={0}",
                                                      "QuestionID",
                                                     "QuestionNM")
        dtList.TableHeader.AddHeaderItem("Category", "SurveyTypeNM")
        dtList.TableHeader.AddHeaderItem("QuestionType", "QuestionTypeCD")
        dtList.TableHeader.AddHeaderItem("ReviewRoleLevel", "ReviewRoleLevel")
        dtList.TableHeader.AddHeaderItem("PossibleAnswerCount", "AnswerCount")
        dtList.TableHeader.AddHeaderItem("SurveyCount", "SurveyCount")
        dtList.TableHeader.AddHeaderItem("ResponseAnswerCount", "ResponseAnswerCount")
        dtList.TableHeader.AddHeaderItem("Value", "QuestionValue")
        dtList.TableHeader.AddHeaderItem("MinScore", "MinScore")
        dtList.TableHeader.AddHeaderItem("MaxScore", "MaxScore")

        dtList.BuildTableFromGrid(dtList.TableHeader, myGrid)
    End Sub

    Protected Sub cmd_GetSurveys_Click(sender As Object, e As EventArgs)
        pnlEdit.Controls.Clear()
        dtList.Visible = True
        Dim myGrid As CO_DataGrid
        Dim myCon As New DataGridVisualization.DataController()

        SetActiveMenu(ViewType.Survey)

        myGrid = myDACon.GetSurveySummaryGrid()
        dtList.TableHeader.TableTitle = "Survey List (<a href='/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyview&surveyid=-1'>New Survey</a>)"
        dtList.TableHeader.DetailFieldName = "SurveyNM"
        dtList.TableHeader.DetailKeyName = "SurveyID"
        dtList.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyview&surveyid={0}"

        dtList.TableHeader.AddHeaderItem("SurveyShortNM", "SurveyShortNM")

        dtList.TableHeader.AddHeaderItem("ApplicationCount", "ApplicationCount")
        dtList.TableHeader.AddHeaderItem("SurveyResponseCount", "SurveyResponseCount")
        dtList.TableHeader.AddHeaderItem("QuestionCount", "QuestionCount")
        dtList.TableHeader.AddHeaderItem("QuestionGroupCount", "QuestionGroupCount")

        dtList.BuildTableFromGrid(dtList.TableHeader, myGrid)
    End Sub
    Protected Sub cmd_GetApplicationType_Click(sender As Object, e As EventArgs)
        pnlEdit.Controls.Clear()
        dtList.Visible = True

        SetActiveMenu(ViewType.ApplicationType)

        Dim mycon As New CODataCon.DataControler()

        dtList.EnableCSV = False
        dtList.TableHeader = New DataGridVisualization.DisplayTableHeader
        dtList.TableHeader.TableTitle = "Project Type (<a href='/CO_Apps/SurveyAdmin/navigator.aspx?action=applicationtypeview&applicationtypeid=-1'>New Project Type</a>)"
        dtList.TableHeader.DetailFieldName = "ApplicationTypeNM"
        dtList.TableHeader.DetailKeyName = "ApplicationTypeID"
        dtList.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=ApplicationTypeview&ApplicationTypeid={0}"
        dtList.TableHeader.AddHeaderItem("ApplicationCount", "ApplicationCount")
        dtList.TableHeader.AddHeaderItem("SurveyTypeCount", "SurveyTypeCount")
        dtList.BuildTable(dtList.TableHeader, mycon.GetApplicationTypeList())

    End Sub
    Protected Sub cmd_GetSurveyType_Click(sender As Object, e As EventArgs)
        pnlEdit.Controls.Clear()
        dtList.Visible = True
        Dim mycon As New CODataCon.DataControler()

        SetActiveMenu(ViewType.SurveyCategory)

        ' Dim x = New SurveyTypeItem


        dtList.EnableCSV = False

        dtList.TableHeader = New DataGridVisualization.DisplayTableHeader
        dtList.TableHeader.TableTitle = "Survey Category (<a href='/CO_Apps/SurveyAdmin/navigator.aspx?action=surveytypeview&surveytypeid=-1'>New Category</a>)"
        dtList.TableHeader.DetailFieldName = "SurveyTypeNM"
        dtList.TableHeader.DetailKeyName = "SurveyTypeID"
        dtList.TableHeader.DetailDisplayName = "Survey Category"
        dtList.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveytypeview&surveytypeid={0}"
        dtList.TableHeader.AddHeaderItem("Project Type", "ApplicationTypeNM")
        dtList.TableHeader.AddHeaderItem("QuestionCount", "QuestionCount")
        dtList.TableHeader.AddHeaderItem("SurveyCount", "SurveyCount")
        dtList.TableHeader.AddHeaderItem("ChildCount", "ChildCount")
        dtList.TableHeader.AddHeaderItem("LevelNumber", "LevelNumber")

        dtList.BuildTable(dtList.TableHeader, mycon.GetSurveyCategoryList())

    End Sub

    Protected Sub cmd_GetUsers_Click(sender As Object, e As EventArgs)
        pnlEdit.Controls.Clear()
        dtList.Visible = True
        Dim myGrid As New List(Of Object)
        Dim myCon As New CODataCon.DataControler()

        SetActiveMenu(ViewType.User)


        myGrid.AddRange(myCon.GetApplicationUserList())
        Dim myDisplayTableHeader As New DataGridVisualization.DisplayTableHeader
        myDisplayTableHeader.TableTitle = "User List (<a href='/CO_Apps/SurveyAdmin/navigator.aspx?action=applicationuserview&applicationuserid=-1'>New User</a>)"
        myDisplayTableHeader.AddLinkHeaderItem("eMailAddress",
                                           "eMailAddress",
                                           "/CO_Apps/SurveyAdmin/navigator.aspx?action=applicationuserview&applicationuserid={0}",
                                           "ApplicationUserID",
                                           "eMailAddress")
        myDisplayTableHeader.AddHeaderItem("Applicatons", "ApplicatonUserRoleCount")
        myDisplayTableHeader.AddHeaderItem("SurveyResponses", "SurveyResponseCount")
        myDisplayTableHeader.AddHeaderItem("FirstNM", "FirstNM")
        myDisplayTableHeader.AddHeaderItem("LastNM", "LastNM")
        myDisplayTableHeader.AddHeaderItem("SupervisorAccountNM", "SupervisorAccountNM")
        myDisplayTableHeader.AddHeaderItem("CompanyNM", "CompanyNM")
        dtList.BuildTable(myDisplayTableHeader, myGrid)
    End Sub


    Protected Sub cmd_GetCompany_Click(sender As Object, e As EventArgs)
        pnlEdit.Controls.Clear()
        dtList.Visible = True

        SetActiveMenu(ViewType.Company)

        Dim mycon As New CODataCon.DataControler()
        dtList.EnableCSV = False
        dtList.TableHeader = New DataGridVisualization.DisplayTableHeader
        dtList.TableHeader.TableTitle = "Company (<a href='/CO_Apps/SurveyAdmin/navigator.aspx?action=companyview&companyid=-1'>New Company</a>)"
        dtList.TableHeader.DetailFieldName = "CompanyNM"
        dtList.TableHeader.DetailKeyName = "CompanyID"
        dtList.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=companyview&companyid={0}"
        dtList.TableHeader.AddHeaderItem("CompanyCD", "CompanyCD")
        dtList.TableHeader.AddHeaderItem("CompanyDS", "CompanyDS")
        dtList.TableHeader.AddHeaderItem("ProjectCount", "ProjectCount")
        dtList.TableHeader.AddHeaderItem("UserCount", "UserCount")

        dtList.BuildTable(dtList.TableHeader, mycon.GetCompanyList())
    End Sub



    Public Sub SetActiveMenu(ByVal activeType As ViewType)
        ' cmd_GetApplications.CssClass = ""
        cmd_GetApplicationType.CssClass = ""
        ' cmd_GetCompany.CssClass = ""
        ' cmd_GetQuestions.CssClass = ""
        cmd_GetSurveys.CssClass = ""
        cmd_GetSurveyType.CssClass = ""
        ' cmd_GetUsers.CssClass = ""
        cmd_GetQuestionCategory.CssClass = ""

        Select Case activeType
            Case ViewType.Application
                '        cmd_GetApplications.CssClass = "active"
            Case ViewType.ApplicationType
                cmd_GetApplicationType.CssClass = "active"
            Case ViewType.Company
                '     cmd_GetCompany.CssClass = "active"
            Case ViewType.Question
                '      cmd_GetQuestions.CssClass = "active "
            Case ViewType.Survey
                cmd_GetSurveys.CssClass = "active"
            Case ViewType.SurveyCategory
                cmd_GetSurveyType.CssClass = "active"
            Case ViewType.QuestionCategory
                cmd_GetQuestionCategory.CssClass = "active"
            Case ViewType.User
                ' cmd_GetUsers.CssClass = "active"
            Case ViewType.SurveyResponse
            Case Else
        End Select
    End Sub

    Protected Sub cmd_GetQuestionCategory_Click(sender As Object, e As EventArgs)
        pnlEdit.Controls.Clear()
        dtList.Visible = True
        Dim mycon As New CODataCon.DataControler()

        SetActiveMenu(ViewType.QuestionCategory)

        ' Dim x = New SurveyTypeItem


        dtList.EnableCSV = False

        dtList.TableHeader = New DataGridVisualization.DisplayTableHeader
        dtList.TableHeader.TableTitle = "Question Category (<a href='/CO_Apps/SurveyAdmin/navigator.aspx?action=surveytypeview&surveytypeid=-1'>New Category</a>)"
        dtList.TableHeader.DetailFieldName = "SurveyTypeNM"
        dtList.TableHeader.DetailKeyName = "SurveyTypeID"
        dtList.TableHeader.DetailDisplayName = "Question Category"
        dtList.TableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveytypeview&surveytypeid={0}"
        dtList.TableHeader.AddHeaderItem("Project Type", "ApplicationTypeNM")
        dtList.TableHeader.AddHeaderItem("QuestionCount", "QuestionCount")
        dtList.TableHeader.AddHeaderItem("SurveyCount", "SurveyCount")
        dtList.TableHeader.AddHeaderItem("ChildCount", "ChildCount")
        dtList.TableHeader.AddHeaderItem("LevelNumber", "LevelNumber")

        dtList.BuildTable(dtList.TableHeader, mycon.GetQuestionCategoryList())


    End Sub
End Class
