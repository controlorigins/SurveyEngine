Namespace COUtility

Public Class LogHelper
    ' Constants
    Public Shared AreaName As String = "SPSurvey SurveyTimer Timer"
    Public Enum CategoryID
        None = 0
        Processing = 100
        Faulting = 200
    End Enum

    ' Constructors
    Public Sub New()

    End Sub

    ' Properties
    Private Shared m_local As LogHelper
    Public Shared ReadOnly Property Local() As LogHelper
        Get
            If True Then
                If m_local Is Nothing Then
                    m_local = New LogHelper()
                End If
            End If
            Return m_local
        End Get
    End Property
    ' Methods

    Public Shared Sub Log(ex As Exception, errorMessage As String)
    End Sub
    Public Shared Sub LogError(errorMessage As String)
    End Sub

    Public Shared Sub LogInfo(infoMessage As String)
    End Sub
End Class

End Namespace