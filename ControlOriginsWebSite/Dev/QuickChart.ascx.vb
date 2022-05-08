Imports System.Web.UI.DataVisualization.Charting
Imports DataGridVisualization
Imports DataGridVisualization.ControlOriginsWS


Public Class Co_Apps_SurveyApp_QuickChart
    Inherits SurveyUserControlBase
    Public myGrid As New CO_DataGrid
    Public myChartConfig As New DataGridVisualization.DataGridVisualization

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack Then
            cmd_SetChartConfig.CssClass = "btn btn-warning"
        Else
            ddlDataSetName.Items.Clear()
            ddlDataSetName.DataSource = CType([Enum].GetNames(GetType(DataSetName)), String())
            ddlDataSetName.DataBind()

            hfSeries.Value = ""
            hfAxis.Value = ""
            hfFilter.Value = ""
            tbTitle.Text = AppInfo.ApplicationNM

            ddlChartType.DataSource = CType([Enum].GetNames(GetType(SeriesChartType)), String())
            ddlChartType.DataBind()
            ddlChartType.SelectedValue = "StackedBar"

            ddlChartPalette.DataSource = CType([Enum].GetNames(GetType(ChartColorPalette)), String())
            ddlChartPalette.DataBind()
            ddlChartPalette.SelectedValue = "BrightPastel"

            ddlCalc.Items.Clear()
            ddlCalc.Items.Add(New ListItem With {.Text = "Average", .Value = "Average"})
            ddlCalc.Items.Add(New ListItem With {.Text = "Sum", .Value = "Sum", .Selected = True})
            ddlCalc.Items.Add(New ListItem With {.Text = "Count", .Value = "Count"})

            ResetForm(AppInfo.ApplicationNM)

        End If

    End Sub

    Protected Sub ddlConfig_SelectedIndexChanged(sender As Object, e As EventArgs)
        ResetForm(ddlConfig.SelectedValue)
    End Sub

    Protected Sub cmd_SetChartConfig_Click(sender As Object, e As EventArgs)
        myChartConfig = PivotParmList.GetConfiguration(AppInfo.ApplicationID, ddlConfig.SelectedValue)

        myChartConfig.ChartTitles.Clear()
        myChartConfig.ChartTitles.Add(tbTitle.Text)
        myChartConfig.ChartTitles.Add(tbSubTitle.Text)
        myChartConfig.ChartPalette = ddlChartPalette.SelectedIndex
        myChartConfig.ChartType = CType(ddlChartType.SelectedIndex, SeriesChartType)
        myChartConfig.ChartStyle = ddlChartStyle.SelectedValue
        myChartConfig.BackgroundImage = ddlBackgroundImage.SelectedValue

        LoadData(CType(ddlDataSetName.SelectedIndex, DataSetName))
        myChartConfig.ReturnChartObject(myChart, myGrid)
    End Sub



    Protected Sub cmd_SaveVisualization_Click(sender As Object, e As EventArgs)
        Dim myConfigurationList As New DataGridVisualizationList()
        myConfigurationList.Clear()
        myConfigurationList.AddRange(PivotParmList.ToArray)

        If myConfigurationList.Count > 0 Then
            myChartConfig = myConfigurationList.GetConfiguration(AppInfo.ApplicationID, CType(ddlDataSetName.SelectedIndex, DataSetName), ddlConfig.SelectedValue)
            If myChartConfig Is Nothing Then
                myChartConfig = New DataGridVisualization.DataGridVisualization
                myChartConfig.Name = ddlConfig.SelectedValue
                myChartConfig.ApplicationID = AppInfo.ApplicationID
                myChartConfig.DataSource = CType(ddlDataSetName.SelectedIndex, DataSetName)
            End If
        Else
            myChartConfig.Name = ddlConfig.SelectedValue
            myChartConfig.ApplicationID = AppInfo.ApplicationID
            myChartConfig.DataSource = CType(ddlDataSetName.SelectedIndex, DataSetName)
        End If

        myChartConfig.ChartTitles.Clear()
        myChartConfig.ChartTitles.Add(tbTitle.Text)
        myChartConfig.ChartTitles.Add(tbSubTitle.Text)
        myChartConfig.ChartPalette = ddlChartPalette.SelectedIndex
        myChartConfig.ChartType = CType(ddlChartType.SelectedIndex, SeriesChartType)
        myChartConfig.ChartStyle = ddlChartStyle.SelectedValue
        myChartConfig.BackgroundImage = ddlBackgroundImage.SelectedValue

        myConfigurationList.AddToList(myChartConfig)

        PivotParmList = myConfigurationList
        ResetForm(myChartConfig.Name)


    End Sub

    Public Sub SetDataControl()
        If myGrid.GridRows.Count > 0 Then
            ddlValueColumn.Items.Clear()
            ' Where i.ColumnDisplayFormat <> DisplayFormat.Text And i.ColumnDisplayFormat <> DisplayFormat.Hidden 
            For Each c In (From i In myGrid.GridColumns Select i.DisplayName Distinct)
                ddlValueColumn.Items.Add(c)
            Next
            ddlAxis.Items.Clear()
            ddlFilter.Items.Clear()
            ddlSeries.Items.Clear()
            ' Where i.ColumnDisplayFormat = DisplayFormat.Text 
            For Each c In (From i In myGrid.GridColumns Select i.DisplayName)
                ddlAxis.Items.Add(c)
                ddlSeries.Items.Add(c)
                ddlFilter.Items.Add(c)
            Next

            hfSeries.Value = ddlSeries.SelectedValue
            hfAxis.Value = ddlAxis.SelectedValue
            hfFilter.Value = ddlFilter.SelectedValue

            SetupColumnSelection(myGrid, ddlSeries, cbSeries, ddlSeries.SelectedValue)
            SetupColumnSelection(myGrid, ddlAxis, cbAxis, ddlAxis.SelectedValue)
            SetupColumnSelection(myGrid, ddlFilter, cbFilter, ddlFilter.SelectedValue)

        End If

    End Sub

    Public Sub LoadData(ByVal MyDataSet As DataSetName)
        Dim myCon As New DataController
        Dim myFilter As New List(Of SQLFilterClause)
        MyDataSet = CType(ddlDataSetName.SelectedIndex, DataSetName)

        Select Case MyDataSet
            Case DataSetName.ApplicationDetail
                myFilter.Add(New SQLFilterClause With {.Argument = AppInfo.ApplicationID,
                                                       .Conjunction = SQLFilterConjunction.andConjunction,
                                                       .Field = "ApplicationID",
                                                       .FieldOperator = SQLFilterOperator.Equal,
                                                       .FieldType = "integer"})
                myGrid = myCon.myWS.GetSurveyResponseAnswersGrid(myFilter.ToArray(), myCon.myGUID)
            Case DataSetName.ApplicationList

                myGrid = myCon.myWS.GetApplicationGrid(myCon.myGUID)
            Case DataSetName.ApplicationSummary

                myGrid = myCon.GetApplicationGrid()
            Case DataSetName.QuestionList

                myGrid = myCon.myWS.GetQuestionList(myFilter.ToArray(), myCon.myGUID)
            Case DataSetName.SurveyGroupSummary
                myFilter.Add(New SQLFilterClause With {.Argument = AppInfo.ApplicationID,
                                                       .Conjunction = SQLFilterConjunction.andConjunction,
                                                       .Field = "ApplicationID",
                                                       .FieldOperator = SQLFilterOperator.Equal,
                                                       .FieldType = "integer"})

                myGrid = myCon.myWS.GetSurveyResponseGroupGrid(myFilter.ToArray, myCon.myGUID)
            Case DataSetName.SurveyList

                myFilter.Add(New SQLFilterClause With {.Argument = AppInfo.ApplicationID,
                                                       .Conjunction = SQLFilterConjunction.andConjunction,
                                                       .Field = "ApplicationID",
                                                       .FieldOperator = SQLFilterOperator.Equal,
                                                       .FieldType = "integer"})
                myGrid = myCon.myWS.GetSurveySummaryGrid(myFilter.ToArray(), myCon.myGUID)
            Case DataSetName.SurveySummary

                myFilter.Add(New SQLFilterClause With {.Argument = AppInfo.ApplicationID,
                                                       .Conjunction = SQLFilterConjunction.andConjunction,
                                                       .Field = "ApplicationID",
                                                       .FieldOperator = SQLFilterOperator.Equal,
                                                       .FieldType = "integer"})
                myGrid = myCon.myWS.GetSurveyResponseSummaryGrid(myFilter.ToArray, myCon.myGUID)
            Case DataSetName.UserList

                myGrid = myCon.GetApplicationGrid()
            Case Else
                myGrid = myCon.GetApplicationGrid()
        End Select
    End Sub




    Public Sub ResetForm(ByVal Name As String)
        ddlConfig.Items.Clear()
        ddlConfig.Items.AddRange((From i In PivotParmList.GetConfigLookup(AppInfo.ApplicationID) Select New ListItem With {.Text = i, .Value = i}).ToArray())
        If ddlConfig.Items.Count < 1 Then
            ddlConfig.Items.Add(New ListItem With {.Text = AppInfo.ApplicationNM, .Value = AppInfo.ApplicationNM})
        End If
        ddlConfig.SelectedValue = Name

        myChartConfig = PivotParmList.GetConfiguration(AppInfo.ApplicationID, Name)

        If Not myChartConfig Is Nothing Then
            ddlDataSetName.SelectedIndex = myChartConfig.DataSource
        End If
        LoadData(CType(ddlDataSetName.SelectedIndex, DataSetName))
        SetDataControl()

        ddlValueColumn.SelectedValue = myChartConfig.Vals(0).DisplayName
        ddlSeries.SelectedValue = myChartConfig.Cols(0).DisplayName
        ddlAxis.SelectedValue = myChartConfig.Rows(0).DisplayName

        hfSeries.Value = ddlSeries.SelectedValue
        hfAxis.Value = ddlAxis.SelectedValue
        hfFilter.Value = ddlFilter.SelectedValue

        SetupColumnSelection(myGrid, ddlSeries, cbSeries, ddlSeries.SelectedValue)
        SetupColumnSelection(myGrid, ddlAxis, cbAxis, ddlAxis.SelectedValue)
        SetupColumnSelection(myGrid, ddlFilter, cbFilter, ddlFilter.SelectedValue)

        SetChartConfiguration(myChartConfig)

        myChartConfig.ReturnChartObject(myChart, myGrid)

    End Sub

    Public Sub SetupColumnSelection(
                                ByRef MyGrid As CO_DataGrid,
                                ByRef ddl As DropDownList,
                                ByRef cbl As CheckBoxList,
                                ByVal ColumnName As String)

        Dim Column As New GridColumn

        For index = 0 To MyGrid.GridColumns.Count - 1
            If MyGrid.GridColumns(index).DisplayName = ColumnName Then
                Column.Index = index
                Column.SourceName = MyGrid.GridColumns(index).DisplayName
                ddl.SelectedValue = Column.DisplayName
                Exit For
            End If
        Next
        cbl.Items.Clear()

        For Each c In (From i In MyGrid.GridRows Where i.Value(Column.Index) <> String.Empty Select i.Value(Column.Index) Distinct).Take(30)
            cbl.Items.Add(c)
        Next

        If cbl.Items.Count > 0 Then
            For i = 0 To cbl.Items.Count - 1
                cbl.Items(i).Selected = True
            Next
        End If

    End Sub

    Private Sub SetChartConfiguration(myConfiguration As DataGridVisualization.DataGridVisualization)
        If myConfiguration.ChartTitles.Count = 1 Then
            tbTitle.Text = myConfiguration.ChartTitles(0)
        ElseIf myConfiguration.ChartTitles.Count = 2 Then
            tbTitle.Text = myConfiguration.ChartTitles(0)
            tbSubTitle.Text = myConfiguration.ChartTitles(1)
        Else
            tbTitle.Text = String.Format("{0} ({1})", myConfiguration.Name, myConfiguration.ApplicationID)
            tbSubTitle.Text = String.Empty
        End If
        ddlBackgroundImage.SelectedValue = myConfiguration.BackgroundImage
        ddlChartPalette.SelectedIndex = myConfiguration.ChartPalette
        ddlChartType.SelectedIndex = myConfiguration.ChartType
        ddlChartStyle.SelectedValue = myConfiguration.ChartStyle
    End Sub

