<%@ Control Language="C#" Inherits="System.Web.Mvc.MvcFieldTemplateUserControlBase" %>

<%= Html.TextBox(MetaMember.Name, DataValueEditString,
        new { @class = HasPreviousErrors ? "errorField" : "" })%>
