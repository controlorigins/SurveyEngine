<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DisplayTable.ascx.vb" Inherits="controls_DisplayTable" EnableViewState="false" %>
<div class="table-responsive">
    <h2>
        <asp:Literal ID="tblTitle" runat="server"></asp:Literal><asp:LinkButton ID="cmd_GetCSV" runat="server" OnClick="lbGetCSV_Click" Text="Get CSV" Visible="false"></asp:LinkButton></h2>
    <table class="data_table table table-striped">
        <thead>
            <tr>
                <asp:Literal ID="rptHeader" runat="server" Text=""></asp:Literal>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptDataRows" runat="server" EnableViewState="false">
                <ItemTemplate>
                    <tr><%# Container.DataItem%></tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</div>
<asp:HiddenField ID="hfCSV" runat="server" />
