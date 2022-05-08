Public Module MessageSystemControler

    Dim mydb As New DataClassesDataContext


    Public Function GetallMessages() As List(Of UserMessage)
        Return mydb.UserMessages.ToList
    End Function

    Public Function GetMessage(messageid As Integer) As UserMessage
        Return (From i In mydb.UserMessages Where i.Id = messageid).Single
    End Function

    Public Function GetUserMessages(userID As Integer) As List(Of UserMessage)
        Return (From i In mydb.UserMessages Where i.ToUserID = userID).ToList
    End Function

    Public Function GetUserSentMessages(userID As Integer) As List(Of UserMessage)
        Return (From i In mydb.UserMessages Where i.FromUserID = userID).ToList
    End Function

    Public Sub UserMessageOpened(message As UserMessage)
        message.Opened = True
        mydb.SubmitChanges()
    End Sub

    Public Sub UserMessageOpened(messageID As Integer)
        UserMessageOpened((From i In mydb.UserMessages Where i.Id = messageID).Single)
    End Sub

    Public Function SendMessage(FromUserID As Integer, ToUserID As Integer, subject As String, Message As String) As Boolean
        Try
            Dim newmessage As New UserMessage
            With newmessage
                .FromUserID = FromUserID
                .ToUserID = ToUserID
                .Message = Message
                .CratedDateTime = Now
                .Opened = False
                .Subject = subject
                .Deleted = False
            End With
            mydb.UserMessages.InsertOnSubmit(newmessage)
            mydb.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeleteMessage(message As UserMessage) As Boolean
        Try
            mydb.UserMessages.DeleteOnSubmit(message)
            mydb.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeleteMessage(messageID As Integer) As Boolean

        Try

            Dim messagetodelete = (From i In mydb.UserMessages Where i.Id = messageID).Single
            DeleteMessage(messagetodelete)
            Return True

        Catch ex As Exception
            Return False
        End Try


    End Function

    Public Function GetavailabletoUsers(userid As Integer) As List(Of OnlineUserInfo)
        Dim mycoll As New List(Of OnlineUserInfo)
        Dim myapps = (From i In GetMyApps(userid) Select i.Id)
        Dim templist As New List(Of OnlineUserInfo)
        For Each app In myapps
            templist.AddRange(From i In GetUsersbyAppID(app) Where i.UserID <> userid)
        Next
        mycoll.AddRange(From i In templist Distinct)
        Return mycoll
    End Function


#Region "Time Display"

    Public Function TimeAgo(time As DateTime) As String
        Dim startDate As DateTime = DateTime.Now
        Dim deltaMinutes As TimeSpan = startDate.Subtract(time)
        Dim distance As String = String.Empty
        If deltaMinutes.TotalMinutes <= (8724 * 60) Then
            distance = DistanceOfTimeInWords(deltaMinutes.TotalMinutes)
            If deltaMinutes.Minutes < 0 Then
                Return distance & Convert.ToString(" from  now")
            Else
                Return distance & Convert.ToString(" ago")
            End If
        Else
            Return "on " + time.ToString()
        End If
    End Function

    Public Function DistanceOfTimeInWords(minutes As Double) As String
        If minutes < 1 Then
            Return "less than a minute"
        ElseIf minutes < 50 Then
            Return minutes.ToString() + " minutes"
        ElseIf minutes < 90 Then
            Return "about one hour"
        ElseIf minutes < 1080 Then
            Return Math.Round(New Decimal((minutes / 60))).ToString() + " hours"
        ElseIf minutes < 1440 Then
            Return "one day"
        ElseIf minutes < 2880 Then
            Return "about one day"
        Else
            Return Math.Round(New Decimal((minutes / 1440))).ToString() + " days"
        End If
    End Function


#End Region


End Module
