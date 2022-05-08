Imports Microsoft.VisualBasic
Imports System.Text

Public Class PortalPageBase
    Inherits System.Web.UI.Page
    Public Const STR_ApplicationTheme As String = "ApplicationTheme"

    Public Property UserInfo As OnlineUserInfo
        Get
            If Session("UserInfo") Is Nothing Then
                Session("UserInfo") = New OnlineUserInfo With {.UserID = 1}
               
            End If
            Return CType(Session("Userinfo"), OnlineUserInfo)
        End Get
        Set(value As OnlineUserInfo)
            Session("Userinfo") = value
        End Set
    End Property

    Public Property AppInfo As SiteApp
        Get
            If Session("AppInfo") Is Nothing Then
                Session("AppInfo") = AppControler.GetAppByID(5)
            End If
            Return CType(Session("AppInfo"), SiteApp)
        End Get
        Set(value As SiteApp)
            Session("AppInfo") = value
        End Set
    End Property






#Region "Page Arg Handlers"




    Public Function GetPageArgument(ByVal key As String) As Dictionary(Of String, String)
        Return New Dictionary(Of String, String)
    End Function

    Public Function GetPageArguments() As Dictionary(Of String, String)
        Try
            Return CType(Session("PageArgs"), Global.System.Collections.Generic.Dictionary(Of String, String))
        Catch ex As Exception
            ClearPageArguments()
            Return CType(Session("PageArgs"), Global.System.Collections.Generic.Dictionary(Of String, String))
        End Try

    End Function

    Public Function ClearPageArguments() As Boolean
        Session("PageArgs") = New Dictionary(Of String, String)
        Return True
    End Function

    Public Function SetPageArgument(ByVal theKey As String, ByVal theValue As String) As Boolean

        Dim mydic = CType(Session("PageArgs"), Dictionary(Of String, String))
        If mydic.ContainsKey(theKey) Then
            mydic(theKey) = theValue
        Else
            mydic.Add(theKey, theValue)
        End If
        Session("PageArgs") = mydic
        Return True
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

    Public Sub LoadPage()
        Response.Redirect("/")
    End Sub


#End Region





End Class
