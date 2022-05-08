Imports System.Reflection
Imports System
Imports DataGridVisualization
Imports DataGridVisualization.ControlOriginsWS

Public Class Co_Apps_SurveyApp_ApplicationDataPresentation
    Inherits SurveyUserControlBase

    Public myGrid As New CO_DataGrid
    Public myChartConfig As New DataGridVisualization.DataGridVisualization
    Public Property PivotParms As String = "derivedAttributes: {},"
    Public Property JSONGrid As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ddlDataSetName.Items.Clear()
            ddlDataSetName.DataSource = CType([Enum].GetNames(GetType(DataSetName)), String())
            ddlDataSetName.DataBind()
            ResetForm(AppInfo.ApplicationNM)
        End If
    End Sub

    Protected Sub ddlDataSetName_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadData(CType(ddlDataSetName.SelectedIndex, DataSetName))
        JSONGrid = FormatJSON(myGrid)
    End Sub

    Protected Sub ddlConfig_SelectedIndexChanged(sender As Object, e As EventArgs)
        ResetForm(ddlConfig.SelectedValue)
    End Sub

    Protected Sub cmd_SaveVisualization_Click(sender As Object, e As EventArgs)
        SetConfiguration()
    End Sub

    Protected Sub cmd_Delete_Click(sender As Object, e As EventArgs)
        Dim ConfigName As String = AppInfo.ApplicationNM
        If ddlConfig.SelectedValue <> AppInfo.ApplicationNM Then
            Dim myConfig = PivotParmList.GetConfiguration(AppInfo.ApplicationID, ddlDataSetName.SelectedIndex, ddlConfig.SelectedValue)
            Dim myConfigurationList As New DataGridVisualizationList()
            myConfigurationList.Clear()
            myConfigurationList.AddRange(PivotParmList.ToArray)
            For i = 0 To myConfigurationList.Count - 1
                
                If myConfigurationList(i).ApplicationID = myConfig.ApplicationID AndAlso myConfigurationList(i).Name = myConfig.Name Then
                    myConfigurationList.RemoveAt(i)
                    Exit For
                    Else
                    ConfigName = myConfigurationList(i).Name
                End If
            Next
            PivotParmList = myConfigurationList
        End If
        ResetForm(ConfigName)
    End Sub

    Public Sub ResetForm(ByVal Name As String)
        tbName.Text = String.Empty
        ddlConfig.Items.Clear()
        ddlConfig.Items.AddRange((From i In PivotParmList.GetConfigLookup(AppInfo.ApplicationID) Select New ListItem With {.Text = i, .Value = i}).ToArray())
        If ddlConfig.Items.Count < 1 Then
            ddlConfig.Items.Add(New ListItem With {.Text = AppInfo.ApplicationNM, .Value = AppInfo.ApplicationNM})
            tbName.Enabled = False
        Else
            tbName.Enabled = True
        End If
      
        ddlConfig.SelectedValue= Name
        If Name = AppInfo.ApplicationNM Then
            cmd_Delete.Enabled = False
            cmd_Delete.Visible = False
        Else
            cmd_Delete.Enabled = True
            cmd_Delete.Visible = True
        End If

        Dim myConfig = PivotParmList.GetConfiguration(AppInfo.ApplicationID, Name)
        If Not myConfig Is Nothing Then
            PivotParms = myConfig.GetPivotParm
            ddlDataSetName.SelectedIndex = myConfig.DataSource
        End If
        LoadData(CType(ddlDataSetName.SelectedIndex, DataSetName))
        JSONGrid = FormatJSON(myGrid)
    End Sub
    Public Sub LoadData(ByVal MyDataSet As DataSetName)
        Try
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
        Catch ex As Exception

        End Try

    End Sub
    Function FormatJSON(ByRef theDataGrid As CO_DataGrid) As String
        Dim myJSON As New StringBuilder()
        myJSON.AppendLine("")
        myJSON.AppendLine("[")
        For Each myRow In theDataGrid.GridRows
            myJSON.Append("{ ")
            For x = 0 To theDataGrid.GridColumns.Count - 1
                If x = theDataGrid.GridColumns.Count - 1 Then
                    myJSON.Append(String.Format("{0}: ""{1}""", FormatDisplayName(theDataGrid.GridColumns(x).DisplayName), myRow.Value(x)))
                Else
                    myJSON.Append(String.Format("{0}: ""{1}"",", FormatDisplayName(theDataGrid.GridColumns(x).DisplayName), myRow.Value(x)))
                End If
            Next
            myJSON.AppendLine("},")
        Next
        myJSON.AppendLine("]")
        myJSON.AppendLine("")
        Return myJSON.ToString()
    End Function


    Protected Sub SetConfiguration()
        Dim myConfigurationList As New DataGridVisualizationList()
        myConfigurationList.Clear()
        myConfigurationList.AddRange(PivotParmList.ToArray)

        Dim myJSON As New JSONObject(tbJSON.Text)

        Dim myConfiguration As DataGridVisualization.DataGridVisualization = New DataGridVisualization.DataGridVisualization
        myConfiguration.Name = ddlConfig.SelectedValue
        myConfiguration.ApplicationID = AppInfo.ApplicationID
        myConfiguration.DataSource = CType(ddlDataSetName.SelectedIndex, DataSetName)

        If Not String.IsNullOrEmpty(tbName.Text) Then
            myConfiguration.Name = tbName.Text.Trim()
        End If

        myConfiguration.Cols.Clear()
        For Each mycol As JSONValue In myJSON.GetProperty("cols").Value
            myConfiguration.Cols.Add(New GridColumn With {.SourceName = mycol.Value(), .DisplayName = mycol.Value()})
        Next

        myConfiguration.Rows.Clear()
        For Each mycol As JSONValue In myJSON.GetProperty("rows").Value
            myConfiguration.Rows.Add(New GridColumn With {.SourceName = mycol.Value(), .DisplayName = mycol.Value()})
        Next

        myConfiguration.Vals.Clear()
        For Each mycol As JSONValue In myJSON.GetProperty("vals").Value
            myConfiguration.Vals.Add(New GridColumn With {.SourceName = mycol.Value(), .DisplayName = mycol.Value()})
        Next

        myConfiguration.ExcluedValues.Clear()
        Dim myEx = TryCast(myJSON.GetProperty("exclusions").Value, JSONObject)
        For Each myExclusion In myEx.GetProperties().ToList
            Dim myValues As New List(Of String)
            Dim myValSet As New GridColumn With {.SourceName = myExclusion.Key, .DisplayName = myExclusion.Key}

            For Each myProp In JSONObject.ParseJSONArray(myExclusion.Value)
                myValues.Add(myProp.Replace("""", ""))
            Next

            myValSet.ColumnValues = myValues.ToArray()
            myConfiguration.ExcluedValues.Add(myValSet)
        Next

        myConfiguration.IncludeValues.Clear()
        Dim myIn = TryCast(myJSON.GetProperty("inclusions").Value, JSONObject)
        For Each myInclusion In myIn.GetProperties().ToList
            Dim myValues As New List(Of String)
            Dim myValSet As New GridColumn With {.SourceName = myInclusion.Key, .DisplayName = myInclusion.Key}

            For Each myProp In JSONObject.ParseJSONArray(myInclusion.Value)
                myValues.Add(myProp.Replace("""", ""))
            Next

            myValSet.ColumnValues = myValues.ToArray()
            myConfiguration.IncludeValues.Add(myValSet)
        Next

        myConfiguration.AggregatorName = myJSON.GetProperty("aggregatorName").Value
        myConfiguration.rendererName = myJSON.GetProperty("rendererName").Value

        myConfigurationList.AddToList(myConfiguration)
        PivotParmList = myConfigurationList

        PivotParms = myConfiguration.GetPivotParm
        ResetForm(myConfiguration.Name)
    End Sub

    Public Function FormatDisplayName(ByVal DisplayName As String) As String
        If InStr(DisplayName, ".", CompareMethod.Text) Then
            Return Right(DisplayName, Len(DisplayName) - InStr(DisplayName, ".", CompareMethod.Text))
        Else
            Return DisplayName
        End If
    End Function

End Class


'Protected Sub cmd_LoadSummaryData_Click(sender As Object, e As EventArgs)
'    If rblOutput.SelectedValue = "CSV" Then
'        If CurUser.RoleCD.ToLower = "admin" Then
'            Response.Redirect(String.Format("/Co_Apps/SurveyApp/module/CookiesHandler.ashx?method=GetSurveySummaryCSV&ApplicationID={0}&ApplicationUserID=1000000", CurUser.ApplicationID))
'        Else
'            Response.Redirect(String.Format("/Co_Apps/SurveyApp/module/CookiesHandler.ashx?method=GetSurveySummaryCSV&ApplicationID={0}&ApplicationUserID={1}", CurUser.ApplicationID, CurUser.ApplicationUserID))
'        End If
'    Else
'        Dim myFilterList As New List(Of CODataCon.com.controlorigins.ws.SQLFilterClause)
'        Dim myCon As New CODataCon.DataControler()
'        Dim myGrid As CODataCon.com.controlorigins.ws.CO_DataGrid
'        If CurUser.RoleCD.ToLower = "admin" Then
'            myGrid = myCon.GetSurveyResponseSummary(CurUser.ApplicationID, 100000)
'        Else
'            myGrid = myCon.GetSurveyResponseSummary(CurUser.ApplicationID, CurUser.ApplicationUserID)
'        End If
'        rptHeaderRow.DataSource = myGrid.GridColumns
'        rptHeaderRow.DataBind()
'        rptDataRows.DataSource = myGrid.GridRows
'        rptDataRows.DataBind()
'        pnlPivotData.Visible = True
'        pnlPivotTable.Visible = True
'    End If
'End Sub
'Protected Sub cmd_LoadGroupData_Click(sender As Object, e As EventArgs)
'    If rblOutput.SelectedValue = "CSV" Then
'        If CurUser.RoleCD.ToLower = "admin" Then
'            Response.Redirect(String.Format("/Co_Apps/SurveyApp/module/CookiesHandler.ashx?method=GetSurveyGroupSummaryCSV&ApplicationID={0}&ApplicationUserID=1000000", CurUser.ApplicationID))
'        Else
'            Response.Redirect(String.Format("/Co_Apps/SurveyApp/module/CookiesHandler.ashx?method=GetSurveyGroupSummaryCSV&ApplicationID={0}&ApplicationUserID={1}", CurUser.ApplicationID, CurUser.ApplicationUserID))
'        End If
'    Else
'        Dim myFilterList As New List(Of CODataCon.com.controlorigins.ws.SQLFilterClause)
'        Dim myCon As New CODataCon.DataControler()
'        Dim myGrid As CODataCon.com.controlorigins.ws.CO_DataGrid

'        If CurUser.RoleCD.ToLower = "admin" Then
'            myGrid = myCon.GetSurveyResponseGroupSummary(CurUser.ApplicationID, 100000)
'        Else
'            myGrid = myCon.GetSurveyResponseGroupSummary(CurUser.ApplicationID, CurUser.ApplicationUserID)
'        End If

'        rptHeaderRow.DataSource = myGrid.GridColumns
'        rptHeaderRow.DataBind()
'        rptDataRows.DataSource = myGrid.GridRows
'        rptDataRows.DataBind()
'        pnlPivotData.Visible = True
'        pnlPivotTable.Visible = True
'    End If
'End Sub
'Protected Sub cmd_LoadAnswerData_click(sender As Object, e As EventArgs)
'    If rblOutput.SelectedValue = "CSV" Then
'        If CurUser.RoleCD.ToLower = "admin" Then
'            Response.Redirect(String.Format("/Co_Apps/SurveyApp/module/CookiesHandler.ashx?method=GetSurveyAnswersCSV&ApplicationID={0}&ApplicationUserID=1000000", CurUser.ApplicationID))
'        Else
'            Response.Redirect(String.Format("/Co_Apps/SurveyApp/module/CookiesHandler.ashx?method=GetSurveyAnswersCSV&ApplicationID={0}&ApplicationUserID={1}", CurUser.ApplicationID, CurUser.ApplicationUserID))
'        End If
'    Else
'        Dim myCon As New CODataCon.DataControler
'        Dim myGrid As CODataCon.com.controlorigins.ws.CO_DataGrid
'        If CurUser.RoleCD.ToLower = "admin" Then
'            myGrid = myCon.GetSurveyResponseAnswers(CurUser.ApplicationID, 100000)
'        Else
'            myGrid = myCon.GetSurveyResponseAnswers(CurUser.ApplicationID, CurUser.ApplicationUserID)
'        End If
'        rptHeaderRow.DataSource = myGrid.GridColumns
'        rptHeaderRow.DataBind()
'        rptDataRows.DataSource = myGrid.GridRows
'        rptDataRows.DataBind()
'        pnlPivotData.Visible = True
'        pnlPivotTable.Visible = True
'    End If
'End Sub
'Public Property TheView As String
'Public Property PivotJS As String

'Public Function GetPivotJS() As String
'    Dim mySB As New StringBuilder("")
'    mySB.AppendLine("            <script type=""text/javascript"">")
'    mySB.AppendLine("        $(function () { ")
'    mySB.AppendLine("            var derivers = $.pivotUtilities.derivers; ")
'    mySB.AppendLine("            var renderers = $.extend($.pivotUtilities.renderers, $.pivotUtilities.c3_renderers, $.pivotUtilities.d3_renderers); ")
'    mySB.AppendLine("            var input = $(""table.pivot_data"") ")
'    mySB.AppendLine("            $(""#output"").pivotUI(input, { ")

'    mySB.AppendLine("                renderers: renderers, ")

'    Select Case ddlView.SelectedValue
'        Case "Status"
'            mySB.AppendLine("                cols: ['StatusNM'], ")
'            mySB.AppendLine("                rows: ['SurveyNM'], ")
'            mySB.AppendLine("                vals: ['SurveyResponseScore'], exclusions: { 'SurveyNM': [''] }, ")
'            mySB.AppendLine("                unusedAttrsVertical: 'true', ")
'            mySB.AppendLine("                autoSortUnusedAttrs: true, ")
'            mySB.AppendLine("                aggregatorName: 'Count', ")
'            mySB.AppendLine("                rendererName: 'Table', ")
'        Case "Survey"
'            mySB.AppendLine("                cols: ['SurveyNM'], ")
'            mySB.AppendLine("                rows: ['StatusNM'], ")
'            mySB.AppendLine("                vals: ['SurveyResponseScore'], exclusions: { 'SurveyNM': [''] }, ")
'            mySB.AppendLine("                unusedAttrsVertical: 'true', ")
'            mySB.AppendLine("                autoSortUnusedAttrs: true, ")
'            mySB.AppendLine("                aggregatorName: 'Average', ")
'            mySB.AppendLine("                rendererName: 'Bar Chart', ")
'        Case Else
'            mySB.AppendLine("                cols: ['StatusNM'], ")
'            mySB.AppendLine("                rows: ['SurveyNM'], ")
'            mySB.AppendLine("                vals: ['SurveyResponseScore'], exclusions: { 'SurveyNM': [''] }, ")
'            mySB.AppendLine("                unusedAttrsVertical: 'true', ")
'            mySB.AppendLine("                autoSortUnusedAttrs: true, ")
'            mySB.AppendLine("                aggregatorName: 'Count', ")
'            mySB.AppendLine("                rendererName: 'Table', ")
'    End Select

'    mySB.AppendLine("            } ")
'    mySB.AppendLine("           ); ")
'    mySB.AppendLine("        });                   ")
'    mySB.AppendLine("       </script>     ")

'    PivotJS = mySB.ToString()
'    Return PivotJS
'End Function



