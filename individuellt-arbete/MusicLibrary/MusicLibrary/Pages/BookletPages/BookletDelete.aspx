<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="BookletDelete.aspx.cs" Inherits="MusicLibrary.Pages.BookletPages.BookletDelete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <h2 class="pageheader">Radera nothäfte</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="row">
        <div class="small-12 large-10 columns booklet-delete-card">

            <%-- Validation Summary for delete booklet --%>
            <asp:ValidationSummary ID="BookletEditValidationSummary" data-alert="data-alert" CssClass="alert-box error" runat="server" ShowModelStateErrors="true" DisplayMode="List" />

            <asp:PlaceHolder ID="ConfirmPlaceHolder" runat="server" Visible="true">
                <p><asp:Literal ID="ConfirmLiteral" runat="server" Text="Är du säker på att du vill ta bort nothäftet {0}?" /></p>
                <p>Nothäftet kommer att raderas permanent från databasen.</p>

                <div class="booklet-delete-buttons">
                    <asp:LinkButton ID="YesLinkButton" CssClass="button tiny" runat="server" Text="Ja" OnCommand="YesLinkButton_Command" CommandArgument='<%$ RouteValue:id %>' CausesValidation="false" />
                    <asp:HyperLink ID="NoHyperLink" CssClass="button tiny" runat="server" NavigateUrl='<%$ RouteUrl:routename=Booklets %>' Text="Avbryt" />
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="BackPlaceHolder" runat="server" Visible="false">
                <asp:HyperLink ID="BackHyperlink" CssClass="button" NavigateUrl='<%$ RouteUrl:routename=Booklets %>' ImageUrl="~/Content/Icons/Back-icon-smaller.png" ToolTip="Tillbaka till listan" runat="server" Text="Tillbaka till listan" />
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
</asp:Content>