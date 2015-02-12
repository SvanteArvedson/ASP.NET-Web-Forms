<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Hur_manga_versaler.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="sv">
<head runat="server">
    <title>Hur många versaler</title>
    <link href="~/Content/Site.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <h1>Hur många versaler?</h1>
        <form id="form1" runat="server">
            <div>
                <asp:TextBox ID="TextBox" runat="server" TextMode="MultiLine" ViewStateMode="Enabled"></asp:TextBox>
                <asp:Label ID="ResultLabel" runat="server" Visible="False"></asp:Label>
                <asp:Button ID="SendButton" runat="server" Text="Bestäm antalet versaler" OnClick="SendButton_Click" />
            </div>
        </form>
    </div>
</body>
</html>
