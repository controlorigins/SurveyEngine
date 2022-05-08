<%@ Control Language="VB" AutoEventWireup="false" CodeFile="availibleApps.ascx.vb" Inherits="Co_Apps_ControlOrigins_availibleApps_Old" %>
<%@ Register Src="~/controls/AlertBox.ascx" TagPrefix="uc1" TagName="AlertBox" %>

<asp:Panel ID="content" runat="server">
<div id="page-wrapper">
    <div class="col-lg-12">
        <h1>Available Projects</h1>
        <asp:Repeater ID="availableApps" runat="server">
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
                            <asp:LinkButton data-appid='<%# Eval("ApplicationID")%>' CssClass="btn btn-primary" ID="cmd_SubscribetoApp" OnClick="cmd_SubscribetoApp_Click" runat="server"><span style="color:green;" class="glyphicon glyphicon-download-alt"></span> Subscribe to this Project</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <uc1:AlertBox runat="server" ID="AlertBox" />
    </div>
</div>
</asp:Panel>