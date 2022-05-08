<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyComment.ascx.vb" Inherits="SurveyComment" %>
<div class="row">
    <asp:HiddenField ID="hfQAID" runat="server" />
    <asp:HiddenField ID="SurveyAnswerID" runat="server" />
    <asp:Label ID="labeltbComment"  AssociatedControlID="tbComment" runat="server"></asp:Label><br />
    <asp:TextBox ValidationGroup="surveyformitem" ID="tbComment" runat="server" CssClass="COSurvey_TextBox form-control" TextMode="MultiLine" ></asp:TextBox>
    <asp:RequiredFieldValidator Enabled="true" ValidationGroup="SurveyApp" ID="tb_textvalidate" ControlToValidate="tbComment" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
</div>
