<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="BankOfPluto.Views.Profile.List" %>


<%@ Register src="ProfileListControl.ascx" tagname="ProfileListControl" tagprefix="uc1" %>


<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    
    <form id="form1" runat="server">
 
    <uc1:ProfileListControl ID="ProfileListControl1" runat="server" />
    </form>
 
</asp:Content>
