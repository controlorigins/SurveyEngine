Imports System.Text
Imports ControlOrigins.COUtility

Public Class SurveyResponseQuestionForm
    Property Question As com.controlorigins.ws.QuestionItem
    Property CurrentResponseList As New List(Of com.controlorigins.ws.SurveyResponseAnswerItem)
    Property SurveyResponseID As Integer
    Property QuestionIndex As Integer
    Property curSurveyResponseAnswer As com.controlorigins.ws.SurveyResponseAnswerItem
    Property StatusID As Integer
    Property SequenceNumber As Integer = 1
    Property TabIndex As Integer

    Public Enum QuestionType
        DDL
        DBL
        CBL
        INT
        DT
        CUR
        COM
        PCT
        CALC
        RBL
        TXT
    End Enum

    Public Function GetQuestionAnswerForm(ByVal SequenceNumber As Integer, bAddDiv As Boolean, ByVal myTabIndex As Integer) As String
        Dim bShowComment As Boolean
        Dim sForm As New StringBuilder(String.Empty)
        TabIndex = myTabIndex
        sForm.Append(vbCrLf)
        If CurrentResponseList.Count = 1 Then
            curSurveyResponseAnswer = CurrentResponseList(0)
        Else
            curSurveyResponseAnswer = New com.controlorigins.ws.SurveyResponseAnswerItem
        End If
        If bAddDiv Then
            sForm.Append("<div class=""QuestionAnswerForm"">")
        End If
        Select Case Question.QuestionTypeCD
            Case "DDL"
                sForm.Append(GetDropDownListQuestionHTML(bShowComment))
            Case "CBL"
                sForm.Append(GetCheckBoxListQuestionHTML(bShowComment))
            Case "RBL"
                sForm.Append(GetRadioButtonListQuestionHTML(bShowComment))
            Case "COM"
                sForm.Append(GetMemoInput(GetQuestionName(SurveyResponseID, SequenceNumber, Question), GetQuestionName(SurveyResponseID, SequenceNumber, Question), curSurveyResponseAnswer.AnswerComment, "display:inline;", Question.QuestionDS, Question.QuestionAnswerItemList(0).QuestionAnswerCommentFL))
            Case "PCT"
                sForm.Append(GetPercentInput(GetQuestionName(SurveyResponseID, SequenceNumber, Question), GetQuestionName(SurveyResponseID, SequenceNumber, Question), CStr(curSurveyResponseAnswer.AnswerQuantity), "display:inline;"))
            Case "DT"
                sForm.Append(GetDateInput(GetQuestionName(SurveyResponseID, SequenceNumber, Question), GetQuestionName(SurveyResponseID, SequenceNumber, Question), GetDateString(curSurveyResponseAnswer.AnswerDate), "display:inline;"))
            Case "CUR"
                sForm.Append(GetCurrencyInput(GetQuestionName(SurveyResponseID, SequenceNumber, Question), GetQuestionName(SurveyResponseID, SequenceNumber, Question), CStr(curSurveyResponseAnswer.AnswerQuantity), "display:inline;"))
            Case Else
                Select Case curSurveyResponseAnswer.AnswerType
                    Case "Quantity"
                        sForm.Append(GetTextInput(GetQuestionName(SurveyResponseID, SequenceNumber, Question), GetQuestionName(SurveyResponseID, SequenceNumber, Question), CStr(curSurveyResponseAnswer.AnswerQuantity), "display:inline;"))
                    Case "Date"
                        sForm.Append(GetDateInput(GetQuestionName(SurveyResponseID, SequenceNumber, Question), GetQuestionName(SurveyResponseID, SequenceNumber, Question), CStr(curSurveyResponseAnswer.AnswerDate), "display:inline;"))
                    Case Else
                        sForm.Append(GetTextInput(GetQuestionName(SurveyResponseID, SequenceNumber, Question), GetQuestionName(SurveyResponseID, SequenceNumber, Question), curSurveyResponseAnswer.AnswerComment, "display:inline;"))
                End Select
        End Select
        If bAddDiv Then
            sForm.Append("</div>")
        End If
        sForm.Append(vbCrLf)
        Return sForm.ToString()
    End Function
    Public Shared Function GetQuestionAnswerReviewForm(ByRef ReviewStatusList As List(Of com.controlorigins.ws.SurveyReviewStatusItem),
                                                       ByVal ReviewLevel As Integer,
                                                       ByRef CurrentResponse As com.controlorigins.ws.SurveyResponseAnswerItem) As String
        Dim QuestionAnswerReview As New com.controlorigins.ws.SurveyResponseAnswerReviewItem
        Dim sForm As New StringBuilder(String.Empty)
        sForm.Append(GetHiddenFormField(GetSurveyQuestionAnswerIDName(CurrentResponse.SurveyResponseID, CurrentResponse.SequenceNumber, CurrentResponse.QuestionID), CStr(CurrentResponse.SurveyAnswerID)))

        For Each myAnswerReview In CurrentResponse.AnswerReviewList
            If ReviewLevel = myAnswerReview.ReviewLevel Then
                QuestionAnswerReview = myAnswerReview
                sForm.Append(GetHiddenFormField(GetSurveyQuestionAnswerReviewIDName(CurrentResponse.SurveyResponseID, CurrentResponse.SequenceNumber, CurrentResponse.QuestionID), CStr(myAnswerReview.SurveyResponseAnswerReviewID)))
            End If
        Next

        For Each ReviewStatus As com.controlorigins.ws.SurveyReviewStatusItem In ReviewStatusList
            If ReviewStatus.ReviewStatusID = QuestionAnswerReview.ReviewStatusID Then
                If ReviewStatus.ApprovedFL Then
                    sForm.Append(GetRadioButtonOptionalTextBoxItem(GetQuestionName(CurrentResponse.SurveyResponseID, 1, CurrentResponse.QuestionID),
                                                           GetQuestionAnswerReviewItemID(CurrentResponse.SurveyResponseID, 1, CurrentResponse.QuestionID, ReviewStatus.ReviewStatusID),
                                                           CStr(ReviewStatus.ReviewStatusID),
                                                           GetQuestionAnswerComment(CurrentResponse.SurveyResponseID, 1, CurrentResponse.QuestionID),
                                                           True,
                                                           ReviewStatus.ReviewStatusDS,
                                                           ReviewStatus.CommentFL))
                Else
                    sForm.Append(GetRadioButtonTextBoxItem(GetQuestionName(CurrentResponse.SurveyResponseID, 1, CurrentResponse.QuestionID),
                                                           GetQuestionAnswerReviewItemID(CurrentResponse.SurveyResponseID, 1, CurrentResponse.QuestionID, ReviewStatus.ReviewStatusID),
                                                           CStr(ReviewStatus.ReviewStatusID),
                                                           GetQuestionAnswerComment(CurrentResponse.SurveyResponseID, 1, CurrentResponse.QuestionID),
                                                           True,
                                                           ReviewStatus.ReviewStatusDS,
                                                           ReviewStatus.CommentFL))

                End If
            Else
                If ReviewStatus.ApprovedFL Then
                    sForm.Append(GetRadioButtonOptionalTextBoxItem(GetQuestionName(CurrentResponse.SurveyResponseID, 1, CurrentResponse.QuestionID),
                                                           GetQuestionAnswerReviewItemID(CurrentResponse.SurveyResponseID, 1, CurrentResponse.QuestionID, ReviewStatus.ReviewStatusID),
                                                           CStr(ReviewStatus.ReviewStatusID),
                                                           GetQuestionAnswerComment(CurrentResponse.SurveyResponseID, 1, CurrentResponse.QuestionID),
                                                           False,
                                                           ReviewStatus.ReviewStatusDS,
                                                           ReviewStatus.CommentFL))
                Else
                    sForm.Append(GetRadioButtonTextBoxItem(GetQuestionName(CurrentResponse.SurveyResponseID, 1, CurrentResponse.QuestionID),
                                                           GetQuestionAnswerReviewItemID(CurrentResponse.SurveyResponseID, 1, CurrentResponse.QuestionID, ReviewStatus.ReviewStatusID),
                                                           CStr(ReviewStatus.ReviewStatusID),
                                                           GetQuestionAnswerComment(CurrentResponse.SurveyResponseID, 1, CurrentResponse.QuestionID),
                                                           False,
                                                           ReviewStatus.ReviewStatusDS,
                                                           ReviewStatus.CommentFL))

                End If
            End If
            sForm.Append(String.Format("{0}", vbCrLf))
        Next
        sForm.Append(GetMemoInput(GetQuestionAnswerComment(CurrentResponse.SurveyResponseID, 1, CurrentResponse.QuestionID),
                                  GetQuestionAnswerComment(CurrentResponse.SurveyResponseID, 1, CurrentResponse.QuestionID),
                                  QuestionAnswerReview.ModifiedComment, "", String.Empty, False))
        sForm.Append(vbCrLf)
        Return sForm.ToString()
    End Function

    Private Function GetDropDownListQuestionHTML(ByRef bShowComment As Boolean) As String
        Dim sbReturn As New StringBuilder(vbCrLf)
        sbReturn.Append(String.Format("<div class=""styled-select""><select title=""You must select an answer"" tabindex=""{2}"" class=""required"" name=""{1}"" id=""{0}"" >",
                                      GetQuestionName(SurveyResponseID, SequenceNumber, Question),
                                      GetQuestionName(SurveyResponseID, SequenceNumber, Question), TabIndex))

        sbReturn.Append(GetDropDownListItem(String.Empty,
                                            GetQuestionAnswerItemID(SurveyResponseID, SequenceNumber, Question, New com.controlorigins.ws.QuestionAnswerItem),
                                            String.Empty,
                                            GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                            True,
                                            String.Empty,
                                            False))

        For Each qAnswer As com.controlorigins.ws.QuestionAnswerItem In Question.QuestionAnswerItemList
            If curSurveyResponseAnswer.QuestionAnswerID = qAnswer.QuestionAnswerID Then
                ' bShowComment = qAnswer.QuestionAnswerCommentFL
                sbReturn.Append(GetDropDownListItem(GetQuestionName(SurveyResponseID, SequenceNumber, Question),
                                                       GetQuestionAnswerItemID(SurveyResponseID, SequenceNumber, Question, qAnswer),
                                                       CStr(qAnswer.QuestionAnswerID),
                                                       GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                                       True,
                                                       qAnswer.QuestionAnswerNM,
                                                       qAnswer.QuestionAnswerCommentFL))
                sbReturn.Append(String.Format("{0}", vbCrLf))
            Else
                sbReturn.Append(GetDropDownListItem(GetQuestionName(SurveyResponseID, SequenceNumber, Question), _
                                                    GetQuestionAnswerItemID(SurveyResponseID, SequenceNumber, Question, qAnswer), _
                                                    CStr(qAnswer.QuestionAnswerID), _
                                                    GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question), _
                                                    False, _
                                                    qAnswer.QuestionAnswerNM, qAnswer.QuestionAnswerCommentFL))
                sbReturn.Append(String.Format("{0}", vbCrLf))

            End If
        Next
        sbReturn.Append(String.Format("</select><br/>{0}", vbCrLf))


        ' bShowComment = True

        If Question.CommentFL Then
            sbReturn.Append(GetMemoInput(GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                         GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                         curSurveyResponseAnswer.AnswerComment,
                                         "",
                                         String.Empty,
                                         False))
        End If
        sbReturn.Append(String.Format("</div>{0}", vbCrLf))

        Return sbReturn.ToString
    End Function
    Private Function IsCurrentReponse(ByVal QAID As Integer) As Boolean
        Dim bReturn As Boolean = False
        For Each ra In CurrentResponseList
            If QAID = ra.QuestionAnswerID Then
                bReturn = True
                Exit For
            End If
        Next
        Return bReturn
    End Function


    Private Function GetCheckBoxListQuestionHTML(ByRef bShowComment As Boolean) As String
        Dim sbReturn As New StringBuilder(vbCrLf)
        sbReturn.Append(String.Format("<div class=""styled-select"">",
                                      GetQuestionName(SurveyResponseID, SequenceNumber, Question),
                                      GetQuestionName(SurveyResponseID, SequenceNumber, Question)))

        For Each qAnswer As com.controlorigins.ws.QuestionAnswerItem In Question.QuestionAnswerItemList

            If IsCurrentReponse(qAnswer.QuestionAnswerID) Then
                bShowComment = qAnswer.QuestionAnswerCommentFL
                sbReturn.Append(GetCheckBoxListItem(GetQuestionName(SurveyResponseID, SequenceNumber, Question),
                                                       GetQuestionAnswerItemID(SurveyResponseID, SequenceNumber, Question, qAnswer),
                                                       CStr(qAnswer.QuestionAnswerID),
                                                       GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                                       True,
                                                       qAnswer.QuestionAnswerNM,
                                                       qAnswer.QuestionAnswerCommentFL))
                sbReturn.Append(String.Format("{0}", vbCrLf))
            Else
                sbReturn.Append(GetCheckBoxListItem(GetQuestionName(SurveyResponseID, SequenceNumber, Question), _
                                                    GetQuestionAnswerItemID(SurveyResponseID, SequenceNumber, Question, qAnswer), _
                                                    CStr(qAnswer.QuestionAnswerID), _
                                                    GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question), _
                                                    False, _
                                                    qAnswer.QuestionAnswerNM, qAnswer.QuestionAnswerCommentFL))
                sbReturn.Append(String.Format("{0}", vbCrLf))
            End If
        Next

        If Question.CommentFL Then
            sbReturn.Append(GetMemoInput(GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                         GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                         curSurveyResponseAnswer.AnswerComment,
                                         "",
                                         String.Empty,
                                         False))
        End If

        sbReturn.Append(String.Format("<br/>{0}{1}</div>{1}", String.Empty, vbCrLf))
        Return sbReturn.ToString
    End Function

    Private Function GetRadioButtonListQuestionHTML(ByRef bShowComment As Boolean) As String
        Dim sbReturn As New StringBuilder(vbCrLf)
        For Each qAnswer As com.controlorigins.ws.QuestionAnswerItem In Question.QuestionAnswerItemList
            If curSurveyResponseAnswer.QuestionAnswerID = qAnswer.QuestionAnswerID Then
                bShowComment = qAnswer.QuestionAnswerCommentFL
                sbReturn.Append(GetRadioButtonTextBoxItem(GetQuestionName(SurveyResponseID, SequenceNumber, Question),
                                                          GetQuestionAnswerItemID(SurveyResponseID, SequenceNumber, Question, qAnswer),
                                                          CStr(qAnswer.QuestionAnswerID),
                                                          GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                                          True,
                                                          qAnswer.QuestionAnswerDS,
                                                          qAnswer.QuestionAnswerCommentFL))
                sbReturn.Append(String.Format("{0}", vbCrLf))
            Else
                sbReturn.Append(GetRadioButtonTextBoxItem(GetQuestionName(SurveyResponseID, SequenceNumber, Question),
                                                          GetQuestionAnswerItemID(SurveyResponseID, SequenceNumber, Question, qAnswer),
                                                          CStr(qAnswer.QuestionAnswerID),
                                                          GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                                          False,
                                                          qAnswer.QuestionAnswerDS,
                                                          qAnswer.QuestionAnswerCommentFL))
                sbReturn.Append(String.Format("{0}", vbCrLf))
            End If
        Next
        If bShowComment Then
            sbReturn.Append(GetMemoInput(GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                         GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                         curSurveyResponseAnswer.AnswerComment,
                                         "",
                                         Question.QuestionDS,
                                         False))
        Else
            sbReturn.Append(GetMemoInput(GetQuestionAnswerComment(SurveyResponseID,
                                                                                       SequenceNumber,
                                                                                       Question),
                                         GetQuestionAnswerComment(SurveyResponseID,
                                                                                       SequenceNumber,
                                                                                       Question),
                                         curSurveyResponseAnswer.AnswerComment,
                                         "display:none;",
                                         Question.QuestionDS,
                                         False))
        End If
        Return sbReturn.ToString
    End Function
    Private Shared Function GetDropDownListItem(ByVal Name As String, ByVal ID As String, ByVal Value As String, ByVal TextBoxID As String, ByVal Checked As Boolean, ByVal Label As String, ByVal RequireComment As Boolean) As String
        If Checked Then
            If RequireComment Then
                Return String.Format("<option value=""{1}"" selected >{0}</option>{2}", Label, Value, vbCrLf)
            Else
                Return String.Format("<option value=""{1}"" selected >{0}</option>{2}", Label, Value, vbCrLf)
            End If
        Else
            If RequireComment Then
                Return String.Format("<option value=""{1}"" >{0}</option>{2}", Label, Value, vbCrLf)
            Else
                Return String.Format("<option value=""{1}"" >{0}</option>{2}", Label, Value, vbCrLf)
            End If
        End If
    End Function

    Private Function GetCheckBoxListItem(ByVal Name As String, ByVal ID As String, ByVal Value As String, ByVal TextBoxID As String, ByVal Checked As Boolean, ByVal Label As String, ByVal RequireComment As Boolean) As String
        If Checked Then
            Return String.Format("<input type=""checkbox"" name=""{2}[]"" value=""{1}"" checked tabindex=""{4}"" />{0}<br />", Label, Value, Name, vbCrLf, TabIndex)
        Else
            Return String.Format("<input type=""checkbox"" name=""{2}[]"" value=""{1}"" tabindex=""{4}""  />{0}<br />", Label, Value, Name, vbCrLf, TabIndex)
        End If
    End Function


    Private Shared Function GetRadioButtonOptionalTextBoxItem(ByVal Name As String, ByVal ID As String, ByVal Value As String, ByVal TextBoxID As String, ByVal Checked As Boolean, ByVal Label As String, ByVal RequireComment As Boolean) As String
        If Checked Then
            If RequireComment Then
                Return String.Format("<input type=""radio"" class=""required"" title=""You must select an answer"" name=""{0}"" id=""{1}"" value=""{2}"" onclick=""enableOptionalTxt('{3}')"" checked=""checked"" /><label for=""{1}"">{4}</label><br />", Name, ID, Value, TextBoxID, Label)
            Else
                Return String.Format("<input type=""radio"" class=""required""  title=""You must select an answer"" name=""{0}"" id=""{1}"" value=""{2}"" onclick=""disableTxt('{3}')"" checked=""checked"" /><label for=""{1}"">{4}</label><br />", Name, ID, Value, TextBoxID, Label)
            End If
        Else
            If RequireComment Then
                Return String.Format("<input type=""radio"" class=""required""  title=""You must select an answer"" name=""{0}"" id=""{1}"" value=""{2}"" onclick=""enableOptionalTxt('{3}')"" /><label for=""{1}"">{4}</label><br />", Name, ID, Value, TextBoxID, Label)
            Else
                Return String.Format("<input type=""radio"" class=""required""  title=""You must select an answer"" name=""{0}"" id=""{1}"" value=""{2}"" onclick=""disableTxt('{3}')"" /><label for=""{1}"">{4}</label><br />", Name, ID, Value, TextBoxID, Label)
            End If
        End If
    End Function

    Private Shared Function GetRadioButtonTextBoxItem(ByVal Name As String, ByVal ID As String, ByVal Value As String, ByVal TextBoxID As String, ByVal Checked As Boolean, ByVal Label As String, ByVal RequireComment As Boolean) As String
        If Checked Then
            If RequireComment Then
                Return String.Format("<input type=""radio"" class=""required"" title=""You must select an answer"" name=""{0}"" id=""{1}"" value=""{2}"" onclick=""enableTxt('{3}')"" checked=""checked"" /><label for=""{1}"">{4}</label>", Name, ID, Value, TextBoxID, Label)
            Else
                Return String.Format("<input type=""radio"" class=""required""  title=""You must select an answer"" name=""{0}"" id=""{1}"" value=""{2}"" onclick=""disableTxt('{3}')"" checked=""checked"" /><label for=""{1}"">{4}</label>", Name, ID, Value, TextBoxID, Label)
            End If
        Else
            If RequireComment Then
                Return String.Format("<input type=""radio"" class=""required""  title=""You must select an answer"" name=""{0}"" id=""{1}"" value=""{2}"" onclick=""enableTxt('{3}')"" /><label for=""{1}"">{4}</label>", Name, ID, Value, TextBoxID, Label)
            Else
                Return String.Format("<input type=""radio"" class=""required""  title=""You must select an answer"" name=""{0}"" id=""{1}"" value=""{2}"" onclick=""disableTxt('{3}')"" /><label for=""{1}"">{4}</label>", Name, ID, Value, TextBoxID, Label)
            End If
        End If
    End Function

    Private Shared Function GetMemoInput(ByVal Name As String, ByVal ID As String, ByVal CurrentValue As String, ByVal style As String, ByVal Description As String, ByVal RequireComment As Boolean) As String
        If RequireComment Then
            If style = String.Empty Then
                Return String.Format("<textarea style=""width: 90%; height: 77px;{2}"" id=""{1}"" cols=""20"" rows=""5"" name=""{0}"" class=""required"">{3}</textarea><label for=""{1}""></label>", Name, ID, style, CurrentValue)
            Else
                Return String.Format("<textarea style=""width: 90%; height: 77px;{2}"" id=""{1}"" cols=""20"" rows=""5"" name=""{0}"" class=""required"">{3}</textarea><label for=""{1}""></label>", Name, ID, style, CurrentValue)
            End If
        Else
            If style = String.Empty Then
                Return String.Format("<textarea style=""width: 90%; height: 77px;{2}"" id=""{1}"" cols=""20"" rows=""5"" name=""{0}"">{3}</textarea><label for=""{1}""></label>", Name, ID, style, CurrentValue)
            Else
                Return String.Format("<textarea style=""width: 90%; height: 77px;{2}"" id=""{1}"" cols=""20"" rows=""5"" name=""{0}"">{3}</textarea><label for=""{1}""></label>", Name, ID, style, CurrentValue)
            End If
        End If
    End Function

    Private Function GetTextInput(ByVal Name As String, ByVal ID As String, ByVal CurrentValue As String, ByVal style As String) As String
        Dim sbReturn As New StringBuilder(String.Empty)
        If style = String.Empty Then
            sbReturn.Append(String.Format("<input style=""width:100%;padding:0;display:block;margin:0;{2}"" id=""{1}"" name=""{0}"" value=""{3}"" tabindex=""{4}""></input><label for=""{1}""></label>", Name, ID, style, CurrentValue, TabIndex))
        Else
            sbReturn.Append(String.Format("<input style=""width:100%;padding:0;display:block;margin:0;{2}"" id=""{1}"" name=""{0}""value=""{3}"" tabindex=""{4}""></input><label for=""{1}""></label>", Name, ID, style, CurrentValue, TabIndex))
        End If
        If Question.CommentFL Then
            sbReturn.Append(GetMemoInput(GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                         GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                         curSurveyResponseAnswer.AnswerComment,
                                         "",
                                         String.Empty,
                                         False))
        End If
        Return sbReturn.ToString()
    End Function

    Private Function GetCurrencyInput(ByVal Name As String, ByVal ID As String, ByVal CurrentValue As String, ByVal style As String) As String
        Dim sbReturn As New StringBuilder(String.Empty)
        If style = String.Empty Then
            sbReturn.Append(String.Format("<input type=""number"" pattern=""[0-9]+([\,|\.][0-9]+)?"" step=""0.01"" id=""{1}"" name=""{0}"" value=""{3}"" tabindex=""{4}"" ></input>", Name, ID, style, CurrentValue, TabIndex))
        Else
            sbReturn.Append(String.Format("<input type=""number"" pattern=""[0-9]+([\,|\.][0-9]+)?"" step=""0.01""  id=""{1}"" name=""{0}"" value=""{3}"" tabindex=""{4}"" ></input>", Name, ID, style, CurrentValue, TabIndex))
        End If
        If Question.CommentFL Then
            sbReturn.Append(GetMemoInput(GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                         GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                         curSurveyResponseAnswer.AnswerComment,
                                         "",
                                         String.Empty,
                                         False))
        End If
        Return sbReturn.ToString()
    End Function

    Private Function GetPercentInput(ByVal Name As String, ByVal ID As String, ByVal CurrentValue As String, ByVal style As String) As String
        Dim sbReturn As New StringBuilder(String.Empty)
        If style = String.Empty Then
            sbReturn.Append(String.Format("<div class=""SliderControl"" tabindex=""{4}""><label for=""{1}""></label><input type=""text"" class=""SliderText"" readonly=""readonly"" id=""{1}"" name=""{0}"" value=""{3}"" ></input><div class=""PercentSlider"" id=""{1}Slider""></div>", Name, ID, style, CurrentValue, TabIndex))
        Else
            sbReturn.Append(String.Format("<div class=""SliderControl"" tabindex=""{4}""><label for=""{1}""></label><input type=""text"" class=""SliderText"" readonly=""readonly"" id=""{1}"" name=""{0}"" value=""{3}"" ></input><div class=""PercentSlider"" id=""{1}Slider""></div>", Name, ID, style, CurrentValue, TabIndex))
        End If

        If Question.CommentFL Then
            sbReturn.Append(GetMemoInput(GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                         GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                         curSurveyResponseAnswer.AnswerComment,
                                         "",
                                         String.Empty,
                                         False))
        End If
        Return sbReturn.ToString()
    End Function

    Private Function GetDateInput(ByVal Name As String, ByVal ID As String, ByVal CurrentValue As String, ByVal style As String) As String
        Dim sbReturn As New StringBuilder(String.Empty)
        Try
            If style = String.Empty Then
                sbReturn.Append(String.Format("<input type=""text"" class=""DatePicker"" readonly=""readonly"" id=""{1}"" name=""{0}"" value=""{3}"" tabindex=""{4}"" ></input>", Name, ID, style, CurrentValue, TabIndex))
            Else
                sbReturn.Append(String.Format("<input type=""text"" class=""DatePicker"" readonly=""readonly"" id=""{1}"" name=""{0}"" value=""{3}"" tabindex=""{4}"" ></input>", Name, ID, style, CurrentValue, TabIndex))
            End If
            If Question.CommentFL Then
                sbReturn.Append(GetMemoInput(GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                             GetQuestionAnswerComment(SurveyResponseID, SequenceNumber, Question),
                                             curSurveyResponseAnswer.AnswerComment,
                                             "",
                                             String.Empty,
                                             False))
            End If
        Catch ex As Exception
            ApplicationLogging.ErrorLog(ex.ToString, "SurveyResponseItem.GetDateInput")
        End Try
        Return sbReturn.ToString()
    End Function

    Private Function GetDateString(ByVal theDate As Date?) As String
        Try
            Return CStr(theDate)
        Catch
            Return String.Empty
        End Try
    End Function


End Class

'<div class="SliderControl">
'    <label for="s1">Slider 1:</label>
'    <input type="text" class="SliderText" readonly="readonly" id="s1" name="s1"/>
'    <div class="RatingSlider" id="s1Slider"></div> 
'</div>