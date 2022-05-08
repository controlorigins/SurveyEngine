Imports CODataCon
Imports CODataCon.com.controlorigins.ws

Public Class Co_Apps_SurveyApp_SurveyResponse
    Inherits SurveyUserControlBase
    Implements ISurveyResponse
    Public mySurveyResponse As New SurveyResponseItemUTIL
    Public myGroupQuestionList As New List(Of QuestionItem)
    Private myControl As ISurveyQuestionControl

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        PopulateSurveyResponse()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

         ' SetupGroupMenu()
         LoadSurvey()
    End Sub

    Protected Sub cmd_GroupSelection_Click(sender As LinkButton, e As EventArgs)
        mySurveyResponse.CurrentQuestionGroupID = sender.Attributes("data-quesitongroupid")
        SetupGroupMenu()
        LoadSurvey()
    End Sub
    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

    End Sub
    Protected Sub btn_Cancel_Click(sender As Object, e As EventArgs)

    End Sub

    Public Sub SetupGroupMenu()
        'Process the Groups - Figure out which group to show, then build the group menu
        If mySurveyResponse.Survey.QuestionGroupList.Count > 1 Then
            If mySurveyResponse.CurrentQuestionGroupID < 1 Then
                If mySurveyResponse.Survey.QuestionGroupList.Count > 0 Then
                    mySurveyResponse.CurrentQuestionGroupID = mySurveyResponse.Survey.QuestionGroupList(0).QuestionGroupID
                End If
                For Each QGroup As QuestionGroupItem In mySurveyResponse.Survey.QuestionGroupList
                    If Not mySurveyResponse.IsGroupComplete(QGroup) Then
                        mySurveyResponse.CurrentQuestionGroupID = QGroup.QuestionGroupID
                        Exit For
                    End If
                Next
            End If
            GroupMenu.Visible = True
            Dim myGroupMenuItems As New List(Of GroupMenuItem)
            For Each myGroup In mySurveyResponse.Survey.QuestionGroupList
                Dim myGroupItem As New GroupMenuItem() With {.QuestionGroupID = myGroup.QuestionGroupID,
                                                             .QuestionGroupNM = myGroup.QuestionGroupNM,
                                                             .QuestionGroupDS = myGroup.QuestionGroupDS,
                                                             .CssClass = "",
                                                             .QuestionGroupOrder = myGroup.QuestionGroupOrder
                                                              }
                If myGroup.QuestionGroupID = mySurveyResponse.CurrentQuestionGroupID Then
                    myGroupItem.CssClass = "active"
                End If
                myGroupMenuItems.Add(myGroupItem)
            Next
            GroupMenu.DataSource = myGroupMenuItems
            GroupMenu.DataBind()
        Else
            mySurveyResponse.CurrentQuestionGroupID = mySurveyResponse.Survey.QuestionGroupList(0).QuestionGroupID
            GroupMenu.Visible = False
        End If
    End Sub
    Protected Sub PopulateSurveyResponse()
        Using mypresenter As New SurveyResponseUI()
            mypresenter.SetSurveyResponseUI(Me)
            mypresenter.GetSurveyResponseBySurveyResponseID()
        End Using
    End Sub


    Protected Sub LoadSurvey()
        If SurveyResponseID > 0 Then
            If mySurveyResponse.Survey.QuestionGroupList.Count > 1 Then
                myGroupQuestionList = QuestionListUtility.FindQuestionByQuestionGroupID(mySurveyResponse.CurrentQuestionGroupID, mySurveyResponse.Survey.QuestionList)
            Else
                myGroupQuestionList = mySurveyResponse.Survey.QuestionList.ToList()
            End If

            Dim myAnswers As New SurveyResponseAnswerListBL(mySurveyResponse.AnswerList)
            For Each mySeq In mySurveyResponse.SequenceList
                If mySurveyResponse.SequenceList.Count > 1 Then
                    content.Controls.Add(New Literal With {.Text = String.Format("<div class='col-lg-4 panel panel-default '><div class='panel-heading'>{0}</div><div class='panel-body'>", mySeq.SequenceText)})
                Else
                    content.Controls.Add(New Literal With {.Text = "<div class='col-lg-12'><div>"})
                End If
                For Each myQuestion In myGroupQuestionList
                    Select Case myQuestion.QuestionTypeCD
                        Case "DDL"
                            myControl = DirectCast(Page.LoadControl("/Co_Apps/SurveyApp/controls/SurveyDropDownList.ascx"), ISurveyQuestionControl)
                        Case "CBL"
                            myControl = DirectCast(Page.LoadControl("/Co_Apps/SurveyApp/controls/SurveyCheckBoxList.ascx"), ISurveyQuestionControl)
                        Case "Int"
                            myControl = DirectCast(Page.LoadControl("/Co_Apps/SurveyApp/controls/SurveyInteger.ascx"), ISurveyQuestionControl)
                        Case "DT"
                            myControl = DirectCast(Page.LoadControl("/Co_Apps/SurveyApp/controls/SurveyDate.ascx"), ISurveyQuestionControl)
                        Case "CUR"
                            myControl = DirectCast(Page.LoadControl("/Co_Apps/SurveyApp/controls/SurveyCurrency.ascx"), ISurveyQuestionControl)
                        Case "com"
                            myControl = DirectCast(Page.LoadControl("/Co_Apps/SurveyApp/controls/SurveyComment.ascx"), ISurveyQuestionControl)
                        Case "PCT"
                            myControl = DirectCast(Page.LoadControl("/Co_Apps/SurveyApp/controls/SurveyPercent.ascx"), ISurveyQuestionControl)
                        Case "CALC"
                            myControl = DirectCast(Page.LoadControl("/Co_Apps/SurveyApp/controls/SurveyComment.ascx"), ISurveyQuestionControl)
                        Case "RBL"
                            myControl = DirectCast(Page.LoadControl("/Co_Apps/SurveyApp/controls/SurveyRadioButtonList.ascx"), ISurveyQuestionControl)
                        Case "TXT"
                            myControl = DirectCast(Page.LoadControl("/Co_Apps/SurveyApp/controls/SurveyComment.ascx"), ISurveyQuestionControl)
                        Case "DBL"
                            myControl = DirectCast(Page.LoadControl("/Co_Apps/SurveyApp/controls/SurveyInteger.ascx"), ISurveyQuestionControl)
                        Case Else
                            myControl = DirectCast(Page.LoadControl("/Co_Apps/SurveyApp/controls/SurveyComment.ascx"), ISurveyQuestionControl)
                    End Select
                    myControl.SetControlID(String.Format("SR{0}SQ{1}Q{2}", mySurveyResponse.SurveyResponseID, mySeq.SequenceID, myQuestion.QuestionID))
                    myControl.SetQuestion(myQuestion, myAnswers.FindAnswersByQuestionID(myQuestion.QuestionID, mySeq.SequenceNumber))
                    content.Controls.Add(myControl)
                Next
                content.Controls.Add(New Literal With {.Text = "</div></div>"})
            Next
        End If
    End Sub
    Protected Sub cmd_submit_Click(sender As Object, e As EventArgs)
        Dim myAnswers As New List(Of SurveyResponseAnswerItem)
        Dim myAnswer As SurveyResponseAnswerItem
        Dim myGroupQuestionList As New List(Of QuestionItem)
        If mySurveyResponse.Survey.QuestionGroupList.Count > 1 Then
            myGroupQuestionList = QuestionListUtility.FindQuestionByQuestionGroupID(mySurveyResponse.CurrentQuestionGroupID, mySurveyResponse.Survey.QuestionList)
        Else
            myGroupQuestionList = mySurveyResponse.Survey.QuestionList.ToList()
        End If

        For Each mySeq In mySurveyResponse.SequenceList
            For Each myQuestion In myGroupQuestionList
                myControl = CType(content.FindControl(String.Format("SR{0}SQ{1}Q{2}", mySurveyResponse.SurveyResponseID, mySeq.SequenceID, myQuestion.QuestionID)), ISurveyQuestionControl)
                If Not myControl Is Nothing Then
                    myAnswer = myControl.GetAnswer(mySurveyResponse.SurveyResponseID, mySeq.SequenceNumber, myQuestion.QuestionID)
                    If IsValidAnswer(myAnswer) Then
                        myAnswers.Add(myAnswer)
                    End If
                End If
            Next
        Next
        If myAnswers.Count > 0 Then
            Using mypresenter As New SurveyResponseUI()
                mypresenter.SetSurveyResponseUI(Me)
                mypresenter.GetSurveyResponseBySurveyResponseID()
                If Not mySurveyResponse Is Nothing Then
                    If mySurveyResponse.IsValid() Then
                        mySurveyResponse.NewAnswerList = myAnswers.ToArray()
                        mySurveyResponse.ModifiedID = CurUser.ApplicationUserID
                        mySurveyResponse.DataSource = HttpContext.Current.Request.Url.DnsSafeHost
                        mypresenter.ProcessNewAnswers(mySurveyResponse.CurrentQuestionGroupID)
                    End If
                End If
            End Using
        End If

        Dim CurGroupID As Integer = 0
        Dim NextGroupID As Integer = 0
        Dim GroupNumber As Integer = 1
        For Each myGroup As QuestionGroupItem In mySurveyResponse.Survey.QuestionGroupList
            If myGroup.QuestionGroupID = mySurveyResponse.CurrentQuestionGroupID Then
                mySurveyResponse.SetNextPreviousGroupID(CurGroupID, NextGroupID, GroupNumber)
                Exit For
            End If
            GroupNumber = GroupNumber + 1
        Next

        If NextGroupID = GlobalApplicationProperties.INT_SurveyCompleteGroupID Then
            ' Show Done Message
        Else
            mySurveyResponse.CurrentQuestionGroupID = NextGroupID
        End If

    End Sub
    Public Function GetGroupMenuClass(ByVal QuestionGroupID As Integer) As String
        Return "active"
    End Function

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
