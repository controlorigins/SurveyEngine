Imports System.Web.UI.DataVisualization
Imports System.Web.UI.DataVisualization.Charting
Imports System.Text
Imports WebProjectMechanics

'Public Class CO_SimpleChart

'    Property Title As String
'    Property ChartColumns As New ColumnColl
'    Property ChartRows As New RowColl
'    Property Width As Integer
'    Property Height As Integer
'    Property TypeChart As ChartType

'    Private Function GetChartType() As String
'        Dim output As String
'        Select Case TypeChart
'            Case ChartType.Area
'                output = "AreaChart"""
'            Case ChartType.Bar
'                output = "BarChart"
'            Case ChartType.Bubble
'                output = "BubbleChart"
'            Case ChartType.Line
'                output = "LineChart"
'            Case ChartType.Pie
'                output = "Pie"
'            Case ChartType.Scatter
'                output = "ScatterChart"
'            Case ChartType.Column
'                output = "ColumnChart"
'            Case Else
'                output = "BarChart"
'        End Select
'        Return output
'    End Function


'    Overrides Function tostring() As String
'        Return renderJS()
'    End Function


'    Public Function DemoData() As CO_SimpleChart
'        Dim mychart As New CO_SimpleChart
'        mychart.Title = "This is a chart title"
'        mychart.Width = 600
'        mychart.Height = 400
'        mychart.TypeChart = CO_SimpleChart.ChartType.Bar

'        mychart.ChartColumns.Add(New CO_SimpleChart.ChartColumn With {.DataType = "string", .Name = "Topping"})
'        mychart.ChartColumns.Add(New CO_SimpleChart.ChartColumn With {.DataType = "number", .Name = "Slices"})
'        mychart.ChartColumns.Add(New CO_SimpleChart.ChartColumn With {.DataType = "number", .Name = "Pies"})
'        mychart.ChartColumns.Add(New CO_SimpleChart.ChartColumn With {.DataType = "number", .Name = "Returns"})

'        mychart.ChartRows.Add(New CO_SimpleChart.ChartRow With {.Value = Split("4,15,1", ",").ToList, .name = "Mushrooms"})
'        mychart.ChartRows.Add(New CO_SimpleChart.ChartRow With {.Value = Split("23,14,3", ",").ToList, .name = "Onions"})
'        mychart.ChartRows.Add(New CO_SimpleChart.ChartRow With {.Value = Split("13,24,2", ",").ToList, .name = "Olives"})
'        mychart.ChartRows.Add(New CO_SimpleChart.ChartRow With {.Value = Split("32,44,1", ",").ToList, .name = "Zucchini"})
'        mychart.ChartRows.Add(New CO_SimpleChart.ChartRow With {.Value = Split("37,41,2", ",").ToList, .name = "Pepperoni"})
'        Return mychart
'    End Function



'    Public Function renderJS() As String
'        Dim output As New StringBuilder


'        output.AppendLine("<script type='text/javascript' src='https://www.google.com/jsapi'></script>")


'        ' start Scriptblock
'        output.AppendLine("<script type='text/javascript'>")

'        ' Load google Visualization and set callback 
'        output.AppendLine("google.load('visualization', '1.0', { 'packages': ['corechart'] });")
'        output.AppendLine("google.setOnLoadCallback(drawChart);")


'        ' start of function 
'        output.AppendLine("function drawChart() {")
'        ' Set up DataVar
'        output.AppendLine("var data = new google.visualization.arrayToDataTable([")
'        ' Chrat Add Columns to DataVar
'        output.AppendLine(Me.ChartColumns.renderJS)
'        ' Chart Add Rows to DataVar
'        output.AppendLine(Me.ChartRows.renderJS)

'        output.AppendLine("]);")

'        ' Chart options to DataVar
'        output.Append("var options = {")
'        output.AppendFormat("'title': '{0}','width': {1},'height': {2},'is3D': {3}", Title, Width, Height, True.ToString.ToLower)
'        output.AppendLine("};")



'        output.AppendFormat("var chart = new google.visualization.{0}(document.getElementById('chart_div'));", GetChartType)
'        output.Append("chart.draw(data, options);")

'        ' end of function
'        output.AppendLine("}")
'        ' end Script Block 
'        output.AppendLine("</script>")


'        ' chart render div tag
'        output.AppendLine("<div id='chart_div'></div>")
'        Return output.ToString

'    End Function





'    Public Class ChartColumn

'        Property DataType As String
'        Property Name As String

'    End Class

'    Public Class ColumnColl
'        Inherits List(Of ChartColumn)

