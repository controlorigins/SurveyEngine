<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AppClone.ascx.vb" Inherits="AppClone" %>
<link rel="stylesheet" type="text/css" href="/Content/bootstrap.min.css" />

<div>
    <div class="panel panel-default ">
        <div class="panel-heading">
            Project Clone Utility
        </div>
        <div class="panel-body">
            <label>Select Project to Clone : </label>
            <asp:DropDownList ID="curApps" runat="server" AutoPostBack="True"></asp:DropDownList>
        </div>
        <hr />
        <div>
            <div class="input-group">
                <span class="input-group-addon">Project Name</span>
                <asp:TextBox ID="CurAppName" runat="server" CssClass="form-control" placeholder="Select a project above" />
            </div>
            <div class="input-group">
                <span class="input-group-addon">App Folder</span>
                <asp:TextBox ID="CurAppPath" runat="server" type="text" class="form-control" placeholder="Select an Application above" />
            </div>
            <div class="input-group">
                <span class="input-group-addon">App Folder Found</span>
                <asp:CheckBox ID="AppFolder" runat="server" type="text" class="form-control" placeholder="Select an Application above" />
            </div>
            <div class="input-group">
                <span class="input-group-addon">App_Code Folder Found</span>
                <asp:CheckBox ID="AppCodeFolder" runat="server" type="text" class="form-control" placeholder="Select an Application above" />
            </div>
            <hr />
            <div class="input-group">
                <span class="input-group-addon">Clone App Name</span>
                <asp:TextBox ID="NewAppName" runat="server" CssClass="form-control" placeholder="Enter New App Name" />
                <span class="input-group-addon">
                    <asp:Button ID="cmd_cloneapp" runat="server" CssClass="btn btn-default" Text="Clone Applicaion" />
                </span>
            </div>
        </div>
        <div class="panel-footer">
        </div>
    </div>
</div>
