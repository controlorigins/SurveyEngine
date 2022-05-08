<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CloneDev.aspx.vb" Inherits="CloneDev" %>

<%@ Register Src="~/AppClone.ascx" TagPrefix="uc1" TagName="AppClone" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="stylesheet" type="text/css" href="/Content/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/Content/font-awesome.min.css" />

    <script type="text/javascript" src="/Scripts/jquery-2.1.3.min.js"></script>
    <script type="text/javascript" src="/Scripts/bootstrap.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:AppClone runat="server" ID="AppClone" />
    </div>
    </form>
</body>
</html>
