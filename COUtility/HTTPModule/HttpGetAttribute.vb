
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Namespace COUtility

    Public Class HttpGetAttribute
        Inherits HttpVerbAttribute
        Public Overrides ReadOnly Property HttpVerb() As String
            Get
                Return "GET"
            End Get
        End Property
    End Class
End Namespace