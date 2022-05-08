Imports System.Text
Namespace COUtility
Public NotInheritable Class App
    Public Const STR_SurveyLayoutsPath As String = ""

    Public Shared MessageStack As New List(Of String)
    Public Shared Function AddMessage(ByVal Message As String) As Boolean
        MessageStack.Add(Message)
        Return True
    End Function
    Public Shared Function AddMessage(ByVal Message1 As String, ByVal Message2 As String) As Boolean
        MessageStack.Add(String.Format("{0}-{1}", Message1, Message2))
        ApplicationLogging.ErrorLog(String.Format("{0}-{1}", Message1, Message2), "App.AddMessage")
        Return True
    End Function
    Public Shared Function AddMessage(ByRef ex As Exception, ByVal Message As String) As Boolean
        MessageStack.Add(Message)
        ApplicationLogging.ErrorLog(String.Format("{0} -- {1}", Message, ex), "App.AddMessage")
        Return True
    End Function
    Public Shared Function ResetMessageStack() As Boolean
        MessageStack.Clear()
        Return True
    End Function
    Public Shared Function GetMessageList() As String
        If MessageStack.Count > 0 Then
            Dim myReturn As New StringBuilder("<ul>")
            For Each myMessage As String In MessageStack
                myReturn.Append(String.Format("<li>{0}</li>", myMessage))
            Next
            myReturn.Append("</ul>")
            Return myReturn.ToString
        Else
            Return String.Empty
        End If
    End Function

End Class

End Namespace
