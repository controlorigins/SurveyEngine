<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TagItem.ascx.vb" Inherits="Co_Apps_CityInformation_Tag" %>

<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>

<asp:HiddenField ID="hfTagID" runat="server" />

<asp:Panel ID="pnlTagDetail" runat="server" CssClass="panel panel-primary">
    <div class="panel-heading">
        <h4>Tag</h4>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-12">
                <div class="form-group">
                    <asp:Label ID="labeltbTagNM" runat="server" AssociatedControlID="tbTagNM">Tag:</asp:Label>
                    <asp:TextBox ID="tbTagNM" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labelddlTagType" runat="server" AssociatedControlID="ddlTagType">Type:</asp:Label>
                    <asp:DropDownList ID="ddlTagType" runat="server" DataSourceID="TagTypeDataSource" DataTextField="Name" DataValueField="Id" CssClass="form-control" ></asp:DropDownList>
                    <asp:ObjectDataSource ID="TagTypeDataSource" runat="server" SelectMethod="GetTagTypes" TypeName="CityTagManager.DataController"></asp:ObjectDataSource>

                </div>
                <div class="form-group">
                    <asp:LinkButton ID="cmd_SaveTag" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_SaveTag_Click" ></asp:LinkButton>
                    <asp:LinkButton ID="cmd_CancelTag" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_CancelTag_Click" ></asp:LinkButton>
                    <asp:LinkButton ID="cmd_DeleteTag" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_DeleteTag_Click" ></asp:LinkButton>
                </div>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-12">
            </div>
        </div>
        <div class="panel-footer"></div>
    </div>
</asp:Panel>

