Imports System.Web
Imports System.Text
Imports System.Web.UI.WebControls
Imports System.Xml.Serialization
Imports System.IO
Imports ControlOrigins.COUtility

<Serializable()> Public Class SurveyResponseItemBL
    Inherits SurveyResponseItem
    Private Const STR_DefaultResponse As String = "<p>{0}</p>"
    Private Const STR_DefaultResponseComment As String = "<p>{0}</p><blockquote>""{1}""</blockquote>"
    Private Const STR_DefaultReview As String = "<p>{0} by {1}</p>"
    Private Const STR_DefaultReviewComment As String = "<p>{0} by {1}</p><blockquote>""{4}""</blockquote>"
    Private Const STR_ResponseDIV As String = "<div class=""QuestionResponse""><h3>{3} ({1}-{2})</h3>{0}</div>"
    Public Const STR_SurveyResponseID As String = "CurrentSurveyResponseID"
    Public Const STR_CurrentStatusID As String = "CurrentStatusID"
    Public Const STR_CurrentQuestionGroupID As String = "CurrentQuestionGroupID"
    Public Const STR_NextQuestionGroupID As String = "NextQuestionGroupID"
    Public Const STR_PreviousQuestionGroupID As String = "PreviousQuestionGroupID"
    Public Const STR_QuestionAnswerName As String = "QAN-{0}-{1}-{2}"
    Public Const STR_QuestionAnswerComment As String = "QAC-{0}-{1}-{2}"
    Public Const STR_QuestionAnswerItem As String = "QAI-{0}-{1}-{2}-{3}"
    Public Const STR_QuestionAnswerReviewItem As String = "QARI-{0}-{1}-{2}-{3}"
    Public Const STR_SurveyQuestionAnswerID As String = "SQAI-{0}-{1}-{2}"
    Public Const STR_SurveyQuestionAnswerReviewID As String = "SQARI-{0}-{1}-{2}"
    'Private _currentQuestionGroupID As Integer
    'Public Property CurrentQuestionGroupID As Integer
    '    Get
    '        Return _currentQuestionGroupID
    '    End Get
    '    Set(value As Integer)
    '        If _currentQuestionGroupID = value Then
    '            Return
    '        End If
    '        _currentQuestionGroupID = value
    '    End Set
    'End Property
    Public ReadOnly Property AllQuestionsAnswered As Boolean
        Get
            Dim bReturn As Boolean = False
            If Survey.QuestionList.Count = NewAnswerList.Count Then
                bReturn = True
            Else
                bReturn = False
            End If
            Return bReturn
        End Get
    End Property
    Public Function IsValid() As Boolean
        Dim bReturn As Boolean
        If SurveyResponseID > 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        If Survey Is Nothing Then
            bReturn = False
        Else
            If Survey.QuestionList.Count < 1 Then
                bReturn = False
            End If
            If Survey.QuestionGroupList Is Nothing Then
                bReturn = False
            End If
        End If
        If SequenceList Is Nothing Then
            bReturn = False
        Else
            If SequenceList.Count < 1 Then
                bReturn = False
            End If
        End If
        Return bReturn
    End Function
    Public Shared Function GetHiddenFormField(ByVal Name As String, ByVal Value As String) As String
        Return String.Format("{2}<input type=""hidden"" name=""{0}"" value=""{1}"">{2}", Name, Value, vbCrLf)
    End Function
    Public Shared Function GetQuestionAnswerReviewItemID(ByRef SurveyResponseID As Integer, ByVal SequenceNumber As Integer, ByRef QuestionID As Integer, ByRef ReviewStatusID As Integer) As String
        Return String.Format(STR_QuestionAnswerReviewItem, SurveyResponseID, SequenceNumber, QuestionID, ReviewStatusID)
    End Function
    Public Shared Function GetQuestionAnswerItemID(ByRef SurveyResponseID As Integer, ByVal SequenceNumber As Integer, ByRef question As QuestionItem, ByRef qAnswer As QuestionAnswerItem) As String
        Return String.Format(STR_QuestionAnswerItem, SurveyResponseID, SequenceNumber, question.QuestionID, qAnswer.QuestionAnswerID)
    End Function
    Public Shared Function GetQuestionAnswerComment(ByRef SurveyResponseID As Integer, ByVal SequenceNumber As Integer, ByRef Question As QuestionItem) As String
        Return String.Format(STR_QuestionAnswerComment, SurveyResponseID, SequenceNumber, Question.QuestionID)
    End Function
    Public Shared Function GetQuestionAnswerComment(ByRef SurveyResponseID As Integer, ByVal SequenceNumber As Integer, ByRef QuestionID As Integer) As String
        Return String.Format(STR_QuestionAnswerComment, SurveyResponseID, SequenceNumber, QuestionID)
    End Function
    Public Shared Function GetQuestionName(ByRef SurveyresponseID As Integer, ByVal SequenceNumber As Integer, ByRef Question As QuestionItem) As String
        Return String.Format(STR_QuestionAnswerName, SurveyresponseID, SequenceNumber, Question.QuestionID)
    End Function
    Public Shared Function GetQuestionName(ByRef SurveyresponseID As Integer, ByVal SequenceNumber As Integer, ByRef QuestionID As Integer) As String
        Return String.Format(STR_QuestionAnswerName, SurveyresponseID, SequenceNumber, QuestionID)
    End Function
    Public Shared Function GetSurveyQuestionAnswerIDName(ByRef SurveyresponseID As Integer, ByVal SequenceNumber As Integer, ByRef QuestionID As Integer) As String
        Return String.Format(STR_SurveyQuestionAnswerID, SurveyresponseID, SequenceNumber, QuestionID)
    End Function
    Public Shared Function GetSurveyQuestionAnswerReviewIDName(ByRef SurveyresponseID As Integer, ByVal SequenceNumber As Integer, ByRef QuestionID As Integer) As String
        Return String.Format(STR_SurveyQuestionAnswerReviewID, SurveyresponseID, SequenceNumber, QuestionID)
    End Function
    Public Shared Function GetSurveyQuestionAnswer(ByRef Question As QuestionItem, ByVal IndexVal As Integer) As QuestionAnswerItem
        Return Question.QuestionAnswerItemList.Item(IndexVal)
    End Function
    Public Shared Function ResetSurveyResponseAnswerItem(ByRef QuestionReponse As SurveyResponseAnswerItem, ByVal AnswerDataType As String) As SurveyResponseAnswerItem
        QuestionReponse.AnswerQuantity = Nothing
        QuestionReponse.AnswerDate = Nothing
        QuestionReponse.AnswerComment = Nothing
        QuestionReponse.QuestionAnswerID = Nothing
        QuestionReponse.AnswerType = AnswerDataType
        Return QuestionReponse
    End Function
    Public Shared Function SaveXML(ByRef mySR As SurveyResponseItemBL) As String
        Try
            Dim myXML As New SurveyXmlDocument()
            If mySR Is Nothing Then
                Return String.Empty
            Else
                Dim oXS As XmlSerializer = New XmlSerializer(GetType(SurveyResponseItemBL))
                Using writer As New StringWriter()
                    oXS.Serialize(writer, mySR)
                    myXML.LoadXml(writer.ToString)
                End Using
                myXML.Save(String.Format("c:\SPSurvey\xml\{0}.xml", mySR.SurveyResponseNM))
            End If
        Catch ex As Exception
            Return "ERROR"
        End Try
        Return String.Empty
    End Function
    Public Function SaveXML() As String
        Try
            Dim myXML As New SurveyXmlDocument()
            If Me Is Nothing Then
                Return String.Empty
            Else
                Dim oXS As XmlSerializer = New XmlSerializer(GetType(SurveyResponseItemBL))
                Using writer As New StringWriter()
                    oXS.Serialize(writer, Me)
                    myXML.LoadXml(writer.ToString)
                End Using
                myXML.Save(String.Format("c:\SPSurvey\xml\{0}.xml", SurveyResponseNM))
            End If
        Catch ex As Exception
            Return "ERROR"
        End Try
        Return String.Empty
    End Function
    Private Shared Function GetScore(ByRef thisAnswer As SurveyResponseAnswerItem) As Decimal
        Return thisAnswer.QuestionValue * thisAnswer.QuestionAnswerValue
    End Function
    Private Function GetAnswerReviewHTML(ByRef thisReview As SurveyResponseAnswerReviewItem, ByVal AddModifyComments As Boolean) As String
        Dim bReturn As String = String.Empty
        For Each myStatus As SurveyReviewStatusItem In Survey.ReviewStatusList
            If thisReview.ReviewStatusID = myStatus.ReviewStatusID Then
                If thisReview.ReviewLevel = 2 Then
                    thisReview.ReviewerNM = "Admin"
                End If

                If AddModifyComments Then
                    If (thisReview.ModifiedComment.Trim = String.Empty) Then
                        bReturn = String.Format(STR_DefaultReview, myStatus.ReviewStatusNM, thisReview.ReviewerNM, thisReview.ModifiedDT.ToShortDateString, thisReview.ModifiedDT.ToShortTimeString)
                    Else
                        bReturn = String.Format(STR_DefaultReviewComment, myStatus.ReviewStatusNM, thisReview.ReviewerNM, thisReview.ModifiedDT.ToShortDateString, thisReview.ModifiedDT.ToShortTimeString, thisReview.ModifiedComment)
                    End If
                Else
                    bReturn = String.Format(STR_DefaultReview, myStatus.ReviewStatusNM, thisReview.ReviewerNM, thisReview.ModifiedDT.ToShortDateString, thisReview.ModifiedDT.ToShortTimeString)
                End If
                Exit For
            End If
        Next
        Return bReturn
    End Function
    Private Shared Function GetAnswerText(ByRef thisAnswer As SurveyResponseAnswerItem, ByVal AddModifyComments As Boolean) As String
        Dim sReturn As String = "GetAnserText()"
        Try
            Select Case thisAnswer.AnswerType
                Case "QuestionAnswerID"
                    If AddModifyComments Then

                        If (thisAnswer.AnswerComment Is Nothing) Then
                            sReturn = String.Format(STR_DefaultResponse, thisAnswer.QuestionAnswerNM)
                        ElseIf (thisAnswer.AnswerComment.Trim = String.Empty) Then
                            sReturn = String.Format(STR_DefaultResponse, thisAnswer.QuestionAnswerNM)
                        Else
                            sReturn = String.Format(STR_DefaultResponseComment, thisAnswer.QuestionAnswerNM, thisAnswer.AnswerComment)
                        End If
                    Else
                        sReturn = String.Format(STR_DefaultResponse, thisAnswer.QuestionAnswerNM)
                    End If
                Case "Quantity"
                    If AddModifyComments Then
                        sReturn = String.Format(STR_DefaultResponseComment, thisAnswer.AnswerQuantity.ToString, thisAnswer.AnswerComment)
                    Else
                        sReturn = String.Format(STR_DefaultResponse, thisAnswer.AnswerQuantity.ToString)
                    End If
                Case "Date"
                    If (thisAnswer.AnswerDate.HasValue) Then
                        If AddModifyComments Then
                            sReturn = String.Format(STR_DefaultResponseComment, thisAnswer.AnswerDate.Value.ToShortDateString(), thisAnswer.AnswerComment)
                        Else
                            sReturn = String.Format(STR_DefaultResponse, thisAnswer.AnswerDate.Value.ToShortDateString())
                        End If
                    Else
                        If AddModifyComments Then
                            sReturn = String.Format(STR_DefaultResponseComment, String.Empty, thisAnswer.AnswerComment)
                        Else
                            sReturn = String.Format(STR_DefaultResponse, String.Empty)
                        End If
                    End If
                Case Else
                    sReturn = String.Format(STR_DefaultResponseComment, thisAnswer.QuestionAnswerNM, thisAnswer.AnswerComment)
            End Select
        Catch ex As Exception
            sReturn = "GetAnswerTextException"
        End Try
        Return sReturn
    End Function
    Private Shared Function GetContentCell(ByVal CSSClass As String, ByVal Content As String) As TableCell
        Using mycell As New TableCell() With {.CssClass = CSSClass, .Width = New Unit(100, UnitType.Percentage)}
            mycell.Controls.Add(New Literal() With {.Text = Content})
            Return mycell
        End Using
    End Function
    Private Shared Function GetControlCell(ByVal CSSClass As String, ByVal Content As String) As TableCell
        Using mycell As New TableCell() With {.CssClass = CSSClass, .Width = New Unit(100, UnitType.Percentage), .Text = Content}
            Return mycell
        End Using
    End Function
    Public Shared Sub AddULEndTag(ByRef mySB As StringBuilder)
        mySB.Append("</ul>")
    End Sub
    Public Function GetSurveyResponseHiddenFieldsHTML() As String
        Dim strReturn As New StringBuilder
        strReturn.Append(GetHiddenFormField(STR_SurveyResponseID, CStr(SurveyResponseID)))
        strReturn.Append(GetHiddenFormField(STR_CurrentQuestionGroupID, CStr(CurrentQuestionGroupID)))
        strReturn.Append(GetHiddenFormField(STR_CurrentStatusID, CStr(StatusID)))
        Return strReturn.ToString()
    End Function
    Private Shared Function GetGroupHiddenFields(ByVal PreviousGroupID As Integer, ByVal NextGroupID As Integer) As String
        Dim strReturn As New StringBuilder
        strReturn.Append(GetHiddenFormField(STR_NextQuestionGroupID, CStr(NextGroupID)))
        strReturn.Append(GetHiddenFormField(STR_PreviousQuestionGroupID, CStr(PreviousGroupID)))
        Return strReturn.ToString()
    End Function
    Private Function BuildGroupHeaderTableRow(ByVal mySurveyQuestionGroupID As Integer) As TableRow
        Dim myTableRow As New TableRow With {.CssClass = "SurveyQuestionGroupHeaderRow"}
        If Not (Survey.QuestionGroupList Is Nothing) AndAlso Survey.QuestionGroupList.Count > 0 Then
            For Each myGroup As QuestionGroupItem In Survey.QuestionGroupList
                If myGroup.QuestionGroupID = mySurveyQuestionGroupID Then
                    myTableRow.Cells.Add(New TableCell With {.ColumnSpan = 1 + SequenceList.Count, .CssClass = "SurveyQuestionGroupHeaderCell", .Text = myGroup.QuestionGroupHeader})
                    Exit For
                End If
            Next
        Else
            myTableRow.Cells.Add(New TableCell With {.ColumnSpan = 1 + SequenceList.Count, .CssClass = "SurveyQuestionGroupHeaderCell", .Text = String.Format("Group List is Empty Header-GroupID={0}", mySurveyQuestionGroupID)})
        End If
        Return myTableRow
    End Function
    Private Function BuildSurveyHeaderTableRow(ByVal myPreviousGroupID As Integer, ByVal myNextGroupID As Integer) As TableRow
        Dim myTableRow As New TableHeaderRow With {.CssClass = "SurveyHeaderRow"}
        If SequenceList.Count > 1 Then
            myTableRow.CssClass = "SurveyHeaderRowGrid"
            myTableRow.Cells.Add(New TableHeaderCell With {.ColumnSpan = 1, .CssClass = "SurveyHeaderCellGrid", .Wrap = False, .Text = GetGroupHiddenFields(myPreviousGroupID, myNextGroupID), .Width = New Unit(20, UnitType.Percentage)})

            For Each mySeq In SequenceList
                myTableRow.Cells.Add(New TableHeaderCell With {.ColumnSpan = 1, .CssClass = "SurveyHeaderCellGrid", .Text = mySeq.SequenceText, .Width = New Unit(GetSequenceWidth(), UnitType.Percentage)})
            Next
        Else
            myTableRow.Cells.Add(New TableHeaderCell With {.ColumnSpan = 1 + SequenceList.Count, .CssClass = "SurveyHeaderCell", .Text = GetGroupHiddenFields(myPreviousGroupID, myNextGroupID)})
        End If
        Return myTableRow
    End Function
    Private Function BuildGroupFooterTableRow(ByVal mySurveyQuestionGroupID As Integer) As TableRow
        Dim myTableRow As New TableRow With {.CssClass = "SurveyQuestionGroupFooterRow"}
        If Not (Survey.QuestionGroupList Is Nothing) AndAlso Survey.QuestionGroupList.Count > 0 Then
            For Each myGroup In Survey.QuestionGroupList
                If myGroup.QuestionGroupID = mySurveyQuestionGroupID Then
                    myTableRow.Cells.Add(New TableCell With {.ColumnSpan = 1 + SequenceList.Count, .CssClass = "SurveyQuestionGroupFooterCell", .Text = myGroup.QuestionGroupFooter})
                    Exit For
                End If
            Next
        Else
            myTableRow.Cells.Add(New TableCell With {.ColumnSpan = 1 + SequenceList.Count, .CssClass = "SurveyQuestionGroupFooterCell", .Text = String.Format("Group List is Empty Footer-GroupID={0}", mySurveyQuestionGroupID)})
        End If
        Return myTableRow
    End Function
    Public Function GetSurveyHeaderHTML(ByVal CurrentLocalPath As String, ByVal cssClass As String) As String
        Dim myTabs As New StringBuilder
        If IsValid() Then
            If CurrentQuestionGroupID < 1 Then
                CurrentQuestionGroupID = Survey.QuestionGroupList(0).QuestionGroupID
            End If
            ' myTabs.Append(String.Format("<h2>{0}</h2>", SurveyResponseNM, vbCrLf))
            If Survey.UseSurveyGroupsFL Then
                myTabs.Append(GetSurveyNavigationHTML(CurrentLocalPath, cssClass))
            End If
        End If
        Return myTabs.ToString()
    End Function
    Private Function GetGroupURL(ByVal CurrentLocalPath As String, ByVal myGroup As QuestionGroupItem) As String
        Return String.Format("{2}?SurveyResponseID={0}&QuestionGroupID={1}", SurveyResponseID, myGroup.QuestionGroupID, Replace(Replace(CurrentLocalPath, "~/", String.Empty), "/default.aspx", String.Empty))
    End Function
    Private Function GetSurveyNavigationHTML(ByVal CurrentLocalPath As String, ByVal cssClass As String) As String
        Dim myTabs As New StringBuilder(vbCrLf)
        myTabs.Append(String.Format("{0}<ul class=""{1}"">", vbCrLf, cssClass))
        For Each myGroup As QuestionGroupItem In Survey.QuestionGroupList
            If myGroup.QuestionGroupID = CurrentQuestionGroupID Then
                myTabs.Append(String.Format("{3}<li class=""active""><a href=""{0}"">{2}</a></li>", GetGroupURL(CurrentLocalPath, myGroup), myGroup.QuestionGroupID, myGroup.QuestionGroupNM, vbCrLf))
            Else
                myTabs.Append(String.Format("{3}<li ><a href=""{0}"">{2}</a></li>", GetGroupURL(CurrentLocalPath, myGroup), myGroup.QuestionGroupID, myGroup.QuestionGroupNM, vbCrLf))
            End If
        Next
        myTabs.Append(String.Format("{0}</ul>{0}", vbCrLf))
        Return myTabs.ToString
    End Function

    Private Function BuildQuestionAnswerForm(ByRef myQuestion As QuestionItem, ByRef curSequence As SurveyResponseSequenceItem) As String
        Dim myReturn As StringBuilder = New StringBuilder

        Dim myCurrentAnswers As New SurveyResponseAnswerListBL(AnswerList)

        If myQuestion.QuestionAnswerItemList.Count = 0 Then
            ApplicationLogging.ErrorLog(String.Format("Question with No Answers Defined: QuestionShortNM=", myQuestion.QuestionShortNM), "SurveyResponseItemBL.BuildQuestionAnswerForm")
        End If

        Dim myFQ As New SurveyResponseQuestionForm With {.SurveyResponseID = SurveyResponseID,
                                                         .StatusID = StatusID,
                                                         .Question = myQuestion,
                                                         .CurrentResponseList = myCurrentAnswers.FindAnswersByQuestionID(myQuestion.QuestionID, curSequence.SequenceNumber),
                                                         .QuestionIndex = myQuestion.SurveyDisplayOrder,
                                                         .SequenceNumber = curSequence.SequenceNumber}
        If curSequence.SequenceNumber = 1 Then
            myReturn.Append(GetQuestionFormHeader(myQuestion))
        End If
        myReturn.Append(myFQ.GetQuestionAnswerForm(curSequence.SequenceNumber, True, myQuestion.SurveyDisplayOrder))

        Return myReturn.ToString
    End Function
    Private Function DisplayQuestion(ByVal myQuestion As QuestionItem) As Boolean
        Dim bShowQuestion As Boolean
        If StatusID = 1 Then
            bShowQuestion = True
        Else
            bShowQuestion = True
            For Each mySRA As SurveyResponseAnswerItem In AnswerList
                If mySRA.QuestionID = myQuestion.QuestionID Then
                    For Each myQuestionAnswer As QuestionAnswerItem In myQuestion.QuestionAnswerItemList
                        If myQuestionAnswer.QuestionAnswerID = mySRA.QuestionAnswerID Then
                            If myQuestionAnswer.QuestionAnswerCommentFL Then
                                If mySRA.AnswerReviewList.Count = 0 Then
                                    If StatusID < 3 Then
                                        bShowQuestion = False
                                    End If
                                Else
                                    For Each myQAReview In mySRA.AnswerReviewList
                                        Select Case StatusID
                                            Case 1
                                                bShowQuestion = True
                                            Case 2
                                                If myQAReview.ApprovedFL Then
                                                    bShowQuestion = False
                                                Else
                                                    bShowQuestion = True
                                                End If
                                            Case 3
                                                If myQAReview.ApprovedFL And myQAReview.ReviewLevel = 1 Then
                                                    bShowQuestion = False
                                                End If
                                            Case 4
                                                If myQAReview.ApprovedFL And myQAReview.ReviewLevel = 2 Then
                                                    bShowQuestion = False
                                                End If
                                            Case Else
                                                bShowQuestion = True
                                        End Select
                                    Next
                                End If
                            Else
                                bShowQuestion = False
                            End If
                        End If
                    Next
                    Exit For
                End If
            Next
        End If
        Return bShowQuestion
    End Function
    Private Function GetSequenceWidth() As Double
        Dim SequenceWidth As Double
        Try
            SequenceWidth = Math.Round(80 / SequenceList.Count, 2)
        Catch ex As Exception
            SequenceWidth = 10
        End Try
        Return SequenceWidth
    End Function
    Private Sub BuildGroupQuestionsForm(ByRef myTableHeaderRow As TableRow, ByRef myTable As Table, ByVal reqQuestionGroupID As Integer)
        Dim myReturn As StringBuilder = New StringBuilder
        Dim QuestionIndex As Integer = 1
        Dim SequenceIndex As Integer = 1

        Dim myGroupQuestionList As List(Of QuestionItem) = QuestionListUtility.FindQuestionByQuestionGroupID(reqQuestionGroupID, Survey.QuestionList)

        For Each myQuestion As QuestionItem In myGroupQuestionList
            Try
                If myQuestion.QuestionAnswerItemList.Count = 0 Then
                    AppLog.ErrorLog(String.Format("Question with No Answers Defined: QuestionShortNM=", myQuestion.QuestionShortNM), "SurveyResponseItemBL.BuildGroupQuestionsForm")
                End If

                If DisplayQuestion(myQuestion) Then
                    Dim myAnswerRow As New TableRow() With {.CssClass = "SurveyQuestionAnswerRow"}

                    ' Setup Initial Group Header
                    If QuestionIndex = 1 Then
                        myTable.Rows.Add(BuildGroupHeaderTableRow(reqQuestionGroupID))
                        myTable.Rows.Add(myTableHeaderRow)
                    End If

                    ' Loop all Sequences 
                    If SequenceList.Count > 1 Then
                        myAnswerRow.Cells.Add(New TableCell With {.CssClass = "SurveyGridRowLabel", .VerticalAlign = VerticalAlign.Top, .Wrap = True, .Text = GetQuestionFormHeader(myQuestion), .Width = New Unit(20, UnitType.Percentage)})
                        For Each curSequence As SurveyResponseSequenceItem In SequenceList
                            Dim myCurrentAnswers As New SurveyResponseAnswerListBL(AnswerList)
                            myReturn.Length = 0
                            Dim myFQ As New SurveyResponseQuestionForm With {.SurveyResponseID = SurveyResponseID,
                                                                             .StatusID = StatusID,
                                                                             .Question = myQuestion,
                                                                             .CurrentResponseList = myCurrentAnswers.FindAnswersByQuestionID(myQuestion.QuestionID, curSequence.SequenceNumber),
                                                                             .QuestionIndex = myQuestion.SurveyDisplayOrder,
                                                                             .SequenceNumber = curSequence.SequenceNumber}
                            myAnswerRow.Cells.Add(New TableCell With {.CssClass = "SurveyQuestionAnswerCell",
                                                                      .Text = myFQ.GetQuestionAnswerForm(curSequence.SequenceNumber, False, CShort(((SequenceIndex - 1) * (Survey.QuestionList.Count)) + myQuestion.SurveyDisplayOrder)),
                                                                      .Width = New Unit(GetSequenceWidth(), UnitType.Percentage)})
                            SequenceIndex += 1
                        Next
                        SequenceIndex = 1
                        myTable.Rows.Add(myAnswerRow)
                    Else
                        For Each curSequence As SurveyResponseSequenceItem In SequenceList
                            myAnswerRow.Cells.Add(GetControlCell("SurveyQuestionAnswerCell", BuildQuestionAnswerForm(myQuestion, curSequence)))
                        Next
                        myTable.Rows.Add(myAnswerRow)
                    End If

                End If
                QuestionIndex += 1
            Catch ex As Exception
                AppLog.ErrorLog(ex.ToString, "SurveyResponseItemBL.BuildGroupQuestionsForm")
            End Try


        Next
        myTable.Rows.Add(BuildGroupFooterTableRow(reqQuestionGroupID))
    End Sub
    Private Function GetGroupFormAsTable() As Table
        Dim myPreviousGroupID As Integer
        Dim myNextGroupID As Integer
        Dim bFoundGroup As Boolean = False

        Dim myTable As New Table With {.CssClass = "SurveyFormTable", .GridLines = GridLines.None, .Width = New Unit(100, UnitType.Percentage)}
        Using myTableHeaderRow As New TableRow() With {.CssClass = "SurveyFormTableHeaderRow"}
            ' Setup Header Row
            myTableHeaderRow.Cells.Add(GetContentCell("SurveyFormHeader", ""))
            If CurrentQuestionGroupID = 0 Then
                If Survey.QuestionGroupList Is Nothing Then
                    ' Error No questions with QuestionGroupID = 0 and No QuestionGroups
                    App.AddMessage("SurveyResponseItemBL.GetGroupFormAsTable", String.Format("No Questions for SurveyResponseID-{0}", SurveyResponseID))
                Else
                    ' Logic to Auto Select First Group when all questions are in groups there is nothing to show with 0 so select first group 
                    If Survey.QuestionGroupList.Count = 1 Then
                        CurrentQuestionGroupID = Survey.QuestionGroupList.Item(0).QuestionGroupID
                    ElseIf Survey.QuestionGroupList.Count > 1 Then

                    End If
                End If
            End If

            If Not (Me Is Nothing) Then
                If Not (Survey.QuestionList Is Nothing) Then
                    If (Survey.QuestionList.Count > 0) Then
                        ' Loop all Groups
                        Dim GroupNumber As Integer = 1
                        For Each myGroup As QuestionGroupItem In Survey.QuestionGroupList
                            If myGroup.QuestionGroupID = CurrentQuestionGroupID Then

                                BuildGroupQuestionsForm(myTableHeaderRow, myTable, myGroup.QuestionGroupID)

                                If Survey.QuestionGroupList.Count > GroupNumber Then
                                    myNextGroupID = Survey.QuestionGroupList.Item(GroupNumber).QuestionGroupID
                                Else
                                    myNextGroupID = GlobalApplicationProperties.INT_SurveyCompleteGroupID
                                End If
                                If GroupNumber > 1 Then
                                    myPreviousGroupID = Survey.QuestionGroupList.Item(GroupNumber - 2).QuestionGroupID
                                Else
                                    myPreviousGroupID = GlobalApplicationProperties.INT_SurveyCompleteGroupID
                                End If
                                bFoundGroup = True
                                Exit For
                            End If
                            GroupNumber = GroupNumber + 1
                        Next
                        myTable.Rows.AddAt(0, BuildSurveyHeaderTableRow(myPreviousGroupID, myNextGroupID))
                        If Not bFoundGroup Then
                            App.AddMessage("An Error has Occurred, Question Group Not Found")
                        End If
                    Else
                        App.AddMessage("An Error has Occurred, Survey Type Question Count is zero")
                    End If
                Else
                    App.AddMessage("An Error has Occurred, Survey Type has no questions")
                End If
            Else
                App.AddMessage("An Error has Occurred, System can not determine Survey Type")
            End If
        End Using
        Return myTable
    End Function
    Public Sub SetNextPreviousGroupID(ByRef myPreviousGroupID As Integer, ByRef myNextGroupID As Integer, ByVal GroupNumber As Integer)
        If Survey.QuestionGroupList.Count > GroupNumber Then
            myNextGroupID = Survey.QuestionGroupList.Item(GroupNumber).QuestionGroupID
        Else
            myNextGroupID = GlobalApplicationProperties.INT_SurveyCompleteGroupID
        End If
        If GroupNumber > 1 Then
            myPreviousGroupID = Survey.QuestionGroupList.Item(GroupNumber - 2).QuestionGroupID
        Else
            myPreviousGroupID = GlobalApplicationProperties.INT_SurveyCompleteGroupID
        End If
    End Sub
    Private Function GetAllGroupsFormAsTable() As Table
        Dim myPreviousGroupID As Integer
        Dim myNextGroupID As Integer
        Dim bFoundGroup As Boolean = False

        Dim myTable As New Table With {.CssClass = "SurveyFormTable", .GridLines = GridLines.None, .Width = New Unit(100, UnitType.Percentage)}
        Using myTableHeaderRow As New TableRow() With {.CssClass = "SurveyFormTableHeaderRow"}
            ' Setup Header Row
            myTableHeaderRow.Cells.Add(GetContentCell("SurveyFormHeader", ""))
            If CurrentQuestionGroupID = 0 Then
                If Survey.QuestionGroupList Is Nothing Then
                    ' Error No questions with QuestionGroupID = 0 and No QuestionGroups
                    App.AddMessage("BuildForm", String.Format("No Questions for SurveyResponseID-{0}", SurveyResponseID))
                Else
                    ' Logic to Auto Select First Group when all questions are in groups there is nothing to show with 0 so select first group 
                    If Survey.QuestionGroupList.Count > 0 Then
                        CurrentQuestionGroupID = Survey.QuestionGroupList.Item(0).QuestionGroupID
                    End If
                End If
            End If

            If Not (Me Is Nothing) Then
                If Not (Survey.QuestionList Is Nothing) Then
                    If (Survey.QuestionList.Count > 0) Then
                        ' Loop all Groups
                        Dim GroupNumber As Integer = 1
                        For Each myGroup As QuestionGroupItem In Survey.QuestionGroupList
                            BuildGroupQuestionsForm(myTableHeaderRow, myTable, myGroup.QuestionGroupID)
                            SetNextPreviousGroupID(myPreviousGroupID, myNextGroupID, GroupNumber)
                            GroupNumber = GroupNumber + 1
                            bFoundGroup = True
                        Next
                        myTable.Rows.AddAt(0, BuildSurveyHeaderTableRow(myPreviousGroupID, myNextGroupID))
                        If Not bFoundGroup Then
                            App.AddMessage("An Error has Occurred, Question Group Not Found")
                        End If
                    Else
                        App.AddMessage("An Error has Occurred, Survey Type Question Count is zero")
                    End If
                Else
                    App.AddMessage("An Error has Occurred, Survey Type has no questions")
                End If
            Else
                App.AddMessage("An Error has Occurred, System can not determine Survey Type")
            End If
        End Using
        Return myTable
    End Function
    Private Sub GetFormQuestionAnswers(ByRef Request As HttpRequest, ByRef myQuestion As QuestionItem, ByVal SequenceNumber As Integer)
        ' Dim qaAnswerItem As String
        Dim qaComment As String = Request.Form(GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, myQuestion))
        Dim qaValues As New List(Of String)
        If myQuestion.QuestionAnswerItemList.Count = 0 Then
            ' Error answers should already be filled
            AppLog.ErrorLog("Question is missing Answers", "SurveyResponseItemBL.GetFormQuestionAnswers")
        End If

        Dim myKey = GetQuestionName(SurveyResponseID, SequenceNumber, myQuestion)

        If myQuestion.QuestionTypeCD = "CBL" Then
            myKey = myKey & "[]"
        End If

        Dim qaAnswerItem = Request.Form(myKey)

        If Not String.IsNullOrEmpty(qaAnswerItem) Then
            If myQuestion.QuestionTypeCD = "CBL" Then
                qaValues.AddRange(Split(qaAnswerItem, ","))
            Else
                qaValues.Add(qaAnswerItem)
            End If
        End If
        If qaValues.Count > 0 Then
            NewAnswerList.Add(New SurveyResponseAnswerItem With {.AnswerComment = qaComment,
                                                                 .ModifiedComment = String.Empty,
                                                                 .QuestionID = myQuestion.QuestionID,
                                                                 .SequenceNumber = SequenceNumber,
                                                                 .SurveyResponseID = SurveyResponseID,
                                                                 .ResponseList = qaValues})
        End If
    End Sub
    Private Sub GetFormQuestionReviewAnswers(ByRef Request As HttpRequest,
                                             ByRef myQuestion As QuestionItem,
                                             ByVal SequenceNumber As Integer,
                                             ByRef curUser As ApplicationUserRoleItem,
                                             ByRef ReviewStatusList As List(Of SurveyReviewStatusItem))
        Dim qaAnswerItem As String
        Dim sqaID As String
        Dim sqarID As String
        Dim qaComment As String = Request.Form(GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, myQuestion))
        If myQuestion.QuestionAnswerItemList.Count = 0 Then
            AppLog.ErrorLog("Question is missing Answers", "SurveyResponseItemBL.GetFormQuestionReviewAnswers")
        End If
        qaAnswerItem = Request.Form(GetQuestionName(SurveyResponseID, SequenceNumber, myQuestion))
        sqaID = Request.Form(GetSurveyQuestionAnswerIDName(SurveyResponseID, SequenceNumber, myQuestion.QuestionID))
        sqarID = Request.Form(GetSurveyQuestionAnswerReviewIDName(SurveyResponseID, SequenceNumber, myQuestion.QuestionID))
        If Not IsNumeric(sqarID) Then
            sqarID = CStr(0)
        End If

        If Not String.IsNullOrEmpty(qaAnswerItem) AndAlso IsNumeric(qaAnswerItem) Then
            For Each myReview In ReviewStatusList
                If myReview.ReviewStatusID = AppUtility.GetDBInteger(qaAnswerItem) Then
                    If Not myReview.CommentFL Then
                        qaComment = String.Empty
                    End If
                    Exit For
                End If
            Next
            If NewAnswerReviewList Is Nothing Then
                AppLog.ErrorLog("NewAnswerReviewList is Nothing", "SurveyResponseItemBL.GetFormQuestionReviewAnswers")
            Else
                For Each myReview As SurveyReviewStatusItem In ReviewStatusList
                    If myReview.ReviewStatusID = AppUtility.GetDBInteger(qaAnswerItem) Then
                        NewAnswerReviewList.Add(New SurveyResponseAnswerReviewItem With {.SurveyResponseAnswerReviewID = AppUtility.GetDBInteger(sqarID),
                                                                                         .ApplicationUserRoleID = curUser.ApplicationUserRoleID,
                                                                                         .ModifiedID = curUser.ApplicationUserID,
                                                                                         .ReviewLevel = curUser.ReviewLevel,
                                                                                         .SurveyAnswerID = AppUtility.GetDBInteger(sqaID),
                                                                                         .ModifiedComment = qaComment,
                                                                                         .ReviewStatusID = myReview.ReviewStatusID,
                                                                                         .ApprovedFL = myReview.ApprovedFL,
                                                                                         .ModifiedDT = Now()})
                        Exit For
                    End If
                Next

            End If
        End If
    End Sub
    Public Sub GetFormAnswers(ByVal Request As HttpRequest)
        For Each myQuestion In Survey.QuestionList
            For Each Sequence In SequenceList
                GetFormQuestionAnswers(Request, myQuestion, Sequence.SequenceNumber)
            Next
        Next
    End Sub
    Public Sub GetFormReviewAnswer(ByVal Request As HttpRequest,
                                   ByRef curUser As ApplicationUserRoleItem,
                                   ByRef ReviewStatusList As List(Of SurveyReviewStatusItem))
        For Each myQuestion In Survey.QuestionList
            For Each Sequence In SequenceList
                GetFormQuestionReviewAnswers(Request,
                                             myQuestion,
                                             Sequence.SequenceNumber,
                                             curUser,
                                             ReviewStatusList)
            Next
        Next
    End Sub
    Public Function GetViewOnlyForm() As String
        Dim mySB As StringBuilder = New StringBuilder(String.Empty)
        If IsValid() Then
            '  RefreshAnswers()
            Dim QuestionIndex As Integer = 1
            For Each myGroup As QuestionGroupItem In Survey.QuestionGroupList
                QuestionIndex = 1
                If IsGroupRequired(myGroup) Then
                    mySB.Append(String.Format("<h3>{0}</h3>", myGroup.QuestionGroupNM))
                    For Each myQuestion As QuestionItem In Survey.QuestionList
                        If myQuestion.QuestionGroupMember.QuestionGroupID = myGroup.QuestionGroupID Then
                            mySB.Append(GetQuestionDisplay(myQuestion))
                            QuestionIndex += 1
                        End If
                    Next
                End If
            Next
            mySB.Append(GetSurveyResponseHistory())
            Return mySB.ToString()
        Else
            Return String.Empty
        End If
    End Function
    Public Function IsGroupRequired(ByVal myGroup As QuestionGroupItem) As Boolean
        If String.IsNullOrEmpty(myGroup.DependentQuestionGroupID.ToString()) Then
            Return True
        Else
            Dim dScore As Decimal = CType((From gqi In (From ai In AnswerList
                                                        Join q In Survey.QuestionList On ai.QuestionID Equals q.QuestionID
                                                        Where q.QuestionGroupMember.QuestionGroupID = myGroup.DependentQuestionGroupID
                                                        Select ai).ToList()
                                                    Select gqi).Sum(Function(i) i.QuestionAnswerValue * i.QuestionValue), Decimal)
            If dScore >= myGroup.DependentMinScore And dScore <= myGroup.DependentMaxScore Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
    Public Function GetFormAsTable(ByRef ReviewStatusList As List(Of SurveyReviewStatusItem)) As Table
        If CurrentQuestionGroupID = GlobalApplicationProperties.INT_SurveyCompleteGroupID Then
            App.AddMessage("The data entry is complete")
        Else
            If IsValid() Then
                ' RefreshAnswers()
                If Not Me Is Nothing Then
                    If Not Survey.QuestionList Is Nothing Then
                        Select Case StatusID
                            Case 1
                                If Survey.UseSurveyGroupsFL Then
                                    Return GetGroupFormAsTable()
                                Else
                                    Return GetAllGroupsFormAsTable()
                                End If
                            Case 2
                                If Survey.UseSurveyGroupsFL Then
                                    Return GetGroupFormAsTable()
                                Else
                                    Return GetAllGroupsFormAsTable()
                                End If
                            Case 3
                                Return GetSupervisorReviewFormAsTable(ReviewStatusList)
                            Case 4
                                Return GetAdminReviewFormAsTable(ReviewStatusList)
                            Case 5
                                If Survey.UseSurveyGroupsFL Then
                                    Return GetGroupFormAsTable()
                                Else
                                    Return GetAllGroupsFormAsTable()
                                End If
                                '                                Return GetAllQuestionsAsTable()
                            Case 6
                                If Survey.UseSurveyGroupsFL Then
                                    Return GetGroupFormAsTable()
                                Else
                                    Return GetAllGroupsFormAsTable()
                                End If
                                ' Return GetAllQuestionsAsTable()
                            Case Else
                                If Survey.UseSurveyGroupsFL Then
                                    Return GetGroupFormAsTable()
                                Else
                                    Return GetAllGroupsFormAsTable()
                                End If
                                '                                Return GetAllQuestionsAsTable()
                        End Select
                    End If
                End If
            End If
        End If
        Using ErrorTable As New Table()
            Dim ErrorRow As New TableRow()
            Dim ErrorCell As New TableCell() With {.Text = "Survey Response Complete"}
            ErrorRow.Cells.Add(ErrorCell)
            ErrorTable.Rows.Add(ErrorRow)
            Return ErrorTable
        End Using
    End Function
    Private Function GetSurveyResponseAnswerReviewDIV(ByVal myReview As SurveyResponseAnswerReviewItem) As String
        If myReview.ReviewLevel = 1 Then
            Return (String.Format(STR_ResponseDIV, GetAnswerReviewHTML(myReview, True), myReview.ModifiedDT.ToShortDateString, myReview.ModifiedDT.ToShortTimeString, "Supervisor Review"))
        Else
            Return (String.Format(STR_ResponseDIV, GetAnswerReviewHTML(myReview, True), myReview.ModifiedDT.ToShortDateString, myReview.ModifiedDT.ToShortTimeString, "Admin Review"))
        End If
    End Function
    Private Shared Function GetSurveyResponseAnswerDIV(ByVal myAnswer As SurveyResponseAnswerItem) As String
        ' Return (String.Format(STR_ResponseDIV, GetAnswerText(myAnswer, True), myAnswer.ModifiedDT.ToShortDateString, GetScore(myAnswer), "Response"))
        ' Return (String.Format("<div class=""QuestionResponse""><h3>{3} ({2})</h3>{0}</div>", GetAnswerText(myAnswer, True), myAnswer.ModifiedDT.ToShortDateString, GetScore(myAnswer), "Score"))
        Return (String.Format("<div class=""QuestionResponse"">{0}</div>", GetAnswerText(myAnswer, True)))
    End Function
    Private Function GetQuestionFormHeader(ByRef myQuestion As QuestionItem) As String
        Dim mySB As New StringBuilder()
        Dim myScore As Decimal
        Dim bFirst As Boolean = True
        Dim myCurrentAnswers As New SurveyResponseAnswerListBL(AnswerList)
        If SequenceList.Count > 1 Then
            mySB.Append(String.Format("<div class=""Question"">{1}. &nbsp;{0}<span>{2}</span></div>", myQuestion.QuestionNM, myQuestion.SurveyDisplayOrder, myQuestion.QuestionDS))
        Else
            If myCurrentAnswers.FindAnswersByQuestionID(myQuestion.QuestionID, 1).Count = 0 Or StatusID < 2 Then
                mySB.Append(String.Format("<div class=""Question""><p>{1}. &nbsp;{0}</p></div>", myQuestion.QuestionNM, myQuestion.SurveyDisplayOrder))
                If Not String.IsNullOrEmpty(myQuestion.QuestionDS) Then
                    mySB.Append(String.Format("<div class=""QuestionDescription"">{0}</div>", AppUtility.ConvertRichTextToHTML(myQuestion.QuestionDS)))
                End If
            Else
                mySB.Append(String.Format("<div class=""QuestionReview""><p>{1}. &nbsp;{0}</p></div>", myQuestion.QuestionNM, myQuestion.SurveyDisplayOrder))
                For Each myAnswer As SurveyResponseAnswerItem In myCurrentAnswers.FindAnswersByQuestionID(myQuestion.QuestionID, 1)
                    mySB.Append(GetSurveyResponseAnswerDIV(myAnswer))
                    myScore = +GetScore(myAnswer)
                    For Each myReview As SurveyResponseAnswerReviewItem In myAnswer.AnswerReviewList
                        mySB.Append(GetSurveyResponseAnswerReviewDIV(myReview))
                    Next
                Next
            End If
        End If
        Return mySB.ToString()
    End Function
    Private Function GetQuestionDisplay(ByRef myQuestion As QuestionItem) As String
        Dim mySB As New StringBuilder()
        Dim myScore As Decimal
        Dim myCurrentAnswers As New SurveyResponseAnswerListBL(AnswerList)
        If SequenceList.Count > 1 Then
            For Each mySequence As SurveyResponseSequenceItem In SequenceList
                For Each myAnswer As SurveyResponseAnswerItem In myCurrentAnswers.FindAnswersByQuestionID(myQuestion.QuestionID, AppUtility.GetDBInteger(mySequence.SequenceNumber))
                    mySB.Append(String.Format("{0}<br/>", GetAnswerText(myAnswer, False)))
                Next
            Next
        Else
            If myCurrentAnswers.FindAnswersByQuestionID(myQuestion.QuestionID, 1).Count = 0 Then
                mySB.Append(String.Format("<div class=""Question""><p>{1}. &nbsp;{0}</p></div>", myQuestion.QuestionNM, myQuestion.SurveyDisplayOrder))
            Else
                mySB.Append(String.Format("<div class=""QuestionReview""><p>{1}. &nbsp;{0}</p></div>", myQuestion.QuestionNM, myQuestion.SurveyDisplayOrder))
                For Each myAnswer As SurveyResponseAnswerItem In myCurrentAnswers.FindAnswersByQuestionID(myQuestion.QuestionID, 1)

                    mySB.Append(GetSurveyResponseAnswerDIV(myAnswer))

                    myScore = +GetScore(myAnswer)
                    For Each myReview As SurveyResponseAnswerReviewItem In myAnswer.AnswerReviewList
                        mySB.Append(GetSurveyResponseAnswerReviewDIV(myReview))
                    Next
                Next
            End If
        End If
        Return mySB.ToString()
    End Function
    Private Function GetQuestionRow(ByRef myQuestion As QuestionItem) As TableRow
        Using myTableRow As New TableRow() With {.CssClass = "SurveyQuestionRow"}
            Dim mySB As New StringBuilder()
            Dim myScore As Decimal
            Dim myCurrentAnswers As New SurveyResponseAnswerListBL(AnswerList)
            If SequenceList.Count > 1 Then
                For Each mySequence As SurveyResponseSequenceItem In SequenceList
                    For Each myAnswer As SurveyResponseAnswerItem In myCurrentAnswers.FindAnswersByQuestionID(myQuestion.QuestionID, AppUtility.GetDBInteger(mySequence.SequenceNumber))
                        mySB.Append(String.Format("{0}<br/>", GetAnswerText(myAnswer, False)))
                    Next
                    If CDbl(mySequence.SequenceNumber) = 1 Then
                        myTableRow.Cells.Add(New TableCell() With {.Wrap = True, .ColumnSpan = 1, .Text = String.Format("{1}. &nbsp;{0}", myQuestion.QuestionNM, myQuestion.SurveyDisplayOrder, myQuestion.QuestionGroupMember.QuestionWeight), .VerticalAlign = VerticalAlign.Top})
                    End If
                    myTableRow.Cells.Add(New TableCell() With {.ColumnSpan = 1, .Text = String.Format("{0}", mySB.ToString)})
                    mySB.Length = 0
                Next
            Else
                If myCurrentAnswers.FindAnswersByQuestionID(myQuestion.QuestionID, 1).Count = 0 Then
                    mySB.Append(String.Format("<div class=""Question""><p>{1}. &nbsp;{0}</p></div>", myQuestion.QuestionNM, myQuestion.SurveyDisplayOrder))
                Else
                    mySB.Append(String.Format("<div class=""QuestionReview""><p>{1}. &nbsp;{0}</p></div>", myQuestion.QuestionNM, myQuestion.SurveyDisplayOrder))
                    For Each myAnswer As SurveyResponseAnswerItem In myCurrentAnswers.FindAnswersByQuestionID(myQuestion.QuestionID, 1)
                        mySB.Append(GetSurveyResponseAnswerDIV(myAnswer))
                        myScore = +GetScore(myAnswer)
                        For Each myReview As SurveyResponseAnswerReviewItem In myAnswer.AnswerReviewList
                            mySB.Append(GetSurveyResponseAnswerReviewDIV(myReview))
                        Next
                    Next
                    myTableRow.Cells.Add(GetControlCell("SurveyQuestionCell", mySB.ToString))
                End If
            End If
            Return myTableRow
        End Using
    End Function
    Private Function GetAllQuestionsAsTable() As Table
        Dim myTable As New Table With {.CssClass = "SurveyFormTable", .GridLines = GridLines.None, .Width = New Unit(100, UnitType.Percentage)}
        Dim QuestionIndex As Integer = 1
        For Each myQuestion As QuestionItem In Survey.QuestionList
            Try
                myTable.Rows.Add(GetQuestionRow(myQuestion))
                QuestionIndex += 1
            Catch ex As Exception
               AppLog.ErrorLog(ex.ToString, "SurveyResponseItemBL.GetAllQuestionsAsTable")
            End Try
        Next
        myTable.Rows.Add(GetSurveyResponseHistoryRow())

        Return myTable
    End Function
    Private Function GetSupervisorReviewFormAsTable(ByRef ReviewStatusList As List(Of SurveyReviewStatusItem)) As Table
        Dim myTable As New Table With {.CssClass = "SurveyFormTable", .GridLines = GridLines.None, .Width = New Unit(100, UnitType.Percentage)}
        Dim QuestionIndex As Integer = 1
        For Each myQuestion As QuestionItem In Survey.QuestionList
            If myQuestion.ReviewRoleLevel = 1 Then
                If DisplayQuestion(myQuestion) Then
                    For Each myAnswer As SurveyResponseAnswerItem In AnswerList
                        For Each myQA As QuestionAnswerItem In myQuestion.QuestionAnswerItemList
                            If myAnswer.QuestionAnswerID = myQA.QuestionAnswerID Then
                                If myQA.QuestionAnswerCommentFL Then
                                    ' Add Row for Question with the Employee Answer
                                    myTable.Rows.Add(GetQuestionRow(myQuestion))
                                    Dim myAnswerRow As New TableRow() With {.CssClass = "SurveyQuestionAnswerRow"}
                                    myTable.Rows.Add(myAnswerRow)
                                    If myQuestion.QuestionAnswerItemList.Count = 0 Then
                                        AppLog.ErrorLog("Question with No QuestionAnswers", "AutoFormSingleSequence.BuildQuestionAnswerForm")
                                    End If
                                    myAnswerRow.Cells.Add(GetControlCell("SurveyQuestionAnswerCell",
                                                                                              String.Format("<div class=""QuestionAnswerForm""><h3>Supervisor Review</h3>{0}{1}</div>",
                                                                                                            vbCrLf,
                                                                                                            SurveyResponseQuestionForm.GetQuestionAnswerReviewForm(ReviewStatusList,
                                                                                                                                                                  1, myAnswer))))
                                End If
                            End If
                        Next
                    Next
                End If
            End If
            QuestionIndex += 1
        Next
        Return myTable
    End Function
    Private Function GetAdminReviewFormAsTable(ByRef ReviewStatusList As List(Of SurveyReviewStatusItem)) As Table
        Dim myTable As New Table With {.CssClass = "SurveyFormTable", .GridLines = GridLines.None, .Width = New Unit(100, UnitType.Percentage)}
        Dim QuestionIndex As Integer = 1
        For Each myQuestion As QuestionItem In Survey.QuestionList
            If DisplayQuestion(myQuestion) Then
                For Each myAnswer As SurveyResponseAnswerItem In AnswerList
                    For Each myQA As QuestionAnswerItem In myQuestion.QuestionAnswerItemList
                        If myAnswer.QuestionAnswerID = myQA.QuestionAnswerID Then
                            If myQA.QuestionAnswerCommentFL Then
                                Dim myAnswerRow As New TableRow() With {.CssClass = "SurveyQuestionAnswerRow"}
                                myTable.Rows.Add(myAnswerRow)
                                If myQuestion.QuestionAnswerItemList.Count = 0 Then
                                    AppLog.ErrorLog("Question with No QuestionAnswers", "AutoFormSingleSequence.BuildQuestionAnswerForm")
                                End If
                                myAnswerRow.Cells.Add(GetControlCell("SurveyQuestionAnswerCell",
                                                                     String.Format("{1}<div class=""QuestionAnswerForm""><h3>Admin Review</h3>{0}</div>",
                                                                                   SurveyResponseQuestionForm.GetQuestionAnswerReviewForm(ReviewStatusList, 2, myAnswer),
                                                                                   GetQuestionFormHeader(myQuestion))))
                            End If
                        End If
                    Next
                Next
            End If
            QuestionIndex += 1
        Next
        Return myTable
    End Function
    Private Function GetSurveyResponseHistory() As String
        Dim mySB As New StringBuilder("<strong>History</strong><ul>")
        For Each myHistory As SurveyResponseHistoryItem In SurveyResponseHistory
            If myHistory.StatusID = 1 Then
                myHistory.StatusNM = "Submitted"
            End If
            mySB.Append(String.Format("<li>{0} by {3} on {1}-{2}</li>", myHistory.StatusNM, myHistory.ModifiedDT.ToShortDateString, myHistory.ModifiedDT.ToShortTimeString, myHistory.ModifiedNM))
        Next
        mySB.Append("</ul>")

        mySB.Append("<strong>Email History</strong><ul>")
        For Each myState As SurveyResponseStateItem In StateList

            If myState.EmailSent Then
                mySB.Append(String.Format("<li>{0} on {1}-{2}</li>", myState.EmailBody, myState.ModifiedDT.ToShortDateString, myState.ModifiedDT.ToShortTimeString))
            End If
        Next
        mySB.Append("</ul>")

        Return mySB.ToString()
    End Function
    Private Function GetSurveyResponseHistoryRow() As TableRow
        Using myTableRow As New TableRow() With {.CssClass = "SurveyQuestionRow"}
            myTableRow.Cells.Add(GetContentCell("SurveyHistory", GetSurveyResponseHistory()))
            Return myTableRow
        End Using
    End Function
    Public Shared Function IsValid(ByRef mySR As SurveyResponseItemBL) As Boolean
        Dim bReturn As Boolean
        If mySR.SurveyResponseID > 0 Then
            bReturn = True
        Else
            bReturn = False
        End If
        If mySR.Survey Is Nothing Then
            bReturn = False
        Else
            If mySR.Survey.QuestionList.Count < 1 Then
                bReturn = False
            End If
            If mySR.Survey.QuestionGroupList Is Nothing Then
                bReturn = False
            End If
        End If
        If mySR.SequenceList Is Nothing Then
            bReturn = False
        Else
            If mySR.SequenceList.Count < 1 Then
                bReturn = False
            End If
        End If
        Return bReturn
    End Function


