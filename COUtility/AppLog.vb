Imports System.IO
Imports System.Data.SqlClient
Imports System.Text
Imports System.Web
Namespace COUtility
    Public Class AppLog
        Public Shared Function EmailLog(ByVal accessMessage As String, ByVal accessProcess As String) As Boolean
            LogHelper.LogInfo(String.Format("{0} - {1}", accessProcess, accessMessage))
            Return True
            Return ApplicationLogging.WriteLog(accessProcess, accessMessage, "EmailLog.csv")
        End Function
        Public Shared Function SurveyLog(ByVal accessMessage As String, ByVal accessProcess As String) As Boolean
            LogHelper.LogInfo(String.Format("{0} - {1}", accessProcess, accessMessage))
            App.AddMessage(String.Format("SurveyError {1} - {0}", accessMessage, accessProcess))
            '        Return True
            Return ApplicationLogging.WriteLog(accessProcess, accessMessage, "SurveyLog.csv")
        End Function
        Public Shared Function ErrorLog(ByVal accessMessage As String, ByVal accessProcess As String) As Boolean
            LogHelper.LogInfo(String.Format("{0} - {1}", accessProcess, accessMessage))
            Return ApplicationLogging.WriteLog(accessProcess, accessMessage, "ErrorLog.csv")
        End Function
        Public Shared Function ErrorLog(ByVal accessMessage As String) As Boolean
            LogHelper.LogInfo(accessMessage)
            Return ApplicationLogging.WriteLog(accessMessage, String.Empty, "ErrorLog.csv")
        End Function
        Public Shared Function AccessLog(ByVal accessMessage As String, ByVal accessProcess As String) As Boolean
            LogHelper.LogInfo(String.Format("{0} - {1}", accessProcess, accessMessage))
            Return ApplicationLogging.WriteLog(accessProcess, accessMessage, "AccessLog.csv")
        End Function
        Public Shared Function AuditLog(ByVal accessMessage As String, ByVal accessProcess As String) As Boolean
            LogHelper.LogInfo(String.Format("{0} - {1}", accessProcess, accessMessage))
            Return ApplicationLogging.WriteLog(accessProcess, accessMessage, "AuditLog.csv")
        End Function
        Public Shared Function SQLAudit(ByVal ErrorMessage As String, ByVal CallingMethod As String) As Boolean
            LogHelper.LogInfo(String.Format("{0} - {1}", CallingMethod, ErrorMessage))
            Return ApplicationLogging.WriteLog(CallingMethod, ErrorMessage, "SQL-Audit.csv")
        End Function
        Public Shared Function SQLSelectError(ByVal ErrorMessage As String, ByVal CallingMethod As String) As Boolean
            LogHelper.LogInfo(String.Format("{0} - {1}", CallingMethod, ErrorMessage))
            Return ApplicationLogging.WriteLog(CallingMethod, ErrorMessage, ("SQL-Error.csv"))
        End Function
        Public Shared Function SQLError(ByVal ErrorMessage As String, ByVal CallingMethod As String) As Boolean
            LogHelper.LogInfo(String.Format("{0} - {1}", CallingMethod, ErrorMessage))
            Return ApplicationLogging.WriteLog(CallingMethod, ErrorMessage, ("SQL-Error.csv"))
        End Function
        Public Shared Function SQLInsertError(ByVal strSQL As String, ByVal pageID As String) As Boolean
            LogHelper.LogInfo(String.Format("SQLInsertError: {0} - {1}", pageID, strSQL))
            Return ApplicationLogging.WriteLog(pageID, strSQL, ("SQL-Error.csv"))
        End Function
        Public Shared Function SQLDeleteError(ByVal strSQL As String, ByVal pageID As String) As Boolean
            LogHelper.LogInfo(String.Format("SQLDeleteError: {0} - {1}", pageID, strSQL))
            Return ApplicationLogging.WriteLog(pageID, strSQL, ("SQL-Error.csv"))
        End Function
        Public Shared Function SQLExceptionLog(ByVal strCallingFunction As String, ByRef ex As SqlException) As Boolean
            If ex.InnerException Is Nothing Then
                LogHelper.LogInfo(String.Format("SQLExceptionLog:{0} - {1}", strCallingFunction, ex.ToString))
                Return ApplicationLogging.WriteLog(strCallingFunction, ex.ToString, ("SQL-Error.csv"))
            Else
                LogHelper.LogInfo(String.Format("SQLExceptionLog:{0} - {1}", strCallingFunction, ex.InnerException.ToString))
                Return ApplicationLogging.WriteLog(strCallingFunction, ex.InnerException.ToString, ("SQL-Error.csv"))
            End If
        End Function
        Public Shared Function MembershipProviderError(ByVal Error1 As String, ByVal Error2 As String) As Boolean
            LogHelper.LogInfo(String.Format("{0} - {1}", Error1, Error2))
            Return ApplicationLogging.WriteLog(Error1, Error2, ("MembershipProvider.csv"))
        End Function
    End Class
End Namespace