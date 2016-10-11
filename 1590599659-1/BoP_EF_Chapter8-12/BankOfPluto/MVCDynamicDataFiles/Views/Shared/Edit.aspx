<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Title="Edit item" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <%= this.RenderUserControl("StatusMessage.ascx") %>

    <% var dd = DynamicViewData.GetDynamicViewData(this); %>

    <h1>Edit item from the <%= dd.MetaTable.Name%> table</h1>
    
    <% object entity = dd.DataItem; %>

    <%= this.RenderUserControl("ErrorSummary.ascx")%>

    <form method="post" action="<%= dd.MakeUrl("update", entity) %>">
        <table class="detailsview">
        <%foreach (var member in dd.DisplayMetaMembers) {%>
        <tr>
            <td>
            <b><%=member.Name %></b>
            </td>
            <td>
            <% dd.RenderEdit(entity, member.Name); %>
            </td>
        </tr>
        <%}%>
        </table>
        
        <%=Html.SubmitButton() %>
        <input type="submit" value="Cancel" name="cancel" />
    </form>
</asp:Content>
