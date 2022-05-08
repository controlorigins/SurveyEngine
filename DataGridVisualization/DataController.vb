Imports DataGridVisualization.ControlOriginsWS

Public Class DataController
    Implements IDisposable

    Public myWS As New ControlOriginsWS.DataAnalysis

    Public ReadOnly Property myGUID As Guid
        Get
            Return Guid.Parse("85AAA903-3C57-4FB0-B91D-B46633C7C637")
        End Get
    End Property

    Public Function GetApplicationGrid() As CO_DataGrid
        Return myWS.GetApplicationGrid(myGUID)
    End Function



    #Region "Get Data Grids for Reporting / Analysis"
    Public Function GetQuestions(ByVal SurveyTypeID As Integer) As CO_DataGrid
        Dim myFilterList As New List(Of SQLFilterClause)
        myFilterList.Add(New SQLFilterClause With {.Argument = CStr(SurveyTypeID),
                                                   .Conjunction = SQLFilterConjunction.andConjunction,
                                                   .Field = "SurveyTypeID ",
                                                   .FieldOperator = SQLFilterOperator.Equal,
                                                   .FieldType = "Question"})
        Return myWS.GetQuestionList(myFilterList.ToArray, myGUID)
    End Function
    Public Function GetQuestions() As CO_DataGrid
        Dim myFilterList As New List(Of SQLFilterClause)
        Return myWS.GetQuestionList(myFilterList.ToArray, myGUID)
    End Function
    Public Function GetQuestions(myFilterList As List(Of SQLFilterClause)) As CO_DataGrid
        Return myWS.GetQuestionList(myFilterList.ToArray, myGUID)
    End Function

    Public Function GetSurveySummaryGrid() As CO_DataGrid
        Dim myFilterList As New List(Of SQLFilterClause)
        Return myWS.GetSurveySummaryGrid(myFilterList.ToArray, myGUID)
    End Function

    Public Function GetSurveyResponseAnswers(ByVal ApplicationID As Integer, ByVal ApplicationUserID As Integer) As CO_DataGrid
        Dim myFilterList As New List(Of SQLFilterClause)
        myFilterList.Add(New SQLFilterClause With {.Argument = CStr(ApplicationID),
                                                   .Conjunction = SQLFilterConjunction.andConjunction,
                                                   .Field = "ApplicationID",
                                                   .FieldOperator = SQLFilterOperator.Equal,
                                                   .FieldType = "Application"})
        If ApplicationUserID < 9999 Then
            myFilterList.Add(New SQLFilterClause With {.Argument = CStr(ApplicationUserID),
                                                       .Conjunction = SQLFilterConjunction.andConjunction,
                                                       .Field = "ApplicationUserID",
                                                       .FieldOperator = SQLFilterOperator.Equal,
                                                       .FieldType = "Application"})
        End If
        Return myWS.GetSurveyResponseAnswersGrid(myFilterList.ToArray, myGUID)
    End Function

    Public Function GetSurveyResponseAnswersByQuestionID(ByVal QuestionID As Integer) As CO_DataGrid
        Dim myFilterList As New List(Of SQLFilterClause)
        myFilterList.Add(New SQLFilterClause With {.Argument = CStr(QuestionID),
                                                   .Conjunction = SQLFilterConjunction.andConjunction,
                                                   .Field = "QuestionID",
                                                   .FieldOperator = SQLFilterOperator.Equal,
                                                   .FieldType = "Application"})
        Return myWS.GetSurveyResponseAnswersGrid(myFilterList.ToArray, myGUID)
    End Function


    Public Function GetSurveyResponseGroupSummary(ByVal ApplicationID As Integer, ByVal ApplicationUserID As Integer) As CO_DataGrid
        Dim myFilterList As New List(Of SQLFilterClause)
        myFilterList.Add(New SQLFilterClause With {.Argument = CStr(ApplicationID),
                                                   .Conjunction = SQLFilterConjunction.andConjunction,
                                                   .Field = "ApplicationID",
                                                   .FieldOperator = SQLFilterOperator.Equal,
                                                   .FieldType = "Application"})
        If ApplicationUserID < 9999 Then
            myFilterList.Add(New SQLFilterClause With {.Argument = CStr(ApplicationUserID),
                                                       .Conjunction = SQLFilterConjunction.andConjunction,
                                                       .Field = "ApplicationUserID",
                                                       .FieldOperator = SQLFilterOperator.Equal,
                                                       .FieldType = "Application"})
        End If
        Return myWS.GetSurveyResponseGroupGrid(myFilterList.ToArray, myGUID)
    End Function

    Public Function GetSurveyResponseSummaryBySurveyID(ByVal SurveyID As Integer) As CO_DataGrid
        Dim myFilterList As New List(Of SQLFilterClause)
        myFilterList.Add(New SQLFilterClause With {.Argument = CStr(SurveyID),
                                                   .Conjunction = SQLFilterConjunction.andConjunction,
                                                   .Field = "SurveyID",
                                                   .FieldOperator = SQLFilterOperator.Equal,
                                                   .FieldType = "Application"})
        Return myWS.GetSurveyResponseSummaryGrid(myFilterList.ToArray, myGUID)
    End Function


    Public Function GetSurveyResponseSummary(ByVal ApplicationID As Integer, ByVal ApplicationUserID As Integer) As CO_DataGrid
        Dim myFilterList As New List(Of SQLFilterClause)
        myFilterList.Add(New SQLFilterClause With {.Argument = CStr(ApplicationID),
                                                   .Conjunction = SQLFilterConjunction.andConjunction,
                                                   .Field = "ApplicationID",
                                                   .FieldOperator = SQLFilterOperator.Equal,
                                                   .FieldType = "Application"})
        If ApplicationUserID < 9999 Then
            myFilterList.Add(New SQLFilterClause With {.Argument = CStr(ApplicationUserID),
                                                       .Conjunction = SQLFilterConjunction.andConjunction,
                                                       .Field = "ApplicationUserID",
                                                       .FieldOperator = SQLFilterOperator.Equal,
                                                       .FieldType = "Application"})
        End If
        Return myWS.GetSurveyResponseSummaryGrid(myFilterList.ToArray, myGUID)
    End Function

#End Region





#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls
    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
