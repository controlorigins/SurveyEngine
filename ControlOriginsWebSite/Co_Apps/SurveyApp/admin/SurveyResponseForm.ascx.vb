Imports CODataCon.com.controlorigins.ws

Partial Class Co_Apps_SurveyApp_controls_SurveyResponseForm
    Inherits System.Web.UI.UserControl

    Public Sub SetSurveyResponse(surveyresponse As SurveyResponseItem, app As ApplicationItem)
        Dim myStatusList As New List(Of LookupItem)
        Dim mySurveyList As New List(Of LookupItem)
        Dim myApplicationUserList As New List(Of LookupItem)

        For Each mySurvey In app.ApplicationSurveyList
            mySurveyList.Add(New LookupItem With {.Value = mySurvey.Survey.SurveyID, .Name = mySurvey.Survey.SurveyNM})

            If mySurvey.Survey.SurveyID = surveyresponse.Survey.SurveyID Then
                myStatusList.AddRange((From i In mySurvey.Survey.StatusList Select New LookupItem With {.Name = i.StatusNM, .Value = i.StatusID}).ToList)
            End If
        Next
        If myStatusList.Count = 0 Then
            myStatusList.Add(New LookupItem With {.Value = 1, .Name = "Assigned"})
        End If

        myApplicationUserList.Add(New LookupItem With {.Value = "", .Name = "All Users"})
        myApplicationUserList.AddRange((From i In app.ApplicationUserList Select New LookupItem With {.Name = i.AccountNM, .Value = i.ApplicationUserID}).ToList)
        

        DDStatus.Items.Clear()
        DDStatus.DataSource = myStatusList
        DDStatus.DataTextField = "Name"
        DDStatus.DataValueField = "Value"
        DDStatus.DataBind()

        DDServey.Items.Clear()
        DDServey.DataSource = mySurveyList
        DDServey.DataTextField = "Name"
        DDServey.DataValueField = "Value"
        DDServey.DataBind()

        DDAssignTo.Items.Clear()
        DDAssignTo.DataSource = myApplicationUserList
        DDAssignTo.DataTextField = "Name"
        DDAssignTo.DataValueField = "Value"
        DDAssignTo.DataBind()

        With surveyresponse
            LBID.Text = .SurveyResponseID
            TBName.Text = CODataCon.AppUtility.GetDBString(.SurveyResponseNM)

            If surveyresponse.StatusID > 0 Then
                ' DDStatus.SelectedValue = .StatusID
            End If

            If .AssignedUserID Is Nothing Then
            Else
                DDAssignTo.SelectedValue = .AssignedUserID
            End If

            If .Survey Is Nothing Then
            Else
                If .Survey.SurveyID > 0 Then
                    DDServey.SelectedValue = .Survey.SurveyID
                End If
            End If
            HFAppID.Value = app.ApplicationID
            HFModID.Value = 999
        End With
    End Sub

    Public ReadOnly Property GetSurveyResponse() As SurveyResponseItem
        Get
            Dim myresponseitem As New SurveyResponseItem

            With myresponseitem
                .Survey = New SurveyItem
                Try
                    .SurveyResponseID = CInt(LBID.Text)
                Catch ex As Exception
                    .SurveyResponseID = -1
                End Try
                .ApplicationID = HFAppID.Value
                .SurveyResponseNM = TBName.Text.Trim
                .StatusID = CInt(DDStatus.SelectedValue)
                .AssignedUserID = DDAssignTo.SelectedValue
                .Survey.SurveyID = CInt(DDServey.SelectedValue)
                .ApplicationID = CInt(HFAppID.Value)
                .ModifiedID = CInt(HFModID.Value)
                .ModifiedDT = Now
                .DataSource = "SAS.SurveyResponseForm"
            End With

            Return myresponseitem
        End Get

    End Property


End Class
