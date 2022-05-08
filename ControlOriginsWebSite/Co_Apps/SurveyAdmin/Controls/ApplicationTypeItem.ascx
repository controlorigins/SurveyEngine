<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ApplicationTypeItem.ascx.vb" Inherits="Co_Apps_SurveyAdmin_ApplicationType" %>

<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>

<asp:HiddenField ID="hfApplicationTypeID" runat="server" />

<asp:Panel ID="pnlApplicationTypeDetail" runat="server" CssClass="panel panel-primary">
    <div class="panel-heading">
        <h4>Project Type</h4>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-12">
                <div class="form-group">
                    <asp:Label ID="labeltbApplicationTypeNM" runat="server" AssociatedControlID="tbApplicationTypeNM">Project Type:</asp:Label>
                    <asp:TextBox ID="tbApplicationTypeNM" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbApplicationTypeDS" runat="server" AssociatedControlID="tbApplicationTypeDS">Description:</asp:Label>
                    <asp:TextBox ID="tbApplicationTypeDS" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:LinkButton ID="cmd_SaveApplicationType" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_SaveApplicationType_Click" ></asp:LinkButton>
                    <asp:LinkButton ID="cmd_CancelApplicationType" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_CancelApplicationType_Click" ></asp:LinkButton>
                    <asp:LinkButton ID="cmd_DeleteApplicationType" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_DeleteApplicationType_Click" ></asp:LinkButton>
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

