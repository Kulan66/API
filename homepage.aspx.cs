using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TechFix
{
    public partial class homepage : System.Web.UI.Page
    {
        // Define the connection string from web.config
        private string connectionString = ConfigurationManager.ConnectionStrings["TechFixDB"].ConnectionString;

        // This DataTable will hold the cart items
        private DataTable cartTable;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories(); // Load product categories in the dropdown
                LoadProducts();   // Load products for display
                InitializeCart(); // Initialize cart on first load
                LoadConfirmedOrders();
            }
        }

        // Method to load categories from the database
        private void LoadCategories()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryID, CategoryName FROM Category", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlCategory.DataSource = reader;
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryID";
                ddlCategory.DataBind();
            }
            ddlCategory.Items.Insert(0, new ListItem("All Categories", "0")); // Add "All Categories" as the first option
        }

        // Method to load products from the database
        private void LoadProducts(string search = "", int categoryId = 0)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductID, Name AS ProductName, Stock, Price, CategoryName FROM Product INNER JOIN Category ON Product.CategoryID = Category.CategoryID WHERE Stock > 0";

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND Name LIKE '%' + @Search + '%'";
                }

                if (categoryId != 0)
                {
                    query += " AND Product.CategoryID = @CategoryID";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Search", search);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
        }

        // Method to handle search functionality
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();
            int categoryId = int.Parse(ddlCategory.SelectedValue);
            LoadProducts(search, categoryId);
        }

        // Initialize the cart when the page is first loaded
        private void InitializeCart()
        {
            if (Session["Cart"] == null)
            {
                cartTable = new DataTable();
                cartTable.Columns.Add("ProductID", typeof(int));
                cartTable.Columns.Add("ProductName", typeof(string));
                cartTable.Columns.Add("Quantity", typeof(int));
                cartTable.Columns.Add("Price", typeof(decimal));
                cartTable.Columns.Add("TotalPrice", typeof(decimal), "Quantity * Price");
                Session["Cart"] = cartTable;
            }
            else
            {
                cartTable = (DataTable)Session["Cart"];
                BindCart(); // Bind the existing cart to the GridView
            }
        }

        // Method to add a product to the cart
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int productId = Convert.ToInt32(btn.CommandArgument);

                GridViewRow row = (GridViewRow)btn.NamingContainer;

                // Retrieve the labels containing product name and price
                string productName = ((Label)row.FindControl("lblProductName")).Text;
                int quantity = int.Parse(((TextBox)row.FindControl("txtQuantity")).Text);
                decimal price = Convert.ToDecimal(((Label)row.FindControl("lblPrice")).Text.Trim('$')); // Remove the $ symbol for conversion

                AddToCart(productId, productName, quantity, price);
            }
            catch (Exception ex)
            {
                // Handle any potential errors, e.g., invalid quantity, etc.
                Response.Write("<script>alert('Error: Unable to find product details. " + ex.Message + "');</script>");
            }
        }

        // Method to add the selected product to the cart table
        private void AddToCart(int productId, string productName, int quantity, decimal price)
        {
            cartTable = (DataTable)Session["Cart"];

            // Check if the product already exists in the cart
            DataRow[] rows = cartTable.Select("ProductID = " + productId);
            if (rows.Length == 0)
            {
                // Add new product to cart
                DataRow dr = cartTable.NewRow();
                dr["ProductID"] = productId;
                dr["ProductName"] = productName;
                dr["Quantity"] = quantity;
                dr["Price"] = price;
                cartTable.Rows.Add(dr);
            }
            else
            {
                // Update the quantity if the product already exists in the cart
                rows[0]["Quantity"] = (int)rows[0]["Quantity"] + quantity;
            }

            // Update the session and bind the cart again
            Session["Cart"] = cartTable;
            BindCart();
        }

        // Method to bind the cart data to the GridView
        private void BindCart()
        {
            gvCart.DataSource = cartTable;
            gvCart.DataBind();
            UpdateCartTotal();
        }

        // Update the total price of the cart
        private void UpdateCartTotal()
        {
            decimal totalPrice = 0;
            foreach (DataRow row in cartTable.Rows)
            {
                totalPrice += (decimal)row["TotalPrice"];
            }
            lblTotalPrice.Text = "Total: $" + totalPrice.ToString("0.00");
        }

        // Method to handle the checkout process
        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            // Ensure cartTable is initialized
            if (Session["Cart"] == null)
            {
                InitializeCart(); // Initialize if it wasn't done
            }
            SaveOrderToDatabase(); // Proceed to save order
            Session["Cart"] = null; // Clear the cart after checkout
            InitializeCart();       // Re-initialize the cart for future orders
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Order completed successfully!');", true);
        }

        // Method to save the order to the database
        private void SaveOrderToDatabase()
        {
            cartTable = (DataTable)Session["Cart"]; // Retrieve cart from session
            if (cartTable == null || cartTable.Rows.Count == 0) // Check if cart is empty
            {
                throw new InvalidOperationException("Unable to find product details.");
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (DataRow row in cartTable.Rows)
                {
                    // Save each order to the database with CustomerID = 1
                    SqlCommand cmd = new SqlCommand("INSERT INTO [Order] (CustomerID, ProductID, Quantity, TotalPrice, OrderStatus) VALUES (1, @ProductID, @Quantity, @TotalPrice, 'Pending')", conn);
                    cmd.Parameters.AddWithValue("@ProductID", row["ProductID"]);
                    cmd.Parameters.AddWithValue("@Quantity", row["Quantity"]);
                    cmd.Parameters.AddWithValue("@TotalPrice", row["TotalPrice"]);
                    cmd.ExecuteNonQuery();

                    // Update stock in the Product table
                    UpdateProductStock((int)row["ProductID"], (int)row["Quantity"]);
                }
            }
        }

        // Method to update the stock of a product
        private void UpdateProductStock(int productId, int quantitySold)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Product SET Stock = Stock - @Quantity WHERE ProductID = @ProductID", conn);
                cmd.Parameters.AddWithValue("@ProductID", productId);
                cmd.Parameters.AddWithValue("@Quantity", quantitySold);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void LoadConfirmedOrders()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT OrderID, CustomerID, ProductID, Quantity, TotalPrice, OrderStatus FROM [Order] WHERE OrderStatus = 'Confirmed'", conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvConfirmedOrders.DataSource = dt;
                gvConfirmedOrders.DataBind();
            }
        }


        protected void btnRefreshOrders_Click(object sender, EventArgs e)
        {
            LoadConfirmedOrders();
        }
    }
}