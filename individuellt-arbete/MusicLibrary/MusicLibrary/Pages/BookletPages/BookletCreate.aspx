<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="BookletCreate.aspx.cs" Inherits="MusicLibrary.Pages.BookletPages.BookletCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <h2 class="pageheader">Skapa nytt nothäfte</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="row">
        <div class="small-12 large-10 columns booklet-create-card">

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

            <asp:FormView ID="BookletCreateFormView" runat="server"
                ItemType="MusicLibrary.Model.BLL.Booklet"
                DataKeyNames="BookletID"
                DefaultMode="Insert"
                InsertMethod="BookletCreateFromView_InsertItem"
                RenderOuterTable="false"
                OnPreRender="BookletCreateFromView_PreRender">

                <InsertItemTemplate>
                    <%-- In this section the user can insert data. --%>
                    <div class="small-12 columns booklet-details-card-info">
                        <div class="row">
                            <label class="small-12 large-10 columns custom-label">
                                Titel:
                                <asp:TextBox ID="nameTextBox" runat="server" CssClass="error" Text="<%# BindItem.Name %>" MaxLength="100" />
                                <%-- Validation controls --%>
                                <asp:RequiredFieldValidator ID="nameRequiredFieldValidator" runat="server" CssClass="error" ErrorMessage="Fältet för titel får inte vara tomt" Text="Tomt fält" ControlToValidate="nameTextBox" SetFocusOnError="True" Display="Dynamic" />
                            </label>
                        </div>
                        <div class="row">
                            <label class="small-12 large-6 columns custom-label">
                                Förlag:
                                <asp:DropDownList ID="publisherDropDownList" runat="server" OnPreRender="PublisherDropDownList_PreRender" />
                                <%-- No validation required. --%>
                            </label>
                        </div>
                        <div class="row">
                            <label class="small-12 large-2 columns custom-label">
                                Utgivningsår:
                                <asp:TextBox ID="yearOfPublicationTextBox" CssClass="error" runat="server" Text="<%# BindItem.YearOfPublication.Year %>" MaxLength="4" />
                                <%-- Validation controls --%>
                                <asp:RequiredFieldValidator ID="yearRequiredFieldValidator" runat="server" CssClass="error" Display="Dynamic" ControlToValidate="yearOfPublicationTextBox" SetFocusOnError="True" Text="Tomt fält" ErrorMessage="Fältet för utgivningsår får inte vara tomt" />
                                <asp:RegularExpressionValidator ID="yearRegularExpressionValidator" runat="server" CssClass="error" ErrorMessage="Inmatningen i fältet för utgivningår måste hålla formatet [ÅÅÅÅ]" Display="Dynamic" ControlToValidate="yearOfPublicationTextBox" ValidationExpression="^\d{4}$" Text="Ogiltigt format" />
                                <asp:CustomValidator ID="CheckYearValidator" CssClass="error" runat="server" ErrorMessage="Utgivningsår får inte vara i framtiden." ControlToValidate="yearOfPublicationTextBox" ClientValidationFunction="CheckYear" OnServerValidate="CheckYear" Display="Dynamic" Text="Felaktigt årtal" />
                            </label>
                        </div>
                        <div class="row">
                            <label class="small-12 large-2 columns custom-label">
                                Hylla:
                                <asp:TextBox ID="placeTextBox" runat="server" CssClass="error" Text="<%# BindItem.Place %>" MaxLength="6" />
                                <%-- Validation controls --%>
                                <asp:RequiredFieldValidator ID="placeRequiredFieldValidator" runat="server" CssClass="error" ErrorMessage="Fältet för hylla får inte vara tomt" ControlToValidate="placeTextBox" Text="Tomt fält" Display="Dynamic" SetFocusOnError="True" />
                                <asp:RegularExpressionValidator ID="placeRegularExpressionValidator" runat="server" CssClass="error" ErrorMessage="Inmatningen i fältet för hylla måste hålla formatet [AA0000]" Display="Dynamic" Text="Ogiltigt format" ControlToValidate="placeTextBox" ValidationExpression="^[A-Z]{2}\d{4}" />
                            </label>
                        </div>
                    </div>

                    <%-- I this section user edit the data in table appShema.BookletContent. --%>
                    <div class="small-12 columns booklet-details-card-pieces">
                        <p><span class="custom-label">Innehåll:</span></p>

                        <asp:ListView ID="BookletPiecesListView"
                            runat="server"
                            DataKeyNames="PieceID, BookletID"
                            ItemType="MusicLibrary.Model.BLL.BookletContent" SelectMethod="BookletPiecesListView_GetData"
                            DeleteMethod="BookletPiecesListView_DeleteItem"
                            OnItemDataBound="BookletPiecesListView_ItemDataBound"
                            InsertMethod="BookletPiecesListView_InsertItem"
                            InsertItemPosition="LastItem">

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
                                            <th></th>
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
                                    <td class="buttonColumn">
                                        <asp:LinkButton ID="DeleteLinkButton" CssClass="button" CommandName="Delete" runat="server" ToolTip="Radera" CausesValidation="false">
                                            <asp:Image ID="DeleteImage" runat="server" ImageUrl="~/Content/Icons/Delete-icon-smaller.png" AlternateText="Radera" />
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>

                            <InsertItemTemplate>
                                <tr>
                                    <td colspan="6">
                                        <label class="custom-label">
                                            Lägg till ny:
                                            <asp:DropDownList ID="PieceDropDownList"
                                                ItemType="MusicLibrary.Model.BLL.Piece"
                                                DataValueField="PieceID"
                                                SelectMethod="PieceDropDownList_GetData"
                                                DataTextField="PieceID"
                                                SelectedValue="<%# BindItem.PieceID %>"
                                                runat="server"
                                                OnPreRender="PieceDropDownList_PreRender" />
                                        </label>
                                    </td>
                                    <td class="buttonColumn">
                                        <asp:LinkButton ID="InsertLinkButton" CssClass="button" CommandName="Insert" runat="server" ToolTip="Spara" CausesValidation="false">
                                            <asp:Image ID="SaveImage" runat="server" ImageUrl="~/Content/Icons/Save-icon-smaller.png" AlternateText="Spara" />
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </InsertItemTemplate>

                            <%-- If no BookletContent existed from the Booklet. --%>
                            <EmptyDataTemplate>
                                <p>Nothäftet har inget innehåll.</p>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>

                    <%-- In this section user edit the data in tables appSchema.Note and appSchema.BorrowedBy --%>
                    <%-- For the moment no data is here is editable. for the moment update and insert methods ignore this section. --%>
                    <div class="booklet-details-card-other small-12 columns">
                        <div class="small-12 medium-9 large-9 columns">
                            <div class="booklet-details-card-other-note small-12 columns">
                                <div class="row">
                                    <label class="small-12 large-10 columns custom-label">
                                        Anteckningar:
                                        <asp:TextBox ID="notesTextBox" runat="server" MaxLength="400" TextMode="MultiLine" Text="" />
                                        <%-- No validation required. --%>
                                    </label>
                                </div>
                            </div>
                            <div class="booklet-details-card-other-borrowedTo small-12 columns">
                                <div class="row">
                                    <label class="small-12 large-5 columns custom-label">
                                        Utlånad till:
                                        <asp:TextBox ID="borrowedToTextBox" MaxLength="40" runat="server" Text="" />
                                        <%-- No validation required. --%>
                                    </label>
                                </div>
                            </div>
                        </div>

                        <%-- This section contains the command buttons for the form. --%>
                        <div class="booklet-details-card-buttons small-12 medium-3 large-3 columns">
                            <ul class="button-list">
                                <li>
                                    <asp:LinkButton ID="CancelLinkButton" CssClass="button" ToolTip="Tillbaka" runat="server" OnClick="CancelLinkButton_Click" CausesValidation="false">
                                        <asp:Image ID="CancelImage" ImageUrl="~/Content/Icons/Back-icon-smaller.png" AlternateText="Tillbaka" runat="server" />
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="SaveLinkButton" CssClass="button" CommandName="Insert" ToolTip="Spara ändringar" runat="server" CausesValidation="true">
                                        <asp:Image ID="SaveImage" ImageUrl="~/Content/Icons/Save-icon-smaller.png" AlternateText="Spara ändringar" runat="server" />
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </InsertItemTemplate>
            </asp:FormView>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
</asp:Content>