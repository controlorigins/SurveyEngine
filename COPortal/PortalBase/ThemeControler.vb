Imports Microsoft.VisualBasic

Public Module ThemeControler




    Public Function GetAllThemes(path As String) As List(Of String)
        Dim mydir As IO.DirectoryInfo = New IO.DirectoryInfo(path)
        Return (From i In mydir.GetFiles("*.css") Select name = i.Name.Replace(".css", "") Order By name).ToList
    End Function




End Module
