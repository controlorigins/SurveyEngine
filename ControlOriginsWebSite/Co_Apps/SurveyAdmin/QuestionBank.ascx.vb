Imports DataGridVisualization.ControlOriginsWS
Imports CODataCon.com.controlorigins.ws
Imports CODataCon
Imports System.IO

Partial Class Co_Apps_SurveyAdmin_Controls_QuestionBank
    Inherits SurveyUserControlBase

    Public myQuestion As QuestionItem

#Region "Question Filter"
    Protected Sub ddlSubCategory_DataBound(sender As Object, e As EventArgs) Handles ddlQuestionCategory.DataBound
        If ddlQuestionCategory.Items.Count > 0 Then
            ddlQuestionCategory.SelectedIndex = 0
            ddlSubCategory_SelectedIndexChanged(sender, e)
        Else
            If Not IsNothing(ddlCategory.SelectedItem) Then
                ddlQuestionCategory.Items.Add(New ListItem With {.Value = ddlCategory.SelectedValue, .Text = ddlCategory.SelectedItem.Text})
                ddlQuestionCategory.SelectedIndex = 0
                ddlSubCategory_SelectedIndexChanged(sender, e)
            End If
        End If
    End Sub
    Protected Sub ddlCategory_DataBound(sender As Object, e As EventArgs) Handles ddlCategory.DataBound
        If ddlCategory.Items.Count > 0 Then
            ddlCategory.SelectedIndex = 0
            ddlQuestionCategory.Items.Clear()
            ddlQuestionCategory.DataBind()
        Else
            ddlQuestionCategory.Items.Clear()
        End If
    End Sub
    Protected Sub ddlSubCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim myGrid As New CO_DataGrid
        Dim myQuestions As New List(Of QuestionItem)
        Dim iQuestionID As Integer
        Dim iQuestionNM As Integer
        Dim iQuestionShortNM As Integer
        Dim iQuestionDS As Integer
        Dim iQuestionOrder As Integer
        QuestionList.Visible = True

        Using myCon As New DataGridVisualization.DataController()
            myGrid = myCon.GetQuestions(ddlQuestionCategory.SelectedValue)
        End Using

        For gridindex = 0 To myGrid.GridColumns.Count - 1
            Dim x = gridindex
            If "QuestionID" = myGrid.GridColumns(x).DisplayName Then
                iQuestionID = x
            End If
            If "QuestionNM" = myGrid.GridColumns(x).DisplayName Then
                iQuestionNM = x
            End If
            If "QuestionShortNM" = myGrid.GridColumns(x).DisplayName Then
                iQuestionShortNM = x
            End If
            If "QuestionDS" = myGrid.GridColumns(x).DisplayName Then
                iQuestionDS = x
            End If
            If "QuestionSort" = myGrid.GridColumns(x).DisplayName Then
                iQuestionOrder = x
            End If
        Next

        For Each myRow In myGrid.GridRows
            myQuestions.Add(New QuestionItem With {.QuestionID = myRow.Value(iQuestionID),
                                                   .QuestionNM = myRow.Value(iQuestionNM),
                                                   .QuestionShortNM = myRow.Value(iQuestionShortNM),
                                                   .QuestionDS = myRow.Value(iQuestionDS),
                                                   .SurveyDisplayOrder = myRow.Value(iQuestionOrder)})
        Next

        QuestionList.DataSource = myQuestions
        QuestionList.DataBind()

        ClearPageArguments(GetPageArgument("pid").Second.ToString)
        SetPageArgument("action", "questionbank")
        pnlQuestionAnswer.Visible = False
        pnlQuestionDetail.Visible = False
    End Sub

