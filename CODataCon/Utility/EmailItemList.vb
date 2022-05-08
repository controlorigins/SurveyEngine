Imports System.IO
Imports System.Xml.Serialization

Public Class EmailItemList
    Inherits List(Of EmailItem)
    Public Function SaveXML(ByVal FileName As String) As String
        FileName = AppUtility.FixInvalidCharacters(FileName)
        Try
            Dim myXML As New SurveyXmlDocument()
            If Me Is Nothing Then
                Return String.Empty
            Else
                Dim oXS As XmlSerializer = New XmlSerializer(GetType(EmailItemList))
                Using writer As New StringWriter()
                    oXS.Serialize(writer, Me)
                    myXML.LoadXml(writer.ToString)
                End Using
                myXML.Save(String.Format("c:\SPSurvey\xml\{0}-EmailList.xml", FileName))
            End If
        Catch ex As Exception
            Return "ERROR"
        End Try
        Return String.Empty
    End Function
End Class
