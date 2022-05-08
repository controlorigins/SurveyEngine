Imports Microsoft.VisualBasic
Imports LINQHelper.System.Linq.Dynamic
Imports System.Web.Services
Imports System.IO
Imports ControlOrigins.COUtility

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://ws.cityinformationcenter.com")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class DTFMWS
    Inherits WebService
    Dim mydb As New CityTagDataContext
    Public Property DataSource As String


    Const appkey As String = "66416D98-740B-439C-82C9-21E5BCC7E093"

#Region "Tags"
    <WebMethod()> Public Function GetAllTags(key As String) As List(Of TagItem)
        If key = appkey Then
            ApplicationLogging.AuditLog("DTFMWS.GetAllTags", DataSource)
            Return (From i In mydb.Tags Order By i.Name Select New TagItem With {.Id = i.Id,
                                                                                 .Name = i.Name,
                                                                                 .TagTypeID = i.TagTypeID,
                                                                                 .TagTypeName = i.TagType.Name}).ToList
        Else
            Return New List(Of TagItem)
        End If
    End Function
    <WebMethod()> Public Function PutTagItem(ByVal Tag As TagItem, key As String) As TagItem
        If key = appkey Then
            ApplicationLogging.AuditLog("DTFMWS.PutTagItem", DataSource)
            Try
                Dim myReturn = mydb.Tag_Insert(Tag.Name, Tag.TagTypeID).SingleOrDefault

                With myReturn
                    Tag.Id = myReturn.Id
                End With
                Return Tag
            Catch ex As Exception
                Tag.Id = -1
                Return Tag
            End Try
        Else
            Return New TagItem
        End If
    End Function

    <WebMethod()> Public Function DeleteTagItem(ByVal Tag As TagItem, key As String) As Integer
        If key = appkey Then
            ApplicationLogging.AuditLog("DTFMWS.DeleteTagItem", DataSource)
            Dim myReturn = mydb.Tag_DeleteRow(Tag.Id)
            Return myReturn
        Else
            Return -1
        End If
    End Function



    <WebMethod()> Public Function GetTagsByType(ByVal reqTagTypeID As Integer, key As String) As List(Of TagItem)
        If key = appkey Then
            ApplicationLogging.AuditLog(String.Format("DTFMWS.GetTagsByType = {0}", reqTagTypeID), DataSource)
            Return (From i In mydb.Tags
                    Where i.TagTypeID = reqTagTypeID
                    Order By i.Name
                    Select New TagItem With {.Id = i.Id,
                                             .Name = i.Name,
                                             .TagTypeID = i.TagTypeID,
                                             .TagTypeName = i.TagType.Name}).ToList
        Else
            Return New List(Of TagItem)
        End If
    End Function
    <WebMethod()> Public Function GetTagsByContentID(contentID As Integer, key As String) As List(Of TagItem)
        If key = appkey Then
            ApplicationLogging.AuditLog(String.Format("DTFMWS.GetTagsByContentID = {0}", contentID), DataSource)

            Dim mylinktags = (From i In mydb.ContentTags Where i.LinkedContentID = contentID)

            Dim mycoll As New List(Of TagItem)

            For Each ct In mylinktags
                mycoll.Add((From i In mydb.Tags
                            Where i.Id = ct.TagID
                            Select New TagItem With {.Id = i.Id,
                                             .Name = i.Name,
                                             .TagTypeID = i.TagTypeID,
                                             .TagTypeName = i.TagType.Name}).SingleOrDefault)
            Next
            Return mycoll
        Else
            Return New List(Of TagItem)
        End If
    End Function