End Class


'Public Sub SetData()
'    Dim coGrid As New CODataCon.com.controlorigins.ws.CO_DataGrid
'    '     Dim mySQLFilter As New SQLFilterList
'    Session(AppDataName) = Nothing
'    Try
'        If Session(AppDataName) Is Nothing Then
'            Dim myCon As New CODataCon.DataControler()

'            Select Case ddlDataSet.SelectedValue.ToLower()
'                Case "summary"
'                    coGrid = myCon.GetSurveyResponseSummary(CurUser.ApplicationID, 19999)
'                Case "group"
'                    coGrid = myCon.GetSurveyResponseGroupSummary(CurUser.ApplicationID, 19999)
'                Case "detail"
'                    coGrid = myCon.GetSurveyResponseAnswers(CurUser.ApplicationID, 19999)
'                Case Else
'                    coGrid = myCon.GetSurveyResponseSummary(CurUser.ApplicationID, 19999)
'            End Select

'            myChartConfig.SourceData.GridColumns.Clear()
'            For Each myCol In coGrid.GridColumns
'                myChartConfig.SourceData.GridColumns.Add(New GridColumn With {.DataType = myCol.DataType, .Name = myCol.DisplayName, .Source = myCol.SourceName})
'            Next

'            myChartConfig.SourceData.GridRows.Clear()
'            For Each myRow In coGrid.GridRows
'                myChartConfig.SourceData.GridRows.Add(New wpm_DataGrid.GridRow With {.name = myRow.name, .Value = myRow.Value.ToList()})
'            Next

