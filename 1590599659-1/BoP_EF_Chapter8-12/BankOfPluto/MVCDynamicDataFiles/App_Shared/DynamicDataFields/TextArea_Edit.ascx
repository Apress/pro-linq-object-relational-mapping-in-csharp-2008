<%@ Control Language="C#" Inherits="System.Web.Mvc.MvcFieldTemplateUserControlBase" %>

<%= Html.TextArea(MetaMember.Name, DataValueEditString, 5, 80, 0,
    new { @class = HasPreviousErrors ? "errorField" : "" })%>
