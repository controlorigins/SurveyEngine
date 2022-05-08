<%@ Control Language="VB" AutoEventWireup="false" CodeFile="QuestionList.ascx.vb" Inherits="Co_Apps_SurveyApp_admin_QuestionList" %>
<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>


<div class="row">
    <div class="panel panel-default" id="SurveyChart">
        <div class="panel-heading">
            Select Category
        </div>
        <div class="panel-body">
            <asp:Label ID="lblddlQuestionCategory" runat="server" AssociatedControlID="ddlQuestionCategory">Category</asp:Label>
            <asp:DropDownList ID="ddlQuestionCategory" runat="server" Width="100%" AutoPostBack="True"  OnSelectedIndexChanged="ddlQuestionCategory_SelectedIndexChanged" DataSourceID="ObjectDataSource1" DataTextField="SurveyTypeNM" DataValueField="SurveyTypeID"></asp:DropDownList>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetQuestionCategoryList" TypeName="CODataCon.DataControler"></asp:ObjectDataSource>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-lg-12">
        <uc1:DisplayTable runat="server" ID="dtList" />
    </div>
</div>
