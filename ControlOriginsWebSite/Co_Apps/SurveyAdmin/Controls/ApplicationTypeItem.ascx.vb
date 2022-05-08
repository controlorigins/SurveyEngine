Imports CODataCon.com.controlorigins.ws
Imports CODataCon

Public Class Co_Apps_SurveyAdmin_ApplicationType
    Inherits SurveyUserControlBase
    Public myApplicationType As ApplicationTypeItem
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        hfApplicationTypeID.Value = AppUtility.GetDBInteger(GetPageArgument("ApplicationTypeid").Second, 0)
        If Not IsPostBack Then
            If AppUtility.GetDBInteger(hfApplicationTypeID.Value) > 0 Then
                myApplicationType = myCon.GetApplicationTypeByApplicationTypeID(AppUtility.GetDBInteger(hfApplicationTypeID.Value))
                With myApplicationType
                    tbApplicationTypeNM.Text = .ApplicationTypeNM
                    tbApplicationTypeDS.Text = .ApplicationTypeDS
                End With
            End If
        End If
    End Sub
    Protected Sub cmd_SaveApplicationType_Click(sender As Object, e As EventArgs)
        If hfApplicationTypeID.Value = "-1" Then
            myApplicationType = New ApplicationTypeItem With {.ApplicationTypeID = -1}
        Else
            myApplicationType = myCon.GetApplicationTypeByApplicationTypeID(AppUtility.GetDBInteger(hfApplicationTypeID.Value))
        End If
        With myApplicationType
            .ApplicationTypeNM = tbApplicationTypeNM.Text
            .ApplicationTypeDS = tbApplicationTypeDS.Text
        End With
        myCon.PutApplicationType(myApplicationType)
        ReloadPage()
    End Sub
    Protected Sub cmd_CancelApplicationType_Click(sender As Object, e As EventArgs)
        ReloadPage()
    End Sub
    Protected Sub cmd_DeleteApplicationType_Click(sender As Object, e As EventArgs)
        myApplicationType = myCon.GetApplicationTypeByApplicationTypeID(AppUtility.GetDBInteger(hfApplicationTypeID.Value))
        myCon.DeleteApplicationType(myApplicationType)
        ReloadPage()
    End Sub
    Protected Sub ReloadPage()
        ClearPageArguments()
        SetPageArgument("ApplicationTypeid", 0)
        SetPageArgument("action", "applicationtypeview")
        LoadPage()
    End Sub


End Class
