<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyItem.ascx.vb" Inherits="Co_Apps_SurveyAdmin_Controls_SurveyItem" %>

<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>

<asp:HiddenField ID="hfSurveyID" runat="server" />
<asp:HiddenField ID="hfQuestionGroupID" runat="server" />
<asp:HiddenField ID="hfQuestionGroupMemberID" runat="server" />

<asp:Panel ID="pnlApplicatonDetail" runat="server" CssClass="panel panel-primary">
    <div class="panel-heading">
        <h4>Survey</h4>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-12">
                <div class="form-group">
                    <asp:Label ID="labeltbSurveyNM" runat="server" AssociatedControlID="tbSurveyNM">Survey:</asp:Label>
                    <asp:TextBox ID="tbSurveyNM" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="labeltbSurveyShortNM" runat="server" AssociatedControlID="tbSurveyShortNM">Short Name:</asp:Label>
                    <asp:TextBox ID="tbSurveyShortNM" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="labeltbSurveyDS" runat="server" AssociatedControlID="tbSurveyDS">Description:</asp:Label>
                    <asp:TextBox ID="tbSurveyDS" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="labeltbCompletionMessage" runat="server" AssociatedControlID="tbCompletionMessage">Completion Message:</asp:Label>
                    <asp:TextBox ID="tbCompletionMessage" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="labeltbStartDate" runat="server" AssociatedControlID="tbStartDate">Start Date:</asp:Label>
                    <asp:TextBox ID="tbStartDate" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="labeltbEndDate" runat="server" AssociatedControlID="tbEndDate">End Date:</asp:Label>
                    <asp:TextBox ID="tbEndDate" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="labelddlSurveyType" runat="server" AssociatedControlID="ddlSurveyType">Category:</asp:Label>
                    <asp:DropDownList ID="ddlSurveyType" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:Label ID="labeltbTotalGroupWeight" runat="server" AssociatedControlID="tbTotalGroupWeight">Total Group Weight:</asp:Label>
                    <asp:TextBox ID="tbTotalGroupWeight" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:LinkButton ID="cmd_SaveSurvey" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_SaveSurvey_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_CancelSurvey" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_CancelSurvey_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_DeleteSurvey" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_DeleteSurvey_Click"></asp:LinkButton>
                </div>

                <div class="form-group">
                    <uc1:DisplayTable runat="server" ID="dtSurveyStatus" EnableCSV="false" />
                    <uc1:DisplayTable runat="server" ID="dtSurveyReviewStatus" EnableCSV="false" />
                </div>



            </div>
            <div class="col-lg-8 col-md-8 col-sm-12">
                <uc1:DisplayTable runat="server" ID="dtList" EnableCSV="false" />
                <uc1:DisplayTable runat="server" ID="dtQuestions" EnableCSV="false" />
                <uc1:DisplayTable runat="server" ID="dtApplications" EnableCSV="false" />
                <uc1:DisplayTable runat="server" ID="dtSurveyResponses" EnableCSV="false" />
            </div>
        </div>
        <div class="panel-footer"></div>
    </div>

</asp:Panel>


<asp:Panel ID="pnlQuestionGroup" runat="server" CssClass="panel panel-primary" Visible="true">
    <div class="panel-heading">
        <h4>Survey Question Group</h4>
    </div>
    <div class="panel-body">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="form-group">
                <asp:Label ID="labeltbQuestionGroupNM" runat="server" AssociatedControlID="tbQuestionGroupNM">Group Name:</asp:Label>
                <asp:TextBox ID="tbQuestionGroupNM" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Label ID="labeltbQuestionGroupShortNM" runat="server" AssociatedControlID="tbQuestionGroupShortNM">Short Name:</asp:Label>
                <asp:TextBox ID="tbQuestionGroupShortNM" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Label ID="labeltbQuestionGroupDS" runat="server" AssociatedControlID="tbQuestionGroupDS">Description:</asp:Label>
                <asp:TextBox ID="tbQuestionGroupDS" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Label ID="labeltbGroupWeight" runat="server" AssociatedControlID="tbGroupWeight">Weight:</asp:Label>
                <asp:TextBox ID="tbGroupWeight" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Label ID="labeltbGroupOrder" runat="server" AssociatedControlID="tbGroupHeader">Order:</asp:Label>
                <asp:TextBox ID="tbGroupOrder" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Label ID="labeltbGroupHeader" runat="server" AssociatedControlID="tbGroupHeader">Header:</asp:Label>
                <asp:TextBox ID="tbGroupHeader" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Label ID="labeltbGroupFooter" runat="server" AssociatedControlID="tbGroupFooter">Footer:</asp:Label>
                <asp:TextBox ID="tbGroupFooter" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Label ID="labeltbTotalQuestionWeight" runat="server" AssociatedControlID="tbTotalQuestionWeight">Total Question Weight:</asp:Label>
                <asp:TextBox ID="tbTotalQuestionWeight" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="form-group">
                <asp:LinkButton ID="cmd_SaveQuestionGroup" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_SaveQuestionGroup_Click"></asp:LinkButton>
                <asp:LinkButton ID="cmd_CancelQuestionGroup" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_CancelQuestionGroup_Click"></asp:LinkButton>
                <asp:LinkButton ID="cmd_DeleteQuestionGroup" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_DeleteQuestionGroup_Click"></asp:LinkButton>
            </div>
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12">
            <uc1:DisplayTable runat="server" ID="dtQuestionGroupMember" EnableCSV="false" />
        </div>


    </div>
    <div class="panel-footer"></div>
</asp:Panel>



<asp:Panel ID="pnlGroupMember" runat="server" CssClass="panel panel-primary" Visible="false">
    <div class="panel-heading">
        <h4>Survey Group Member Question</h4>
    </div>
    <div class="panel-body">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="form-group">
                <asp:Label ID="labelddlMemberQuestion" runat="server" AssociatedControlID="ddlMemberQuestion">Question:</asp:Label>
                <asp:DropDownList ID="ddlMemberQuestion" runat="server" CssClass="form-control"></asp:DropDownList>

                <asp:Label ID="labeltbMemberOrder" runat="server" AssociatedControlID="tbMemberOrder">Order:</asp:Label>
                <asp:TextBox ID="tbMemberOrder" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Label ID="labeltbMemberWeight" runat="server" AssociatedControlID="tbMemberWeight">Weight:</asp:Label>
                <asp:TextBox ID="tbMemberWeight" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="form-group">
                <asp:LinkButton ID="cmd_GroupMemberSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_GroupMemberSave_Click"></asp:LinkButton>
                <asp:LinkButton ID="cmd_GroupMemberCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_GroupMemberCancel_Click"></asp:LinkButton>
                <asp:LinkButton ID="cmd_GroupMemberDelete" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_GroupMemberDelete_Click"></asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="panel-footer">
    </div>
</asp:Panel>
