Namespace COUtility
    Public Class LookupItem
        Implements ILookup
        Public Property Name As String Implements ILookup.Name
        Public Property Value As String Implements ILookup.Value
    End Class
    Public Interface ILookup
        Property Name As String
        Property Value As String
    End Interface
    Public Interface ILookupItemList
        WriteOnly Property LookupList As List(Of LookupItem)
        ReadOnly Property value As String
    End Interface
End Namespace