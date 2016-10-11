<%@ Control Language="C#" Inherits="System.Web.Mvc.MvcFieldTemplateUserControlBase" %>

<%= Html.TextBox(MetaMember.Name, DataValueEditString, 1, 10,
            new { @class = HasPreviousErrors ? "errorField" : "" })%>
