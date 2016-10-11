<%@ Control Language="C#" Inherits="System.Web.Mvc.MvcFieldTemplateUserControlBase" %>

<script runat="server">
    private string SelectedAttribute(object entity) {
        if (entity == DataValue)
            return @"selected = ""true""";
        return String.Empty;
    }
</script>

<% var parentTable = MetaForeignKeyMember.ParentMetaTable; %>

<%--<%= Html.Select(MetaMember.Name, parentTable.Query,
        parentTable.IdentityMetaMembers[0].Name,
        parentTable.DisplayMetaMember.Name,
        DataBinder.GetPropertyValue(DataValue, parentTable.IdentityMetaMembers[0].Name))%>
--%>
<select name="<%= MetaMember.Name %>">
<% foreach (var entry in parentTable.GetQuery(DynamicViewData.DataContext)) {%>
    <option value="<%= DataBinder.GetPropertyValue(entry, parentTable.IdentityMetaMembers[0].Name) %>" <%= SelectedAttribute(entry) %>>
        <%= DataBinder.GetPropertyValue(entry, parentTable.DisplayMetaMember.Name) %>
    </option>
<% }%>
</select>
