<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebGallery.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="sv">
<head runat="server">
    <title>Webbgalleri</title>
    <link href="Content/Styles/Style.css" rel="stylesheet" />
</head>
<body>
    <div id="Container">
        <form id="form1" runat="server">
            <header>
                <h1>Webbgalleri</h1>
            </header>
            <div id="Main">
                <%-- PlaceHolder for the big image. --%>
                <asp:PlaceHolder ID="BigImagePlaceHolder" runat="server" Visible="true">
                    <div id="BigImageDiv">
                        <asp:Image ID="BigImage" runat="server" />
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="NoImagesPlaceHolder" runat="server" Visible="false">
                    <div>
                        <p>Just nu finns det inga bilder uppladdade.</p>
                    </div>
                </asp:PlaceHolder>
                <%-- Place for the thumbnails. Repeater binds images to the page. --%>
                <div id="ThumbnailsDiv">
                    <asp:Repeater ID="ThumbnailRepeater" runat="server" ItemType="WebGallery.Model.ImageNames" SelectMethod="ThumbnailRepeater_GetData">
                        <HeaderTemplate>
                            <ul id="ThumbnailsList">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="<%# Item.imageName %>">
                                    <asp:Image ID="Image1" runat="server" CssClass="<%# Item.cssClass %>" ImageUrl="<%# Item.thumbnailImageName %>" />
                                </asp:HyperLink>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <footer>

                <%-- The fileUpload controls and tags. --%>
                <div id="FileUploadDiv">
                    <h2>Ladda upp ny bild</h2>
                    <asp:PlaceHolder ID="RightMessagePlaceHolder" runat="server" Visible="false">
                        <div id="RightMessageDiv">
                            <asp:Label ID="RightMessageLabel" CssClass="RightMessage" runat="server" Text="{0} är uppladdad."></asp:Label>
                            <asp:HyperLink ID="RightMessageCloseAnchor" runat="server" NavigateUrl="~/"><img id="RightMessageCloseIcon" src="Content/Images/Icons/dialog_close.png" /></asp:HyperLink>
                        </div>
                    </asp:PlaceHolder>
                    <%-- Validation for FildUpload. --%>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="ErrorMessageBox" DisplayMode="SingleParagraph" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Du måste välja en fil." Display="Dynamic" ControlToValidate="FileUpload1" CssClass="ErrorMessage" Text="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Du kan bara ladda upp filer av typen GIF, JPG eller PNG." ControlToValidate="FileUpload1" CssClass="ErrorMessage" Display="Dynamic" ValidationExpression="^.*\.(gif|jpg|png|GIF|JPG|PNG)$" Text="*"></asp:RegularExpressionValidator>
                    <%-- The FileUpload contril and send button. --%>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:Button ID="UploadFileButton" runat="server" Text="Ladda upp bild" OnClick="UploadFileButton_Click" />
                </div>
            </footer>
        </form>
    </div>

    <script src="Scripts/Script.js"></script>
</body>
</html>
