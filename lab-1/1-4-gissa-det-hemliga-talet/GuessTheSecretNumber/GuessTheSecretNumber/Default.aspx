<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GuessTheSecretNumber.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gissa det hemliga talet</title>
    <link href="~/Content/Styles/Site.css" rel="stylesheet" />
</head>
<body>
    <div id="Container">
        <header>
            <h1>Gissa det hemliga talet</h1>
        </header>
        <div id="Content">
            <form id="form1" runat="server" defaultbutton="MakeGuessButton">
                <div>
                    <asp:Label ID="MakeGuessLabel" runat="server" Text="Gissa på ett tal mellan 1 och 100:" AssociatedControlID="MakeGuessTextBox"></asp:Label>
                    <asp:TextBox ID="MakeGuessTextBox" runat="server" Enabled="false"></asp:TextBox>
                    <asp:Button ID="MakeGuessButton" runat="server" Text="Gissa" CssClass="Button" Enabled="false" OnClick="MakeGuessButton_Click" />
                    <%-- Validation of MakeGuessTextBox --%>
                    <div id="ErrorList">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMakeGuessTextBox" runat="server" ErrorMessage="Du måste ange ett tal" Display="Dynamic" ControlToValidate="MakeGuessTextBox" SetFocusOnError="True" Text="Du måste ange ett tal" CssClass="Error"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidatorMakeGuessTextBox" runat="server" ErrorMessage="Du måste ange ett tal mellan 1 och 100" MaximumValue="100" MinimumValue="1" Type="Integer" Text="Du måste ange ett tal mellan 1 och 100" Display="Dynamic" ControlToValidate="MakeGuessTextBox" SetFocusOnError="True" CssClass="Error"></asp:RangeValidator>
                    </div>
                    <%--<asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="SingleParagraph" ShowMessageBox="True" ShowSummary="False" />--%>
                </div>

                <%-- Result area --%>
                <asp:PlaceHolder ID="ResultPlaceholder" runat="server" Visible="false">
                    <div>
                        <asp:Label ID="PreviousGuessesLabel" runat="server" Text="" CssClass="ResultText"></asp:Label>
                        <asp:Label ID="ResultLabel" runat="server" Text="" CssClass="ResultText"></asp:Label>
                        <asp:Label ID="GameOverLabel" runat="server" Text="" CssClass="ResultText" Visible="false"></asp:Label>
                    </div>
                </asp:PlaceHolder>
                <%-- New Game Button --%>
                <div>
                    <asp:Button ID="NewGameButton" runat="server" Text="Nytt spel" CssClass="Button" CausesValidation="False" OnClick="NewGameButton_Click" />
                </div>
            </form>
        </div>
    </div>

    <script src="Content/Scripts/OwnScript.js"></script>
</body>
</html>
