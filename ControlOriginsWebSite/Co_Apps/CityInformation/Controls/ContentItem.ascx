<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ContentItem.ascx.vb" Inherits="Co_Apps_CityInformation_Controls_ContentItem" %>
<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>
<uc1:DisplayTable runat="server" ID="DisplayTable" />
<asp:Panel ID="pnlContentItem" runat="server" CssClass="panel panel-primary">
    <div class="panel-heading">
        <h4>Content Item</h4>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-6">
                <div class="form-group">
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:Label ID="labeltbTitle" runat="server" AssociatedControlID="tbTitle">Title:</asp:Label>
                    <asp:TextBox ID="tbTitle" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="labeltbDescription" runat="server" AssociatedControlID="tbDescription">Description:</asp:Label>
                    <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    <asp:Label ID="labeltbLastEditDate" runat="server" AssociatedControlID="tbLastEditDate">Last Edit Date:</asp:Label>
                    <asp:TextBox ID="tbLastEditDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    <asp:Label ID="labeltbLinkAnchorText" runat="server" AssociatedControlID="tbLinkAnchorText">Link Anchor Text:</asp:Label>
                    <asp:TextBox ID="tbLinkAnchorText" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="labeltbReadMoreText" runat="server" AssociatedControlID="tbReadMoreText">Read More Text:</asp:Label>
                    <asp:TextBox ID="tbReadMoreText" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="labelcbActive" runat="server" AssociatedControlID="cbActive">Active:</asp:Label>
                    <asp:CheckBox ID="cbActive" runat="server" CssClass="form-control"></asp:CheckBox>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6">
                <div class="form-group">
                    <asp:Label ID="labeltbIconURL" runat="server" AssociatedControlID="tbIconURL">Icon URL:</asp:Label>
                    <asp:TextBox ID="tbIconURL" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="labeltbImageURL" runat="server" AssociatedControlID="tbImageURL">Image URL:</asp:Label>
                    <asp:TextBox ID="tbImageURL" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="labeltbLinkURL" runat="server" AssociatedControlID="tbLinkURL">Link URL:</asp:Label>
                    <asp:TextBox ID="tbLinkURL" runat="server" CssClass="form-control"></asp:TextBox>
                    <fieldset style="background-color: #ccc; padding: 15px 5px 15px 5px;">
                        <legend>Geo Tags</legend>
                        <asp:Label ID="labelddlCountryTag" runat="server" AssociatedControlID="ddlCountryTag">Country:</asp:Label>
                        <asp:DropDownList ID="ddlCountryTag" runat="server" CssClass="form-control" DataSourceID="ObjectDataSourceCountry" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSourceCountry" runat="server" SelectMethod="GetAllCountryTags" TypeName="CityTagManager.DataController"></asp:ObjectDataSource>

                        <asp:Label ID="labelddlStateTag" runat="server" AssociatedControlID="ddlStateTag">State:</asp:Label>
                        <asp:DropDownList ID="ddlStateTag" runat="server" CssClass="form-control" DataSourceID="ObjectDataSourceState" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSourceState" runat="server" SelectMethod="GetAllStateTags" TypeName="CityTagManager.DataController"></asp:ObjectDataSource>

                        <asp:Label ID="labelddlCityTag" runat="server" AssociatedControlID="ddlCityTag">City:</asp:Label>
                        <asp:DropDownList ID="ddlCityTag" runat="server" CssClass="form-control" DataSourceID="ObjectDataSourceCity" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSourceCity" runat="server" SelectMethod="GetAllCityTags" TypeName="CityTagManager.DataController"></asp:ObjectDataSource>

                    </fieldset>
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12">
                <div class="form-group">
                    <asp:LinkButton ID="cmd_Save" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_Save_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_Cancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_Cancel_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_Delete" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_Delete_Click"></asp:LinkButton>
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12">
                <asp:Label ID="labelddlKeywordTag" runat="server" AssociatedControlID="ddlKeywordTag">Keyword:</asp:Label>
                <asp:DropDownList ID="ddlKeywordTag" runat="server" CssClass="form-control" DataSourceID="ObjectDataSourceKeyword" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                <asp:ObjectDataSource ID="ObjectDataSourceKeyword" runat="server" SelectMethod="GetAllKeywordTags" TypeName="CityTagManager.DataController"></asp:ObjectDataSource>

                <uc1:DisplayTable runat="server" ID="dtList" EnableViewState="false" EnableCSV="false" />
            </div>
        </div>
    </div>


</asp:Panel>
