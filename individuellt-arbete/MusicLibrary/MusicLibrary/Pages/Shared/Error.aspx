<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="MusicLibrary.Pages.Shared.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <h2 class="pageheader">Serverfel</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="row">
        <div class="small-12 large-10 columns error-card">
            <h3>Serverfel</h3>
            <p>Ett fel inträffade när servern behandlade din sidförfrågan.</p>
            <asp:HyperLink ID="BackHyperlink" CssClass="button" NavigateUrl='<%$ RouteUrl:routename=Default %>' ImageUrl="~/Content/Icons/Back-icon-smaller.png" ToolTip="Till startsidan" runat="server" Text="Till startsidan" />
        </div>
    </div>
</asp:Content>
