Imports System.IO
Imports CODataCon.com.controlorigins.ws
Imports ControlOrigins.COUtility

Partial Class Co_Apps_ControlOrigins_AppManager
    Inherits ApplicationControlBase

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            GetCurrentApps()
            GetAppFolders()
            GetRoles()
            GetApplicationtypes()
            GetSiteStyles()
        End If
    End Sub

#Region "Form Data Support"
    Protected Sub GetCurrentApps()
        AppListing.DataSource = AppControler.GetAllApps
        AppListing.DataTextField = "ApplicationNM"
        AppListing.DataValueField = "ApplicationID"
        AppListing.DataBind()
    End Sub
    Protected Sub GetRoles()
        ddroles.DataSource = UserControler.GetRoles
        ddroles.DataTextField = "RoleName"
        ddroles.DataValueField = "Id"
        ddroles.DataBind()
    End Sub
    Protected Sub GetApplicationtypes()
        ddlApplicationType.DataSource = AppControler.GetApplicationTypes
        ddlApplicationType.DataTextField = "name"
        ddlApplicationType.DataValueField = "value"
        ddlApplicationType.DataBind()
    End Sub
    Protected Sub GetSiteStyles()
        CSSDropdown.DataSource = GetAllThemes(Server.MapPath("/css/"))
        CSSDropdown.DataBind()
    End Sub
    Protected Sub GetAppFolders()
        Dim mydir As DirectoryInfo = New DirectoryInfo(Server.MapPath("/Co_Apps"))
        Dim myappdirlist = mydir.GetDirectories
        ddAppFolder.DataSource = (From i In myappdirlist Select i.Name).ToList
        ddAppFolder.DataBind()
    End Sub
    Protected Sub GetAppControls(ByVal sFolderName As String)
        Dim ControlPath As String = Server.MapPath("/co_Apps/" & sFolderName)

        If FileProcessing.IsValidFolder(ControlPath) Then
            ddAppFiles.Items.Clear()
            Dim myappdir As DirectoryInfo = New DirectoryInfo(ControlPath)
            ddAppFiles.DataSource = myappdir.GetFiles("*.ascx")
            ddAppFiles.DataTextField = "Name"
            ddAppFiles.DataValueField = "Name"
            ddAppFiles.DataBind()
        End If
    End Sub
#End Region



#Region "Application Menu "

    Protected Sub cmd_cancelAddPage_Click(sender As Object, e As EventArgs)
        addnewpagePanel.Visible = False

        SetAddDefinition(True)
        Dim myindex = lbAppDefinitionList.SelectedIndex
        Select Case myindex
            Case -1
                DisableAddpageEdites()
            Case Else
                EnableApppageEdites()
        End Select

    End Sub

    Protected Sub cmd_addPageDialog_Click(sender As Object, e As EventArgs)
        addnewpagePanel.Visible = True
        cmd_UpdatePage.Visible = False
        cmd_savePage.Visible = True
        SetAddDefinition(False)
        DisableAddpageEdites()
    End Sub

    Protected Sub cmd_savePage_Click(sender As Object, e As EventArgs)
        Dim NewPageDef As New NavigationMenuItem
        With NewPageDef
            .MenuText = tbMenuText.Text
            .SiteAppID = AppID.Value
            .TartgetPage = ddAppFiles.SelectedValue
            .SiteRoleID = ddroles.SelectedValue
            .GlyphName = tbGlyphName.Text
            .MenuOrder = 1
            .ViewInMenu = cbviewinmenu.Checked
        End With
        AppControler.SaveAppPageDefinition(NewPageDef)
        updatepagediflist()

        ' clear form inputs
        addnewpagePanel.Visible = False

        SetAddDefinition(True)
    End Sub

    Protected Sub updatepagediflist()
        lbAppDefinitionList.DataSource = GetAppByID(AppID.Value).Navigation.ToList()
        lbAppDefinitionList.DataTextField = "MenuText"
        lbAppDefinitionList.DataValueField = "Id"
        lbAppDefinitionList.DataBind()
    End Sub

