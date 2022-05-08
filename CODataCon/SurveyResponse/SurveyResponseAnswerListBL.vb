Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports CODataCon.com.controlorigins.ws

<Serializable()> Public Class SurveyResponseAnswerListBL

    Private _SurveyResponseAnswerItemList As New List(Of SurveyResponseAnswerItem)
    Private SearchQuestionID As Integer
    Private SearchSequenceNumber As Integer

    Sub New(surveyResponseAnswerItem As SurveyResponseAnswerItem())
        _SurveyResponseAnswerItemList = surveyResponseAnswerItem.ToList()
    End Sub

    Public WriteOnly Property SurveyResponseAnswerList As List(Of SurveyResponseAnswerItem) 
        Set(value As List(Of SurveyResponseAnswerItem))
            _SurveyResponseAnswerItemList.Clear()
            _SurveyResponseAnswerItemList.AddRange(value)
        End Set
    End Property
    Public Sub New(thiSurveyResponseAnswerItemList As List(Of SurveyResponseAnswerItem))
        _SurveyResponseAnswerItemList = thiSurveyResponseAnswerItemList
    End Sub
    Public Sub New()
        _SurveyResponseAnswerItemList.Clear()
    End Sub

    Public Function GetSurveyResponseAnswerList() As List(Of SurveyResponseAnswerItem)
        Return _SurveyResponseAnswerItemList
    End Function

    Public Function FindScoreByQuestionID(ByVal QuestionID As Integer, ByVal SequenceNumber As Integer) As Double
        Dim dbReturn As Double = 0
        For Each myAnswer In FindAnswersByQuestionID(QuestionID, SequenceNumber)
            dbReturn = dbReturn + (myAnswer.QuestionAnswerValue * myAnswer.QuestionValue)
        Next
        Return dbReturn
    End Function
    Public Function FindAnswersByQuestionID(ByVal QuestionID As Integer) As List(Of SurveyResponseAnswerItem)
        SearchQuestionID = QuestionID
        Return _SurveyResponseAnswerItemList.FindAll(AddressOf FindQuestionByQuestionID)
    End Function

    Public Function FindAnswersByQuestionID(ByVal QuestionID As Integer, ByVal SequenceNumber As Integer) As List(Of SurveyResponseAnswerItem)
        SearchQuestionID = QuestionID
        SearchSequenceNumber = SequenceNumber
        Return _SurveyResponseAnswerItemList.FindAll(AddressOf FindQuestionBySequenceNumberQuestionID)
    End Function
    Private Function FindQuestionBySequenceNumberQuestionID(ByVal Question As SurveyResponseAnswerItem) As Boolean
        If Question.QuestionID = SearchQuestionID AndAlso Question.SequenceNumber = SearchSequenceNumber Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function FindQuestionByQuestionID(ByVal Question As SurveyResponseAnswerItem) As Boolean
        If Question.QuestionID = SearchQuestionID Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetXML() As String
        Dim oXS As XmlSerializer = New XmlSerializer(Me.GetType)
        Using writer As New StringWriter()
            oXS.Serialize(writer, Me)
            Return writer.ToString
        End Using

    End Function
    Public Shared Function GetFromXML(ByVal UserXML As String) As SurveyResponseAnswerListBL
        Dim myObject As New SurveyResponseAnswerListBL
        If Not (UserXML Is Nothing) And Not (UserXML = String.Empty) Then
            Using read As StringReader = New StringReader(UserXML)
                Dim serializer As New XmlSerializer(myObject.GetType())
                Using reader As XmlReader = New XmlTextReader(read)
                    Try
                        myObject = DirectCast(serializer.Deserialize(reader), SurveyResponseAnswerListBL)
                    Catch
                        Throw
                    End Try
                End Using
            End Using
        End If
        Return myObject
    End Function

End Class