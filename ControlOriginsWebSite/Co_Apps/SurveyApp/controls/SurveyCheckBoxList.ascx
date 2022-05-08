<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyCheckBoxList.ascx.vb" Inherits="SurveyCheckBoxList" %>
<div class="row">
    <asp:Literal ID="litQuestion" runat="server"></asp:Literal><br />
    <asp:CheckBoxList ID="cblAnswers" runat="server"  ClientIDMode="Inherit" RepeatLayout="UnorderedList" CssClass="COSurvey_CheckBoxList"></asp:CheckBoxList>
</div>
