
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Namespace COUtility

	Public Class HttpPostAttribute
		Inherits HttpVerbAttribute
		Public Overrides ReadOnly Property HttpVerb() As String
			Get
				Return "POST"
			End Get
		End Property
	End Class
    End Namespace