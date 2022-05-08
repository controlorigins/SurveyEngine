<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CoUserMessages.ascx.vb" Inherits="Co_Apps_ControlOrigins_CoUserMessages" %>
<%@ Register Src="~/controls/MessageFormControl.ascx" TagPrefix="uc1" TagName="MessageFormControl" %>
<div class="container">
    <!-- Modal -->
    <asp:Repeater ID="MyMessagesModals" runat="server" >
        <ItemTemplate>
            <div class="modal fade" id='<%# "myModal" & Eval("Id").ToString%>' tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal"><span style="color: grey;" class="glyphicon glyphicon-remove-circle" aria-hidden="true"></span><span class="sr-only">Close</span></button>
                            <h4 class="modal-title" id="myModalLabel"><%# Eval("Subject")%></h4>
                            from :&nbsp;  <%# GetUserDisplayName(Eval("FromUserID"))%>
                        </div>
                        <div class="modal-body">
                            <%# Eval("Message")%>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="cmd_replay" data-messageid='<%# Eval("ID")%>' runat="server" OnClick="cmd_replay_Click" type="button" class="btn btn-primary">Reply</asp:LinkButton>
                            <asp:LinkButton ID="cmd_deleteMess" runat="server" OnClick="cmd_deleteMessage_Click" type="button" data-messageid='<%# Eval("ID")%>' class="btn btn-danger">Delete</asp:LinkButton>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater> 
    
       <uc1:MessageFormControl runat="server" ID="MessageFormControl" />
    <br />    <br />

    <!-- List current message here -->
    <asp:GridView ID="messagelist" CssClass="table table-responsive" runat="server" Width="80%" AutoGenerateColumns="False" GridLines="None">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:LinkButton ID="cmd_Refress" OnClick="cmd_Refress_Click" CssClass="btn btn-sm" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                </HeaderTemplate>
                <ItemTemplate>
                    <!-- Button trigger modal -->
                    <asp:LinkButton ID="cmd_ViewMessage" CommandArgument='<%# Eval("ID")%>' runat="server" type="button" class="btn btn-primary btn-lg" data-toggle="modal" data-target='<%# "#myModal" & Eval("Id").ToString%>'>
                            <span class="glyphicon glyphicon-envelope"></span>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="From">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# GetUserDisplayName(Eval("FromUserID"))%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Received">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# CType(Eval("CratedDateTime"), Date).ToShortDateString%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Subject">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tools">
                <ItemTemplate>
                    <asp:LinkButton ID="cmd_deleteMessage" runat="server" data-MessageID='<%# Eval("ID")%>' OnClick="cmd_deleteMessage_Click" class="btn btn-danger btn-sm">
                    <span class="glyphicon glyphicon-trash"></span>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
        <EmptyDataTemplate>
            <h2>You have no messages.</h2>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:HiddenField ID="UserID" runat="server" />
    

</div>

