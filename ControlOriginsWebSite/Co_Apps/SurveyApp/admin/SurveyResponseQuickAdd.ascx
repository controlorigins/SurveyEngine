<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyResponseQuickAdd.ascx.vb" Inherits="Co_Apps_SurveyApp_controls_SurveyResponseQuickAdd" %>
<%@ Register Src="~/Co_Apps/SurveyApp/admin/SurveyResponseForm.ascx" TagPrefix="uc1" TagName="SurveyResponseForm" %>

<!-- Button trigger modal -->
<button type="button" class="btn btn-primary " data-toggle="modal" data-target='#SurveyResponseModal<%=SurveyResponseID %>' >
    <span id="glyphplus" runat="server" class="glyphicon glyphicon-plus"></span><span class="glyphicon glyphicon-th"></span>
</button>
<asp:HiddenField runat="server" ID="hfSurveyResponseID" />

<!-- Modal -->
<div class="modal fade" id='SurveyResponseModal<%=SurveyResponseID %>' tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                <h4 class="modal-title" id="myModalLabel"><span class="glyphicon glyphicon-home"></span>&nbsp; Add New Data Set</h4>
            </div>
            <div class="modal-body">
                <uc1:SurveyResponseForm runat="server" ID="SurveyResponseForm1" />         
            </div>
            <div class="modal-footer">
                <asp:LinkButton ID="CMD_Cancel" runat="server" type="button" class="btn btn-default" data-dismiss="modal">Close</asp:LinkButton>
                <asp:LinkButton ID="CMD_Save" runat="server"  type="button" class="btn btn-primary" OnClick="CMD_Save_Click" >Save changes</asp:LinkButton>
            </div>
        </div>
    </div>
</div>