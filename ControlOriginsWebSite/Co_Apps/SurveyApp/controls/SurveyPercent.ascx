<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyPercent.ascx.vb" Inherits="SurveyPercent" %>
<div class="row">
    <asp:Literal ID="litQuestion" runat="server"></asp:Literal><br />
    <asp:TextBox ValidationGroup="surveyformitem" ID="tb_text" runat="server" TextMode="Number"></asp:TextBox>
    <asp:RequiredFieldValidator Enabled="true" ValidationGroup="SurveyApp" ID="tb_textvalidate" ControlToValidate="tb_text" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
    <asp:HiddenField ID="hfQAID" runat="server" />
    <asp:HiddenField ID="SurveyAnswerID" runat="server" />
</div>