'            Session(AppDataName) = myChartConfig.SourceData
'            myChartConfig.SourceData = TryCast(Session(AppDataName), wpm_DataGrid)
'        Else
'            myChartConfig.SourceData = TryCast(Session(AppDataName), wpm_DataGrid)
'        End If

'        Dim myDisplayTableHeader As New DataGridVisualization.DisplayTableHeader
'        For Each myCol In coGrid.GridColumns
'            myDisplayTableHeader.AddHeaderItem(myCol.DisplayName , myCol.SourceName)
'        Next
'        dtList.BuildTableFromGrid(myDisplayTableHeader, coGrid)


'        If myChartConfig.SourceData.GridRows.Count > 0 Then
'            If ddlValue.Items.Count = 0 Then
'                ddlValue.Items.Clear()
'                For Each c In (From i In myChartConfig.SourceData.GridColumns Where (i.DataType.ToLower.Contains("float") Or i.DataType.ToLower.Contains("currency") Or i.DataType.ToLower.Contains("integer") Or i.DataType.ToLower.Contains("double")) And Not i.Name.EndsWith("ID") Select i.Name)
'                    ddlValue.Items.Add(c)
'                Next
'            End If
'            If ddlAxis.Items.Count = 0 Or ddlFilter.Items.Count = 0 Or ddlSeries.Items.Count = 0 Then
'                ddlAxis.Items.Clear()
'                ddlFilter.Items.Clear()
'                ddlSeries.Items.Clear()
'                For Each c In (From i In myChartConfig.SourceData.GridColumns Where i.DataType.Contains("string") Or i.Source = "Context" Select i.Name)
'                    ddlAxis.Items.Add(c)
'                    ddlSeries.Items.Add(c)
'                    ddlFilter.Items.Add(c)
'                Next

