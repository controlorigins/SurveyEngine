<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyDate.ascx.vb" Inherits="SurveyDate" %>
<div class="row">
    <asp:Literal ID="litQuestion" runat="server"></asp:Literal><br />
    <asp:TextBox ValidationGroup="surveyformitem" ID="tb_text" runat="server" TextMode="Date"></asp:TextBox>
    <asp:RequiredFieldValidator Enabled="true" ValidationGroup="SurveyApp" ID="tb_textvalidate" ControlToValidate="tb_text" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
    <asp:HiddenField ID="hfQAID" runat="server" />
    <asp:HiddenField ID="SurveyAnswerID" runat="server" />
</div>
