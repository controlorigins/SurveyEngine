<%@ Control Language="VB" AutoEventWireup="false" CodeFile="QuickChart.ascx.vb" Inherits="Co_Apps_SurveyApp_QuickChart" %>
<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>



<script type="text/javascript">

    $(document).ready(function () {
        var myimg = $("img-responsive");
        myimg.attr("style", "");
        myimg.css("height", "auto");
        myimg.css("width", "100%");
    });


</script>


<div class="form panel panel-primary">
    <div class="panel-heading">
        <h3>Chart and Configuration</h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-lg-8 form-inline">
                <div class="form-group">
                    <label for="ddlConfig">Select Visualization:</label>
                    <asp:DropDownList ID="ddlConfig" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlConfig_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                    <asp:LinkButton ID="cmd_SaveVisualization" runat="server" Text="Update Config" OnClick="cmd_SaveVisualization_Click" CssClass="btn btn-primary"></asp:LinkButton>
                </div>
            </div>
            <div class="col-lg-4">
            </div>
        </div>

        <div class="row">
            <div class="col-lg-3 col-md-4 col-sm=4 col-xs-12">
            </div>
            <div class="col-lg-3 col-md-4 col-sm=4 col-xs-12">
                <div class="form-group">
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-4 col-sm=4 col-xs-12">
                <div class="form-group">
                    <label>Title:</label>
                    <asp:TextBox ID="tbTitle" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>SubTitle:</label>
                    <asp:TextBox ID="tbSubTitle" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-lg-3 col-md-4 col-sm=4 col-xs-12">
                <div class="form-group">
                    <label>Background Image:</label>
                    <asp:DropDownList ID="ddlBackgroundImage" runat="server" CssClass="form-control">
                        <asp:ListItem Selected="True" Value="">No Image</asp:ListItem>
                        <asp:ListItem Value="~/images/WorldGlobe.jpg">WorldGlobe</asp:ListItem>
                        <asp:ListItem Value="~/images/slider_img_1.jpg">Control Origins 1</asp:ListItem>
                        <asp:ListItem Value="~/images/slider_img_2.jpg">Control Origins 2</asp:ListItem>
                        <asp:ListItem Value="~/images/slider_img_3.jpg">Control Origins 3</asp:ListItem>
                        <asp:ListItem Value="~/images/slider_img_4.jpg">Control Origins 4</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-lg-3 col-md-4 col-sm=4 col-xs-12">
                <div class="form-group">
                    <label>Chart Type:</label>
                    <asp:DropDownList ID="ddlChartType" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Chart Palette:</label>
                    <asp:DropDownList ID="ddlChartPalette" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Chart Style:</label>
                    <asp:DropDownList ID="ddlChartStyle" runat="server" CssClass="form-control">
                        <asp:ListItem>2D</asp:ListItem>
                        <asp:ListItem>3D</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm=12 col-xs-12">
                <asp:LinkButton ID="cmd_SetChartConfig" runat="server" OnClick="cmd_SetChartConfig_Click" CssClass="btn btn-primary">Update Chart</asp:LinkButton>
                <br />
                <br />
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm=12 col-xs-12">
                <asp:Chart ID="myChart" runat="server" Height="400px" Width="900px" BorderlineColor="14, 89, 168" BorderlineDashStyle="Solid" BorderlineWidth="5" CssClass="img-responsive">
                </asp:Chart>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm=6 col-xs-12">
                <div class="form-group">
                    <label>Data Set:</label>
                    <asp:DropDownList ID="ddlDataSetName" runat="server" CssClass="form-control" Enabled="false">
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Chart Value Field:</label>
                    <asp:DropDownList ID="ddlValueColumn" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Value Calculation Method:</label>
                    <asp:DropDownList ID="ddlCalc" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                </div>

                <div class="form-group">
                    <label>Series:</label>
                    <asp:DropDownList ID="ddlSeries" runat="server" Enabled="false" CssClass="form-control"></asp:DropDownList>
                    <asp:HiddenField ID="hfSeries" runat="server" />
                    <asp:CheckBoxList ID="cbSeries" runat="server" Height="70px" Enabled="false">
                    </asp:CheckBoxList>
                    <asp:LinkButton ID="lbSeriesData" runat="server" Text="Select Series Data" CssClass="btn btn-primary"></asp:LinkButton>
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm=6 col-xs-12">
                <div class="form-group">
                    <label>Axis:</label>
                    <asp:DropDownList ID="ddlAxis" runat="server" Enabled="false" CssClass="form-control"></asp:DropDownList>
                    <asp:HiddenField ID="hfAxis" runat="server" />
                    <asp:CheckBoxList ID="cbAxis" runat="server" Height="70px" Enabled="false">
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm=6 col-xs-12">
                <div class="form-group">
                    <label>Filter:</label>
                    <asp:DropDownList ID="ddlFilter" runat="server" Enabled="false" CssClass="form-control"></asp:DropDownList>
                    <asp:HiddenField ID="hfFilter" runat="server" />
                    <asp:CheckBoxList ID="cbFilter" runat="server" Height="70px" Enabled="false">
                    </asp:CheckBoxList>
                </div>
            </div>
        </div>




    </div>
    <div class="panel-footer">
    </div>

    <uc1:DisplayTable runat="server" ID="dtList" />

</div>
