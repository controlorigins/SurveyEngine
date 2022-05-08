Imports CODataCon

Public Class admin_UserControls_HTMLTextBox
    Inherits System.Web.UI.UserControl
    Implements IHTMLControl

    Public Function GetHTML() As String Implements IHTMLControl.GetHTML
        Return Request.Form("editor1")
    End Function

    Public Sub SetHTML(myHTML As String) Implements IHTMLControl.SetHTML
        litHTMLControl.Text = String.Format("<textarea name=""editor1"">{0}</textarea>", myHTML)
    End Sub
End Class
