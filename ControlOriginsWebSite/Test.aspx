<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Test.aspx.vb" Inherits="Test" %>
<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <meta name="description" content="ControlOrigins Portal" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />


    <script type="text/javascript" src="https://code.jquery.com/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.11.4/jquery-ui.min.js"></script>

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="https://cdn.datatables.net/1.10.7/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/plug-ins/1.10.7/integration/bootstrap/3/dataTables.bootstrap.js"></script>

    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />

    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/plug-ins/1.10.7/integration/bootstrap/3/dataTables.bootstrap.css" />

    <style type="text/css">
        body {
            padding-top: 70px;
        }

        .container {
            width: 98%;
        }
    </style>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $('table.data_table').dataTable();
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    Please Wait. . .
                </ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>

                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="labelddlApplicationType" runat="server" AssociatedControlID="ddlApplicationType">Project Type</asp:Label>
                            <asp:DropDownList ID="ddlApplicationType" runat="server" CssClass="form-control" AutoPostBack="True" DataSourceID="ObjectDataSourceApplicationType" DataTextField="ApplicationTypeNM" DataValueField="ApplicationTypeID"></asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSourceApplicationType" runat="server" SelectMethod="GetApplicationTypeList" TypeName="CODataCon.DataControler"></asp:ObjectDataSource>

                            <asp:Label ID="labelddlCategory" runat="server" AssociatedControlID="ddlCategory">Survey Category:</asp:Label>
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" AutoPostBack="True" DataSourceID="ObjectDataSourceSurveyType" DataTextField="SurveyTypeNM" DataValueField="SurveyTypeID">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSourceSurveyType" runat="server" SelectMethod="GetSurveyCategoryListByApplicationTypeID" TypeName="CODataCon.DataControler">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlApplicationType" Name="reqApplicationTypeID" PropertyName="SelectedValue" Type="Int32" DefaultValue="-1" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:Label ID="labelddlSubCategory" runat="server" AssociatedControlID="ddlSubCategory">Question Category:</asp:Label>
                            <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control" DataSourceID="ObjectDataSourceSubCategory" DataTextField="SurveyTypeNM" DataValueField="SurveyTypeID" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>

                            <asp:ObjectDataSource ID="ObjectDataSourceSubCategory" runat="server" SelectMethod="GetQuestionCategoryListByParentCategoryID" TypeName="CODataCon.DataControler">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlCategory" Name="reqSurveyTypeID" PropertyName="SelectedValue" Type="Int32" DefaultValue="-1" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <uc1:displaytable runat="server" id="dtList" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>




            <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />



            <asp:Literal ID="litMain" runat="server" Visible="false"></asp:Literal>
            <asp:Panel runat="server" ID="pnlEdit" Visible="false" CssClass="panel panel-primary">
                <div class="panel-heading">Bob</div>
                <div class="panel-body">
                    <div class="form-group">
                        <asp:Label ID="labelFileUpload" runat="server" AssociatedControlID="FileUpload">File:</asp:Label>
                        <asp:FileUpload ID="FileUpload" runat="server" CssClass="form-control" />
                        <asp:LinkButton ID="cmd_SaveFile" runat="server" CssClass="btn btn-primary" OnClick="cmd_SaveFile_Click">Save</asp:LinkButton>
                    </div>
                    <div class="panel-footer"></div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
