Imports System.Text
Imports DataGridVisualization.ControlOriginsWS

Public Class DisplayTableHeader
    Public HeaderItems As New List(Of DisplayTableHeaderItem)
    Public TableTitle As String
    Public DetailPath As String
    Public DetailKeyName As String
    Public DetailFieldName As String
    Public DetailKeyGridIndex As Integer
    Public DetailFieldGridIndex As Integer
    Public DetailDisplayName As String

    Public Sub AddHeaderItem(ByVal Name As String, ByVal Value As String)
        HeaderItems.Add(New DisplayTableHeaderItem With {.Name = Name, .Value = Value})
    End Sub
    Public Sub AddHeaderItem(ByVal Name As String, ByVal Value As String, ByVal ShowOnPhone As Boolean)
        HeaderItems.Add(New DisplayTableHeaderItem With {.Name = Name, .Value = Value, .ViewOnPhone = ShowOnPhone})
    End Sub
    Public Sub AddHeaderItem(ByVal Name As String, ByVal Value As String, ByVal ShowOnPhone As Boolean, ByVal Display As DisplayFormat)
        HeaderItems.Add(New DisplayTableHeaderItem With {.Name = Name,
                                                         .Value = Value,
                                                         .ViewOnPhone = ShowOnPhone,
                                                         .DisplayFormat = Display})
    End Sub


    Public Sub AddLinkHeaderItem(ByVal DisplayName As String, ByVal Value As String, ByVal LinkPath As String, ByVal LinkKeyName As String)
        HeaderItems.Add(New DisplayTableHeaderItem With {.Name = DisplayName,
                                                         .Value = Value,
                                                         .KeyField = True,
                                                         .LinkKeyName = LinkKeyName,
                                                         .LinkPath = LinkPath,
                                                         .LinkTextName = Value})
    End Sub
    Sub AddLinkHeaderItem(DisplayName As String, Value As String, LinkPath As String, LinkKeyName As String, LinkTextName As String)
        HeaderItems.Add(New DisplayTableHeaderItem With {.Name = DisplayName,
                                                         .Value = Value,
                                                         .KeyField = True,
                                                         .LinkKeyName = LinkKeyName,
                                                         .LinkPath = LinkPath,
                                                         .LinkTextName = LinkTextName})
    End Sub
    Sub AddLinkHeaderItem(DisplayName As String, Value As String, LinkPath As String, LinkKeyName As String, LinkTextName As String, ThumbnailPath As String)
        HeaderItems.Add(New DisplayTableHeaderItem With {.Name = DisplayName,
                                                         .Value = Value,
                                                         .KeyField = True,
                                                         .LinkKeyName = LinkKeyName,
                                                         .LinkPath = LinkPath,
                                                         .LinkTextName = LinkTextName,
                                                         .ThumbnailPath = ThumbnailPath})
    End Sub
    Sub AddHeaderThumbnailItem(DisplayName As String, Value As String, LinkPath As String, LinkKeyName As String, LinkTextName As String, ThumbPath As String)
        HeaderItems.Add(New DisplayTableHeaderItem With {.Name = DisplayName,
                                                         .Value = Value,
                                                         .KeyField = True,
                                                         .LinkKeyName = LinkKeyName,
                                                         .LinkPath = LinkPath,
                                                         .ThumbnailPath = ThumbPath,
                                                         .LinkTextName = LinkTextName})
    End Sub

    Public Function GetDisplayTableHeader() As String
        Dim myrow As New StringBuilder
        If DetailFieldName <> String.Empty Then
            If DetailDisplayName <> String.Empty Then
                myrow.AppendLine(String.Format("<th>{0}</th>", FormatHeaderColumn(DetailDisplayName)))
            Else
                myrow.AppendLine(String.Format("<th>{0}</th>", FormatHeaderColumn(DetailFieldName)))
            End If
        End If
        For Each myHeaderItem As DisplayTableHeaderItem In HeaderItems
            myrow.AppendLine(String.Format("<th {1}  >{0}</th>", FormatHeaderColumn(myHeaderItem.Name), GetTDCssClass(myHeaderItem)))
        Next
        Return myrow.ToString()
    End Function
    Private Function FormatHeaderColumn(ByVal myColName As String) As String
        Dim newColumnName As String
        Select Case Right(myColName, 2)
            Case "NM"
                newColumnName = Left(myColName, Len(myColName) - 2) & " Name"
            Case "CD"
                newColumnName = Left(myColName, Len(myColName) - 2) & " Code"
            Case "DS"
                newColumnName = Left(myColName, Len(myColName) - 2) & " Description"
            Case "ID"
                newColumnName = Left(myColName, Len(myColName) - 2) & " Id"
            Case Else
                newColumnName = myColName
        End Select

        Dim newstring As String = ""
        For i As Integer = 0 To newColumnName.Length - 1
            If Char.IsUpper(newColumnName(i)) AndAlso i > 0 Then
                newstring += " "
            End If
            newstring += newColumnName(i).ToString()
        Next
        Return newstring
    End Function
    Private Function GetTDCssClass(ByRef p1 As DisplayTableHeaderItem) As String
        If p1.ViewOnPhone Then
            Return String.Empty
        Else
            Return " class='hidden-xs' "
        End If
    End Function

End Class

