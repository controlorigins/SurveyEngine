<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ApplicationUserItem.ascx.vb" Inherits="Co_Apps_SurveyAdmin_Controls_ApplicationUserItem" %>
<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>


<asp:Panel ID="pnlApplicatonDetail" runat="server" CssClass="panel panel-primary">
    <div class="panel-heading">
        <h4>Application User</h4>
    </div>
    <div class="panel-body">
        <asp:HiddenField ID="hfApplicationUserID" runat="server" />
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-12">
                <div class="form-group">
                    <asp:Label ID="labeltbAccountNM" runat="server" AssociatedControlID="tbAccountNM">Account:</asp:Label>
                    <asp:TextBox ID="tbAccountNM" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbFirstNM" runat="server" AssociatedControlID="tbFirstNM">First:</asp:Label>
                    <asp:TextBox ID="tbFirstNM" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="labeltbLastNM" runat="server" AssociatedControlID="tbLastNM">Last:</asp:Label>
                    <asp:TextBox ID="tbLastNM" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="labeltbEMailAddress" runat="server" AssociatedControlID="tbEMailAddress">Email:</asp:Label>
                    <asp:TextBox ID="tbEMailAddress" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="labeltbSupervisorAccountNM" runat="server" AssociatedControlID="tbSupervisorAccountNM">Supervisor Account:</asp:Label>
                    <asp:TextBox ID="tbSupervisorAccountNM" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="labeltbComment" runat="server" AssociatedControlID="tbComment">Comment:</asp:Label>
                    <asp:TextBox ID="tbComment" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="labeltbLastLoginDT" runat="server" AssociatedControlID="tbLastLoginDT">Last Login DT:</asp:Label>
                    <asp:TextBox ID="tbLastLoginDT" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    <asp:Label ID="labeltbLastLogin" runat="server" AssociatedControlID="tbLastLogin">Last Login:</asp:Label>
                    <asp:TextBox ID="tbLastLogin" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="labelddlCompany" runat="server" AssociatedControlID="ddlCompany">Company:</asp:Label>
                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" DataSourceID="DSCompany" DataTextField="CompanyNM" DataValueField="CompanyID"></asp:DropDownList>
                    <asp:ObjectDataSource ID="DSCompany" runat="server" SelectMethod="GetCompanyList" TypeName="CODataCon.DataControler"></asp:ObjectDataSource>
                </div>

                <div class="form-group">
                    <asp:LinkButton ID="cmd_Save" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_Save_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_Cancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_Cancel_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_Delete" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_Delete_Click"></asp:LinkButton>
                </div>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-12">
                <uc1:DisplayTable runat="server" ID="dtApplications" Visible="false" />
                <uc1:DisplayTable runat="server" ID="dtSurveyResponse" Visible="false" />
            </div>
        </div>
        <div class="panel-footer"></div>
    </div>




</asp:Panel>
