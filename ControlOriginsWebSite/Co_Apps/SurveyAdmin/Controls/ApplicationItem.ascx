<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ApplicationItem.ascx.vb" Inherits="Co_Apps_SurveyAdmin_Controls_ApplicationItem" %>
<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>

<asp:Panel ID="pnlApplicatonDetail" runat="server" CssClass="panel panel-primary" Visible="false">
    <div class="panel-heading">
        <h4>Application Settings</h4>
    </div>
    <div class="panel-body">
        <asp:HiddenField ID="hfApplicationID" runat="server" />
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-12">
                <div class="form-group">
                    <asp:Label ID="labeltbApplicationNM" runat="server" AssociatedControlID="tbApplicationNM">Application:</asp:Label>
                    <asp:TextBox ID="tbApplicationNM" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="labeltbApplicationCD" runat="server" AssociatedControlID="tbApplicationCD">Code:</asp:Label>
                    <asp:TextBox ID="tbApplicationCD" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="labeltbApplicationShortNM" runat="server" AssociatedControlID="tbApplicationShortNM">Short Name:</asp:Label>
                    <asp:TextBox ID="tbApplicationShortNM" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="labeltbMenuOrder" runat="server" AssociatedControlID="tbMenuOrder">Order:</asp:Label>
                    <asp:TextBox ID="tbMenuOrder" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="labelddlApplicationType" runat="server" AssociatedControlID="ddlApplicationType">Type:</asp:Label>
                    <asp:DropDownList ID="ddlApplicationType" runat="server" CssClass="form-control" DataSourceID="DSApplicationType" DataTextField="Name" DataValueField="Value"></asp:DropDownList>
                    <asp:ObjectDataSource ID="DSApplicationType" runat="server" SelectMethod="GetApplicationTypes" TypeName="CODataCon.DataControler"></asp:ObjectDataSource>
                </div>
                <div class="form-group">
                    <asp:Label ID="labelddlCompany" runat="server" AssociatedControlID="ddlCompany">Company:</asp:Label>
                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" DataSourceID="DSCompany" DataTextField="CompanyNM" DataValueField="CompanyID"></asp:DropDownList>
                    <asp:ObjectDataSource ID="DSCompany" runat="server" SelectMethod="GetCompanyList" TypeName="CODataCon.DataControler"></asp:ObjectDataSource>
                </div>
                <div class="form-group">
                    <asp:Label ID="labeltbApplicationDS" runat="server" AssociatedControlID="tbApplicationDS">Description:</asp:Label>
                    <asp:TextBox ID="tbApplicationDS" runat="server" CssClass="form-control" Height="113px" Rows="5" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:LinkButton ID="cmd_SaveApplication" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_SaveApplication_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_Cancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_Cancel_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_Delete" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_Delete_Click"></asp:LinkButton>
                </div>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-12">
                <uc1:DisplayTable runat="server" ID="dtUserList" EnableCSV="false"  />
                <uc1:DisplayTable runat="server" ID="dtSurvey" EnableCSV="false" />
                <uc1:DisplayTable runat="server" ID="dtSurveyResponse" EnableCSV="false" />
                <uc1:DisplayTable runat="server" ID="dtNavigation" EnableCSV="false" />
                <uc1:DisplayTable runat="server" ID="dtProperties" EnableCSV="false" />
            </div>
        </div>
        <div class="panel-footer"></div>
    </div>
</asp:Panel>

<asp:Panel ID="pnlApplicationUser" runat="server" CssClass="panel panel-primary" Visible="false">
    <div class="panel-heading">
        <h4>Application User Assignment</h4>
    </div>
    <div class="panel-body">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="form-group">
                <asp:Label ID="labelddlUser" runat="server" AssociatedControlID="ddlUser">User:</asp:Label>
                <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <asp:Label ID="labelddlRole" runat="server" AssociatedControlID="ddlRole">Role:</asp:Label>
                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                </asp:DropDownList>

            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="form-group">
                <asp:LinkButton ID="cmd_SaveUserAssignment" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_SaveUserAssignment_Click"></asp:LinkButton>
                <asp:LinkButton ID="cmd_CancelUserAssignment" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_CancelUserAssignment_Click"></asp:LinkButton>
                <asp:LinkButton ID="cmd_DeleteUserAssignment" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_DeleteUserAssignment_Click"></asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="panel-footer"></div>
</asp:Panel>

<asp:Panel ID="pnlApplicationSurvey" runat="server" CssClass="panel panel-primary" Visible="false">
    <div class="panel-heading">
        <h4>Application Survey Assignments</h4>
    </div>
    <div class="panel-body">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="form-group">
                <asp:Label ID="labelddlSurvey" runat="server" AssociatedControlID="ddlUser">Survey:</asp:Label>
                <asp:DropDownList ID="ddlSurvey" runat="server" CssClass="form-control"></asp:DropDownList>

                <asp:Label ID="labelddlDefaultRole" runat="server" AssociatedControlID="ddlDefaultRole">Default Role:</asp:Label>
                <asp:DropDownList ID="ddlDefaultRole" runat="server" CssClass="form-control" DataSourceID="dsRoleLookup" DataTextField="RoleCD" DataValueField="RoleID"></asp:DropDownList>
                <asp:ObjectDataSource ID="dsRoleLookup" runat="server" SelectMethod="GetRoles" TypeName="CODataCon.DataControler"></asp:ObjectDataSource>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="form-group">
                <asp:LinkButton ID="cmd_SaveSurveyAssignment" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_SaveSurveyAssignment_Click"></asp:LinkButton>
                <asp:LinkButton ID="cmd_CancelSurveyAssignment" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_CancelSurveyAssignment_Click"></asp:LinkButton>
                <asp:LinkButton ID="cmd_DeleteSurveyAssignment" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_DeleteSurveyAssignment_Click"></asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="panel-footer"></div>
</asp:Panel>

<asp:Panel ID="pnlSurveyResponse" runat="server" CssClass="panel panel-primary" Visible="false">
    <div class="panel-heading">
        <h4>Application Survey Responses</h4>
    </div>
    <div class="panel-body">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="form-group">
                <asp:Label ID="labelddlApplicationSurvey" runat="server" AssociatedControlID="ddlApplicationSurvey">Survey:</asp:Label>
                <asp:DropDownList ID="ddlApplicationSurvey" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <asp:Label ID="labelddlApplicationUser" runat="server" AssociatedControlID="ddlApplicationUser">User:</asp:Label>
                <asp:DropDownList ID="ddlApplicationUser" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <asp:Label ID="labeltbSurveyResponseName" runat="server" AssociatedControlID="tbSurveyResponseName">Survey Response Name:</asp:Label>
                <asp:TextBox ID="tbSurveyResponseName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="form-group">
                <asp:LinkButton ID="cmd_SaveSurveyResponse" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_SaveSurveyResponse_Click"></asp:LinkButton>
                <asp:LinkButton ID="cmd_CancelSurveyResponse" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_CancelSurveyResponse_Click"></asp:LinkButton>
                <asp:LinkButton ID="cmd_ResetSurveyResponse" runat="server" Text="Reset" CssClass="btn btn-primary"  OnClick="cmd_ResetSurveyResponse_Click"></asp:LinkButton>
                <asp:LinkButton ID="cmd_DeleteSurveyResponse" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_DeleteSurveyResponse_Click"></asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="panel-footer"></div>
</asp:Panel>
