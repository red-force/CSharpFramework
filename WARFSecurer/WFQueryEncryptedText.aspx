<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFQueryEncryptedText.aspx.cs" Inherits="WARFSecurer.WFQueryEncryptedText" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="Panel1" runat="server">
            <asp:Label ID="lEncryptionName" runat="server" Text="Encryption Name:"></asp:Label>
            <asp:DropDownList ID="ddlEncryptionName" runat="server" AutoPostBack="True" Height="16px" oninit="ddlEncryptionName_Init" onselectedindexchanged="ddlEncryptionName_SelectedIndexChanged" Width="112px">
            </asp:DropDownList>
            <br />
            <asp:Label ID="lText" runat="server" Text="Text:"></asp:Label>
            <asp:TextBox ID="tbText" runat="server" ontextchanged="tbText_TextChanged" TextMode="Password"></asp:TextBox>
            <asp:CheckBox ID="cbShowText" runat="server" AutoPostBack="True" oncheckedchanged="cbShowText_CheckedChanged" Text="Show" />
            <br />
            <asp:Label ID="lEncryptedText" runat="server" Text="Encrypted Text:"></asp:Label>
            <asp:TextBox ID="tbEncryptedText" runat="server" ReadOnly="True" Width="60px"></asp:TextBox>
            <asp:CheckBox ID="cbShowEncryptedText" runat="server" oncheckedchanged="cbShowEncryptedText_CheckedChanged" Text="Show" />
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
