Imports System.IO
Imports System.Xml.Serialization

Namespace COUtility

Public Class EmailItem
    Public Property ToAccount As String
    Public Property ToEmailAddress As String
    Public Property FromAccount As String
    Public Property FromEmailAdderss As String
    Public Property CCAccount As String
    Public Property CCEmailAddress As String
    Public Property BCCAccount As String
    Public Property BCCEmailAddress As String
    Public Property EmailSubject As String
    Public Property EmailBody As String
    Public Property ContentType As String


    Public Function SaveXML() As String
        Try
            Dim myXML As New COXmlDocument()
            If Me Is Nothing Then
                Return String.Empty
            Else
                Dim oXS As XmlSerializer = New XmlSerializer(GetType(EmailItem))
                Using writer As New StringWriter()
                    oXS.Serialize(writer, Me)
                    myXML.LoadXml(writer.ToString)
                End Using
                myXML.Save(String.Format("c:\SPSurvey\xml\{0}-Email-{1}.xml", EmailSubject, Utility.FixInvalidCharacters(Now.ToLongTimeString)))
            End If
        Catch ex As Exception
            Return "ERROR"
        End Try
        Return String.Empty
    End Function



End Class
End Namespace