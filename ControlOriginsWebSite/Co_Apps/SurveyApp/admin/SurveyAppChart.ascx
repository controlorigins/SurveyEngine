<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyAppChart.ascx.vb" Inherits="Co_Apps_SurveyApp_controls_SurveyAppChart" %>
<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>

<asp:Panel ID="pnlChartConfiguration" runat="server" CssClass="form panel panel-default">
    <div class="panel-heading">
        <h3>Chart Configuration</h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-lg-3 col-md-4 col-sm=4 col-xs-12">
                <div class="form-group">
                    <label>Data Set:</label>
                    <asp:DropDownList ID="ddlDataSet" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDataSet_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem>Summary</asp:ListItem>
                        <asp:ListItem>Group</asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField ID="hfDataSet" runat="server" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm=6 col-xs-12">
                <div class="form-group">
                    <label>Series:</label>
                    <asp:DropDownList ID="ddlSeries" runat="server" CssClass="form-control"></asp:DropDownList>
                    <asp:HiddenField ID="hfSeries" runat="server" />
                    <asp:CheckBoxList ID="cbSeries" runat="server" Height="70px">
                    </asp:CheckBoxList>
                    <asp:LinkButton ID="lbSeriesData" runat="server" Text="Select Series Data" OnClick="lbSeriesData_Click" CssClass="btn btn-primary"></asp:LinkButton>
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm=6 col-xs-12">
                <div class="form-group">
                    <label>Axis:</label>
                    <asp:DropDownList ID="ddlAxis" runat="server" CssClass="form-control"></asp:DropDownList>
                    <asp:HiddenField ID="hfAxis" runat="server" />
                    <asp:CheckBoxList ID="cbAxis" runat="server" Height="70px">
                    </asp:CheckBoxList>
                    <asp:LinkButton ID="lbAxisData" runat="server" Text="Select Axis Data" OnClick="lbAxisData_Click" CssClass="btn btn-primary"></asp:LinkButton>
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm=6 col-xs-12">
                <div class="form-group">
                    <label>Filter:</label>
                    <asp:DropDownList ID="ddlFilter" runat="server" CssClass="form-control"></asp:DropDownList>
                    <asp:HiddenField ID="hfFilter" runat="server" />
                    <asp:CheckBoxList ID="cbFilter" runat="server" Height="70px">
                    </asp:CheckBoxList>
                    <asp:LinkButton ID="lbFilterData" runat="server" Text="Select Filter Data" OnClick="lbFilterData_Click" CssClass="btn btn-primary"></asp:LinkButton>
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
                    <label>Chart Value:</label>
                    <asp:DropDownList ID="ddlValue" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Value Calculation Method:</label>
                    <asp:DropDownList ID="ddlCalc" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
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
                        <asp:ListItem>2d</asp:ListItem>
                        <asp:ListItem>3d</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm=12 col-xs-12">
                <div class="form-group">
                    <asp:LinkButton ID="cmd_SetChartConfig" runat="server" CssClass="btn btn-primary" OnClick="cmd_SetChartConfig_Click">Update Chart</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

</asp:Panel>
<asp:Panel ID="pnlChartAndData" CssClass="panel panel-default" runat="server">
    <div class="panel-heading">
        <h3>Chart&nbsp; and Data</h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <asp:Chart ID="myChart" runat="server" Height="1800px" Width="1800px" BackColor="23, 138, 204" BackGradientStyle="VerticalCenter" BackSecondaryColor="23, 138, 204" BorderlineColor="14, 89, 168" BorderlineDashStyle="Solid" BorderlineWidth="5" CssClass="img-responsive" Palette="Grayscale">
            </asp:Chart>
        </div>
        <div class="row">
            <div class="table-responsive">
                <table class="table table-striped">
                    <thead>
                        <tr>

                            <th>Series Name</th>
                            <asp:Repeater ID="rptChartDataHeader" runat="server">
                                <ItemTemplate>
                                    <th><%# Eval("PointLabel") %></th>
                                </ItemTemplate>
                            </asp:Repeater>

                        </tr>
                    </thead>

                    <asp:Repeater ID="rptChartData" runat="server">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("SeriesName") %></td>
                                <asp:Repeater ID="SubRepeater" runat="server" DataSource='<%# Container.DataItem%>'>
                                    <ItemTemplate>
                                        <td><%# GetIconSpan(Eval("PointValue")) %>   </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="row">
            <uc1:DisplayTable runat="server" ID="dtList" />
        </div>
    </div>
    <div class="panel-footer">
        <br />
        <br />
    </div>
</asp:Panel>
<asp:LinkButton ID="cmd_ResetCache" runat="server" OnClick="cmd_ResetCache_Click" Text="Reset Cache" CssClass="btn btn-primary"></asp:LinkButton>
