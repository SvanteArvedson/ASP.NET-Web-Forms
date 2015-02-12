<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Error404.aspx.cs" Inherits="MusicLibrary.Pages.Shared.Error404" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <h2 class="pageheader">Fel 404</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="row">
        <div class="small-12 large-10 columns error-card">
            <h3>Sidan finns inte</h3>
            <p>Sidan du efterfrågade existerar inte. Kontrollera webbadressen och försök igen.</p>
            <asp:HyperLink ID="BackHyperlink" CssClass="button" NavigateUrl='<%$ RouteUrl:routename=Default %>' ImageUrl="~/Content/Icons/Back-icon-smaller.png" ToolTip="Till startsidan" runat="server" Text="Till startsidan" />
        </div>
    </div>
</asp:Content>
