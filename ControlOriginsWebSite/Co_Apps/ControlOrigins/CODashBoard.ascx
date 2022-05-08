<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CODashBoard.ascx.vb" Inherits="Co_Apps_ControlOrigins_CODashBoard" %>
<%@ Register Src="~/controls/AlertBox.ascx" TagPrefix="uc1" TagName="AlertBox" %>
<%@ Register Src="~/controls/Co_HTML.ascx" TagPrefix="uc1" TagName="Co_HTML" %>

<div id="page-wrapper">
    <div class="col-lg-12">
        <div class="form-group>">
            <div class="col-lg=12">
                <h1>Your Projects</h1>
                <uc1:AlertBox runat="server" ID="YourAppAlert" Visible="false" />
                <asp:Repeater ID="RegisteredApps" runat="server">
                    <ItemTemplate>
                        <div class="col-lg-4 col-md-6 col-sm-6">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h5><%# Eval("ApplicationNM")%></h5>
                                </div>
                                <div class="panel-body">
                                    <small><i><%# Eval("ApplicationDS")%></i></small><br />
                                </div>
                                <div class="panel-footer">
                                    <asp:LinkButton OnClick="cmd_LoadApp_Click" data-appid='<%# Eval("ApplicationID")%>' CssClass="btn btn-primary" ID="cmd_LoadApp" runat="server"><span style="color:green;" class="glyphicon glyphicon-off"></span> Open Project</asp:LinkButton>
                                    <asp:LinkButton OnClick="cmd_RemoveApp_Click" ID="cmd_RemoveApp" runat="server" data-appid='<%# Eval("ApplicationID")%>' CssClass="btn btn-warning"><span  class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</div>
