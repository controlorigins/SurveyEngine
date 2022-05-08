Imports CODataCon.com.controlorigins.ws
Imports CODataCon


Public Class SurveyResponseFilter
    Inherits SurveyUserControlBase

    Implements IManagerList
    Implements ISurveyStatusLookupList

    Private mypresenter As New SurveyResponseUI()

    Private myFilterList As New List(Of SQLFilterClause)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        mypresenter.SetMangerListUI(Me)
        If Not IsPostBack Then
            mypresenter.GetSurveyResponseStatusList()
            Page_SurveyListUpdated()
        End If

    End Sub

    Public Function GetSurveyID() As String
        If ddlSurvey.SelectedValue <> String.Empty Then
            Return ddlSurvey.SelectedValue
        Else
            Return "-1"
        End If
    End Function

    Public Function GetSurveyStatusID() As String
        If ddlSurveyResponseStatus.SelectedValue <> String.Empty Then
            Return ddlSurveyResponseStatus.SelectedValue
        Else
            Return "-1"
        End If
    End Function

    Private Function GetSQLFilterList(ByVal ApplicationID As Integer) As list (of SQLFilterClause)
                                                                               
        myFilterList.Clear()
        'myFilterList.Add(New SQLFilterClause("ApplicationID", SQLFilterOperator.Equal, ApplicationID.ToString(), SQLFilterConjunction.andConjunction, "Survey"))

        'If tbVariantCount.Text <> String.Empty Then
        '    myFilterList.Add(New SQLFilterClause(String.Empty, SQLFilterOperator.Equal, String.Format("VariantCount {0}", tbVariantCount.Text), SQLFilterConjunction.andConjunction, "Survey"))
        'End If
        'If tbACECount.Text <> String.Empty Then
        '    myFilterList.Add(New SQLFilterClause ( String.Empty, SQLFilterOperator.Equal, String.Format("ComplianceReview {0}", tbACECount.Text), SQLFilterConjunction.andConjunction, "Survey"))
        'End If
        'If tbDaysSinceModified.Text <> String.Empty Then
        '    myFilterList.Add(New co_SQLFilterClause(String.Empty, SQLFilterOperator.Equal, String.Format("DaySinceModified {0}", tbDaysSinceModified.Text), SQLFilterConjunction.andConjunction, "Survey"))
        'End If
        'If tbFirstName.Text <> String.Empty Then
        '    myFilterList.Add(New co_SQLFilterClause("FIRST_NAME", SQLFilterOperator.dbLike, tbFirstName.Text, SQLFilterConjunction.andConjunction, "User"))
        'End If
        'If tbLastName.Text <> String.Empty Then
        '    myFilterList.Add(New co_SQLFilterClause("LAST_NAME", SQLFilterOperator.dbLike, tbLastName.Text, SQLFilterConjunction.andConjunction, "User"))
        'End If
        'If tbManagerName.Text <> String.Empty Then
        '    myFilterList.Add(New co_SQLFilterClause("MANAGER_NAME", SQLFilterOperator.dbLike, tbManagerName.Text, SQLFilterConjunction.andConjunction, "User"))
        'End If
        'If ddlSurveyResponseStatus.SelectedValue <> String.Empty Then
        '    myFilterList.Add(GetControlSQLFilterClause(ddlSurveyResponseStatus, "Survey", "StatusID"))
        'End If
        ''If ddlManager.SelectedValue <> String.Empty Then
        ''    myFilterList.Add(New SQLFilterClause(String.Empty, SQLFilterOperator.Equal, ddlManager.SelectedValue, SQLFilterConjunction.andConjunction, "User"))
        ''End If
        'If ddlSurvey.SelectedValue <> String.Empty Then
        '    myFilterList.Add(GetControlSQLFilterClause(ddlSurvey, "Survey", "SurveyID"))
        'End If
        Return myFilterList
    End Function
#Region "Interface Implementation"


    Public WriteOnly Property LookupList As List(Of LookupItem) Implements IManagerList.LookupList
        Set(value As List(Of LookupItem))
        End Set
    End Property

    Public ReadOnly Property Value As String Implements IManagerList.Value
        Get
            Return String.Empty
        End Get
    End Property

    Public WriteOnly Property LookupList12 As List(Of LookupItem) Implements ISurveyStatusLookupList.LookupList
        Set(value As List(Of LookupItem))
            LoadDropDown(ddlSurveyResponseStatus, value)
        End Set
    End Property

    Public ReadOnly Property Value12 As Integer Implements ISurveyStatusLookupList.Value
        Get
            Return CInt(ddlSurveyResponseStatus.SelectedValue)
        End Get
    End Property
#End Region

    Protected Sub Page_SurveyListUpdated() Handles Me.SurveyListUpdated
        LoadSurveyDropDown(ddlSurvey, _SurveyList)
    End Sub
End Class
