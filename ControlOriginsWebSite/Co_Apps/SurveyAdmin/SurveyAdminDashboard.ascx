<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyAdminDashboard.ascx.vb" Inherits="Co_Apps_SurveyAdmin_Dashboard" %>
<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>
<div id="page-wrapper">
    <asp:Panel runat="server" ID="pnlListChoice" CssClass="row">
        <div class="panel panel-default" id="navs">
            <div class="panel-heading">
                Administration Areas
            </div>
            <div class="panel-body clearfix">
                <ul class="nav nav-pills">
                    <li>
                        <asp:LinkButton ID="cmd_GetApplicationType" runat="server" OnClick="cmd_GetApplicationType_Click">Project Type</asp:LinkButton></li>
                    <li>
                        <asp:LinkButton ID="cmd_GetSurveyType" runat="server" OnClick="cmd_GetSurveyType_Click">Survey Category</asp:LinkButton></li>
                    <li>
                        <asp:LinkButton ID="cmd_GetQuestionCategory" runat="server" OnClick="cmd_GetQuestionCategory_Click" >Question Category</asp:LinkButton></li>
                    <li>
                        <asp:LinkButton ID="cmd_GetSurveys" runat="server" OnClick="cmd_GetSurveys_Click">Survey</asp:LinkButton></li>
                    
                    <li>
                        <asp:LinkButton ID="cmd_GetApplications" runat="server" OnClick="cmd_GetApplications_Click">Project</asp:LinkButton></li>
                    <li>
                        <asp:LinkButton ID="cmd_GetQuestions" runat="server"  OnClick="cmd_GetQuestions_Click">Question Bank</asp:LinkButton></li>
                    <li>
                        <asp:LinkButton ID="cmd_GetUsers" runat="server" OnClick="cmd_GetUsers_Click">User</asp:LinkButton></li>
                    <li>
                        <asp:LinkButton ID="cmd_GetCompany" runat="server" OnClick="cmd_GetCompany_Click">Company</asp:LinkButton></li>

                </ul>
            </div>
        </div>
    </asp:Panel>
    <div class="row">
        <uc1:DisplayTable runat="server" ID="dtList" EnableViewState="false" EnableCSV="false" />
    </div>
    <asp:Panel runat="server" ID="pnlEdit" CssClass="row"></asp:Panel>
</div>

