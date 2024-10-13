<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addcategory.aspx.cs" Inherits="TechFix.addcategory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
        <div class="container">
            <h2>Add New Category</h2>
            <div>
                <label for="txtCategoryName">Category Name:</label>
                <asp:TextBox ID="txtCategoryName" runat="server" required></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnAddCategory" runat="server" Text="Add Category" OnClick="btnAddCategory_Click" />
            </div>
            <div>
                <asp:Label ID="lblMessage" runat="server" ForeColor="green"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
