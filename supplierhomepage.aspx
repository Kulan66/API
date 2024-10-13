<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="supplierhomepage.aspx.cs" Inherits="TechFix.supplierhomepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
    body {
        font-family: Arial, sans-serif;
        background-color: #f4f4f4; /* Light gray background */
        margin: 0;
        padding: 20px;
    }

    h1 {
        text-align: center;
        color: #007bff; /* Bright blue for main heading */
    }

    h2 {
        color: #0056b3; /* Darker blue for section headings */
        margin-top: 20px;
        margin-bottom: 10px;
    }

    label {
        display: block;
        margin-bottom: 5px;
        color: #333; /* Dark text for labels */
    }

    table {
        width: 100%; /* Full width for tables */
        margin-bottom: 20px; /* Space below tables */
        border-collapse: collapse; /* Collapse borders */
    }

    td {
        padding: 10px; /* Padding for table cells */
        border: 1px solid #dee2e6; /* Light border */
    }

    .btn {
        background-color: #28a745; /* Green button */
        color: white;
        border: none;
        padding: 10px 15px;
        border-radius: 4px;
        cursor: pointer;
        font-size: 14px;
        transition: background-color 0.3s; /* Smooth transition */
    }

    .btn:hover {
        background-color: #218838; /* Darker green on hover */
    }

    /* Styling for GridView */
    .table {
        border-collapse: collapse; /* Collapse borders */
        width: 100%; /* Full width */
        margin-top: 10px; /* Margin above tables */
    }

    .table th, .table td {
        padding: 10px; /* Padding for cells */
        text-align: left; /* Align text to the left */
        border: 1px solid #dee2e6; /* Light border */
    }

    .table th {
        background-color: #007bff; /* Header background color */
        color: white; /* Header text color */
    }

    .table tr:nth-child(even) {
        background-color: #f9f9f9; /* Light gray for even rows */
    }

    /* Error message styling */
    #lblError {
        color: red; /* Red color for error messages */
        font-weight: bold; /* Bold text */
        margin-top: 10px; /* Space above */
    }
</style>

</head>
<body>
    <form id="form1" runat="server">
        <h1>Supplier Dashboard</h1>

        <!-- Add new product section -->
        <h2>Add New Product</h2>
        <asp:Label ID="lblError" runat="server" Text="" Visible="false"></asp:Label> <!-- Error message label -->
        <table>
            <tr>
                <td><label for="txtProductName">Product Name:</label></td>
                <td><asp:TextBox ID="txtProductName" runat="server" /></td>
            </tr>
            <tr>
                <td><label for="txtStock">Stock:</label></td>
                <td><asp:TextBox ID="txtStock" runat="server" TextMode="Number" /></td>
            </tr>
            <tr>
                <td><label for="txtPrice">Price:</label></td>
                <td><asp:TextBox ID="txtPrice" runat="server" TextMode="Number" /></td>
            </tr>
            <tr>
                <td><label for="ddlCategories">Category:</label></td>
                <td><asp:DropDownList ID="ddlCategories" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" CssClass="btn" OnClick="btnAddProduct_Click" />
                </td>
            </tr>
        </table>

        <!-- Display products added by the supplier -->
        <h2>Your Products</h2>
        <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False" GridLines="Both" CssClass="table">
            <Columns>
                <asp:BoundField DataField="ProductID" HeaderText="Product ID" ReadOnly="True" />
                <asp:BoundField DataField="Name" HeaderText="Product Name" />
                <asp:BoundField DataField="Stock" HeaderText="Stock" />
                <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                <asp:BoundField DataField="CategoryName" HeaderText="Category" />
            </Columns>
        </asp:GridView>

        <!-- Display orders related to this supplier -->
        <h2>Orders</h2>
        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" GridLines="Both" CssClass="table">
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="Order ID" ReadOnly="True" />
                <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="TotalPrice" HeaderText="Total Price" DataFormatString="{0:C}" />
                <asp:BoundField DataField="OrderStatus" HeaderText="Status" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnConfirmOrder" runat="server" CommandArgument='<%# Eval("OrderID") %>' Text="Confirm" CssClass="btn" OnClick="btnConfirmOrder_Click" />
                        <asp:Button ID="btnUnconfirmOrder" runat="server" CommandArgument='<%# Eval("OrderID") %>' Text="Unconfirm" CssClass="btn" OnClick="btnUnconfirmOrder_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