#End Region
    Protected Sub cmd_selectQuestion_Click(sender As Object, e As EventArgs)
        SetPageArgument("questionid", CType(sender, LinkButton).Attributes("data-questionid"))
        SetPageArgument("action", "questionview")
        pnlQuestionDetail.Visible = True
        pnlQuestionList.Visible = False
        SetQuestion(CType(sender, LinkButton).Attributes("data-questionid"))
    End Sub

    ''' <summary>
    '''     Set True to Enable the Cat DropDown Functions or False to DisAble them.
    ''' </summary>
    ''' <param name="DC"></param>
    Private Sub EnableCatEditButtons(DC As Boolean)
        ddlApplicationType.Enabled = DC
        'cmd_ProjectTypeEdit.Visible = DC
        'cmd_ProjectTypeNew.Visible = DC

        ddlCategory.Enabled = DC
        cmd_SurveyCategoryEdit.Visible = DC
        cmd_SurveyCategoryNew.Visible = DC

        ddlQuestionCategory.Enabled = DC
        cmd_QuestionCategoryEdit.Visible = DC
        cmd_QuestionCategoryNew.Visible = DC


    End Sub


#Region "Question Administration"
    Public Sub SetQuestion(ByVal reqQuestionID As Integer)
        hfQuestionID.Value = reqQuestionID.ToString
        EnableCatEditButtons(False)

        If reqQuestionID > 0 Then
            Select Case GetPageArgument("action").Second.ToString.ToLower
                Case "questionview"
                    Try
                        myQuestion = myCon.GetQuestionByQuestionID(reqQuestionID)
                    Catch ex As Exception
                        myQuestion = New QuestionItem
                    End Try
                    Select Case GetPageArgument("subaction").Second.ToString.ToLower
                        Case "questionanswerid"
                            SetQuestionAnswer((From i In myQuestion.QuestionAnswerItemList Where i.QuestionAnswerID = hfQuestionAnswerID.Value Select i).SingleOrDefault)
                        Case Else
                    End Select
                Case Else
                    myQuestion = New QuestionItem
            End Select

            With myQuestion
                hfQuestionID.Value = .QuestionID
                tbQuestionNM.Text = .QuestionNM
                tbQuestionDS.Text = .QuestionDS
                ddlQuestionType.SelectedValue = .QuestionTypeID
                If .FileData.Length > 1 Then
                    QuestionImage.Visible = True
                    QuestionImage.ImageUrl = String.Format("/controls/GetImage.ashx?id={0}", .QuestionID)
                    QuestionImage.CssClass = "img-responsive"
                Else
                    QuestionImage.Visible = False
                    QuestionImage.ImageUrl = "/images/spacer.gif"
                    QuestionImage.CssClass = "img-responsive"
                End If
                QuestionAnswerList.DataSource = .QuestionAnswerItemList
                QuestionAnswerList.DataBind()
            End With
        Else
            myQuestion = New QuestionItem With {.QuestionID = -1}
            hfQuestionID.Value = -1
        End If

    End Sub
    Protected Sub cmd_SaveQuestion_Click(sender As Object, e As EventArgs)
        myQuestion = myCon.GetQuestionByQuestionID(AppUtility.GetDBInteger(hfQuestionID.Value))
        With myQuestion
            .QuestionNM = tbQuestionNM.Text
            .QuestionDS = tbQuestionDS.Text
            .QuestionSort = 1
            .CommentFL = True
            .ModifiedID = UserInfo.ApplicationUserID
            .QuestionTypeID = ddlQuestionType.SelectedValue
            .QuestionValue = 1
            .ReviewRoleLevel = 1
            .SurveyTypeID = ddlQuestionCategory.SelectedValue

            If FileUpload.HasFile Then
                FileUpload.SaveAs(Server.MapPath("/images/client/") & FileUpload.FileName)
                ' Read the file and convert it to Byte Array
                Dim filePath As String = Server.MapPath("/images/client/" & FileUpload.FileName)
                Dim filename As String = Path.GetFileName(filePath)
                Using fs As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
                    Using br As BinaryReader = New BinaryReader(fs)
                        .FileData = br.ReadBytes(Convert.ToInt32(fs.Length))
                    End Using
                End Using
            End If
        End With
        myQuestion = myCon.PutQuestionItem(myQuestion)
        hfQuestionID.Value = myQuestion.QuestionID

        ResetQuestionView(0)
    End Sub
    Protected Sub cmd_SaveAsNewQuestion_Click(sender As Object, e As EventArgs)
        myQuestion = myCon.GetQuestionByQuestionID(AppUtility.GetDBInteger(hfQuestionID.Value))
        With myQuestion
            .QuestionID = -1
            .QuestionNM = tbQuestionNM.Text
            .QuestionDS = tbQuestionDS.Text
            .QuestionSort = 1
            .CommentFL = True
            .Keywords = String.Empty
            .ModifiedID = UserInfo.ApplicationUserID
            .QuestionTypeID = ddlQuestionType.SelectedValue
            .QuestionValue = 1
            .ReviewRoleLevel = 1
            .SurveyTypeID = ddlQuestionCategory.SelectedValue
            If FileUpload.HasFile Then
                FileUpload.SaveAs(Server.MapPath("/images/client/") & FileUpload.FileName)
                ' Read the file and convert it to Byte Array
                Dim filePath As String = Server.MapPath("/images/client/" & FileUpload.FileName)
                Dim filename As String = Path.GetFileName(filePath)
                Using fs As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
                    Using br As BinaryReader = New BinaryReader(fs)
                        .FileData = br.ReadBytes(Convert.ToInt32(fs.Length))
                    End Using
                End Using
            End If
        End With
        For Each myA In myQuestion.QuestionAnswerItemList
            myA.QuestionAnswerID = -1
            myA.QuestionID = -1
        Next
        myQuestion = myCon.PutQuestionItem(myQuestion)
        hfQuestionID.Value = myQuestion.QuestionID
        ResetQuestionView(myQuestion.QuestionID)
    End Sub
    Protected Sub cmd_CancelQuestion_Click(sender As Object, e As EventArgs)
        ResetQuestionView(0)
    End Sub
    Protected Sub cmd_DeleteQuestion_Click(sender As Object, e As EventArgs)
        Dim QuestionID = AppUtility.GetDBInteger(hfQuestionID.Value)
        myCon.DeleteQuestionByQuestionID(QuestionID)
        ResetQuestionView(0)
    End Sub
    Protected Sub ResetQuestionView(ByVal reqQuestionID As Integer)
        ClearPageArguments(GetPageArgument("pid").Second.ToString)
        SetPageArgument("questionid", reqQuestionID)
        SetPageArgument("action", "questionview")
        If reqQuestionID > 0 Then
            hfQuestionID.Value = reqQuestionID
            hfQuestionAnswerID.Value = String.Empty
            Try
                myQuestion = myCon.GetQuestionByQuestionID(hfQuestionID.Value)
            Catch ex As Exception
                myQuestion = New QuestionItem
            End Try
            pnlQuestionAnswer.Visible = False
            pnlQuestionDetail.Visible = True
            ddlCategory.Enabled = True
            ddlApplicationType.Enabled = False
            ddlQuestionCategory.Enabled = False
            pnlQuestionList.Visible = False
            SetQuestion(reqQuestionID)
            EnableCatEditButtons(False)
        Else
            hfQuestionAnswerID.Value = String.Empty
            hfQuestionID.Value = String.Empty
            myQuestion = New QuestionItem
            pnlQuestionAnswer.Visible = False
            pnlQuestionDetail.Visible = False
            ddlCategory.Enabled = True
            ddlApplicationType.Enabled = True
            ddlQuestionCategory.Enabled = True
            pnlQuestionList.Visible = True
            ddlSubCategory_SelectedIndexChanged(New Object, New EventArgs)
            EnableCatEditButtons(True)
        End If
    End Sub

