Imports CODataCon.com.controlorigins.ws
Imports CODataCon

Public Class Co_Apps_SurveyAdmin_Company
    Inherits SurveyUserControlBase
    Public myCompany As CompanyItem
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        hfCompanyID.Value = AppUtility.GetDBInteger(GetPageArgument("companyid").Second, 0)
        If Not IsPostBack Then
            If AppUtility.GetDBInteger(hfCompanyID.Value) > 0 Then
                myCompany = myCon.GetCompanyByCompanyID(AppUtility.GetDBInteger(hfCompanyID.Value))
                With myCompany
                    tbCompanyCD.Text = .CompanyCD
                    tbCompanyNM.Text = .CompanyNM
                    tbCompanyDS.Text = .CompanyDS
                    tbTitle.Text = .Title
                    tbTheme.Text = .SiteTheme
                    tbSiteURL.Text = .SiteURL
                    tbDefaultTheme.Text = .DefaultSiteTheme
                    tbGalleryFolder.Text = .GalleryFolder
                    tbAddress1.Text = .Address1
                    tbAddress2.Text = .Address2
                    tbCity.Text = .City
                    tbState.Text = .State
                    tbCountry.Text = .Country
                    tbPostalCode.Text = .PostalCode

                    dtProject.TableHeader = New DataGridVisualization.DisplayTableHeader
                    With dtProject.TableHeader
                        .TableTitle = "Company Projects"
                        .DetailDisplayName = "Project"
                        .DetailFieldName = "ApplicationNM"
                        .DetailKeyName = "ApplicationID"
                        .DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=applicationview&applicationid={0}"
                    End With
                    dtProject.BuildTable(dtProject.TableHeader,.ProjectList)
                    dtUser.TableHeader = New DataGridVisualization.DisplayTableHeader
                    With dtUser.TableHeader
                        .TableTitle = "Company Users"
                        .DetailDisplayName = "User"
                        .DetailFieldName = "DisplayName"
                        .DetailKeyName = "ApplicationUserID"
                        .DetailPath = "{0}"
                    End With
                    dtUser.BuildTable(dtUser.TableHeader,.UserList)


                End With
            End If
        End If
    End Sub
    Protected Sub cmd_SaveCompany_Click(sender As Object, e As EventArgs)
        If hfCompanyID.Value = "-1" Then
            myCompany = New CompanyItem With {.CompanyID = -1}
        Else
            myCompany = myCon.GetCompanyByCompanyID(AppUtility.GetDBInteger(hfCompanyID.Value))
        End If
        With myCompany
            .Active = True
            .CompanyCD = tbCompanyCD.Text
            .CompanyNM = tbCompanyNM.Text
            .CompanyDS = tbCompanyDS.Text
            .Title = tbTitle.Text
            .SiteTheme = tbTheme.Text
            .DefaultSiteTheme = tbDefaultTheme.Text
            .GalleryFolder = tbGalleryFolder.Text 
            .SiteURL = tbSiteURL.Text 
            .Address1 = tbAddress1.Text
            .Address2 = tbAddress2.Text
            .City = tbCity.Text
            .State = tbState.Text
            .Country = tbCountry.Text
            .PostalCode = tbPostalCode.Text
            .ModifiedDT = Now()
            .ModifiedID = UserInfo.ApplicationUserID
        End With
        myCon.PutCompany(myCompany)
        ReloadPage()
    End Sub
    Protected Sub cmd_CancelCompany_Click(sender As Object, e As EventArgs)
        ReloadPage()
    End Sub
    Protected Sub cmd_DeleteCompany_Click(sender As Object, e As EventArgs)
        myCompany = myCon.GetCompanyByCompanyID(AppUtility.GetDBInteger(hfCompanyID.Value))
        myCon.DeleteCompany(myCompany)
        ReloadPage()
    End Sub
    Protected Sub ReloadPage()
        ClearPageArguments( GetPageArgument("pid").Second.ToString)
        SetPageArgument("companyid", 0)
        SetPageArgument("action", "companyview")
        LoadPage()
    End Sub


End Class
