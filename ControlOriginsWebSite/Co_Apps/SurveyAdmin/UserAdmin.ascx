<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserAdmin.ascx.vb" Inherits="Co_Apps_SurveyAdmin_UserAdmin" %>
<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>
<div id="page-wrapper">
    <div class="row">
        <uc1:DisplayTable runat="server" ID="dtList" EnableViewState="false" EnableCSV="false" />
    </div>
    <asp:Panel runat="server" ID="pnlEdit" CssClass="row"></asp:Panel>
</div>