#End Region

#Region "Question Answer Administration"
    Protected Sub cmd_selectQuestionAnswer_Click(sender As Object, e As EventArgs)
        SetPageArgument("questionid", CType(sender, LinkButton).Attributes("data-questionid"))
        SetPageArgument("questionanswerid", CType(sender, LinkButton).Attributes("data-questionanswerid"))
        SetPageArgument("action", "questionbank")
        SetPageArgument("subaction", "questionanswerid")
        hfQuestionID.Value = CType(sender, LinkButton).Attributes("data-questionid")
        hfQuestionAnswerID.Value = CType(sender, LinkButton).Attributes("data-questionanswerid")
        pnlQuestionDetail.Visible = False
        pnlQuestionAnswer.Visible = True
        pnlQuestionList.Visible = False
        Try
            myQuestion = myCon.GetQuestionByQuestionID(hfQuestionID.Value)
        Catch ex As Exception
            myQuestion = New QuestionItem
        End Try
        lblQuestionAnswerTitle.Text = myQuestion.QuestionNM
        SetQuestionAnswer((From i In myQuestion.QuestionAnswerItemList Where i.QuestionAnswerID = hfQuestionAnswerID.Value Select i).SingleOrDefault)
    End Sub
    Public Sub SetQuestionAnswer(ByRef myAnswer As QuestionAnswerItem)
        If myAnswer Is Nothing Then
            hfQuestionAnswerID.Value = "-1"
        Else
            With myAnswer
                tbQuestionAnswerNM.Text = .QuestionAnswerNM
                tbQuestionAnswerDS.Text = .QuestionAnswerDS
                tbQuestionAnswerShortNM.Text = .QuestionAnswerShortNM
                tbQuestionAnswerSort.Text = .QuestionAnswerSort
                tbQuestionAnswerValue.Text = .QuestionAnswerValue
                cbQuestionAnswerActiveFL.Checked = .QuestionAnswerActiveFL
                cbQuestionAnswerCommentFL.Checked = .QuestionAnswerCommentFL
            End With
        End If
    End Sub
    Public Function GetQuestionAnswer() As QuestionAnswerItem
        Dim myAnswer As New QuestionAnswerItem
        With myAnswer
            .QuestionID = AppUtility.GetDBInteger(hfQuestionID.Value)
            .QuestionAnswerID = AppUtility.GetDBInteger(hfQuestionAnswerID.Value)
            .QuestionAnswerNM = tbQuestionAnswerNM.Text
            .QuestionAnswerDS = tbQuestionAnswerDS.Text
            .QuestionAnswerShortNM = tbQuestionAnswerShortNM.Text
            .QuestionAnswerSort = tbQuestionAnswerSort.Text
            .QuestionAnswerValue = tbQuestionAnswerValue.Text
            .QuestionAnswerActiveFL = cbQuestionAnswerActiveFL.Checked
            .QuestionAnswerCommentFL = cbQuestionAnswerCommentFL.Checked
        End With
        Return myAnswer
    End Function
    Protected Sub cmd_SaveQuestionAnswer_Click(sender As Object, e As EventArgs)
        myQuestion = myCon.GetQuestionByQuestionID(AppUtility.GetDBInteger(hfQuestionID.Value))
        myQuestion.ModifiedID = UserInfo.ApplicationUserID

        If hfQuestionAnswerID.Value = "-1" Then
            ' New Quesiton Answer
            myQuestion.QuestionAnswerItemList.Add(GetQuestionAnswer())
        Else
            ' update Question Answer
            For gIndex = 0 To myQuestion.QuestionAnswerItemList.Count - 1
                If myQuestion.QuestionAnswerItemList(gIndex).QuestionAnswerID = AppUtility.GetDBInteger(hfQuestionAnswerID.Value) Then
                    With myQuestion.QuestionAnswerItemList(gIndex)
                        .QuestionAnswerNM = tbQuestionAnswerNM.Text
                        .QuestionAnswerDS = tbQuestionAnswerDS.Text
                        .QuestionAnswerShortNM = tbQuestionAnswerShortNM.Text
                        .QuestionAnswerSort = tbQuestionAnswerSort.Text
                        .QuestionAnswerValue = tbQuestionAnswerValue.Text
                        .QuestionAnswerActiveFL = cbQuestionAnswerActiveFL.Checked
                        .QuestionAnswerCommentFL = cbQuestionAnswerCommentFL.Checked
                    End With
                    Exit For
                End If
            Next
        End If
        myQuestion = myCon.PutQuestionItem(myQuestion)
        ResetQuestionView(GetDBInteger(hfQuestionID.Value))
    End Sub
    Protected Sub cmd_CancelQuestionAnswer_Click(sender As Object, e As EventArgs)
        ResetQuestionView(GetDBInteger(hfQuestionID.Value))
    End Sub
    Protected Sub cmd_DeleteQuestionAnswer_Click(sender As Object, e As EventArgs)
        myQuestion = myCon.GetQuestionByQuestionID(AppUtility.GetDBInteger(hfQuestionID.Value))
        If hfQuestionAnswerID.Value = "-1" Then
            ' New Quesiton Answer - DO NOTHING
        Else
            ' update Question Answer
            For gIndex = 0 To myQuestion.QuestionAnswerItemList.Count - 1
                If myQuestion.QuestionAnswerItemList(gIndex).QuestionAnswerID = AppUtility.GetDBInteger(hfQuestionAnswerID.Value) Then
                    With myQuestion.QuestionAnswerItemList(gIndex)
                        .QuestionAnswerNM = tbQuestionAnswerNM.Text
                        .QuestionAnswerDS = tbQuestionAnswerDS.Text
                        .QuestionAnswerShortNM = tbQuestionAnswerShortNM.Text
                        .QuestionAnswerSort = tbQuestionAnswerSort.Text
                        .QuestionAnswerValue = tbQuestionAnswerValue.Text
                        .QuestionAnswerActiveFL = cbQuestionAnswerActiveFL.Checked
                        .QuestionAnswerCommentFL = cbQuestionAnswerCommentFL.Checked
                        .MarkedForDeletion = True
                    End With
                    Exit For
                End If
            Next
        End If
        myQuestion = myCon.PutQuestionItem(myQuestion)
        hfQuestionID.Value = myQuestion.QuestionID
        ResetQuestionView(GetDBInteger(hfQuestionID.Value))
    End Sub

