<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NavFrame.ascx.vb" Inherits="controls_NavFrame" %>

<nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
    <div class="navbar-header">
        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-ex1-collapse">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
        </button>
        <a class="navbar-brand" href="/"><span class="hidden-xs"><%= AppInfo.CompanyNM %></span></a>
    </div>

    <div id="navbar" class="navbar-collapse collapse navbar-ex1-collapse">

        <ul class="nav navbar-nav ">
                        <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><span class="glyphicon glyphicon-home"></span> <span > <%= AppInfo.ApplicationNM%></span></a>
                <ul class="dropdown-menu">

            <asp:Repeater ID="CurrentMenu" runat="server">
                <ItemTemplate>
                    <li id="li1" class='<%# Eval("Css")%>' runat="server">
                        <asp:LinkButton ID="cmd_PageSelection" runat="server" OnClick="cmd_PageSelection_Click" data-pid='<%# Eval("id")%>'>
                            <span class='<%# Eval("GlyphName")%>'></span> <%# Eval("MenuText")%>    
                        </asp:LinkButton>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul></li></ul>

        <ul id="ulMessageDropdown" runat="server" class="nav navbar-nav navbar-right navbar-user">
            <li>
                <asp:LinkButton ID="cmd_Myapps" runat="server" OnClick="cmd_Myapps_Click" Visible="false">My Projects</asp:LinkButton></li>
            <li class="dropdown messages-dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown"><span class='<%# Eval("GlyphName")%>'></span>Messages <span class="badge"><%= UserInfo.MessageCount%></span> <b class="caret"></b></a>
                <ul class="dropdown-menu">

                    <li class="dropdown-header"><%= UserInfo.MessageCount%> New Messages</li>
                    <asp:Repeater ID="messagepreview" runat="server">
                        <ItemTemplate>
                            <li class="message-preview">
                                <a href="#">
                                    <span class="avatar"><i class="fa fa-bell"></i></span>
                                    <span class="message"><%# Eval("Message")%></span>
                                </a>
                            </li>
                        </ItemTemplate>
                        <SeparatorTemplate>
                            <li class="divider"></li>
                        </SeparatorTemplate>
                    </asp:Repeater>

                    <li class="divider"></li>
                    <li>
                        <asp:LinkButton ID="cmd_gotoInbox" runat="server" OnClick="cmd_gotoInbox_Click">Go to Inbox <span class="badge"><%= UserInfo.MessageCount%></span></asp:LinkButton></li>
                </ul>
            </li>
            <li class="dropdown user-dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-user"></i>&nbsp;<%= UserInfo.DisplayName%><b class="caret"></b></a>
                <ul class="dropdown-menu">
                    <li>
                        <asp:LinkButton ID="cmd_logout" runat="server" OnClick="cmd_logout_Click"><i class="fa fa-power-off"></i> Log Out</asp:LinkButton></li>
                </ul>
            </li>
        </ul>
    </div>
</nav>
