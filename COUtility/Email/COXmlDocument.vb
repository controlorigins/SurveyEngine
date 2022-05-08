Imports System.Xml

Namespace COUtility
    Public Class COXmlDocument
        Inherits XmlDocument
        Public Sub New()
        End Sub
        Public Sub New(ByVal nt As XmlNameTable)
            MyBase.New(nt)
        End Sub
        Public Shared Function GetFileAgeMinutes(ByVal path As String) As Double
            If IsValidPath(path) Then
                Return DateTime.Now.Subtract(GetFileDate(path)).TotalMinutes
            Else
                Return 100000
            End If
        End Function
        Private Shared Function IsValidPath(ByVal sPath As String) As Boolean
            Return My.Computer.FileSystem.FileExists(sPath)
        End Function
        Private Shared Function GetFileDate(ByVal path As String) As Date
            Return IO.File.GetLastWriteTime(path)
        End Function
    End Class
End Namespace