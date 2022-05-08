Imports System.IO
Imports System.Xml.Serialization
Imports System.Xml
Imports System.Text

Public Class SurveyItemBL
    Inherits SurveyItem

    Sub New(mySurvey As SurveyItem)
        With Me
            .CompletionMessage = mySurvey.CompletionMessage
            .AutoAssignFilter = mySurvey.AutoAssignFilter
            .ResponseNMTemplate = mySurvey.ResponseNMTemplate
            .SurveyDS = mySurvey.SurveyDS
            .SurveyNM = mySurvey.SurveyNM
            .SurveyShortNM = mySurvey.SurveyShortNM
            .SurveyTypeID = mySurvey.SurveyTypeID
            .SurveyID = mySurvey.SurveyID
            .StartDT = mySurvey.StartDT
            .EndDT = mySurvey.EndDT
            .ReviewerAccountNM = mySurvey.ReviewerAccountNM
            .UseSurveyGroupsFL = mySurvey.UseSurveyGroupsFL
            .QuestionGroupList = mySurvey.QuestionGroupList
            .QuestionList = mySurvey.QuestionList
            .ParentSurveyID = mySurvey.ParentSurveyID
            .StatusList = mySurvey.StatusList
            .ReviewStatusList = mySurvey.ReviewStatusList
            .EmailTemplateList = mySurvey.EmailTemplateList
        End With
    End Sub

    Sub New()

    End Sub

    Public Function Validate(ByRef sbValidateMessage As StringBuilder) As Boolean
        Dim bRetun As Boolean = True
        Dim iQuestionCount As Integer = 0
        If QuestionList.Count < 1 Then
            bRetun = False
            sbValidateMessage.Append("<br/>Survey Has No Questions<br/>")
        Else

        End If
        If QuestionGroupList.Count < 1 Then
            bRetun = False
            sbValidateMessage.Append("<br/>Survey Has No Groups<br/>")
        Else
            For Each myGroup As QuestionGroupItem In QuestionGroupList
                iQuestionCount = 0
                For Each myQuestion In QuestionList
                    If myQuestion.QuestionGroupMember.QuestionGroupID = myGroup.QuestionGroupID Then
                        iQuestionCount = iQuestionCount + 1
                        If myQuestion.QuestionAnswerItemList.Count < 1 Then
                            bRetun = False
                            sbValidateMessage.Append(String.Format("<br/>Question ({1}.{0}) Has no answers<br/>", myQuestion.QuestionShortNM, myQuestion.SurveyDisplayOrder))
                        End If
                    End If
                Next
                If iQuestionCount = 0 Then
                    bRetun = False
                    sbValidateMessage.Append(String.Format("<br/><em style='color:red;'>{0}</em> Has NO questions<br/>", myGroup.QuestionGroupNM))
                Else
                    sbValidateMessage.Append(String.Format("<br/><em>{0}</em> Has {1} questions<br/>", myGroup.QuestionGroupNM, iQuestionCount))
                End If
            Next
        End If
        Return bRetun
    End Function


    Public Shared Function GetFromXML(ByVal UserXML As String) As SurveyItemBL
        Dim myObject As New SurveyItemBL
        If Not (UserXML Is Nothing) And Not (UserXML = String.Empty) Then
            Using read As StringReader = New StringReader(UserXML)
                Dim serializer As New XmlSerializer(myObject.GetType())
                Using reader As XmlReader = New XmlTextReader(read)
                    Try
                        myObject = DirectCast(serializer.Deserialize(reader), SurveyItemBL)
                    Catch
                        Throw
                    End Try
                End Using
            End Using
        End If
        Return myObject
    End Function

    'Public Function SaveXML() As String
    '    Try
    '        Dim myXML As New SurveyXmlDocument()
    '        If Me Is Nothing Then
    '            Return String.Empty
    '        Else
    '            Dim oXS As XmlSerializer = New XmlSerializer(GetType(SurveyItemBL))
    '            Using writer As New StringWriter()
    '                oXS.Serialize(writer, Me)
    '                myXML.LoadXml(writer.ToString)
    '            End Using
    '            myXML.Save(String.Format("c:\SPSurvey\survey\{0}.xml", SurveyNM))
    '        End If
    '    Catch ex As Exception
    '        Return "ERROR"
    '    End Try
    '    Return String.Empty
    'End Function

    Public Function GetXML() As String
        Dim oXS As XmlSerializer = New XmlSerializer(Me.GetType)
        Using writer As New StringWriter()
            oXS.Serialize(writer, Me)
            Return writer.ToString
        End Using
    End Function

    Public Function GetXML(ByVal sPath As String) As SurveyItemBL
        Dim mySurvey As New SurveyItemBL
        Dim x As New XmlSerializer(GetType(SurveyItemBL))
        Try
            Using objStreamReader As New StreamReader(sPath)
                mySurvey = CType(x.Deserialize(objStreamReader), SurveyItemBL)
                objStreamReader.Close()
                With Me
                    .SurveyID = mySurvey.SurveyID
                    .SurveyNM = mySurvey.SurveyNM
                    .SurveyShortNM = mySurvey.SurveyShortNM
                    .SurveyDS = mySurvey.SurveyDS
                    .CompletionMessage = mySurvey.CompletionMessage
                    .AutoAssignFilter = mySurvey.AutoAssignFilter
                    .ResponseNMTemplate = mySurvey.ResponseNMTemplate
                    .SurveyTypeID = mySurvey.SurveyTypeID
                    .StartDT = mySurvey.StartDT
                    .EndDT = mySurvey.EndDT
                    .ReviewerAccountNM = mySurvey.ReviewerAccountNM
                    .UseSurveyGroupsFL = mySurvey.UseSurveyGroupsFL
                    .QuestionGroupList = mySurvey.QuestionGroupList
                    .QuestionList = mySurvey.QuestionList
                    .ParentSurveyID = mySurvey.ParentSurveyID
                    .StatusList = mySurvey.StatusList
                    .ReviewStatusList = mySurvey.ReviewStatusList
                    .EmailTemplateList = mySurvey.EmailTemplateList
                End With
            End Using
        Catch ex As Exception
            AppLog.ErrorLog(ex.ToString, "SurveyItemBL.GetXML")
        End Try
        Return mySurvey
    End Function

    Public Sub SaveXML(ByVal sFilePath As String)
        Try
            Dim sPath = String.Format("{0}\survey_{1}.xml", sFilePath, SurveyNM)
            Dim myXML As New SurveyXmlDocument()
            If Me Is Nothing Then
                ' Do Nothing 
            Else
                Dim oXS As XmlSerializer = New XmlSerializer(GetType(SurveyItemBL))
                Using writer As New StringWriter()
                    oXS.Serialize(writer, Me)
                    myXML.LoadXml(writer.ToString)
                End Using
                myXML.Save(sPath)
            End If
        Catch ex As Exception
            AppLog.ErrorLog(ex.ToString, String.Format("SurveyItemBL.SaveXML - {0}", SurveyNM))
        End Try
    End Sub

End Class
