<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SurveyDashboard.ascx.vb" Inherits="Co_Apps_SurveyApp_SurveyDashboard" %>

<%@ Register Src="~/controls/Co_Image.ascx" TagPrefix="uc1" TagName="Co_Image" %>
<%@ Register Src="~/Co_Apps/SurveyApp/admin/SurveyResponseListDashboard.ascx" TagPrefix="uc1" TagName="SurveyResponseListDashboard" %>
<%@ Register Src="~/controls/Co_HTML.ascx" TagName="Co_HTML" TagPrefix="uc1" %>

<div class="row">
    <div class="col-lg-3 col-md-3" style="text-align: left;">
        <uc1:Co_Image runat="server" ID="Co_MainLogo" AppPropID="MainLogo" CssClass="img-responsive" CssStyle="max-width:200px;float:left;" />
    </div>
    <div class="col-lg-6  col-md-6" style="text-align: center;">
        <uc1:Co_HTML ID="Co_ApplicationTitle" AppPropID="ApplicationTitle" runat="server" />
    </div>
    <div class="col-lg-3 col-md-3" style="text-align: right;">
        <uc1:Co_Image runat="server" ID="Co_ClientLogo" AppPropID="ClientLogo" CssClass="img-responsive" CssStyle="max-width:200px;text-align:right;float:right;" />
    </div>
</div>
<uc1:SurveyResponseListDashboard runat="server" ID="SurveyResponseListDashboard" />


