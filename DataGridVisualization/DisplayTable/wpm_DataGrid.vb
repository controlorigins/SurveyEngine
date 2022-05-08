Imports System.Reflection
Imports System.Text
Imports DataGridVisualization.ControlOriginsWS
Imports System.Collections.ObjectModel

Public Class wpm_DataGrid
    Inherits CO_DataGrid

    Public Sub New(ByVal myGrid As CO_DataGrid)
        With myGrid
            Me.GridRows = .GridRows
            Me.GridColumns = .GridColumns
            Me.Title = .Title
        End With
    End Sub
    Public Function GetGrid() As CO_DataGrid
        Return TryCast(Me, CO_DataGrid)
    End Function

    Public Function GetRowAggregate(ByVal AggType As wpm_GridAggregateType, ByVal ColumnName As String, ByVal GroupBy As List(Of String)) As String
        Dim idx As Integer = 0
        Dim FormatString As String = String.Empty
        Dim myReturn As String = String.Empty
        For Each myCol As GridColumn In GridColumns
            If myCol.DisplayName = ColumnName Then
                Select Case myCol.DataType.ToLower()
                    Case "currency"
                        FormatString = "c"
                    Case "integer"
                        FormatString = "n"
                    Case "decimal"
                        FormatString = "n"
                    Case "float"
                        FormatString = "n"
                    Case Else
                        FormatString = "n"
                End Select


                Exit For
            End If
            idx = idx + 1
        Next
        Select Case AggType
            Case wpm_GridAggregateType.Avg
                myReturn = (From i In GridRows Select ControlOrigins.COUtility.Utility.GetDBDouble(i.Value(idx))).Sum().ToString()
            Case wpm_GridAggregateType.Count
                myReturn = (From i In GridRows Select ControlOrigins.COUtility.Utility.GetDBDouble(i.Value(idx))).Sum().ToString()
            Case wpm_GridAggregateType.Sum
                myReturn = (From i In GridRows Select ControlOrigins.COUtility.Utility.GetDBDouble(i.Value(idx))).Sum().ToString(FormatString)
        End Select

        Return myReturn
    End Function

    Public Sub SetHeaders(ByRef myrows As List(Of Object))
        Dim objType As Type
        Dim pInfo As PropertyInfo
        Dim PropValue As New Object
        Dim myCol As List(Of GridColumn) = GridColumns.ToList()
        For Each myItem As Object In myrows
            Try
                objType = myItem.GetType()
                For Each pInfo In objType.GetProperties()
                    myCol.Add(New GridColumn With {.DataType = "string", .DisplayName = pInfo.Name})
                Next
            Catch ex As Exception
                PropValue = String.Empty
            End Try
            Exit For
        Next
        GridColumns = myCol.ToArray()
    End Sub

    Public Function GetPropertyValue(ByVal obj As Object, ByVal PropName As String) As Object
        Dim objType As Type
        Dim pInfo As PropertyInfo
        Dim PropValue As New Object
        If PropName.Contains(".") Then
            Dim PropertyNameArray As String() = Split(PropName, ".")

            If PropertyNameArray.Count = 2 Then
                Try
                    objType = obj.GetType()
                    pInfo = objType.GetProperty(PropertyNameArray(0))
                    PropValue = pInfo.GetValue(obj, Reflection.BindingFlags.GetProperty, Nothing, Nothing, Nothing)

                    objType = PropValue.GetType()
                    pInfo = objType.GetProperty(PropertyNameArray(1))
                    PropValue = pInfo.GetValue(PropValue, Reflection.BindingFlags.GetProperty, Nothing, Nothing, Nothing)
                Catch ex As Exception
                    PropValue = String.Empty
                End Try
            End If
        Else
            Try
                objType = obj.GetType()
                pInfo = objType.GetProperty(PropName)
                PropValue = pInfo.GetValue(obj, Reflection.BindingFlags.GetProperty, Nothing, Nothing, Nothing)
            Catch ex As Exception
                PropValue = String.Empty
            End Try
            If PropValue Is Nothing Then
                PropValue = String.Empty
            End If
        End If
        Return PropValue
    End Function
    Public Function GetCSV() As String
        Dim csv As New StringBuilder
        For Each column As GridColumn In GridColumns
            'Add the Header row for CSV file.
            csv.Append(column.DisplayName + ","c)
        Next
        'Add new line.
        csv.Append(vbCr & vbLf)
        For Each row As GridRow In GridRows
            For Each myValue As String In row.Value
                'Add the Data rows.
                csv.Append((myValue).ToString().Replace(",", ";") + ","c)
            Next
            'Add new line.
            csv.Append(vbCr & vbLf)
        Next
        Return csv.ToString()
    End Function

    Public Function GetJSON() As String
        Dim csv As New StringBuilder
        For Each column As GridColumn In GridColumns
            'Add the Header row for CSV file.
            csv.Append(column.DisplayName + ","c)
        Next
        'Add new line.
        csv.Append(vbCr & vbLf)
        For Each row As GridRow In GridRows
            For Each myValue As String In row.Value
                'Add the Data rows.
                csv.Append((myValue).ToString().Replace(",", ";") + ","c)
            Next
            'Add new line.
            csv.Append(vbCr & vbLf)
        Next
        Return csv.ToString()
    End Function

    Public Enum wpm_GridAggregateType
        Sum
        Avg
        Count
    End Enum
End Class
