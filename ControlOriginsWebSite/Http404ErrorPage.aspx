﻿<%@ Page Language="VB" %>

<script runat="server">
  Dim ex As HttpException

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
    ' Log the exception 
    ex = New HttpException("defaultRedirect")
    ExceptionUtility.LogException(ex, "Caught in DefaultRedirectErrorPage")
  End Sub

</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
  <title>HTTP 404 Error Page</title>
</head>
<body>
  <form id="form1" runat="server">
  <div>
    <h2>
      HTTP 404 Error Page</h2>
    Standard error message suitable for file not found errors. 
    The original exception object is not available, 
    but the original requested URL is in the query string.<br />
    <br />
    Return to the <a href='logout.aspx'> Default Page</a>
  </div>
  </form>
</body>
</html>
