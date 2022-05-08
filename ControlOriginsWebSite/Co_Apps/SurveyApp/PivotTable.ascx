<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PivotTable.ascx.vb" Inherits="Co_Apps_SurveyApp_ApplicationDataPresentation" %>

<!-- external libs from cdnjs -->
<link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/c3/0.4.10/c3.min.css">
<link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.4.2/chosen.min.css">


<!-- PivotTable.js libs from ../dist -->
<script type="text/javascript" src="<%=RootPath %>/pivottable/dist/pivot.js"></script>
<script type="text/javascript" src="<%=RootPath %>/pivottable/dist/export_renderers.js"></script>
<script type="text/javascript" src="<%=RootPath %>/pivottable/dist/d3_renderers.js"></script>
<script type="text/javascript" src="<%=RootPath %>/pivottable/dist/c3_renderers.js"></script>


<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/d3/3.5.5/d3.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui-touch-punch/0.2.3/jquery.ui.touch-punch.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-csv/0.71/jquery.csv-0.71.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.4.2/chosen.jquery.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/c3/0.4.10/c3.min.js"></script>
<style>
    body {
        font-family: Verdana;
    }
</style>

<link rel="stylesheet" type="text/css" href="<%=RootPath %>/pivottable/dist/pivot.css">

<div id="page-wrapper">
    <div class="panel panel-default">
        <div class="panel-heading">Configuration</div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-10 form-inline">
                    <div class="form-group">
                        <label for="ddlConfig">Select Data:</label>
                        <asp:DropDownList ID="ddlDataSetName" runat="server" ClientIDMode="Static" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlDataSetName_SelectedIndexChanged"></asp:DropDownList>
                        <label for="tbName">New Visualization:</label>
                        <asp:TextBox ID="tbName" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                        <label for="ddlConfig">Select Visualization:</label>
                        <asp:DropDownList ID="ddlConfig" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlConfig_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                        <asp:LinkButton ID="cmd_SaveVisualization" runat="server" Text="Save Config" CssClass="btn btn-primary" OnClick="cmd_SaveVisualization_Click"></asp:LinkButton>
                        <asp:LinkButton ID="cmd_Delete" runat="server" Text="Delete Config" CssClass="btn btn-warning" OnClick="cmd_Delete_Click"></asp:LinkButton>
                    </div>
                </div>
                <div class="col-lg-2">
                </div>
            </div>
        </div>
        <div class="panel-footer">
            <div class="row">
                <div class="col-lg-6">
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function () {
            var derivers = $.pivotUtilities.derivers;
            var renderers = $.extend(
                $.pivotUtilities.renderers,
                $.pivotUtilities.c3_renderers,
                $.pivotUtilities.d3_renderers,
                $.pivotUtilities.export_renderers
                );
            $("#output").pivotUI(<%=JSONGrid %> ,
                {
                    <%=PivotParms %>
                    renderers: renderers,
                    onRefresh: function (config) {
                        var config_copy = JSON.parse(JSON.stringify(config));
                        //delete some values which are functions
                        delete config_copy["renderers"];
                        delete config_copy["aggregators"];
                        delete config_copy["derivedAttributes"];
                        //delete some bulky default values
                        delete config_copy["rendererOptions"];
                        delete config_copy["localeStrings"];
                        //$("#config_json").text(JSON.stringify(config_copy, undefined, 2));
                        $("#tbJSON").text(JSON.stringify(config_copy, undefined, 2));
                    }
                }
             );
        });
    </script>

    <div id="output" style="margin: 10px;"></div>

    <asp:TextBox ID="tbJSON" runat="server" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
