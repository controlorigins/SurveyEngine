Imports CODataCon.com.controlorigins.ws

Partial Class Co_Apps_ControlOrigins_UserManagment
    Inherits ApplicationControlBase

    Property EditUser As New ApplicationuserItem

        Sub pagestartup() Handles Me.Load

        If Not IsPostBack Then
            PagingSystemControl.TotalItems = UserControler.GetAllUsers.Count
            PagingSystemControl.ItemsPerPage = 10
            PagingSystemControl.CurPage = 1
            UpdateData()
        End If

    End Sub



    Sub UpdateData()

        PagingSystemControl.UpdatePager()

        Dim mycoll = UserControler.GetPagedUsers(PagingSystemControl.TakeItems, PagingSystemControl.SkipItems)

        UserList.DataSource = mycoll
        UserList.DataBind()

    End Sub

    Protected Sub PagingSystemControl_PageSelected(SelectedPageID As Integer) Handles PagingSystemControl.PageSelected
        UpdateData()
    End Sub



    Protected Sub cmd_selectUser_Click(sender As Object, e As EventArgs)

        PNUserList.Visible = False
        EditUser = GetUserByID(CType(sender, LinkButton).Attributes("data-UserID"))
        TempUserID.Value = EditUser.ApplicationUserID
        tbDisplayName.Text = EditUser.DisplayName
        tbEmailAddress.Text = EditUser.EmailAddress
        tbfirstName.Text = EditUser.FirstNM
        tblastName.Text = EditUser.LastNM
        tbassaccount.Text = EditUser.AccountNM
        
        ' user app list 

        UsersApps.DataSource = EditUser.ApplicationUserRoleList
        UsersApps.DataBind()

        PNUserEdit.Visible = True

    End Sub

    Protected Sub cmd_Cancel_Click()

        PNUserEdit.Visible = False
        tbDisplayName.Text = ""
        tbEmailAddress.Text = ""
        tbfirstName.Text = ""
        tblastName.Text = ""
        tbassaccount.Text = ""
        PNUserList.Visible = True

    End Sub

    Protected Sub cmd_update_Click(sender As Object, e As EventArgs)

        EditUser = GetUserByID(CInt(TempUserID.Value))
        EditUser.DisplayName = tbDisplayName.Text
        EditUser.EmailAddress = tbEmailAddress.Text
        EditUser.FirstNM = tbfirstName.Text
        EditUser.LastNM = tblastName.Text
        EditUser.AccountNM = tbassaccount.Text
        UpdateUser(EditUser)
        UserList.DataBind()
        cmd_Cancel_Click()

    End Sub


End Class
