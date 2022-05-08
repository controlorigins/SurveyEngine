Imports Microsoft.VisualBasic
Imports CODataCon.com.controlorigins.ws
Imports CODataCon
Imports System.Web.UI.WebControls
Imports System.Text
Imports System.Web

Public MustInherit Class SurveyUserControlBase
    Inherits ApplicationControlBase
    Implements ISurveyResponseList

    Public Event SurveyListUpdated()
    Public Event SurveyUpdated()
    Public Event SurveyResponseListUpdated()
    Public Event ApplicationUserRoleUpdated()
    Public Event ApplicationUserListUpdated()
    Public Event ApplicationUpdated()

    Public myCon As New DataControler()
    Public SurveyApplicationID As String = CStr(84)
    Public Const STR_Certifications As String = "DataSets"
    Public Const STR_Applicationid As String = "ApplicationID"
    Public Const STR_Questiongroupid As String = "GuestionGroupID"
    Public Const STR_ApplicationTheme As String = "ApplicationTheme"

    Private _Application As New ApplicationItem
    Const STR_AppUserCode As String = "Survey_ApplicationUser-{0}-{1}"
    Const STR_AppCode As String = "Survey_Application-{0}"
    Public curProperties As GlobalApplicationProperties

    Public ReadOnly Property CurUser As ApplicationUserRoleItem
        Get
            Try
                If CType(HttpContext.Current.Session(GetAppUserCode()), ApplicationUserRoleItem) Is Nothing Then
                    If CurApplication.ApplicationID > 0 Then
                        SetCurUser(myCon.ApplicationUserLogin(UserInfo.EmailAddress, CurApplication.ApplicationID))
                        WebProjectMechanics.ApplicationLogging.AuditLog(String.Format("SessionUpdated:{0}", GetAppUserCode()), "SurveyUserControlBase.CurUser")
                        RaiseEvent ApplicationUserRoleUpdated()
                    End If
                    Return CType(HttpContext.Current.Session(GetAppUserCode()), ApplicationUserRoleItem)
                Else
                    Return CType(HttpContext.Current.Session(GetAppUserCode()), ApplicationUserRoleItem)
                End If
            Catch
                HttpContext.Current.Application(GetAppUserCode()) = New ApplicationUserRoleItem
                Return CType(HttpContext.Current.Session(GetAppUserCode()), ApplicationUserRoleItem)
            End Try
            Return New ApplicationUserRoleItem
        End Get
    End Property
    Public Function SetCurUser(ByVal CurUser As ApplicationUserRoleItem) As ApplicationUserRoleItem
        HttpContext.Current.Session.Item(GetAppUserCode()) = CurUser
        Return CurUser
    End Function
    Public ReadOnly Property CurApplication As ApplicationItem
        Get
            Try
                If CType(HttpContext.Current.Session(GetAppCode()), ApplicationItem) Is Nothing Then
                    Try
                        _Application.ApplicationID = CInt(GetProperty("applicationid"))
                        _Application = myCon.GetApplicationByApplicationID(_Application.ApplicationID)
                        SetApplication(_Application)
                        WebProjectMechanics.ApplicationLogging.AuditLog(String.Format("SessionUpdated:{0}", GetAppCode()), "SurveyUserControlBase.CurApplication")
                        RaiseEvent ApplicationUpdated()
                    Catch ex As Exception
                        WebProjectMechanics.ApplicationLogging.ErrorLog("ApplicationID is not found", "curApplication")
                    End Try
                    Return CType(HttpContext.Current.Session(GetAppCode()), ApplicationItem)
                Else
                    Return CType(HttpContext.Current.Session(GetAppCode()), ApplicationItem)
                End If
            Catch
                HttpContext.Current.Application(GetAppCode()) = New ApplicationItem
                Return CType(HttpContext.Current.Session(GetAppCode()), ApplicationItem)
            End Try
            Return New ApplicationItem
        End Get
    End Property
    Public Function SetApplication(ByVal myApplication As ApplicationItem) As ApplicationItem
        HttpContext.Current.Session.Item(GetAppCode()) = myApplication
        Return myApplication
    End Function
    Public Function GetAppUserCode() As String
        Return String.Format(STR_AppUserCode, UserInfo.EmailAddress, GetProperty(STR_Applicationid))
    End Function
    Public Function GetAppCode() As String
        Return String.Format(STR_AppCode, GetProperty(STR_Applicationid))
    End Function


    Public ReadOnly Property _SurveyList As List(Of SurveyItem)
        Get
            Return (From i In CurApplication.ApplicationSurveyList Order By i.Survey.SurveyNM Select i.Survey).ToList()
        End Get
    End Property

    Public _SurveyResponseList As New List(Of SurveyResponseItemBL)
    Public _Survey As New SurveyItem

    Protected Function GetPositiveIntegerProperty(ByVal myProperty As String, ByVal curValue As Integer) As Integer
        Dim myValue As Integer
        If IsNumeric(Request.QueryString(myProperty)) AndAlso CInt(Request.QueryString(myProperty)) > 0 Then
            myValue = CInt(Request.QueryString(myProperty))
        ElseIf IsNumeric(Request.Form.Item(myProperty)) AndAlso CInt(Request.Form.Item(myProperty)) > 0 Then
            myValue = CInt(Request.Form.Item(myProperty))
        Else
            myValue = curValue
        End If
        Return myValue
    End Function
    Public Function GetBooleanProperty(ByVal myProperty As String, ByVal curValue As String) As Boolean
        Dim myValue As String = ""
        If Len(Request.QueryString(myProperty)) > 0 Then
            myValue = Request.QueryString(myProperty).ToString
        ElseIf Len(Request.Form.Item(myProperty)) > 0 Then
            myValue = Request.Form.Item(myProperty).ToString
        Else
            If curValue Is Nothing Then
                myValue = String.Empty
            Else
                myValue = curValue
            End If
        End If
        If myValue = "True" Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetMessageBox(ByVal title As String, ByVal message As String, ByVal type As String) As String
        Dim mysb As StringBuilder = New StringBuilder(String.Empty)
        mysb.Append("<div class=""row"">")
        mysb.Append("<div class=""col-lg-12"">")
        mysb.Append(String.Format("<div class=""alert alert-dismissable alert-{0}"">", type.ToLower))
        mysb.Append("<button type=""button"" class=""close"" data-dismiss=""alert"">×</button>")
        mysb.Append(String.Format("<h4>{0}</h4>", title))
        mysb.Append(String.Format("<p>{0}</p>", message))
        mysb.Append("</div></div></div>")
        Return mysb.ToString()
    End Function
    Public Function IsValidAnswer(ByRef myAnswerItem As SurveyResponseAnswerItem) As Boolean
        Dim bIsValid As Boolean = True
        If myAnswerItem.SurveyResponseID < 1 Then
            bIsValid = False
        End If
        If myAnswerItem.QuestionID < 1 Then
            bIsValid = False
        End If
        If myAnswerItem.ResponseList Is Nothing Then
            bIsValid = False
        Else
            If myAnswerItem.ResponseList.Count < 1 Then
                bIsValid = False
            End If
        End If
        Return bIsValid
    End Function

    Public Function GetSurveyList() As List(Of SurveyItem)
        Return (From i In CurApplication.ApplicationSurveyList Order By i.Survey.SurveyNM Select i.Survey).ToList()
    End Function

    Public Shared Sub LoadDropDown(ByRef ddl As ListBox, value As List(Of LookupItem))
        ddl.DataValueField = "Value"
        ddl.DataTextField = "Name"
        ddl.DataSource = value
        ddl.DataBind()
        ddl.Items.Add(New ListItem("ALL", String.Empty, True))
        ddl.Items.FindByText("ALL").Selected = True
    End Sub

    Public Shared Sub LoadDropDown(ByRef ddl As DropDownList, value As List(Of LookupItem))
        ddl.DataValueField = "Value"
        ddl.DataTextField = "Name"
        ddl.DataSource = value
        ddl.DataBind()
        ddl.Items.Add(New ListItem("ALL", String.Empty, True))
        ddl.Items.FindByText("ALL").Selected = True
    End Sub
    Public Shared Sub LoadSurveyDropDown(ByRef ddl As DropDownList, value As List(Of SurveyItem))
        Try
            ddl.Items.Clear()
            ddl.SelectedValue = Nothing
            ddl.DataValueField = "SurveyID"
            ddl.DataTextField = "SurveyNM"
            ddl.DataSource = value
            ddl.DataBind()
            ddl.Items.Add(New ListItem("ALL", String.Empty, True))
            ddl.Items.FindByText("ALL").Selected = True
        Catch ex As Exception
            AppLog.ErrorLog(ex.ToString, "SPSurveyAdminUserControl.LoadSurveyDropDown")
        End Try
    End Sub
    Public Shared Sub LoadSurveyDropDown(ByRef ddl As ListBox, value As List(Of SurveyItem))
        ddl.DataValueField = "SurveyID"
        ddl.DataTextField = "SurveyNM"
        ddl.DataSource = value
        ddl.DataBind()
        ddl.Items.Add(New ListItem("ALL", String.Empty, True))
        ddl.Items.FindByText("ALL").Selected = True
    End Sub
    Protected Shared Function GetControlSQLFilterClause(ByRef myListBox As ListBox, ByVal myFieldType As String, ByVal FieldName As String) As CODataCon.com.controlorigins.ws.SQLFilterClause
        Dim iCount As Integer = 0
        Dim myReturn As String = String.Empty
        Dim mySQLFilterClause = New CODataCon.com.controlorigins.ws.SQLFilterClause()
        With mySQLFilterClause
            .Field = FieldName
            .FieldOperator = CODataCon.com.controlorigins.ws.SQLFilterOperator.dbIn
            .Argument = myReturn
            .Conjunction = CODataCon.com.controlorigins.ws.SQLFilterConjunction.andConjunction
            .FieldType = myFieldType
        End With

        If myListBox.SelectionMode = ListSelectionMode.Multiple Then
            For Each iSelect As Integer In myListBox.GetSelectedIndices()
                If iCount > 0 Then
                    myReturn = String.Format("{0},'{1}'", myReturn, myListBox.Items(iSelect).Value.Replace("'", "''"))
                Else
                    myReturn = String.Format("{0}'{1}'", myReturn, myListBox.Items(iSelect).Value.Replace("'", "''"))
                End If
                iCount = iCount + 1
            Next
            mySQLFilterClause.FieldOperator = SQLFilterOperator.dbIn
        Else
            mySQLFilterClause.FieldOperator = SQLFilterOperator.Equal
            myReturn = myListBox.SelectedValue
        End If
        mySQLFilterClause.Argument = myReturn
        Return mySQLFilterClause
    End Function

    Public WriteOnly Property SurveyResponseList As List(Of SurveyResponseItemBL) Implements ISurveyResponseList.SurveyResponseList
        Set(value As List(Of SurveyResponseItemBL))
            _SurveyResponseList = value
        End Set
    End Property

    Public Function GetApplicationUserRoleTableHeader() As WebProjectMechanics.DisplayTableHeader
        '        Dim myUser As New ApplicationUserRoleItem
        Dim myHeader As New WebProjectMechanics.DisplayTableHeader
        myHeader.DetailFieldName = "eMailAddress"
        myHeader.DetailKeyName = "ApplicationUserID"
        myHeader.DetailPath = "/Co_Apps/SurveyAdmin/navigator.aspx?ApplicationUserID={0}&Action=applicationuserview"
        Return myHeader
    End Function

    Public Function GetSurveyTableHeader() As WebProjectMechanics.DisplayTableHeader
        ' Dim myApplicationSurvey As New SurveyItem
        Dim myHeader As New WebProjectMechanics.DisplayTableHeader
        myHeader.DetailFieldName = "SurveyNM"
        myHeader.DetailKeyName = "SurveyID"
        myHeader.DetailPath = "/Co_Apps/SurveyAdmin/navigator.aspx?SurveyID={0}&Action=surveyview"
        Return myHeader
    End Function

    Public Function GetSurveyResponseTableHeader() As WebProjectMechanics.DisplayTableHeader
        '  Dim myApplicationSurvey As New SurveyResponseItem
        Dim myHeader As New WebProjectMechanics.DisplayTableHeader
        myHeader.DetailFieldName = "SurveyResponseNM"
        myHeader.DetailKeyName = "SurveyResponseID"
        myHeader.DetailPath = "/Co_Apps/SurveyAdmin/navigator.aspx?SurveyResponseID={0}&Action=surveyresponseview"
        myHeader.AddHeaderItem("AssignedUserID", "AssignedUserID")
        myHeader.AddHeaderItem("SurveyResponseScore", "SurveyResponseScore")
        myHeader.AddHeaderItem("AverageQuestionScore", "AverageQuestionScore")
        myHeader.AddHeaderItem("AnswerCount", "AnswerCount", False)
        myHeader.AddHeaderItem("QuestionCount", "QuestionCount", False)
        myHeader.AddHeaderItem("StatusNM", "StatusNM", False)
        Return myHeader
    End Function

    Public Function ShortenNameBy(ByVal BreakString As String, ByVal myValue As String) As String
        Dim myTemp = myValue.Split(CChar(BreakString))
        Return myTemp(myTemp.Count - 1)
    End Function

End Class


