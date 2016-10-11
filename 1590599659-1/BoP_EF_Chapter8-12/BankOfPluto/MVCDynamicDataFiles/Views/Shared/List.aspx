<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Title="All items" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <%= this.RenderUserControl("StatusMessage.ascx") %>

    <% var dd = DynamicViewData.GetDynamicViewData(this); %>
    
    <h1><%= dd.MetaTable.Name %></h1>
    
    <table class="gridview">
    <tr>
    <th></th>
    <%foreach (var member in dd.DisplayShortMetaMembers) {%>
        <th>
        <a href="?sort=<%=member.Name %>"><%=member.Name %></a>
        </th>
    <%}%>
    </tr>

    <%int count=0; %>
    <%foreach (var o in dd.Data) {%>
        <tr class='<%=(++count)%2==0?"even":"" %>'>

        <td>
            <a href='<%= dd.MakeUrl("show", o) %>'>Details</a>
            <% if (DynamicSecurityUtility.AllowWrite(o)) { %>
                <a href='<%= dd.MakeUrl("edit", o) %>'>Edit</a>
                <a href='<%= dd.MakeUrl("destroy", o) %>'>Delete</a>
            <% } %>
        </td>
        
        <%foreach (DynamicMetaMember member in dd.DisplayShortMetaMembers) {%>
            <% if (member.IsLongString) continue; %>
            <td>
            <% dd.Render(o, member.Name); %>
            </td>
        <%}%>
        </tr>
    <%}%>
    </table>
    
    <%if (count == 0) { %>
        <p>This table is currently empty</p>
    <%}%>

    <a href='<%= Url.Action("new") %>'>Add new item</a>
</asp:Content>