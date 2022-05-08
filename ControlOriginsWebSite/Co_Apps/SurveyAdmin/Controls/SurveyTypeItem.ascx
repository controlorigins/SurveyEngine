<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyTypeItem.ascx.vb" Inherits="Co_Apps_SurveyAdmin_SurveyType" %>

<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>

<asp:HiddenField ID="hfSurveyTypeID" runat="server" />

<asp:Panel ID="pnlSurveyTypeDetail" runat="server" CssClass="panel panel-primary">
    <div class="panel-heading">
        <h4>Survey Category</h4>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-12">
                <div class="form-group">
                    <asp:Label ID="labeltbSurveyTypeNM" runat="server" AssociatedControlID="tbSurveyTypeNM">Survey Category:</asp:Label>
                    <asp:TextBox ID="tbSurveyTypeNM" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbSurveyTypeShortNM" runat="server" AssociatedControlID="tbSurveyTypeShortNM">Short:</asp:Label>
                    <asp:TextBox ID="tbSurveyTypeShortNM" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbSurveyTypeDS" runat="server" AssociatedControlID="tbSurveyTypeDS">Description:</asp:Label>
                    <asp:TextBox ID="tbSurveyTypeDS" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbSurveyTypeComment" runat="server" AssociatedControlID="tbSurveyTypeComment">Comment:</asp:Label>
                    <asp:TextBox ID="tbSurveyTypeComment" runat="server" CssClass="form-control"></asp:TextBox>


                    <asp:Label ID="labelcbMultipleSequence" runat="server" AssociatedControlID="cbMultipleSequence">Multiple Sequence:</asp:Label>
                    <asp:CheckBox ID="cbMultipleSequence" runat="server" CssClass="form-control"  />

                    <asp:Label ID="labelddlApplicationType" runat="server" AssociatedControlID="ddlApplicationType">Project Type:</asp:Label>
                    <asp:DropDownList ID="ddlApplicationType" runat="server" CssClass="form-control"></asp:DropDownList>

                    <asp:Label ID="labelddlParentSurveyType" runat="server" AssociatedControlID="ddlParentSurveyType">Parent:</asp:Label>
                    <asp:DropDownList ID="ddlParentSurveyType" runat="server" CssClass="form-control"></asp:DropDownList>

                </div>
                <div class="form-group">
                    <asp:LinkButton ID="cmd_SaveSurveyType" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_SaveSurveyType_Click" ></asp:LinkButton>
                    <asp:LinkButton ID="cmd_CancelSurveyType" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_CancelSurveyType_Click" ></asp:LinkButton>
                    <asp:LinkButton ID="cmd_DeleteSurveyType" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_DeleteSurveyType_Click" ></asp:LinkButton>
                </div>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-12">
                <uc1:DisplayTable runat="server" ID="dtQuestion" Visible="true" EnableCSV="false" />
                <uc1:DisplayTable runat="server" ID="dtSurvey" Visible="false" EnableCSV="false" />
            </div>
        </div>
        <div class="panel-footer"></div>
    </div>
</asp:Panel>

