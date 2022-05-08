<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Co_Image.ascx.vb" Inherits="controls_Co_Image" %>

<div id="pnEdit" runat="server" visible="false">
    <div>
        <asp:DropDownList ID="ddlImageList" runat="server"></asp:DropDownList>
        <asp:LinkButton ID="cmd_AddImage" runat="server" OnClick="cmd_AddImage_Click"><span class="glyphicon glyphicon-plus"></span></asp:LinkButton><asp:LinkButton ID="cmd_SetImage" runat="server" onclick="cmd_SetImage_Click" ><span class="glyphicon glyphicon-ok"></span></asp:LinkButton>
    </div>
</div>

<div id="pnView" runat="server" visible="true">
    <img src='<%=  String.Format("/images/client/{0}", GetProperty(AppPropID)) %>' class='<%=  CssClass %>' style='<%=  CssStyle %>'  /><asp:LinkButton Visible="false" ID="cmd_EditHtml" runat="server" OnClick="cmd_EditHtml_Click"><span class="glyphicon glyphicon-picture"></span></asp:LinkButton>
</div>

<div id="pnlAddImage" runat="server" visible="false">
    <asp:FileUpload ID="fuGetImage" runat="server" />
    <asp:LinkButton ID="cmd_UploadImage" runat="server" OnClick="cmd_UploadImage_Click"><span class="glyphicon glyphicon-ok"></span></asp:LinkButton>
    <asp:LinkButton ID="cmd_Cancel" runat="server" OnClick="cmd_Cancel_Click"><span class="glyphicon glyphicon-thumbs-down"></span></asp:LinkButton>
    <asp:Literal ID="litMessage" runat="server" ></asp:Literal>
</div>




