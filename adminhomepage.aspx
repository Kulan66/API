<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminhomepage.aspx.cs" Inherits="TechFix.adminhomepage" %>

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
        color: #007bff; /* Bright blue for the main heading */
    }

    h2 {
        color: #0056b3; /* Darker blue for section headings */
        margin-top: 20px;
        margin-bottom: 10px;
    }

    div {
        margin: 20px auto; /* Center divs */
        padding: 20px;
        background-color: #ffffff; /* White background for content */
        border-radius: 8px;
        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        max-width: 800px; /* Limit the width */
    }

    button {
        background-color: #28a745; /* Green button for actions */
        color: white;
        border: none;
        padding: 10px 15px;
        border-radius: 4px;
        cursor: pointer;
        font-size: 14px;
        margin-bottom: 10px; /* Space between buttons */
        transition: background-color 0.3s;
    }

    button:hover {
        background-color: #218838; /* Darker green on hover */
    }

    /* GridView styling */
    .gridview-header {
        background-color: #007bff; /* Header background */
        color: white;
    }

    .gridview-row {
        background-color: #ffffff; /* White for rows */
    }

    .gridview-row:nth-child(even) {
        background-color: #f9f9f9; /* Light gray for even rows */
    }

    /* Styling for GridView */
    table {
        width: 100%; /* Full width */
        border-collapse: collapse; /* Collapse borders */
    }

    th, td {
        padding: 10px; /* Padding for cells */
        text-align: left; /* Align text to the left */
        border: 1px solid #dee2e6; /* Light border */
    }

    th {
        background-color: #007bff; /* Header background */
        color: white; /* Header text color */
    }

    td {
        color: #333; /* Dark text for rows */
    }

    /* Responsive adjustments */
    @media screen and (max-width: 600px) {
        button {
            width: 100%; /* Full width buttons on small screens */
        }

        div {
            padding: 15px; /* Reduced padding for smaller screens */
        }

        th, td {
            font-size: 12px; /* Smaller font size for smaller screens */
        }
    }
</style>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Admin Dashboard</h1>

            <!-- Manage Products -->
            <h2>Manage Products</h2>
            <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" OnClick="btnAddProduct_Click" />
            <br />
            <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
                <Columns>
                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                    <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                    <asp:BoundField DataField="Stock" HeaderText="Stock" />
                    <asp:BoundField DataField="Price" HeaderText="Price ($)" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnEditProduct" runat="server" Text="Edit" 
                                CommandArgument='<%# Eval("ProductID") %>' 
                                OnClick="btnEditProduct_Click" />
                            <asp:Button ID="btnDeleteProduct" runat="server" Text="Delete" 
                                CommandArgument='<%# Eval("ProductID") %>' 
                                OnClick="btnDeleteProduct_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <!-- Manage Categories -->
            <h2>Manage Categories</h2>
            <asp:Button ID="btnAddCategory" runat="server" Text="Add Category" OnClick="btnAddCategory_Click" />
            <br />
            <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnEditCategory" runat="server" Text="Edit" 
                                CommandArgument='<%# Eval("CategoryID") %>' 
                                OnClick="btnEditCategory_Click" />
                            <asp:Button ID="btnDeleteCategory" runat="server" Text="Delete" 
                                CommandArgument='<%# Eval("CategoryID") %>' 
                                OnClick="btnDeleteCategory_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <!-- Manage Orders -->
            <h2>Manage Orders</h2>
            <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="TotalPrice" HeaderText="Total Price ($)" />
                    <asp:BoundField DataField="OrderStatus" HeaderText="Status" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnConfirmOrder" runat="server" Text="Confirm" 
                                CommandArgument='<%# Eval("OrderID") %>' 
                                OnClick="btnConfirmOrder_Click" />
                            <asp:Button ID="btnUnconfirmOrder" runat="server" Text="Unconfirm" 
                                CommandArgument='<%# Eval("OrderID") %>' 
                                OnClick="btnUnconfirmOrder_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <!-- Manage Suppliers -->
            <h2>Manage Suppliers</h2>
            <asp:Button ID="btnAddSupplier" runat="server" Text="Add Supplier" OnClick="btnAddSupplier_Click" />
            <br />
            <asp:GridView ID="gvSuppliers" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Username" HeaderText="Supplier Username" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnEditSupplier" runat="server" Text="Edit" 
                                CommandArgument='<%# Eval("SupplierID") %>' 
                                OnClick="btnEditSupplier_Click" />
                            <asp:Button ID="btnDeleteSupplier" runat="server" Text="Delete" 
                                CommandArgument='<%# Eval("SupplierID") %>' 
                                OnClick="btnDeleteSupplier_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
