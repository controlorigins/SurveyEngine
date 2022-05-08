Imports System.Web.UI.DataVisualization.Charting
Public Class SeriesData
    Inherits List(Of PointData)
    Property SeriesLabel As String
    Property SeriesName As String
    ' Property SeriesWeight As Double
    Public Function GetSeriesData() As Series
        Dim newSeries As New Series() With {.Name = SeriesName, 
                                            .label = SeriesLabel, 
                                            .IsValueShownAsLabel = True, 
                                            .legendText =  ShortenNameBy("-", SeriesName),
                                            .PostBackValue = "?Series=" & SeriesName,
                                            .ToolTip = SeriesName}
        For Each DP As PointData In Me
            Dim myPoint As New DataPoint() With {.AxisLabel = DP.PointLabel, 
                                                 .ToolTip = String.Format("{0}-{1}-{2}", SeriesName, DP.PointLabel, DP.PointValue),
                                                 .PostBackValue = String.Format("?Series={0};Axis={1};Value={2}", SeriesName, DP.PointLabel, DP.PointValue),
                                                 .YValues = {DP.PointValue}, 
                                                 .IsValueShownAsLabel = True, 
                                                 .Font = New System.Drawing.Font("Verdana", 8, Drawing.FontStyle.Bold), 
                                                 .LabelForeColor = Drawing.Color.Black, 
                                                 .LabelBackColor = Drawing.Color.Transparent}
            newSeries.Points.Add(myPoint)
        Next
        Return newSeries
    End Function
    Public Function ShortenNameBy(ByVal BreakString As String, ByVal myValue As String) As String
        Dim myTemp as String()= myValue.Split(CChar(BreakString))
        Return myTemp(myTemp.Count - 1)
    End Function
End Class

Public Enum DataSetName
    ApplicationDetail
    ApplicationList
    ApplicationSummary
    QuestionList
    SurveyGroupSummary
    SurveyList
    SurveySummary
    UserList
End Enum
