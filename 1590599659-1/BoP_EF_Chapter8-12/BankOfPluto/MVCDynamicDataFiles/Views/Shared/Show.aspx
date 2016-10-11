<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Title="Item details" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <%= this.RenderUserControl("StatusMessage.ascx") %>

    <% var dd = DynamicViewData.GetDynamicViewData(this); %>

    <h1>Entry from the <%= dd.MetaTable.Name%> table</h1>
    
    <% object entity = dd.DataItem; %>
    <table class="detailsview">
    <%foreach (var member in dd.DisplayMetaMembers) {%>
    <tr>
        <td>
        <b><%=member.Name %></b>
        </td>
        <td>
        <% dd.Render(entity, member.Name); %>
        </td>
    </tr>
    <%}%>
    </table>
    
    <% if (DynamicSecurityUtility.AllowWrite(entity)) { %>
        <a href='<%= dd.MakeUrl("edit", entity) %>'>Edit</a>
        <a href='<%= dd.MakeUrl("destroy", entity) %>'>Delete</a><br />
    <% } %>
    <a href='<%= Url.Action("list") %>'>Show all items</a>
</asp:Content>
