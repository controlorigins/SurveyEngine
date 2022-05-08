<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AlertBox.ascx.vb" Inherits="controls_AlertBox" %>
            <asp:panel ID="NoApplications" runat="server">
               
                <div id="alertbox" runat="server" class="alert alert-dismissable alert-warning">
                    <button id="closebutton" runat="server" data-dismiss="alert" class="close" type="button"><span class="glyphicon glyphicon-remove-sign"></span></button>
                    <h4><%= boldnote%></h4>
                    <p><%= message%></p>
                </div>
            </asp:panel>