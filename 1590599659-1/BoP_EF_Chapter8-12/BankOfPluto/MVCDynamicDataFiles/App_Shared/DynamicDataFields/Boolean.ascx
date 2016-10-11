<%@ Control Language="C#" Inherits="System.Web.Mvc.MvcFieldTemplateUserControlBase" %>

<script runat="server">
    private string CheckedAttribute {
        get {
            if (DataValue == null || !((bool)DataValue))
                return String.Empty;

            return "Checked=\"Checked\"";
        }
    }
</script>

<input type="checkbox" disabled="disabled" <%= CheckedAttribute %> />
