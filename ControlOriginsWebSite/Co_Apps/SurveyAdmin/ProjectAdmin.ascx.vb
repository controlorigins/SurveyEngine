Imports CODataCon
Imports CODataCon.com.controlorigins.ws
Imports DataGridVisualization.ControlOriginsWS

Public Class Co_Apps_SurveyAdmin_ProjectAdmin
    Inherits SurveyUserControlBase
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim ProjectID As Integer = AppUtility.GetDBInteger(GetPageArgument("projectid").Second, 0)
        dtList.Visible = False
        If  ProjectID= 0 Then
            cmd_GetList_Click(Nothing, New EventArgs)
        Else
            myControl = DirectCast(Page.LoadControl("~/Co_Apps/SurveyAdmin/Controls/ProjectItem.ascx"), SurveyUserControlBase)
            pnlEdit.Controls.Add(myControl)
        End If
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


    Protected Sub cmd_GetList_Click(sender As Object, e As EventArgs)
        Dim mycon As New CODataCon.DataControler()
        Dim myGrid As DataGridVisualization.ControlOriginsWS.CO_DataGrid
'        Dim myDACon As New DataGridVisualization.DataController()
        Dim myDisplayTableHeader As New DataGridVisualization.DisplayTableHeader

        pnlEdit.Controls.Clear()
        dtList.EnableCSV = False
        dtList.Visible = True

        myGrid = myDACon.GetApplicationGrid()
        myDisplayTableHeader.TableTitle = "Project List (<a href='/CO_Apps/SurveyAdmin/navigator.aspx?action=projectview&projectid=-1'>New Project</a>)"
        myDisplayTableHeader.DetailFieldName = "ApplicationNM"
        myDisplayTableHeader.DetailKeyName = "ApplicationID"
        myDisplayTableHeader.DetailDisplayName = "Project"
        myDisplayTableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=projectview&projectid={0}"

        myDisplayTableHeader.AddHeaderItem("Code", "ApplicationCD", False, DisplayFormat.Text)
        myDisplayTableHeader.AddHeaderItem("Type", "ApplicationTypeNM", False, DisplayFormat.Text)
        myDisplayTableHeader.AddHeaderItem("CompanyNM", "CompanyNM")
        myDisplayTableHeader.AddHeaderItem("UserCount", "UserCount", True, DisplayFormat.Number)
        myDisplayTableHeader.AddHeaderItem("SurveyCount", "SurveyCount", True, DisplayFormat.Number)
        myDisplayTableHeader.AddHeaderItem("SurveyResponseCount", "SurveyResponseCount", True, DisplayFormat.Number)
        dtList.BuildTableFromGrid(myDisplayTableHeader, myGrid)

    End Sub

End Class
