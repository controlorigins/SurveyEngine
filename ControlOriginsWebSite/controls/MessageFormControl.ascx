<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MessageFormControl.ascx.vb" Inherits="controls_MessageFormControl" %>
<%--<link href="../bootstrap/css/bootstrap.css" rel="stylesheet" />--%>

<asp:LinkButton ID="cmd_NewMESSAGE" runat="server" type="button" class="btn btn-primary btn-lg" data-toggle="modal" data-target="#NewMessageModal"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
<div class="modal fade" id="NewMessageModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <label for="ddtoUsers">To:</label>
                    <asp:DropDownList ID="ddtoUsers" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="tbsubject">Subject</label>
                    <asp:TextBox ID="tbsubject"  runat="server" CssClass="form-control" placeholder="Enter Subject" ></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="tbmessage">Message</label>
                    <asp:TextBox ID="tbmessage" Rows="5" runat="server" CssClass="form-control" placeholder="Enter message" Height="72px" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div id="AdminPanel" runat="server" visible="false" >
                    <h3>Admin Test</h3>
                </div>
                <div class="form-group">
                    <asp:LinkButton ID="cmd_SendMessage" runat="server" OnClick="cmd_SendMessage_Click" CssClass="btn btn-primary">Send</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-warning" data-dismiss="modal">Cancel</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</div>