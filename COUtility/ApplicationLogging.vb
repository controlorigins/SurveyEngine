Imports System.IO
Imports System.Web
Namespace COUtility

Public Class ApplicationLogging
    Private Const STR_LogFilePathTemplate As String = "{0}\log\{1}"
    '********************************************************************************

    Public Shared Function WriteLog(ByVal MessageOne As String, ByVal MessageTwo As String, ByVal LogFileName As String) As Boolean
        Dim bReturn As Boolean = False
        Dim sQuoteComma As String = (""",""")
        Dim sQuote As String = ("""")
        MessageOne = wpm_ApplyHTMLFormatting(wpm_GetStringValue(MessageOne))
        MessageTwo = wpm_ApplyHTMLFormatting(wpm_GetStringValue(MessageTwo))


        HttpContext.Current.Application.Lock()
        Try
            Using sw As New StreamWriter(GetFilePath(LogFileName), True)
                Try
                    sw.WriteLine(String.Format("{0}{2}{1}{3}{1}{4}{1}{5}{1}{6}{1}{7}{1}{8}{0}", 
                                     sQuote, 
                                     sQuoteComma, 
                                     GetUserName(), 
                                     GetHost(), 
                                     GetAbsoluteUri(), 
                                     DateTime.Now.ToShortDateString, 
                                     DateTime.Now.ToLongTimeString, 
                                     MessageOne, 
                                     MessageTwo))
                Catch 
                    ' Do Nothing
                End Try
            End Using
            bReturn = True
        Catch
            ' do nothing
        End Try
        HttpContext.Current.Application.UnLock()
        Return bReturn
    End Function

        Public Shared Function GetUserName() As String 
            Try
                Return Utility.GetDBString(HttpContext.Current.Session("ContactName"))
            Catch ex As Exception
                Return String.Empty
            End Try
        End Function
        Public Shared Function GetHost() As String 
            Try
                Return Utility.GetDBString(HttpContext.Current.Request.Url.Host)
            Catch ex As Exception
                Return String.Empty
            End Try
        End Function
        Public Shared Function GetAbsoluteUri() As String 
            Try
                Return Utility.GetDBString(HttpContext.Current.Request.Url.AbsoluteUri)
            Catch ex As Exception
                Return String.Empty
            End Try
        End Function
    Public Shared Function GetFilePath(ByVal LogFileName As String) As String
        Dim sQuoteComma As String = (""",""")
        Dim sQuote As String = ("""")
        Dim ConfigFolderPath As String = HttpContext.Current.Server.MapPath("/App_Data")

        If Not FileProcessing.VerifyFolderExists(ConfigFolderPath) Then
            FileProcessing.CreateFolder(ConfigFolderPath)
        End If

        If Not FileProcessing.VerifyFolderExists(String.Format("{0}\log", ConfigFolderPath)) Then
            FileProcessing.CreateFolder(String.Format("{0}\log", ConfigFolderPath))
        End If

        If Not FileProcessing.FileExists(String.Format(STR_LogFilePathTemplate, ConfigFolderPath, LogFileName)) Then
            Try
                Using sw As New StreamWriter(String.Format(STR_LogFilePathTemplate, ConfigFolderPath, LogFileName), True)
                    Try
                        sw.WriteLine( _
                               String.Format("{0}UserName{1}HostName{1}RequestURL{1}Date{1}Time{1}MessageOne{1}MessageTwo{0}", sQuote, sQuoteComma))
                    Catch
                        ' Do Nothing
                    End Try
                End Using
            Catch
                ' do nothing
            End Try
        End If
        Return String.Format(STR_LogFilePathTemplate, ConfigFolderPath, LogFileName)
    End Function

    ' ****************************************************************************
    Public Shared Function XMLLog(ByVal accessMessage As String, ByVal accessProcess As String) As Boolean
        Return WriteLog(accessProcess, accessMessage, ("XML.csv"))
    End Function
    ' ****************************************************************************
    Public Shared Function FileNotFoundLog(ByVal accessMessage As String, ByVal accessProcess As String) As Boolean
        Return WriteLog(accessProcess, accessMessage, ("404.csv"))
    End Function
    ' ****************************************************************************
    Public Shared Function SiteReferLog(ByVal accessMessage As String, ByVal accessProcess As String) As Boolean
        Return WriteLog(accessProcess, accessMessage, ("Refer.csv"))
    End Function
    ' ****************************************************************************
    Public Shared Function ErrorLog(ByVal logProcess As String, ByVal logMessage As String) As Boolean
        Return WriteLog(logProcess, logMessage, ("ErrorLog.csv"))
    End Function
    ' ****************************************************************************
    Public Shared Function ConfigLog(ByVal logProcess As String, ByVal logMessage As String) As Boolean
        Return WriteLog(logProcess, logMessage, ("ConfigError.csv"))
    End Function
    ' ****************************************************************************
    Public Shared Function SearchLog(ByVal accessMessage As String, ByVal accessProcess As String) As Boolean
        Return WriteLog(accessProcess, accessMessage, ("Search.csv"))
    End Function
    ' ****************************************************************************
    Public Shared Function AccessLog(ByVal accessMessage As String, ByVal accessProcess As String) As Boolean
        Return WriteLog(accessMessage, accessProcess, ("Access.csv"))
    End Function
    ' ****************************************************************************
    Public Shared Function GameLog(ByVal accessMessage As String, ByVal accessProcess As String) As Boolean
        Return WriteLog(accessMessage, accessProcess, ("Game.csv"))
    End Function
    '********************************************************************************
    Public Shared Function DownloadLog(ByVal frmName As String, ByVal frmEmail As String, ByVal DownloadFile As String) As Boolean
        Return WriteLog(String.Format("{0}"",""{1}", DownloadFile, frmName), frmEmail, ("DownloadLog.csv"))
    End Function
    '********************************************************************************
    Public Shared Function AuditLog(ByVal AuditMessage As String, ByVal AuditProcess As String) As Boolean
        Return WriteLog(AuditProcess, AuditMessage, ("AuditLog.csv"))
    End Function
    '********************************************************************************
    Public Shared Function SQLAudit(ByVal strSQL As String, ByVal pageID As String) As Boolean
        Return WriteLog(strSQL, pageID, ("SQL-Audit.csv"))
    End Function
    Public Shared Function SQLSelectError(ByVal strSQL As String, ByVal pageID As String) As Boolean
        Return WriteLog(strSQL, pageID, ("SQL-Select-Error.csv"))
    End Function
    Public Shared Function SQLInsertError(ByVal strSQL As String, ByVal pageID As String) As Boolean
        Return WriteLog(strSQL, pageID, ("SQL-Insert-Error.csv"))
    End Function
    Public Shared Function SQLUpdateError(ByVal strSQL As String, ByVal pageID As String) As Boolean
        Return WriteLog(strSQL, pageID, ("SQL-Update-Error.csv"))
    End Function
    Public Shared Function MembershipProviderError(ByVal Error1 As String, ByVal Error2 As String) As Boolean
        Return WriteLog(Error1, Error2, ("MembershipProvider.csv"))
    End Function
    Public Shared Function SQLDeleteError(ByVal strSQL As String, ByVal pageID As String) As Boolean
        Return WriteLog(strSQL, pageID, ("SQL-Delete-Error.csv"))
    End Function
    Public Shared Function SQLExceptionLog(ByVal strCallingFunction As String, ByRef ex As Exception) As Boolean
        If ex.InnerException Is Nothing Then
            Return WriteLog(strCallingFunction, ex.ToString, ("SQLError.csv"))
        Else
            Return WriteLog(strCallingFunction, ex.InnerException.ToString, ("SQLError.csv"))
        End If
    End Function
End Class
End Namespace