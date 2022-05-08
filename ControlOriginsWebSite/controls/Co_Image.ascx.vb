
Partial Class controls_Co_Image
    Inherits ApplicationControlBase
    Property AppPropID As String
    Property CssClass As String = "img-responsive"
    Property CssStyle As String = String.Empty
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(AppPropID) Then
            Throw New Exception("You must set an apppropid to store the html in.")
        End If
        If Not IsPostBack Then
            UpdateDropDown()
        End If

        If IsAdmin Then
            cmd_EditHtml.Visible = True
        Else
            cmd_EditHtml.Visible = False
        End If
    End Sub

    Protected Sub cmd_EditHtml_Click(sender As Object, e As EventArgs)
        pnEdit.Visible = True
        pnView.Visible = False
        pnlAddImage.Visible = False
    End Sub
    Protected Sub cmd_SaveHtml_Click(sender As Object, e As EventArgs)
        pnEdit.Visible = False
        pnView.Visible = True
    End Sub

    Protected Sub cmd_AddImage_Click(sender As Object, e As EventArgs)
        pnlAddImage.Visible = True
        pnEdit.Visible = False
    End Sub

    Protected Sub cmd_UploadImage_Click(sender As Object, e As EventArgs)
        If fuGetImage.HasFile Then
            fuGetImage.SaveAs(Server.MapPath("/images/client/") & fuGetImage.FileName)
            UpdateDropDown()
        End If
        pnlAddImage.Visible = False
        pnEdit.Visible = True
    End Sub

    Public Sub UpdateDropDown()
        Dim myDir As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(Server.MapPath("/images/client"))
        Dim myFiles = myDir.GetFiles()
        ddlImageList.DataSource = myFiles
        ddlImageList.DataTextField = "Name"
        ddlImageList.DataValueField = "FullName"
        ddlImageList.DataBind()
    End Sub

    Protected Sub cmd_SetImage_Click(sender As Object, e As EventArgs)
        SetProperty(AppPropID, ddlImageList.SelectedItem.Text)

        pnEdit.Visible = False
        pnView.Visible = True
    End Sub

    Protected Sub cmd_Cancel_Click(sender As Object, e As EventArgs)

    End Sub
End Class
