<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ApplicationPivot.ascx.vb" Inherits="Co_Apps_SurveyApp_admin_ApplicationPivot" %>
<%@ Register src="../../../controls/DisplayTable.ascx" tagname="DisplayTable" tagprefix="uc1" %>

<uc1:DisplayTable ID="DisplayTable1" runat="server" />
<br />
<asp:TextBox ID="tbFilter" runat="server" Height="67px" TextMode="MultiLine" Width="873px" BorderColor="#000099" BorderStyle="Solid" BorderWidth="2px"></asp:TextBox>
<asp:Literal ID="litError" runat="server"></asp:Literal>

