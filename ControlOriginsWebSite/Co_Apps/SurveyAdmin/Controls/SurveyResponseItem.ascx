<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyResponseItem.ascx.vb" Inherits="Co_Apps_SurveyAdmin_Controls_SurveyResponseItem" %>


<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>


<asp:Panel ID="pnlApplicatonDetail" runat="server" CssClass="panel panel-primary">
    <div class="panel-heading">
        <h4>SurveyResponse</h4>
    </div>
    <div class="panel-body">
        <asp:HiddenField ID="hfSurveyResponseID" runat="server" />
        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-12">
                <div class="form-group">
                    <asp:Label ID="labeltbSurveyResponseNM" runat="server" AssociatedControlID="tbSurveyResponseNM">SurveyResponse:</asp:Label>
                    <asp:TextBox ID="tbSurveyResponseNM" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labelddlApplicationUser" runat="server" AssociatedControlID="ddlApplicationUser">Assigned User:</asp:Label>
                    <asp:DropDownList ID="ddlApplicationUser" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                    <asp:Label ID="labelddlStatus" runat="server" AssociatedControlID="ddlStatus">Status:</asp:Label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>

                    <asp:Label ID="labellbApplicationNM" runat="server" AssociatedControlID="lbApplicationNM">Application:</asp:Label>
                    <asp:LinkButton ID="lbApplicationNM" runat="server" CssClass="form-control"></asp:LinkButton>

                    <asp:Label ID="labellbSurveyNM" runat="server" AssociatedControlID="lbSurveyNM">Survey:</asp:Label>
                    <asp:LinkButton ID="lbSurveyNM" runat="server" CssClass="form-control"></asp:LinkButton>

                    <asp:Label ID="labeltbSurveyResponseDS" runat="server" AssociatedControlID="tbSurveyResponseDS">Description:</asp:Label>
                    <asp:TextBox ID="tbSurveyResponseDS" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbSurveyResponseScore" runat="server" AssociatedControlID="tbSurveyResponseScore">Score:</asp:Label>
                    <asp:TextBox ID="tbSurveyResponseScore" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

                    <asp:Label ID="labeltbDataSource" runat="server" AssociatedControlID="tbDataSource">Data Source:</asp:Label>
                    <asp:TextBox ID="tbDataSource" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

                    <asp:Label ID="labeltbResponseHistory" runat="server" AssociatedControlID="tbResponseHistory">History:</asp:Label>
                    <asp:TextBox ID="tbResponseHistory" runat="server" TextMode="MultiLine" Rows="10" CssClass="form-control" Enabled="false"></asp:TextBox>


                </div>
                <div class="form-group">
                    <asp:LinkButton ID="cmd_Save" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_Save_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_Cancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_Cancel_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_Reset" runat="server" Text="Reset" CssClass="btn btn-warning" OnClick="cmd_Reset_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_Delete" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_Delete_Click"></asp:LinkButton>
                </div>
            </div>
            <div class="col-lg-9 col-md-9 col-sm-12">
                <uc1:DisplayTable runat="server" ID="dtSurveyResponseAnswer"  EnableCSV="false" />
                <uc1:DisplayTable runat="server" ID="dtHistory" EnableCSV="false" />
                <uc1:DisplayTable runat="server" ID="dtStateHistory" EnableCSV="false" />
           </div>
        </div>
        <div class="panel-footer"></div>
    </div>
</asp:Panel>
