Imports System.Text
Imports System.Collections.Generic
Imports System.Web.UI
Imports CODataCon.com.controlorigins.ws

Public Class ApplicationControlBase
    Inherits UserControl
    Event Save(Data As Object)
    Event Close(sender As Object, e As Object)

#Region "Userinfo and Appinfo Objects"
    Public Property UserInfo As ApplicationUserItem
        Get
            If Session("UserInfo") Is Nothing Then
                Session("UserInfo") = New ApplicationUserItem With {.ApplicationUserID = -1, .UserRoleID = 1}
            End If
            Return Session("Userinfo")
        End Get
        Set(value As ApplicationUserItem)
            Session("Userinfo") = value
        End Set
    End Property

    Public Property AppInfo As ApplicationItem
        Get
            If Session("AppInfo") Is Nothing Then
                Session("AppInfo") = AppControler.GetAppByID(1)
            End If
            Return Session("AppInfo")
        End Get
        Set(value As ApplicationItem)
            Session("AppInfo") = value
        End Set
    End Property


#End Region

#Region "Prop functions"


    Public Function GetProperty(PropertyKey As String) As String
        Dim myprop = (From i In AppInfo.Properties.ToList Where i.Key = PropertyKey).ToList()
        If myprop.Count = 1 Then
            Return myprop(0).Value
        Else
            Return "Empty"
        End If
    End Function

    Public Function GetProperties() As Dictionary(Of String, String)
        Dim mydic As New Dictionary(Of String, String)
        For Each i In AppInfo.Properties
            mydic.Add(i.Key, i.Value)
        Next
        Return mydic
    End Function


    Public Function SetProperty(PropertyKey As String, value As String) As Boolean
        Dim bReturn = AppControler.SetProperty(AppInfo.ApplicationID, PropertyKey, value)
        If bReturn Then
            AppInfo = GetAppByID(AppInfo.ApplicationID)
        End If
        Return bReturn
    End Function

    Public Function DeleteProperty(propertyKey As String) As Boolean
        Dim bReturn = AppControler.DeleteProperty(AppInfo.ApplicationID, propertyKey)
        If bReturn Then
            AppInfo = GetAppByID(AppInfo.ApplicationID)
        End If
        Return bReturn
    End Function

#End Region

#Region "Page Arg Handlers"
    Public Sub LoadPage()
        Response.Redirect("/")
    End Sub
    Public Function GetPageArguments() As Dictionary(Of String, String)
        Return Session("PageArgs")
    End Function
    Public Function ClearPageArguments() As Boolean
        Session("PageArgs") = New Dictionary(Of String, String)
        Return True
    End Function
    Public Function ClearPageArguments(ByVal pid As string) As Boolean
        Session("PageArgs") = New Dictionary(Of String, String)
        SetPageArgument("pid",pid)
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
            Return New Pair With {.First = theKey, .Second = "0"}
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
        SetPageArgument("pid", (From i In AppInfo.Navigation Where i.TartgetPage.ToLower = myControlName.ToLower() Select i.Id).Single())
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

        Return <smtpResponse>
                   <status><%= status %></status>
                   <statusMessage><%= StatusMessage %></statusMessage>
               </smtpResponse>

    End Function

    Public ReadOnly Property RootPath As String
        Get
            Dim appUrl = HttpRuntime.AppDomainAppVirtualPath
            If HttpRuntime.AppDomainAppVirtualPath <> "/" Then
                appUrl = appUrl & "/"
            End If
            Dim baseUrl = String.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, appUrl)
            Return baseUrl
        End Get
    End Property


    Public ReadOnly Property IsAdmin As Boolean
        Get
            If UserInfo.ApplicationUserRoleList.Count > 0 Then
                Dim myRole = (From i In UserInfo.ApplicationUserRoleList Where i.ApplicationID = AppInfo.ApplicationID Select i).SingleOrDefault()
                If myRole Is Nothing Then
                    Return False
                Else
                    If myRole.IsUserAdmin Then
                    return True
                    Else
                        Return False
                    End If
                End If
            Else
                Return False
            End If
            Return False
        End Get
    End Property


End Class


