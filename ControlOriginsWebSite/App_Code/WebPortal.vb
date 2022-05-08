Imports System.Web.Services
Imports CODataCon.com.controlorigins.ws

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://ws.controlorigins.com/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WebPortal
    Inherits WebService
    Dim mycon As New CODataCon.DataControler()

'#Region "Application"

'    <WebMethod()> _
'    Public Function SiteAppList() As List(Of ApplicationItem)
'        Return mycon.GetApplicationList()
'    End Function

'#End Region

'#Region "User"
'    <WebMethod()> _
'    Public Function GetUserList() As List(Of ApplicationuserItem)
'        Return mycon.GetSiteUserList()
'    End Function

'#End Region





End Class