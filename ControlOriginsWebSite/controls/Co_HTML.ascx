<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Co_HTML.ascx.vb" Inherits="controls_Co_HTML" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor" TagPrefix="cc1" %>

<div id="pnEdit" runat="server" visible="false">
    <div>
        <asp:LinkButton ID="cmd_SaveHtml" OnClick="cmd_SaveHtml_Click" runat="server"><span class="glyphicon glyphicon-save"></span></asp:LinkButton>
        <cc1:HtmlEditor ID="HtmlEditor1" runat="server" ToggleMode="Buttons" />
    </div>
</div>

<div id="pnView" runat="server" visible="true">
    <asp:LinkButton Visible="false" ID="cmd_EditHtml" runat="server" OnClick="cmd_EditHtml_Click"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
    <%= GetProperty(AppPropID)%>
</div>