'                For index = 0 To myChartConfig.SourceData.GridColumns.Count - 1
'                    If myChartConfig.SourceData.GridColumns(index).Name = ddlSeries.SelectedValue Then
'                        myChartConfig.yColumn.Index = index
'                        Exit For
'                    End If
'                Next




'                If cbAxis.Items.Count = 0 Or cbFilter.Items.Count = 0 Or cbSeries.Items.Count = 0 Then
'                    For Each c In (From i In myChartConfig.SourceData.GridRows Where i.Value(myChartConfig.yColumn.Index) <> String.Empty Select i.Value(myChartConfig.yColumn.Index) Distinct)
'                        cbAxis.Items.Add(New ListItem With {.Text = c, .Value = c, .Selected = True})
'                        cbSeries.Items.Add(New ListItem With {.Text = c, .Value = c, .Selected = True})
'                        cbFilter.Items.Add(New ListItem With {.Text = c, .Value = c, .Selected = True})

'                    Next
'                End If
'            End If
'        End If
'    Catch ex As Exception
'        ' oops
'    End Try
'End Sub

'Public Sub DrawChart()
'    For i = 0 To myChartConfig.SourceData.GridColumns.Count - 1
'        If myChartConfig.SourceData.GridColumns(i).Name = ddlValue.SelectedValue Then
'            myChartConfig.ValueColumn.Index = i
'            myChartConfig.ValueColumn.Name = ddlValue.SelectedValue
'        End If
'        If myChartConfig.SourceData.GridColumns(i).Name = ddlAxis.SelectedValue Then
'            myChartConfig.xColumn.Index = i
'            myChartConfig.xColumn.Name = ddlAxis.SelectedValue
'            myChartConfig.xColumnSelected.Clear()
'            For ic = 0 To cbAxis.Items.Count - 1
'                If cbAxis.Items(ic).Selected = True Then
'                    myChartConfig.xColumnSelected.Add(cbAxis.Items(ic).Text)
'                End If
'            Next
'        End If
'        If myChartConfig.SourceData.GridColumns(i).Name = ddlSeries.SelectedValue Then
'            myChartConfig.yColumn.Index = i
'            myChartConfig.yColumn.Name = ddlSeries.SelectedValue
'            myChartConfig.yColumnSelected.Clear()
'            For ic = 0 To cbSeries.Items.Count - 1
'                If cbSeries.Items(ic).Selected = True Then
'                    myChartConfig.yColumnSelected.Add(cbSeries.Items(ic).Text)
'                End If
'            Next
'        End If

