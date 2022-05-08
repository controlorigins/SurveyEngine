Imports Microsoft.VisualBasic

Public Module DataTools





    Public Function GetProps(sender As Object) As List(Of String)
        Dim mycoll As New List(Of String)
        For Each i In sender.GetType().GetProperties()
            mycoll.Add(i.Name)
        Next
        Return mycoll
    End Function




End Module
