Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Web
Imports System.IO
Imports System.Web.UI
Imports ControlOrigins.COUtility

Public NotInheritable Class AppUtility
    Public Const MsgConfirmDeleteSitePropertyFormatString As String = "return confirm('{0}');"
    Public Const PageNameManageSiteProperties As String = "default.aspx"
    Public Const QueryKeyParamProperty As String = "property"
    Public Const QueryKeyParamSource As String = "Source"

    Public Shared Function GetAddSitePropertyUrl() As String
        Return [String].Format("SPAddSiteProperty.aspx?{0}={1}", QueryKeyParamSource, PageNameManageSiteProperties)
    End Function

    Public Shared Function GetEditSitePropertyUrl(propertyNameUrlEncoded As String) As String
        Return [String].Format("EditSiteProperty.aspx?{0}={1}&{2}={3}", QueryKeyParamProperty, propertyNameUrlEncoded, QueryKeyParamSource, PageNameManageSiteProperties)
    End Function

    Public Shared Sub ModalDialogClose(returnValue As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        page.Response.Clear()
        page.Response.Write([String].Format("<script language=""javascript"" type=""text/javascript"">window.frameElement.commonModalDialogClose(1, ""{0}"");</script>", returnValue))
        page.Response.[End]()
    End Sub

    Public Shared Function CheckForMatch(ByVal StringOne As String, ByVal StringTwo As String) As Boolean
        Dim bMatch As Boolean = False
        If StringOne Is Nothing Or StringTwo Is Nothing Then
            If StringOne Is Nothing And StringTwo Is Nothing Then
                bMatch = True
            End If
        Else
            ' To Make this Easier, let's ignore case and spaces and ampersands and dashes
            StringOne = (StringOne.ToLower)
            StringOne = Replace(StringOne, "/", "")
            StringOne = Replace(StringOne, ".html", "")
            StringOne = Replace(StringOne, ".htm", "")
            StringOne = Replace(StringOne, "&amp;", "&")
            StringOne = Replace(StringOne, "%20", "")
            StringOne = Replace(StringOne, "-", "")
            StringOne = Replace(StringOne, " ", "")
            StringTwo = (StringTwo.ToLower)
            StringTwo = Replace(StringTwo, "/", "")
            StringTwo = Replace(StringTwo, ".html", "")
            StringTwo = Replace(StringTwo, ".htm", "")
            StringTwo = Replace(StringTwo, "%20", "")
            StringTwo = Replace(StringTwo, " ", "")
            StringTwo = Replace(StringTwo, "&amp;", "&")
            StringTwo = Replace(StringTwo, "-", "")
            If (StringOne = StringTwo) Then
                bMatch = True
            Else
                bMatch = False
            End If
        End If
        Return bMatch
    End Function
    Public Shared Function ConvertHTMLToRichText(ByVal sTextToCovert As String) As String
        Dim sbReturn As New StringBuilder(sTextToCovert)
        Return (sbReturn.ToString)
    End Function

    Public Shared Function ConvertRichTextToHTML(ByVal sTextToCovert As String) As String
        Dim sbReturn As New StringBuilder(sTextToCovert)
        sbReturn.Replace("'", "&quot;")
        sbReturn.Replace("  ", " &nbsp;")
        sbReturn.Replace(vbCrLf, "<br/>")
        sbReturn.Replace(vbLf, "<br/>")
        Return (sbReturn.ToString)
    End Function
    Public Shared Function ClearLineFeeds(ByVal sTextToCovert As String) As String
        sTextToCovert = sTextToCovert.Replace(vbCrLf, " ")
        sTextToCovert = sTextToCovert.Replace(vbLf, " ")
        Return sTextToCovert
    End Function

    Public Shared Function RemoveInvalidCharacters(ByVal Value As String) As String
        If Value Is Nothing Then
            Value = String.Empty
        Else
            Value = (Trim(Value.ToLower))
            Value = Replace(Value, " & ", "_and_")
            Value = Replace(Value, "&", "_and_")
            Value = Replace(Value, " | ", String.Empty)
            Value = Replace(Value, "|", String.Empty)
            Value = Replace(Value, ",", String.Empty)
            Value = Replace(Value, "/", String.Empty)
            Value = Replace(Value, "\", String.Empty)
            Value = Replace(Value, "<", String.Empty)
            Value = Replace(Value, ">", String.Empty)
            Value = Replace(Value, "(", String.Empty)
            Value = Replace(Value, ")", String.Empty)
            Value = Replace(Value, ",", String.Empty)
            Value = Replace(Value, "'", String.Empty)
            Value = Replace(Value, ";", String.Empty)
            Value = Replace(Value, ":", String.Empty)
            Value = Replace(Value, " ", "_")
            Value = Replace(Value, "?", String.Empty)
            Value = Replace(Value, "%22", String.Empty)
            Value = Replace(Value, "=", String.Empty)
            Value = Replace(Value, "%", "_pct")
        End If
        Return Value
    End Function
    Public Shared Function FixInvalidCharacters(ByVal Value As String) As String
        Value = (Trim(Value.ToLower))
        Value = Replace(Value, " & ", "-and-")
        Value = Replace(Value, "&", "-and-")
        Value = Replace(Value, " | ", "-and-")
        Value = Replace(Value, "|", "-and-")
        Value = Replace(Value, ",", "-")
        Value = Replace(Value, "/", "-")
        Value = Replace(Value, "\", "-")
        Value = Replace(Value, "<", "-")
        Value = Replace(Value, ">", "-")
        Value = Replace(Value, "(", "-")
        Value = Replace(Value, ")", "-")
        Value = Replace(Value, ",", "-")
        Value = Replace(Value, "'", "-")
        Value = Replace(Value, ";", "-")
        Value = Replace(Value, ":", "-")
        Value = Replace(Value, " ", "-")
        Return Value
    End Function
    Public Shared Function RemoveHtml(ByVal sContent As String) As String
        Return Regex.Replace(sContent, "<[^>]*>", "")
    End Function
    Public Shared Function RemoveTags(ByVal sContent As String) As String
        Return Regex.Replace(sContent, "~~(.|\n)+?~~", "")
    End Function
    Public Shared Function StripTags(ByVal html As String) As String
        Return Regex.Replace(html, "<.*?>", "")
    End Function

    Public Shared Function FormatNameForURL(ByVal Name As String) As String
        Name = (Replace(Name.ToLower, " ", "-"))
        Name = Replace(Name, "(", "-")
        Name = Replace(Name, ")", "-")
        Return Name
        ' Return FixInvalidCharacters(Name)
    End Function
    Public Shared Function ApplyHTMLFormatting(ByVal strInput As String) As String
        strInput = "~" & strInput
        strInput = Replace(strInput, ",", "-")
        strInput = Replace(strInput, "'", "&quot;")
        strInput = Replace(strInput, """", "&quot;")
        strInput = Replace(strInput, "~", "")
        '  strInput = Replace(strInput, " " , "_")
        ApplyHTMLFormatting = strInput
    End Function
    '********************************************************************************
    Public Shared Function GetDBByte(ByVal dbobject As Data.Linq.Binary) As Byte()
        Dim myReturn() As Byte = New Byte(0) {}
        Try
            Return dbobject.ToArray()
        Catch ex As Exception
            Return myReturn
        End Try

    End Function

    'Public Shared Function GetDBByte(ByVal dbobject As Object) As Byte()
    '    Dim myReturn() As Byte = New Byte(0) {}
    '    Try
    '        Return CType(dbobject, Byte())
    '    Catch ex As Exception
    '        Return myReturn
    '    End Try
    'End Function
    Public Shared Function GetDBString(ByVal dbObject As Object) As String
        Dim strEntry As String = String.Empty
        Try
            If Not (IsDBNull(dbObject) Or dbObject Is Nothing) Then
                strEntry = CStr(dbObject)
                If strEntry = " " Then strEntry = String.Empty
            End If
        Catch ex As Exception
            App.AddMessage(ex, "GetDBString")
        End Try
        Return strEntry
    End Function
    Public Shared Function GetStringValue(ByVal myString As String) As String
        If (IsDBNull(myString) Or myString Is Nothing) Then
            myString = String.Empty
        End If
        Return myString
    End Function

    '********************************************************************************
    Public Shared Function GetDBDate(ByVal dbObject As Object) As Date
        If IsDBNull(dbObject) Then
            Return Now.AddDays(-365)
        Else
            Return CDate(dbObject)
        End If
    End Function
    Public Shared Function GetDBDecimal(ByVal dbObject As Object) As Decimal
        If IsDBNull(dbObject) Then
            Return CType(0, Decimal)
        ElseIf dbObject Is Nothing Then
            Return CType(0, Decimal)
        Else
            Return CDec(dbObject)
        End If
    End Function
    Public Shared Function GetDBDouble(ByVal dbObject As Object) As Double
        If IsDBNull(dbObject) Then
            Return CType(0, Double)
        ElseIf dbObject Is Nothing Then
            Return CType(0, Double)
        Else
            Return CDbl(dbObject)
        End If
    End Function
    Public Shared Function GetDBInteger(ByVal dbObject As Object) As Integer
        If IsDBNull(dbObject) Then
            Return CType(0, Integer)
        ElseIf dbObject Is Nothing Then
            Return CType(0, Integer)
        Else
            If dbObject.ToString = String.Empty Then
                Return CType(0, Integer)
            End If
            Return CInt(dbObject)
        End If
    End Function
    Public Shared Function GetDBInteger(ByVal dbObject As Object, ByVal DefaultValue As Integer) As Integer
        If IsDBNull(dbObject) Then
            Return DefaultValue
        ElseIf dbObject Is Nothing Then
            Return DefaultValue
        Else
            Return CInt(dbObject)
        End If
    End Function
    Public Shared Function GetDBBoolean(ByVal dbObject As Object) As Boolean
        If IsDBNull(dbObject) Then
            Return False
        Else
            Return CBool(dbObject)
        End If
    End Function
    Public Shared Function GetDBBoolean(ByVal dbObject As Object, ByVal DefaultValue As Boolean) As Boolean
        If IsDBNull(dbObject) Then
            Return DefaultValue
        Else
            Return CBool(dbObject)
        End If
    End Function
    Public Shared Function FormatPageNameForURL(ByVal PageName As String) As String
        If PageName Is Nothing Then
            Return Nothing
        Else
            Return (FixInvalidCharacters(PageName.ToLower))
        End If
    End Function
    ' ****************************************************************************
    Public Shared Function LeadingZero(ByRef pStrValue As String) As String
        If Len(CStr(pStrValue)) < 2 Then pStrValue = "0" & pStrValue
        Return pStrValue
    End Function

    Public Shared Function fncGetDayOrdinal(ByVal intDay As Integer) As String
        ' Accepts a day of the month as an integer and returns the
        ' appropriate suffix
        Dim strOrd As String = ("")
        Select Case intDay
            Case 1, 21, 31
                strOrd = "st"
            Case 2, 22
                strOrd = "nd"
            Case 3, 23
                strOrd = "rd"
            Case Else
                strOrd = "th"
        End Select
        Return strOrd
    End Function ' fncGetDayOrdinal


    Public Shared Function GetRFC822Date(ByVal dbObject As Object) As String
        Dim offset As Integer = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours
        Dim timeZone__1 As String = "+" & offset.ToString().PadLeft(2, "0"c)
        Dim myDate As Date
        If IsDBNull(dbObject) Then
            myDate = Now.AddDays(-100)
        Else
            myDate = CDate(dbObject)
        End If

        If offset < 0 Then
            Dim i As Integer = offset * -1
            timeZone__1 = "-" & i.ToString().PadLeft(2, "0"c)
        End If
        Return myDate.ToString("ddd, dd MMM yyyy HH:mm:ss " & timeZone__1.PadRight(5, "0"c))
    End Function

    Public Shared Function fncFmtDate(ByVal strDate As String, ByRef strFormat As String) As String
        ' Accepts strDate as a valid date/time,
        ' strFormat as the output template.
        ' The function finds each item in the
        ' template and replaces it with the
        ' relevant information extracted from strDate

        ' Template items (example)
        ' %m Month as a decimal (02)
        ' %B Full month name (February)
        ' %b Abbreviated month name (Feb )
        ' %d Day of the month (23)
        ' %O Ordinal of day of month (eg st or rd or nd)
        ' %j Day of the year (54)
        ' %Y Year with century (1998)
        ' %y Year without century (98)
        ' %w Weekday as integer (0 is Sunday)
        ' %a Abbreviated day name (Fri)
        ' %A Weekday Name (Friday)
        ' %H Hour in 24 hour format (24)
        ' %h Hour in 12 hour format (12)
        ' %N Minute as an integer (01)
        ' %n Minute as optional if minute <> 0
        ' %S Second as an integer (55)
        ' %P AM/PM Indicator (PM)

        On Error Resume Next
        Dim mySB As New StringBuilder(strFormat)
        Dim int12HourPart As New Integer
        Dim str24HourPart As String = ("")
        Dim strMinutePart As String = ("")
        Dim strSecondPart As String = ("")
        Dim strAMPM As String = ("")

        ' Insert Month Numbers
        mySB.Replace("%m", DatePart("m", strDate).ToString)

        ' Insert non-Abbreviated Month Names
        mySB.Replace("%B", MonthName(DatePart("m", strDate), False))

        ' Insert Abbreviated Month Names
        mySB.Replace("%b", MonthName(DatePart("m", strDate), True))

        ' Insert Day Of Month
        mySB.Replace("%d", DatePart("d", strDate).ToString)

        ' Insert Day of Month Ordinal (eg st, th, or rd)
        mySB.Replace("%O", fncGetDayOrdinal(Day(CDate(strDate))))

        ' Insert Day of Year
        mySB.Replace("%j", DatePart("y", CDate(strDate)).ToString)

        ' Insert Long Year (4 digit)
        mySB.Replace("%Y", DatePart("yyyy", CDate(strDate)).ToString)

        ' Insert Short Year (2 digit)
        mySB.Replace("%y", Right(DatePart("yyyy", CDate(strDate)).ToString, 2))

        ' Insert Weekday as Integer (eg 0 = Sunday)
        mySB.Replace("%w", DatePart("w", CDate(strDate), FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1).ToString)

        ' Insert Abbreviated Weekday Name (eg Sun)
        mySB.Replace("%a", WeekdayName(DatePart("w", strDate, FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1), True))

        ' Insert non-Abbreviated Weekday Name
        mySB.Replace("%A", WeekdayName(DatePart("w", strDate, FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1), False))

        ' Insert Hour in 24hr format
        str24HourPart = DatePart("h", CDate(strDate)).ToString
        If Len(str24HourPart) < 2 Then
            str24HourPart = "0" & str24HourPart
        End If
        mySB.Replace("%H", str24HourPart)

        ' Insert Hour in 12hr format
        int12HourPart = DatePart("h", strDate) Mod 12
        If int12HourPart = 0 Then
            int12HourPart = 12
        End If
        mySB.Replace("%h", int12HourPart.ToString)

        ' Insert Minutes
        strMinutePart = DatePart("n", CDate(strDate)).ToString
        If Len(strMinutePart) < 2 Then
            strMinutePart = "0" & strMinutePart
        End If
        mySB.Replace("%N", strMinutePart)

        ' Insert Optional Minutes
        If CInt(strMinutePart) = 0 Then
            mySB.Replace("%n", "")
        Else
            If CInt(strMinutePart) < 10 Then
                strMinutePart = "0" & strMinutePart
            End If
            strMinutePart = ":" & strMinutePart
            mySB.Replace("%n", strMinutePart)
        End If

        ' Insert Seconds
        strSecondPart = DatePart("s", CDate(strDate)).ToString
        If Len(strSecondPart) < 2 Then _
                 strSecondPart = "0" & strSecondPart
        mySB.Replace("%S", strSecondPart)

        ' Insert AM/PM indicator
        If DatePart("h", strDate) >= 12 Then
            strAMPM = "PM"
        Else
            strAMPM = "AM"
        End If

        mySB.Replace("%P", strAMPM)

        'If there is an error output its value
        If Err.Number <> 0 Then
            mySB.Append("ERROR in fncFmtDate: " & Err.Description)
        End If
        Return mySB.ToString
    End Function ' fncFmtDate
    Public Shared Function IsBreak(ByVal hInput As Object, ByVal hBreakPoint As Double) As Boolean
        ' remove anything after the decimal point and the decimal point if found
        ' because we need a whole number to use Mod
        If InStr(hInput.ToString, ".") <> 0 Then hInput = Left(hInput.ToString, InStrRev(hInput.ToString, ".") - 1)
        ' If not numeric, exit with a Null value
        If Not IsNumeric(hInput) Then
            IsBreak = False
            Exit Function
        End If
        ' Add one to adjust for starting at zero with arrays
        ' zero would always be the break point
        hInput = CInt(hInput) + 1
        ' use the Mod function to get the remainder of dividing the input by the break point. If it's 0, the
        ' number is we are NOT at a break point, If it's 1, the we ARE at a break point.
        If CBool(CInt(hInput) Mod hBreakPoint) Then
            IsBreak = False
        Else
            IsBreak = True
        End If
    End Function



    Public Shared Function BuildGraphicLink(ByVal LinkURL As String, ByVal sImage As String) As String
        Return String.Format("<a href=""{0}""><img alt=""{1}"" border=0 src=""/images/{1}.gif""  /></a>", LinkURL, sImage)
    End Function
    Public Shared Function FormatLink(ByVal LinkName As String, ByVal LinkType As String, ByVal LinkURL As String) As String
        Dim sReturn As String
        Select Case LinkType
            Case "Contact"
                sReturn = LinkName
            Case Else
                sReturn = BuildLink(LinkName, LinkURL)
        End Select
        Return sReturn
    End Function
    Public Shared Function BuildLayoutsLink(ByVal sTitle As String, ByVal sURL As String) As String
        Return String.Format("<a href=""{1}"" target=""layouts"">{0}</a>", sTitle, sURL)
    End Function
    Public Shared Function BuildLayoutsLink(ByVal sTitle As String, ByVal sURL As String, ByVal sComment As String) As String
        Return String.Format("<a href=""{1}"" target=""layouts"">{0}</a><blockquote>{2}</blockquote>", sTitle, sURL, sComment)
    End Function
    Public Shared Function BuildLink(ByVal sTitle As String, ByVal sURL As String) As String
        Return String.Format("<a href=""{1}"" target=""_new"">{0}</a>", sTitle, sURL)
    End Function
    Public Shared Function BuildListItemLink(ByVal sTitle As String, ByVal sURL As String) As String
        Return String.Format("<li><a href=""{1}"" target=""_new"">{0}</a></li>", sTitle, sURL)
    End Function
    Public Shared Function BuildLink(ByVal sTitle As String, ByVal sURL As String, ByVal sDescription As String) As String
        Return String.Format("<a href=""{0}"" title=""{1}"">{2}</a>", sURL, sDescription, sTitle)
    End Function
    Public Shared Function FormatLink(ByVal LinkName As String, ByVal LinkType As String, ByVal LinkURL As String, ByVal LinkDescription As String) As String
        Dim sReturn As String
        Select Case LinkType
            Case "Contact"
                sReturn = LinkName
            Case Else
                sReturn = BuildLink(LinkName, LinkURL, LinkDescription)
        End Select
        Return sReturn
    End Function
    Public Shared Function FormatPageNameLink(ByVal sPageName As String) As String
        Dim sReturn As String = ""
        If Trim(sPageName) = "" Then
            sReturn = BuildLink("Home Page", "/")
        Else
            sReturn = BuildLink(sPageName, FormatPageNameURL(sPageName))
        End If
        Return sReturn.ToLower
    End Function
    Public Shared Function FormatPageNameURL(ByVal sPageName As String) As String
        If Trim(sPageName) = "" Then
            Return "/"
        Else
            Return String.Format("/{0}", FixInvalidCharacters(sPageName))
        End If
    End Function
    Public Shared Function FormatTextEntry(ByVal strEntry As String) As String
        If strEntry = "" Then strEntry = " "
        strEntry = Replace(strEntry, "'", "&#39;")
        Return strEntry
    End Function
    Public Shared Function InternetTime() As String
        Dim lLngTime As Single = (Hour(TimeOfDay) * 3600 * 1000) + (Minute(TimeOfDay) * 60 * 1000) + (Second(TimeOfDay) * 1000 + 3600000)
        Dim lLngBeats As Single = lLngTime / 86400
        Dim lLngBeatsRound As Double = Math.Round(lLngBeats)
        If lLngBeatsRound > 1000 Then
            InternetTime = "@0" & lLngBeatsRound
        Else
            InternetTime = "@" & lLngBeatsRound
        End If
        If lLngBeatsRound > 100 Then InternetTime = "@0" & lLngBeatsRound
    End Function ' InternetTime()
    Public Shared Function LeadingZero(ByRef pStrValue As Integer) As String
        If Len(CStr(pStrValue)) < 2 Then
            Return "0" & pStrValue.ToString
        Else
            Return pStrValue.ToString
        End If
    End Function ' LeadingZero(ByRef pStrValue)
    Public Shared Function IsLeap() As Boolean
        Dim lLngYear As Integer = Year(Now)
        If (lLngYear Mod 4 = 0) And (lLngYear Mod 100 <> 0) Or (lLngYear Mod 400 = 0) Then
            Return True
        Else
            Return False
        End If
    End Function ' IsLeap()
    Public Shared Function DaysInMonth() As String
        Dim lLngMonth As Integer = Month(Now)
        Select Case lLngMonth
            Case 9, 4, 6, 11
                DaysInMonth = "30"
            Case 2
                If CBool(IsLeap()) Then DaysInMonth = "29" Else DaysInMonth = "28"
            Case Else
                DaysInMonth = "31"
        End Select
    End Function ' DaysInMonth()
    Public Shared Function FormatHour() As Integer
        Dim lDtmNow As String = FormatDateTime(Now, DateFormat.LongTime)
        Return CInt(Left(lDtmNow, InStr(lDtmNow, ":") - 1))
    End Function ' FormatHour()
    Public Shared Function OrdinalSuffix() As String
        Dim lLngDay As Integer = Day(Now)
        If Right(lLngDay.ToString, 1) = "1" Then
            OrdinalSuffix = "st"
        Else
            OrdinalSuffix = "th"
        End If
        If Right(lLngDay.ToString, 1) = "2" Then OrdinalSuffix = "nd"
        If Right(lLngDay.ToString, 1) = "3" Then OrdinalSuffix = "rd"
    End Function ' OrdinalSuffix()
    Public Shared Function GetCurrentDate() As String
        Dim dRightNow As DateTime = DateTime.Now
        GetCurrentDate = dRightNow.ToLongDateString
    End Function
    Public Shared Function FormatDate(ByVal lDtmNow As Date, ByRef pStrDate As String) As String
        ' Define local variables
        Dim lObjRegExp As Regex = New Regex("([a-z][a-z]*[a-z])*[a-z]", RegexOptions.IgnoreCase)
        Dim lLngHour As Integer = Hour(lDtmNow)
        Dim lLngWeekDay As Integer = Weekday(lDtmNow)
        Dim lLngSecond As Integer = Second(lDtmNow)
        Dim lLngMinute As Integer = Minute(lDtmNow)
        Dim lLngDay As Integer = Day(lDtmNow)
        Dim lLngMonth As Integer = Month(lDtmNow)
        Dim lLngYear As Integer = Year(lDtmNow)

        ' Prepare RegExp object and set parameters

        ' List each individual match and compare to different date functoids
        For Each lObjMatch As Match In lObjRegExp.Matches(pStrDate)
            Select Case lObjMatch.Value
                Case "a"
                    pStrDate = Replace(pStrDate, "a", (Right(lDtmNow.ToString.ToLower, 2)))
                Case "A"
                    pStrDate = Replace(pStrDate, "A", (Right(lDtmNow.ToString.ToUpper, 2)))
                Case "B"
                    pStrDate = Replace(pStrDate, "B", InternetTime())
                Case "d"
                    pStrDate = Replace(pStrDate, "d", LeadingZero(lLngDay))
                Case "D"
                    pStrDate = Replace(pStrDate, "D", Left(WeekdayName(lLngWeekDay), 3))
                Case "M"
                    pStrDate = Replace(pStrDate, "M", MonthName(lLngMonth))
                Case "g"
                    pStrDate = Replace(pStrDate, "g", FormatHour().ToString)
                Case "G"
                    pStrDate = Replace(pStrDate, "G", lLngHour.ToString)
                Case "h"
                    pStrDate = Replace(pStrDate, "h", LeadingZero(FormatHour()))
                Case "H"
                    pStrDate = Replace(pStrDate, "H", LeadingZero(lLngHour))
                Case "i"
                    pStrDate = Replace(pStrDate, "i", LeadingZero(lLngMinute))
                Case "j"
                    pStrDate = Replace(pStrDate, "j", lLngDay.ToString)
                Case "l"
                    pStrDate = Replace(pStrDate, "l", WeekdayName(lLngWeekDay))
                Case "L"
                    pStrDate = Replace(pStrDate, "L", IsLeap().ToString)
                Case "m"
                    pStrDate = Replace(pStrDate, "m", LeadingZero(lLngMonth))
                Case "M"
                    pStrDate = Replace(pStrDate, "M", Left(MonthName(lLngMonth), 3))
                Case "n"
                    pStrDate = Replace(pStrDate, "n", lLngMonth.ToString)
                Case "r"
                    pStrDate = Replace(pStrDate, "r", String.Format("{0}, {1} {2} {3} {4}:{5}", Left(WeekdayName(lLngWeekDay), 3), lLngDay, Left(MonthName(lLngMonth), 3), lLngYear, FormatDateTime(TimeOfDay, DateFormat.LongTime), LeadingZero(lLngSecond)))
                Case "s"
                    pStrDate = Replace(pStrDate, "s", LeadingZero(lLngSecond))
                Case "S"
                    pStrDate = Replace(pStrDate, "S", OrdinalSuffix())
                Case "t"
                    pStrDate = Replace(pStrDate, "t", DaysInMonth())
                Case "U"
                    pStrDate = Replace(pStrDate, "U", CStr(DateDiff(DateInterval.Second, DateSerial(1970, 1, 1), lDtmNow)))
                Case "w"
                    pStrDate = Replace(pStrDate, "w", CStr(lLngWeekDay - 1))
                Case "W"
                    pStrDate = Replace(pStrDate, "W", "1")
                Case "Y"
                    pStrDate = Replace(pStrDate, "Y", lLngYear.ToString)
                Case "y"
                    pStrDate = Replace(pStrDate, "y", Right(lLngYear.ToString, 2))
                Case "z"
                    pStrDate = Replace(pStrDate, "z", "1")
                Case Else
                    pStrDate = pStrDate & ""
            End Select
        Next lObjMatch
        lObjRegExp = Nothing
        FormatDate = pStrDate
    End Function
    Public Shared Function IsDate(ByVal strDate As String) As Boolean
        Dim dtDate As DateTime
        Dim bValid As Boolean = True
        Try
            dtDate = DateTime.Parse(strDate)
        Catch eFormatException As FormatException
            ' the Parse method failed => the string strDate cannot be converted to a date.
            bValid = False
        End Try
        Return bValid
    End Function

    Public Shared Function Build301Redirect(ByVal sNewURL As String) As Boolean
        HttpContext.Current.Response.Status = "301 Moved Permanently"
        HttpContext.Current.Response.AddHeader("Location", sNewURL)
        Return True
    End Function
    Public Shared Function HexStringToColor(ByVal hex As String) As System.Drawing.Color
        hex = hex.Replace("#", "")
        If hex.Length <> 6 Then
            Throw New Exception(hex & " is not a valid 6-place hexadecimal color code.")
        End If
        Return System.Drawing.Color.FromArgb(HexStringToBase10Int(hex.Substring(0, 2)), HexStringToBase10Int(hex.Substring(2, 2)), HexStringToBase10Int(hex.Substring(4, 2)))
    End Function
    Public Shared Function HexStringToBase10Int(ByVal hex As String) As Integer
        Dim base10value As Integer = 0
        Try
            base10value = Convert.ToInt32(hex, 16)
        Catch
            base10value = 0
        End Try
        Return base10value
    End Function


    Public Shared Function GetDirectoryMenuHTML(ByVal sPath As String) As String
        Dim iMenuItemCount As Integer
        Dim mysb As New StringBuilder
        mysb.Append(String.Format("<div class=""{0}""><ul class=""{0}"">", "DirectoryMenu"))
        ' mysb.Append(String.Format("<li><a href=""{0}"">{1}</a></li>", sPath, sPath.Replace("/", "")))
        For Each baseDir As DirectoryInfo In My.Computer.FileSystem.GetDirectoryInfo(HttpContext.Current.Server.MapPath(sPath)).GetDirectories
            If Not (baseDir.Extension.ToLower = ".svn") Then
                iMenuItemCount = iMenuItemCount + 1
                mysb.Append(String.Format("<li><a href=""{0}/{1}"">{2}</a></li>", sPath, baseDir.Name, baseDir.Name.Replace(".aspx", "")).Replace("//", "/"))
            End If
        Next
        mysb.Append("</ul></div><br/><br/>")
        If iMenuItemCount = 0 Then
            mysb.Length = 0
        End If
        Return mysb.ToString
    End Function
    Public Shared Function GetASPXMenuHTML(ByVal sPath As String) As String
        Dim mySB As New StringBuilder
        Dim iMenuItemCount As Integer
        mySB.Append(String.Format("<div class=""{0}""><ul class=""{0}"">", "FileMenu"))
        For Each baseFile As FileInfo In My.Computer.FileSystem.GetDirectoryInfo(sPath).GetFiles()
            If baseFile.Extension.ToLower = ".aspx" AndAlso baseFile.Name.ToLower <> "default.aspx" Then
                iMenuItemCount = iMenuItemCount + 1
                mySB.Append(String.Format("<li><a href=""{0}"">{1}</a></li>", baseFile.Name, baseFile.Name.Replace(".aspx", "")))
            End If
        Next
        mySB.Append("</ul></div><div style=""clear:both;""><br/></div>")
        If iMenuItemCount = 0 Then
            mySB.Length = 0
        End If
        Return mySB.ToString
    End Function

    Public Shared Function Replace(ByVal original As String, pattern As String, replacement As String) As String
        Return Replace(original, pattern, replacement, StringComparison.CurrentCulture)
    End Function

    Public Shared Function Replace(original As String, pattern As String, replacement As String, comparisonType As StringComparison) As String
        Return Replace(original, pattern, replacement, comparisonType, -1)
    End Function

    Public Shared Function Replace(original As String, pattern As String, replacement As String, comparisonType As StringComparison, stringBuilderInitialSize As Integer) As String
        If original Is Nothing Then
            Return Nothing
        End If
        If [String].IsNullOrEmpty(pattern) Then
            Return original
        End If
        Dim lenPattern As Integer = pattern.Length
        Dim idxNext As Integer = original.IndexOf(pattern, comparisonType)
        Dim posCurrent As Integer = 0
        Dim result As New StringBuilder(If(stringBuilderInitialSize < 0, Math.Min(4096, original.Length), stringBuilderInitialSize))
        While idxNext >= 0
            result.Append(original, posCurrent, idxNext - posCurrent)
            result.Append(replacement)
            posCurrent = idxNext + lenPattern
            idxNext = original.IndexOf(pattern, posCurrent, comparisonType)
        End While
        result.Append(original, posCurrent, original.Length - posCurrent)
        Return result.ToString()
    End Function



    'Public Shared Sub WriteToCSV(objectList As List(Of ApplicationUserItem))
    '    Dim returnSB As New StringBuilder(String.Empty)
    '    Dim columnNames As New List(Of String)
    '    HttpContext.Current.Response.Clear()
    '    HttpContext.Current.Response.ClearHeaders()
    '    HttpContext.Current.Response.ClearContent()
    '    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=users.csv")
    '    HttpContext.Current.Response.ContentType = "text/csv"
    '    HttpContext.Current.Response.AddHeader("Pragma", "public")
    '    For Each objectItem In objectList
    '        If columnNames.Count = 0 Then
    '            columnNames = GetColumnNames(objectItem)
    '            returnSB.Append(WriteColumnName(columnNames))
    '        End If
    '        returnSB.Append(GetItemRow(objectItem, columnNames))
    '        returnSB.Append(Environment.NewLine)
    '    Next
    '    HttpContext.Current.Response.Write(returnSB.ToString())
    'End Sub

    'Public Shared Sub WriteToCSV(objectList As List(Of SurveyResponseItemBL))
    '    Dim returnSB As New StringBuilder(String.Empty)
    '    Dim columnNames As New List(Of String)
    '    HttpContext.Current.Response.Clear()
    '    HttpContext.Current.Response.ClearHeaders()
    '    HttpContext.Current.Response.ClearContent()
    '    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Certifications.csv")
    '    HttpContext.Current.Response.ContentType = "text/csv"
    '    HttpContext.Current.Response.AddHeader("Pragma", "public")
    '    For Each objectItem In objectList
    '        If columnNames.Count = 0 Then
    '            columnNames = GetColumnNames(objectItem)
    '            returnSB.Append(WriteColumnName(columnNames))
    '        End If
    '        returnSB.Append(GetItemRow(objectItem, columnNames))
    '        returnSB.Append(Environment.NewLine)
    '    Next
    '    HttpContext.Current.Response.Write(returnSB.ToString())
    'End Sub

    Private Shared Function GetItemRow(ByRef objectItem As SurveyResponseItemBL, ByRef ColumnNames As List(Of String)) As String
        Dim returnSB As New StringBuilder()
        For Each colName In ColumnNames
            returnSB.Append(AddComma(GetProperty(objectItem, colName)))
        Next
        Return returnSB.ToString()
    End Function
    Private Shared Function GetItemRow(ByRef objectItem As ApplicationUserItem, ByRef ColumnNames As List(Of String)) As String
        Dim returnSB As New StringBuilder()
        For Each colName In ColumnNames
            returnSB.Append(AddComma(GetProperty(objectItem, colName)))
        Next
        Return returnSB.ToString()
    End Function

    Private Shared Function AddComma(value As String) As String
        Dim mySB As New StringBuilder
        mySB.Append(String.Format("{0}", value.Replace(","c, " "c).Replace(""""c, "'"c)))
        mySB.Append(", ")
        Return mySB.ToString
    End Function

    Public Shared Function GetProperty(ByRef objectItem As ApplicationUserItem, ByVal PropertyName As String) As String
        Dim myValue As New Object
        For Each myProperty In objectItem.GetType.GetProperties
            If myProperty.Name = PropertyName Then
                myValue = myProperty.GetValue(objectItem, Nothing)
                Exit For
            ElseIf AppUtility.CheckForMatch(myProperty.Name, PropertyName) Then
                myValue = myProperty.GetValue(objectItem, Nothing)
                Exit For
            End If
        Next
        Try
            If String.IsNullOrEmpty(CStr(myValue)) Then
                myValue = String.Empty
            End If
        Catch ex As Exception
            myValue = String.Empty
        End Try
        Return myValue.ToString()
    End Function
    Public Shared Function GetProperty(ByRef objectItem As SurveyResponseItemBL, ByVal PropertyName As String) As String
        Dim myValue As New Object
        For Each myProperty In objectItem.GetType.GetProperties
            If myProperty.Name = PropertyName Then
                myValue = myProperty.GetValue(objectItem, Nothing)
                Exit For
            ElseIf AppUtility.CheckForMatch(myProperty.Name, PropertyName) Then
                myValue = myProperty.GetValue(objectItem, Nothing)
                Exit For
            End If
        Next
        Try
            If String.IsNullOrEmpty(CStr(myValue)) Then
                myValue = String.Empty
            End If
        Catch ex As Exception
            myValue = String.Empty
        End Try
        Return myValue.ToString()
    End Function

    Private Shared Function WriteColumnName(ByRef ColumnNames As List(Of String)) As String
        Dim returnSB As New StringBuilder(String.Empty)
        For Each colName In ColumnNames
            returnSB.Append(AddComma(colName))
        Next
        returnSB.Append(Environment.NewLine)
        Return returnSB.ToString()
    End Function

    Private Shared Function GetColumnNames(ByVal ObjectItem As Object) As List(Of String)
        Dim myColumns As New List(Of String)
        For Each myProperty In ObjectItem.GetType.GetProperties
            myColumns.Add(myProperty.Name)
        Next
        Return myColumns
    End Function

End Class



