Imports ControlOrigins.COUtility

Public Interface ISurveyStatusLookupList
    WriteOnly Property LookupList As List(Of LookupItem)
    ReadOnly Property Value As Integer
End Interface
