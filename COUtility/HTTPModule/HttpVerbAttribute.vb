
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Namespace COUtility

	<AttributeUsage(AttributeTargets.Method Or AttributeTargets.[Class])> _
	Public MustInherit Class HttpVerbAttribute
		Inherits Attribute

		Public MustOverride ReadOnly Property HttpVerb() As String

	End Class
    End Namespace