#End Region

    Protected Sub cmd_SurveyCategoryEdit_Click(sender As Object, e As EventArgs)
        hfParentSurveyTypeID.Value = CInt(0)
        SetSurveyType(ddlCategory.SelectedItem.Value)
    End Sub

    Protected Sub cmd_SurveyCategoryNew_Click(sender As Object, e As EventArgs)
        hfParentSurveyTypeID.Value = CInt(0)
        SetSurveyType(-1)
    End Sub


#Region "Question Category Admin"
    Protected Sub cmd_QuestionCategoryNew_Click(sender As Object, e As EventArgs)
        hfParentSurveyTypeID.Value = CInt(ddlCategory.SelectedValue)
        SetSurveyType(-1)
    End Sub
    Protected Sub cmd_QuestionCategoryEdit_Click(sender As Object, e As EventArgs)
        SetSurveyType(ddlQuestionCategory.SelectedItem.Value)
    End Sub
    Private Sub SetSurveyType(ByVal SurveyTypeID As Integer)
        EnableCatEditButtons(False)
        pnlQuestionList.Visible = False
        pnlQuestionCategory.Visible = True
        If SurveyTypeID > 0 Then
            Dim myCat = myCon.GetSurveyTypeBySurveyTypeID(SurveyTypeID)
            tbSurveyTypeNM.Text = myCat.SurveyTypeNM
            tbSurveyTypeComment.Text = myCat.SurveyTypeComment
            tbSurveyTypeShortNM.Text = myCat.SurveyTypeShortNM
            tbSurveyTypeDS.Text = myCat.SurveyTypeDS
            hfCurSurveyTypeID.Value = myCat.SurveyTypeID
        Else
            tbSurveyTypeNM.Text = ""
            tbSurveyTypeComment.Text = ""
            tbSurveyTypeShortNM.Text = ""
            tbSurveyTypeDS.Text = ""
            hfCurSurveyTypeID.Value = -1
        End If
    End Sub
    Protected Sub cmd_SaveSurveyType_Click(sender As Object, e As EventArgs)
        Dim myCat = New SurveyTypeItem
        If CInt(hfCurSurveyTypeID.Value) > 0 Then
            ' we have an update
            ' Passupdate to WS
            myCat = myCon.GetSurveyTypeBySurveyTypeID(CInt(hfCurSurveyTypeID.Value))
            myCat.SurveyTypeNM = tbSurveyTypeNM.Text
            myCat.SurveyTypeComment = tbSurveyTypeComment.Text
            myCat.SurveyTypeShortNM = tbSurveyTypeShortNM.Text
            myCat.SurveyTypeDS = tbSurveyTypeDS.Text
        Else
            myCat = New SurveyTypeItem
            myCat.SurveyTypeNM = tbSurveyTypeNM.Text
            myCat.SurveyTypeComment = tbSurveyTypeComment.Text
            myCat.SurveyTypeShortNM = tbSurveyTypeShortNM.Text
            myCat.SurveyTypeDS = tbSurveyTypeDS.Text
            If CInt(hfParentSurveyTypeID.Value) = 0 Then
                myCat.ParentSurveyTypeID = 0
            Else
                myCat.ParentSurveyTypeID = CInt(ddlCategory.SelectedValue)
            End If
            myCat.ApplicationTypeID = CInt(ddlApplicationType.SelectedValue)
            myCat.SurveyTypeID = -1
        End If

        Dim results = myCon.PutSurveyType(myCat)

        ddlCategory.DataBind()

        cmd_CancelSurveyType_Click(Nothing, Nothing)
    End Sub
    Protected Sub cmd_CancelSurveyType_Click(sender As Object, e As EventArgs)
        EnableCatEditButtons(True)
        pnlQuestionCategory.Visible = False
        pnlQuestionList.Visible = True
    End Sub
    Protected Sub cmd_DeleteSurveyType_Click(sender As Object, e As EventArgs)
        Dim myCat = myCon.GetSurveyTypeBySurveyTypeID(CInt(hfCurSurveyTypeID.Value))
        myCon.DeleteSurveyType(myCat)
        ddlCategory.DataBind()
        ' ddlQuestionCategory.DataBind()
        cmd_CancelSurveyType_Click(Nothing, Nothing)
    End Sub


#End Region

    Protected Sub cmd_CreateQuestion_Click(sender As Object, e As EventArgs)
        myQuestion = New QuestionItem With {.QuestionID = -1}
        hfQuestionID.Value = -1
        ClearPageArguments(GetPageArgument("pid").Second.ToString)
        SetPageArgument("questionid", -1)
        SetPageArgument("action", "questionview")
        hfQuestionID.Value = "-1"
        hfQuestionAnswerID.Value = String.Empty
        Try
            myQuestion = myCon.GetQuestionByQuestionID(hfQuestionID.Value)
        Catch ex As Exception
            myQuestion = New QuestionItem
        End Try
        pnlQuestionAnswer.Visible = False
        pnlQuestionDetail.Visible = True
        ddlCategory.Enabled = True
        ddlApplicationType.Enabled = False
        ddlQuestionCategory.Enabled = False
        pnlQuestionList.Visible = False
        SetQuestion(-1)
        EnableCatEditButtons(False)
    End Sub
End Class