'        If myChartConfig.SourceData.GridColumns(i).Name = ddlFilter.SelectedValue Then
'            myChartConfig.FilterColumn.Index = i
'            myChartConfig.FilterColumn.Name = ddlFilter.SelectedValue
'            myChartConfig.FilterColumnSelected.Clear()
'            For ic = 0 To cbFilter.Items.Count - 1
'                If cbFilter.Items(ic).Selected = True Then
'                    myChartConfig.FilterColumnSelected.Add(cbFilter.Items(ic).Text)
'                End If
'            Next
'        End If
'    Next

'    myChartConfig.ChartTitles.Add(tbTitle.Text)
'    myChartConfig.ChartTitles.Add(tbSubTitle.Text)
'    myChartConfig.SourceData.Title = AppInfo.AppName
'    myChartConfig.ChartWidth = 800
'    myChartConfig.ChartHeight = 400

'    If ddlChartStyle.SelectedIndex > 0 Then
'        myChartConfig.ChartStyle = ddlChartStyle.SelectedValue.ToUpper()
'    Else
'        myChartConfig.ChartStyle = "2D"
'    End If
'    myChartConfig.ChartType = CType(ddlChartType.SelectedIndex, SeriesChartType)
'    myChartConfig.ChartPalette = CType(ddlChartPalette.SelectedIndex, ChartColorPalette)
'    myChartConfig.AggregateFunction = ddlCalc.SelectedValue

'    ' End of Chart Configuration Update
'    myChartConfig.ReturnChartObject(myChart)

'    If myChartConfig.ChartDataGrid.GridRows.Count > 0 Then
'        cmd_SetChartConfig.CssClass = "btn btn-primary"
'        Dim ChartDataHeader As New DisplayTableHeader
'        ChartDataHeader.AddHeaderItem(ddlAxis.SelectedValue, ddlAxis.SelectedValue)
'        ChartDataHeader.AddHeaderItem(ddlFilter.SelectedValue, ddlFilter.SelectedValue)
'        ChartDataHeader.AddHeaderItem(ddlSeries.SelectedValue, ddlSeries.SelectedValue)
'        ChartDataHeader.AddHeaderItem(ddlValue.SelectedValue, ddlValue.SelectedValue)
'        dtList.BuildTableFromGrid(ChartDataHeader, myChartConfig.ChartDataGrid)
'    Else
'        cmd_SetChartConfig.CssClass = "btn btn-warning"

'    End If
'End Sub

