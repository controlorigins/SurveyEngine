Imports CODataCon
Imports CODataCon.com.controlorigins.ws
Imports DataGridVisualization.ControlOriginsWS

Public Class Co_Apps_SurveyAdmin_UserAdmin
    Inherits SurveyUserControlBase
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim UserID As Integer = AppUtility.GetDBInteger(GetPageArgument("applicationuserid").Second, 0)

        dtList.Visible = False
        If  UserID= 0 Then
            cmd_GetUsers_Click(Nothing, New EventArgs)
        Else
            myControl = DirectCast(Page.LoadControl("~/Co_Apps/SurveyAdmin/Controls/ApplicationUserItem.ascx"), SurveyUserControlBase)
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


    Protected Sub cmd_GetUsers_Click(sender As Object, e As EventArgs)
        pnlEdit.Controls.Clear()
        dtList.Visible = True
        Dim myGrid As New List(Of Object)
        Dim myCon As New CODataCon.DataControler()

        myGrid.AddRange(myCon.GetApplicationUserList())
        Dim myDisplayTableHeader As New DataGridVisualization.DisplayTableHeader
        myDisplayTableHeader.TableTitle = "User List (<a href='/CO_Apps/SurveyAdmin/navigator.aspx?action=applicationuserview&applicationuserid=-1'>New User</a>)"
        myDisplayTableHeader.AddLinkHeaderItem("eMailAddress",
                                           "eMailAddress",
                                           "/CO_Apps/SurveyAdmin/navigator.aspx?action=applicationuserview&applicationuserid={0}",
                                           "ApplicationUserID",
                                           "eMailAddress")
        myDisplayTableHeader.AddHeaderItem("FirstNM", "FirstNM")
        myDisplayTableHeader.AddHeaderItem("LastNM", "LastNM")
        myDisplayTableHeader.AddHeaderItem("CompanyNM", "CompanyNM")
        myDisplayTableHeader.AddHeaderItem("SupervisorAccountNM", "SupervisorAccountNM")
        myDisplayTableHeader.AddHeaderItem("Applicatons", "ApplicatonUserRoleCount")
        myDisplayTableHeader.AddHeaderItem("SurveyResponses", "SurveyResponseCount")
        dtList.BuildTable(myDisplayTableHeader, myGrid)
    End Sub

End Class
