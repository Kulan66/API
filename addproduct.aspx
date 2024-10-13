<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addproduct.aspx.cs" Inherits="TechFix.addproduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
        <div class="container">
            <h2>Add New Product</h2>
            <div>
                <label for="txtProductName">Product Name:</label>
                <asp:TextBox ID="txtProductName" runat="server" required></asp:TextBox>
            </div>
            <div>
                <label for="ddlCategories">Category:</label>
                <asp:DropDownList ID="ddlCategories" runat="server" required></asp:DropDownList>
            </div>
            <div>
                <label for="txtStock">Stock:</label>
                <asp:TextBox ID="txtStock" runat="server" required></asp:TextBox>
            </div>
            <div>
                <label for="txtPrice">Price:</label>
                <asp:TextBox ID="txtPrice" runat="server" required></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" OnClick="btnAddProduct_Click" />
            </div>
            <div>
                <asp:Label ID="lblMessage" runat="server" ForeColor="green"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
