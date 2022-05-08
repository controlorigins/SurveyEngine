Imports System.Reflection
Imports DataGridVisualization.ControlOriginsWS
Imports DataGridVisualization
Imports ControlOrigins.COUtility

Public Class controls_DisplayTable
    Inherits DataTableControlBase
    Implements Icontrols_DisplayTable
    Public Property EnableCSV As Boolean Implements Icontrols_DisplayTable.EnableCSV
        Get
            Return cmd_GetCSV.Visible
        End Get
        Set(value As Boolean)
            cmd_GetCSV.Visible = value
        End Set
    End Property
    Public Property TableHeader As New DisplayTableHeader Implements Icontrols_DisplayTable.TableHeader

    Public Sub BuildTable(myRows As Object) Implements Icontrols_DisplayTable.BuildTable
        BuildTable(GetHeaders(myRows), myRows)
    End Sub
    Public Sub BuildTableFromGrid(myHeader As DisplayTableHeader, myDataGrid As CO_DataGrid) Implements Icontrols_DisplayTable.BuildTableFromGrid
        Dim myTableHeaders As New List(Of String)
        Dim myrow As New StringBuilder
        Dim myTableRows As New List(Of String)
        Dim PropValue As String
        tblTitle.Text = myHeader.TableTitle
        myrow = New StringBuilder
        For gridindex = 0 To myDataGrid.GridColumns.Count - 1
            Dim x = gridindex
            For Each myHeaderItem In (From i In myHeader.HeaderItems Where i.Value = myDataGrid.GridColumns(x).DisplayName)
                myHeaderItem.GridIndex = x
            Next
            If myHeader.DetailKeyName = myDataGrid.GridColumns(x).DisplayName Then
                myHeader.DetailKeyGridIndex = x
            End If
            If myHeader.DetailFieldName = myDataGrid.GridColumns(x).DisplayName Then
                myHeader.DetailFieldGridIndex = x
            End If
        Next
        rptHeader.Text = myHeader.GetDisplayTableHeader()

        For Each i In myDataGrid.GridRows
            myrow = New StringBuilder
            If myHeader.DetailFieldName <> String.Empty Then
                Try
                    If myHeader.DetailKeyGridIndex <= i.Value.Count AndAlso myHeader.DetailFieldGridIndex <= i.Value.Count Then
                        Dim myTest1 = i.Value(myHeader.DetailKeyGridIndex)
                        Dim MyTest2 = i.Value(myHeader.DetailFieldGridIndex)
                        myrow.AppendLine(String.Format("<td><a href='{0}' >{1}</a></td>", String.Format(myHeader.DetailPath, myTest1), MyTest2))
                    End If
                Catch ex As Exception
                    ApplicationLogging.ErrorLog("DisplayTable-BuildTable with Grid", ex.ToString)
                End Try
            End If
            For Each p In myHeader.HeaderItems
                PropValue = i.Value(p.GridIndex).ToString()
                If p.KeyField Then
                    myrow.AppendLine(p.GetFormatTableCell(PropValue, String.Format(p.LinkPath, i.Value(myHeader.DetailKeyGridIndex))))
                Else
                    myrow.AppendLine(p.GetFormatTableCell(PropValue))
                End If
            Next
            myTableRows.Add(myrow.ToString())
        Next
        rptDataRows.DataSource = myTableRows
        rptDataRows.DataBind()
    End Sub
    'Public Sub BuildTableFromGrid(ByVal myHeader As DisplayTableHeader, ByVal myDataGrid As CODataCon.com.controlorigins.ws.CO_DataGrid)
    '    Dim myrow As New StringBuilder
    '    Dim myTableRows As New List(Of String)
    '    Dim PropValue As String = ""
    '    tblTitle.Text = myHeader.TableTitle
    '    myrow = New StringBuilder
    '    For gridindex = 0 To myDataGrid.GridColumns.Count - 1
    '        Dim x = gridindex
    '        Dim y = myDataGrid.GridColumns(gridindex)

    '        For Each myHeaderItem In myHeader.HeaderItems
    '            If myHeaderItem.Value = y.DisplayName then 
    '                myHeaderItem.GridIndex = gridindex
    '            End If
    '        Next
    '        For Each myHeaderItem In (From i In myHeader.HeaderItems Where i.Value = myDataGrid.GridColumns(x).DisplayName)
    '            myHeaderItem.GridIndex = x
    '        Next
    '        If myHeader.DetailKeyName = myDataGrid.GridColumns(x).DisplayName Then
    '            myHeader.DetailKeyGridIndex = x
    '        End If
    '        If myHeader.DetailFieldName = myDataGrid.GridColumns(x).DisplayName Then
    '            myHeader.DetailFieldGridIndex = x
    '        End If
    '    Next
    '    rptHeader.Text = myHeader.GetDisplayTableHeader()
    '    For Each i In myDataGrid.GridRows
    '        myrow = New StringBuilder
    '        If myHeader.DetailFieldName <> String.Empty Then
    '            Try
    '                If myHeader.DetailKeyGridIndex <= i.Value.Count AndAlso myHeader.DetailFieldGridIndex <= i.Value.Count Then
    '                    Dim myTest1 = i.Value(myHeader.DetailKeyGridIndex)
    '                    Dim MyTest2 = i.Value(myHeader.DetailFieldGridIndex)
    '                    myrow.AppendLine(String.Format("<td><a href='{0}' >{1}</a></td>", String.Format(myHeader.DetailPath, myTest1), MyTest2))
    '                End If
    '            Catch ex As Exception
    '                ApplicationLogging.ErrorLog("DisplayTable-BuildTable with Grid", ex.ToString)
    '            End Try
    '        End If

    '        For Each p In myHeader.HeaderItems
    '            PropValue = i.Value(p.GridIndex)
    '            If p.KeyField Then
    '                myrow.AppendLine(p.GetFormatTableCell(PropValue, String.Format(p.LinkPath, i.Value(myHeader.DetailKeyGridIndex))))
    '            Else
    '                myrow.AppendLine(p.GetFormatTableCell(PropValue))
    '            End If
    '        Next
    '        myTableRows.Add(myrow.ToString())
    '    Next

    '    rptDataRows.DataSource = myTableRows
    '    rptDataRows.DataBind()
    '    If EnableCSV Then
    '        hfCSV.Value = GetCSV(myHeader, myTableRows)
    '    End If


    'End Sub
    Public Sub BuildTable(ByVal myHeader As DisplayTableHeader, ByVal myRows As Object) Implements Icontrols_DisplayTable.BuildTable
        Dim myrow As New StringBuilder
        Dim myTableRows As New List(Of String)
        Dim PropValue As String
        tblTitle.Text = myHeader.TableTitle

        If Not IsNothing(myRows) Then
            rptHeader.Text = myHeader.GetDisplayTableHeader()
            For Each i In myRows
                myrow = New StringBuilder
                If myHeader.DetailFieldName <> String.Empty Then
                    myrow.AppendLine(String.Format("<td ><a href='{0}' >{1}</a></td>", String.Format(myHeader.DetailPath, GetPropertyValue(i, myHeader.DetailKeyName)), GetPropertyValue(i, myHeader.DetailFieldName)))
                End If
                For Each p In myHeader.HeaderItems
                    PropValue = GetPropertyValue(i, p.Value).ToString()

                    If p.KeyField Then
                        myrow.AppendLine(p.GetFormatTableCell(PropValue, String.Format(p.LinkPath, GetPropertyValue(i, p.LinkKeyName).ToString())))
                    Else
                        myrow.AppendLine(p.GetFormatTableCell(PropValue))
                    End If
                Next
                myTableRows.Add(myrow.ToString())
            Next

            rptDataRows.DataSource = myTableRows
            rptDataRows.DataBind()
            If EnableCSV Then
                hfCSV.Value = GetCSV(myHeader, myRows)
            End If
        End If

    End Sub
    Public Function GetCSV(ByVal myheader As DisplayTableHeader, ByVal myRows As Object) As String Implements Icontrols_DisplayTable.GetCSV
        Dim myReturn As New StringBuilder
        Dim myrow As New StringBuilder
        Dim iRow As Integer = 1
        Try
            myrow = New StringBuilder
            For Each p In myheader.HeaderItems
                If iRow < myheader.HeaderItems.Count Then
                    myrow.Append(AddComma(p.Name) & ",")
                    iRow += 1
                Else
                    myrow.Append(AddComma(p.Name))
                    myrow.Append(Environment.NewLine)
                End If
            Next
            myReturn.Append(myrow.ToString)

            For Each i In myRows
                myrow = New StringBuilder
                iRow = 1
                For Each p In myheader.HeaderItems
                    If iRow < myheader.HeaderItems.Count Then
                        myrow.Append(AddComma(GetPropertyValue(i, p.Value)) & ",")
                        iRow += 1
                    Else
                        myrow.Append(AddComma(GetPropertyValue(i, p.Value)))
                        myrow.Append(Environment.NewLine)
                    End If
                Next
                myReturn.Append(myrow.ToString)
            Next
        Catch ex As Exception
            ApplicationLogging.ErrorLog(ex.ToString, "DisplayTable.ascx.GetSCV")
        End Try
        Return myReturn.ToString
    End Function
    Protected Sub lbGetCSV_Click(sender As Object, e As EventArgs)
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.ClearHeaders()
        HttpContext.Current.Response.ClearContent()
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=datasets.csv")
        HttpContext.Current.Response.ContentType = "text/csv"
        HttpContext.Current.Response.AddHeader("Pragma", "public")
        HttpContext.Current.Response.Write(hfCSV.Value)
        Response.Flush()
        Response.End()
    End Sub

End Class
