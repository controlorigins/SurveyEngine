Imports CityTagManager.WSCityInformation

Public Class DataController
    Implements IDisposable

    Dim WithEvents myWS As DTFMWS = New DTFMWS
    Const appkey As String = "66416D98-740B-439C-82C9-21E5BCC7E093"

#Region "Content Tag"
    Public Function GetAllContentTags() As List(Of ContentTagItem)
        Return myWS.GetAllContentTags(appkey).ToList()
    End Function

#End Region

#Region "Linked Content"
    Public Function GetAllContent() As List(Of ContentItem)
        Return myWS.GetAllContent(appkey).ToList()
    End Function
    Public Function GetContentById(ByVal reqId As Integer)
        Return myWS.GetContentByID(reqId, appkey)
    End Function
    Public Function GetContentByTagList(ByVal reqTags As List(Of TagItem)) As List(Of ContentItem)
        Return myWS.GetContentByTagList(reqTags.ToArray).ToList
    End Function
    Public Function PutContentItem(ByVal reqContent As ContentItem) As ContentItem
        Return myWS.PutContentItem(reqContent, appkey)
    End Function
    Public Function DeleteContentItem(ByVal reqContet As ContentItem) As Boolean
        Return myWS.RemoveLinkedItem(reqContet.Id, appkey)
    End Function
#End Region

#Region "Tag"
    Public Function GetAllTags() As List(Of TagItem)
        Return myWS.GetAllTags(appkey).ToList
    End Function
    Public Function GetAllKeywordTags() As List(Of TagItem)
        Return (From i In myWS.GetTagsByType(0, appkey)).ToList()
    End Function
    Public Function GetAllCityTags() As List(Of TagItem)
        Return (From i In myWS.GetTagsByType(1, appkey)).ToList()
    End Function
    Public Function GetAllStateTags() As List(Of TagItem)
        Return (From i In myWS.GetTagsByType(2, appkey)).ToList()
    End Function
    Public Function GetAllCountryTags() As List(Of TagItem)
        Return (From i In myWS.GetTagsByType(3, appkey)).ToList()
    End Function
    Public Function PutTag(ByVal Tag As TagItem) As TagItem
        Return myWS.PutTagItem(Tag, appkey)
    End Function
    Public Function DeleteTag(ByVal Tag As TagItem) As Integer
        Return myWS.DeleteTagItem(Tag, appkey)
    End Function
    Public Function GetTagByTagID(ByVal TagID As Integer) As TagItem
        Return (From i In GetAllTags() Where i.Id = TagID).SingleOrDefault
    End Function
#End Region

#Region "Tag Type"

    Public Function PutTagType(ByVal TagType As TagTypeItem) As TagTypeItem
        Return myWS.PutTagType(TagType, appkey)
    End Function
    Public Function GetTagTypeByTagTypeID(ByVal TagTypeID As Integer) As TagTypeItem
        Return myWS.GetTagTypeByTagTypeID(TagTypeID, appkey)
    End Function
    Public Function GetTagTypes() As List(Of TagTypeItem)
        Return myWS.GetTagTypes(appkey).ToList()
    End Function
    Public Function DeleteTagType(ByVal TagType As TagTypeItem) As Integer
        Return myWS.DeleteTagType(TagType,appkey)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
