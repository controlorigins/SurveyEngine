<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserManagment.ascx.vb" Inherits="Co_Apps_ControlOrigins_UserManagment" %>
<%@ Register Src="~/controls/PagingSystemControl.ascx" TagPrefix="uc1" TagName="PagingSystemControl" %>

<%--<link href="../../bootstrap/css/bootstrap.css" rel="stylesheet" />--%>
<div id="page-wrapper">
    <div class="row">
        <div class="col-lg-4">
            <%-- Page Header --%>
            <div class="row">
                <div class="col-lg-12">
                    <h1>User Admin <small class="hidden-xs">Host Level</small></h1>
                </div>
            </div>
        </div>
    </div>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <asp:Panel ID="PNUserList" runat="server">
                    <div class="panel panel-default col-md-6">
                        <asp:Repeater ID="UserList" runat="server">
                            <HeaderTemplate>
                                <ul class="nav nav-pills nav-stacked">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>
                                    <asp:LinkButton ID="cmd_selectUser" data-userid='<%# Eval("ApplicationUserID")%>' runat="server" OnClick="cmd_selectUser_Click"><span class="badge pull-right"><%# GetMyApps(Eval("ApplicationUserID")).Count.ToString%></span><span  class="badge badge-alert pull-right"><%# Eval("UserRoleName")%></span><%# eval("DisplayName") %></asp:LinkButton>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img src="/images/20.gif" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <uc1:PagingSystemControl runat="server" ID="PagingSystemControl" />
                    </div>
                </asp:Panel>


                <asp:Panel ID="PNUserEdit" runat="server" Visible="false">
                    <div class="panel panel-default col-md-6">
                        <div class="input-group">
                            <label for="tbDisplayName" class="input-group-label">Display Name</label>
                            <asp:TextBox CssClass="form-control" ID="tbDisplayName" runat="server"></asp:TextBox>
                        </div>
                        <div class="input-group">
                            <label for="tbEmailAddress" class="input-group-label">Email Address</label>
                            <asp:TextBox CssClass="form-control" ID="tbEmailAddress" runat="server"></asp:TextBox>
                        </div>
                        <div class="input-group">
                            <label for="tbfirstName" class="input-group-label">First Name</label>
                            <asp:TextBox CssClass="form-control" ID="tbfirstName" runat="server"></asp:TextBox>
                        </div>
                        <div class="input-group">
                            <label for="tblastName" class="form-label">Last Name</label>
                            <asp:TextBox CssClass="form-control" ID="tblastName" runat="server"></asp:TextBox>
                        </div>
                        <div class="input-group">
                            <label for="tbassaccount" class="form-label">Portal Login Account</label>
                            <asp:TextBox CssClass="form-control" ID="tbassaccount" runat="server"></asp:TextBox>
                        </div>
                        <br />
                        <div class="input-group">
                            <label class="input-group-label">Role</label><br />
                            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                                <%= EditUser.UserRoleName%> <span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu" role="menu">
                                <asp:Repeater ID="rolelistselection" runat="server" DataSourceID="ObjectDataSource2">
                                    <ItemTemplate>
                                        <li><a href="#"><%# Eval("RoleName")%></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetRoles" TypeName="UserControler"></asp:ObjectDataSource>
                            </ul>
                        </div>
                        <br />
                        <div class="button-group">
                            <asp:HiddenField ID="TempUserID" runat="server" />
                            <asp:LinkButton CssClass="btn btn-primary" data-userid='<%# Eval("ApplicationUserID")%>' OnClick="cmd_update_Click" ID="cmd_update" runat="server" Text="Update"></asp:LinkButton>
                            <asp:LinkButton CssClass="btn btn-warning" data-userid='<%# Eval("ApplicationUserID")%>' OnClick="cmd_Cancel_Click" ID="cmd_Cancel" runat="server" Text="Cancel"></asp:LinkButton>
                        </div>

                    </div>



                    <div class="panel panel-default col-md-6">
                        <div class="panel-body ">

                            <h4>Created: <span class="label label-default"><%= GetUserByID(TempUserID.Value).ModifiedDT.ToString%></span></h4>

                            <h4>Last Login:<span class="label label-default"> <%= GetUserByID(TempUserID.Value).LastLoginDT.ToString%></span></h4>



                            <label class="input-group-label">Users Projects:</label><br />
                            <asp:Repeater ID="UsersApps" runat="server">
                                <ItemTemplate>
                                    <%# Eval("ApplicationNM")%><br />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>


                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
