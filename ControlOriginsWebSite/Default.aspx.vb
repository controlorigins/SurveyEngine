Imports System
Imports CODataCon.com.controlorigins.ws

Partial Class _Default
    Inherits PortalPageBase

    Sub Pagestartup() Handles Me.Load
        Dim PID As String
        Dim comset = (From i In GetPageArguments() Where i.Key.ToLower = "pid")
        If comset.Count > 0 Then
            PID = comset(0).Value
        Else
            PID = ""
        End If

        Dim mypage As New NavigationMenuItem

        If String.IsNullOrEmpty(PID) Then
            ' default page no page id 
            If UserInfo.UserRoleID = 0 Then
                UserInfo = GetGuestUser()
            End If

            If UserInfo.UserRoleID = 1 Then
                SetPageArgument("pid", "1")
                LoadPage()
            Else
                SetPageArgument("pid", AppInfo.DefaultAppPage)
                LoadPage()
            End If
        Else
            ' load the page id needed 
            If UserInfo.UserRoleID > 1 And (PID = 1 Or PID = 2) Then
                mypage = AppControler.GetNavigationMenuItem(AppInfo, 3)
            Else
                mypage = AppControler.GetNavigationMenuItem(AppInfo,CInt(PID))
            End If
        End If

        Try
            Dim mycontrol = DirectCast(Page.LoadControl(String.Format("/Co_Apps/{0}/{1}", AppInfo.ApplicationFolder, mypage.TartgetPage)), ApplicationControlBase)
            content.Controls.Add(mycontrol)
        Catch ex As Exception
            AlertBox1.boldnote = "Developers Note - Error Adding User Control"
            AlertBox1.message = ex.StackTrace.ToString
            AlertBox1.Visible = True
        End Try

        If String.IsNullOrEmpty(AppInfo.ApplicationNM) Then
            AppInfo = GetAppByID(1)
        End If

        Master.PageTitle = AppInfo.ApplicationNM
        SelectCSS()

    End Sub

    Protected Sub SelectCSS()
        Dim myprop = (From i In AppInfo.Properties.ToList Where i.Key = STR_ApplicationTheme).ToList()
        If myprop.Count = 1 Then
            Mycsslink.Attributes.Remove("href")
            Mycsslink.Attributes.Add("href", String.Format("/css/{0}.css", myprop(0).Value ))
        Else
            Mycsslink.Attributes.Remove("href")
            Mycsslink.Attributes.Add("href", String.Format("/css/{0}.css", "Local"))
        End If
    End Sub

End Class
