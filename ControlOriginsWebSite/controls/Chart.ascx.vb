Imports DataGridVisualization
Imports ControlOrigins.COUtility

Partial Class controls_Chart
    Inherits System.Web.UI.UserControl
    Implements Icontrols_Chart

    Public Sub BuildChart(Configuration As DataGridVisualization.DataGridVisualization, ByRef SourceData As ControlOriginsWS.CO_DataGrid) Implements Icontrols_Chart.BuildChart
        Try
            Configuration.ReturnChartObject(myChart, SourceData)
            myChart.ID = String.Format("{0}{1}", Configuration.Name, Configuration.ApplicationID).Replace(".", String.Empty)
        Catch ex As Exception
            ApplicationLogging.ErrorLog("Chart.BuildChart", ex.ToString())
        End Try
    End Sub
End Class