#End Region
#Region "TagType"
    <WebMethod()> Public Function GetTagTypes(key As String) As List(Of TagTypeItem)
        If key = appkey Then
            ApplicationLogging.AuditLog(String.Format("DTFMWS.GetAllTagTypes = {0}", String.Empty), DataSource)

            Dim myReturn = New List(Of TagTypeItem)
            Try
                myReturn = (From i In mydb.TagType_SelectAll
                        Order By i.Name
                        Select New TagTypeItem With {.Id = i.ID,
                                                     .Name = i.Name,
                                                     .Description = i.Descsription}).ToList
                Return myReturn
            Catch ex As Exception
                Return New List(Of TagTypeItem)
            End Try
        Else
            Return New List(Of TagTypeItem)
        End If
    End Function
    <WebMethod()> Public Function GetTagTypeByTagTypeID(ByVal TagTypeID As Integer, key As String) As TagTypeItem
        If key = appkey Then
            ApplicationLogging.AuditLog(String.Format("DTFMWS.GetTagTypeByTagTypeID = {0}", TagTypeID.ToString), DataSource)
            Dim myReturn = New TagTypeItem
            Try
                myReturn = (From i In mydb.TagType_SelectRow(TagTypeID)
                        Select New TagTypeItem With {.Id = i.ID,
                                                     .Name = i.Name,
                                                     .Description = i.Descsription}).SingleOrDefault
                Return myReturn
            Catch ex As Exception
                Return New TagTypeItem
            End Try
        Else
            Return New TagTypeItem
        End If
    End Function
    <WebMethod()> Public Function PutTagType(ByVal TagType As TagTypeItem, key As String) As TagTypeItem
        If key = appkey Then
            ApplicationLogging.AuditLog(String.Format("DTFMWS.PutTagType= {0}", TagType.Id.ToString), DataSource)
            Dim myReturn = New TagTypeItem
            Try
                If TagType.Id > 0 Then
                    myReturn = (From i In mydb.TagType_Update(TagType.Id, TagType.Name, TagType.Description)
                                Select New TagTypeItem With {.Id = i.ID, .Name = i.Name, .Description = i.Descsription}).SingleOrDefault
                Else
                    myReturn = (From i In mydb.TagType_Insert(TagType.Name, TagType.Description)
                                Select New TagTypeItem With {.Id = i.ID, .Name = i.Name, .Description = i.Descsription}).SingleOrDefault
                End If
                Return myReturn
            Catch ex As Exception
                Return New TagTypeItem
            End Try
        Else
            Return New TagTypeItem
        End If
    End Function
    <WebMethod()> Public Function DeleteTagType(ByVal TagType As TagTypeItem, ByVal key As String) As Integer
        Return -1
    End Function

