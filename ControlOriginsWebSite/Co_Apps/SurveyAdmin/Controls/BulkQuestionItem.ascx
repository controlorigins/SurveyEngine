<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BulkQuestionItem.ascx.vb" Inherits="Co_Apps_SurveyAdmin_Controls_BulkQuestionItem" %>

<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>

<asp:HiddenField ID="hfQuestionID" runat="server" />

<asp:Panel ID="pnlApplicatonDetail" runat="server" CssClass="panel panel-primary">
    <div class="panel-heading">
        <h4>Bulk Question Entry</h4>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12">
                <div class="form-group" style="background-color:bisque;">
                    <asp:Label ID="labeltbQuestionNM" runat="server" AssociatedControlID="tbQuestionNM">Questions (Each Carriage Return is a different question) :</asp:Label>
                    <asp:TextBox ID="tbQuestionNM" runat="server" CssClass="form-control" TextMode="MultiLine" Height="200px" Rows="10" Width="100%"></asp:TextBox>

                    <asp:Label ID="labeltbQuestionShortNM" runat="server" AssociatedControlID="tbQuestionShortNM">Short (Pattern a dash and number will be added i.e. '-1'):</asp:Label>
                    <asp:TextBox ID="tbQuestionShortNM" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbQuestionSort" runat="server" AssociatedControlID="tbQuestionSort" Text="1">Question Sort Start (Integer, will add 1 for each question):</asp:Label>
                    <asp:TextBox ID="tbQuestionSort" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>
                </div></div>
            <div class="col-lg-4 col-md-4 col-sm-12">
                <div class="form-group">

                    <asp:Label ID="labeltbQuestionValue" runat="server" AssociatedControlID="tbQuestionValue" Text="1">Question Value:</asp:Label>
                    <asp:TextBox ID="tbQuestionValue" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labelddlReviewRoleList" runat="server" AssociatedControlID="ddlReviewRoleList">Review Role:</asp:Label>
                    <asp:DropDownList ID="ddlReviewRoleList" runat="server" CssClass="form-control"></asp:DropDownList>

                    <asp:Label ID="labelddlSurveyType" runat="server" AssociatedControlID="ddlSurveyType">Category:</asp:Label>
                    <asp:DropDownList ID="ddlSurveyType" runat="server" CssClass="form-control"></asp:DropDownList>

                    <asp:Label ID="labelddlQuestionType" runat="server" AssociatedControlID="ddlQuestionType">Question Type:</asp:Label>
                    <asp:DropDownList ID="ddlQuestionType" runat="server" CssClass="form-control" DataSourceID="QuestionTypeDataSource" DataTextField="Name" DataValueField="Value"></asp:DropDownList>
                    <asp:ObjectDataSource ID="QuestionTypeDataSource" runat="server" SelectMethod="GetQuestionTypeList" TypeName="CODataCon.DataControler"></asp:ObjectDataSource>

                    <asp:Label ID="labelddlUnitOfMeasure" runat="server" AssociatedControlID="ddlUnitOfMeasure">Unit of Measure:</asp:Label>
                    <asp:DropDownList ID="ddlUnitOfMeasure" runat="server" CssClass="form-control"></asp:DropDownList>

                </div>
                <div class="form-group">
                    <asp:LinkButton ID="cmd_SaveQuestion" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_SaveQuestion_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_CancelQuestion" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_CancelQuestion_Click"></asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="panel-footer"></div>
    </div>
</asp:Panel>

