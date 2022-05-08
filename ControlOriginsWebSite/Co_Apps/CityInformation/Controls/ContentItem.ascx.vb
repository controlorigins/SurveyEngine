Imports CityTagManager.WSCityInformation
Imports ControlOrigins.COUtility
Partial Class Co_Apps_CityInformation_Controls_ContentItem
    Inherits ApplicationControlBase
    Public myContentItem As New ContentItem
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        hfId.Value = Utility.GetDBInteger(GetPageArgument("contentid").Second, 0)
        If Not IsPostBack Then
            Using myDb As New CityTagManager.DataController()
                If Utility.GetDBInteger(hfId.Value) > 0 Then
                    myContentItem = myDb.GetContentById(Utility.GetDBInteger(hfId.Value))

                    With myContentItem
                        tbTitle.Text = .Title
                        tbDescription.Text = .Description
                        cbActive.Checked = .Active
                        tbTitle.Text = .Description
                        tbIconURL.Text = .IconURL
                        tbImageURL.Text = .ImageURL
                        tbLinkURL.Text = .LinkURL
                        tbLinkAnchorText.Text = .LinkAnchorText
                        tbReadMoreText.Text = .ReadMoreText

                        For Each tag In .TagList
                            If tag.TagTypeID = 0 Then
                                ddlKeywordTag.SelectedValue = tag.Id
                            End If
                            If tag.TagTypeID = 1 Then
                                ddlCityTag.SelectedValue = tag.Id
                            End If
                            If tag.TagTypeID = 2 Then
                                ddlStateTag.SelectedValue = tag.Id
                            End If
                            If tag.TagTypeID = 3 Then
                                ddlCountryTag.SelectedValue = tag.Id
                            End If
                        Next
                        dtList.TableHeader = New DataGridVisualization.DisplayTableHeader
                        dtList.TableHeader.TableTitle = ""
                        dtList.TableHeader.DetailDisplayName = "Tag"
                        dtList.TableHeader.DetailFieldName = "Name"
                        dtList.TableHeader.DetailKeyName = "Id"
                        dtList.TableHeader.DetailPath = String.Format("/Co_Apps/CityInformation/navigator.aspx?action=contentitem&contentid={0}", .Id)
                        dtList.BuildTable(dtList.TableHeader, (from i in .TagList Where i.TagTypeID=0).tolist)


                    End With
                End If
            End Using
        End If
    End Sub

    Protected Sub cmd_Save_Click(sender As Object, e As EventArgs)
        With myContentItem
            .Title = tbTitle.Text
            .Description = tbDescription.Text
            .Active = cbActive.Checked
            .Description = tbTitle.Text
            .EndPubDate = Now()
            .IconURL = tbIconURL.Text
            .ImageURL = tbImageURL.Text
            .LinkURL = tbLinkURL.Text
            .LastEditDate = Now()
            .LinkAnchorText = tbLinkAnchorText.Text
            .ReadMoreText = tbReadMoreText.Text
        End With

        Dim myTagList = New List(Of TagItem)
        myTagList.Add(New TagItem With {.Id = ddlCountryTag.SelectedValue, .Name = ddlCountryTag.SelectedItem.Text, .TagTypeID = 3})
        myTagList.Add(New TagItem With {.Id = ddlStateTag.SelectedValue, .Name = ddlStateTag.SelectedItem.Text, .TagTypeID = 2})
        myTagList.Add(New TagItem With {.Id = ddlCityTag.SelectedValue, .Name = ddlCityTag.SelectedItem.Text, .TagTypeID = 1})
        myTagList.Add(New TagItem With {.Id = ddlKeywordTag.SelectedValue, .Name = ddlKeywordTag.SelectedItem.Text, .TagTypeID = 0})
        myContentItem.TagList = myTagList.ToArray()

        Using myCon As New CityTagManager.DataController()
            myContentItem = myCon.PutContentItem(myContentItem)
            hfId.Value = myContentItem.Id
        End Using

        ResetForm()
    End Sub

    Protected Sub cmd_Cancel_Click(sender As Object, e As EventArgs)

        ResetForm()
    End Sub

    Protected Sub cmd_Delete_Click(sender As Object, e As EventArgs)
        Using myCon As New CityTagManager.DataController()
            myContentItem = myCon.GetContentById(Utility.GetDBInteger(hfId.Value))
            myCon.DeleteContentItem(myContentItem)
        End Using
        ResetForm()
    End Sub

    Protected Sub ResetForm()
        ClearPageArguments(GetPageArgument("pid").Second.ToString)
        LoadPage()
    End Sub

End Class
