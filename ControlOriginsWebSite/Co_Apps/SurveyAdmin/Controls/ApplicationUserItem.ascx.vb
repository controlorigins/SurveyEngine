Imports CODataCon.com.controlorigins.ws
Imports CODataCon
Imports DataGridVisualization.ControlOriginsWS


Public Class Co_Apps_SurveyAdmin_Controls_ApplicationUserItem
    Inherits SurveyUserControlBase

    Public myApplicationUser As New ApplicationUserItem With {.ApplicationUserID = -1}

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim ApplicationUserID = AppUtility.GetDBInteger(GetPageArgument("applicationuserid").Second, 0)

        If ApplicationUserID > 0 Then

            Select Case GetPageArgument("action").Second
                Case "applicationuserview"
                    Try
                        myApplicationUser = myCon.GetApplicationUserByApplicationUserID(ApplicationUserID)
                    Catch ex As Exception
                        myApplicationUser = New ApplicationUserItem With {.ApplicationUserID = -1}
                    End Try
                Case Else
                    myApplicationUser = New ApplicationUserItem With {.ApplicationUserID = -1}
            End Select
            With myApplicationUser
                tbAccountNM.Text = .AccountNM
                hfApplicationUserID.Value = .ApplicationUserID
                tbEMailAddress.Text = .eMailAddress
                tbFirstNM.Text = .FirstNM
                tbLastNM.Text = .LastNM
                tbComment.Text = .CommentDS
                tbSupervisorAccountNM.Text = .SupervisorAccountNM
                tbLastLoginDT.Text = .LastLoginDT
                tbLastLogin.Text = .LastLoginLocation
                If .CompanyID > 0 Then
                    ddlCompany.SelectedValue = .CompanyID
                End If

                dtApplications.Visible = True
                Dim myDisplayTableHeader As New DataGridVisualization.DisplayTableHeader
                myDisplayTableHeader.TableTitle = "Applications"
                myDisplayTableHeader.DetailFieldName = "ApplicationNM"
                myDisplayTableHeader.DetailKeyName = "ApplicationID"
                myDisplayTableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=applicationview&applicationid={0}"
                myDisplayTableHeader.AddHeaderItem("RoleCD", "RoleCD")
                dtApplications.BuildTable(myDisplayTableHeader, .ApplicationUserRoleList)

                dtSurveyResponse.Visible = True
                Dim mySRDisplayTableHeader As New DataGridVisualization.DisplayTableHeader
                mySRDisplayTableHeader.TableTitle = "Survey Response List"
                mySRDisplayTableHeader.DetailFieldName = "SurveyResponseNM"
                mySRDisplayTableHeader.DetailKeyName = "SurveyResponseID"
                mySRDisplayTableHeader.DetailPath = "/CO_Apps/SurveyAdmin/navigator.aspx?action=surveyresponseview&surveyresponseid={0}"
                mySRDisplayTableHeader.AddHeaderItem("StatusNM", "StatusNM", True, DisplayFormat.Text)
                mySRDisplayTableHeader.AddHeaderItem("SurveyResponseScore", "SurveyResponseScore", True, DisplayFormat.Number)
                mySRDisplayTableHeader.AddHeaderItem("AnswerCount", "AnswerCount", True, DisplayFormat.Number)
                mySRDisplayTableHeader.AddHeaderItem("QuestionCount", "QuestionCount", True, DisplayFormat.Number)
                dtSurveyResponse.BuildTable(mySRDisplayTableHeader, .SurveyResponseList)


            End With
        Else
            myApplicationUser = New ApplicationUserItem With {.ApplicationUserID = -1}
            hfApplicationUserID.Value = -1
        End If
    End Sub

    Protected Sub cmd_Save_Click(sender As Object, e As EventArgs)
        myApplicationUser = myCon.GetApplicationUserByApplicationUserID(hfApplicationUserID.Value)
        With myApplicationUser
            .AccountNM = tbAccountNM.Text
            .ApplicationUserID = hfApplicationUserID.Value
            .eMailAddress = tbEMailAddress.Text
            .FirstNM = tbFirstNM.Text
            .LastNM = tbLastNM.Text
            .CommentDS = tbComment.Text
            .CompanyID = ddlCompany.SelectedValue
            .SupervisorAccountNM = tbSupervisorAccountNM.Text
            .ModifiedID = UserInfo.ApplicationUserID
            .ModifiedDT = Now()
        End With
        myApplicationUser = myCon.UpdateApplicationUser(myApplicationUser)
        hfApplicationUserID.Value = myApplicationUser.ApplicationUserID

        If myApplicationUser.ApplicationUserID > 0 Then
            ClearPageArguments(GetPageArgument("pid").Second.ToString)
            SetPageArgument("applicationuserid", 0)
            SetPageArgument("action", "applicationuserview")
            LoadPage()
        End If

    End Sub

    Protected Sub cmd_Cancel_Click(sender As Object, e As EventArgs)

        ClearPageArguments( GetPageArgument("pid").Second.ToString)
        SetPageArgument("applicationuserid", 0)
        SetPageArgument("action", "applicationuserview")
        LoadPage()
    End Sub

    Protected Sub cmd_Delete_Click(sender As Object, e As EventArgs)
        With myApplicationUser
            .AccountNM = tbAccountNM.Text
            .ApplicationUserID = hfApplicationUserID.Value
            .eMailAddress = tbEMailAddress.Text
            .FirstNM = tbFirstNM.Text
            .LastNM = tbLastNM.Text
            .CommentDS = tbComment.Text
            .SupervisorAccountNM = tbSupervisorAccountNM.Text
            .ModifiedID = 99999
        End With
        myCon.DeleteApplicationUser(myApplicationUser)

        ClearPageArguments(GetPageArgument("pid").Second.ToString)
        SetPageArgument("applicationuserid", 0)
        SetPageArgument("action", "applicationuserview")
        LoadPage()
    End Sub
End Class
