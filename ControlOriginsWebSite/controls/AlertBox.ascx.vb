
Partial Class controls_AlertBox
    Inherits System.Web.UI.UserControl

    Property boldnote As String
    Property message As String

    Property alertType As AlertBoxType

    Property dismissable As Boolean


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If dismissable = True Then
            alertbox.Attributes.Remove("class")
            alertbox.Attributes.Add("class", "alert alert-dismissable " & GetAlertCSSType())
            closebutton.Visible = True
        Else
            alertbox.Attributes.Remove("class")
            alertbox.Attributes.Add("class", "alert " & GetAlertCSSType())
            closebutton.Visible = False
        End If

    End Sub


    Protected Function GetAlertCSSType() As String

        Select Case alertType
            Case AlertBoxType._Default
                Return "alert-default"
            Case AlertBoxType.danger
                Return "alert-danger"
            Case AlertBoxType.info
                Return "alert-info"
            Case AlertBoxType.primary
                Return "alert-primary"
            Case AlertBoxType.success
                Return "alert-success"
            Case AlertBoxType.warning
                Return "alert-warning"
            Case Else
                Return "alert-default"
        End Select
    End Function

End Class
