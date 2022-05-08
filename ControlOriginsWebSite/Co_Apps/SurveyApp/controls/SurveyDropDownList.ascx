<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyDropDownList.ascx.vb" Inherits="SurveyDropDownList" %>
<div class="row">
    <div class="col-md-3 col-sm-3">
        <asp:Literal ID="litQuestion" runat="server"></asp:Literal><br />
    </div>
    <div class="col-md-4  col-sm-4">
        <asp:DropDownList ID="ddlAnswers" runat="server"></asp:DropDownList>
        <asp:HiddenField ID="SurveyAnswerID" runat="server" />
    </div>
    <div class="col-md-4  col-sm-4">
        <asp:TextBox ID="tbComment" TextMode="MultiLine" Rows="3" runat="server" Width="100%" Visible="false"></asp:TextBox>
    </div>
</div>
