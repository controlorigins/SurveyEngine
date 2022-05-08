Imports CODataCon.com.controlorigins.ws
Imports CODataCon

Public Class Co_Apps_SurveyAdmin_Controls_ProjectItem
    Inherits SurveyUserControlBase
    Public myApplication As ApplicationItem
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim ApplicationID = GetPageArgument("projectid").Second
        If ApplicationID > 0 Then
            myApplication = myCon.GetApplicationByApplicationID(ApplicationID)
        End If
        If Not IsPostBack Then

            Select Case GetPageArgument("action").Second
                Case "projectview"
                    If ApplicationID > 0 Then
                        ' myApplication = myCon.GetApplicationByApplicationID(ApplicationID)
                        With myApplication
                            tbApplicationNM.Text = .ApplicationNM
                            hfApplicationID.Value = .ApplicationID
                            tbApplicationDS.Text = .ApplicationDS
                            tbApplicationShortNM.Text = .ApplicationShortNM
                            tbMenuOrder.Text = .MenuOrder
                            tbApplicationCD.Text = .ApplicationCD
                            ddlApplicationType.SelectedValue = .ApplicationTypeID
                            If .CompanyID > 0 Then
                                ddlCompany.SelectedValue = .CompanyID
                            End If
                            Dim mySurveyList As New List(Of Object)
                            Dim myDataSetList As New List(Of Object)
                            For Each mySurvey In .ApplicationSurveyList
                                mySurveyList.Add(mySurvey.Survey)
                                For Each mySR In mySurvey.SurveyResponseList
                                    myDataSetList.Add(mySR)
                                Next
                            Next

                            dtSurveyResponse.TableHeader = GetSurveyResponseTableHeader(ApplicationID)
                            dtSurveyResponse.BuildTable(dtSurveyResponse.TableHeader, myDataSetList)

                            dtSurvey.TableHeader = GetApplicationSurveyTableHeader(ApplicationID)
                            dtSurvey.BuildTable(dtSurvey.TableHeader, .ApplicationSurveyList)

                            dtUserList.TableHeader = GetApplicationUserRoleTableHeader(ApplicationID)
                            dtUserList.BuildTable(dtUserList.TableHeader, .ApplicationUserList)


                            dtNavigation.TableHeader.TableTitle = "Navigation"
                            dtNavigation.TableHeader.DetailFieldName = "MenuText"
                            dtNavigation.TableHeader.DetailKeyName = "Id"
                            dtNavigation.TableHeader.DetailPath = "/Co_Apps/SurveyAdmin/navigator.aspx?action=projectview&subaction=navigationview&navigatonid={0}"
                            dtNavigation.TableHeader.AddHeaderItem("GlyphName", "GlyphName")
                            dtNavigation.TableHeader.AddHeaderItem("TartgetPage", "TartgetPage")
                            dtNavigation.TableHeader.AddHeaderItem("ViewInMenu", "ViewInMenu")
                            dtNavigation.TableHeader.AddHeaderItem("SiteRoleID", "SiteRoleID")
                            dtNavigation.BuildTable(dtNavigation.TableHeader, .Navigation)

                            dtProperties.TableHeader.TableTitle = "Properties"
                            dtProperties.TableHeader.DetailFieldName = "Key"
                            dtProperties.TableHeader.DetailKeyName = "Id"
                            dtProperties.TableHeader.DetailPath = "/Co_Apps/SurveyAdmin/navigator.aspx?action=projectview&subaction=propertyview&propertyid={0}"
                            dtProperties.TableHeader.AddHeaderItem("Value", "Value")

                            dtProperties.BuildTable(dtProperties.TableHeader, .Properties)

                        End With
                        Select Case GetPageArgument("subaction").Second
                            Case "addsurveyresponse"
                                pnlSurveyResponse.Visible = True
                                ddlApplicationSurvey.Items.Clear()
                                For Each mySurvey In myApplication.ApplicationSurveyList
                                    ddlApplicationSurvey.Items.Add(New ListItem With {.Value = mySurvey.Survey.SurveyID, .Text = mySurvey.Survey.SurveyNM})
                                Next
                                ddlApplicationUser.Items.Clear()
                                ddlApplicationUser.Items.Add(New ListItem With {.Value = String.Empty, .Text = "All Users"})
                                For Each myUser In myApplication.ApplicationUserList
                                    ddlApplicationUser.Items.Add(New ListItem With {.Value = myUser.ApplicationUserID, .Text = myUser.AccountNM})
                                Next
                            Case "addsurvey"
                                pnlApplicationSurvey.Visible = True
                                Dim curSurvey As New ApplicationSurveyItem
                                curSurvey = (From i In myApplication.ApplicationSurveyList Where i.Survey.SurveyID = GetPageArgument("surveyid").Second).SingleOrDefault

                                If IsNothing(curSurvey) Then
                                    ddlSurvey.Items.Clear()
                                    For Each mySurvey In myCon.GetSurveyLookupList()
                                        If IsNothing((From i In myApplication.ApplicationSurveyList Where i.Survey.SurveyID = mySurvey.Value).SingleOrDefault) Then
                                            ddlSurvey.Items.Add(New ListItem With {.Value = mySurvey.Value, .Text = mySurvey.Name})
                                        End If
                                    Next
                                Else

                                    ddlDefaultRole.SelectedValue = curSurvey.DefaultRoleID
                                    ddlSurvey.Items.Clear()
                                    ddlSurvey.Items.Add(New ListItem With {.Text = curSurvey.Survey.SurveyNM, .Value = curSurvey.Survey.SurveyID, .Selected = True})
                                    ddlSurvey.Enabled = False
                                End If
                            Case "adduser"
                                pnlApplicationUser.Visible = True
                                ddlUser.Items.Clear()
                                ddlRole.Items.Clear()
                                For Each myItem In myCon.GetRoles()
                                    ddlRole.Items.Add(New ListItem With {.Value = myItem.RoleID, .Text = myItem.RoleCD})
                                Next
                                Dim myUserRole As New ApplicationUserRoleItem
                                myUserRole = (From i In myApplication.ApplicationUserList Where i.ApplicationUserID = GetPageArgument("applicationuserid").Second).SingleOrDefault

                                If Not IsNothing(myUserRole) Then
                                    ddlRole.SelectedValue = myUserRole.RoleID
                                End If
                                For Each myUser In myCon.GetApplicationUserList()
                                    If IsNothing((From i In myApplication.ApplicationUserList Where i.ApplicationUserID = myUser.ApplicationUserID).SingleOrDefault) Then
                                        If myUser.ApplicationUserID = GetPageArgument("applicationuserid").Second Then
                                            ddlUser.Items.Add(New ListItem With {.Value = myUser.ApplicationUserID, .Text = myUser.AccountNM, .Selected = True})
                                        Else
                                            ddlUser.Items.Add(New ListItem With {.Value = myUser.ApplicationUserID, .Text = myUser.AccountNM})
                                        End If
                                    Else
                                        If myUser.ApplicationUserID = GetPageArgument("applicationuserid").Second Then
                                            ddlUser.Items.Add(New ListItem With {.Value = myUser.ApplicationUserID, .Text = myUser.AccountNM, .Selected = True})
                                        End If
                                    End If
                                Next
                            Case "navigationview"
                                pnlApplicatonDetail.Visible = False

                            Case "propertyview"
                                pnlApplicatonDetail.Visible = False

                            Case Else
                                pnlApplicatonDetail.Visible = True
                        End Select
                    Else
                        myApplication = New ApplicationItem()
                        pnlApplicatonDetail.Visible = True
                        dtSurvey.Visible = False
                        dtSurveyResponse.Visible = False
                        dtUserList.Visible = False
                        hfApplicationID.Value = -1
                    End If
                Case Else
                    myApplication = New ApplicationItem
            End Select
        End If

    End Sub

    Protected Sub cmd_SaveApplication_Click(sender As Object, e As EventArgs)
        myApplication = myCon.GetApplicationByApplicationID(hfApplicationID.Value)
        With myApplication
            .ApplicationID = hfApplicationID.Value
            .ApplicationCD = tbApplicationCD.Text
            .ApplicationDS = tbApplicationDS.Text
            .ApplicationNM = tbApplicationNM.Text
            .ApplicationShortNM = tbApplicationShortNM.Text
            .ApplicationTypeID = ddlApplicationType.SelectedValue
            .CompanyID = ddlCompany.SelectedValue
            .MenuOrder = tbMenuOrder.Text
            If .ApplicationID = 1 Then
                .ApplicationFolder = "ControlOrigins"
            Else
                .ApplicationFolder = "SurveyApp"
            End If
            .ModifiedID = UserInfo.ApplicationUserID
            .ModifiedDT = Now()
        End With
        myApplication = myCon.UpdateApplication(myApplication)
        ClearPageArguments(GetPageArgument("pid").Second.ToString)
        If AppUtility.GetDBInteger(hfApplicationID.Value, 0) < 0 Then
            SetPageArgument("projectid", myApplication.ApplicationID)
        Else
            SetPageArgument("projectid", 0)
        End If
        SetPageArgument("action", "projectview")
        LoadPage()
    End Sub

    Protected Sub cmd_Cancel_Click(sender As Object, e As EventArgs)
        ClearPageArguments(GetPageArgument("pid").Second.ToString)
        SetPageArgument("projectid", 0)
        SetPageArgument("action", "projectview")
        LoadPage()
    End Sub

    Protected Sub cmd_Delete_Click(sender As Object, e As EventArgs)
        myApplication = myCon.GetApplicationByApplicationID(hfApplicationID.Value)
        If myApplication.ApplicationSurveyList.Count = 0 AndAlso myApplication.ApplicationUserList.Count = 0 Then
            myCon.DeleteApplication(myApplication)
            ClearPageArguments(GetPageArgument("pid").Second.ToString)
            SetPageArgument("projectid", 0)
            SetPageArgument("action", "projectview")
            LoadPage()
        Else
            ' Message - Can't delete project with users and surveys
        End If
    End Sub

    Protected Sub cmd_SaveUserAssignment_Click(sender As Object, e As EventArgs)
        Dim myAppUserAssignment As New ApplicationUserRoleItem
        Dim sReturn As String = String.Empty
        With myAppUserAssignment
            .ApplicationUserRoleID = -1
            .ApplicationID = hfApplicationID.Value
            .ApplicationUserID = ddlUser.SelectedValue
            .RoleID = ddlRole.SelectedValue
            .ModifiedID = 9999
        End With
        myCon.UpdateApplicationUserRole(myAppUserAssignment, sReturn)

        If sReturn = String.Empty Then
            ClearPageArguments(GetPageArgument("pid").Second.ToString)
            SetPageArgument("projectid", hfApplicationID.Value)
            SetPageArgument("action", "projectview")
            LoadPage()
        Else
            MsgBox(sReturn)
        End If

    End Sub

    Protected Sub cmd_CancelUserAssignment_Click(sender As Object, e As EventArgs)
        ClearPageArguments(GetPageArgument("pid").Second.ToString)
        SetPageArgument("projectid", hfApplicationID.Value)
        SetPageArgument("action", "projectview")
        LoadPage()
    End Sub

    Protected Sub cmd_DeleteUserAssignment_Click(sender As Object, e As EventArgs)

        Dim myAppUserAssignment As New ApplicationUserRoleItem
        Dim sReturn As String = String.Empty
        With myAppUserAssignment
            .ApplicationUserRoleID = -1
            .ApplicationID = hfApplicationID.Value
            .ApplicationUserID = ddlUser.SelectedValue
            .RoleID = ddlRole.SelectedValue
            .ModifiedID = 9999
        End With
        myCon.DeleteApplicationUserRole(myAppUserAssignment)
        ClearPageArguments()
        SetPageArgument("projectid", hfApplicationID.Value)
        SetPageArgument("action", "projectview")
        LoadPage()
    End Sub

    Protected Sub cmd_SaveSurveyAssignment_Click(sender As Object, e As EventArgs)

        Dim myApplicationSurvey As New ApplicationSurveyItem With {.ApplicationSurveyID = -1}
        Try
            For Each myAppSurvey As ApplicationSurveyItem In (From i In myApplication.ApplicationSurveyList Where i.Survey.SurveyID = ddlSurvey.SelectedValue).ToList()
                myApplicationSurvey.ApplicationSurveyID = myAppSurvey.ApplicationSurveyID
            Next
        Catch ex As Exception
            ControlOrigins.COUtility.ApplicationLogging.ErrorLog("ApplicationItem.cmd_SaveSurveyAssignment", ex.ToString)
        End Try

        With myApplicationSurvey
            .ApplicationID = hfApplicationID.Value
            .Survey = New SurveyItem With {.SurveyID = ddlSurvey.SelectedValue}
            .DefaultRoleID = ddlDefaultRole.SelectedValue
        End With

        myApplicationSurvey = myCon.UpateApplicationSurvey(myApplicationSurvey)

        ClearPageArguments()
        SetPageArgument("projectid", hfApplicationID.Value)
        SetPageArgument("action", "projectview")
        LoadPage()
    End Sub

    Protected Sub cmd_CancelSurveyAssignment_Click(sender As Object, e As EventArgs)
        ClearPageArguments()
        SetPageArgument("projectid", hfApplicationID.Value)
        SetPageArgument("action", "projectview")
        LoadPage()
    End Sub

    Protected Sub cmd_DeleteSurveyAssignment_Click(sender As Object, e As EventArgs)
        Dim myApplicationSurvey As New ApplicationSurveyItem

        For Each myAS As ApplicationSurveyItem In myApplication.ApplicationSurveyList
            If myAS.Survey.SurveyID = ddlSurvey.SelectedValue Then
                myApplicationSurvey.ApplicationSurveyID = myAS.ApplicationSurveyID
            End If
        Next

        With myApplicationSurvey
            .ApplicationID = hfApplicationID.Value
            .Survey = New SurveyItem With {.SurveyID = ddlSurvey.SelectedValue}
            .DefaultRoleID = ddlDefaultRole.SelectedValue
        End With
        myCon.DeleteApplicationSurvey(myApplicationSurvey)
        ClearPageArguments()
        SetPageArgument("projectid", hfApplicationID.Value)
        SetPageArgument("action", "projectview")
        LoadPage()
    End Sub

    Protected Sub cmd_SaveSurveyResponse_Click(sender As Object, e As EventArgs)

        Dim mySR As New SurveyResponseItem
        With mySR
            .ApplicationID = AppUtility.GetDBInteger(hfApplicationID.Value)
            .Survey = New SurveyItem With {.SurveyID = AppUtility.GetDBInteger(ddlApplicationSurvey.SelectedValue)}
            .SurveyResponseNM = tbSurveyResponseName.Text
            .SurveyResponseID = -1
            If ddlApplicationUser.SelectedValue <> String.Empty Then
                .AssignedUserID = AppUtility.GetDBInteger(ddlApplicationUser.SelectedValue)
            End If
            .ModifiedID = UserInfo.ApplicationUserID
            .ModifiedDT = Now()
            .DataSource = "SurveyAdmin.ApplicationItem"
        End With
        mySR = myCon.PutSurveyResponseItem(mySR)
        ClearPageArguments(GetPageArgument("pid").Second.ToString)
        SetPageArgument("projectid", hfApplicationID.Value)
        SetPageArgument("action", "projectview")
        LoadPage()
    End Sub

    Protected Sub cmd_CancelSurveyResponse_Click(sender As Object, e As EventArgs)

        ClearPageArguments(GetPageArgument("pid").Second.ToString)
        SetPageArgument("projectid", hfApplicationID.Value)
        SetPageArgument("action", "projectview")
        LoadPage()
    End Sub

    Protected Sub cmd_DeleteSurveyResponse_Click(sender As Object, e As EventArgs)

        ClearPageArguments(GetPageArgument("pid").Second.ToString)
        SetPageArgument("projectid", hfApplicationID.Value)
        SetPageArgument("action", "projectview")
        LoadPage()
    End Sub

    Protected Sub cmd_ResetSurveyResponse_Click(sender As Object, e As EventArgs)
        ClearPageArguments(GetPageArgument("pid").Second.ToString)
        SetPageArgument("projectid", hfApplicationID.Value)
        SetPageArgument("action", "projectview")
        LoadPage()
    End Sub

End Class
