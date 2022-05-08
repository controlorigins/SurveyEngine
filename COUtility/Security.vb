'Imports System.IO
'Imports System.Security.Cryptography
'Imports System.Text
'Public Class Security
'    Protected Const STR_Key As String = "&/?@*>:>"

'    Public Function encrypt(ByVal strtext As String) As String
'        Return EncryptThis(strtext)
'    End Function

'    Public Function decrypt(ByVal strtext As String) As String
'        Return DecryptThis(strtext)
'    End Function

'    'The function used to encrypt the text
'    Private Function EncryptThis(ByVal strText As String) As String
'        Dim byKey() As Byte = {}
'        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
'        Try
'            byKey = Encoding.UTF8.GetBytes(Left(STR_Key, 8))
'            Dim des As New DESCryptoServiceProvider()
'            Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(strText)
'            Dim ms As New MemoryStream()
'            Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write)
'            cs.Write(inputByteArray, 0, inputByteArray.Length)
'            cs.FlushFinalBlock()
'            Return Convert.ToBase64String(ms.ToArray())
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function

'    'The function used to decrypt the text
'    Private Function DecryptThis(ByVal strText As String) As String
'        Dim byKey() As Byte = {}
'        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
'        Dim inputByteArray(strText.Length) As Byte

'        Try
'            Dim des As New DESCryptoServiceProvider()
'            Dim encoding As Encoding = encoding.UTF8
'            Dim ms As New MemoryStream()
'            byKey = encoding.UTF8.GetBytes(Left(STR_Key, 8))
'            Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)
'            inputByteArray = Convert.FromBase64String(strText)
'            cs.Write(inputByteArray, 0, inputByteArray.Length)
'            cs.FlushFinalBlock()
'            Return encoding.GetString(ms.ToArray())
'        Catch ex As Exception
'            Return ex.Message
'        End Try
'    End Function
'End Class