#Region "EMail Functions"
    Public Function GetFormatedTemplate(ByVal SiteURL As String, ByVal EmailTemplate As String) As String
        If EmailTemplate Is Nothing Then
            EmailTemplate = "NO EMAIL"
        End If
        EmailTemplate = EmailTemplate.Replace("~SurveyResponseName~", SurveyResponseNM)
        EmailTemplate = EmailTemplate.Replace("~SurveyResponseNM~", SurveyResponseNM)
        EmailTemplate = EmailTemplate.Replace("~SurveyResponseLink~", String.Format("<a href='{2}/_layouts/SPSurvey/surveyresponse.aspx?SurveyResponseID={0}'>{1}</a>", SurveyResponseID, SurveyResponseNM, SiteURL))
        EmailTemplate = EmailTemplate.Replace("~SurveyResponseURL~", String.Format("{1}/_layouts/SPSurvey/surveyresponse.aspx?SurveyResponseID={0}", SurveyResponseID, SiteURL))
        EmailTemplate = EmailTemplate.Replace("~AssignedUserName~", String.Format("{0} {1}", Employee_FName, Employee_LName))
        EmailTemplate = EmailTemplate.Replace("~AssignedName~", String.Format("{0} {1}", Employee_FName, Employee_LName))
        EmailTemplate = EmailTemplate.Replace("~SupervisorName~", Manager_Name)
        EmailTemplate = EmailTemplate.Replace("~SurveyName~", Survey.SurveyNM)
        Return EmailTemplate
    End Function
    Public Function GetSurveyStatusEmailTemplate(ByRef StatusList As List(Of SurveyStatusItem)) As SurveyEmailTemplateItem
        Dim curEmailTemplate As SurveyEmailTemplateItem = New SurveyEmailTemplateItem
        For Each myStatus As SurveyStatusItem In StatusList
            If myStatus.StatusID = StatusID AndAlso myStatus.SurveyID = Survey.SurveyID Then
                StatusNM = myStatus.StatusNM
                curEmailTemplate.StatusID = myStatus.StatusID
                curEmailTemplate.BodyTemplate = myStatus.BodyTemplate
                curEmailTemplate.SubjectTemplate = myStatus.SubjectTemplate
                Exit For
            End If
        Next
        Return curEmailTemplate
    End Function
    Public Function GetSurveyStatusEmailTemplate() As SurveyEmailTemplateItem
        Dim curEmailTemplate As SurveyEmailTemplateItem = New SurveyEmailTemplateItem
        For Each myStatus As SurveyStatusItem In Survey.StatusList
            If myStatus.StatusID = StatusID AndAlso myStatus.SurveyID = Survey.SurveyID Then
                StatusNM = myStatus.StatusNM
                curEmailTemplate.StatusID = myStatus.StatusID
                curEmailTemplate.BodyTemplate = myStatus.BodyTemplate
                curEmailTemplate.SubjectTemplate = myStatus.SubjectTemplate
                Exit For
            End If
        Next
        Return curEmailTemplate
    End Function
    Public Function GetEmailItem(ByVal AppEmail As ApplicationEmailConfiguration) As EmailItem
        Dim SurveyEmailTemplate As SurveyEmailTemplateItem = GetSurveyStatusEmailTemplate()
        If SurveyEmailTemplate.FromEmailAddress = String.Empty Then
            SurveyEmailTemplate.FromEmailAddress = AppEmail.DefaultFromEmailAddress
        End If
        Dim thisEmail As New EmailItem() With {.BCCEmailAddress = SurveyEmailTemplate.FromEmailAddress, _
                                               .CCEmailAddress = String.Empty, _
                                               .ToAccount = GetSendToAccount(SurveyEmailTemplate), _
                                               .ContentType = "text/html", _
                                               .EmailBody = GetFormatedTemplate(AppEmail.SurveyRootWebURL, SurveyEmailTemplate.BodyTemplate), _
                                               .EmailSubject = GetFormatedTemplate(AppEmail.SurveyRootWebURL, SurveyEmailTemplate.SubjectTemplate), _
                                               .FromEmailAdderss = SurveyEmailTemplate.FromEmailAddress}
        Return thisEmail
    End Function
    Public Function GetEmailItem(ByVal AppEmail As ApplicationEmailConfiguration, ByVal SurveyEmailTemplate As SurveyEmailTemplateItem) As EmailItem
        If SurveyEmailTemplate.FromEmailAddress = String.Empty Then
            SurveyEmailTemplate.FromEmailAddress = AppEmail.DefaultFromEmailAddress
        End If
        Dim thisEmail As New EmailItem() With {.BCCEmailAddress = SurveyEmailTemplate.FromEmailAddress, _
                                               .CCEmailAddress = String.Empty, _
                                               .ToAccount = GetSendToAccount(SurveyEmailTemplate), _
                                               .ContentType = "text/html", _
                                               .EmailBody = GetFormatedTemplate(AppEmail.SurveyRootWebURL, SurveyEmailTemplate.BodyTemplate), _
                                               .EmailSubject = GetFormatedTemplate(AppEmail.SurveyRootWebURL, SurveyEmailTemplate.SubjectTemplate), _
                                               .FromEmailAdderss = SurveyEmailTemplate.FromEmailAddress}
        Return thisEmail
    End Function
    Public Function GetCompletionEmailItem(ByVal AppEmail As ApplicationEmailConfiguration, ByVal SurveyEmailTemplate As SurveyEmailTemplateItem) As EmailItem
        If SurveyEmailTemplate.FromEmailAddress = String.Empty Then
            SurveyEmailTemplate.FromEmailAddress = AppEmail.DefaultFromEmailAddress
        End If
        Dim thisEmail As New EmailItem() With {.BCCEmailAddress = SurveyEmailTemplate.FromEmailAddress, _
                                               .CCEmailAddress = String.Empty, _
                                               .ToAccount = AccountNM, _
                                               .ContentType = "text/html", _
                                               .EmailBody = GetFormatedTemplate(AppEmail.SurveyRootWebURL, Survey.CompletionMessage), _
                                               .EmailSubject = GetFormatedTemplate(AppEmail.SurveyRootWebURL, SurveyEmailTemplate.SubjectTemplate), _
                                               .FromEmailAdderss = SurveyEmailTemplate.FromEmailAddress}
        Return thisEmail
    End Function
    Public Function GetSendToAccount(ByVal EmailTemplate As SurveyEmailTemplateItem) As String
        Dim SendToEmail As String = String.Empty
        Try
            Select Case StatusID
                Case 1
                    If EmailTemplate.SendToSupervisor Then
                        SendToEmail = SupervisorAccountNM
                    Else
                        SendToEmail = AccountNM
                    End If
                Case 2
                    If EmailTemplate.SendToSupervisor Then
                        SendToEmail = SupervisorAccountNM
                    Else
                        SendToEmail = AccountNM
                    End If
                Case 3
                    SendToEmail = SupervisorAccountNM
                Case 4
                    SendToEmail = Survey.ReviewerAccountNM
                Case 5
                    SendToEmail = AccountNM
                Case 6
                    SendToEmail = Survey.ReviewerAccountNM
                Case Else
                    SendToEmail = Survey.ReviewerAccountNM
            End Select
        Catch ex As Exception
            SendToEmail = String.Empty
        End Try
        Return SendToEmail
    End Function


#End Region

End Class
