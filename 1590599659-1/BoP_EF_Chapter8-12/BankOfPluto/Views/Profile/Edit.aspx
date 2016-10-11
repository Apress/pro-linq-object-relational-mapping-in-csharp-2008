<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="BankOfPluto.Views.Profile.Edit" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

 <h2>Update your profile by editing the fields below and clicking the Save button.</h2>

    
    <form action="<%=Url.Action(new { Action="UpdateProfile"}) %>" method="post">
    
        <table>
            <%=Html.Hidden("StakeHolderId", ViewData.StakeHolderId)%>    
            <tr>
            
                <td>Tax Id (Not Editable - Please Contact Customer Service for changes):</td>
                <td><%= Html.TextBox("TaxId", ViewData.TaxId, new { ReadOnly = "true", style = "font-family: Webdings" })%></td>
            </tr>
            <tr>
                <td>First Name:</td>
                <td><%= Html.TextBox("FirstName", ViewData.FirstName) %></td>
            </tr>

            <tr>
                <td>Last Name:</td>
                <td><%= Html.TextBox("LastName", ViewData.LastName) %></td>
            </tr>

            <tr>
                <td>Email:</td>
                <td><%= Html.TextBox("Email", ViewData.Email) %></td>
            </tr>
            <tr>
                <td>Gender:</td>
                <td><%= Html.Select("Gender", new string[] { "M", "F" }, ViewData.Gender)%></td>
            </tr>
            <tr>
                <td>Date of Birth:</td>
                <td><%= Html.TextBox("DOB", ViewData.DOB.Value.ToShortDateString()) %></td>
            </tr>
            
        </table>

        <p></p>

        <input type="submit" value="Save" />

    </form>


</asp:Content>
