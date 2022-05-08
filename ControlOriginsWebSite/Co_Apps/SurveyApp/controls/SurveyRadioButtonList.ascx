<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyRadioButtonList.ascx.vb" Inherits="SurveyRadioButtonList" %>
<asp:UpdatePanel ID="srblPanel" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <div class="row COSurvey_Row">
            <div class="col-md-3  col-sm-3">
                <asp:HiddenField ID="SurveyAnswerID" runat="server" />
                <asp:Label ID="labQuestion" runat="server" CssClass="COSurvey_Question"></asp:Label>
            </div>
            <div class="col-md-4  col-sm-4">
                <asp:RadioButtonList ID="rblAnswers" CssClass="COSurvey_RadioButtonList" runat="server" RepeatLayout="UnorderedList" OnSelectedIndexChanged="rblAnswers_SelectedIndexChanged" AutoPostBack="true"></asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rblAnswersValidator" ForeColor="Red" runat="server" ControlToValidate="rblAnswers" ErrorMessage="Please Select an answer" ValidationGroup="SurveyApp">
                </asp:RequiredFieldValidator>
            </div>
            <div class="col-md-4  col-sm-4">
                <asp:Label ID="LabeltbComment" runat="server" CssClass="" AssociatedControlID="tbComment" Visible="true">Comment:</asp:Label>
                <asp:TextBox ID="tbComment" runat="server" CssClass="COSurvey_TextBox form-control" Rows="3" TextMode="MultiLine" Visible="true" Width="100%"></asp:TextBox>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>


