<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Converter.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="sv">
<head runat="server">
    <title>Konvertera temperaturer</title>
    <link href="~/Styles/Site.css" rel="stylesheet" />
</head>
<body>
    <div id="Container">
        <header>
            <h1>Konvertera temperaturer</h1>
        </header>
        <div id="Content">
            <form id="form1" runat="server" defaultbutton="ConvertTemperatureButton" defaultfocus="StartTemperature">
                <div id="ContentBoxLeft" class="ContentBox">
                    <%-- Validation summary --%>
                    <asp:ValidationSummary ID="ErrorList" runat="server" DisplayMode="BulletList" CssClass="Error" />
                    <%-- Input Start Temperature --%>
                    <div class="InputBox">
                        <asp:Label ID="LabelStartTemperature" runat="server" Text="Starttemperatur:" AssociatedControlID="StartTemperature" CssClass="Label"></asp:Label>
                        <asp:TextBox ID="StartTemperature" runat="server" CssClass="TextBox"></asp:TextBox>
                        <%-- Validation Controls --%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Starttemperatur får inte vara tomt." Text="*" Display="Dynamic" ControlToValidate="StartTemperature" CssClass="Error" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Starttemperatur måste vara ett heltal." ControlToValidate="StartTemperature" Operator="DataTypeCheck" Type="Integer" Display="Dynamic" Text="*" CssClass="Error" SetFocusOnError="True"></asp:CompareValidator>
                    </div>
                    <%-- Input End Temperature --%>
                    <div class="InputBox">
                        <asp:Label ID="LabelEndTemperature" runat="server" Text="Sluttemperatur:" AssociatedControlID="EndTemperature" CssClass="Label"></asp:Label>
                        <asp:TextBox ID="EndTemperature" runat="server" CssClass="TextBox"></asp:TextBox>
                        <%-- Validation Controls --%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Sluttemperatur får inte vara tomt." Display="Dynamic" ControlToValidate="EndTemperature" Text="*" CssClass="Error" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Sluttemperatur måste vara ett heltal större än starttemperaturen." Display="Dynamic" Text="*" Type="Integer" Operator="GreaterThan" ControlToValidate="EndTemperature" ControlToCompare="StartTemperature" CssClass="Error" SetFocusOnError="True"></asp:CompareValidator>
                    </div>
                    <%-- Input Temperature Step --%>
                    <div class="InputBox">
                        <asp:Label ID="LabelTemperatureStep" runat="server" Text="Temperatursteg:" AssociatedControlID="TemperatureStep" CssClass="Label"></asp:Label>
                        <asp:TextBox ID="TemperatureStep" runat="server" CssClass="TextBox"></asp:TextBox>
                        <%-- Validation Controls --%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Temperatursteg får inte vara tomt." Display="Dynamic" ControlToValidate="TemperatureStep" Text="*" CssClass="Error" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Temperatursteg måste vara ett heltal mellan 1 och 100." Display="Dynamic" ControlToValidate="TemperatureStep" Text="*" Type="Integer" MinimumValue="1" MaximumValue="100" CssClass="Error" SetFocusOnError="True"></asp:RangeValidator>
                    </div>
                    <%-- Input Convert Direction --%>
                    <div class="InputBox">
                        <asp:Label ID="LabelRadioButton" runat="server" Text="Typ av konvertering" AssociatedControlID="RadioButtonConvertDirection" CssClass="LabelRadioButtons"></asp:Label>
                        <asp:RadioButtonList ID="RadioButtonConvertDirection" runat="server" RepeatLayout="UnorderedList" RepeatDirection="Vertical">
                            <asp:ListItem Selected="True" Value="CtoF">Celcius till Farenheit</asp:ListItem>
                            <asp:ListItem Value="FtoC">Farenheit till Celcius</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <%-- Send Data --%>
                    <div class="InputBox">
                        <asp:Button ID="ConvertTemperatureButton" runat="server" Text="Konvertera" OnClick="ConvertTemperatureButton_Click" />
                    </div>
                </div>

                <div id="ContentBoxRight" class="ContentBox">
                    <asp:Table ID="ResultTable" runat="server" Visible="false">
                        <%-- Place for the result --%>
                    </asp:Table>
                </div>
            </form>
        </div>
        <footer></footer>
    </div>
</body>
</html>
