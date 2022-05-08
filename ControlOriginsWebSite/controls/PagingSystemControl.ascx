<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PagingSystemControl.ascx.vb" Inherits="Controls_PagingSystemControl" %>

<asp:HiddenField ID="HFTotalItems" runat="server" />
<asp:HiddenField ID="HFItemsPerPage" runat="server" />
<asp:HiddenField ID="HFCurPage" runat="server" />
<asp:Repeater ID="Pagenumberlist" runat="server">
    <HeaderTemplate>
        <nav>
            <ul class="pagination">
                <li class='<%= CanGoBack%>'>
                    <asp:linkbutton ID="cmd_back" OnClick="cmd_back_Click" runat="server"  aria-label="Previous">
                        <span aria-hidden="true" class="glyphicon glyphicon-chevron-left"></span>
                    </asp:linkbutton>
                </li>
    </HeaderTemplate>
    <ItemTemplate>
        <li class='<%# Eval("Css")%>'>
            <asp:LinkButton ID="cmd_SelectPage" OnClick="cmd_SelectPage_Click" data-SelectedPageID='<%# Eval("value")%>' runat="server"><%# Eval("Name")%></asp:LinkButton>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        <li class='<%= CanGoNext%>'>
            <asp:linkbutton ID="cmd_next" runat="server"  OnClick="cmd_next_Click" aria-label="Next">
                <span aria-hidden="true" class="glyphicon glyphicon-chevron-right"></span>
            </asp:linkbutton>
        </li>
        </ul>
</nav>
    </FooterTemplate>
</asp:Repeater>