#End Region

    Protected Sub cmd_SetDefault_Click(sender As Object, e As EventArgs)
        ' set page as the default page for application startup 
        Dim thisapp = GetAppByID(AppID.Value)
        SetDefaultNavigationItem(thisapp, lbAppDefinitionList.SelectedValue)
    End Sub

    Protected Sub cmd_SaveAppBasic_Click(sender As Object, e As EventArgs)

        Dim myApp As New ApplicationItem
        If CType(sender, Button).Text = "Next" Then
            ' we have a new application 
            ' persist basic application informion  
            With myApp
                .ApplicationID = -1
                .ApplicationCD = tbAppName.Text.Trim
                .ApplicationShortNM = tbAppName.Text.Trim
                .ApplicationNM = tbAppName.Text.Trim
                .ApplicationDS = TbAppDescription.Text.Trim
                .ApplicationTypeID = ddlApplicationType.SelectedValue
                .ApplicationTypeNM = ddlApplicationType.SelectedItem.Text.Trim
                .MenuOrder = 1
                .ModifiedDT = Now
                .ModifiedID = UserInfo.ApplicationUserID
                .UserCount = 0
                .SurveyResponseCount = 0
                .SurveyCount = 0
                .ApplicationFolder = ddAppFolder.SelectedValue
                .DefaultAppPage = -1
            End With
            myApp = AppControler.PutOnlineSiteApp(myApp)
            AppID.Value = myApp.ApplicationID
        Else
            ' we are updating an application 
            myApp = GetAppByID(AppID.Value)
            With myApp
                .ApplicationNM = tbAppName.Text.Trim
                .ApplicationDS = TbAppDescription.Text.Trim
                .ApplicationFolder = ddAppFolder.SelectedValue
            End With
            myApp = AppControler.PutOnlineSiteApp(myApp)
        End If
        AppControler.SetProperty(AppID.Value, "ApplicationTheme", CSSDropdown.SelectedValue)

        GetAppControls(myApp.ApplicationFolder)

        ' update console 

        AppName.Text = tbAppName.Text.Trim
        AppPath.Text = TbAppDescription.Text.Trim

        SetAddDefinition(True)

    End Sub



    Protected Sub lbAppDefinitionList_SelectedIndexChanged(sender As Object, e As EventArgs)

        ' app page definition selection
        Dim myindex = lbAppDefinitionList.SelectedIndex
        Select Case myindex

            Case -1

                DisableAddpageEdites()

            Case Else

                EnableApppageEdites()



        End Select

    End Sub

    Protected Sub AppListing_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim myindex = AppListing.SelectedIndex
        Select Case myindex
            Case -1
                ' nothing selected do nothing 
            Case 0
                ' selecet one selected do nothing
            Case 1
                ' we need a new application
                basicsettingspanel.Visible = True
                AppListing.Visible = False
            Case Is > 1
                ' we have an excisting app selected
                AppID.Value = AppListing.SelectedValue
                Dim myapp = AppControler.GetAppByID(AppListing.SelectedValue)


                ' update console 
                AppName.Text = myapp.ApplicationNM
                AppPath.Text = myapp.ApplicationDS
                hfDefaultRoleID.Value = 1
                ddlApplicationType.SelectedValue = myapp.ApplicationTypeID

                DefinitionList.Visible = True
                updatepagediflist()
                SetAddDefinition(True)
                AppListing.Visible = False


                ' setup basic settings for update
                basicsettingspanel.Visible = True
                tbAppName.Text = myapp.ApplicationNM
                TbAppDescription.Text = myapp.ApplicationDS

                GetAppControls(myapp.ApplicationFolder)
                ddAppFolder.SelectedValue = myapp.ApplicationFolder

                cmd_SaveAppBasic.Visible = True
                cmd_SaveAppBasic.Text = "Update"
                cmd_cancelAppBasic.Visible = False

                Try
                    CSSDropdown.SelectedValue = (From i In myapp.Properties Where i.Key = "ApplicationTheme" Select i.Value ).SingleOrDefault
                Catch ex As Exception

                End Try

                ' User list and available user list 
                GetAppUserData(myapp.ApplicationID)
            Case Else
        End Select

    End Sub

    Private Sub GetAppUserData(myapp As Integer)

        curappid.Value = myapp

        Dim mycoll = GetAllUsers()
        Dim appusers = GetUsersbyAppID(myapp)
        For Each user In appusers
            Dim thisuser = (From i In mycoll Where i.ApplicationUserID = user.ApplicationUserID).Single
            mycoll.Remove(thisuser)
        Next

        availableUserList.DataSource = mycoll
        availableUserList.DataBind()

        UserList.DataSource = appusers
        UserList.DataBind()
        UserinformationBlock.Visible = True

    End Sub

    Protected Sub cmd_cancelAppBasic_Click(sender As Object, e As EventArgs)
        basicsettingspanel.Visible = False
        AppListing.Visible = True
        AppListing.SelectedIndex = -1
    End Sub

    Protected Sub cmd_DeleteDef_Click(sender As Object, e As EventArgs)
        ' delete selected page def
        Dim myNavMenuItem = AppControler.GetNavigationMenuItem(GetAppByID(AppID.Value), CInt(lbAppDefinitionList.SelectedValue))
        If AppControler.DeleteAppPageDefinition(myNavMenuItem) = True Then
            ' Update list of app page definitions
            updatepagediflist()
            lbAppDefinitionList.SelectedIndex = -1
            DisableAddpageEdites()
        Else
            ' Give error notice to use
        End If
    End Sub

    Protected Sub cmd_EditDef_Click(sender As Object, e As EventArgs) Handles cmd_EditDef.Click
        ' edit selected page def
        Dim mypage = AppControler.GetNavigationMenuItem(GetAppByID(AppID.Value), lbAppDefinitionList.SelectedItem.Value)
        Try
            ddAppFiles.SelectedValue = mypage.TartgetPage
        Catch
        End Try
        tbMenuText.Text = mypage.MenuText
        tbGlyphName.Text = mypage.GlyphName
        ddroles.SelectedValue = mypage.SiteRoleID
        cbviewinmenu.Checked = mypage.ViewInMenu

        ' hide save button
        cmd_savePage.Visible = False
        ' show update button
        cmd_UpdatePage.Visible = True
        ' make a delete button

        addnewpagePanel.Visible = True
        SetAddDefinition(False)
        DisableAddpageEdites()



    End Sub

    Protected Sub MoveItem()
        ' move item by selected options
        addnewpagePanel.Visible = True
        cmd_UpdatePage.Visible = False
        cmd_savePage.Visible = True
        SetAddDefinition(False)
        DisableAddpageEdites()
    End Sub

    Protected Sub EnableApppageEdites()
        cmd_EditDef.CssClass = "btn btn-info btn-sm"
        cmd_DeleteDef.CssClass = "btn btn-danger btn-sm"
        cmd_SetDefault.CssClass = "btn btn-danger btn-sm"
        cmd_MoveDef.CssClass = "btn btn-warning btn-sm dropdown-toggle "

    End Sub

    Protected Sub DisableAddpageEdites()
        cmd_EditDef.CssClass = "btn btn-info btn-sm disabled"
        cmd_DeleteDef.CssClass = "btn btn-danger btn-sm disabled"
        cmd_SetDefault.CssClass = "btn btn-danger btn-sm disabled"
        cmd_MoveDef.CssClass = "btn btn-warning btn-sm dropdown-toggle disabled "
    End Sub



    Protected Sub SetAddDefinition(Enabled As Boolean)
        If Enabled = True Then
            cmd_addPageDialog.CssClass = "btn btn-success btn-sm "
        Else
            cmd_addPageDialog.CssClass = "btn btn-success btn-sm disabled"
        End If
    End Sub

    Protected Sub cmd_UpdatePage_Click(sender As Object, e As EventArgs) Handles cmd_UpdatePage.Click
        Dim mypage = AppControler.GetNavigationMenuItem(GetAppByID(AppID.Value), lbAppDefinitionList.SelectedItem.Value)
        mypage.TartgetPage = ddAppFiles.SelectedValue
        mypage.MenuText = tbMenuText.Text
        mypage.GlyphName = tbGlyphName.Text
        mypage.SiteRoleID = ddroles.SelectedValue
        mypage.ViewInMenu = cbviewinmenu.Checked

        mypage = AppControler.SaveAppPageDefinition(mypage)

        ' Update item to be edited .
        addnewpagePanel.Visible = False
        SetAddDefinition(True)
        EnableApppageEdites()



    End Sub

    Protected Sub cmd_removeUser_Click(sender As Object, e As EventArgs)

        Dim userID = CType(sender, LinkButton).Attributes("data-userid")
        AppControler.RemoveUserFromApp(userID, AppListing.SelectedValue)
        GetAppUserData(AppListing.SelectedValue)
    End Sub

    Protected Sub cmd_AddUser_Click(sender As Object, e As EventArgs)
        Dim myApplicationUserRole As New ApplicationUserRoleItem With {.ApplicationUserID = CType(sender, LinkButton).Attributes("data-userid"),
                                                                       .ApplicationID = AppListing.SelectedValue,
                                                                       .ModifiedID = UserInfo.ApplicationUserID,
                                                                       .ApplicationUserRoleID = -1,
                                                                       .StartupDate = Now(),
                                                                       .RoleID = cint(hfDefaultRoleID.Value),                                                                       
                                                                       .UserInroled = False,
                                                                       .isMonthlyPrice =False,
                                                                       .IsDemo = True,
                                                                       .Price = 0}
        Dim userid = CType(sender, LinkButton).Attributes("data-userid")
        AppControler.UpdateApplicationUserRole(myApplicationUserRole)
        GetAppUserData(AppListing.SelectedValue)
    End Sub

    Protected Sub cmd_MovetoTop_Click(sender As Object, e As EventArgs)
        Dim mybutton = CType(sender, LinkButton)
        Dim mycoll =  AppControler.GetAppNavigation(AppID.Value)
        Dim a As Integer = 10

        For Each i In mycoll
            i.MenuOrder = a
            a += 10
        Next

        Dim myitem = (From i In mycoll Where i.Id = lbAppDefinitionList.SelectedValue).Single

        Select Case mybutton.CommandName
            Case "move2Top"
                myitem.MenuOrder = 1
            Case "moveUP"
                myitem.MenuOrder = myitem.MenuOrder - 15
            Case "moveDown"
                myitem.MenuOrder = myitem.MenuOrder + 15
            Case "move2Bottom"
                myitem.MenuOrder = a + 10
        End Select
        myitem = AppControler.SaveAppPageDefinition(myitem)
        updatepagediflist()
        lbAppDefinitionList.SelectedValue = myitem.Id
    End Sub

    Protected Sub cmd_AdminChange_Click(sender As Object, e As EventArgs)
        ' toggle and Update Admin 
        Dim mybutton = CType(sender, LinkButton)
        Dim thisuser = GetUserapprelationbyuserandapp(mybutton.Attributes("data-userid"), curappid.Value)
        If thisuser.IsUserAdmin = True Then
            thisuser.IsUserAdmin = False
        Else
            thisuser.IsUserAdmin = True
        End If
        AppControler.UpdateSiteAppUser(thisuser)

        GetAppUserData(curappid.Value)
    End Sub


End Class