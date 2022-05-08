<%@ Page Title=""  MaintainScrollPositionOnPostback="true" Language="VB" MasterPageFile="~/SiteMain.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" Trace="False" TraceMode="SortByTime" EnableEventValidation="false" ValidateRequest="false"  %>
<%@ MasterType VirtualPath="~/SiteMain.master" %>
<%@ Register Src="~/controls/AlertBox.ascx" TagPrefix="uc1" TagName="AlertBox" %>


<asp:Content ID="headerincludes" ContentPlaceHolderID="headerincludes" Runat="Server">
    <link id="Mycsslink" runat="server" rel='stylesheet' type='text/css' href='/css/local.css' />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:Label ID="content" runat="server">
    </asp:Label>   
    <uc1:AlertBox Visible="false"  alertType="warning" message="Devlopment Notes: Please insure that you are inheriting from 'ApplicationControlBase' " dismissable="true" runat="server" ID="AlertBox1" />
</asp:Content>

