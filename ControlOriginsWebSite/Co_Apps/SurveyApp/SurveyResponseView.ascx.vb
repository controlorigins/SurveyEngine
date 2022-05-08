Imports CODataCon
Imports CODataCon.com.controlorigins.ws

Public Class Co_Apps_SurveyApp_SurveyResponseView
    Inherits SurveyUserControlBase
    Implements ISurveyResponse

    Public ctlPlaceHolderLeftNav As Control
    Public mySurveyResponse As New SurveyResponseItemUTIL

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Used to build HTML Display of Response
        SurveyResponseID = GetPageArgument("SurveyResponseID").Second
        If SurveyResponseID > 0 Then
            Using mypresenter As New SurveyResponseUI()
                mypresenter.SetSurveyResponseUI(Me)
                mypresenter.GetSurveyResponseBySurveyResponseID()
                SetPageArgument("surveyresponseid", SurveyResponseID)
            End Using
        End If
        If mySurveyResponse.IsValid() Then
            pnlForm.Controls.Add(New Literal With {.Text = mySurveyResponse.GetViewOnlyForm()})
            pnlError.Controls.Add(New Literal With {.Text = mySurveyResponse.GetSurveyResponseHistory()})
            LoadSubmitPanel("Continue")
            pnlForm.Visible = True
            mySurveyResponse.SaveXML()
        End If
    End Sub

    Private Sub LoadSubmitPanel(ByVal ButtonText As String)
        Dim sbHTML As StringBuilder = New StringBuilder(String.Empty)
        sbHTML.Append(String.Format("<input name=""SubmitButton"" class=""btn btn-primary"" id=""SubmitButton"" type=""submit"" value=""{0}""/>", ButtonText))
        sbHTML.Append(String.Format("<div style=""display:none;"" id=""PostBackMsg"">{0}</div>", GetMessageBox("Processing Information", "Processing your information, please wait", "info")))
        pnlSubmit.Controls.AddAt(0, New Literal With {.Text = sbHTML.ToString()})
    End Sub

