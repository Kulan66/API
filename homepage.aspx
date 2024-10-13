<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="TechFix.homepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Homepage</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #e9f5ff; /* Light blue background */
            display: flex;
            justify-content: center;
            align-items: flex-start; /* Aligns items to the top */
            height: 100vh;
            margin: 0;
            padding: 20px; /* Add padding around the body */
            box-sizing: border-box; /* Include padding in width calculations */
        }

        #form1 {
            background-color: #ffffff; /* White background for the form */
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.2);
            width: 100%;
            max-width: 800px; /* Maximum width for better alignment */
            position: relative; /* Added for positioning child elements */
        }

        h1 {
            color: #007bff; /* Bright blue for the main heading */
            text-align: center;
            margin-bottom: 20px;
        }

        h2 {
            color: #007bff; /* Bright blue for the subheading */
            margin-top: 30px; /* Add some spacing above the heading */
        }

        label {
            display: block;
            margin-bottom: 5px;
            color: #333;
        }

        input[type="text"],
        input[type="password"],
        select {
            width: 100%;
            padding: 12px;
            margin-bottom: 15px;
            border: 1px solid #007bff; /* Blue border */
            border-radius: 4px;
            transition: border-color 0.3s; /* Smooth transition */
        }

        input[type="text"]:focus,
        input[type="password"]:focus,
        select:focus {
            border-color: #0056b3; /* Darker blue on focus */
            outline: none;
        }

        button {
            background-color: #007bff; /* Blue button */
            color: white;
            border: none;
            padding: 12px 15px;
            border-radius: 4px;
            cursor: pointer;
            width: 100%;
            font-size: 16px; /* Increased font size */
            transition: background-color 0.3s; /* Smooth transition */
        }

        button:hover {
            background-color: #0056b3; /* Darker blue on hover */
        }

        #gvProducts,
        #gvCart {
            width: 100%;
            margin-top: 20px; /* Add space between grids and other elements */
            border-collapse: collapse; /* Merge borders for a cleaner look */
        }

        #gvProducts th,
        #gvCart th {
            background-color: #007bff; /* Blue header for grid */
            color: white;
            padding: 10px;
        }

        #gvProducts td,
        #gvCart td {
            padding: 10px;
            border: 1px solid #ddd; /* Light gray border */
        }

        #lblTotalPrice {
            font-weight: bold;
            color: #333; /* Dark gray for total price */
        }

        #lblSearch {
            margin-top: 10px; /* Space above search label */
        }

        /* Styles for the admin/supplier login button */
        #btnLogin {
            position: absolute; /* Position it absolutely within the form */
            top: 20px; /* Distance from the top */
            right: 20px; /* Distance from the right */
            width: auto; /* Width should fit content */
            height: auto; /* Height should fit content */
            font-size: 14px; /* Font size can be adjusted */
        }
    </style>
</head>
<body>
   <form id="form1" runat="server">
        <div>
            <h1>Welcome to TechFix</h1>

            <!-- Search Bar -->
            <asp:Label ID="lblSearch" runat="server" Text="Search Products"></asp:Label>
            <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
            <asp:DropDownList ID="ddlCategory" runat="server" Width="150px">
                <asp:ListItem Text="All Categories" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" style="height: 29px" />

            <br /><br />

            <!-- Product Listing -->
            <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
                <Columns>
                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                    <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                    <asp:BoundField DataField="Stock" HeaderText="Stock Available" />
                    <asp:BoundField DataField="Price" HeaderText="Price ($)" DataFormatString="{0:C}" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:TextBox ID="txtQuantity" runat="server" Width="50px"></asp:TextBox>
                            <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" 
                                CommandArgument='<%# Eval("ProductID") %>' 
                                OnClick="btnAddToCart_Click" />
                            <!-- Adding Labels to access product details -->
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <br /><br />

            <!-- Cart Section -->
            <h2>Your Cart</h2>
            <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="Price" HeaderText="Price ($)" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="TotalPrice" HeaderText="Total Price ($)" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblTotalPrice" runat="server" Text="Total: $0.00"></asp:Label>
            <br />
            <asp:Button ID="btnCheckout" runat="server" Text="Checkout" OnClick="btnCheckout_Click" />

            <br /><br />

            <!-- Confirmed Orders Section -->
            <h2>Confirmed Orders</h2>
            <asp:GridView ID="gvConfirmedOrders" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                    <asp:BoundField DataField="CustomerID" HeaderText="Customer ID" />
                    <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="TotalPrice" HeaderText="Total Price ($)" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="OrderStatus" HeaderText="Order Status" />
                </Columns>
            </asp:GridView>
            <br />

              <!-- Refresh Orders Button -->
            <asp:Button ID="btnRefreshOrders" runat="server" Text="Refresh Orders" OnClick="btnRefreshOrders_Click" />
            <br /><br />

            <!-- Login Button for Admin and Supplier -->
            <asp:Button ID="btnLogin" runat="server" Text="Admin/Supplier Login" PostBackUrl="~/loginpage.aspx" />
        </div>
    </form>
</body>
</html>
