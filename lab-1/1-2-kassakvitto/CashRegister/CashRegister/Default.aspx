<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CashRegister.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="sv">
<head runat="server">
    <title>Kassakvitto</title>
    <link href="~/Content/Site.css" rel="stylesheet" />
</head>
<body>
    <div id="Container">

        <header>
            <h1>Kassakvitto</h1>
        </header>

        <div id="Content">
            <form id="form1" runat="server" defaultbutton="SendButton">

                <div id="Inputarea">
                    <%-- Input price --%>
                    <asp:Label ID="LabelInputSubtotal" runat="server" Text="Total köpesumma:" AssociatedControlID="InputSubtotal"></asp:Label>
                    <asp:TextBox ID="InputSubtotal" runat="server"></asp:TextBox><asp:Label ID="Unit" runat="server" Text=" kr"></asp:Label>
                    <asp:Button ID="SendButton" runat="server" Text="Beräkna rabatt" OnClick="SendButton_Click" />
                    <%-- Validation TextBox not empty --%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="InputSubtotal" CssClass="field-validation-error" Text="Du måste mata in ett pris"></asp:RequiredFieldValidator>
                    <%-- Validation TextBox double greater than 0 --%>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ControlToValidate="InputSubtotal" Operator="GreaterThan" ValueToCompare="0" Type="Double" CssClass="field-validation-error" Text="Priset måste kunna tolkas som ett flyttal större än noll"></asp:CompareValidator>
                </div>

                <%-- Receipt area --%>
                <asp:Panel ID="ReceiptDiv" runat="server" Visible="false">
                    <header>
                        <h2>S-Market</h2>
                        <p>Tel. 040-68 66 278</p>
                        <p>Öppettider må-lö 07:00-21:00</p>
                    </header>
                    <%-- Here is the calculations presented. --%>
                    <div id="ReceiptContent">
                        <dl>
                            <dt>Totalt:</dt>
                            <dd>
                                <asp:Literal ID="LiteralSubtotal" runat="server">{0:c}</asp:Literal>
                            </dd>
                            <dt>Rabattsats:</dt>
                            <dd>
                                <asp:Literal ID="LiteralDiscountRate" runat="server">{0:P0}</asp:Literal>
                            </dd>
                            <dt>Rabatt:</dt>
                            <dd>
                                <asp:Literal ID="LiteralMoneyOff" runat="server">{0:c}</asp:Literal>
                            </dd>
                            <dt>Att betala:</dt>
                            <dd>
                                <asp:Literal ID="LiteralTotal" runat="server">{0:c}</asp:Literal>
                            </dd>
                        </dl>
                    </div>

                    <footer>
                        <p>ORG.NR: 202100-6271</p>
                        <p>Tack för besöket!</p>
                    </footer>

                </asp:Panel>
            </form>
        </div>

        <%-- Hand-written JavaScript --%>
        <script src="Content/OwnScript.js"></script>
    </div>
</body>
</html>