'Public ReadOnly Property AppDataName As String
'    Get
'        Return String.Format("_AppData-{0}-{1}", AppInfo.AppName, ddlDataSet.SelectedValue.ToLower())
'    End Get
'End Property
'Protected Sub ddlDataSet_SelectedIndexChanged(sender As Object, e As EventArgs)
'    ddlValue.Items.Clear()
'    ddlAxis.Items.Clear()
'    ddlFilter.Items.Clear()
'    ddlSeries.Items.Clear()
'    cbAxis.Items.Clear()
'    cbFilter.Items.Clear()
'    cbSeries.Items.Clear()
'    SetData()
'End Sub
'Protected Sub lbFilterData_Click(sender As Object, e As EventArgs)
'    If (From i As ListItem In cbFilter.Items Where i.Selected = True).Count = 0 Then
'        For i = 0 To cbFilter.Items.Count - 1
'            cbFilter.Items(i).Selected = True
'        Next
'    Else
'        For i = 0 To cbFilter.Items.Count - 1
'            cbFilter.Items(i).Selected = False
'        Next
'    End If
'End Sub
'Protected Sub lbAxisData_Click(sender As Object, e As EventArgs)
'    If (From i As ListItem In cbAxis.Items Where i.Selected = True).Count = 0 Then
'        For i = 0 To cbAxis.Items.Count - 1
'            cbAxis.Items(i).Selected = True
'        Next
'    Else
'        For i = 0 To cbAxis.Items.Count - 1
'            cbAxis.Items(i).Selected = False
'        Next
'    End If
'End Sub
'Protected Sub lbSeriesData_Click(sender As Object, e As EventArgs)
'    If (From i As ListItem In cbSeries.Items Where i.Selected = True).Count = 0 Then
'        For i = 0 To cbSeries.Items.Count - 1
'            cbSeries.Items(i).Selected = True
'        Next
'    Else
'        For i = 0 To cbSeries.Items.Count - 1
'            cbSeries.Items(i).Selected = False
'        Next
'    End If
'End Sub
'Protected Sub cmd_SetChartConfig_Click(sender As Object, e As EventArgs)
'    DrawChart()
'End Sub
'Protected Sub cmd_ResetCache_Click(sender As Object, e As EventArgs)
'    Session(AppDataName) = Nothing
'End Sub
'Protected Sub ddlSeries_SelectedIndexChanged(sender As Object, e As EventArgs)
'    SetData()
'    If hfSeries.Value <> ddlSeries.SelectedValue Then
'        hfSeries.Value = ddlSeries.SelectedValue
'        cbSeries.Items.Clear()
'        For index = 0 To myChartConfig.SourceData.GridColumns.Count - 1
'            If myChartConfig.SourceData.GridColumns(index).Name = ddlSeries.SelectedValue Then
'                myChartConfig.yColumn.Index = index
'                Exit For
'            End If
'        Next

'        For Each c In (From i In myChartConfig.SourceData.GridRows Where i.Value(myChartConfig.yColumn.Index) <> String.Empty Select i.Value(myChartConfig.yColumn.Index) Distinct)
'            cbSeries.Items.Add(c)
'        Next
'        For i = 0 To cbSeries.Items.Count - 1
'            cbSeries.Items(i).Selected = True
'        Next
'    End If

'End Sub
'Protected Sub ddlAxis_SelectedIndexChanged(sender As Object, e As EventArgs)
'    SetData()
'    If hfAxis.Value <> ddlAxis.SelectedValue Then
'        hfAxis.Value = ddlAxis.SelectedValue
'        cbAxis.Items.Clear()
'        For index = 0 To myChartConfig.SourceData.GridColumns.Count - 1
'            If myChartConfig.SourceData.GridColumns(index).Name = ddlAxis.SelectedValue Then
'                myChartConfig.xColumn.Index = index
'                Exit For
'            End If
'        Next
'        For Each c In (From i In myChartConfig.SourceData.GridRows Where i.Value(myChartConfig.xColumn.Index) <> String.Empty Select i.Value(myChartConfig.xColumn.Index) Distinct)
'            cbAxis.Items.Add(c)
'        Next
'        For i = 0 To cbAxis.Items.Count - 1
'            cbAxis.Items(i).Selected = True
'        Next
'    End If

'End Sub
'Protected Sub ddlFilter_SelectedIndexChanged(sender As Object, e As EventArgs)
'    SetData()
'    If hfFilter.Value <> ddlFilter.SelectedValue Then
'        hfFilter.Value = ddlFilter.SelectedValue
'        cbFilter.Items.Clear()
'        For index = 0 To myChartConfig.SourceData.GridColumns.Count - 1
'            If myChartConfig.SourceData.GridColumns(index).Name = ddlFilter.SelectedValue Then
'                myChartConfig.FilterColumn.Index = index
'                Exit For
'            End If
'        Next
'        For Each c In (From i In myChartConfig.SourceData.GridRows Where i.Value(myChartConfig.FilterColumn.Index) <> String.Empty Select i.Value(myChartConfig.FilterColumn.Index) Distinct)
'            cbFilter.Items.Add(c)
'        Next
'        For i = 0 To cbFilter.Items.Count - 1
'            cbFilter.Items(i).Selected = True
'        Next
'    End If
'End Sub



