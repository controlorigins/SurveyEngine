Imports System.Web.UI.DataVisualization.Charting
Imports DataGridVisualization.ControlOriginsWS
Imports DataGridVisualization
Imports CODataCon


Public Class Co_Apps_SurveyApp_controls_SurveyAppChart
    Inherits SurveyUserControlBase

    Private _MyData As CO_DataGrid
    Private iAxisKey As Integer
    Private iSeriesKey As Integer
    Private iFilterKey As Integer
    Private iStatusNM As Integer
    Private iChartValueKey As Integer

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If ddlDataSet.SelectedValue = "Group" Then
            SetGroupData()
        Else
            SetData()
        End If
        If IsPostBack Then
            cmd_SetChartConfig.CssClass = "btn btn-warning"
            pnlChartAndData.Visible = False
        Else
            hfSeries.Value = ""
            hfAxis.Value = ""
            hfFilter.Value = ""

            If _MyData.GridRows.Count > 0 Then
                ddlValue.DataSource = (From i In _MyData.GridColumns Where i.DataType.Contains("double") Or i.DataType.Contains("integer") And Not i.DisplayName.EndsWith("ID") Select i.DisplayName).ToList()
                ddlValue.DataBind()
                ddlValue.SelectedValue = "SurveyResponseScore"

                ddlAxis.DataSource = (From i In _MyData.GridColumns Where i.DataType.Contains("string") Or i.SourceName = "Context" Select i.DisplayName).ToList()
                ddlAxis.DataBind()
                Try
                    ddlAxis.SelectedValue = (From i In _MyData.GridColumns Where i.SourceName = "Context" Select i.DisplayName).First()
                Catch
                End Try

                ddlSeries.DataSource = (From i In _MyData.GridColumns Where i.DataType.Contains("string") Or i.SourceName = "Context" Select i.DisplayName).ToList()
                ddlSeries.DataBind()
                ddlSeries.SelectedValue = "SurveyNM"

                ddlFilter.DataSource = (From i In _MyData.GridColumns Where i.DataType.Contains("string") Or i.SourceName = "Context" Select i.DisplayName).ToList()
                ddlFilter.DataBind()
                ddlFilter.SelectedValue = "SurveyNM"

            End If
            ddlChartType.DataSource = CType([Enum].GetNames(GetType(SeriesChartType)), String())
            ddlChartType.DataBind()
            ddlChartType.SelectedValue = "StackedBar"

            ddlChartPalette.DataSource = CType([Enum].GetNames(GetType(ChartColorPalette)), String())
            ddlChartPalette.DataBind()
            ddlChartPalette.SelectedValue = "BrightPastel"

            ddlCalc.Items.Clear()
            ddlCalc.Items.Add(New ListItem With {.Text = "Average", .Value = "Average", .Selected = True})
            ddlCalc.Items.Add(New ListItem With {.Text = "Sum", .Value = "Sum"})
            ddlCalc.Items.Add(New ListItem With {.Text = "Count", .Value = "Count"})
            DrawChart()
        End If

    End Sub

    Private Sub ProcessChartConfig()
        Dim MyChartMuncher As New DataGridVisualization.DataGridVisualization
        MyChartMuncher.ChartTitles.Add(tbTitle.Text)
        MyChartMuncher.ChartTitles.Add(tbSubTitle.Text)
        MyChartMuncher.ChartWidth = 800
        MyChartMuncher.ChartHeight = 400




        Dim ChartRows As New List(Of GridRow)
        Dim ChartData As New CO_DataGrid
        ChartData.GridColumns = (From i In _MyData.GridColumns).ToArray

        For Each myRow In _MyData.GridRows
            If FilterList.Contains(myRow.Value(iFilterKey)) AndAlso
                AxisList.Contains(myRow.Value(iAxisKey)) AndAlso
                 SeriesList.Contains(myRow.Value(iSeriesKey)) AndAlso
                  AppUtility.GetDBDouble(myRow.Value(iChartValueKey)) > 0 Then
                ChartRows.Add(myRow)
            End If
        Next

        ChartData.GridRows = ChartRows.ToArray

        If ChartRows.Count = 0 Then
            cmd_SetChartConfig.CssClass = "btn btn-warning"
            pnlChartAndData.Visible = False

        Else
            cmd_SetChartConfig.CssClass = "btn btn-primary"
            pnlChartAndData.Visible = True

            ChartData.Title = AppInfo.ApplicationNM

            For Each thisSeriesItem In SeriesList
                Dim thisSeriesData = (From s In (From i In ChartData.GridRows Where SeriesList.Contains(i.Value(iSeriesKey))).ToList() Where s.Value(iSeriesKey) = thisSeriesItem)
                Dim AxisLabel As String = String.Empty

                Dim newSeriesData As New DataGridVisualization.SeriesData
                Dim iRunningScore As Double
                Dim iCount As Integer
                newSeriesData = New DataGridVisualization.SeriesData
                For Each Axis In AxisList
                    If Axis = String.Empty Then
                        AxisLabel = "Not Answered"
                    Else
                        AxisLabel = Axis
                    End If
                    iCount = 0
                    iRunningScore = 0
                    For Each AxisData In (From i In thisSeriesData Where i.Value(iAxisKey) = Axis).ToList


                        iCount += 1
                        iRunningScore = iRunningScore + AppUtility.GetDBDouble(AxisData.Value(iChartValueKey))
                    Next
                    If iCount > 0 Then
                        Select Case ddlCalc.SelectedValue
                            Case "Sum"
                                newSeriesData.Add(New DataGridVisualization.PointData With {.PointLabel = AxisLabel, .PointValue = Math.Round(iRunningScore, 2)})
                            Case "Average"
                                If iCount > 0 Then
                                    newSeriesData.Add(New DataGridVisualization.PointData With {.PointLabel = AxisLabel, .PointValue = Math.Round(iRunningScore / iCount, 2)})
                                Else
                                    newSeriesData.Add(New DataGridVisualization.PointData With {.PointLabel = AxisLabel, .PointValue = 0})
                                End If
                            Case "Count"
                                newSeriesData.Add(New DataGridVisualization.PointData With {.PointLabel = AxisLabel, .PointValue = iCount})
                            Case Else
                                newSeriesData.Add(New DataGridVisualization.PointData With {.PointLabel = AxisLabel, .PointValue = iRunningScore})
                        End Select

                    End If
                Next
                If thisSeriesItem = String.Empty Then
                    newSeriesData.SeriesName = "Unknown"
                Else
                    newSeriesData.SeriesName = thisSeriesItem
                End If
                If newSeriesData.Count > 0 Then
                    MyChartMuncher.SeriesDataList.Add(newSeriesData)
                End If
            Next

            MyChartMuncher.ReturnChartObject(myChart, New CO_DataGrid)

            Try
                If ddlChartStyle.SelectedIndex > 0 Then
                    myChart.ChartAreas(0).Area3DStyle.Enable3D = True
                Else
                    myChart.ChartAreas(0).Area3DStyle.Enable3D = False
                End If
                For Each s As Series In myChart.Series
                    s.ChartType = CType(ddlChartType.SelectedIndex, SeriesChartType)
                Next
                myChart.Palette = CType(ddlChartPalette.SelectedIndex, ChartColorPalette)
            Catch ex As Exception
                ControlOrigins.COUtility.ApplicationLogging.ErrorLog("DevChart.UpdateChart", ex.ToString)
            End Try
            Dim myHeaders = (From i In MyChartMuncher.SeriesDataList(0) Select i).ToList()
            rptChartDataHeader.DataSource = myHeaders
            rptChartDataHeader.DataBind()
            rptChartData.DataSource = MyChartMuncher.SeriesDataList
            rptChartData.DataBind()
            Dim ChartDataHeader As New DataGridVisualization.DisplayTableHeader
            ChartDataHeader.AddHeaderItem(ddlAxis.SelectedValue, ddlAxis.SelectedValue)
            ChartDataHeader.AddHeaderItem(ddlFilter.SelectedValue, ddlFilter.SelectedValue)
            ChartDataHeader.AddHeaderItem(ddlSeries.SelectedValue, ddlSeries.SelectedValue)
            ChartDataHeader.AddHeaderItem(ddlValue.SelectedValue, ddlValue.SelectedValue)
            dtList.BuildTableFromGrid(ChartDataHeader, ChartData)
        End If
    End Sub

    Public Function GetIconSpan(ByVal myScore As Double) As String

        Dim myFormat = "<span><img class='img-responsive' style='max-width: 50px;display:inline' title='Score:{0}' alt='{0}' src='/images/Balls/{1}'><span class='badge' style='vertical-align:bottom;'>{0}</span></span>"
        Dim myGlyf As String = String.Empty
        Select Case myScore
            Case Is <= 1
                myGlyf = String.Format(myFormat, myScore, "harvey_balls_red_01.png")
            Case Is <= 2
                myGlyf = String.Format(myFormat, myScore, "harvey_balls_red_02.png")
            Case Is <= 3
                myGlyf = String.Format(myFormat, myScore, "harvey_balls_color_03.png")
            Case Is <= 4
                myGlyf = String.Format(myFormat, myScore, "harvey_balls_green_04.png")
            Case Else
                myGlyf = String.Format(myFormat, myScore, "harvey_balls_green_05.png")
        End Select
        Return myGlyf
    End Function


    Public Sub SetData(ByRef MyData As CO_DataGrid)
        Session(MyDataName) = MyData
        _MyData = Session(MyDataName)
    End Sub
    Public Sub SetData()
        If Session(MyDataName) Is Nothing Then
            Dim myCon As New DataGridVisualization.DataController()
            If CurUser.RoleCD.ToLower = "admin" Then
                Session(MyDataName) = myCon.GetSurveyResponseSummary(CurUser.ApplicationID, 10000000)
            Else
                Session(MyDataName) = myCon.GetSurveyResponseSummary(CurUser.ApplicationID, CurUser.ApplicationUserID)
            End If
            _MyData = Session(MyDataName)
        Else
            _MyData = Session(MyDataName)
        End If
    End Sub

    Public Sub SetGroupData()
        If Session(MyGroupDataName) Is Nothing Then
            Dim myCon As New DataGridVisualization.DataController()
            If CurUser.RoleCD.ToLower = "admin" Then
                Session(MyGroupDataName) = myCon.GetSurveyResponseGroupSummary(CurUser.ApplicationID, 1000000)
            Else
                Session(MyGroupDataName) = myCon.GetSurveyResponseGroupSummary(CurUser.ApplicationID, CurUser.ApplicationUserID)
            End If
            _MyData = Session(MyGroupDataName)
        Else
            _MyData = Session(MyGroupDataName)
        End If
    End Sub



    Public Sub DrawChart()
        iAxisKey = 0
        iSeriesKey = 0
        iChartValueKey = 0
        iFilterKey = 0

        For i = 0 To _MyData.GridColumns.Count - 1
            If _MyData.GridColumns(i).DisplayName = ddlAxis.SelectedValue Then
                iAxisKey = i
            End If
            If _MyData.GridColumns(i).DisplayName = ddlSeries.SelectedValue Then
                iSeriesKey = i
            End If
            If _MyData.GridColumns(i).DisplayName = ddlValue.SelectedValue Then
                iChartValueKey = i
            End If
            If _MyData.GridColumns(i).DisplayName = ddlFilter.SelectedValue Then
                iFilterKey = i
            End If
            If iAxisKey > 0 AndAlso iSeriesKey > 0 AndAlso iChartValueKey > 0 AndAlso iFilterKey > 0 Then
                Exit For
            End If
        Next

        If hfFilter.Value <> ddlFilter.SelectedValue Then
            hfFilter.Value = ddlFilter.SelectedValue
            cbFilter.Items.Clear()
            Dim myFilterList = (From i In _MyData.GridRows Where i.Value(iFilterKey) <> String.Empty Select i.Value(iFilterKey) Distinct).ToList()
            cbFilter.DataSource = myFilterList
            cbFilter.DataBind()
            For i = 0 To cbFilter.Items.Count - 1
                cbFilter.Items(i).Selected = True
            Next

        End If


        If hfSeries.Value <> ddlSeries.SelectedValue Then
            hfSeries.Value = ddlSeries.SelectedValue
            cbSeries.Items.Clear()
            Dim mySeriesList = (From i In _MyData.GridRows Where i.Value(iSeriesKey) <> String.Empty Select i.Value(iSeriesKey) Distinct).ToList()
            cbSeries.DataSource = mySeriesList
            cbSeries.DataBind()
            For i = 0 To cbSeries.Items.Count - 1
                cbSeries.Items(i).Selected = True
            Next
        End If

        If hfAxis.Value <> ddlAxis.SelectedValue Then
            hfAxis.Value = ddlAxis.SelectedValue
            cbAxis.Items.Clear()
            Dim myAxisList = (From i In _MyData.GridRows Where i.Value(iAxisKey) <> String.Empty Select i.Value(iAxisKey) Distinct).ToList()
            cbAxis.DataSource = myAxisList
            cbAxis.DataBind()
            For i = 0 To cbAxis.Items.Count - 1
                cbAxis.Items(i).Selected = True
            Next
        End If
        ProcessChartConfig()
    End Sub


    Protected Sub cmd_SetChartConfig_Click(sender As Object, e As EventArgs)
        DrawChart()
    End Sub

    Protected Sub ddlDataSet_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlDataSet.SelectedValue = "Group" Then
            SetGroupData()
        Else
            SetData()
        End If
        If ddlDataSet.SelectedValue <> hfDataSet.Value Then
            hfDataSet.Value = ddlDataSet.SelectedValue
            hfSeries.Value = ""
            hfAxis.Value = ""

            If _MyData.GridRows.Count > 0 Then
                ddlValue.DataSource = (From i In _MyData.GridColumns Where i.DataType.Contains("double") Or i.DataType.Contains("integer") And Not i.DisplayName.EndsWith("ID") Select i.DisplayName).ToList()
                ddlValue.DataBind()
                ddlValue.SelectedValue = "SurveyResponseScore"

                ddlAxis.DataSource = (From i In _MyData.GridColumns Where i.DataType.Contains("string") Select i.DisplayName).ToList()
                ddlAxis.DataBind()
                Try
                    ddlAxis.SelectedValue = (From i In _MyData.GridColumns Where i.SourceName = "Context" Select i.DisplayName).First()
                Catch
                End Try

                ddlSeries.DataSource = (From i In _MyData.GridColumns Where i.DataType.Contains("string") Select i.DisplayName).ToList()
                ddlSeries.DataBind()
                ddlSeries.SelectedValue = "SurveyNM"

                ddlFilter.DataSource = (From i In _MyData.GridColumns Where i.DataType.Contains("string") Select i.DisplayName).ToList()
                ddlFilter.DataBind()
                ddlFilter.SelectedValue = "SurveyNM"


            End If
            ddlChartType.DataSource = CType([Enum].GetNames(GetType(SeriesChartType)), String())
            ddlChartType.DataBind()
            ddlChartType.SelectedValue = "StackedBar"


            ddlCalc.Items.Clear()
            ddlCalc.Items.Add(New ListItem With {.Text = "Average", .Value = "Average", .Selected = True})
            ddlCalc.Items.Add(New ListItem With {.Text = "Sum", .Value = "Sum"})
            ddlCalc.Items.Add(New ListItem With {.Text = "Count", .Value = "Count"})
            DrawChart()
        End If

    End Sub

    Protected Sub lbSeriesData_Click(sender As Object, e As EventArgs)
        If SeriesList.Count = 0 Then
            For i = 0 To cbSeries.Items.Count - 1
                cbSeries.Items(i).Selected = True
            Next
        Else
            For i = 0 To cbSeries.Items.Count - 1
                cbSeries.Items(i).Selected = False
            Next
        End If
    End Sub


    Protected Sub lbAxisData_Click(sender As Object, e As EventArgs)
        If AxisList.Count = 0 Then
            For i = 0 To cbAxis.Items.Count - 1
                cbAxis.Items(i).Selected = True
            Next
        Else
            For i = 0 To cbAxis.Items.Count - 1
                cbAxis.Items(i).Selected = False
            Next
        End If

    End Sub

    Protected Sub lbFilterData_Click(sender As Object, e As EventArgs)
        If FilterList.Count = 0 Then
            For i = 0 To cbFilter.Items.Count - 1
                cbFilter.Items(i).Selected = True
            Next
        Else
            For i = 0 To cbFilter.Items.Count - 1
                cbFilter.Items(i).Selected = False
            Next
        End If

    End Sub
    Public ReadOnly Property MyDataName As String
        Get
            Return String.Format("_MyData-{0}", CurUser.ApplicationID)
        End Get
    End Property
    Public ReadOnly Property MyGroupDataName As String
        Get
            Return String.Format("_MyGroupData-{0}", CurUser.ApplicationID)
        End Get
    End Property
    Public ReadOnly Property SeriesList As List(Of String)
        Get
            Dim myList As New List(Of String)
            For i = 0 To cbSeries.Items.Count - 1
                If cbSeries.Items(i).Selected = True Then
                    myList.Add(cbSeries.Items(i).Text)
                End If
            Next
            Return myList
        End Get
    End Property
    Public ReadOnly Property AxisList As List(Of String)
        Get
            Dim myList As New List(Of String)
            For i = 0 To cbAxis.Items.Count - 1
                If cbAxis.Items(i).Selected = True Then
                    myList.Add(cbAxis.Items(i).Text)
                End If
            Next
            Return myList
        End Get
    End Property
    Public ReadOnly Property FilterList As List(Of String)
        Get
            Dim myList As New List(Of String)
            For i = 0 To cbFilter.Items.Count - 1
                If cbFilter.Items(i).Selected = True Then
                    myList.Add(cbFilter.Items(i).Text)
                End If
            Next
            Return myList
        End Get
    End Property

    Protected Sub cmd_ResetCache_Click(sender As Object, e As EventArgs)
        Session(MyDataName) = Nothing
        Session(MyGroupDataName) = Nothing
    End Sub
End Class
