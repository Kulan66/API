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
    public partial class supplierhomepage : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TechFixDB"].ConnectionString;

        // Use a fixed Supplier ID
        private const int SupplierId = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                LoadProducts();
                LoadOrders();
            }
        }

        private void LoadCategories()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryID, CategoryName FROM Category", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlCategories.DataSource = dt;
                ddlCategories.DataTextField = "CategoryName";
                ddlCategories.DataValueField = "CategoryID";
                ddlCategories.DataBind();
            }
        }

        private void LoadProducts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT ProductID, Name, Stock, Price, CategoryName FROM Product INNER JOIN Category ON Product.CategoryID = Category.CategoryID WHERE SupplierID = @SupplierID", conn);
                cmd.Parameters.AddWithValue("@SupplierID", SupplierId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
        }

        private void LoadOrders()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT OrderID, ProductID, Quantity, TotalPrice, OrderStatus FROM [Order] WHERE ProductID IN (SELECT ProductID FROM Product WHERE SupplierID = @SupplierID)", conn);
                cmd.Parameters.AddWithValue("@SupplierID", SupplierId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvOrders.DataSource = dt;
                gvOrders.DataBind();
            }
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                // Input validation
                if (string.IsNullOrWhiteSpace(txtProductName.Text) || string.IsNullOrWhiteSpace(txtStock.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
                {
                    lblError.Text = "All fields are required.";
                    lblError.Visible = true;
                    return;
                }

                string productName = txtProductName.Text.Trim();
                int stock = int.Parse(txtStock.Text);
                decimal price = decimal.Parse(txtPrice.Text);
                int categoryId = int.Parse(ddlCategories.SelectedValue);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Product (Name, Stock, Price, CategoryID, SupplierID) VALUES (@Name, @Stock, @Price, @CategoryID, @SupplierID)", conn);
                    cmd.Parameters.AddWithValue("@Name", productName);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierId); // Use fixed Supplier ID

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadProducts();
                ClearFields();
                lblError.Visible = false; // Hide error label if operation was successful
            }
            catch (SqlException ex)
            {
                lblError.Text = "Database error: " + ex.Message;
                lblError.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occurred: " + ex.Message;
                lblError.Visible = true;
            }
        }

        private void ClearFields()
        {
            txtProductName.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtPrice.Text = string.Empty;
        }

        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int orderId = Convert.ToInt32(btn.CommandArgument);
            UpdateOrderStatus(orderId, "Confirmed");
        }

        protected void btnUnconfirmOrder_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int orderId = Convert.ToInt32(btn.CommandArgument);
            UpdateOrderStatus(orderId, "Unconfirmed");
        }

        private void UpdateOrderStatus(int orderId, string status)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE [Order] SET OrderStatus = @OrderStatus WHERE OrderID = @OrderID", conn);
                cmd.Parameters.AddWithValue("@OrderStatus", status);
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            LoadOrders();
        }
    }
}