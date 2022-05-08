Imports System.Xml.Serialization
Imports System.IO
Imports System.Collections.Specialized
Imports System.Text
Imports CODataCon.com.controlorigins.ws

Public Class UIPresenterBase
    Implements IDisposable
    ' Protected Const STR_UserDomainMask As String = "ControlOrigins2\{0}"
    Public ReadOnly myController As DataControler

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If myController IsNot Nothing Then
                    ' ToDO: Dispose
                End If
            End If
            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub
    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

#End Region
    Public Sub New()
        myController = New DataControler()
    End Sub

    'Public Function SendViaEmail(ByVal thisEmail As EmailItem, ByVal AppEmail As ApplicationEmailConfiguration) As EmailItem
    '    Try
    '        Dim headers As StringDictionary = New StringDictionary()
    '        headers.Add("to", thisEmail.ToEmailAddress)
    '        headers.Add("from", thisEmail.FromEmailAdderss)
    '        headers.Add("bcc", thisEmail.BCCEmailAddress)
    '        headers.Add("subject", thisEmail.EmailSubject)
    '        headers.Add("content-type", thisEmail.ContentType)
    '        '            Dim isExternalEmailSet As Boolean = Microsoft.SharePoint.Utilities.SPUtility.IsEmailServerSet(oWeb)
    '        If AppEmail.SendEmail Then
    '            If AppEmail.SendEmail Then
    '                If thisEmail.EmailSubject = "NO EMAIL" Or String.IsNullOrEmpty(thisEmail.EmailSubject) Or thisEmail.EmailBody = "NO EMAIL" Or String.IsNullOrEmpty(thisEmail.EmailBody) Then
    '                    AppLog.SurveyLog("Missing Email Subject or Body", String.Format("UIPresenterBase.SendViaEmail Subject={0}", thisEmail.EmailSubject))
    '                Else
    '                    AppLog.SurveyLog("Email Send", String.Format("UIPresenterBase.SendViaEmail Subject={0}", thisEmail.EmailSubject))
    '                    Dim message As New System.Net.Mail.MailMessage(headers("from"), headers("to"), headers("subject"), thisEmail.EmailBody)
    '                    Dim mailclinet As New System.Net.Mail.SmtpClient
    '                    mailclinet.Send(message)
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        AppLog.ErrorLog(ex.ToString, String.Format("UIPresenterBase.SendViaEmail Subject={0}", thisEmail.EmailSubject))
    '    End Try
    '    Return thisEmail
    'End Function
    'Public Function GetXML(ByVal myObject As Object) As String
    '    If myObject Is Nothing Then
    '        Return String.Empty
    '    Else
    '        Dim oXS As XmlSerializer = New XmlSerializer(myObject.GetType())
    '        Using writer As New StringWriter()
    '            oXS.Serialize(writer, myObject)
    '            Return writer.ToString
    '        End Using
    '    End If
    'End Function
    'Public Function GetSurveyResponseNM(ByVal mySurvey As SurveyItem, ByVal myAPCUser As ApplicationUserRoleItem, ByVal iCount As Integer) As String
    '    Dim SRNM As StringBuilder = New StringBuilder(mySurvey.ResponseNMTemplate)
    '    SRNM.Replace("~AssignedUserName~", String.Format("{0} {1}", myAPCUser.FirstNM, myAPCUser.LastNM))
    '    SRNM.Replace("~AssignedName~", String.Format("{0} {1}", myAPCUser.FirstNM, myAPCUser.LastNM))
    '    SRNM.Replace("~AssignedNM~", String.Format("{0} {1}", myAPCUser.FirstNM, myAPCUser.LastNM))
    '    SRNM.Replace("~MM~", Now.Month.ToString())
    '    SRNM.Replace("~DD~", Now.Day.ToString())
    '    SRNM.Replace("~YYYY~", Now.Year.ToString())
    '    SRNM.Replace("~#~", iCount.ToString())
    '    SRNM.Replace("~SurveyName~", mySurvey.SurveyNM)
    '    Return SRNM.ToString
    'End Function
    'Public Function GetSurveyResponseNM(ByVal mySurvey As SurveyItem, ByVal myAPCUser As ApplicationUserItem) As String
    '    Dim SRNM As StringBuilder = New StringBuilder(mySurvey.ResponseNMTemplate)
    '    SRNM.Replace("~AssignedUserName~", String.Format("{0} {1}", myAPCUser.FirstNM, myAPCUser.LastNM))
    '    SRNM.Replace("~AssignedNM~", String.Format("{0} {1}", myAPCUser.FirstNM, myAPCUser.LastNM))
    '    SRNM.Replace("~AssignedName~", String.Format("{0} {1}", myAPCUser.FirstNM, myAPCUser.LastNM))
    '    SRNM.Replace("~MM~", Now.Month.ToString())
    '    SRNM.Replace("~DD~", Now.Day.ToString())
    '    SRNM.Replace("~YYYY~", Now.Year.ToString())
    '    Return SRNM.ToString
    'End Function
    'Public Function GetSurveyEmailTemplate(ByVal mySurvey As SurveyItem, ByVal StatusID As Integer) As SurveyEmailTemplateItem
    '    Dim myTemplate As New SurveyEmailTemplateItem
    '    For Each myStatus In mySurvey.StatusList
    '        If myStatus.StatusID = StatusID Then
    '            With myTemplate
    '                .BodyTemplate = myStatus.BodyTemplate
    '                .SubjectTemplate = myStatus.SubjectTemplate
    '            End With
    '        End If
    '    Next
    '    Return myTemplate
    'End Function

End Class
