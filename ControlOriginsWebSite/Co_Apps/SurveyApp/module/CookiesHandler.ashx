<%@ WebHandler Language="VB" Class="Handler" %>

Imports System
Imports System.Web
Imports System.Web.Script.Serialization
Imports CODataCon
Imports System.IO
Imports System.Data
Imports DataGridVisualization.ControlOriginsWS
Imports ControlOrigins.COUtility

Public Class Handler
    Inherits BaseHandler
    
    Public Function UseCookie(cookie As String) As String
        Return context.Request.Cookies(cookie).Value
    End Function

    Public Function GreetMe(name As String) As Object
        Return String.Format("Hello {0}!", name)
    End Function

    Public Function TalkAboutMe(name As String, age As Integer) As Object
        Return String.Format("My name is {0} and I'm {1}!", name, age)
    End Function

    ''' <summary>
    ''' Return HTML instead of JSON
    ''' </summary>
    ''' <param name="text"></param>
    ''' <returns></returns>
    Public Function GiveMeSomeHTML(text As String) As Object
        Dim sb As New StringBuilder()
        sb.Append("<head><title>My Handler!</title></head>")
        sb.Append("<body>")
        sb.Append("<p>This is a HTML page returned from the Handler</p>")
        sb.AppendFormat("<p>The text passed was: {0}</p>", text)
        sb.Append("</body>")

        context.Response.ContentType = "text/html"
        SkipContentTypeEvaluation = True
        ' the handler won't try to identify the content type automatically
        SkipDefaultSerialization = True
        ' the handler won't serialize the result to JSON automatically
        Return sb.ToString()
    End Function

    Public Function AJAXSendIntArray(items As Integer()) As Object
        Dim jsonSer = New JavaScriptSerializer()
        Dim json As String = jsonSer.Serialize(items)
        Return json
    End Function

    Public Function GetSurveySummaryCSV() As Object
        Dim ApplicationID As Integer = AppUtility.GetDBInteger(context.Request("ApplicationID"))
        Dim ApplicationUserID As Integer = AppUtility.GetDBInteger(context.Request("ApplicationUserID"))
        If ApplicationID < 1 Then
            ApplicationID = 125
        End If
        Dim csv As New StringBuilder
        Dim myCon As New DataGridVisualization.DataController()
        Dim myGrid = myCon.GetSurveyResponseSummary(ApplicationID, ApplicationUserID)
        For Each column In myGrid.GridColumns
            'Add the Header row for CSV file.
            csv.Append(column.DisplayName + ","c)
        Next
        'Add new line.
        csv.Append(vbCr & vbLf)
 
        For Each row In myGrid.GridRows
            For Each myValue In row.Value
                'Add the Data rows.
                If IsNumeric(myValue) Then
                    csv.Append((AppUtility.GetDBString(myValue)).Replace(",", ";") + ","c)
                Else
                    csv.Append(""""c + (AppUtility.GetDBString(myValue)).Replace(",", ";") + """"c + ","c)
                End If
            Next
            'Add new line.
            csv.Append(vbCr & vbLf)
        Next
        'Download the CSV file.
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=SqlExport.csv")
        HttpContext.Current.Response.Charset = ""
        HttpContext.Current.Response.ContentType = "application/text"
        HttpContext.Current.Response.Output.Write(csv)
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.End()
        Return ""
    End Function

    Public Function GetSurveyGroupSummaryCSV() As Object
        Dim ApplicationID As Integer = context.Request("ApplicationID")
        Dim ApplicationUserID As Integer = AppUtility.GetDBInteger(context.Request("ApplicationUserID"))

        If ApplicationID < 1 Then
            ApplicationID = 125
        End If
        
        Dim csv As New StringBuilder
        
        Dim myCon As New DataGridVisualization.DataController()
        Dim myGrid = myCon.GetSurveyResponseGroupSummary(ApplicationID, ApplicationUserID)
        For Each column In myGrid.GridColumns
            'Add the Header row for CSV file.
            csv.Append(column.DisplayName + ","c)
        Next
        'Add new line.
        csv.Append(vbCr & vbLf)
 
        For Each row In myGrid.GridRows
            For Each myValue In row.Value
                'Add the Data rows.
                If IsNumeric(myValue) Then
                    csv.Append((AppUtility.GetDBString(myValue)).Replace(",", ";") + ","c)
                Else
                    csv.Append(""""c + (AppUtility.GetDBString(myValue)).Replace(",", ";") + """"c + ","c)
                End If
            Next
            'Add new line.
            csv.Append(vbCr & vbLf)
        Next

        'Download the CSV file.
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=SqlExport.csv")
        HttpContext.Current.Response.Charset = ""
        HttpContext.Current.Response.ContentType = "application/text"
        HttpContext.Current.Response.Output.Write(csv)
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.End()
        Return ""
    End Function

    
    
    Public Function GetSurveyAnswersCSV() As Object
        Dim ApplicationUserID As Integer = AppUtility.GetDBInteger(context.Request("ApplicationUserID"))
        Dim ApplicationID As Integer = context.Request("ApplicationID")
        If ApplicationID < 1 Then
            ApplicationID = 125
        End If
        
        Dim csv As New StringBuilder
        Dim myGrid As New CO_DataGrid
        Dim myCon As New DataGridVisualization.DataController()
        myGrid = myCon.GetSurveyResponseAnswers(ApplicationID, ApplicationUserID)
       
        For Each column In myGrid.GridColumns
            'Add the Header row for CSV file.
            csv.Append(column.DisplayName + ","c)
        Next
        'Add new line.
        csv.Append(vbCr & vbLf)
 
        For Each row In myGrid.GridRows
            For Each myValue In row.Value
                'Add the Data rows.
                If IsNumeric(myValue) Then
                    csv.Append((AppUtility.GetDBString(myValue)).Replace(",", ";") + ","c)
                Else
                    csv.Append(""""c + (AppUtility.GetDBString(myValue)).Replace(",", ";") + """"c + ","c)
                End If
            Next
            'Add new line.
            csv.Append(vbCr & vbLf)
        Next

        'Download the CSV file.
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=SqlExport.csv")
        HttpContext.Current.Response.Charset = ""
        HttpContext.Current.Response.ContentType = "application/text"
        HttpContext.Current.Response.Output.Write(csv)
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.End()
        Return ""
    End Function

    Public Function PostData() As Object
        Return "Here's your POST response."
    End Function

    Public Function PutData() As Object
        Return "Here's your PUT response."
    End Function

    Public Function DeleteData() As Object
        Return "Here's your DELETE response."
    End Function

    Public Function PostOrPutData() As Object
        Return "Here's your POST or PUT response."
    End Function
    
    
End Class