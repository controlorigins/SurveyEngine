'Imports System.Web.Configuration

'Public Class FileConfig
'    Inherits ConfigurationBase
'    Public Sub New()
'        MyBase.New(WebConfigurationManager.AppSettings("FileConfig"))
'    End Sub
'    Public Sub New(ByVal myConfigFilePath As String)
'        MyBase.New(myConfigFilePath)
'    End Sub
'    Public ReadOnly Property ConfigFolderPath() As String
'        Get
'            Return "c:\SPSurvey\"
'        End Get
'    End Property
'    Public Property DefaultApplicationID As Integer
'        Get
'            If IsNumeric(GetSiteConfig("ApplicationID", String.Empty)) Then
'                Return CInt(GetSiteConfig("ApplicationID", String.Empty))
'            Else
'                Return -1
'            End If
'        End Get
'        Set(value As Integer)
'            SetSiteConfig("ApplicationID", String.Empty)
'        End Set
'    End Property
'    Public ReadOnly Property ConnectionString() As String
'        Get
'            Return GetSiteConfig("ConnectionString", "Data Source=CONTROLORIGINS\SharePoint;Initial Catalog=SurveyDB;")
'        End Get
'    End Property
'    Public Property ThowExceptionOnError() As Boolean
'        Get
'            Return CBool(GetSiteConfig("ThowExceptionOnError", CStr(True)))
'        End Get
'        Set(ByVal value As Boolean)
'            SetSiteConfig("ThowExceptionOnError", CStr(value))
'        End Set
'    End Property

'End Class
