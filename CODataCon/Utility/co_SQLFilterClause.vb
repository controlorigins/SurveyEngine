'Imports Microsoft.VisualBasic
'Imports CODataCon.com.controlorigins.ws
'Imports System.Text

'Public Class co_SQLFilterClause
'    Inherits CODataCon.com.controlorigins.ws.SQLFilterClause
'    Public ReadOnly Property ClauseConjunction As String
'        Get
'            Select Case Conjunction
'                Case SQLFilterConjunction.andConjunction
'                    Return " and "
'                Case SQLFilterConjunction.orConjunction
'                    Return " or "
'                Case Else
'                    Return " and "
'            End Select
'        End Get
'    End Property
'    Public ReadOnly Property Statement As String
'        Get
'            If Field = String.Empty Then
'                Return Argument
'            Else
'                Select Case FieldOperator
'                    Case SQLFilterOperator.Equal
'                        If IsNumeric(Argument) Then
'                            Return String.Format(" {0} {1} {2} ", Field, " = ", Argument)
'                        Else
'                            Return String.Format(" {0}.Contains(""{1}"") ", Field, Argument)
'                        End If
'                    Case SQLFilterOperator.NotEqual
'                        Return String.Format(" {0} {1} '{2}' ", Field, " <> ", Argument)
'                    Case SQLFilterOperator.LessThanEqual
'                        Return String.Format(" {0} {1} '{2}' ", Field, " <= ", Argument)
'                    Case SQLFilterOperator.LessThan
'                        Return String.Format(" {0} {1} '{2}' ", Field, " < ", Argument)
'                    Case SQLFilterOperator.GreaterThanEqual
'                        Return String.Format(" {0} {1} '{2}' ", Field, " >= ", Argument)
'                    Case SQLFilterOperator.GreaterThan
'                        Return String.Format(" {0} {1} '{2}' ", Field, " > ", Argument)
'                    Case SQLFilterOperator.dbLike
'                        Return String.Format(" {0} {1} '{2}' ", Field, " like ", Argument)
'                    Case SQLFilterOperator.dbIn
'                        Return String.Format(" {0} {1} ({2}) ", Field, " in ", Argument)
'                    Case SQLFilterOperator.dbBetween
'                        Return String.Format(" {0} {1} {2} ", Field, " between ", Argument)
'                    Case SQLFilterOperator.dbIsNotNull
'                        Return String.Format(" {0} is not null ", Field)
'                    Case SQLFilterOperator.dbIsNull
'                        Return String.Format(" {0} is null ", Field)
'                    Case Else
'                        Return String.Format(" {0} {1} '{2}' ", Field, " = ", Argument)
'                End Select
'            End If
'        End Get
'    End Property
'    Public Sub New(ByVal myField As String, ByVal myOperator As SQLFilterOperator, ByVal myArgument As String, ByVal myConjunction As SQLFilterConjunction, ByVal myFieldType As String)
'        Field = myField
'        FieldOperator = myOperator
'        Argument = myArgument
'        Conjunction = myConjunction
'        FieldType = myFieldType
'    End Sub
'    Public Sub New()

'    End Sub

'    Public Enum SQLFilterOperator
'        Equal
'        NotEqual
'        GreaterThan
'        LessThan
'        GreaterThanEqual
'        LessThanEqual
'        dbLike
'        dbIn
'        dbBetween
'        dbIsNull
'        dbIsNotNull
'    End Enum

'    Public Enum SQLFilterConjunction
'        andConjunction
'        orConjunction
'    End Enum



'End Class

'Public Class co_SQLClauseList
'    Inherits List(Of co_SQLFilterClause)

'    Public Sub New()
'    End Sub
'    Public Sub New(ByVal capacity As Integer)
'        MyBase.New(capacity)
'    End Sub
'    Public Sub New(ByVal collection As IEnumerable(Of co_SQLFilterClause))
'        MyBase.New(collection)
'    End Sub
'    Private SearchField As String
'    Private Function FindClauseByField(ByVal FilterClause As co_SQLFilterClause) As Boolean
'        If FilterClause.Field = SearchField Then
'            Return True
'        Else
'            Return False
'        End If
'    End Function
'    Public Function FindField(ByVal reqSearchField As String) As List(Of co_SQLFilterClause)
'        SearchField = reqSearchField
'        Return FindAll(AddressOf FindClauseByField)
'    End Function

'    Public Function GetWhereClause() As String
'        Dim myReturn As StringBuilder = New StringBuilder(String.Empty)
'        Dim iLoopIndex As Integer
'        If Count = 1 Then
'            myReturn.Append(String.Format("where {0} {1}", Me(0).Statement, vbCrLf))
'        ElseIf Count > 1 Then
'            For iLoopIndex = 0 To (Count - 1)
'                If iLoopIndex = 0 Then
'                    myReturn.Append(String.Format("where {0} {1}", Me(iLoopIndex).Statement, vbCrLf))
'                Else
'                    myReturn.Append(String.Format("{1}  {0} {2}", Me(iLoopIndex).Statement, Me(iLoopIndex).ClauseConjunction, vbCrLf))
'                End If
'            Next iLoopIndex
'        End If
'        Return myReturn.ToString
'    End Function

'    Public Function GetLINQWhere() As String
'        Dim myReturn As StringBuilder = New StringBuilder(String.Empty)
'        Dim iLoopIndex As Integer
'        If Count = 1 Then
'            myReturn.Append(String.Format("{0} {1}", Me(0).Statement, String.Empty))
'        ElseIf Count > 1 Then
'            For iLoopIndex = 0 To (Count - 1)
'                If iLoopIndex = 0 Then
'                    myReturn.Append(String.Format(" {0} {1}", Me(iLoopIndex).Statement, String.Empty))
'                Else
'                    myReturn.Append(String.Format("{1}  {0} {2}", Me(iLoopIndex).Statement, Me(iLoopIndex).ClauseConjunction, String.Empty))
'                End If
'            Next iLoopIndex
'        End If
'        Return myReturn.ToString
'    End Function

'    Public Function GetWhereClause(ByVal FieldType As String) As String
'        Dim myReturn As StringBuilder = New StringBuilder(String.Empty)
'        Dim iClauseCount As Integer = 0
'        Dim iLoopIndex As Integer
'        If Count = 1 Then
'            If Me(0).FieldType = FieldType Then
'                myReturn.Append(String.Format("where {0} {1}", Me(0).Statement, vbCrLf))
'                iClauseCount = iClauseCount + 1
'            End If
'        ElseIf Count > 1 Then
'            For iLoopIndex = 0 To (Count - 1)
'                If Me(iLoopIndex).FieldType = FieldType Then
'                    If iClauseCount = 0 Then
'                        myReturn.Append(String.Format("where {0} {1}", Me(iLoopIndex).Statement, vbCrLf))
'                        iClauseCount = iClauseCount + 1
'                    Else
'                        myReturn.Append(String.Format("{1}  {0} {2}", Me(iLoopIndex).Statement, Me(iLoopIndex).ClauseConjunction, vbCrLf))
'                        iClauseCount = iClauseCount + 1
'                    End If
'                End If
'            Next iLoopIndex
'        End If
'        Return myReturn.ToString
'    End Function

'End Class