
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Namespace COUtility

	Public Class HttpVerbNotAllowedException
		Inherits Exception
		Public Sub New()
			MyBase.New("The operation does not support the request HTTP verb.")
		End Sub
	End Class
End Namespace