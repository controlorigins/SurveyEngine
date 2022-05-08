<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyResponseListDashboard.ascx.vb" Inherits="SurveyApp_controls_SurveyResponseDashboard" %>
<%@ Register Src="~/controls/DisplayTable.ascx" TagPrefix="uc1" TagName="DisplayTable" %>

<div class="row">
    <div class="col-lg-6 ">
        <div class="panel panel-default" id="SurveyChart">
            <div class="panel-heading">
                Scores Summary
            </div>
            <div class="panel-body">
                <asp:Chart ID="MySurveyChart" runat="server" CssClass="img-responsive">
                    <Series>
                        <asp:Series ChartType="Bar" Name="Series1" YValuesPerPoint="2"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>
        </div>
    </div>
    <div class="col-lg-6">
        <div class="panel panel-default" id="SurveyStatusChart">
            <div class="panel-heading">
                Status by Type
            </div>
            <div class="panel-body">
                <asp:Chart ID="MySurveyStatusChart" runat="server" CssClass="img-responsive">
                    <Series>
                        <asp:Series ChartType="Doughnut" Name="Series1"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-lg-12">
        <uc1:DisplayTable runat="server" ID="dtList" />
    </div>
</div>
