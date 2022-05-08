Imports System.Threading
Imports System.Activities
Imports CODataCon.com.controlorigins.ws

Partial Class controls_NavFrame
    Inherits NavigationControlBase

    Property pagename As String

    Sub pagestartup() Handles Me.Load
        If Not IsPostBack Then
            GetCurrentUser()
            GetCurrentApp()
            ' workflowFoo()
        End If
    End Sub

    Sub workflowFoo()
        Dim syncEvent As New AutoResetEvent(False)
        Dim mydict As New Dictionary(Of String, Object)
        mydict.Add("UserID", UserInfo.ApplicationUserID)
        mydict.Add("AppID", AppInfo.ApplicationID)
        Dim wfApp As WorkflowApplication = New WorkflowApplication(New ActivityLibraryCOSAS.linq2sqltest(), mydict)
        With wfApp
            .Completed = Sub(e As WorkflowApplicationCompletedEventArgs)
                             Dim thisthing = e.Outputs
                             Me.Attributes.Add("something", thisthing("cmd_MyappsVisible").ToString)
                             'If UserInfo.UserID >= 1 Then
                             'Else
                             '    UserInfo = GetGuestUser()
                             'End If

                             'ulMessageDropdown.Visible = thisthing("ulMessageDropdownVisible")
                             'cmd_Myapps.Visible = thisthing("cmd_MyappsVisible")
                             syncEvent.Set()
                         End Sub
            .Aborted = Sub(e As WorkflowApplicationAbortedEventArgs)
                           Console.WriteLine(e.Reason)
                           syncEvent.Set()
                       End Sub
            .OnUnhandledException = Function(e As WorkflowApplicationUnhandledExceptionEventArgs)
                                        Console.WriteLine(e.UnhandledException)
                                        syncEvent.Set()
                                        Return UnhandledExceptionAction.Terminate
                                    End Function
        End With
        
        wfApp.Run()

        syncEvent.WaitOne()

    End Sub



    Protected Sub GetCurrentUser()

        If UserInfo.UserRoleID = 1 Then
            ulMessageDropdown.Visible = False
        Else
            ulMessageDropdown.Visible = True
        End If
    End Sub


    Private Function GetNewMenuItem(ByVal PID As Integer, ByVal i As NavigationMenuItem) As NavigationMenuItem
        Dim NewMenuItem As New NavigationMenuItem
        With NewMenuItem
            .Id = i.Id
            .GlyphName = i.GlyphName
            .Id = i.Id
            .MenuOrder = i.MenuOrder
            .MenuText = i.MenuText
            .SiteAppID = i.SiteAppID
            .SiteRoleID = i.SiteRoleID
            .TartgetPage = i.TartgetPage
            If i.Id = PID Then
                .IsSelected = True
                .Css = "selected"
                pagename = " - " & GetNavigationMenuItem(AppInfo,PID).MenuText
            Else
                .IsSelected = False
                .Css = " "
            End If
        End With
        Return NewMenuItem
    End Function
    Protected Sub GetCurrentApp()

        Dim mycoll = (From i In AppInfo.Navigation Order By i.MenuOrder).ToList

        If AppInfo.ApplicationID < 1 Then
            mycoll.AddRange(AppControler.GetAppNavigation(1).ToList)
        End If

        If AppInfo.ApplicationID > 1 Then
            cmd_Myapps.Visible = True
        End If

        If UserInfo.UserRoleID = 1 Then
        Else
            Dim removeloginreg = (From i In mycoll Where i.SiteRoleID = 1).ToList
            For Each i In removeloginreg
                mycoll.Remove(i)
            Next
        End If


        Dim removemenuitems = (From i In mycoll Where i.SiteRoleID > UserInfo.UserRoleID Or i.ViewInMenu = False).ToList
        For Each i In removemenuitems
            mycoll.Remove(i)
        Next

        Dim menulist As New List(Of NavigationMenuItem)
        Dim PID As Integer
        Dim comset = (From i In GetPageArguments() Where i.Key.ToLower = "pid")
        If comset.Count > 0 Then
            PID = comset(0).Value
        Else
            PID = 1
        End If

        If PID = 1 And UserInfo.UserRoleID > 1 Then
            PID = 3
        End If

        For Each i In mycoll
            Dim NewMenuItem As NavigationMenuItem = GetNewMenuItem(PID, i)
            menulist.Add(NewMenuItem)
        Next
        CurrentMenu.DataSource = menulist
        CurrentMenu.DataBind()
    End Sub

    Protected Sub UserMessaging()
        messagepreview.DataSource = GetMessages(UserInfo.ApplicationUserID)
        messagepreview.DataBind()
    End Sub

    Protected Sub cmd_logout_Click()
        Response.Redirect("/logout.aspx")
    End Sub

    Protected Sub cmd_PageSelection_Click(sender As LinkButton, e As System.EventArgs)
        SetPageArgument("pid", sender.Attributes("data-pid"))
        Response.Redirect("/")
    End Sub

    Protected Sub cmd_gotoInbox_Click()
        AppInfo = GetAppByID(1)
        SetPage("CoUserMessages.ascx")
        Response.Redirect("/")
    End Sub

    Protected Sub cmd_Myapps_Click()
        AppInfo = GetAppByID(1)
        SetPage("CODashBoard.ascx")
        Response.Redirect("/")
    End Sub
End Class