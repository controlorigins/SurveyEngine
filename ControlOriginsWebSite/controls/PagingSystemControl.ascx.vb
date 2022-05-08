
Partial Public Class Controls_PagingSystemControl
    Inherits System.Web.UI.UserControl

    Property TotalItems As Integer
        Get

            Try
                Return HFTotalItems.Value
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            HFTotalItems.Value = value.ToString
        End Set
    End Property

    Property ItemsPerPage As Integer
        Get
            Try
                Return HFItemsPerPage.Value
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            HFItemsPerPage.Value = value
        End Set
    End Property

    Property CurPage As Integer
        Get
            Try
                Return HFCurPage.Value
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            HFCurPage.Value = value
        End Set
    End Property


    ReadOnly Property CanGoBack As String
        Get

            If CurPage = 0 Then CurPage = 1

            If CurPage = 1 Then
                Return "disabled"
            Else
                Return ""
            End If
        End Get
    End Property

    ReadOnly Property CanGoNext As String
        Get

            If CurPage = 0 Then CurPage = 1

            If CurPage < mycoll.Count Then
                Return ""
            Else
                Return "disabled"
            End If
        End Get
    End Property


    ReadOnly Property SkipItems As Integer
        Get
            Return ((CurPage - 1) * ItemsPerPage)
        End Get
    End Property

    ReadOnly Property TakeItems As Integer
        Get
            Return ItemsPerPage
        End Get
    End Property


    Dim mycoll As New List(Of PagerItem)
    Public Sub UpdatePager()



        For a = 1 To (TotalItems / ItemsPerPage)

            mycoll.Add(New PagerItem With {.name = a, .value = a, .css = ""})
        Next

        Dim mytest = ((TotalItems / ItemsPerPage) - mycoll.Count)
        If mytest > 0 Then
            mycoll.Add(New PagerItem With {.name = mycoll.Count + 1, .value = mycoll.Count + 1, .css = ""})
        End If

        If CurPage < 1 Then
            CurPage = 1
        End If
        If CurPage > mycoll.Count Then
            CurPage = mycoll.Count
        End If
        mycoll(CurPage - 1).css = "active"

        Pagenumberlist.DataSource = mycoll
        Pagenumberlist.DataBind()




    End Sub


    Event PageSelected(SelectedPageID As Integer)
    Protected Sub cmd_SelectPage_Click(sender As Object, e As EventArgs)
        Dim mybutton = CType(sender, LinkButton)
        Dim selectedPage As Integer = mybutton.Attributes("data-SelectedPageID")
        CurPage = selectedPage
        RaiseEvent PageSelected(selectedPage)
    End Sub

    Protected Sub cmd_back_Click(sender As Object, e As EventArgs)
        CurPage = CurPage - 1
        RaiseEvent PageSelected(CurPage)
    End Sub

    Protected Sub cmd_next_Click(sender As Object, e As EventArgs)
        CurPage = CurPage + 1
        RaiseEvent PageSelected(CurPage)
    End Sub
End Class

Public Class PagerItem

    Property name As String
    Property value As Integer

    Property css As String

End Class