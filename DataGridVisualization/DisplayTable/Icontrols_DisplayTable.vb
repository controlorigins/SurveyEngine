Imports CODataCon.com.controlorigins.ws
Imports DataGridVisualization.ControlOriginsWS

Public Interface Icontrols_DisplayTable
    Sub BuildTableFromGrid(ByVal myHeaders As DisplayTableHeader, ByVal myRows As CO_DataGrid)
    Sub BuildTable(ByVal myRows As Object)
    Sub BuildTable(ByVal myHeaders As DisplayTableHeader, ByVal myRows As Object)
    Function GetCSV(ByVal myHeaders As DisplayTableHeader, ByVal myRows As Object) As String
    Property EnableCSV As Boolean
    Property TableHeader As DisplayTableHeader
End Interface

Public Interface Icontrols_CSV_DisplayTable
    Sub BuildDataTable(ByVal filePath As String, ByVal myDataGrid As CO_DataGrid)
End Interface

Public Interface Icontrols_Chart
    Sub BuildChart(ByVal Configuration As DataGridVisualization, ByRef SourceData As CO_DataGrid)
End Interface

Public Interface Icontrols_ChartConfiguration
    Sub PutChartConfiguration(ByVal Configuration As DataGridVisualization)
    Function GetChartConfiguration() As DataGridVisualization
End Interface