#End Region
#Region "Content"
    <WebMethod()> Public Function GetAllContent(key As String) As List(Of ContentItem)
        If key = appkey Then
            ApplicationLogging.AuditLog(String.Format("DTFMWS.GetAllContent = {0}", String.Empty), DataSource)

            Dim myReturn = New List(Of ContentItem)
            Try
                myReturn = (From i In mydb.LinkedContents
                        Order By i.Title
                        Select New ContentItem With {.Id = i.Id,
                                                     .Title = i.Title,
                                                     .Description = i.Description,
                                                     .PubDate = i.PubDate,
                                                     .EndPubDate = i.EndPubDate,
                                                     .IconURL = i.IconURL,
                                                     .ImageURL = i.ImageURL,
                                                     .LinkURL = i.LinkURL,
                                                     .LastEditDate = i.LastEditDate,
                                                     .LinkAnchorText = i.LinkAnchorText,
                                                     .Active = i.Active,
                                                     .ReadMoreText = i.ReadMoreText,
                                                    .TagList = (From ii In i.ContentTags Select New TagItem With {.Id = ii.Tag.Id, .Name = ii.Tag.Name, .TagTypeID = ii.Tag.TagTypeID, .TagTypeName = ii.Tag.TagType.Name}).ToList,
                                                     .Keywords = String.Empty}).ToList

                For Each myrow In myReturn
                    myrow.Keywords = GetKeywords(myrow.TagList)
                    If String.IsNullOrEmpty(myrow.IconURL) Then myrow.IconURL = String.Format("http://ws.cityinformationcenter.com/images/{0}", "default.png")
                Next
                Return myReturn
            Catch ex As Exception
                Return New List(Of ContentItem)
            End Try
        Else
            Return New List(Of ContentItem)
        End If
    End Function
    Private Function GetKeywords(ByRef TagList As List(Of TagItem)) As String
        Try
            If TagList Is Nothing Then
                Return String.Empty
            ElseIf TagList.Count = 0 Then
                Return String.Empty
            Else
                Return String.Join(",", (From ii In TagList Select ii.Name).ToArray())
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    <WebMethod()> Public Function GetContentByID(id As Integer, key As String) As ContentItem
        If key = appkey Then
            ApplicationLogging.AuditLog(String.Format("DTFMWS.GetContentByID = {0}", id.ToString), DataSource)

            Dim myItem = (From i In mydb.LinkedContents
                    Where i.Id = id
                     Select New ContentItem With {.Id = i.Id,
                                                 .Title = i.Title,
                                                 .Description = i.Description,
                                                 .PubDate = i.PubDate,
                                                 .EndPubDate = i.EndPubDate,
                                                 .IconURL = i.IconURL,
                                                 .ImageURL = i.ImageURL,
                                                 .LinkURL = i.LinkURL,
                                                 .LastEditDate = i.LastEditDate,
                                                 .LinkAnchorText = i.LinkAnchorText,
                                                 .Active = i.Active,
                                                 .ReadMoreText = i.ReadMoreText}).SingleOrDefault
            myItem.TagList = (From i In mydb.ContentTags
                  Where i.LinkedContentID = myItem.Id
                  Select New TagItem With {.Id = i.Tag.Id,
                                           .Name = i.Tag.Name,
                                           .TagTypeID = i.Tag.TagTypeID,
                                           .TagTypeName = i.Tag.TagType.Name}).ToList()
            myItem.Keywords = GetKeywords(myItem.TagList)
            If String.IsNullOrEmpty(myItem.IconURL) Then myItem.IconURL = String.Format("http://ws.cityinformationcenter.com/images/{0}", "default.png")

            Return myItem
        Else
            Return New ContentItem
        End If
    End Function
    <WebMethod()> Public Function GetContentByTagList(ByVal tags As List(Of TagItem)) As List(Of ContentItem)

        ApplicationLogging.AuditLog(String.Format("DTFMWS.GetContentByTagList = {0}", String.Empty), DataSource)

        Dim myCotentList As New List(Of LinkedContent)
        If tags.Count > 0 Then
            Dim myRetunList = (From tempItem In mydb.LinkedContent_SelectByTags(GetKeywords(tags))
                                    Select New ContentItem With {.Id = tempItem.Id,
                                                                 .Title = tempItem.Title,
                                                                 .Description = tempItem.Description,
                                                                 .PubDate = tempItem.PubDate,
                                                                 .EndPubDate = tempItem.EndPubDate,
                                                                 .IconURL = tempItem.IconURL,
                                                                 .ImageURL = tempItem.ImageURL,
                                                                 .LinkURL = tempItem.LinkURL,
                                                                 .LastEditDate = tempItem.LastEditDate,
                                                                 .LinkAnchorText = tempItem.LinkAnchorText,
                                                                 .Active = tempItem.Active,
                                                                 .ReadMoreText = tempItem.ReadMoreText
                                        }).ToList()
            For Each myItem In myRetunList
                myItem.TagList = (From i In mydb.ContentTags
                                  Where i.LinkedContentID = myItem.Id
                                  Select New TagItem With {.Id = i.Tag.Id,
                                                           .Name = i.Tag.Name,
                                                           .TagTypeID = i.Tag.TagTypeID,
                                                           .TagTypeName = i.Tag.TagType.Name}).ToList()
                myItem.Keywords = GetKeywords(myItem.TagList)
                If String.IsNullOrEmpty(myItem.IconURL) Then myItem.IconURL = String.Format("http://ws.cityinformationcenter.com/images/{0}", "default.png")
            Next
            Return (myRetunList)
        Else
            Return GetAllContent(appkey)
        End If

    End Function
    <WebMethod()> Public Function PutContentItem(item As ContentItem, key As String) As ContentItem
        If key = appkey Then
            ApplicationLogging.AuditLog(String.Format("DTFMWS.PutContentItem = {0}", item.Id), DataSource)
            If item.Id < 1 Then
                With item
                    Dim iReturn = mydb.LinkedContent_Insert(.Title,
                                                            .Description,
                                                            .PubDate,
                                                            .EndPubDate,
                                                            .IconURL,
                                                            .ImageURL,
                                                            .LinkURL,
                                                            .LastEditDate,
                                                            .LinkAnchorText,
                                                            .Active,
                                                            .ReadMoreText).SingleOrDefault
                    .Id = iReturn.Id
                End With
                For Each myTag In item.TagList
                    mydb.ContentTag_Insert(item.Id, myTag.Id)
                Next
            Else
                With item
                    Dim iReturn = mydb.LinkedContent_Update(.Id,
                                                            .Title,
                                                            .Description,
                                                            .PubDate,
                                                            .EndPubDate,
                                                            .IconURL,
                                                            .ImageURL,
                                                            .LinkURL,
                                                            .LastEditDate,
                                                            .LinkAnchorText,
                                                            .Active,
                                                            .ReadMoreText)

                End With
                ' Remove Existing Tags
                mydb.ContentTag_DeleteByLinkedContetID(item.Id)
                ' Add Updated Tags
                For Each Tag In item.TagList
                    mydb.ContentTag_Insert(item.Id, Tag.Id)
                Next
            End If
            Return GetContentByID(item.Id, appkey)
        Else
            Return New ContentItem
        End If
    End Function
    <WebMethod()> Public Function AddContent(item As ContentItem, tags As List(Of TagItem), key As String) As ContentItem
        If key = appkey Then
            ApplicationLogging.AuditLog(String.Format("DTFMWS.AddContent = {0}", item.Id), DataSource)
            If item.Id < 1 Then
                ' We have a new Item
                With item
                    Dim iReturn = mydb.LinkedContent_Insert(.Title,
                                                            .Description,
                                                            .PubDate,
                                                            .EndPubDate,
                                                            .IconURL,
                                                            .ImageURL,
                                                            .LinkURL,
                                                            .LastEditDate,
                                                            .LinkAnchorText,
                                                            .Active,
                                                            .ReadMoreText).SingleOrDefault
                    .Id = iReturn.Id
                End With
                For Each myTag In tags
                    mydb.ContentTag_Insert(item.Id, myTag.Id)
                Next
            Else
                Return New ContentItem
            End If
            Return GetContentByID(item.Id, appkey)
        Else
            Return New ContentItem
        End If
    End Function
    <WebMethod()> Public Function UpdateConent(item As ContentItem, tags As List(Of TagItem), Key As String) As Integer
        If Key = appkey Then
            ApplicationLogging.AuditLog(String.Format("DTFMWS.UpdateConent = {0}", item.Id), DataSource)
            UpdateContentOnly(item, Key)
            UpdateContentTags(item.Id, tags, Key)
            Return item.Id
        Else
            Return 0
        End If
    End Function
    <WebMethod()> Public Function UpdateContentOnly(item As ContentItem, key As String) As Integer
        If key = appkey Then
            ApplicationLogging.AuditLog(String.Format("DTFMWS.UpdateContentOnly = {0}", item.Id), DataSource)
            Dim myitem = (From i In mydb.LinkedContents Where i.Id = item.Id).Single
            With myitem
                .Active = item.Active
                .Description = item.Description
                .EndPubDate = item.EndPubDate
                .IconURL = item.IconURL
                .ImageURL = item.ImageURL
                .LastEditDate = Now
                .LinkAnchorText = item.LinkAnchorText
                .LinkURL = item.LinkURL
                .PubDate = item.PubDate
                .Title = item.Title
            End With
            mydb.SubmitChanges()
            Return myitem.Id
        Else
            Return 0
        End If
    End Function
    <WebMethod()> Public Function RemoveLinkedItem(Linkeditemid As Integer, key As String) As Boolean
        If key = appkey Then
            ApplicationLogging.AuditLog(String.Format("DTFMWS.RemoveLinkedItem = {0}", Linkeditemid), DataSource)
            Try
                mydb.ContentTag_DeleteByLinkedContetID(Linkeditemid)
                mydb.LinkedContent_DeleteRow(Linkeditemid)
                Return True
            Catch
                Return False
            End Try
        Else
            Return False
        End If
    End Function
