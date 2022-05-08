<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CompanyItem.ascx.vb" Inherits="Co_Apps_SurveyAdmin_Company" %>

<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>

<asp:HiddenField ID="hfCompanyID" runat="server" />

<asp:Panel ID="pnlCompanyDetail" runat="server" CssClass="panel panel-primary">
    <div class="panel-heading">
        <h4>Company</h4>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-12">
                <div class="form-group">
                    <asp:Label ID="labeltbCompanyNM" runat="server" AssociatedControlID="tbCompanyNM">Company:</asp:Label>
                    <asp:TextBox ID="tbCompanyNM" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbCompanyCD" runat="server" AssociatedControlID="tbCompanyCD">Code:</asp:Label>
                    <asp:TextBox ID="tbCompanyCD" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbCompanyDS" runat="server" AssociatedControlID="tbCompanyDS">Description:</asp:Label>
                    <asp:TextBox ID="tbCompanyDS" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbTitle" runat="server" AssociatedControlID="tbTitle">Title:</asp:Label>
                    <asp:TextBox ID="tbTitle" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbSiteURL" runat="server" AssociatedControlID="tbSiteURL">Site URL:</asp:Label>
                    <asp:TextBox ID="tbSiteURL" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbTheme" runat="server" AssociatedControlID="tbTheme">Theme:</asp:Label>
                    <asp:TextBox ID="tbTheme" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbDefaultTheme" runat="server" AssociatedControlID="tbDefaultTheme">Default Theme:</asp:Label>
                    <asp:TextBox ID="tbDefaultTheme" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbGalleryFolder" runat="server" AssociatedControlID="tbGalleryFolder">Gallery Folder:</asp:Label>
                    <asp:TextBox ID="tbGalleryFolder" runat="server" CssClass="form-control"></asp:TextBox>

                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-12">

                <div class="panel panel-default">
                    <div class="panel-heading"><small style="color: black">Address</small></div>
                    <div class="panel-body">
                        <asp:Label ID="labeltbAddress1" runat="server" AssociatedControlID="tbCompanyNM">Address 1:</asp:Label>
                        <asp:TextBox ID="tbAddress1" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="labeltbAddress2" runat="server" AssociatedControlID="tbAddress2">Address 2:</asp:Label>
                        <asp:TextBox ID="tbAddress2" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="labeltbCity" runat="server" AssociatedControlID="tbCity">City:</asp:Label>
                        <asp:TextBox ID="tbCity" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="labeltbState" runat="server" AssociatedControlID="tbState">State:</asp:Label>
                        <asp:TextBox ID="tbState" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="labeltbCountry" runat="server" AssociatedControlID="tbCountry">Country:</asp:Label>
                        <asp:TextBox ID="tbCountry" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="labeltbPostalCode" runat="server" AssociatedControlID="tbPostalCode">Postal Code:</asp:Label>
                        <asp:TextBox ID="tbPostalCode" runat="server" CssClass="form-control"></asp:TextBox>

                    </div>
                    <div class="panel-footer"></div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-12">
                <div class="form-group">
                    <asp:LinkButton ID="cmd_SaveCompany" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_SaveCompany_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_CancelCompany" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_CancelCompany_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_DeleteCompany" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_DeleteCompany_Click"></asp:LinkButton>
                </div>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-12">
                <uc1:DisplayTable runat="server" ID="dtUser" Visible="true" EnableCSV="false"  />
                <uc1:DisplayTable runat="server" ID="dtProject" Visible="true" EnableCSV="false" />
            </div>
        </div>
        <div class="panel-footer"></div>
    </div>
</asp:Panel>

