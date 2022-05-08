Imports System.Reflection
Imports DataGridVisualization.ControlOriginsWS
Imports DataGridVisualization

Public Class DataTableControlBase
    Inherits ApplicationControlBase

    Public Function AddComma(value As String) As String
        Dim mySB As New StringBuilder
        If value Is Nothing Then
            mySB.Append(String.Format("""{0}""", String.Empty))
        ElseIf IsNumeric(value) Then
            mySB.Append(String.Format("{0}", value))
        Else
            mySB.Append(String.Format("""{0}""", value.Replace(","c, " "c).Replace(""""c, "'"c)))
        End If
        Return mySB.ToString
    End Function
    Public Function GetHeaders(ByRef myrows As Object) As DisplayTableHeader
        Dim ReturnHeader As New DisplayTableHeader
        If IsNothing(myrows) Then
        Else
            Dim objType As Type
            Dim pInfo As PropertyInfo
            Dim PropValue As New Object
            For Each myItem In myrows
                Try
                    objType = myItem.GetType()
                    For Each pInfo In objType.GetProperties()
                        ReturnHeader.AddHeaderItem(pInfo.Name, pInfo.Name)
                    Next
                Catch ex As Exception
                    PropValue = String.Empty
                End Try
                Exit For
            Next
        End If
        Return ReturnHeader
    End Function
    Public Function GetPropertyValue(ByVal obj As Object, ByVal PropName As String) As Object
        Dim objType As Type
        Dim pInfo As PropertyInfo
        Dim PropValue As New Object
        If PropName.Contains(".") Then
            Dim PropertyNameArray = Split(PropName, ".")
            If PropertyNameArray.Count = 2 Then
                Try
                    If PropertyNameArray(1).ToLower = "count" Then
                        objType = obj.GetType()
                        For Each myProp In obj.GetType().GetProperties()
                            If myProp.Name = PropertyNameArray(0) Then
                                Dim x = myProp.GetValue(obj, Nothing)
                                PropValue = DirectCast(x, Object()).Length
                            End If
                        Next
                    Else
                        objType = obj.GetType()
                        pInfo = objType.GetProperty(PropertyNameArray(0))
                        PropValue = pInfo.GetValue(obj, Reflection.BindingFlags.GetProperty, Nothing, Nothing, Nothing)

                        objType = PropValue.GetType()
                        pInfo = objType.GetProperty(PropertyNameArray(1))
                        PropValue = pInfo.GetValue(PropValue, Reflection.BindingFlags.GetProperty, Nothing, Nothing, Nothing)
                    End If
                Catch ex As Exception
                    PropValue = String.Empty
                End Try
            End If
        Else
            Try
                objType = obj.GetType()
                pInfo = objType.GetProperty(PropName)
                PropValue = pInfo.GetValue(obj, Reflection.BindingFlags.GetProperty, Nothing, Nothing, Nothing)
            Catch ex As Exception
                PropValue = String.Empty
            End Try
            If PropValue Is Nothing Then
                PropValue = String.Empty
            End If
        End If
        Return PropValue
    End Function

End Class
