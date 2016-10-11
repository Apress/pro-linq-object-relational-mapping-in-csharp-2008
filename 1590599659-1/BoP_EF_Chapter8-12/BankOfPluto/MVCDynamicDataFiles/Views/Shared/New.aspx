<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Title="Add new item" Inherits="System.Web.Mvc.ViewPage"  %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <%= this.RenderUserControl("StatusMessage.ascx") %>

    <% var dd = DynamicViewData.GetDynamicViewData(this); %>

    <h1>Insert new item in the <%= dd.MetaTable.Name%> table</h1>
    
    <%= this.RenderUserControl("ErrorSummary.ascx")%>

    <form method="post" action="<%= Url.Action("create") %>">
        <table class="detailsview">
        <%foreach (var member in dd.DisplayMetaMembers) {%>
        <tr>
            <td>
            <b><%=member.Name %></b>
            </td>
            <td>
            <% dd.RenderInsert(dd.MetaTable.EntityType, member.Name); %>
            </td>
        </tr>
        <%}%>
        </table>
        
        <br />
        
        <%=Html.SubmitButton() %>
        <input type="submit" value="Cancel" name="cancel" />
    </form>
</asp:Content>
