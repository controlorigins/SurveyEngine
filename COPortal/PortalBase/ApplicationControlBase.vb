Imports System.Web
Imports System.Text

Public Class ApplicationControlBase
    Inherits System.Web.UI.UserControl
    Event Save(Data As Object)
    Event Close(sender As Object, e As Object)

#Region "Userinfo and Appinfo Objects"
    Public Property UserInfo As OnlineUserInfo
        Get
            If Session("UserInfo") Is Nothing Then
                Session("UserInfo") = New OnlineUserInfo With {.UserID = -1, .UserRoleID = 1}
            End If
            Return CType(HttpContext.Current.Session("Userinfo"), OnlineUserInfo)
        End Get
        Set(value As OnlineUserInfo)
            HttpContext.Current.Session("Userinfo") = value
        End Set
    End Property

    Public Property AppInfo As SiteApp
        Get
            If Session("AppInfo") Is Nothing Then
                Session("AppInfo") = AppControler.GetAppByID(1)
            End If
            Return CType(HttpContext.Current.Session("AppInfo"), SiteApp)
        End Get
        Set(value As SiteApp)
            HttpContext.Current.Session("AppInfo") = value
        End Set
    End Property



#End Region


#Region "Prop functions"


    Public Function GetProperty(PropertyKey As String) As String

        Dim myprop = AppControler.GetProperty(AppInfo.Id, PropertyKey)
        If myprop Is Nothing Then
            Return "Empty"
        Else
            Return myprop
        End If

    End Function

    Public Function GetProperties() As Dictionary(Of String, String)
        Return AppControler.GetProperties(AppInfo.Id)
    End Function


    Public Function SetProperty(PropertyKey As String, value As String) As Boolean
        Return AppControler.SetProperty(AppInfo.Id, PropertyKey, value)
    End Function

    Public Function DeleteProperty(propertyKey As String) As Boolean
        Return AppControler.DeleteProperty(AppInfo.Id, propertyKey)
    End Function

#End Region



#Region "Page Arg Handlers"




    Public Sub LoadPage()
        Response.Redirect("/")
    End Sub


    Public Function GetPageArguments() As Dictionary(Of String, String)
        Return CType(HttpContext.Current.Session("PageArgs"), Global.System.Collections.Generic.Dictionary(Of String, String))
    End Function

    Public Function ClearPageArguments() As Boolean
        Session("PageArgs") = New Dictionary(Of String, String)
        Return True
    End Function

    Public Function SetPageArgument(ByVal theKey As String, ByVal theValue As String) As Boolean

        Dim mydic = CType(Session("PageArgs"), Dictionary(Of String, String))
        If mydic.ContainsKey(theKey.ToLower) Then
            mydic(theKey) = theValue
        Else
            mydic.Add(theKey.ToLower, theValue)
        End If
        Session("PageArgs") = mydic
        Return True
    End Function


    Public Function GetPageArgument(ByVal theKey As String) As Pair

        Dim mydic = CType(Session("PageArgs"), Dictionary(Of String, String))
        If mydic.ContainsKey(theKey.ToLower) Then
            Return New Pair With {.First = theKey, .Second = mydic(theKey.ToLower)}
        Else
            Return New Pair With {.First = theKey, .Second = "-1"}
        End If
    End Function

    Public Function GetFormatedPageArguments() As String
        Dim mystringbuilder As New StringBuilder("\?")
        For Each myArg In GetPageArguments()
            mystringbuilder.Append(String.Format("{0}={1}&", myArg.Key, myArg.Value))
        Next
        Return mystringbuilder.ToString()
    End Function


    Private Sub CheckPageArgs() Handles Me.Load
        If TryCast(Session("PageArgs"), Dictionary(Of String, String)) Is Nothing Then
            ClearPageArguments()
        End If
    End Sub

    Public Sub SetPage(ByVal myControlName As String)
        SetPageArgument("pid", CStr((From i In AppInfo.SiteAppMenus Where i.TartgetPage.ToLower = myControlName.ToLower() Select i.Id).Single()))
    End Sub

#End Region



    ''' <summary>
    ''' This Function is in Development Stage | SendMail
    ''' </summary>
    ''' <param name="MessageOBJ"></param>
    ''' <returns></returns>
    ''' <remarks>Do not depend on this function</remarks>
    Public Function SendMail(MessageOBJ As System.Net.Mail.MailMessage) As String

        Dim statmessage As New StringBuilder
        Dim haserrors As Boolean = False

        Dim status As String = ""
        Dim StatusMessage As String = ""

        With MessageOBJ
            ' check to see if the message object contains min required information
            If String.IsNullOrEmpty(.Body) Then
                statmessage.AppendLine(":Body Is Required:")
                haserrors = True
            End If
            If .To.Count < 1 Then
                statmessage.AppendLine(":To Email  Is Required:")
                haserrors = True
            End If
            If String.IsNullOrEmpty(.From.Address) Then
                statmessage.AppendLine(":From Address Is Required:")
                haserrors = True
            End If
        End With
        If haserrors = False Then
            Dim mysmtp As New System.Net.Mail.SmtpClient
            mysmtp.Send(MessageOBJ)
            StatusMessage = "All Good!"
            status = "Sucssess"
        Else
            StatusMessage = statmessage.ToString
        End If

        Return CStr(<smtpResponse>
                   <status><%= status %></status>
                   <statusMessage><%= StatusMessage %></statusMessage>
               </smtpResponse>)

    End Function

End Class


