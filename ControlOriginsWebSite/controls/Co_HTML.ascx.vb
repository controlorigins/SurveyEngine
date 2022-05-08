Partial Class controls_Co_HTML
    Inherits ApplicationControlBase
    Property AppPropID As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(AppPropID) Then
            Throw New Exception("You must set an apppropid to store the html in.")
        End If
        If IsAdmin Then
            cmd_EditHtml.Visible = True
        Else
            cmd_EditHtml.Visible = False
        End If
    End Sub

    Protected Sub cmd_EditHtml_Click(sender As Object, e As EventArgs)
        HtmlEditor1.Text = GetProperty(AppPropID)
        pnEdit.Visible = True
        pnView.Visible = False
    End Sub
    Protected Sub cmd_SaveHtml_Click(sender As Object, e As EventArgs)
        SetProperty(AppPropID, HtmlEditor1.Text)
        pnEdit.Visible = False
        pnView.Visible = True
    End Sub
End Class
