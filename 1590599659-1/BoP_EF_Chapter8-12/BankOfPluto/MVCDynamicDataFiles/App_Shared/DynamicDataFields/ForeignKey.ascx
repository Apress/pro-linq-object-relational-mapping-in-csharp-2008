<%@ Control Language="C#" Inherits="System.Web.Mvc.MvcFieldTemplateUserControlBase" %>

<script runat="server">
    string GetDisplayString() {
        var column = (System.Web.DynamicData.DynamicMetaForeignKeyMember)MetaMember;
        return FormatDataValue(column.OtherMetaTable.GetDisplayString(DataValue));
    }
</script>

<a href="<%= ForeignKeyPath %>"><%= GetDisplayString() %></a>

