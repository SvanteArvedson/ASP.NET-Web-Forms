<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Adventurous_Contacts.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="sv">
<head runat="server">
    <title>Äventyrliga kontakter</title>
    <link href="Content/Styles/Site.css" rel="stylesheet" />
</head>
<body>
    <div id="Container">
        <form id="form1" runat="server">
            <header>
                <h1>Äventyrliga kontakter</h1>
            </header>
            <div id="Main">
                <%-- PlaceHolder for RightMessages --%>
                <asp:PlaceHolder ID="RightBoxPlaceHolder" runat="server" Visible="false">
                    <asp:Panel ID="RightBox" CssClass="RightBox" runat="server">
                        <asp:Label ID="RightMessage" CssClass="RightMessage" runat="server" />
                        <asp:HyperLink href="Default.aspx" ID="CloseIconHyperLink" runat="server">
                            <asp:Image ID="CloseIcon" ImageUrl="~/Content/Styles/Icons/Close.png" runat="server" />
                        </asp:HyperLink>
                        <div id="ClearBoth"></div>
                    </asp:Panel>
                </asp:PlaceHolder>
                <%-- ValidationSummarys for different ValidationGroups. --%>
                <asp:ValidationSummary ID="ValidationSummary1" CssClass="ErrorBox" ValidationGroup="InsertItem" runat="server" />
                <asp:ValidationSummary ID="ValidationSummary2" CssClass="ErrorBox" ValidationGroup="EditItem" ShowModelStateErrors="false" runat="server" />
                <%-- ListView displays the data in a table. --%>
                <asp:ListView ID="ContactListView" runat="server"
                    ItemType="Adventurous_Contacts.Model.BLL.Contact"
                    SelectMethod="ContactListView_GetData"
                    DataKeyNames="ContactId"
                    OnPagePropertiesChanging="ContactListView_PagePropertiesChanging"
                    InsertMethod="ContactListView_InsertItem"
                    UpdateMethod="ContactListView_UpdateItem"
                    DeleteMethod="ContactListView_DeleteItem"
                    InsertItemPosition="FirstItem">
                    <%-- The overall look of the table. --%>
                    <LayoutTemplate>
                        <table>
                            <thead>
                                <tr>
                                    <th>Efternamn</th>
                                    <th>Förnamn</th>
                                    <th>Epost</th>
                                    <th>Redigera post</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                            </tbody>
                        </table>
                        <asp:DataPager ID="ContactDataPager" runat="server" PageSize="20">
                            <Fields>
                                <asp:NextPreviousPagerField ShowFirstPageButton="true" ShowPreviousPageButton="false"
                                    ShowNextPageButton="false" ShowLastPageButton="false" FirstPageText="<<"
                                    PreviousPageText="<" ButtonType="Button" ButtonCssClass="Button" />
                                <asp:NumericPagerField ButtonType="Button" PreviousPageText="<" NextPageText=">"
                                    NextPreviousButtonCssClass="Button" NumericButtonCssClass="Button" />
                                <asp:NextPreviousPagerField ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                    ShowNextPageButton="false" ShowLastPageButton="true" LastPageText=">>" ButtonType="Button"
                                    ButtonCssClass="Button" />
                            </Fields>
                        </asp:DataPager>
                    </LayoutTemplate>
                    <%-- The dispay of the data. --%>
                    <ItemTemplate>
                        <tr>
                            <td><%# Item.LastName %></td>
                            <td><%# Item.FirstName %></td>
                            <td><%# Item.EmailAddress %></td>
                            <td>
                                <asp:Button ID="EditLinkButton" runat="server" CommandName="Edit" Text="Ändra" CausesValidation="false" CssClass="Button" />
                                <asp:Button ID="DeleteLinkButton" runat="server" CommandName="Delete" Text="Radera" CausesValidation="false" CssClass="Button DeleteButton" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <%-- To insert new data. --%>
                    <InsertItemTemplate>
                        <tr>
                            <td>
                                <asp:TextBox ID="LastName" runat="server" MaxLength="50" Text="<%# BindItem.LastName %>" />
                                <asp:RequiredFieldValidator ID="LastNameRequiredFieldValidator" ValidationGroup="InsertItem" CssClass="Error" runat="server" ErrorMessage="Fältet för efternamn får inte vara tomt." Text="*" Display="Dynamic" ControlToValidate="LastName" />
                            </td>
                            <td>
                                <asp:TextBox ID="FirstName" runat="server" MaxLength="50" Text="<%# BindItem.FirstName %>" />
                                <asp:RequiredFieldValidator ID="FirstNameRequiredFieldValidator" ValidationGroup="InsertItem" CssClass="Error" runat="server" ErrorMessage="Fältet för förnamn får inte vara tomt." Text="*" Display="Dynamic" ControlToValidate="FirstName" />
                            </td>
                            <td>
                                <asp:TextBox ID="EmailAddress" runat="server" MaxLength="50" Text="<%# BindItem.EmailAddress %>" />
                                <asp:RequiredFieldValidator ID="EmailAddressRequiredFieldValidator" ValidationGroup="InsertItem" CssClass="Error" runat="server" ErrorMessage="Fältet för epostadress får inte vara tomt." Text="*" Display="Dynamic" ControlToValidate="EmailAddress" />
                                <asp:RegularExpressionValidator ID="EmailAddressRegularExpressionValidator" ValidationGroup="InsertItem" CssClass="Error" runat="server" ErrorMessage="Ogiltig epostadress." Text="*" Display="Dynamic" ControlToValidate="EmailAddress" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                            </td>
                            <td>
                                <asp:Button ID="InsertContactLinkButton" runat="server" ValidationGroup="InsertItem" Text="Spara" CommandName="Insert" CssClass="Button" />
                                <asp:Button ID="CancelInsertContactLinkButton" runat="server" Text="Avbryt" CommandName="Cancel" CausesValidation="false" CssClass="Button" />
                            </td>
                        </tr>
                    </InsertItemTemplate>
                    <%-- To change data. --%>
                    <EditItemTemplate>
                        <tr>
                            <td>
                                <asp:TextBox ID="LastName" runat="server" MaxLength="50" Text="<%# BindItem.LastName %>" />
                                <asp:RequiredFieldValidator ID="LastNameRequiredFieldValidator" ValidationGroup="EditItem" CssClass="Error" runat="server" ErrorMessage="Fältet för efternamn får inte vara tomt." Text="*" Display="Dynamic" ControlToValidate="LastName" />
                            </td>
                            <td>
                                <asp:TextBox ID="FirstName" runat="server" MaxLength="50" Text="<%# BindItem.FirstName %>" />
                                <asp:RequiredFieldValidator ID="FirstNameRequiredFieldValidator" ValidationGroup="EditItem" CssClass="Error" runat="server" ErrorMessage="Fältet för förnamn får inte vara tomt." Text="*" Display="Dynamic" ControlToValidate="FirstName" />
                            </td>
                            <td>
                                <asp:TextBox ID="EmailAddress" runat="server" MaxLength="50" Text="<%# BindItem.EmailAddress %>" />
                                <asp:RequiredFieldValidator ID="EmailAddressRequiredFieldValidator" ValidationGroup="EditItem" CssClass="Error" runat="server" ErrorMessage="Fältet för epostadress får inte vara tomt." Text="*" Display="Dynamic" ControlToValidate="EmailAddress" />
                                <asp:RegularExpressionValidator ID="EmailAddressRegularExpressionValidator" ValidationGroup="EditItem" CssClass="Error" runat="server" ErrorMessage="Ogiltig epostadress." Text="*" Display="Dynamic" ControlToValidate="EmailAddress" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                            </td>
                            <td>
                                <asp:Button ID="UpdateContactLinkButton" ValidationGroup="EditItem" runat="server" Text="Spara" CommandName="Update" CssClass="Button" />
                                <asp:Button ID="CancelUpdateContactLinkButton2" runat="server" Text="Avbryt" CommandName="Cancel" CausesValidation="false" CssClass="Button" />
                            </td>
                        </tr>
                    </EditItemTemplate>
                    <%-- I no data is supplied. --%>
                    <EmptyDataTemplate>
                        <p>Kontaktuppgifter saknas.</p>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <footer>
            </footer>
        </form>
    </div>

    <%-- My own script taking care of event listener and confirm dialog. --%>
    <script src="Scripts/OwnScript.js"></script>
</body>
</html>
