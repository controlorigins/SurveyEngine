Imports CODataCon
Imports System.IO

Partial Class Test
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim mySB As New StringBuilder
        Dim mycon As New DataControler()
        Dim myID As Integer = Context.Request.QueryString("id")
        litMain.Text = ""
        If Not IsPostBack Then
            If myID > 0 Then
                Dim myFile = mycon.GetFileByID(myID)
                download(myFile.Data, myFile.ContentType, myFile.Name)
            Else
                mySB.AppendLine("<ul>")
                For Each i As CODataCon.com.controlorigins.ws.tblFilesItem In mycon.GetFileList()
                    mySB.AppendLine(String.Format("<li><a href='/Test.aspx?id={2}'>{0} - {1}</a></li>", i.Name, i.ContentType, i.Id))
                Next
                mySB.AppendLine("</ul>")
                litMain.Text = mySB.ToString()
            End If
        End If
    End Sub


    Protected Sub download(ByVal bytes() As Byte, ByVal ContentType As String, ByVal Name As String)
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = ContentType
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.End()
    End Sub



    Protected Sub cmd_SaveFile_Click(sender As Object, e As EventArgs)
        Dim mycon As New DataControler()

        If FileUpload.HasFile Then
            FileUpload.SaveAs(Server.MapPath("/images/client/") & FileUpload.FileName)
            ' Read the file and convert it to Byte Array
            Dim filePath As String = Server.MapPath("/images/client/" & FileUpload.FileName)
            Dim filename As String = Path.GetFileName(filePath)

            Dim fs As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
            Dim br As BinaryReader = New BinaryReader(fs)
            Dim bytes As Byte() = br.ReadBytes(Convert.ToInt32(fs.Length))
            br.Close()
            fs.Close()

            Dim myFile As New CODataCon.com.controlorigins.ws.tblFilesItem
            With myFile
                .ContentType = "image/jpeg"
                .Name = FileUpload.FileName
                .Data = bytes
            End With
            mycon.PutFile(myFile)
        End If

        Dim mySB As New StringBuilder
        mySB.AppendLine("<ul>")
        For Each i As CODataCon.com.controlorigins.ws.tblFilesItem In mycon.GetFileList()
            mySB.AppendLine(String.Format("<li><a href='/Test.aspx?id={2}'>{0} - {1}</a></li>", i.Name, i.ContentType, i.Id))
        Next
        mySB.AppendLine("</ul>")
        litMain.Text = mySB.ToString()

    End Sub

    Protected Sub ddlCategory_DataBound(sender As Object, e As EventArgs) Handles ddlCategory.DataBound
        If ddlCategory.Items.Count > 0 Then
            ddlCategory.SelectedIndex = 0
            ddlSubCategory.Items.Clear()
            ddlSubCategory.DataBind()
        Else
            ddlSubCategory.Items.Clear()
        End If
    End Sub
    Protected Sub ddlSubCategory_DataBound(sender As Object, e As EventArgs) Handles ddlSubCategory.DataBound
        If ddlSubCategory.Items.Count>0 then 
            ddlSubCategory.SelectedIndex = 0
            ddlSubCategory_SelectedIndexChanged(sender,e)
        End If
    End Sub
    Protected Sub ddlSubCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim myCon As New DataGridVisualization.DataController()
        Dim myGrid = myCon.GetQuestions(ddlSubCategory.SelectedValue)
        Dim myDisplayTableHeader As New DataGridVisualization.DisplayTableHeader
        myDisplayTableHeader.DetailFieldName = "QuestionNM"
        myDisplayTableHeader.DetailKeyName = "QuestionID"
        myDisplayTableHeader.DetailPath = String.Format("/navigator.aspx?applicationid={0}&applicationuserid={1}", 1, 1) & "&surveyresponseid={0}"
        For Each myCol In myGrid.GridColumns
            If myCol.DataType = "string" Or myCol.DataType = "integer" Then
                myDisplayTableHeader.AddHeaderItem(myCol.DisplayName, myCol.DisplayName, False)
            End If
        Next
        dtList.BuildTableFromGrid(myDisplayTableHeader, myGrid)
    End Sub

End Class
