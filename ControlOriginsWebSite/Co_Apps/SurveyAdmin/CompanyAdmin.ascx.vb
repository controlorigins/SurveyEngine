Imports CODataCon
Imports CODataCon.com.controlorigins.ws
Imports DataGridVisualization.ControlOriginsWS

Public Class Co_Apps_SurveyAdmin_CompanyAdmin
    Inherits SurveyUserControlBase
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim UserID As Integer = AppUtility.GetDBInteger(GetPageArgument("companyid").Second, 0)
        dtList.Visible = False
        If  UserID= 0 Then
            cmd_GetList_Click(Nothing, New EventArgs)
        Else
            myControl = DirectCast(Page.LoadControl("~/Co_Apps/SurveyAdmin/Controls/CompanyItem.ascx"), SurveyUserControlBase)
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
        pnlEdit.Controls.Clear()
        dtList.Visible = True

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

End Class
