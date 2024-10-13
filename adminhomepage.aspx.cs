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
    public partial class adminhomepage : System.Web.UI.Page
    {
        // Define the connection string from web.config
        private string connectionString = ConfigurationManager.ConnectionStrings["TechFixDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();   // Load all products
                LoadCategories(); // Load all categories
                LoadOrders();     // Load all orders
                LoadSuppliers();  // Load all suppliers
            }
        }

        // Load all products from the database
        private void LoadProducts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT ProductID, Name AS ProductName, Stock, Price, CategoryName " +
                    "FROM Product INNER JOIN Category ON Product.CategoryID = Category.CategoryID", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
        }

        // Load all categories from the database
        private void LoadCategories()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryID, CategoryName FROM Category", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvCategories.DataSource = dt;
                gvCategories.DataBind();
            }
        }

        // Load all orders from the database
        private void LoadOrders()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Join with Product table to get ProductName
                SqlCommand cmd = new SqlCommand(
                    @"SELECT o.OrderID, 
                             o.ProductID, 
                             p.Name AS ProductName, 
                             o.Quantity, 
                             o.TotalPrice, 
                             o.OrderStatus 
                      FROM [Order] o 
                      INNER JOIN Product p ON o.ProductID = p.ProductID",
                    conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvOrders.DataSource = dt;
                gvOrders.DataBind();
            }
        }

        // Load all suppliers from the database
        private void LoadSuppliers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT SupplierID, Username FROM Supplier", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvSuppliers.DataSource = dt;
                gvSuppliers.DataBind();
            }
        }

        // Add product button click event
        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/addproduct.aspx"); // Redirect to a page where admin can add new products
        }

        // Edit product button click event
        protected void btnEditProduct_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int productId = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect($"~/editproduct.aspx?ProductID={productId}");
        }

        // Delete product button click event
        protected void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int productId = Convert.ToInt32(btn.CommandArgument);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Product WHERE ProductID = @ProductID", conn);
                cmd.Parameters.AddWithValue("@ProductID", productId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            LoadProducts(); // Reload products after delete
        }

        // Add category button click event
        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/addcategory.aspx"); // Redirect to a page where admin can add new categories
        }

        // Edit category button click event
        protected void btnEditCategory_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int categoryId = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect($"~/editcategory.aspx?CategoryID={categoryId}");
        }

        // Delete category button click event
        protected void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int categoryId = Convert.ToInt32(btn.CommandArgument);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Category WHERE CategoryID = @CategoryID", conn);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            LoadCategories(); // Reload categories after delete
        }

        // Confirm order button click event
        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int orderId = Convert.ToInt32(btn.CommandArgument);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE [Order] SET OrderStatus = 'Confirmed' WHERE OrderID = @OrderID", conn);
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            LoadOrders(); // Reload orders after confirmation
        }

        // Unconfirm order button click event
        protected void btnUnconfirmOrder_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int orderId = Convert.ToInt32(btn.CommandArgument);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE [Order] SET OrderStatus = 'Unconfirmed' WHERE OrderID = @OrderID", conn);
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            LoadOrders(); // Reload orders after unconfirmation
        }

        // Add supplier button click event
        protected void btnAddSupplier_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/addsupplier.aspx"); // Redirect to a page where admin can add new suppliers
        }

        // Edit supplier button click event
        protected void btnEditSupplier_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int supplierId = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect($"~/editsupplier.aspx?SupplierID={supplierId}");
        }

        // Delete supplier button click event
        protected void btnDeleteSupplier_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int supplierId = Convert.ToInt32(btn.CommandArgument);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Supplier WHERE SupplierID = @SupplierID", conn);
                cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            LoadSuppliers(); // Reload suppliers after delete
        }

    }
}