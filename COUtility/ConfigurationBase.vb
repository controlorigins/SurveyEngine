Imports System.Xml
Imports System.Web
Imports ControlOrigins.COUtility

Public Class ConfigurationBase
#Region "Constructors"
    Public Sub New(ByVal myConfigFilePath As String)
        _ConfigFilePath = "c:\SPSurvey\configfile.xml"
        myDoc = New COXmlDocument()
        'Try
        '    _ConfigFilePath = HttpContext.Current.Server.MapPath(myConfigFilePath)
        'Catch ex As Exception
        '    AppLog.ErrorLog("Can't map path to config file: " & myConfigFilePath, ex.ToString)
        'End Try

        'If Not (FileProcessing.VerifyFolderExists(HttpContext.Current.Server.MapPath("~/config/"))) Then
        '    FileProcessing.CreateFolder(HttpContext.Current.Server.MapPath("~/config/"))
        'End If

        Try
            myDoc.Load(ConfigFilePath)
        Catch
            Try
                MakeXML(ConfigFilePath)
                myDoc.Load(ConfigFilePath)
            Catch ex As Exception
                ApplicationLogging.ErrorLog("Can't Create Config File: " & ConfigFilePath, ex.ToString)
            End Try
        End Try
    End Sub
    Public Sub New()

    End Sub
#End Region
#Region "Properties"
    Private ReadOnly myDoc As COXmlDocument
    Private ReadOnly _ConfigFilePath As String

    Private ReadOnly Property ConfigFilePath() As String
        Get
            Return _ConfigFilePath
        End Get
    End Property
#End Region
#Region "Functions"
    Public Function GetSiteConfig(ByVal ConfigName As String) As String
        If myDoc IsNot Nothing Then
            Try
                Return myDoc.GetElementsByTagName(ConfigName).Item(0).ChildNodes.Item(0).Value
            Catch ex As Exception
                SetSiteConfig(ConfigName, String.Empty)
                Return String.Empty
            End Try
        Else
            Return String.Empty
        End If
    End Function
    Public Function GetSiteConfig(ByVal ConfigName As String, ByVal ConfigValaue As String) As String
        Dim myReturn As String = GetSiteConfig(ConfigName)
        If myReturn = String.Empty Then
            SetSiteConfig(ConfigName, ConfigValaue)
            myReturn = ConfigValaue
        End If
        HttpContext.Current.Application(ConfigName) = myReturn
        Return myReturn
    End Function
    Public Sub SetSiteConfig(ByVal ConfigName As String, ByVal ConfigValue As String)
        If myDoc IsNot Nothing Then
            Dim myElement As XmlElement
            Dim myRoot As XmlElement = myDoc.DocumentElement
            Dim myConfig As XmlNodeList = myDoc.GetElementsByTagName(ConfigName)
            Try
                If myConfig.Count = 0 Then
                    myElement = myDoc.CreateElement(ConfigName)
                    myElement.InnerText = ConfigValue
                    myRoot.AppendChild(myElement)
                Else
                    myConfig.Item(0).InnerText = ConfigValue
                End If
                AppLog.AuditLog("Change Site Config:" & ConfigName, ConfigValue)
                myDoc.Save(ConfigFilePath())
                HttpContext.Current.Application(ConfigName) = ConfigValue
            Catch ex As Exception
                AppLog.ErrorLog("ErrorSetting Value: " & ConfigFilePath, ex.ToString)
            End Try
        End If
    End Sub

    Private Shared Function MakeXML(ByVal myFileName As String) As Boolean
        Dim xmlDoc As New XmlDocument()
        Dim xmlDeclaration As XmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", Nothing)
        Dim rootNode As XmlElement = xmlDoc.CreateElement("Configuration")
        xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement)
        xmlDoc.AppendChild(rootNode)
        xmlDoc.Save(myFileName)
        Return True
    End Function
#End Region
End Class
