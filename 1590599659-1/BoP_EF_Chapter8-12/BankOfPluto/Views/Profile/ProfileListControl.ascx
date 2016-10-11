<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileListControl.ascx.cs" Inherits="BankOfPluto.Views.Profile.ProfileListControl" %>
<%@ Import Namespace="BoP.Core.Domain" %>

<asp:DataList ID="profileDataList" runat="server">
<ItemTemplate>
         <table>
         <tr>
            <td><b>First Name:</b></td>
            <td><%# ((Person)(Container.DataItem)).FirstName %></td>
         </tr>
         <tr>
            <td><b>Last Name:</b></td>
            <td><%# ((Person)(Container.DataItem)).LastName %></td>
         </tr>
                  <tr>
            <td><b>Email:</b></td>
            <td><%# ((Person)(Container.DataItem)).Email %></td>
         </tr>
                  <tr>
            <td><b>Gender:</b></td>
            <td><%# ((Person)(Container.DataItem)).Gender %></td>
         </tr>
                  <tr>
            <td><b>Date of Birth:</b></td>
            <td><%# ((Person)(Container.DataItem)).DOB %></td>
         </tr>         
         </table>   
         
</ItemTemplate>
</asp:DataList>