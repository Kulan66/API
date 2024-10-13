<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addsupplier.aspx.cs" Inherits="TechFix.addsupplier" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Add Supplier</h2>
            <div>
                <label for="txtUsername">Username:</label>
                <asp:TextBox ID="txtUsername" runat="server" required></asp:TextBox>
            </div>
            <div>
                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" required></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnAddSupplier" runat="server" Text="Add Supplier" OnClick="btnAddSupplier_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
            <div>
                <asp:Label ID="lblMessage" runat="server" ForeColor="green"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
