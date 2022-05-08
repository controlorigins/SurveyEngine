<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyResponseFilter.ascx.vb" Inherits="SurveyResponseFilter" %>
<asp:Panel ID="pnlFilter" runat="server">

    <div class="form panel panel-default" ">
        <div class="panel-heading">
            Search Criteria
        </div>
        <div class="panel-body">
            <div class="col-lg-4 col-md-6 col-sm=6 col-xs-12">
                <div class="form-group">
                    <asp:Label ID="lblFirstName" runat="server" Text="Frist Name" AssociatedControlID="tbFirstName"></asp:Label><br />
                    <asp:TextBox ID="tbFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="lblLastName" runat="server" Text="Last Name" AssociatedControlID="tbLastName"></asp:Label><br />
                    <asp:TextBox ID="tbLastName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="lblManagerName" runat="server" Text="Manager" AssociatedControlID="tbManagerName"></asp:Label>
                    <asp:TextBox ID="tbManagerName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-lg-4 col-md-6 col-sm=6 col-xs-12">
                <div class="form-group">
                    <asp:Label ID="lblddlSurvey" runat="server" Text="Survey" AssociatedControlID="ddlSurvey"></asp:Label><br />
                    <asp:ListBox ID="ddlSurvey" runat="server" CssClass="form-control" Rows="8" SelectionMode="Multiple"></asp:ListBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblddlSurveyResponseStatus" runat="server" Text="Response Status" AssociatedControlID="ddlSurveyResponseStatus"></asp:Label><br />
                    <asp:ListBox ID="ddlSurveyResponseStatus" runat="server" CssClass="form-control" Rows="8" SelectionMode="Multiple"></asp:ListBox>
                </div>
            </div>
            <div class="col-lg-4 col-md-6 col-sm=6 col-xs-12">
                <div class="form-group">
                    <strong>Use operator (>,<.=) before numeric argument</strong>
                    <asp:Label ID="lbltbVariantCount" runat="server" Text="Variant Count" AssociatedControlID="tbVariantCount"></asp:Label><br />
                    <asp:TextBox ID="tbVariantCount" runat="server"></asp:TextBox><br />
                    <asp:Label ID="lbltbACECount" runat="server" Text="Count" AssociatedControlID="tbACECount"></asp:Label><br />
                    <asp:TextBox ID="tbACECount" runat="server" Text=""></asp:TextBox><br />
                    <asp:Label ID="lbltbDaysSinceModified" runat="server" Text="Days Since Modified" AssociatedControlID="tbDaysSinceModified"></asp:Label><br />
                    <asp:TextBox ID="tbDaysSinceModified" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>

</asp:Panel>

