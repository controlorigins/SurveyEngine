Imports System.Collections.Generic
Imports CODataCon
Imports CODataCon.com.controlorigins.ws

Public Module MessageSystemControler

    Dim mycon As New DataControler()

    Public Function GetallMessages() As List(Of SiteMessageItem)
        Return mycon.GetSiteMessageList()
    End Function

    Public Function GetMessage(messageid As Integer) As SiteMessageItem
        Return mycon.GetSiteMessageByMessageID(messageid)
    End Function

    'Public Function GetUserMessages(userID As Integer) As List(Of SiteMessageItem)
    '    Return mycon.GetApplicationUserByApplicationUserID(userID).Messages().ToList()
    'End Function

    Public Function GetUserSentMessages(userID As Integer) As List(Of SiteMessageItem)
        Return mycon.GetUserSentMessages(userID)
    End Function

    Public Sub UserMessageOpened(message As SiteMessageItem)
        mycon.UserMessageOpened(message)
    End Sub

    Public Sub UserMessageOpened(messageID As Integer)
        UserMessageOpened(GetMessage(messageID))
    End Sub

    Public Function SendMessage(FromUserID As Integer, ToUserID As Integer, subject As String, Message As String) As SiteMessageItem
        Dim newmessage As New SiteMessageItem With {.FromUserID = FromUserID,
                                                    .ToUserID = ToUserID,
                                                    .Message = Message,
                                                    .CratedDateTime = Now,
                                                    .Opened = False,
                                                    .Subject = subject,
                                                    .Deleted = False}
        Return mycon.PutSiteMessage(newmessage)
    End Function

    Public Function DeleteMessage(message As SiteMessageItem) As Boolean
        Return mycon.DeleteMessage(message)
    End Function

    Public Function DeleteMessage(messageID As Integer) As Boolean
        Return mycon.DeleteMessage(GetMessage(messageID))
    End Function

    Public Function GetavailabletoUsers(userid As Integer) As List(Of ApplicationuserItem)
        Return mycon.GetRelatedUsers(userid)
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
            Return "on " + time
        End If
    End Function

    Public Function DistanceOfTimeInWords(minutes As Double) As String
        If minutes < 1 Then
            Return "less than a minute"
        ElseIf minutes < 50 Then
            Return minutes + " minutes"
        ElseIf minutes < 90 Then
            Return "about one hour"
        ElseIf minutes < 1080 Then
            Return Math.Round(New Decimal((minutes / 60))) + " hours"
        ElseIf minutes < 1440 Then
            Return "one day"
        ElseIf minutes < 2880 Then
            Return "about one day"
        Else
            Return Math.Round(New Decimal((minutes / 1440))) + " days"
        End If
    End Function

#End Region


End Module
