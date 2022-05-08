<%@ Control Language="VB" AutoEventWireup="false" CodeFile="QuestionBank.ascx.vb" Inherits="Co_Apps_SurveyAdmin_Controls_QuestionBank" %>
<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>


<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
    <ContentTemplate>
        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <img runat="server" id="img_PleaseWait" alt="Please Wait" src="~/images/updateprogress.gif" />
                <br />
                Please Wait. . .
            </ProgressTemplate>
        </asp:UpdateProgress>
        <script type="text/javascript">
            Sys.Application.add_load(BindEvents);
        </script>
        <asp:HiddenField ID="hfQuestionID" runat="server" />
        <asp:HiddenField ID="hfQuestionAnswerID" runat="server" />

        <div class="panel panel-primary">
            <div class="panel-heading">
                <h4>Question Bank</h4>
            </div>
            <div class="panel-body">
                <asp:Panel runat="server" ID="pnlQuestionCriteria" CssClass="row" BackColor="PaleGoldenrod" Visible="true">
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <asp:Label ID="labelddlApplicationType" runat="server" AssociatedControlID="ddlApplicationType">Project Type:</asp:Label>
                        <asp:LinkButton ID="cmd_ProjectTypeEdit" runat="server" CssClass="btn btn-default" Text="Edit" Enabled="false"></asp:LinkButton>
                        <asp:LinkButton ID="cmd_ProjectTypeNew" runat="server" CssClass="btn btn-default" Text="New" Enabled="false"></asp:LinkButton>
                        <asp:DropDownList ID="ddlApplicationType" runat="server" CssClass="form-control" AutoPostBack="True" DataSourceID="ObjectDataSourceApplicationType" DataTextField="ApplicationTypeNM" DataValueField="ApplicationTypeID"></asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSourceApplicationType" runat="server" SelectMethod="GetApplicationTypeList" TypeName="CODataCon.DataControler"></asp:ObjectDataSource>
                        <br />
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <asp:Label ID="labelddlCategory" runat="server" AssociatedControlID="ddlCategory">Survey Category:</asp:Label>
                        <asp:LinkButton ID="cmd_SurveyCategoryEdit" runat="server" CssClass="btn btn-default" Text="Edit" OnClick="cmd_SurveyCategoryEdit_Click"></asp:LinkButton>
                        <asp:LinkButton ID="cmd_SurveyCategoryNew" runat="server" CssClass="btn btn-default" Text="New" OnClick="cmd_SurveyCategoryNew_Click"></asp:LinkButton>

                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" AutoPostBack="True" DataSourceID="ObjectDataSourceSurveyType" DataTextField="SurveyTypeNM" DataValueField="SurveyTypeID">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSourceSurveyType" runat="server" SelectMethod="GetSurveyCategoryListByApplicationTypeID" TypeName="CODataCon.DataControler">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlApplicationType" Name="reqApplicationTypeID" PropertyName="SelectedValue" Type="Int32" DefaultValue="-1" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <br />
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <asp:Label ID="labelddlQuestionCategory" runat="server" AssociatedControlID="ddlQuestionCategory">Question Category:</asp:Label>
                        <asp:LinkButton ID="cmd_QuestionCategoryEdit" runat="server" CssClass="btn btn-default" Text="Edit" OnClick="cmd_QuestionCategoryEdit_Click"></asp:LinkButton>
                        <asp:LinkButton ID="cmd_QuestionCategoryNew" runat="server" CssClass="btn btn-default" Text="New" OnClick="cmd_QuestionCategoryNew_Click"></asp:LinkButton>
                        <asp:DropDownList ID="ddlQuestionCategory" runat="server" CssClass="form-control" DataSourceID="ObjectDataSourceSubCategory" DataTextField="SurveyTypeNM" DataValueField="SurveyTypeID" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                        </asp:DropDownList>

                        <asp:ObjectDataSource ID="ObjectDataSourceSubCategory" runat="server" SelectMethod="GetQuestionCategoryListByParentCategoryID" TypeName="CODataCon.DataControler">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlCategory" Name="reqSurveyTypeID" PropertyName="SelectedValue" Type="Int32" DefaultValue="-1" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <br />
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlQuestionList" CssClass="row" Visible="true">
                    <div style="text-align: center;width:100%;">
                        <asp:LinkButton ID="cmd_CreateQuestion" runat="server" Text="Add New Question" OnClick="cmd_CreateQuestion_Click" CssClass="btn btn-primary"></asp:LinkButton>
                    </div>
                    <br />
                    <br />
                    <asp:Repeater ID="QuestionList" runat="server">
                        <HeaderTemplate>
                            <table class="data_table table table-striped">
                                <thead>
                                    <tr>
                                        <td>Order</td>
                                        <td>Question</td>
                                        <td>Short Name</td>
                                        <td>Description</td>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("SurveyDisplayOrder") %></td>
                                <td>
                                    <asp:LinkButton ID="cmd_selectQuestion" data-questionid='<%# Eval("QuestionID")%>' runat="server" OnClick="cmd_selectQuestion_Click"><%# eval("QuestionNM") %></asp:LinkButton>
                                </td>
                                <td>
                                    <%# eval("QuestionShortNM") %>
                                </td>
                                <td>
                                    <%# eval("QuestionDS") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <asp:Panel ID="pnlQuestionDetail" runat="server" CssClass="row" BackColor="LightGoldenrodYellow" Visible="true">
                    <div class="col-lg-5 col-md-5 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="labeltbQuestionNM" runat="server" AssociatedControlID="tbQuestionNM">Question:</asp:Label>
                            <asp:TextBox ID="tbQuestionNM" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>

                            <asp:Label ID="labeltbQuestionDS" runat="server" AssociatedControlID="tbQuestionDS">Description:</asp:Label>
                            <asp:TextBox ID="tbQuestionDS" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                            <asp:Label ID="labelddlQuestionType" runat="server" AssociatedControlID="ddlQuestionType">Question Type:</asp:Label>
                            <asp:DropDownList ID="ddlQuestionType" runat="server" CssClass="form-control" DataSourceID="QuestionTypeDataSource" DataTextField="Name" DataValueField="Value"></asp:DropDownList>
                            <asp:ObjectDataSource ID="QuestionTypeDataSource" runat="server" SelectMethod="GetQuestionTypeList" TypeName="CODataCon.DataControler"></asp:ObjectDataSource>
                            <asp:Label ID="labelFileUpload" runat="server" AssociatedControlID="FileUpload">Image:</asp:Label>
                            <asp:FileUpload ID="FileUpload" runat="server" CssClass="form-control" />

                        </div>
                        <div class="form-group">
                            <asp:LinkButton ID="cmd_SaveQuestion" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_SaveQuestion_Click"></asp:LinkButton>
                            <asp:LinkButton ID="cmd_SaveAsNewQuestion" runat="server" Text="Save As New" CssClass="btn btn-primary" OnClick="cmd_SaveAsNewQuestion_Click"></asp:LinkButton>
                            <asp:LinkButton ID="cmd_CancelQuestion" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_CancelQuestion_Click"></asp:LinkButton>
                            <asp:LinkButton ID="cmd_DeleteQuestion" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_DeleteQuestion_Click"></asp:LinkButton>
                        </div>
                        <div class="form-group">
                            <asp:Image ID="QuestionImage" runat="server" Visible="false" />
                        </div>
                    </div>
                    <div class="col-lg-7 col-md-7 col-sm-12">
                        <br />
                        <br />
                        <asp:Repeater ID="QuestionAnswerList" runat="server">
                            <HeaderTemplate>
                                <table class="data_table table table-striped">
                                    <thead>
                                        <tr>
                                            <td>Sort</td>
                                            <td>Answer</td>
                                            <td>Description</td>
                                            <td>Value</td>
                                            <td>Comment</td>
                                            <td>Active</td>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("QuestionAnswerSort") %></td>
                                    <td>
                                        <asp:LinkButton ID="cmd_selectQuestionAnswer" data-questionid='<%# Eval("QuestionID")%>' data-questionanswerid='<%# Eval("QuestionAnswerID")%>' runat="server" OnClick="cmd_selectQuestionAnswer_Click"><%# eval("QuestionAnswerNM") %></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%# eval("QuestionAnswerDS") %>
                                    </td>
                                    <td>
                                        <%# eval("QuestionAnswerValue") %>
                                    </td>
                                    <td>
                                        <%# eval("QuestionAnswerCommentFL") %>
                                    </td>
                                    <td>
                                        <%# eval("QuestionAnswerActiveFL") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlQuestionAnswer" runat="server" CssClass="panel panel-default" Visible="true">
                    <div class="panel-heading">
                        <h4>
                            <asp:Label ID="lblQuestionAnswerTitle" runat="server"></asp:Label>
                            - Answer</h4>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12 col-md-12 col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="labelQuestionAnswerNM" runat="server" AssociatedControlID="tbQuestionAnswerNM">Answer Name:</asp:Label>
                                <asp:TextBox ID="tbQuestionAnswerNM" runat="server" CssClass="form-control"></asp:TextBox>

                                <asp:Label ID="labelQuestionAnswerDS" runat="server" AssociatedControlID="tbQuestionAnswerDS">Description:</asp:Label>
                                <asp:TextBox ID="tbQuestionAnswerDS" runat="server" CssClass="form-control"></asp:TextBox>

                                <asp:Label ID="labelQuestionAnswerValue" runat="server" AssociatedControlID="tbQuestionAnswerValue">Value:</asp:Label>
                                <asp:TextBox ID="tbQuestionAnswerValue" runat="server" CssClass="form-control"></asp:TextBox>

                                <asp:Label ID="labelQuestionAnswerSort" runat="server" AssociatedControlID="tbQuestionAnswerSort">Sort:</asp:Label>
                                <asp:TextBox ID="tbQuestionAnswerSort" runat="server" CssClass="form-control"></asp:TextBox>

                                <asp:Label ID="labelQuestionAnswerActiveFL" runat="server" AssociatedControlID="cbQuestionAnswerActiveFL">Active:</asp:Label>
                                <asp:CheckBox ID="cbQuestionAnswerActiveFL" runat="server" class="form-control" />

                                <asp:Label ID="labelQuestionAnswerCommentFL" runat="server" AssociatedControlID="cbQuestionAnswerCommentFL">Comment:</asp:Label>
                                <asp:CheckBox ID="cbQuestionAnswerCommentFL" runat="server" class="form-control" />

                                <asp:Label ID="labelQuestionAnswerShortNM" runat="server" AssociatedControlID="tbQuestionAnswerShortNM">Short Name:</asp:Label>
                                <asp:TextBox ID="tbQuestionAnswerShortNM" runat="server" CssClass="form-control"></asp:TextBox>
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
                <asp:Panel ID="pnlQuestionCategory" runat="server" CssClass="panel panel-default" Visible="false">
                    <div class="panel-heading">
                        <h4>Category</h4>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-12">
                                <div class="form-group">
                                    <asp:Label ID="labeltbSurveyTypeNM" runat="server" AssociatedControlID="tbSurveyTypeNM">Name:</asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbSurveyTypeNM" ErrorMessage="(*)" Font-Bold="True" Font-Italic="False" Font-Size="Larger" ForeColor="Red" ToolTip="Required Information" ValidationGroup="CategoryValidation"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="tbSurveyTypeNM" runat="server" CssClass="form-control"></asp:TextBox>

                                    <asp:Label ID="labeltbSurveyTypeShortNM" runat="server" AssociatedControlID="tbSurveyTypeShortNM">Short Name:</asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbSurveyTypeShortNM" ErrorMessage="(*)" Font-Bold="True" Font-Italic="False" Font-Size="Larger" ForeColor="Red" ToolTip="Required Information" ValidationGroup="CategoryValidation"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="tbSurveyTypeShortNM" runat="server" CssClass="form-control"></asp:TextBox>

                                    <asp:Label ID="labeltbSurveyTypeDS" runat="server" AssociatedControlID="tbSurveyTypeDS">Description:</asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbSurveyTypeDS" ErrorMessage="(*)" Font-Bold="True" Font-Italic="False" Font-Size="Larger" ForeColor="Red" ToolTip="Required Information" ValidationGroup="CategoryValidation"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="tbSurveyTypeDS" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>

                                    <asp:Label ID="labeltbSurveyTypeComment" runat="server" AssociatedControlID="tbSurveyTypeComment">Comment:</asp:Label>
                                    <asp:TextBox ID="tbSurveyTypeComment" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>

                                    <asp:HiddenField ID="hfParentSurveyTypeID" runat="server" />
                                    <asp:HiddenField ID="hfCurSurveyTypeID" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:LinkButton ID="cmd_SaveSurveyType" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmd_SaveSurveyType_Click" ValidationGroup="CategoryValidation"></asp:LinkButton>
                                    <asp:LinkButton ID="cmd_CancelSurveyType" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmd_CancelSurveyType_Click" CausesValidation="False"></asp:LinkButton>
                                    <asp:LinkButton ID="cmd_DeleteSurveyType" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="cmd_DeleteSurveyType_Click" CausesValidation="False"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                    </div>
                </asp:Panel>
            </div>
            <div class="panel-footer">
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
