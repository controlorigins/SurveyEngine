Imports Microsoft.VisualBasic

Public Class TagItem
    Public Id As Integer
    Public Name As String
    Public TagTypeID As Integer
    Public TagTypeName As String
End Class

Public Class ContentItem
    Public Id As Integer
    Public Title As String
    Public Description As String
    Public PubDate As System.Nullable(Of Date)
    Public EndPubDate As System.Nullable(Of Date)
    Public IconURL As String
    Public ImageURL As String
    Public LinkURL As String
    Public LastEditDate As System.Nullable(Of Date)
    Public LinkAnchorText As String
    Public Active As Boolean
    Public ReadMoreText As String
    Public Keywords As String
    Public TagList As New List(Of TagItem)
End Class

Public Class ContentTagItem
    Public Id As Integer
    Public ContentID As Integer
    Public ContentTitle As String
    Public TagID As Integer
    Public TagName As String
End Class

Public Class TagTypeItem
    Public Id As Integer
    Public Name As String 
    Public Description As String 
End Class
