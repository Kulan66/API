<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editcategory.aspx.cs" Inherits="TechFix.editcategory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
        <div class="container">
            <h2>Edit Category</h2>
            <div>
                <label for="txtCategoryName">Category Name:</label>
                <asp:TextBox ID="txtCategoryName" runat="server" required></asp:TextBox>
                <asp:HiddenField ID="hdnCategoryID" runat="server" />
            </div>
            <div>
                <asp:Button ID="btnUpdateCategory" runat="server" Text="Update Category" OnClick="btnUpdateCategory_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
            <div>
                <asp:Label ID="lblMessage" runat="server" ForeColor="green"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
