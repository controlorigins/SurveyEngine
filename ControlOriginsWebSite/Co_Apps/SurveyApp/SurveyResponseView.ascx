<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyResponseView.ascx.vb" Inherits="Co_Apps_SurveyApp_SurveyResponseView" %>

<div class="container" style="width: 100%;">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <%=SurveyResponseNM%> (<%=StatusNM%>)<br />
                    <%=mySurveyResponse.CurrentQuestionGroup.QuestionGroupNM%>
                </div>
                <asp:Panel ID="pnlForm" CssClass="panel-body" runat="server" BackColor="White"></asp:Panel>
                <div class="panel-footer">
                    <asp:Panel ID="pnlSubmit" CssClass="panel-body" runat="server"></asp:Panel>
                    <asp:Panel ID="pnlError" CssClass="panel-body" runat="server"></asp:Panel>
                </div>
            </div>
        </div>
    </div>
</div>
