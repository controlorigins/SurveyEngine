Public Class DomainSetup
    Implements IDomain
    Public Property DomainComment As String Implements IDomain.DomainComment
    Public Property DomainDS As String Implements IDomain.DomainDS
    Public Property DomainID As Integer Implements IDomain.DomainID
    Public Property DomainNM As String Implements IDomain.DomainNM
    Public Property DomainTitle As String Implements IDomain.DomainTitle
    Public Property DomainURL As String Implements IDomain.DomainURL
        Get
            Return _DomainURL
        End Get
        Set(value As String)
            _DomainURL = value
            Using mycon As New  CityTagDataContext
                Dim myReturn = (From i In mycon.Domains Where i.DomainURL.ToLower = value.ToLower).ToList()
                If myReturn.Count > 0 Then
                    With myReturn(0)
                        DomainComment = .DomainComment
                        DomainDS = .DomainDS
                        DomainID = .DomainID
                        DomainNM = .DomainNM
                        DomainTitle = .DomainTitle
                        GalleryFolder = .GalleryFolder
                        UseBreadCrumbURL = .UseBreadCrumbURL
                        ModifiedDT = .ModifiedDT
                        ModifiedID = .ModifiedID
                        VersionNo = .VersionNo
                    End With
                Else
                    DomainComment = value
                    DomainDS = value
                    DomainID = -1
                    DomainNM = value
                    DomainTitle = value
                    GalleryFolder = "/sites/"
                    UseBreadCrumbURL = False
                    ModifiedDT = Now()
                    ModifiedID = 1
                    VersionNo = 1
                    Save()
                End If
            End Using
        End Set
    End Property


    Private _DomainURL As String

    Public Property GalleryFolder As String Implements IDomain.GalleryFolder
    Public Property ModifiedDT As Date Implements IDomain.ModifiedDT
    Public Property ModifiedID As Integer Implements IDomain.ModifiedID
    Public Property UseBreadCrumbURL As Boolean Implements IDomain.UseBreadCrumbURL
    Public Property VersionNo As Integer Implements IDomain.VersionNo



    Public Function Save() As DomainSetup
        Using mycon As New CityTagDataContext
            Dim myReturn = (From i In mycon.Domains Where i.DomainURL.ToLower = DomainURL.ToLower).ToList()
            If myReturn.Count > 0 Then
                With myReturn(0)
                    .DomainComment = DomainComment
                    .DomainDS = DomainDS
                    .DomainID = DomainID
                    .DomainNM = DomainNM
                    .DomainTitle = DomainTitle
                    .GalleryFolder = GalleryFolder
                    .UseBreadCrumbURL = UseBreadCrumbURL
                    .ModifiedDT = Now()
                    .ModifiedID = 1
                    .VersionNo = .VersionNo + 1
                End With
            Else
                myReturn.Add(New Domain)
                mycon.Domains.InsertOnSubmit(myReturn(0))
                With myReturn(0)
                    .DomainComment = DomainComment
                    .DomainURL = DomainURL
                    .DomainDS = DomainDS
                    .DomainNM = DomainNM
                    .DomainTitle = DomainTitle
                    .GalleryFolder = GalleryFolder
                    .UseBreadCrumbURL =  UseBreadCrumbURL
                    .ModifiedDT = Now()
                    .ModifiedID = 1
                    .VersionNo = .VersionNo + 1
                End With
            End If
            mycon.SubmitChanges()
            DomainID = myReturn(0).DomainID
        End Using
        Return Me
    End Function


End Class