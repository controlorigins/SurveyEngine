'Imports System.IO
'Imports DataGridVisualization
'Imports DataGridVisualization.ControlOriginsWS
'Imports System.Data
'Imports CODataCon
'Imports ControlOrigins.COUtility

'Public Class dawpmPage
'    Inherits PortalPageBase

'    Public Property CsvDataGrid As New CO_DataGrid
'    Public ReadOnly Property CsvFileList As List(Of CsvFile)
'        Get
'            Dim mylist As New List(Of CsvFile)

'            Dim filePaths() As String = Directory.GetFiles(Server.MapPath("~/CSV/"))
'            For Each filePath As String In filePaths
'                mylist.Add(New CsvFile With {.FileName = IO.Path.GetFileName(filePath), .FileDate = File.GetLastWriteTime(filePath), .FileSize = New FileInfo(filePath).Length})
'            Next
'            Return mylist
'        End Get
'    End Property

'    Public Class CsvFile
'        Property FileName As String
'        Property FileDate As Date
'        Property FileSize As String
'    End Class

'    Public Function GetCsvFileList() As List(Of CsvFile)
'        Return CsvFileList
'    End Function

'    Public Property PivotParmList As DataGridVisualizationList
'        Get
'            Return New DataGridVisualizationList(GetProperty(AppInfo.ApplicationID,"PivotParmList"))
'        End Get
'        Set(value As DataGridVisualizationList)
'            SetProperty(AppInfo.ApplicationID,"PivotParmList",value.XMLString)
'        End Set
'    End Property

'    Public Property ActiveName As String
'        Get
'            Return ControlOrigins.COUtility.Utility.GetDBString(Session("ActiveName"))
'        End Get
'        Set(value As String)
'            Session("ActiveName") = ControlOrigins.COUtility.Utility.GetDBString(value)
'        End Set
'    End Property

'    Public ReadOnly Property RootPath As String
'        Get
'            Dim appUrl = HttpRuntime.AppDomainAppVirtualPath
'            If HttpRuntime.AppDomainAppVirtualPath <> "/" Then
'                appUrl = appUrl & "/"
'            End If
'            Dim baseUrl = String.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, appUrl)
'            Return baseUrl
'        End Get
'    End Property

'    Public ReadOnly Property ApplicationUrl As String
'        Get
'            If "/" = HttpRuntime.AppDomainAppVirtualPath Then
'                Return "/"
'            Else
'                Return HttpRuntime.AppDomainAppVirtualPath & "/"
'            End If
'        End Get
'    End Property


'    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

'    End Sub
'    Public Function GetHttpProperty(ByVal myProperty As String, ByVal curValue As String) As String
'        Dim myValue As String = ""
'        If Len(HttpContext.Current.Request.QueryString(myProperty)) > 0 Then
'            myValue = HttpContext.Current.Request.QueryString(myProperty).ToString
'        ElseIf Len(HttpContext.Current.Request.Form.Item(myProperty)) > 0 Then
'            myValue = HttpContext.Current.Request.Form.Item(myProperty).ToString
'        Else
'            If curValue Is Nothing Then
'                myValue = String.Empty
'            Else
'                myValue = curValue
'            End If
'        End If
'        Return myValue
'    End Function

'    Public Shared Function LoadFromCsvFile(ByVal CsvFilePath As String) As CO_DataGrid
'        Dim myDataGrid As New CO_DataGrid
'        Dim myGridColumns = New List(Of GridColumn)
'        Dim myGridRows = New List(Of GridRow)
'        Using parser = New GenericParsing.GenericParserAdapter()
'            parser.SetDataSource(CsvFilePath)
'            parser.ColumnDelimiter = ","
'            parser.FirstRowHasHeader = True
'            parser.SkipStartingDataRows = 10
'            parser.MaxBufferSize = 4096
'            parser.MaxRows = 50000
'            parser.TextQualifier = """"
'            Dim myDT = parser.GetDataTable()
'            myGridColumns.Clear()
'            myGridRows.Clear()
'            For ColIndex = 0 To myDT.Columns.Count() - 1
'                Dim newValues As New List(Of String)
'                Dim newCol As New GridColumn With {.DisplayName = myDT.Columns(ColIndex).ColumnName,
'                                                    .SourceName = myDT.Columns(ColIndex).ColumnName,
'                                                    .Index = ColIndex,
'                                                    .MinValue = "ZZZ",
'                                                    .MaxValue = "000",
'                                                    .UniqueValues = 0,
'                                                    .MostCommon = String.Empty,
'                                                    .LeastCommon = String.Empty}


'                For Each myVal As String In (From i In myDT.AsEnumerable() Select i(newCol.Index) Distinct).ToArray()
'                    newValues.Add(myVal)
'                Next
'                newCol.ColumnValues = newValues.ToArray()
'                myGridColumns.Add(newCol)
'            Next
'            For Each myRow As DataRow In myDT.Rows
'                Try
'                    Dim myDataRow As New GridRow With {.name = String.Empty}
'                    Dim newValues As New List(Of String)
'                    For Each myCol As GridColumn In myDataGrid.GridColumns
'                        If String.IsNullOrEmpty(myCol.MaxValue) Then
'                            myCol.MaxValue = myRow.Item(myCol.SourceName)
'                            myCol.MinValue = myRow.Item(myCol.SourceName)
'                        End If
'                        '  myCol.UpdateDictionary(myRow.Item(myCol.SourceName))
'                        If myCol.MinValue > myRow.Item(myCol.SourceName) Then
'                            myCol.MinValue = myRow.Item(myCol.SourceName)
'                        End If
'                        If myCol.MaxValue < myRow.Item(myCol.SourceName) Then
'                            myCol.MaxValue = myRow.Item(myCol.SourceName)
'                        End If
'                        If String.IsNullOrEmpty(myRow.Item(myCol.SourceName)) Or IsNumeric(myRow.Item(myCol.SourceName)) Then
'                            myCol.ColumnDisplayFormat = DataGridVisualization.ControlOriginsWS.DisplayFormat.Number
'                        Else
'                            myCol.ColumnDisplayFormat = DataGridVisualization.ControlOriginsWS.DisplayFormat.Text
'                        End If
'                        myCol.UniqueValues = myCol.ColumnValues.Count
'                        newValues.Add(myRow.Item(myCol.SourceName))
'                    Next
'                    myDataRow.Value = newValues.ToArray()
'                    myGridRows.Add(myDataRow)
'                Catch ex As Exception
'                    ApplicationLogging.ErrorLog(ex.ToString, "get from CSV")
'                End Try
'            Next
'        End Using
'        For Each myCol In myDataGrid.GridColumns
'            ' myCol.SetCommonValues()
'        Next
'        Return myDataGrid
'    End Function


'End Class
