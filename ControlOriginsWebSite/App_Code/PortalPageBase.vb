Imports CODataCon.com.controlorigins.ws

Public Class PortalPageBase
    Inherits System.Web.UI.Page
    Public Const STR_ApplicationTheme As String = "ApplicationTheme"
    Public Const STR_UserInfo As String = "UserInfo"
    Public Const STR_AppInfo As String = "AppInfo"
    Public Const STR_PageArgs As String = "PageArgs"

    Public Property UserInfo As ApplicationUserItem
        Get
            If Session(STR_UserInfo) Is Nothing Then
                Session(STR_UserInfo) = New ApplicationUserItem With {.ApplicationUserID = 1}
            End If
            Return Session(STR_UserInfo)
        End Get
        Set(value As ApplicationUserItem)
            Session(STR_UserInfo) = value
        End Set
    End Property

    Public Property AppInfo As ApplicationItem
        Get
            If Session(STR_AppInfo) Is Nothing Then
                Session(STR_AppInfo) = AppControler.GetAppByID(1)
            End If
            Return Session(STR_AppInfo)
        End Get
        Set(value As ApplicationItem)
            Session(STR_AppInfo) = value
        End Set
    End Property
#Region "Page Arg Handlers"

    Public Function GetPageArgument(ByVal theKey As String) As Pair
        Dim mydic = CType(Session(STR_PageArgs), Dictionary(Of String, String))
        If mydic.ContainsKey(theKey.ToLower) Then
            Return New Pair With {.First = theKey, .Second = mydic(theKey.ToLower)}
        Else
            Return New Pair With {.First = theKey, .Second = "-1"}
        End If
    End Function

    Public Function GetPageArguments() As Dictionary(Of String, String)
        Try
            Return Session(STR_PageArgs)
        Catch ex As Exception
            ClearPageArguments()
            Return Session(STR_PageArgs)
        End Try

    End Function

    Public Function ClearPageArguments() As Boolean
        Session(STR_PageArgs) = New Dictionary(Of String, String)
        Return True
    End Function

    Public Function SetPageArgument(ByVal theKey As String, ByVal theValue As String) As Boolean
        Dim mydic = CType(Session(STR_PageArgs), Dictionary(Of String, String))
        If mydic.ContainsKey(theKey) Then
            mydic(theKey) = theValue
        Else
            mydic.Add(theKey, theValue)
        End If
        Session(STR_PageArgs) = mydic
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
        If TryCast(Session(STR_PageArgs), Dictionary(Of String, String)) Is Nothing Then
            ClearPageArguments()
        End If
    End Sub

    Public Sub LoadPage()
        Response.Redirect("/")
    End Sub

#End Region

End Class