'        Public Function renderJS() As String
'            Dim output As New StringBuilder
'            output.AppendLine("[")
'            For Each c In Me
'                output.AppendFormat("'{0}'", c.Name)
'                If Not c Is Me.Last Then output.Append(",")
'            Next
'            output.AppendLine("],")
'            Return output.ToString
'        End Function

'    End Class

'    Public Class ChartRow

'        Property name As String
'        Property Value As List(Of String)


'    End Class
'    Public Class RowColl
'        Inherits List(Of ChartRow)


'        Public Function renderJS() As String
'            Dim outstring As New StringBuilder

'            For Each r In Me
'                outstring.Append("['")
'                outstring.Append(r.name)
'                outstring.Append("' ,")
'                For Each v In r.Value
'                    outstring.AppendFormat("{0}", v)
'                    If Not v Is r.Value.Last Then outstring.Append(",")
'                Next
'                outstring.Append("]")
'                If Not r Is Me.Last Then outstring.AppendLine(",")
'            Next

'            Return outstring.ToString
'        End Function

'    End Class

'    Public Enum ChartType
'        Pie = 1
'        Bar = 2
'        Area = 3
'        Bubble = 4
'        Line = 5
'        Scatter = 6
'        Column = 7
'    End Enum

'End Class


Public Class SeriesData
    Inherits System.Collections.Generic.List(Of PointData)

    Property SeriesLabel As String
    Property SeriesName As String
    Property SeriesWeight As Double

    Public Function GetSeriesData() As Series
        Dim newSeries As New Series With {.Name = SeriesName, .label = SeriesLabel, .IsValueShownAsLabel = False}

        newSeries.LegendText = ShortenNameBy("-", SeriesName)

        For Each DP In Me
            Dim myPoint As New DataPoint With {.AxisLabel = DP.PointLabel, .YValues = {DP.PointValue}, .IsValueShownAsLabel = True}
            myPoint.Font = New System.Drawing.Font("Verdana", 9, Drawing.FontStyle.Bold)
            myPoint.LabelForeColor = Drawing.Color.Black
            myPoint.LabelBackColor = Drawing.Color.Transparent
            newSeries.Points.Add(myPoint)
        Next
        Return newSeries
    End Function

    Public Function ShortenNameBy(ByVal BreakString As String, ByVal myValue As String) As String
        Dim myTemp = myValue.Split(CChar(BreakString))
        Return myTemp(myTemp.Count - 1)
    End Function
End Class

Public Class PointData
    Property PointLabel As String
    Property PointValue As Double
    Property PointYaxisValue As Double
    Property pointXaxisValue As Double
End Class

Public Class DataGridColumn
    Public Property Name As String 
    Public Property Index As Integer
End Class


Public Class ChartMuncher
    Property ChartName As String
    Property ChartTitles As New List(Of String)
    Property mySeries As New List(Of SeriesData)
    Property ChartType As SeriesChartType
    Property ChartStyle As String
    '2D vs 3D
    Property ChartPalette As String
    Property ChartWidth As Int16
    Property ChartHeight As Int16

    Property DataSetName As String
    Property DataGrid As new wpm_DataGrid
    Property xColumn As new DataGridColumn
    Property xColumnSelected As New List(Of String)
    Property yColumn As new DataGridColumn
    Property yColumnSelected As New List(Of String)
    Property FilterColumn As new DataGridColumn
    Property FilterColumnSelected As New List(Of String)
    Property ValueColumn As new DataGridColumn
    Property AggregateFunction As String
    ' Valid values: Sum, Average, Count


    Public Sub ReturnChartObject(ByRef yourchart As Chart)
        yourchart.Legends.Clear()
        yourchart.Series.Clear()
        yourchart.ChartAreas.Clear()
        yourchart.Titles.Clear()

        For Each i In ChartTitles
            If Not String.IsNullOrEmpty(i) Then
                Dim myTitle As New Title
                myTitle.Text = i
                myTitle.Font = New System.Drawing.Font("Verdana", 12, Drawing.FontStyle.Bold)
                yourchart.Titles.Add(myTitle)
            End If
        Next
        With yourchart
            .ChartAreas.Add(New ChartArea With {.Name = ChartName, .BackColor = Drawing.Color.White})
            .ChartAreas(0).AxisX.Interval = 1
            For Each s In mySeries
                .Series.Add(s.GetSeriesData)
            Next
        End With
        yourchart.Legends.Add(New Legend With {.Name = "Master Legend"})
        yourchart.Width = ChartWidth
        yourchart.Height = ChartHeight
    End Sub

End Class
