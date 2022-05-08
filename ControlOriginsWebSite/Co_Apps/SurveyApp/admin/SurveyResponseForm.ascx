<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyResponseForm.ascx.vb" Inherits="Co_Apps_SurveyApp_controls_SurveyResponseForm" %>
<%--<link href="~/Content/bootstrap.css" rel="stylesheet" />
<link href="~/Content/bootstrap-theme.css" rel="stylesheet" />--%>

<div class="form">

    <asp:HiddenField ID="HFAppID" runat="server" />
    <asp:HiddenField ID="HFModID" runat="server" />

    <div class="input-group">
        <label for="LBID">ID</label>
        <asp:Label ID="LBID" runat="server" CssClass="form-control" Enabled="false"></asp:Label>
    </div>

    <div class="input-group">
        <label for="TBName">Name</label>
        <asp:TextBox ID="TBName" runat="server"  CssClass="form-control"></asp:TextBox>
    </div>

    <div class="input-group">
        <label for="">Status (Read Only)</label>
        <asp:DropDownList ID="DDStatus" runat="server"  CssClass="form-control" Enabled="false"></asp:DropDownList>
    </div>
   
    <div class="input-group">
        <label for="">Assign to:</label>
        <asp:DropDownList ID="DDAssignTo" runat="server" CssClass="form-control">
        </asp:DropDownList>
    </div>
       
    <div class="input-group">
        <label for="">Survey</label>
        <asp:DropDownList ID="DDServey" runat="server" CssClass="form-control">
        </asp:DropDownList>
    </div>
</div>
