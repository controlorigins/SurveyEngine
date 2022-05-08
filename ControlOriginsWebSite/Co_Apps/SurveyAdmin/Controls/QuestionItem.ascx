<%@ Control Language="VB" AutoEventWireup="false" CodeFile="QuestionItem.ascx.vb" Inherits="Co_Apps_SurveyAdmin_Controls_QuestionItem" %>

<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>

<asp:HiddenField ID="hfQuestionID" runat="server" />
<asp:HiddenField ID="hfQuestionAnswerID" runat="server" />

<asp:Panel ID="pnlApplicatonDetail" runat="server" CssClass="panel panel-primary">
    <div class="panel-heading">
        <h4>Question</h4>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-12">
                <div class="form-group">
                    <asp:Label ID="labeltbQuestionNM" runat="server" AssociatedControlID="tbQuestionNM">Question:</asp:Label>
                    <asp:TextBox ID="tbQuestionNM" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbQuestionShortNM" runat="server" AssociatedControlID="tbQuestionShortNM">Short:</asp:Label>
                    <asp:TextBox ID="tbQuestionShortNM" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbQuestionDS" runat="server" AssociatedControlID="tbQuestionDS">Description:</asp:Label>
                    <asp:TextBox ID="tbQuestionDS" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbKeywords" runat="server" AssociatedControlID="tbKeywords">Keywords:</asp:Label>
                    <asp:TextBox ID="tbKeywords" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbQuestionValue" runat="server" AssociatedControlID="tbQuestionValue" Text="1">Question Value:</asp:Label>
                    <asp:TextBox ID="tbQuestionValue" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labeltbQuestionSort" runat="server" AssociatedControlID="tbQuestionSort" Text="1">Question Sort:</asp:Label>
                    <asp:TextBox ID="tbQuestionSort" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="labelddlReviewRoleList" runat="server" AssociatedControlID="ddlReviewRoleList">Review Role:</asp:Label>
                    <asp:DropDownList ID="ddlReviewRoleList" runat="server" CssClass="form-control"></asp:DropDownList>

                    <asp:Label ID="labelddlSurveyType" runat="server" AssociatedControlID="ddlSurveyType">Category:</asp:Label>
                    <asp:DropDownList ID="ddlSurveyType" runat="server" CssClass="form-control"></asp:DropDownList>

                    <asp:Label ID="labelddlQuestionType" runat="server" AssociatedControlID="ddlQuestionType">Question Type:</asp:Label>
                    <asp:DropDownList ID="ddlQuestionType" runat="server" CssClass="form-control" DataSourceID="QuestionTypeDataSource" DataTextField="Name" DataValueField="Value"></asp:DropDownList>

                    <asp:ObjectDataSource ID="QuestionTypeDataSource" runat="server" SelectMethod="GetQuestionTypeList" TypeName="CODataCon.DataControler"></asp:ObjectDataSource>

                    <asp:Label ID="labelddlUnitOfMeasure" runat="server" AssociatedControlID="ddlUnitOfMeasure">Unit of Measure:</asp:Label>
                    <asp:DropDownList ID="ddlUnitOfMeasure" runat="server" CssClass="form-control"></asp:DropDownList>

                    <asp:Label ID="labelFileUpload" runat="server" AssociatedControlID="FileUpload">Image:</asp:Label>
                    <asp:FileUpload ID="FileUpload" runat="server" CssClass="form-control" />

                </div>
                <div class="form-group">
                    <asp:LinkButton ID="cmd_SaveQuestion" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_SaveQuestion_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_CancelQuestion" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_CancelQuestion_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_DeleteQuestion" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_DeleteQuestion_Click"></asp:LinkButton>
                    <asp:LinkButton ID="cmd_BulkQuestion" runat="server" Text="Bulk Clone" CssClass="btn btn-info" OnClick="cmd_BulkQuestion_Click"></asp:LinkButton>
                </div>
                <div class="form-group">
                    <asp:Image ID="QuestionImage" runat="server" Visible="false" />
                </div>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-12">
                <uc1:DisplayTable runat="server" ID="dtQuestionAnswer" Visible="false" EnableCSV="false" />
                <uc1:DisplayTable runat="server" ID="dtSurvey" Visible="false" EnableCSV="false" />
                <uc1:DisplayTable runat="server" ID="dtResponseAnswers" Visible="false" EnableCSV="false" />
            </div>
        </div>
        <div class="panel-footer"></div>
    </div>
</asp:Panel>

<asp:Panel ID="pnlQuestionAnswer" runat="server" CssClass="panel panel-primary" Visible="true">
    <div class="panel-heading">
        <h4>Question Answer</h4>
    </div>
    <div class="panel-body">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="form-group">
                <asp:Label ID="labeltbQuestionAnswerNM" runat="server" AssociatedControlID="tbQuestionAnswerNM">Answer Name:</asp:Label>
                <asp:TextBox ID="tbQuestionAnswerNM" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Label ID="labeltbQuestionAnswerShortNM" runat="server" AssociatedControlID="tbQuestionAnswerShortNM">Short Name:</asp:Label>
                <asp:TextBox ID="tbQuestionAnswerShortNM" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Label ID="labeltbQuestionAnswerValue" runat="server" AssociatedControlID="tbQuestionAnswerValue">Value:</asp:Label>
                <asp:TextBox ID="tbQuestionAnswerValue" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Label ID="labeltbQuestionAnswerSort" runat="server" AssociatedControlID="tbQuestionAnswerSort">Sort:</asp:Label>
                <asp:TextBox ID="tbQuestionAnswerSort" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Label ID="labelcbQuestionAnswerActiveFL" runat="server" AssociatedControlID="cbQuestionAnswerActiveFL">Active:</asp:Label>
                <asp:CheckBox ID="cbQuestionAnswerActiveFL" runat="server" class="form-control" />

                <asp:Label ID="labelcbQuestionAnswerCommentFL" runat="server" AssociatedControlID="cbQuestionAnswerCommentFL">Comment:</asp:Label>
                <asp:CheckBox ID="cbQuestionAnswerCommentFL" runat="server" class="form-control" />

                <asp:Label ID="labeltbQuestionAnswerDS" runat="server" AssociatedControlID="tbQuestionAnswerDS">Description:</asp:Label>
                <asp:TextBox ID="tbQuestionAnswerDS" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="form-group">
                <asp:LinkButton ID="cmd_SaveQuestionAnswer" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_SaveQuestionAnswer_Click"></asp:LinkButton>
                <asp:LinkButton ID="cmd_CancelQuestionAnswer" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_CancelQuestionAnswer_Click"></asp:LinkButton>
                <asp:LinkButton ID="cmd_DeleteQuestionAnswer" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_DeleteQuestionAnswer_Click"></asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="panel-footer"></div>
</asp:Panel>
