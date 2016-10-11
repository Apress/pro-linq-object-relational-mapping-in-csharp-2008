<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>

<span class="statusMessage">
<%
    string message = Html.ViewContext.TempData.GetStatusMessage();
    if (message != null)
        Response.Write(Server.HtmlEncode(message));
%>
</span>
