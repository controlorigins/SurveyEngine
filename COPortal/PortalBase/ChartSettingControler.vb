Imports Microsoft.VisualBasic
Imports System

Public Module ChartSettingControler


    Dim mydb As New DataClassesDataContext




    Public Function SaveChartSetting(SiteUserID As Integer, SiteAppID As Integer, SettingType As String, SettingName As String, Settingvalue As String, SettingValueEnhanced As String, DateCreated As DateTime, LastUpdated As DateTime) As Boolean

        Try
            Dim newChartSetting As New ChartSetting
            With newChartSetting
                .SiteUserID = SiteUserID
                .SiteAppID = SiteAppID
                .SettingType = SettingType
                .SettingName = SettingName
                .SettingValue = Settingvalue
                .SettingValueEnhanced = SettingValueEnhanced
                .DateCreated = Now
                .LastUpdated = Now
            End With

            Return SaveChartSetting(newChartSetting)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SaveChartSetting(chartdata As ChartSetting) As Boolean
        Try
            mydb.ChartSettings.InsertOnSubmit(chartdata)
            mydb.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetChartSetting(ChartSettingID As Integer) As ChartSetting
        Try
            Return (From i In mydb.ChartSettings Where i.Id = ChartSettingID).Single
        Catch ex As Exception
            Throw New Exception("You must use a valid ChartSettingID - No Record found")
        End Try
    End Function


    Public Function CleanupChartData(input As String) As String
        Try
            Dim mynewstring As String = ""
            Dim outstring As String = ""
            mynewstring = input.Replace(vbCrLf, "")
            mynewstring = mynewstring.Replace(Chr(34), Chr(39))
            mynewstring = mynewstring.Substring(1, mynewstring.Length - 2)
            Dim mystringsplit = mynewstring.Split(CChar(","))
            For Each i In mystringsplit
                If i.Contains("cols") Or i.Contains("rows") Or i.Contains("rendererName") Or i.Contains("aggregatorName") Or i.Contains("exclusions") Then
                    outstring += i
                    outstring += ","
                End If
            Next
            Return outstring
        Catch ex As Exception
            Return "cols: ['Question Group'], rows: ['Survey Response'],"
        End Try
    End Function



End Module
