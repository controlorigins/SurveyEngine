Imports System.Xml.Serialization
Imports System.IO
Imports System.Web
Imports ControlOrigins.COUtility

Public Class DataGridVisualizationList
    Inherits List(Of DataGridVisualization)
    Sub New()
    End Sub
    Sub New(ConfigList As String)
        If Not (String.IsNullOrWhiteSpace(ConfigList) Or ConfigList = "Empty") Then
            XMLString = ConfigList
        End If
    End Sub
    Public Property XMLString() As String
        Get
            Return SerializeObject(Me)
        End Get
        Set(value As String)
            Using mycon As New CODataCon.DataControler
                Me.Clear()
                If value <> String.Empty Then
                    Dim myList As DataGridVisualizationList = DirectCast(XmlDeserializeFromString(value, GetType(DataGridVisualizationList)), DataGridVisualizationList)
                    Me.Clear()
                    For Each myItem As DataGridVisualization In myList
                        Me.Add(myItem)
                    Next
                End If
            End Using
        End Set
    End Property
    Public Function GetConfiguration(ByVal ApplicationID As Integer, ByVal Name As String) As DataGridVisualization
        Try
            Return (From i In Me Where i.ApplicationID = ApplicationID And i.Name = Name).SingleOrDefault
        Catch ex As Exception
            Return New DataGridVisualization With {.ApplicationID = ApplicationID, .Name = Name}
        End Try
    End Function
    Public Function GetConfigLookup(ByVal ApplicationID As Integer) As List(Of String)
        Try
            Return (From i In Me Where i.ApplicationID = ApplicationID Select i.Name).ToList()
        Catch ex As Exception
            Return New List(Of String) From {CStr(ApplicationID)}
        End Try
    End Function
    Public Function GetConfiguration(ByVal ApplicationID As Integer, ByVal DataSource As DataSetName, ByVal Name As String) As DataGridVisualization
        Try
            Return ((From i In Me Where i.ApplicationID = ApplicationID And i.DataSource = DataSource And i.Name = Name).SingleOrDefault)
        Catch ex As Exception
            Return New DataGridVisualization
        End Try
    End Function
    Public Function AddToList(ByVal myConfiguration As DataGridVisualization) As Boolean
        If String.IsNullOrEmpty(myConfiguration.Name) Or String.IsNullOrEmpty(CStr(myConfiguration.ApplicationID)) Or String.IsNullOrEmpty(CStr(myConfiguration.DataSource)) Then
            Return False
        Else
            Dim myIndex As Integer = -1
            For i As Integer = 0 To Count - 1
                If Item(i).ApplicationID = myConfiguration.ApplicationID AndAlso Item(i).DataSource = myConfiguration.DataSource AndAlso Item(i).Name = myConfiguration.Name Then
                    If Item(i).Name = myConfiguration.Name Then
                        myIndex = i
                        Exit For
                    End If
                End If
            Next
            If myIndex > -1 Then
                RemoveAt(myIndex)
            End If
            Add(myConfiguration)
            Return True
        End If
    End Function
    Public Overloads Function IndexOf(ByVal item As DataGridVisualization) As Integer
        Dim myParm As DataGridVisualization = MyBase.Where(Function(c) c.ApplicationID = item.ApplicationID).FirstOrDefault
        Return MyBase.IndexOf(myParm)
    End Function
    Public Shared Function SerializeObject(Of DataGridVisualizationList)(toSerialize As DataGridVisualizationList) As String
        Dim xmlSerializer As New XmlSerializer(toSerialize.[GetType]())
        Using textWriter As New StringWriter()
            xmlSerializer.Serialize(textWriter, toSerialize)
            Return textWriter.ToString()
        End Using
    End Function
    Public Shared Function XmlDeserializeFromString(objectData As String, type As Type) As Object
        Dim serializer As XmlSerializer = New XmlSerializer(type)
        Dim result As Object

        Using reader As TextReader = New StringReader(objectData)
            result = serializer.Deserialize(reader)
        End Using

        Return result
    End Function
    Protected Overridable Function IsInList(ByVal newParm As DataGridVisualization) As Boolean
        Return Me.Contains(newParm, New DataGridVisualizationEqualityComparer)
    End Function

Function GetXML() As String
Return String.Empty
 End Function 

Sub SaveXML()

 End Sub 

End Class
