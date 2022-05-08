Imports CODataCon.com.controlorigins.ws

Partial Class AppClone
    Inherits System.Web.UI.UserControl
    Dim mycon As New CODataCon.DataControler()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim mycoll = (From i In mycon.GetApplicationList() Order By i.ApplicationNM).ToList
            mycoll.Insert(0, New ApplicationItem With {.ApplicationNM = "Select One", .ApplicationID = -1})
            curApps.DataSource = mycoll
            curApps.DataTextField = "ApplicationNM"
            curApps.DataValueField = "ApplicationID"
            curApps.DataBind()
        End If
    End Sub

    Protected Sub curApps_SelectedIndexChanged(sender As Object, e As EventArgs) Handles curApps.SelectedIndexChanged

        If curApps.SelectedIndex > 0 Then
            Dim fromapp = mycon.GetApplicationByApplicationID(CInt(curApps.SelectedValue))
            CurAppName.Text = fromapp.ApplicationNM
            CurAppPath.Text = fromapp.ApplicationFolder
            Dim appfolderDir As IO.DirectoryInfo = New IO.DirectoryInfo(MapPath("App_Code/" & fromapp.ApplicationFolder))
            Dim AppCodeFolderDir As IO.DirectoryInfo = New IO.DirectoryInfo(MapPath("Co_Apps/" & fromapp.ApplicationFolder))
            AppFolder.Checked = appfolderDir.Exists
            AppCodeFolder.Checked = AppCodeFolderDir.Exists
            NewAppName.Text = "Clone of : " & fromapp.ApplicationNM
        Else
            CurAppName.Text = ""
            CurAppPath.Text = ""
            AppFolder.Checked = False
            AppCodeFolder.Checked = False
        End If
    End Sub

    Protected Sub cmd_cloneapp_Click(sender As Object, e As EventArgs) Handles cmd_cloneapp.Click
        If curApps.SelectedIndex > 0 Then
            mycon.CloneSiteApp(CInt(curApps.SelectedValue), NewAppName.Text.Trim)
        End If
    End Sub

End Class
