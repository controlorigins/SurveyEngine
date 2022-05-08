<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyResponseScore.ascx.vb" Inherits="Co_Apps_SurveyApp_SurveyResponseScore" %>

<style type="text/css">
    button,
    input,
    optgroup,
    select,
    textarea {
        margin: 0;
    }

        input.COSurvey_TextBox, textarea.COSurvey_TextBox {
            width: 100%;
        }

    ul.COSurvey_CheckBoxList,
    ul.COSurvey_CheckBoxList li,
    ul.COSurvey_RadioButtonList,
    ul.COSurvey_RadioButtonList li {
        list-style: none;
        list-style-type: none;
        background-color: transparent;
        padding-left: 15px;
        margin-left: 0px;
        left: 0px;
    }

        ul.COSurvey_CheckBoxList li,
        ul.COSurvey_RadioButtonList li {
            background-color: transparent;
            background-image: none;
            background-repeat: no-repeat;
            padding-left: 0px;
            margin-left: 0px;
            left: 0px;
        }

            ul.COSurvey_CheckBoxList li:hover,
            ul.COSurvey_RadioButtonList li:hover,
            span.SurveyRadioListItemSelected label {
                background-color: #ccc;
                color: #000;
            }

            ul.COSurvey_CheckBoxList li label,
            ul.COSurvey_RadioButtonList li label {
                display: block;
                margin-left: 20px;
            }

            ul.COSurvey_CheckBoxList li input,
            ul.COSurvey_RadioButtonList li input {
                float: left;
            }
</style>

<script type="text/javascript">

    $(document).ready(function () {
        $("[data-toggle='tooltip']").tooltip();

        $('.COSurvey_RadioButtonList').on('click', function () {

            if ($(this).is(':checked')) {
                alert('checked');
            }
        });


    });



    function Show() {
        $("#dialog-modal:ui-dialog").dialog("destroy");
        $("#dialog-modal").dialog({
            height: 650,
            width: 900,
            modal: true,
            title: 'Control Origins Video',
            closeOnEscape: false,
            dialogClass: 'no-close',
            show: {
                effect: "blind",
                duration: 1000
            }
        });
    }

    function Close() {
        $("#dialog-modal").dialog("close");
        $("#dialog-modal:ui-dialog").dialog("destroy");
    }


</script>

<div class="container" style="width: 100%;">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <%=SurveyResponseNM%> (<%=StatusNM%>)<br />
                    <%=mySurveyResponse.CurrentQuestionGroup.QuestionGroupNM%>
                    <asp:Repeater ID="GroupMenu" runat="server">
                        <HeaderTemplate>
                            <ul class="nav nav-pills">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li class='<%# Eval("CssClass")%>'>
                                <asp:LinkButton ID="cmd_GroupSelection" runat="server" OnClick="cmd_GroupSelection_Click" data-quesitongroupid='<%# Eval("QuestionGroupID")%>' ToolTip='<%# Eval("QuestionGroupDS")%>'>
                                <%# Eval("QuestionGroupOrder")%>. <%# Eval("QuestionGroupNM")%></asp:LinkButton></li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                    <%=mySurveyResponse.CurrentQuestionGroup.QuestionGroupHeader%>
                </div>
                <div id="content" runat="server" class="panel-body"></div>
                <div id="submitcancel" runat="server" class="panel-body">
                    <asp:LinkButton ValidationGroup="SurveyApp" ID="btn_Submit" runat="server" CssClass="btn btn-primary" Text="Save Response" OnClick="btn_Submit_Click"></asp:LinkButton>
                    <asp:LinkButton ValidationGroup="SurveyApp" ID="btn_Cancel" runat="server" CssClass="btn btn-primary" Text="Cancel" OnClick="btn_Cancel_Click" CausesValidation="false"></asp:LinkButton>
                </div>
                <div class="panel-footer">
                    <%=mySurveyResponse.CurrentQuestionGroup.QuestionGroupFooter%>
                </div>
            </div>
        </div>
    </div>
</div>

