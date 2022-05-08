Imports System.Text
Imports System.Web.UI.DataVisualization.Charting
Imports System.IO
Imports DataGridVisualization.ControlOriginsWS
Imports System.Web
Imports System.Collections.ObjectModel
Imports ControlOrigins.COUtility

Public Class DataGridVisualization
    Public Property Name As String
    Public Property ApplicationID As Integer
    Public Property DataSource As DataSetName
    Public Property ChartTitles As New List(Of String)
    Public Property AggregatorName As String
    Public Property rendererName As String
    Public Property ChartType As SeriesChartType
    Public Property ChartStyle As String
    Public Property ChartPalette As String
    Public Property ChartWidth As Int16
    Public Property ChartHeight As Int16
    Public Property BackgroundImage As String
    Public Property Cols As New ObservableCollection(Of GridColumn)
    Public Property Rows As New ObservableCollection(Of GridColumn)
    Public Property Vals As New ObservableCollection(Of GridColumn)
    Property FilterColumn As New GridColumn
    Public Property IncludeValues As New ObservableCollection(Of GridColumn)
    Public Property ExcluedValues As New ObservableCollection(Of GridColumn)
    Public Property SeriesDataList As New ObservableCollection(Of SeriesData)
    Public Property PrimaryFontColor As System.Drawing.Color
    Private ChartData As New CO_DataGrid
    Public ReadOnly Property ChartDataGrid As CO_DataGrid
        Get
            Return ChartData
        End Get
    End Property

    Public Sub ReturnChartObject(ByRef yourchart As Chart, ByRef SourceData As CO_DataGrid)
        '  Set Up the Chart Data from the SourceData ***********************************
        Dim myRows As New List(Of GridRow)
        Dim myCols As New List(Of GridColumn)

        Try
            FilterColumn = New GridColumn
            FilterColumn.ColumnValues = (New List(Of String)).ToArray
            For Each myCol As GridColumn In SourceData.GridColumns
                myCols.Add(myCol)
            Next
            '  Get The Column Information Based On Column Name    **************************
            For Each myCol As GridColumn In SourceData.GridColumns
                If FilterColumn.DisplayName = myCol.DisplayName Then
                    FilterColumn = myCol
                End If
                If Cols(0).DisplayName = myCol.DisplayName Then
                    Cols(0) = myCol
                End If
                If Rows(0).DisplayName = myCol.DisplayName Then
                    Rows(0) = myCol
                End If
                If Vals(0).DisplayName = myCol.DisplayName Then
                    Vals(0) = myCol
                End If
            Next
            For Each myCol As GridColumn In IncludeValues
                If myCol.DisplayName = Cols(0).DisplayName Then
                    Cols(0).ColumnValues = myCol.ColumnValues
                ElseIf myCol.DisplayName = Rows(0).DisplayName Then
                    Rows(0).ColumnValues = myCol.ColumnValues
                Else
                    FilterColumn.DisplayName = myCol.DisplayName
                    FilterColumn.ColumnValues = myCol.ColumnValues
                End If
            Next
            For i As Integer = 0 To SourceData.GridColumns.Count - 1
                If SourceData.GridColumns(i).DisplayName = FilterColumn.DisplayName Then
                    FilterColumn.Index = i
                End If
            Next
            '  Get The Rows That Match Configruation                        **************************
            For Each myRow As GridRow In SourceData.GridRows
                If FilterColumn.ColumnValues.Count > 0 Then
                    If FilterColumn.ColumnValues.Contains(myRow.Value(FilterColumn.Index)) Then
                        If Cols(0).ColumnValues.Contains(myRow.Value(Cols(0).Index)) Then
                            If Rows(0).ColumnValues.Contains(myRow.Value(Rows(0).Index)) Then
                                If wpm_GetDBDouble(myRow.Value(Vals(0).Index)) > 0 Or AggregatorName = "Count" Then
                                    myRows.Add(myRow)
                                End If
                            End If
                        End If
                    End If
                Else
                    If Cols(0).ColumnValues.Count > 0 Then
                        If Cols(0).ColumnValues.Contains(myRow.Value(Cols(0).Index)) AndAlso
                             Rows(0).ColumnValues.Contains(myRow.Value(Rows(0).Index)) AndAlso
                              wpm_GetDBDouble(myRow.Value(Vals(0).Index)) > 0 Or AggregatorName = "Count" Then
                            myRows.Add(myRow)
                        End If
                    Else
                        If Rows(0).ColumnValues.Count > 0 Then
                            If Rows(0).ColumnValues.Contains(myRow.Value(Rows(0).Index)) AndAlso
                                  wpm_GetDBDouble(myRow.Value(Vals(0).Index)) > 0 Or AggregatorName = "Count" Then
                                myRows.Add(myRow)
                            End If
                        Else
                            If wpm_GetDBDouble(myRow.Value(Vals(0).Index)) > 0 Or AggregatorName = "Count" Then
                                myRows.Add(myRow)
                            End If
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            ApplicationLogging.ErrorLog("DataGridVisualization.ReturnChartObject - Getting the Data", ex.ToString)
        End Try

        ChartData.GridColumns = myCols.ToArray()
        ChartData.GridRows = myRows.ToArray()


        If ChartData.GridRows.Count > 0 Then
            For Each RowColValue As String In Rows(0).ColumnValues
                Dim RowColValueGridRows As List(Of GridRow) = (From s In
                                                          (From i In ChartData.GridRows
                                                           Where Rows(0).ColumnValues.Contains(i.Value(Rows(0).Index))).ToList()
                                                       Where s.Value(Rows(0).Index) = RowColValue).ToList()
                Dim AxisLabel As String = String.Empty
                Dim newSeriesData As New SeriesData
                Dim iRunningScore As Double
                Dim iCount As Integer
                For Each Axis As String In Cols(0).ColumnValues
                    If Axis = String.Empty Then
                        AxisLabel = "No Value"
                    Else
                        AxisLabel = Axis
                    End If

                    iCount = 0
                    iRunningScore = 0

                    For Each AxisData As GridRow In (From i In RowColValueGridRows Where i.Value(Cols(0).Index) = Axis).ToList
                        iCount += 1
                        iRunningScore = iRunningScore + wpm_GetDBDouble(AxisData.Value(Vals(0).Index))
                    Next

                    If iCount > 0 Then
                        Select Case AggregatorName
                            Case "Sum"
                                newSeriesData.Add(New PointData With {.PointLabel = AxisLabel,
                                                                      .PointValue = Math.Round(iRunningScore, 2)})
                            Case "Average"
                                If iCount > 0 Then
                                    newSeriesData.Add(New PointData With {.PointLabel = AxisLabel,
                                                                          .PointValue = Math.Round(iRunningScore / iCount, 2)})
                                Else
                                    newSeriesData.Add(New PointData With {.PointLabel = AxisLabel,
                                                                          .PointValue = 0})
                                End If
                            Case "Count"
                                newSeriesData.Add(New PointData With {.PointLabel = AxisLabel,
                                                                      .PointValue = iCount})
                            Case Else
                                newSeriesData.Add(New PointData With {.PointLabel = AxisLabel,
                                                                      .PointValue = iRunningScore})
                        End Select
                    End If
                Next

                If RowColValue = String.Empty Then
                    newSeriesData.SeriesName = "Unknown"
                Else
                    newSeriesData.SeriesName = RowColValue
                End If
                If newSeriesData.Count > 0 Then
                    SeriesDataList.Add(newSeriesData)
                End If
            Next

            If PrimaryFontColor.Name = "0" Then
                PrimaryFontColor = Drawing.Color.Black
            End If

            With yourchart
                .Legends.Clear()
                .Series.Clear()
                .ChartAreas.Clear()
                .Titles.Clear()

                ' Bacground Image (Chart Background)
                If Not String.IsNullOrEmpty(BackgroundImage) Then
                    .BackImage = BackgroundImage
                    .BackImageAlignment = ChartImageAlignmentStyle.Center
                    .BackImageTransparentColor = Drawing.Color.Transparent
                    .BackImageWrapMode = ChartImageWrapMode.Scaled
                End If

                ' Chart Titles
                If ChartTitles.Count = 0 Then
                    ChartTitles.Add(Name)
                End If
                For i As Integer = 0 To ChartTitles.Count - 1
                    If Not String.IsNullOrEmpty(ChartTitles(i)) Then
                        If i = 0 Then
                            .Titles.Add(New Title() With {.Text = ChartTitles(i),
                                                          .Font = New System.Drawing.Font("Verdana", 16, Drawing.FontStyle.Bold),
                                                          .ForeColor = PrimaryFontColor})
                        Else
                            .Titles.Add(New Title() With {.Text = ChartTitles(i),
                                                          .Font = New System.Drawing.Font("Verdana", 12, Drawing.FontStyle.Italic),
                                                          .ForeColor = PrimaryFontColor})
                        End If
                    End If
                Next

                ' Create  a Single Chart Area - Set to Transparent as we 
                .ChartAreas.Add(New ChartArea With {.Name = Name,
                                                    .BackColor = Drawing.Color.Transparent
                                                    }
                                                )


                With .ChartAreas(0).AxisX
                    .Interval = 1
                    .Title = Cols(0).DisplayName
                    .TitleFont = New System.Drawing.Font("Verdana", 14, Drawing.FontStyle.Bold)
                    .TitleForeColor = PrimaryFontColor
                    .TextOrientation = TextOrientation.Auto
                    .IsReversed = True
                    .IsLabelAutoFit = True
                    .IsStartedFromZero = True
                    .LabelAutoFitMinFontSize = 8
                    .LabelAutoFitMaxFontSize = 16
                    .LabelStyle = New LabelStyle With {.Font = New System.Drawing.Font("Verdana", 50, Drawing.FontStyle.Bold)
                                                       }
                End With

                For Each s As SeriesData In SeriesDataList
                    Try
                        .Series.Add(s.GetSeriesData)
                    Catch
                        ApplicationLogging.ErrorLog("ChartConfiguration.ReturnChartObject", "Failed to add series data")
                    End Try
                Next

                Dim ChartLegend As New System.Web.UI.DataVisualization.Charting.Legend
                With ChartLegend
                    .Alignment = Drawing.StringAlignment.Center
                    .Name = "Legend"
                    .Docking = DataVisualization.Charting.Docking.Bottom
                    .TableStyle = DataVisualization.Charting.LegendTableStyle.Wide
                    '  .Font = New System.Drawing.Font("Verdana", 12, Drawing.FontStyle.Bold)
                    .IsTextAutoFit = True
                    .LegendStyle = DataVisualization.Charting.LegendStyle.Table
                    .TitleAlignment = Drawing.StringAlignment.Center
                    .BackColor = Drawing.Color.Transparent
                    .ForeColor = PrimaryFontColor
                    .BorderColor = Drawing.Color.Black
                    .BorderDashStyle = ChartDashStyle.Solid
                    .BorderWidth = 1
                    .Title = Rows(0).DisplayName
                End With

                .Legends.Add(ChartLegend)

                If ChartWidth > 0 Then
                    .Width = ChartWidth
                End If
                If ChartHeight > 0 Then
                    .Height = ChartHeight
                End If

                Try
                    If ChartPalette = String.Empty Then
                        ChartPalette = CStr(ChartColorPalette.BrightPastel)
                    End If
                    .Palette = CType(ChartPalette, ChartColorPalette)
                Catch ex As Exception
                    .Palette = ChartColorPalette.BrightPastel
                End Try

                Try
                    Select Case ChartType
                        Case DataVisualization.Charting.SeriesChartType.StackedBar
                            ChartStyle = "2D"
                        Case Else
                            ' Do Nothing
                    End Select

                    If ChartStyle = "3D" Then
                        .ChartAreas(0).Area3DStyle.Enable3D = True
                    Else
                        .ChartAreas(0).Area3DStyle.Enable3D = False
                    End If

                    For Each s As Series In .Series
                        s.ChartType = ChartType
                    Next

                Catch ex As Exception
                    ApplicationLogging.ErrorLog("ChartConfiguration.ReturnChartObject", ex.ToString)
                End Try


                Dim mytw As System.IO.TextWriter
                mytw = New StreamWriter(HttpContext.Current.Server.MapPath("~/App_Data/log/TestChartConfig.txt"))
                Using myWriter As New HtmlTextWriter(mytw)
                    Try
                        yourchart.RenderControl(myWriter)
                    Catch ex As Exception
                        ApplicationLogging.ErrorLog("ChartConfiguration.ReturnChartObject-Errror on Test Render Chart", ex.ToString)
                        yourchart.Palette = ChartColorPalette.None
                        For Each s As Series In yourchart.Series
                            s.ChartType = SeriesChartType.Column
                        Next
                        For Each area As ChartArea In yourchart.ChartAreas
                            area.Area3DStyle.Enable3D = False
                        Next
                    End Try
                End Using
                mytw.Close()
                mytw.Dispose()
            End With
        End If
    End Sub
    Public Function GetPivotParm() As String
        Return (String.Format("{0} {1} {2} {3} {4} {5} {6} ",
                       GetColumnNameString(Cols, "cols"),
                       GetColumnNameString(Rows, "rows"),
                       GetColumnNameString(Vals, "vals"),
                       GetColumnValueString(ExcluedValues, "exclusions"),
                       GetColumnValueString(IncludeValues, "inclusions"),
                       GetStringItem(AggregatorName, "aggregatorName"),
                       GetStringItem(rendererName, "rendererName")
                       )) & "derivedAttributes: {},"
    End Function


    Private Shared Function GetStringItem(ByVal myItem As String, ByVal myName As String) As String
        Dim mySB As New StringBuilder
        If Not String.IsNullOrEmpty(myItem) Then
            mySB.Append(String.Format("{0}:", myName))
            mySB.Append(String.Format("""{0}""", myItem))
            mySB.AppendLine(",")
        End If
        Return mySB.ToString()

    End Function
    Private Shared Function GetColumnNameString(ByVal myStringList As ObservableCollection(Of GridColumn), ByVal sName As String) As String
        Dim mySB As New StringBuilder
        If myStringList.Count > 0 Then
            mySB.Append(String.Format("{0}:[", sName))
            For Each myVal As GridColumn In myStringList
                If myVal.SourceName = myStringList.Last.SourceName Then
                    mySB.Append(String.Format("""{0}""", myVal.SourceName))
                Else
                    mySB.Append(String.Format("""{0}"",", myVal.SourceName))
                End If
            Next
            mySB.AppendLine("],")
        End If
        Return mySB.ToString()
    End Function
    Private Shared Function GetColumnValueString(ByVal myValueList As ObservableCollection(Of GridColumn), ByVal sName As String) As String
        Dim mySB As New StringBuilder
        If myValueList.Count > 0 Then
            mySB.Append(String.Format("""{0}"":", sName.Replace("""", String.Empty).Trim) & "{")
            For Each myVal As GridColumn In myValueList
                mySB.Append(String.Format("""{0}"":[", myVal.SourceName.Replace("""", String.Empty).Trim))
                For Each myItem As String In myVal.ColumnValues
                    mySB.Append(String.Format("""{0}"",", myItem.Replace("""", String.Empty).Trim))
                Next
                mySB.AppendLine("],")
            Next
            mySB.AppendLine("},")
        End If
        Return mySB.ToString()
    End Function

End Class