#Region "Implementation of the Interfaces "
    Public Property AssignedUserID As Integer? Implements ISurveyResponse.AssignedUserID
        Get
            Return mySurveyResponse.AssignedUserID
        End Get
        Set(value As Integer?)
            mySurveyResponse.AssignedUserID = value
        End Set
    End Property
    Public Property AssignedSupervisorUserID As Integer? Implements ISurveyResponse.AssignedSupervisorUserID
        Get
            Return mySurveyResponse.AssignedSupervisorUserID
        End Get
        Set(value As Integer?)
            mySurveyResponse.AssignedSupervisorUserID = value
        End Set
    End Property

    Public Property DataSource As String Implements ISurveyResponse.DataSource
        Get
            Return mySurveyResponse.DataSource
        End Get
        Set(value As String)
            mySurveyResponse.DataSource = value
        End Set
    End Property

    Public Property SurveyResponseModifiedID As Integer Implements ISurveyResponse.ModifiedID
        Get
            Return mySurveyResponse.ModifiedID
        End Get
        Set(value As Integer)
            mySurveyResponse.ModifiedID = value
        End Set
    End Property

    Public Property StatusID As Integer Implements ISurveyResponse.StatusID
        Get
            Return mySurveyResponse.StatusID
        End Get
        Set(value As Integer)
            mySurveyResponse.StatusID = value
        End Set
    End Property

    Public Property SurveyResponseNM As String Implements ISurveyResponse.SurveyResponseNM
        Get
            Return mySurveyResponse.SurveyResponseNM
        End Get
        Set(value As String)
            mySurveyResponse.SurveyResponseNM = value
        End Set
    End Property

    Public Property SurveyResponseID As Integer Implements ISurveyResponse.SurveyResponseID
        Get
            Return mySurveyResponse.SurveyResponseID
        End Get
        Set(value As Integer)
            mySurveyResponse.SurveyResponseID = value
        End Set
    End Property

    Public Property SurveyResponseAccountNM As String Implements ISurveyResponse.AccountNM
        Get
            Return mySurveyResponse.AccountNM
        End Get
        Set(value As String)
            mySurveyResponse.AccountNM = value
        End Set
    End Property

    Public Property CurrentAnswerList As List(Of SurveyResponseAnswerItem) Implements ISurveyResponse.AnswerList
        Get
            Return mySurveyResponse.AnswerList.ToList
        End Get
        Set(value As List(Of SurveyResponseAnswerItem))
            mySurveyResponse.AnswerList = value.ToArray
        End Set
    End Property

    Public Property SequenceList As List(Of SurveyResponseSequenceItem) Implements ISurveyResponse.SequenceList
        Get
            Return mySurveyResponse.SequenceList.ToList()
        End Get
        Set(value As List(Of SurveyResponseSequenceItem))
            mySurveyResponse.SequenceList = value.ToArray
        End Set
    End Property

    Public Property StatusNM As String Implements ISurveyResponse.StatusNM
        Get
            Return mySurveyResponse.StatusNM
        End Get
        Set(value As String)
            mySurveyResponse.StatusNM = value
        End Set
    End Property

    Public Property SurveyResponseSurvey As SurveyItem Implements ISurveyResponse.Survey
        Get
            Return mySurveyResponse.Survey
        End Get
        Set(value As SurveyItem)
            mySurveyResponse.Survey = value
        End Set
    End Property

    Public Property ShowQuestionDescription As Boolean Implements ISurveyResponse.ShowQuestionDescription
        Get
            Return mySurveyResponse.ShowQuestionDescription
        End Get
        Set(value As Boolean)
            mySurveyResponse.ShowQuestionDescription = value
        End Set
    End Property

    Public Property NewAnswerList As List(Of SurveyResponseAnswerItem) Implements ISurveyResponse.NewAnswerList
        Get
            Return mySurveyResponse.NewAnswerList.ToList()
        End Get
        Set(value As List(Of SurveyResponseAnswerItem))
            mySurveyResponse.NewAnswerList = value.ToArray()
        End Set
    End Property

    Public Overloads Property ApplicationID As Integer Implements ISurveyResponse.ApplicationID
        Get
            Return mySurveyResponse.ApplicationID
        End Get
        Set(value As Integer)
            mySurveyResponse.ApplicationID = value
        End Set
    End Property
    Public Property NewAnswerReviewList As List(Of SurveyResponseAnswerReviewItem) Implements ISurveyResponse.NewAnswerReviewList
        Get
            Return mySurveyResponse.NewAnswerReviewList.ToList()
        End Get
        Set(value As List(Of SurveyResponseAnswerReviewItem))
            mySurveyResponse.NewAnswerReviewList = value.ToArray()
        End Set
    End Property
    Public Property SurveyResponseHistory As List(Of SurveyResponseHistoryItem) Implements ISurveyResponse.SurveyResponseHistory
        Get
            Return mySurveyResponse.SurveyResponseHistory.ToList()
        End Get
        Set(value As List(Of SurveyResponseHistoryItem))
            mySurveyResponse.SurveyResponseHistory = value.ToArray()
        End Set
    End Property
    Public Property ReviewerAccountNM As String Implements ISurveyResponse.SupervisorAccountNM
        Get
            Return mySurveyResponse.SupervisorAccountNM
        End Get
        Set(value As String)
            mySurveyResponse.SupervisorAccountNM = value
        End Set
    End Property
    Public Property AnswerCount As Integer Implements ISurveyResponse.AnswerCount
        Get
            Return mySurveyResponse.AnswerCount
        End Get
        Set(value As Integer)
            mySurveyResponse.AnswerCount = value
        End Set
    End Property

    Public Property ComplianceReviewCount As Integer Implements ISurveyResponse.ComplianceReviewCount
        Get
            Return mySurveyResponse.ComplianceReviewCount
        End Get
        Set(value As Integer)
            mySurveyResponse.ComplianceReviewCount = value
        End Set
    End Property
    Public Property Manager_Name As String Implements ISurveyResponse.Manager_Name
        Get
            Return mySurveyResponse.Manager_Name
        End Get
        Set(value As String)
            mySurveyResponse.Manager_Name = value
        End Set
    End Property
    Public Property Employee_FName As String Implements ISurveyResponse.Employee_FName
        Get
            Return mySurveyResponse.Employee_FName
        End Get
        Set(value As String)
            mySurveyResponse.Employee_FName = value
        End Set
    End Property
    Public Property Employee_LName As String Implements ISurveyResponse.Employee_LName
        Get
            Return mySurveyResponse.Employee_LName
        End Get
        Set(value As String)
            mySurveyResponse.Employee_LName = value
        End Set
    End Property

    Public Property ManagerUserID As String Implements ISurveyResponse.ManagerUserID
        Get
            Return mySurveyResponse.ManagerUserID
        End Get
        Set(value As String)
            mySurveyResponse.ManagerUserID = value
        End Set
    End Property

    Public Property PercentComplete As Integer Implements ISurveyResponse.PercentComplete
        Get
            Return mySurveyResponse.PercentComplete
        End Get
        Set(value As Integer)
            mySurveyResponse.PercentComplete = value
        End Set
    End Property

    Public Property QuestionCount As Integer Implements ISurveyResponse.QuestionCount
        Get
            Return mySurveyResponse.QuestionCount
        End Get
        Set(value As Integer)
            mySurveyResponse.QuestionCount = value
        End Set
    End Property

    Public Property VariantAnswersCount As Integer Implements ISurveyResponse.VariantAnswersCount
        Get
            Return mySurveyResponse.VariantAnswersCount
        End Get
        Set(value As Integer)
            mySurveyResponse.VariantAnswersCount = value
        End Set
    End Property
    Public Property StateList As List(Of SurveyResponseStateItem) Implements ISurveyResponse.StateList
        Get
            Return mySurveyResponse.StateList.ToList()
        End Get
        Set(value As List(Of SurveyResponseStateItem))
            mySurveyResponse.StateList = value.ToArray()
        End Set
    End Property

#End Region


End Class
