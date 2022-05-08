
Partial Class SiteMain
    Inherits System.Web.UI.MasterPage

    Public Property PageTitle As String 
    Get
            Return litPageTitle.Text 
    End Get
    Set(value As String)
            litPageTitle.Text = value   
    End Set
    End Property




End Class

