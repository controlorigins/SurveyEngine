﻿Imports System.Data
Imports DataGridVisualization.ControlOriginsWS
Imports DataGridVisualization

Public Class controls_CSV_DisplayTable
    Inherits System.Web.UI.UserControl
    Implements Icontrols_CSV_DisplayTable

    Public Sub BuildDataTable(ByVal filePath As String, ByVal myDataGrid As CO_DataGrid) Implements Icontrols_CSV_DisplayTable.BuildDataTable
        BuildTableFromGrid(myDataGrid)
    End Sub

    Public Sub BuildTableFromGrid(myDataGrid As co_DataGrid)
        Dim myrow As New StringBuilder
        Dim myTableRows As New List(Of String)
        Dim PropValue As String
        tblTitle.Text = myDataGrid.Title
        myrow = New StringBuilder
        Dim myHeaderRow As New StringBuilder

        For Each column In myDataGrid.GridColumns
            myHeaderRow.AppendLine(String.Format("<th {1}  >{0}</th>", column.DisplayName, String.Empty))
        Next
        rptHeader.Text = myHeaderRow.ToString()
        For Each i In myDataGrid.GridRows
            myrow = New StringBuilder
            For Each p In myDataGrid.GridColumns
                PropValue = i.Value(p.Index)
                myrow.AppendLine(GetFormatTableCell(PropValue, p.ColumnDisplayFormat))
            Next
            myTableRows.Add(myrow.ToString())
        Next
        rptDataRows.DataSource = myTableRows
        rptDataRows.DataBind()
    End Sub
    Public Shared Function GetFormatTableCell(ByVal PropValue As String, ByVal ColumnDisplayFormat As String, ByVal LinkURL As String) As String
        Dim myReturn As String = ""
        If IsNumeric(PropValue) Then
            Select Case ColumnDisplayFormat
                Case DisplayFormat.Currency
                    PropValue = Math.Round(CDec(PropValue), 2).ToString("c")
                    myReturn = (String.Format("<td {2} style='text-align: right; ' ><a href='{1}' >{0}</a></td>", PropValue, LinkURL, GetTDCssClass(ColumnDisplayFormat)))
                Case DisplayFormat.Text
                    PropValue = Math.Round(CDec(PropValue), 2).ToString()
                    myReturn = (String.Format("<td {2} style='text-align: left; ' ><a href='{1}' >{0}</a></td>", PropValue, LinkURL, GetTDCssClass(ColumnDisplayFormat)))
                Case DisplayFormat.Number
                    PropValue = Math.Round(CDec(PropValue), 2).ToString()
                    myReturn = (String.Format("<td {2} style='text-align: right; ' ><a href='{1}' >{0}</a></td>", PropValue, LinkURL, GetTDCssClass(ColumnDisplayFormat)))
                Case DisplayFormat.Percent
                    PropValue = Math.Round(CDbl(PropValue), 4).ToString()
                    myReturn = (String.Format("<td {2} style='text-align: right; ' ><a href='{1}' >{0}</a></td>", PropValue, LinkURL, GetTDCssClass(ColumnDisplayFormat)))
                Case DisplayFormat.Float
                    PropValue = Math.Round(CDbl(PropValue), 4).ToString()
                    myReturn = (String.Format("<td {2} style='text-align: right; ' ><a href='{1}' >{0}</a></td>", PropValue, LinkURL, GetTDCssClass(ColumnDisplayFormat)))
                Case DisplayFormat.Thumbnail
                    myReturn = (String.Format("<td {2} style='text-align: right; ' ><img width='50px' src='{1}' alt='{0}' />{0}</td>", PropValue, LinkURL, GetTDCssClass(ColumnDisplayFormat)))
                Case Else
                    PropValue = Math.Round(CDec(PropValue), 2).ToString()
                    myReturn = (String.Format("<td {2} style='text-align: left; ' ><a href='{1}' >{0}</a></td>", PropValue, LinkURL, GetTDCssClass(ColumnDisplayFormat)))
            End Select
        Else
            myReturn = (String.Format("<td {2} style='text-align: left; ' ><a href='{1}' >{0}</a></td>", PropValue, LinkURL, GetTDCssClass(ColumnDisplayFormat)))
        End If
        Return myReturn
    End Function
    Public Shared Function GetFormatTableCell(ByVal PropValue As String, ByVal ColumnDisplayFormat As String) As String
        Dim myReturn As String = ""
        If IsNumeric(PropValue) Then
            Select Case ColumnDisplayFormat
                Case DisplayFormat.Currency
                    PropValue = Math.Round(CDec(PropValue), 2).ToString("c")
                    myReturn = (String.Format("<td {1} style='text-align: right; ' >{0}</td>", PropValue, GetTDCssClass(ColumnDisplayFormat)))
                Case DisplayFormat.Text
                    PropValue = Math.Round(CDec(PropValue), 2).ToString()
                    myReturn = (String.Format("<td {1} style='text-align: left; ' >{0}</td>", PropValue, GetTDCssClass(ColumnDisplayFormat)))
                Case DisplayFormat.Number
                    PropValue = Math.Round(CDec(PropValue), 2).ToString("N0")
                    myReturn = (String.Format("<td {1} style='text-align: right; ' >{0}</td>", PropValue, GetTDCssClass(ColumnDisplayFormat)))
                Case DisplayFormat.Percent
                    PropValue = Math.Round(CDbl(PropValue), 4).ToString("P")
                    myReturn = (String.Format("<td {1} style='text-align: right; ' >{0}</td>", PropValue, GetTDCssClass(ColumnDisplayFormat)))
                Case DisplayFormat.Float
                    PropValue = Math.Round(CDbl(PropValue), 4).ToString("N4")
                    myReturn = (String.Format("<td {1} style='text-align: right; ' >{0}</td>", PropValue, GetTDCssClass(ColumnDisplayFormat)))
                Case Else
                    PropValue = Math.Round(CDec(PropValue), 2).ToString()
                    myReturn = (String.Format("<td {1} style='text-align: left; ' >{0}</td>", PropValue, GetTDCssClass(ColumnDisplayFormat)))
            End Select
        Else
            myReturn = (String.Format("<td {1} style='text-align: left; ' >{0}</td>", PropValue, GetTDCssClass(ColumnDisplayFormat)))
        End If
        Return myReturn
    End Function
    Public Shared Function GetTDCssClass(ByVal ColumnDisplayFormat As String) As String
        If ColumnDisplayFormat = DisplayFormat.Text Then
            Return String.Empty
        Else
            Return " class='hidden-xs' "
        End If
    End Function

End Class
