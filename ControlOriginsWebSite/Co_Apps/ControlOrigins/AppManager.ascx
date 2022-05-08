<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AppManager.ascx.vb" Inherits="Co_Apps_ControlOrigins_AppManager" %>
<%--<link href="../../Content/bootstrap.css" rel="stylesheet" />--%>

<%-- page-wrapper--%>
<div id="page-wrapper">


    <%-- Page Header --%>
    <div class="row">
        <div class="col-lg-12">
            <h1>Admin <small class="hidden-xs">application</small></h1>
        </div>
    </div>

    <%-- Select Application Menu --%>
    <div class="row">
        <div class="col-lg-12">
            <asp:DropDownList ID="AppListing" AutoPostBack="true" OnSelectedIndexChanged="AppListing_SelectedIndexChanged" AppendDataBoundItems="true" runat="server" CssClass="form-control">
                <asp:ListItem>Select One</asp:ListItem>
                <asp:ListItem>Add New Application</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <h1>
                <asp:HiddenField ID="AppID" runat="server" />
                <asp:Label ID="AppName" runat="server" />
                <small class="hidden-xs">
                    <asp:Label ID="AppPath" runat="server" /></small></h1>
        </div>
    </div>


    <%-- Application Basic Settings --%>
    <div id="basicsettingspanel" runat="server" class="row" visible="false">

        <div class="panel panel-primary">
            <div class="panel-heading">Application basic settings</div>
            <div class="panel-body">

                <div class="input-group">
                    <span class="input-group-addon">N</span>
                    <asp:HiddenField ID="hfDefaultRoleID" runat="server" />
                    <asp:TextBox ValidationGroup="appbasicinfo" ID="tbAppName" runat="server" CssClass="form-control" placeholder="Application Name Here..."></asp:TextBox>
                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="appbasicinfo" ID="RequiredFieldValidator1" runat="server" ForeColor="Orange" ErrorMessage="You must include an application name." ControlToValidate="tbAppName"></asp:RequiredFieldValidator>

                </div>

                <br />

                <div class="input-group">
                    <span class="input-group-addon">D</span>

                    <asp:TextBox ValidationGroup="appbasicinfo" TextMode="MultiLine" Rows="3" ID="TbAppDescription" runat="server" CssClass="form-control" placeholder="Application Description"></asp:TextBox>
                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="appbasicinfo" ID="RequiredFieldValidator2" runat="server" ForeColor="Orange" ErrorMessage="Please enter a short description as required." ControlToValidate="TbAppDescription"></asp:RequiredFieldValidator>
                </div>

                <br />
                <div class="input-group">
                    <span class="input-group-addon">T</span>
                    <asp:DropDownList ValidationGroup="appbasicinfo" ID="ddlApplicationType" runat="server" class="form-control" AppendDataBoundItems="True">
                        <asp:ListItem Value="0" Text="Select one">Select Type</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="appbasicinfo" ControlToValidate="ddlApplicationType" ID="RequiredFieldValidator9" runat="server" ForeColor="Orange" ErrorMessage="Please Select your Project Type." InitialValue="Select One"></asp:RequiredFieldValidator>

                </div>
                <br />

                <div class="input-group">
                    <span class="input-group-addon">F</span>
                    <asp:DropDownList ValidationGroup="appbasicinfo" ID="ddAppFolder" runat="server" class="form-control" AppendDataBoundItems="True">
                        <asp:ListItem>Select One</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="appbasicinfo" ControlToValidate="ddAppFolder" ID="RequiredFieldValidator3" runat="server" ForeColor="Orange" ErrorMessage="Please Select your Application Folder." InitialValue="Select One"></asp:RequiredFieldValidator>

                </div>
                <br />
                <div class="input-group">
                    <span class="input-group-addon">S</span>
                    <asp:DropDownList ValidationGroup="appbasicinfo" ID="CSSDropdown" runat="server" class="form-control" AppendDataBoundItems="True">
                        <asp:ListItem>Select One</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="appbasicinfo" ControlToValidate="CSSDropdown" ID="RequiredFieldValidator8" runat="server" ForeColor="Orange" ErrorMessage="Please Select Css Style for your Application." InitialValue="Select One"></asp:RequiredFieldValidator>
                </div>
                <br />

                <div class="col-sm-12">
                    <div class="container-fluid">
                        <div class="input-group">
                            <div class="btn-group">
                                <asp:Button ValidationGroup="appbasicinfo" ID="cmd_SaveAppBasic" runat="server" CssClass="btn btn-success" Text="Next" OnClick="cmd_SaveAppBasic_Click" />
                                <asp:Button ID="cmd_cancelAppBasic" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="cmd_cancelAppBasic_Click" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>


    <%--Add New Page Panel--%>
    <div id="addnewpagePanel" runat="server" class="row" visible="false">
        <div class="panel panel-success">
            <div class="panel-heading">Add Application Page Definitions</div>
            <div class="panel-body">

                <div class="row">


                    <div class="input-group col-lg-12">
                        <span class="input-group-addon">D</span>
                        <asp:DropDownList ID="ddAppFiles" runat="server" class="form-control" AppendDataBoundItems="true">
                            <asp:ListItem>Select One</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="Select One" ControlToValidate="ddAppFiles" ValidationGroup="addpageval" Display="Dynamic" ID="RequiredFieldValidator4" runat="server" ErrorMessage="You must select a user control."></asp:RequiredFieldValidator>
                    </div>
                </div>
                <br />
                <div class="row">


                    <div class="col-sm-3">
                        <div class="input-group">
                            <span class="input-group-addon">D</span>
                            <asp:TextBox ID="tbMenuText" runat="server" CssClass="form-control" placeholder="Menu Text"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="tbMenuText" ValidationGroup="addpageval" Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Menu Text is required."></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="input-group">
                            <span class="input-group-addon">D</span>
                            <asp:TextBox ID="tbGlyphName" runat="server" CssClass="form-control" placeholder="fa fa-cogs"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="tbGlyphName" ValidationGroup="addpageval" Display="Dynamic" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Glyph name is required."></asp:RequiredFieldValidator>

                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="input-group">
                            <span class="input-group-addon">D</span>
                            <asp:DropDownList ID="ddroles" runat="server" class="form-control" AppendDataBoundItems="True">
                                <asp:ListItem>Select One</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator InitialValue="Select One" ControlToValidate="ddroles" ValidationGroup="addpageval" Display="Dynamic" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please select Role Level for this Item."></asp:RequiredFieldValidator>
                        </div>
                    </div>


                    <div class="col-sm-3">
                        <div class="input-group">
                            <span class="input-group-addon">D</span>
                            <asp:CheckBox ID="cbviewinmenu" runat="server" Checked="true" class="form-control" AppendDataBoundItems="True" Text="View In Menu"></asp:CheckBox>
                        </div>
                    </div>


                </div>
                <br />
                <br />
                <br />

                <div class="col-sm-12">
                    <div class="container-fluid">
                        <div class="input-group">
                            <div class="btn-group">
                                <asp:Button ValidationGroup="addpageval" ID="cmd_savePage" runat="server" CssClass="btn btn-success" OnClick="cmd_savePage_Click" Text="Save" />
                                <asp:Button ID="cmd_UpdatePage" OnClick="cmd_UpdatePage_Click" runat="server" class="btn btn-success" Text="Update" />
                                <asp:Button ID="cmd_cancelAddPage" runat="server" class="btn btn-danger" OnClick="cmd_cancelAddPage_Click" Text="Cancel" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>


    <%-- Page Definition List --%>
    <div id="DefinitionList" runat="server" visible="false" class="row">
        <div class="panel panel-primary">
            <div class="panel-heading">Application Page Definitions List</div>
            <div class="panel-body">

                <div class="col-sm-12">
                    <div class="input-group">
                        <span class="input-group-addon">
                            <span class="btn-group-vertical">
                                <asp:Button ID="cmd_addPageDialog" class="btn btn-success btn-sm " runat="server" OnClick="cmd_addPageDialog_Click" Text="Add" />
                                <asp:Button CssClass="btn btn-info btn-sm disabled" Text="Edit" ID="cmd_EditDef" runat="server" OnClick="cmd_EditDef_Click" />
                                <asp:Button CssClass="btn btn-danger btn-sm disabled" Text="Delete" ID="cmd_DeleteDef" runat="server" OnClick="cmd_DeleteDef_Click" />
                                <asp:Button CssClass="btn btn-danger btn-sm disabled" Text="Set Default" ID="cmd_SetDefault" runat="server" OnClick="cmd_SetDefault_Click" />
                                <asp:Button class="btn btn-warning btn-sm dropdown-toggle disabled " data-toggle="dropdown" ID="cmd_MoveDef" runat="server" Text="Move" />
                                <ul class="dropdown-menu dropup " role="menu">
                                    <li class="divider"></li>
                                    <li title="Move to Top">
                                        <asp:LinkButton ID="cmd_MovetoTop" runat="server" CommandName="move2Top" CommandArgument="" OnClick="cmd_MovetoTop_Click"><i class="fa fa-angle-double-up" style="color: white;"></i></asp:LinkButton></li>
                                    <li title="Move up One">
                                        <asp:LinkButton ID="cmd_MoveUp" runat="server" CommandName="moveUP" OnClick="cmd_MovetoTop_Click"><i class="fa fa-angle-up" style="color: white;"></i></asp:LinkButton></li>
                                    <li title="Move down One">
                                        <asp:LinkButton ID="cmd_MoveDown" runat="server" CommandName="moveDown" OnClick="cmd_MovetoTop_Click"><i class="fa fa-angle-down" style="color: white;"></i></asp:LinkButton></li>
                                    <li title="Move to Bottom">
                                        <asp:LinkButton ID="cmd_movetoBottom" runat="server" CommandName="move2Bottom" OnClick="cmd_MovetoTop_Click"><i class="fa fa-angle-double-down" style="color: white;"></i></asp:LinkButton></li>
                                    <li class="divider"></li>
                                    <li title="Cancel"><a href="#"><i class="fa fa-times-circle" style="color: white;"></i></a></li>
                                </ul>
                            </span>
                        </span>
                        <asp:ListBox AutoPostBack="true" Rows="7" OnSelectedIndexChanged="lbAppDefinitionList_SelectedIndexChanged" CssClass="form-control" ID="lbAppDefinitionList" runat="server"></asp:ListBox>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--Application Userlisting--%>




    <asp:UpdatePanel ID="userupdatepanel" runat="server">
        <ContentTemplate>
            <div id="UserinformationBlock" runat="server" visible="false" class="row">
                <div class="page-header">
                    <h3>Application User List</h3>
                </div>
                <asp:HiddenField ID="curappid" runat="server" />
                <asp:Repeater ID="UserList" runat="server">
                    <HeaderTemplate>
                        <table class="table table-responsive table-striped">
                            <thead>
                                <tr>
                                    <th><b>User Name</b></th>
                                    <th><b>Last login</b></th>
                                    <th><b>Accepted</b></th>
                                    <th><b>Is Admin</b></th>
                                    <th><b>Remove from App</b></th>

                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("AccountNM")%></td>
                            <td><%# CType(Eval("lastLoginDT"), DateTime).ToLongDateString%></td>
                            <td>
                                <span id="inroledtrue" runat="server" visible='<%# IsUserInroled(Eval("UserInroled"))%>' style="color: green;" class="glyphicon glyphicon-check"></span>
                                <span id="inroledfalse" runat="server" visible='<%# (IsUserInroled(Eval("UserInroled")) <> True)%>' style="color: green;" class="glyphicon glyphicon-unchecked"></span>
                            </td>
                            <td>
                                <asp:LinkButton ID="cmd_AdminChange" data-userid='<%# Eval("ApplicationUserID")%>' OnClick="cmd_AdminChange_Click" runat="server">
                                    <span id="Span1" runat="server" visible='<%# IsUserAppAdmin(Eval("IsUserAdmin"))%>' style="color: green;" class="glyphicon glyphicon-check"></span>
                                    <span id="Span2" runat="server" visible='<%# (IsUserAppAdmin(Eval("IsUserAdmin")) <> True)%>' style="color: green;" class="glyphicon glyphicon-unchecked"></span>
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="cmd_removeUser" runat="server" data-userid='<%# Eval("ApplicationUserID")%>' OnClientClick="return confirm('Are you sure you want to delete?');" OnClick="cmd_removeUser_Click"><span style="color:red;" class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>

                <div class="page-header">
                    <h3>available User List</h3>
                </div>
                <asp:Repeater ID="availableUserList" runat="server">
                    <HeaderTemplate>
                        <table class="table table-responsive table-striped">
                            <thead>
                                <tr>
                                    <th><b>User Name</b></th>
                                    <th><b>Last login</b></th>

                                    <th><b>Add To Application</b></th>

                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("DisplayName")%></td>
                            <td><%# CType(Eval("lastLoginDT"), DateTime).ToLongDateString%></td>

                            <td>
                                <asp:LinkButton ID="cmd_AddUser" runat="server" data-userid='<%# Eval("ApplicationUserID")%>' OnClick="cmd_AddUser_Click"><span style="color:green;" class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
