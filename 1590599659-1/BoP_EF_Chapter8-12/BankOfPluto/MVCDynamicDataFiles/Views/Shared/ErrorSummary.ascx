<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>

<ul class="errorSummary">
<%
    var formErrorInfo = Html.ViewContext.TempData.GetFormErrorInfo() as DynamicFormErrorInfo;
    if (formErrorInfo != null) {

        foreach (string error in formErrorInfo.ErrorMessages) {%>
        <li>
            <%= Server.HtmlEncode(error) %>
        </li>
        <%}

        foreach (var fieldError in formErrorInfo.FieldErrors.Values) {
            foreach (string error in fieldError.ErrorMessages) {%>
            <li>
                <%=fieldError.FieldName %>: 
                <%= Server.HtmlEncode(error) %>
            </li>
            <%}%>
        <%}%>
    <%}%>
</ul>