#End Region
#Region "Content Tag"
    <WebMethod()> Public Function GetAllContentTags(key As String) As List(Of ContentTagItem)
        If key = appkey Then
            ApplicationLogging.AuditLog(String.Format("DTFMWS.GetAllContentTags = {0}", String.Empty), DataSource)

            Return (From i In mydb.ContentTags
                    Select New ContentTagItem With {.ContentID = i.LinkedContentID,
                                                    .ContentTitle = i.LinkedContent.Title,
                                                    .Id = i.Id,
                                                    .TagID = i.TagID,
                                                    .TagName = i.Tag.Name}).ToList
        Else
            Return New List(Of ContentTagItem)
        End If
    End Function
    <WebMethod()> Public Function UpdateContentTags(LinkedContentID As Integer, tags As List(Of TagItem), key As String) As Integer
        If key = appkey Then
            ApplicationLogging.AuditLog(String.Format("DTFMWS.UpdateContentTags = {0}", LinkedContentID), DataSource)

            ' Remove old Tags
            Dim myoldtags = (From i In mydb.ContentTags Where i.LinkedContentID = LinkedContentID).ToList
            For Each t In myoldtags
                Dim myct = (From i In mydb.ContentTags Where i.LinkedContentID = LinkedContentID And i.TagID = t.TagID).Single
                mydb.ContentTags.DeleteOnSubmit(myct)
            Next
            mydb.SubmitChanges()

            ' Add New Tags
            For Each i In tags
                Dim myct As New ContentTag With {.LinkedContentID = LinkedContentID, .TagID = i.Id}
                mydb.ContentTags.InsertOnSubmit(myct)
            Next
            mydb.SubmitChanges()
            Return LinkedContentID
        Else
            Return 0
        End If
    End Function
#End Region
#Region "Image Functions"
    ' Download Image to image dir
    <WebMethod()> Public Function FetchImage(imageURL As String, key As String) As String
        If key = appkey Then
            ApplicationLogging.AuditLog(String.Format("DTFMWS.FetchImage = {0}", imageURL), DataSource)
            Try
                Dim LocalImageURL As String = ""
                Dim featchfile As String = imageURL
                Dim filemunch = Split(featchfile, "/")
                LocalImageURL = "images/" & filemunch(filemunch.Length - 1)
                Dim localfile As FileInfo = New FileInfo(Server.MapPath(LocalImageURL))
                If Not localfile.Exists Then
                    My.Computer.Network.DownloadFile(imageURL, Server.MapPath(LocalImageURL))
                End If
                Return String.Format("http://ws.cityinformationcenter.com/{0}", LocalImageURL)
            Catch ex As Exception
                Return imageURL
            End Try
        Else
            Return "not Authorized"
        End If
    End Function
#End Region
    Private Shared Function CountOccurenceOfValue(list As List(Of ContentTag), valueToFind As Integer) As Integer
        Return ((From temp In list Where temp.LinkedContentID = valueToFind).Count())
    End Function

End Class