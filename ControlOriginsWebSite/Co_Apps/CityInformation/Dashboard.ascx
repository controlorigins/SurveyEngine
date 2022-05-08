<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dashboard.ascx.vb" Inherits="Co_Apps_CityInformation_Dashboard" %>
<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>
<%@ Register src="~/Co_Apps/CityInformation/Controls/ContentItem.ascx" tagname="ContentItem" tagprefix="uc2" %>
<div id="page-wrapper">
    <asp:Panel runat="server" ID="pnlListChoice" CssClass="row">
        <div class="panel panel-default" id="navs">
            <div class="panel-heading">
                City Information Test
            </div>
            <div class="panel-body clearfix">
                <div class="row">
                    <div class="col-lg-3">
                        <asp:DropDownList ID="ddlCityTag" runat="server" CssClass="form-control" DataSourceID="CityTagDataSource" DataTextField="Name" DataValueField="Name"></asp:DropDownList>
                        <asp:ObjectDataSource ID="CityTagDataSource" runat="server" SelectMethod="GetAllCityTags" TypeName="CityTagManager.DataController"></asp:ObjectDataSource>
                        <br />
                        <asp:DropDownList ID="ddlKeywordTag" runat="server" CssClass="form-control" DataSourceID="KeywordTagDataSource" DataTextField="Name" DataValueField="Name"></asp:DropDownList>
                        <asp:ObjectDataSource ID="KeywordTagDataSource" runat="server" SelectMethod="GetAllKeywordTags" TypeName="CityTagManager.DataController"></asp:ObjectDataSource>
                    </div>
                    <div class="col-lg-9">
                        <ul class="nav nav-pills">
                            <li>
                                <asp:LinkButton ID="cmd_FindContent" runat="server" OnClick="cmd_FindContent_Click">Find Content</asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="cmd_GetAllContentTags" runat="server" OnClick="cmd_GetAllContentTags_Click">ContentTags</asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="cmd_GetAllTags" runat="server" OnClick="cmd_GetAllTags_Click">Tags</asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="cmd_GetAllContent" runat="server" OnClick="cmd_GetAllContent_Click">Content</asp:LinkButton></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <uc1:DisplayTable runat="server" ID="dtList" EnableViewState="false" EnableCSV="false" />
    <asp:Panel runat="server" ID="pnlEdit" >

    </asp:Panel>
</div>

