<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="BookletDetails.aspx.cs" Inherits="MusicLibrary.Pages.BookletPages.BookletDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <h2 class="pageheader">Nothäfte</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="row">
        <div class="small-12 large-10 columns booklet-details-card">

            <%-- Success message --%>
            <asp:PlaceHolder ID="SuccessMessagePlaceHolder" runat="server" Visible="false">
                <div data-alert="data-alert" class="alert-box success">
                    <p>
                        <asp:Literal ID="SuccessMessageLiteral" runat="server" />
                    </p>
                    <asp:HyperLink ID="CloseHyperLink" CssClass="close" NavigateUrl="#" runat="server" Text="&times;" />
                </div>
            </asp:PlaceHolder>

            <%-- Validation Summary for booklet edit --%>
            <asp:ValidationSummary ID="BookletEditValidationSummary" data-alert="data-alert" CssClass="alert-box error" runat="server" ShowModelStateErrors="true" DisplayMode="List" />

            <asp:FormView ID="BookletDetailFormView" runat="server"
                ItemType="MusicLibrary.Model.BLL.Booklet"
                DataKeyNames="BookletID"
                DefaultMode="ReadOnly"
                SelectMethod="BookletDetailFormView_GetItem"
                OnDataBound="BookletDetailFormView_DataBound"
                RenderOuterTable="false">

                <ItemTemplate>
                    <%-- This section displays all info in table appSchema.Booklet with the key value in the Url. --%>
                    <div class="small-12 columns booklet-details-card-info">
                        <p>
                            <span class="custom-label">Titel: </span>
                            <asp:Literal ID="nameLiteral" runat="server" Text="<%#: Item.Name %>" />
                        </p>
                        <p>
                            <span class="custom-label">Förlag: </span>
                            <asp:Literal ID="publisherLiteral" runat="server" Text="Förlag" />
                        </p>
                        <p>
                            <span class="custom-label">Utgivningsår: </span>
                            <asp:Literal ID="yearOfPublicationLiteral" runat="server" Text="<%#: Item.YearOfPublication.Year %>" />
                        </p>
                        <p>
                            <span class="custom-label">Hylla: </span>
                            <asp:Literal ID="placeLiteral" runat="server" Text="<%#: Item.Place %>" />
                        </p>
                    </div>

                    <%-- This section displays all Pieces in the booklet, data is fetched from table appSchema.BookletContent and appSchema.Piece. --%>
                    <div class="small-12 columns booklet-details-card-pieces">
                        <p><span class="custom-label">Innehåll:</span></p>

                        <asp:ListView ID="BookletPiecesListView"
                            runat="server"
                            DataKeyNames="PieceID, BookletID"
                            ItemType="MusicLibrary.Model.BLL.BookletContent"
                            SelectMethod="BookletPiecesListView_GetData"
                            OnItemDataBound="BookletPiecesListView_ItemDataBound">

                            <LayoutTemplate>
                                <table>
                                    <thead>
                                        <tr>
                                            <th>Kompositör</th>
                                            <th>Titel</th>
                                            <th>Katalognummer</th>
                                            <th>Tonart</th>
                                            <th>Instrument</th>
                                            <th>Genre</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%-- PlaceHolder for ItemTemplate. --%>
                                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>

                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Literal ID="composerLiteral" runat="server" Text="" />
                                    </td>
                                    <td>
                                        <asp:Literal ID="nameLiteral" runat="server" Text="" />
                                    </td>
                                    <td>
                                        <asp:Literal ID="catalogueNumberLiteral" runat="server" Text="" />
                                    </td>
                                    <td>
                                        <asp:Literal ID="scaleLiteral" runat="server" Text="" />
                                    </td>
                                    <td>
                                        <asp:Literal ID="instrumentsLiteral" runat="server" Text="" />
                                    </td>
                                    <td>
                                        <asp:Literal ID="genreLiteral" runat="server" Text="" />
                                    </td>
                                </tr>
                            </ItemTemplate>

                            <%-- If no BookletContent existed from the Booklet. --%>
                            <EmptyDataTemplate>
                                <p class="no-data-text">Nothäftet har inget innehåll.</p>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>

                    <%-- This section displays info from tables appSchema.Note and appSchema.BorrowdTo where the key is the same as for the booklet. --%>
                    <%-- For the moment no data is fetched from the tables, the fields contains static data. --%>
                    <div class="booklet-details-card-other small-12 columns">
                        <div class="small-12 medium-9 large-9 columns">
                            <div class="booklet-details-card-other-note small-12 columns">
                                <p>
                                    <span class="custom-label">Anteckningar: </span>
                                    <asp:Literal ID="notesLiteral" runat="server" Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus fringilla placerat orci, sed lacinia purus tempus eu. Donec tristique ligula." />
                                </p>
                            </div>
                            <div class="booklet-details-card-other-borrowedTo small-12 columns">
                                <p>
                                    <span class="custom-label">Utlånad till: </span>
                                    <asp:Literal ID="borrowedToLiteral" runat="server" Text="Kompis" />
                                </p>
                            </div>
                        </div>

                        <%-- This section contains the command buttons for the form. --%>
                        <div class="booklet-details-card-buttons small-12 medium-3 large-3 columns">
                            <ul class="button-list">
                                <li>
                                    <asp:HyperLink ID="BackHyperlink" CssClass="button" NavigateUrl='<%$ RouteUrl:routename=Booklets %>' ImageUrl="~/Content/Icons/Back-icon-smaller.png" ToolTip="Tillbaka till listan" runat="server" Text="Tillbaka till listan" />
                                </li>
                                <li>
                                    <asp:ImageButton ID="PrintImageButton" ClientIDMode="Static" CssClass="button" ImageUrl="~/Content/Icons/Print-icon-smaller.png" runat="server" CausesValidation="false" />
                                </li>
                                <li>
                                    <asp:HyperLink ID="EditHyperLink" CssClass="button" NavigateUrl='<%# GetRouteUrl("BookletEdit", new { id = Item.BookletID }) %>' ImageUrl="~/Content/Icons/Edit-icon-smaller.png" ToolTip="Redigera nothäftet" runat="server" Text="Redigera nothäftet" />
                                </li>
                            </ul>
                        </div>
                    </div>
                </ItemTemplate>

                <%-- If no BookletContent where found in table appSchema.BookletContent. --%>
                <EmptyDataTemplate>
                    <asp:HyperLink ID="BackHyperlink" CssClass="button" NavigateUrl='<%$ RouteUrl:routename=Booklets %>' ImageUrl="~/Content/Icons/Back-icon-smaller.png" ToolTip="Tillbaka till listan" runat="server" Text="Tillbaka till listan" />
                </EmptyDataTemplate>
            </asp:FormView>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
    <%: Scripts.Render("~/Scripts/BookletDetailsScript") %>
</asp:Content>
