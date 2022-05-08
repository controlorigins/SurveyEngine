Imports System.Web.Services
Imports SPSurvey.Core
Imports System.Collections.Generic
Imports LINQHelper.System.Linq.Dynamic
Imports ControlOrigins.COUtility

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://ws.controlorigins.com/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class DataAnalysis
    Inherits WebService
    Public ReadOnly Property str_ConnectionString As String
        Get
            Return ConfigurationManager.ConnectionStrings("SPSurveyConnectionString").ConnectionString
        End Get
    End Property

#Region "Get DataGrids for Analytics/Reporting"

    Private Property DataSource As String

    <WebMethod()> _
    Public Function GetSurveyResponseSummaryGrid(ByVal Filters As SQLFilterList, ByVal myGuid As Guid) As CO_DataGrid
        Dim myGRID As New CO_DataGrid
        Dim myDic As New Dictionary(Of String, String)
        Dim myValue As String = String.Empty
        Dim curSRID As Integer = 0
        If ValidateGUID(myGuid) Then
            ApplicationLogging.AuditLog("Service.GetSurveyResponseSummaryGrid", DataSource)

            Try
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyResponseID", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyResponseNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "AccountNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ApplicationNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyShortNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "LastNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "FirstNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "DataSource", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "eMailAddress", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "AnswerCount", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "CommentCount", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "DaySinceModified", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionCount", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "StatusID", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "StatusNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyResponseScore", .DataType = "double", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Float})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "AverageQuestionScore", .DataType = "double", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Float})
                Using myCON As New DataController(str_ConnectionString)
                    Dim myWhere As String = Filters.GetLINQWhere()
                    Dim mySRList = (From i In myCON.vwContextQuestionAnswers.Where(myWhere)).ToList
                    For Each myContext In (From i In mySRList Select i).Distinct
                        If Not String.IsNullOrEmpty(myContext.QuestionNM) AndAlso Not myDic.ContainsKey(myContext.QuestionNM) Then
                            myDic.Add(myContext.QuestionNM, myContext.QuestionNM)
                            myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DataType = myContext.AnswerType, .DisplayName = myContext.QuestionNM, .SourceName = "Context"})
                        End If
                    Next

                    Dim mySRSumary = (From i In myCON.vwApplicationSurveyResponseSummaries.Where(Filters.GetLINQWhere)).ToList
                    For Each mySRA In mySRSumary
                        If mySRA.SurveyResponseID <> curSRID Then
                            Dim myRow As New SPSurvey.Core.CO_DataGrid.GridRow
                            myRow.name = String.Format("{0}", mySRA.SurveyResponseNM)
                            For i As Integer = 0 To myGRID.GridColumns.Count - 1
                                myGRID.GridColumns(i).Index = i
                            Next
                            For Each myCol In myGRID.GridColumns
                                myValue = String.Empty
                                Select Case myCol.SourceName
                                    Case "SurveyResponse"
                                        myValue = myGRID.GetPropertyValue(mySRA, myCol.DisplayName)
                                    Case "Summary"
                                        myValue = myGRID.GetPropertyValue(mySRA, myCol.DisplayName)
                                    Case "Context"
                                        If myCol.DataType = "QuestionAnswerID" Then
                                            myValue = (From i In mySRList Where i.SurveyResponseID = mySRA.SurveyResponseID And i.QuestionNM = myDic(myCol.DisplayName) Select i.QuestionAnswerNM).FirstOrDefault
                                        Else
                                            myValue = (From i In mySRList Where i.SurveyResponseID = mySRA.SurveyResponseID And i.QuestionNM = myDic(myCol.DisplayName) Select i.AnswerComment).FirstOrDefault
                                        End If
                                    Case Else
                                        ' Do Nothing 
                                End Select
                                myRow.Value.Add(myValue)
                                myCol.UpdateDictionary(myValue)
                            Next
                            myGRID.GridRows.Add(myRow)
                        End If
                        curSRID = mySRA.SurveyResponseID
                    Next
                    For Each myCol In myGRID.GridColumns
                        myCol.SetCommonValues()
                    Next
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSurveyResponseSummaryGrid")
            End Try
        End If
        Return myGRID
    End Function
    <WebMethod()> _
    Public Function GetSurveyResponseGroupGrid(ByVal Filters As SQLFilterList, ByVal myGuid As Guid) As CO_DataGrid
        Dim myGRID As New CO_DataGrid
        Dim myDic As New Dictionary(Of String, String)
        Dim myValue As String = String.Empty
        Dim curSRID As Integer = 0
        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.GetSurveyResponseGroupGrid", DataSource)
            Try
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyResponseID", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyResponseNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "AccountNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ApplicationNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyShortNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "LastNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "FirstNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "DataSource", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "eMailAddress", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionGroupNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionGroupShortNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionGroupWeight", .DataType = "double", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Percent})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "AnswerCount", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "CommentCount", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "DaySinceModified", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionCount", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "StatusID", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "StatusNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyResponseScore", .DataType = "double", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Float})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyResponseGroupScore", .DataType = "double", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Float})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "AverageQuestionScore", .DataType = "double", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Float})
                Using myCON As New DataController(str_ConnectionString)
                    Dim mySRList = (From i In myCON.vwContextQuestionAnswers.Where(Filters.GetLINQWhere)).ToList
                    For Each myContext In (From i In mySRList Select i).Distinct
                        If Not String.IsNullOrEmpty(myContext.QuestionNM) AndAlso Not myDic.ContainsKey(myContext.QuestionNM) Then
                            myDic.Add(myContext.QuestionNM, myContext.QuestionNM)
                            myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DataType = myContext.AnswerType, .DisplayName = myContext.QuestionNM, .SourceName = "Context"})
                        End If
                    Next
                    Dim mySRGroupAnswers = (From i In myCON.vwSurveyResponseGroupSummaries.Where(Filters.GetLINQWhere)).ToList
                    For Each mySRGroupAnswer In mySRGroupAnswers
                        Dim myRow As New SPSurvey.Core.CO_DataGrid.GridRow
                        myRow.name = String.Format("{0}", mySRGroupAnswer.SurveyResponseNM)
                        For i As Integer = 0 To myGRID.GridColumns.Count - 1
                            myGRID.GridColumns(i).Index = i
                        Next
                        For Each myCol In myGRID.GridColumns
                            myValue = String.Empty
                            Select Case myCol.SourceName
                                Case "SurveyResponse"
                                    myValue = myGRID.GetPropertyValue(mySRGroupAnswer, myCol.DisplayName)
                                Case "Summary"
                                    myValue = myGRID.GetPropertyValue(mySRGroupAnswer, myCol.DisplayName)
                                Case "Context"
                                    If myCol.DataType = "QuestionAnswerID" Then
                                        myValue = (From i In mySRList Where i.SurveyResponseID = mySRGroupAnswer.SurveyResponseID And i.QuestionNM = myDic(myCol.DisplayName) Select i.QuestionAnswerNM).FirstOrDefault
                                    Else
                                        myValue = (From i In mySRList Where i.SurveyResponseID = mySRGroupAnswer.SurveyResponseID And i.QuestionNM = myDic(myCol.DisplayName) Select i.AnswerComment).FirstOrDefault
                                    End If
                                Case Else
                                    ' Do Nothing 
                            End Select
                            myRow.Value.Add(myValue)
                            myCol.UpdateDictionary(myValue)
                        Next
                        myGRID.GridRows.Add(myRow)
                    Next
                    For Each myCol In myGRID.GridColumns
                        myCol.SetCommonValues()
                    Next

                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSurveyResponseGroupGrid")
            End Try

        End If
        Return myGRID
    End Function
    <WebMethod()> _
    Public Function GetSurveyResponseAnswersGrid(ByVal Filters As SQLFilterList, ByVal myGuid As Guid) As CO_DataGrid
        Dim myGRID As New CO_DataGrid
        Dim myDic As New Dictionary(Of String, String)
        Dim curSRID As Integer = 0
        Dim myValue As String = String.Empty

        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.GetSurveyResponseAnswersGrid", DataSource)
            Try
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyResponseID", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyResponseNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ApplicationNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyShortNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "LastNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "FirstNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "DataSource", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "eMailAddress", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionGroupNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionGroupShortNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionGroupWeight", .DataType = "double", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Percent})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionDS", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionShortNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionWeight", .DataType = "double", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Percent})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "StatusNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyResponseScore", .DataType = "double", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Float})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyResponseGroupScore", .DataType = "double", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Float})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "AverageQuestionScore", .DataType = "double", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Float})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionScore", .DataType = "double", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "AnswerComment", .DataType = "double", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "AccountNM", .DataType = "string", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "AnswerCount", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "CommentCount", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "DaySinceModified", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionCount", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "StatusID", .DataType = "integer", .SourceName = "Summary", .ColumnDisplayFormat = DisplayFormat.Number})
                Using myCON As New DataController(str_ConnectionString)
                    Dim mySRList = (From i In myCON.vwContextQuestionAnswers.Where(Filters.GetLINQWhere)).ToList
                    For Each myContext In (From i In mySRList Select i).Distinct
                        If Not String.IsNullOrEmpty(myContext.QuestionNM) AndAlso Not myDic.ContainsKey(myContext.QuestionNM) Then
                            myDic.Add(myContext.QuestionNM, myContext.QuestionNM)
                            myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DataType = myContext.AnswerType, .DisplayName = myContext.QuestionNM, .SourceName = "Context"})
                        End If
                    Next
                    Dim mySRDetails = (From i In myCON.vwSurveyResponseDetails.Where(Filters.GetLINQWhere)).ToList
                    For Each mySRAnswer In mySRDetails
                        Dim myRow As New SPSurvey.Core.CO_DataGrid.GridRow
                        myRow.name = String.Format("{0}", mySRAnswer.SurveyResponseNM)
                        For i As Integer = 0 To myGRID.GridColumns.Count - 1
                            myGRID.GridColumns(i).Index = i
                        Next

                        For Each myCol In myGRID.GridColumns
                            myValue = String.Empty
                            Select Case myCol.SourceName
                                Case "SurveyResponse"
                                    myValue = myGRID.GetPropertyValue(mySRAnswer, myCol.DisplayName)
                                Case "Summary"
                                    myValue = myGRID.GetPropertyValue(mySRAnswer, myCol.DisplayName)
                                Case "Context"
                                    If myCol.DataType = "QuestionAnswerID" Then
                                        myValue = (From i In mySRList Where i.SurveyResponseID = mySRAnswer.SurveyResponseID And i.QuestionNM = myDic(myCol.DisplayName) Select i.QuestionAnswerNM).FirstOrDefault
                                    Else
                                        myValue = (From i In mySRList Where i.SurveyResponseID = mySRAnswer.SurveyResponseID And i.QuestionNM = myDic(myCol.DisplayName) Select i.AnswerComment).FirstOrDefault
                                    End If
                                Case Else
                                    ' Do Nothing 
                            End Select
                            myRow.Value.Add(myValue)
                            myCol.UpdateDictionary(myValue)
                        Next
                        myGRID.GridRows.Add(myRow)
                    Next
                    If myGRID.GridRows.Count > 0 Then
                        For Each myCol In myGRID.GridColumns
                            myCol.SetCommonValues()
                        Next
                    End If
                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSurveyResponseAnswersGrid")
            End Try
        End If
        Return myGRID
    End Function
    <WebMethod()> _
    Public Function GetApplicationGrid(ByVal myGUID As Guid) As CO_DataGrid
        Dim myGRID As New CO_DataGrid
        Dim myValue As String = String.Empty

        If ValidateGUID(myGUID) Then
            AppLog.AuditLog("Service.GetApplicationGrid", DataSource)
            Try
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ApplicationID", .DataType = "integer", .SourceName = "Application", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ApplicationNM", .DataType = "string", .SourceName = "Application", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ApplicationDS", .DataType = "memo", .SourceName = "Application", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ApplicationCD", .DataType = "string", .SourceName = "Application", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ApplicationShortNM", .DataType = "string", .SourceName = "Application", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ApplicationTypeNM", .DataType = "string", .SourceName = "Application", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ApplicationTypeID", .DataType = "integer", .SourceName = "Application", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "CompanyID", .DataType = "integer", .SourceName = "Application", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "CompanyNM", .DataType = "string", .SourceName = "Application", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "MenuOrder", .DataType = "integer", .SourceName = "Application", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "UserCount", .DataType = "integer", .SourceName = "Application", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyCount", .DataType = "integer", .SourceName = "Application", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyResponseCount", .DataType = "integer", .SourceName = "Application", .ColumnDisplayFormat = DisplayFormat.Number})

                Using mycon As New DataController(str_ConnectionString)
                    For Each myApplication In (From i In mycon.Application_SelectSummary Where i.ApplicationID > 20 Select i).ToArray()
                        Dim myRow As New SPSurvey.Core.CO_DataGrid.GridRow
                        myRow.name = String.Format("{0}", myApplication.ApplicationNM)

                        For i As Integer = 0 To myGRID.GridColumns.Count - 1
                            myGRID.GridColumns(i).Index = i
                        Next

                        For Each myCol In myGRID.GridColumns
                            Select Case myCol.SourceName
                                Case "Application"
                                    myRow.Value.Add(myGRID.GetPropertyValue(myApplication, myCol.DisplayName))
                                Case Else
                                    ' Do Nothing 
                            End Select
                            myCol.UpdateDictionary(myGRID.GetPropertyValue(myApplication, myCol.DisplayName))

                        Next
                        myGRID.GridRows.Add(myRow)
                    Next
                    For Each myCol In myGRID.GridColumns
                        myCol.SetCommonValues()
                    Next

                End Using
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetApplicationGrid")
            End Try
        End If
        Return myGRID
    End Function
    <WebMethod()> _
    Public Function GetSurveySummaryGrid(ByVal Filters As SQLFilterList, ByVal myGuid As Guid) As CO_DataGrid
        Dim myGRID As New CO_DataGrid
        Dim mySurveyList As New List(Of SurveyItem)
        Dim myDic As New Dictionary(Of String, String)
        Dim curSRID As Integer = 0
        Dim myValue As String = String.Empty

        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.GetSurveySummaryGrid", DataSource)
            Try
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyID", .DataType = "integer", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyType.SurveyTypeID", .DataType = "integer", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyType.SurveyTypeNM", .DataType = "string", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "UseSurveyGroupsFL", .DataType = "flag", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyNM", .DataType = "string", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyShortNM", .DataType = "string", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyDS", .DataType = "memo", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "StartDT", .DataType = "date", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "EndDT", .DataType = "date", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ParentSurveyID", .DataType = "integer", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Hidden})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ParentSurveyNM", .DataType = "string", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "CompletionMessage", .DataType = "memo", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "AutoAssignFilter", .DataType = "memo", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ReviewerAccountNM", .DataType = "string", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ResponseNMTemplate", .DataType = "string", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ApplicationCount", .DataType = "integer", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyResponseCount", .DataType = "integer", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionCount", .DataType = "integer", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionGroupCount", .DataType = "integer", .SourceName = "Survey", .ColumnDisplayFormat = DisplayFormat.Number})
                Using myController As New DataController(str_ConnectionString)
                    mySurveyList.AddRange(From i In myController.GetSurveySummary().ToList())
                End Using
                For Each mySurvey In mySurveyList
                    Dim myRow As New SPSurvey.Core.CO_DataGrid.GridRow
                    myRow.name = String.Format("{0}", mySurvey.SurveyNM)
                    For i As Integer = 0 To myGRID.GridColumns.Count - 1
                        myGRID.GridColumns(i).Index = i
                    Next

                    For Each myCol In myGRID.GridColumns
                        myRow.Value.Add(myGRID.GetPropertyValue(mySurvey, myCol.DisplayName))
                        myCol.UpdateDictionary(myGRID.GetPropertyValue(mySurvey, myCol.DisplayName))

                    Next
                    myGRID.GridRows.Add(myRow)
                Next
                For Each myCol In myGRID.GridColumns
                    myCol.SetCommonValues()
                Next

            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetSurveySummaryGrid")
            End Try
        End If
        Return myGRID
    End Function
    <WebMethod()> _
    Public Function GetQuestionList(ByVal Filters As SQLFilterList, ByVal myGuid As Guid) As CO_DataGrid
        Dim myGRID As New CO_DataGrid
        Dim myQuestionList As New List(Of vwQuestionLibrary)
        Dim myDic As New Dictionary(Of String, String)
        Dim curSRID As Integer = 0
        Dim myValue As String = String.Empty

        If ValidateGUID(myGuid) Then
            AppLog.AuditLog("Service.GetQuestionList", String.Format("Filter:{0}", Filters.GetWhereClause))
            Try
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionID", .DataType = "ID", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionShortNM", .DataType = "string", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionNM", .DataType = "string", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionDS", .DataType = "memo", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionSort", .DataType = "integer", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ReviewRoleLevel", .DataType = "integer", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionValue", .DataType = "integer", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyTypeID", .DataType = "ID", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyTypeShortNM", .DataType = "string", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyTypeNM", .DataType = "string", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionTypeID", .DataType = "ID", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "QuestionTypeCD", .DataType = "string", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "AnswerDataType", .DataType = "string", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Text})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "AnswerCount", .DataType = "integer", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "SurveyCount", .DataType = "integer", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "ResponseAnswerCount", .DataType = "integer", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "MinScore", .DataType = "integer", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Number})
                myGRID.GridColumns.Add(New SPSurvey.Core.CO_DataGrid.GridColumn With {.DisplayName = "MaxScore", .DataType = "integer", .SourceName = "Question", .ColumnDisplayFormat = DisplayFormat.Number})
                Using myController As New DataController(str_ConnectionString)
                    If Filters.Count > 0 Then
                        myQuestionList = (From i In myController.vwQuestionLibraries.Where(Filters.GetLINQWhere) Select i).ToList
                    Else
                        myQuestionList = (From i In myController.vwQuestionLibraries.Take(1000) Select i).ToList
                    End If
                End Using
                For Each myQuestion In myQuestionList
                    Dim myRow As New SPSurvey.Core.CO_DataGrid.GridRow
                    myRow.name = String.Format("{0}", myQuestion.QuestionNM)
                    For i As Integer = 0 To myGRID.GridColumns.Count - 1
                        myGRID.GridColumns(i).Index = i
                    Next
                    For Each myCol In myGRID.GridColumns
                        myRow.Value.Add(myGRID.GetPropertyValue(myQuestion, myCol.DisplayName))
                        myCol.UpdateDictionary(myGRID.GetPropertyValue(myQuestion, myCol.DisplayName))
                    Next
                    myGRID.GridRows.Add(myRow)
                Next

                For Each myCol In myGRID.GridColumns
                    myCol.SetCommonValues()
                Next

            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "Service.GetQuestionList")
            End Try
        End If
        Return myGRID
    End Function

#End Region

    Private Function ValidateGUID(ByRef myGuid As Guid) As Boolean
        If myGuid = Guid.Parse("FFB6791E-39BA-404A-BA86-B2C3210CD259") Then
            DataSource = "ControlOriginsMetro"
            Return True
        ElseIf myGuid = Guid.Parse("85AAA903-3C57-4FB0-B91D-B46633C7C637") Then
            DataSource = "CODataCon"
            Return True
        Else
            AppLog.AuditLog(String.Format("Invalid GUID:{0}", myGuid), "Service.ValidateGUID")
            Return False
        End If
    End Function

End Class
