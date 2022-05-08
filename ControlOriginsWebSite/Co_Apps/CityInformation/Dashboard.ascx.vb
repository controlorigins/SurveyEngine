Imports CityTagManager.WSCityInformation
Imports ControlOrigins.COUtility

Public Class Co_Apps_CityInformation_Dashboard
    Inherits ApplicationControlBase


    Public Enum ViewType
        ContentItemByTag
    End Enum
    Private _myControl As ApplicationControlBase
    Public Property myControl As ApplicationControlBase
        Get
            Return _myControl
        End Get
        Set(ByVal value As ApplicationControlBase)
            _myControl = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sAction = GetPageArgument("action").Second
        Select Case sAction
            Case "contentitem"
                pnlListChoice.Visible = False
                pnlEdit.Controls.Clear()
                If Utility.GetDBInteger(GetPageArgument("contentid").Second, 0) = 0 Then
                    ' Do Nothing
                Else
                    myControl = DirectCast(Page.LoadControl("~/Co_Apps/CityInformation/Controls/ContentItem.ascx"), ApplicationControlBase)
                    pnlEdit.Controls.Clear()
                    pnlEdit.Visible = True
                    pnlEdit.Controls.Add(myControl)
                End If
            Case "tagitem"
                pnlListChoice.Visible = False
                pnlEdit.Controls.Clear()
                If Utility.GetDBInteger(GetPageArgument("tagid").Second, 0) = 0 Then
                    ' Do Nothing
                Else
                    myControl = DirectCast(Page.LoadControl("~/Co_Apps/CityInformation/Controls/TagItem.ascx"), ApplicationControlBase)
                    pnlEdit.Controls.Clear()
                    pnlEdit.Visible = True
                    pnlEdit.Controls.Add(myControl)
                End If
            Case Else
        End Select
    End Sub

    Protected Sub cmd_GetAllContentTags_Click(sender As Object, e As EventArgs)
        dtList.Visible = True
        Dim myList As New List(Of ContentTagItem)
        Using myDC As New CityTagManager.DataController()
            myList.AddRange(myDC.GetAllContentTags())
        End Using
        dtList.BuildTable(myList)
    End Sub

    Protected Sub cmd_GetAllTags_Click(sender As Object, e As EventArgs)
        dtList.Visible = True
        Dim myList As New List(Of TagItem)
        Using myDC As New CityTagManager.DataController()
            myList.AddRange(myDC.GetAllTags())
        End Using
        dtList.TableHeader = New DataGridVisualization.DisplayTableHeader
        dtList.TableHeader.TableTitle = String.Format("Tag (<a href='/Co_Apps/CityInformation/navigator.aspx?action=tagitem&tagid={0}'>New</a>)",-1)
        dtList.TableHeader.DetailDisplayName = "Name"
        dtList.TableHeader.DetailFieldName = "Name"
        dtList.TableHeader.DetailKeyName = "Id"
        dtList.TableHeader.DetailPath = "/Co_Apps/CityInformation/navigator.aspx?action=tagitem&tagid={0}"
            dtList.TableHeader.AddHeaderItem("TagTypeName", "TagTypeName", True, DataGridVisualization.ControlOriginsWS.DisplayFormat.Text)
        dtList.BuildTable(dtList.TableHeader, myList)

    End Sub

    Protected Sub cmd_GetAllContent_Click(sender As Object, e As EventArgs)
        Using myDC As New CityTagManager.DataController()
            dtList.TableHeader = New DataGridVisualization.DisplayTableHeader
            dtList.TableHeader.TableTitle = "Tagged Content"
            dtList.TableHeader.DetailDisplayName = "Title"
            dtList.TableHeader.DetailFieldName = "Title"
            dtList.TableHeader.DetailKeyName = "Id"
            dtList.TableHeader.DetailPath = "/Co_Apps/CityInformation/navigator.aspx?action=contentitem&contentid={0}"
            dtList.TableHeader.AddHeaderItem("Id", "Id", True, DataGridVisualization.ControlOriginsWS.DisplayFormat.Text)
            dtList.TableHeader.AddHeaderItem("Description", "Description", False, DataGridVisualization.ControlOriginsWS.DisplayFormat.Text)
            dtList.TableHeader.AddHeaderItem("PubDate", "PubDate", False, DataGridVisualization.ControlOriginsWS.DisplayFormat.Text)
            dtList.TableHeader.AddHeaderItem("LinkURL", "LinkURL", True, DataGridVisualization.ControlOriginsWS.DisplayFormat.Text)
            dtList.TableHeader.AddHeaderItem("Active", "Active", True, DataGridVisualization.ControlOriginsWS.DisplayFormat.Text)
            dtList.TableHeader.AddHeaderItem("Keywords", "Keywords", True, DataGridVisualization.ControlOriginsWS.DisplayFormat.Text)
            dtList.BuildTable(dtList.TableHeader, myDC.GetAllContent())
        End Using
    End Sub

    Protected Sub cmd_FindContent_Click(sender As Object, e As EventArgs)
        dtList.Visible = True
        Dim myTagList As New List(Of TagItem)
        Dim myCotentList As New List(Of ContentItem)

        If ddlCityTag.SelectedItem.Text = String.Empty Then
            myTagList.Add(New TagItem With {.Id = 100, .Name = "Austin", .TagTypeID = 1})
        Else
            myTagList.Add(New TagItem With {.Id = 100, .Name = ddlCityTag.SelectedItem.Text, .TagTypeID = 1})
        End If

        If ddlKeywordTag.SelectedItem.Text = String.Empty Then
            myTagList.Add(New TagItem With {.Id = 99, .Name = "Restaurants", .TagTypeID = 0})
        Else
            myTagList.Add(New TagItem With {.Id = 100, .Name = ddlKeywordTag.SelectedItem.Text, .TagTypeID = 1})
        End If

        Using myDC As New CityTagManager.DataController()
            myCotentList.AddRange(myDC.GetContentByTagList(myTagList))
        End Using

        '        myCotentList(0).PubDate

        dtList.TableHeader = New DataGridVisualization.DisplayTableHeader
        dtList.TableHeader.TableTitle = "Tagged Content"
        dtList.TableHeader.DetailDisplayName = "Title"
        dtList.TableHeader.DetailFieldName = "Title"
        dtList.TableHeader.DetailKeyName = "Id"
        dtList.TableHeader.DetailPath = "/Co_Apps/CityInformation/navigator.aspx?action=contentitem&contentid={0}"
        dtList.TableHeader.AddHeaderItem("Id", "Id", True, DataGridVisualization.ControlOriginsWS.DisplayFormat.Text)
        dtList.TableHeader.AddHeaderItem("Description", "Description", False, DataGridVisualization.ControlOriginsWS.DisplayFormat.Text)
        dtList.TableHeader.AddHeaderItem("PubDate", "PubDate", False, DataGridVisualization.ControlOriginsWS.DisplayFormat.Text)
        dtList.TableHeader.AddHeaderItem("LinkURL", "LinkURL", True, DataGridVisualization.ControlOriginsWS.DisplayFormat.Text)
        dtList.TableHeader.AddHeaderItem("Active", "Active", True, DataGridVisualization.ControlOriginsWS.DisplayFormat.Text)
        dtList.TableHeader.AddHeaderItem("Keywords", "Keywords", True, DataGridVisualization.ControlOriginsWS.DisplayFormat.Text)
        dtList.BuildTable(dtList.TableHeader, myCotentList)

    End Sub
End Class
