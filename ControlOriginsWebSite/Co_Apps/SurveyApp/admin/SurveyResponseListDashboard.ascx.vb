Imports CODataCon.com.controlorigins.ws
Imports CODataCon
Imports DataGridVisualization.ControlOriginsWS

Partial Class SurveyApp_controls_SurveyResponseDashboard
    Inherits SurveyUserControlBase
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim myGrid As CO_DataGrid
        If CurUser Is Nothing Then

        Else
            Dim myCon As New DataGridVisualization.DataController()

            If CurUser.RoleCD = "Admin" Then
                myGrid = myCon.GetSurveyResponseSummary(CurUser.ApplicationID, 10000000)
            Else
                myGrid = myCon.GetSurveyResponseSummary(CurUser.ApplicationID, CurUser.ApplicationUserID)

            End If
            Dim myDisplayTableHeader As New DataGridVisualization.DisplayTableHeader
            myDisplayTableHeader.DetailFieldName = "SurveyResponseNM"
            myDisplayTableHeader.DetailKeyName = "SurveyResponseID"
            myDisplayTableHeader.DetailPath = String.Format("/navigator.aspx?applicationid={0}&applicationuserid={1}", AppInfo.ApplicationID, CurUser.ApplicationUserID) & "&surveyresponseid={0}"
            For Each myCol In myGrid.GridColumns
                If myCol.SourceName = "Context" Then
                    myDisplayTableHeader.AddHeaderItem(myCol.DisplayName, myCol.DisplayName, False)
                End If
            Next
            myDisplayTableHeader.AddHeaderItem("SurveyResponseScore", "SurveyResponseScore", True, DataGridVisualization.ControlOriginsWS.DisplayFormat.Float)
            myDisplayTableHeader.AddHeaderItem("AnswerCount", "AnswerCount", False)
            myDisplayTableHeader.AddHeaderItem("QuestionCount", "QuestionCount", False)
            myDisplayTableHeader.AddHeaderItem("StatusNM", "StatusNM", False)
            dtList.BuildTableFromGrid(myDisplayTableHeader, myGrid)
            If myGrid.GridRows.Count > 0 Then
                FormatProviderScoreChart(MySurveyChart, myGrid)
                FormatSurveyStatusChart(MySurveyStatusChart, myGrid)
            End If
        End If
    End Sub

    Public Sub FormatProviderScoreChart(ByRef myChart As System.Web.UI.DataVisualization.Charting.Chart, ByRef myGrid As CO_DataGrid)
        Dim iSurveyNM As Integer
        Dim iVendorNM As Integer
        Dim iScore As Integer

        For i = 0 To myGrid.GridColumns.Count - 1
            If myGrid.GridColumns(i).DisplayName = "SurveyNM" Then
                iSurveyNM = i
            End If
            Try
                If myGrid.GridColumns(i).DisplayName = (From gc In myGrid.GridColumns Where gc.SourceName = "Context" Select gc.DisplayName).First() Then
                    iVendorNM = i
                End If
            Catch ex As Exception
                iVendorNM = 1
            End Try
            If myGrid.GridColumns(i).DisplayName = "SurveyResponseScore" Then
                iScore = i
            End If
            If iSurveyNM > 0 AndAlso iVendorNM > 0 AndAlso iScore > 0 Then
                Exit For
            End If
        Next

        Dim SurveyList = (From i In myGrid.GridRows Where i.Value(iSurveyNM) <> String.Empty Select i.Value(iSurveyNM) Distinct).ToList()
        Dim VendorList = (From i In myGrid.GridRows Select i.Value(iVendorNM) Distinct).ToList()

        myChart.Titles.Add("Average Score by Key Category")
        myChart.Width = "800"

        myChart.Legends.Clear()

        Dim ChartLegend As New System.Web.UI.DataVisualization.Charting.Legend
        With ChartLegend
            .Alignment = Drawing.StringAlignment.Center
            .Name = "Legend"
            .Docking = DataVisualization.Charting.Docking.Bottom
            .TableStyle = DataVisualization.Charting.LegendTableStyle.Wide
            .IsTextAutoFit = True
            .LegendStyle = DataVisualization.Charting.LegendStyle.Row
            .Title = ""
            .TitleAlignment = Drawing.StringAlignment.Center
        End With

        myChart.Legends.Add(ChartLegend)

        myChart.ChartAreas.Clear()
        Dim ChartArea As New System.Web.UI.DataVisualization.Charting.ChartArea
        'ChartArea.AxisY.Minimum = 0
        'ChartArea.AxisY.Maximum = 5

        myChart.ChartAreas.Add(ChartArea)


        myChart.Series.Clear()
        For Each s In SurveyList
            If s.Trim <> String.Empty Then
                Dim mySeries As New System.Web.UI.DataVisualization.Charting.Series
                mySeries.Label = ShortenNameBy("-", s)
                mySeries.ChartType = DataVisualization.Charting.SeriesChartType.StackedBar
                mySeries.LegendText = ShortenNameBy("-", s)
                Dim myCol = (From i In myGrid.GridRows Where i.Value(iSurveyNM) = s).ToList()
                For Each v In VendorList
                    If Not (IsNothing(v)) AndAlso v.Trim <> String.Empty Then
                        Dim mypoint As New System.Web.UI.DataVisualization.Charting.DataPoint
                        mypoint.AxisLabel = ShortenNameBy("-", v)
                        mypoint.IsVisibleInLegend = True
                        Dim iCount As Integer
                        Dim myValue As Double
                        For Each i In (From score In myCol Where score.Value(iVendorNM) = v Select score.Value(iScore)).ToList
                            If AppUtility.GetDBDouble(i) > 0 Then
                                myValue = myValue + AppUtility.GetDBDouble(i)
                                iCount = iCount + 1
                            End If
                        Next
                        mypoint.Label = Math.Round(myValue / iCount, 2)
                        mypoint.YValues = {Math.Round(myValue / iCount, 2)}
                        'If IsUserAppAdmin(UserInfo.UserID, AppInfo.Id) Then
                        'Else
                        '    mypoint.LabelUrl = String.Format("http://www.google.com?test={0}", s)
                        'End If
                        mySeries.Points.Add(mypoint)
                        iCount = 0
                        myValue = 0
                    End If
                Next
                myChart.Series.Add(mySeries)
            End If
        Next


    End Sub


    Public Sub FormatStatusSummaryChart(ByRef myChart As System.Web.UI.DataVisualization.Charting.Chart, ByRef myGrid As CO_DataGrid)
        Dim iSeriesOne As Integer
        Dim iScore As Integer

        For i = 0 To myGrid.GridColumns.Count - 1
            If myGrid.GridColumns(i).DisplayName = "StatusNM" Then
                iSeriesOne = i
            End If
            If myGrid.GridColumns(i).DisplayName = "SurveyResponseScore" Then
                iScore = i
            End If
            If iSeriesOne > 0 AndAlso iScore > 0 Then
                Exit For
            End If
        Next

        Dim SeriesOneLIst = (From i In myGrid.GridRows Where i.Value(iSeriesOne) <> String.Empty Select i.Value(iSeriesOne) Distinct).ToList()

        myChart.Titles.Add("Response By Status")

        myChart.Legends.Clear()
        Dim ChartLegend As New System.Web.UI.DataVisualization.Charting.Legend
        With ChartLegend
            .Alignment = Drawing.StringAlignment.Center
            .Name = "Legend"
            .Docking = DataVisualization.Charting.Docking.Top
            .TableStyle = DataVisualization.Charting.LegendTableStyle.Tall
            .IsTextAutoFit = True
            .LegendStyle = DataVisualization.Charting.LegendStyle.Column
            .Title = ""
        End With
        myChart.Legends.Add(ChartLegend)

        Dim ChartArea As New System.Web.UI.DataVisualization.Charting.ChartArea

        myChart.ChartAreas.Add(ChartArea)

        myChart.Series.Clear()
        For Each s In SeriesOneLIst
            If s.Trim <> String.Empty Then
                Dim mySeries As New System.Web.UI.DataVisualization.Charting.Series
                mySeries.Name = ShortenNameBy("-", s)
                mySeries.ChartType = DataVisualization.Charting.SeriesChartType.StackedBar100
                Dim myCol = (From i In myGrid.GridRows Where i.Value(iSeriesOne) = s).ToList()
                Dim mypoint As New System.Web.UI.DataVisualization.Charting.DataPoint
                mypoint.IsVisibleInLegend = True
                mypoint.LegendText = ShortenNameBy("-", s)
                Dim iCount As Integer
                Dim myValue As Double
                For Each i In (From score In myCol Select score.Value(iScore)).ToList
                    myValue = myValue + AppUtility.GetDBDouble(i)
                    iCount = iCount + 1
                Next
                mypoint.Label = Math.Round(iCount, 0)
                mypoint.YValues = {Math.Round(iCount, 0)}
                mySeries.Points.Add(mypoint)
                iCount = 0
                myValue = 0
                myChart.Series.Add(mySeries)
            End If
        Next
    End Sub

    Public Sub FormatSurveyStatusChart(ByRef myChart As System.Web.UI.DataVisualization.Charting.Chart, ByRef myGrid As CO_DataGrid)
        Dim iSurveyNM As Integer
        Dim iVendorNM As Integer
        Dim iScore As Integer

        For i = 0 To myGrid.GridColumns.Count - 1
            If myGrid.GridColumns(i).DisplayName = "StatusNM" Then
                iSurveyNM = i
            End If
            If myGrid.GridColumns(i).DisplayName = "SurveyNM" Then
                iVendorNM = i
            End If
            If myGrid.GridColumns(i).DisplayName = "SurveyResponseScore" Then
                iScore = i
            End If
            If iSurveyNM > 0 AndAlso iVendorNM > 0 AndAlso iScore > 0 Then
                Exit For
            End If
        Next

        Dim SurveyList = (From i In myGrid.GridRows Where i.Value(iSurveyNM) <> String.Empty Select i.Value(iSurveyNM) Distinct).ToList()
        Dim VendorList = (From i In myGrid.GridRows Select i.Value(iVendorNM) Distinct).ToList()

        myChart.Titles.Add("Scoring Completion by Criteria Group")
        myChart.Width = "800"

        myChart.Legends.Clear()

        Dim ChartLegend As New System.Web.UI.DataVisualization.Charting.Legend
        With ChartLegend
            .Alignment = Drawing.StringAlignment.Center
            .Name = "Legend"
            .Docking = DataVisualization.Charting.Docking.Bottom
            .TableStyle = DataVisualization.Charting.LegendTableStyle.Wide
            .IsTextAutoFit = True
            .LegendStyle = DataVisualization.Charting.LegendStyle.Row
            .Title = ""
            .TitleAlignment = Drawing.StringAlignment.Center
        End With

        myChart.Legends.Add(ChartLegend)

        myChart.ChartAreas.Clear()
        Dim ChartArea As New System.Web.UI.DataVisualization.Charting.ChartArea
        'ChartArea.AxisY.Minimum = 0
        'ChartArea.AxisY.Maximum = 5

        myChart.ChartAreas.Add(ChartArea)


        myChart.Series.Clear()
        For Each s In SurveyList
            If s.Trim <> String.Empty Then
                Dim mySeries As New System.Web.UI.DataVisualization.Charting.Series
                mySeries.Label = ShortenNameBy("-", s)
                mySeries.ChartType = DataVisualization.Charting.SeriesChartType.StackedBar100
                mySeries.LegendText = ShortenNameBy("-", s)
                Dim myCol = (From i In myGrid.GridRows Where i.Value(iSurveyNM) = s).ToList()
                For Each v In VendorList
                    If Not (IsNothing(v)) AndAlso v.Trim <> String.Empty Then
                        Dim mypoint As New System.Web.UI.DataVisualization.Charting.DataPoint
                        mypoint.AxisLabel = ShortenNameBy("-", v)
                        mypoint.IsVisibleInLegend = True
                        Dim iCount As Integer
                        Dim myValue As Double
                        For Each i In (From score In myCol Where score.Value(iVendorNM) = v Select score.Value(iScore)).ToList
                            myValue = myValue + AppUtility.GetDBDouble(i)
                            iCount = iCount + 1
                        Next
                        mypoint.Label = Math.Round(iCount, 2)
                        mypoint.YValues = {Math.Round(iCount, 2)}
                        mySeries.Points.Add(mypoint)
                        iCount = 0
                        myValue = 0
                    End If
                Next
                myChart.Series.Add(mySeries)
            End If
        Next


    End Sub

End Class
