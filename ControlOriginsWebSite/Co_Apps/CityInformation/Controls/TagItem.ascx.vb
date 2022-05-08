Imports CityTagManager.WSCityInformation
Imports ControlOrigins.COUtility

Public Class Co_Apps_CityInformation_Tag
    Inherits ApplicationControlBase
    Public myTag As TagItem
    Public myCon As New CityTagManager.DataController()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        hfTagID.Value = Utility.GetDBInteger(GetPageArgument("tagid").Second, 0)
        If Not IsPostBack Then
            If Utility.GetDBInteger(hfTagID.Value) > 0 Then
                myTag = myCon.GetTagByTagID(Utility.GetDBInteger(hfTagID.Value))
                With myTag
                    tbTagNM.Text = .Name
                    ddlTagType.SelectedValue = .TagTypeID
                End With
            End If
        End If
    End Sub
    Protected Sub cmd_SaveTag_Click(sender As Object, e As EventArgs)
        If hfTagID.Value = "-1" Then
            myTag = New TagItem With {.Id = -1}
        Else
            myTag = myCon.GetTagByTagID(Utility.GetDBInteger(hfTagID.Value))
        End If
        With myTag
            .Name    = tbTagNM.Text
            .TagTypeID = ddlTagType.SelectedValue
        End With
        myCon.PutTag(myTag)
        ReloadPage()
    End Sub
    Protected Sub cmd_CancelTag_Click(sender As Object, e As EventArgs)
        ReloadPage()
    End Sub
    Protected Sub cmd_DeleteTag_Click(sender As Object, e As EventArgs)
        myTag = myCon.GetTagByTagID(Utility.GetDBInteger(hfTagID.Value))
        myCon.DeleteTag(myTag)
        ReloadPage()
    End Sub
    Protected Sub ReloadPage()
        ClearPageArguments()
        SetPageArgument("Id", 0)
        SetPageArgument("action", "tagview")
        LoadPage()
    End Sub

End Class
