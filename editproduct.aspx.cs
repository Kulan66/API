using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechFix
{
    public partial class editproduct : System.Web.UI.Page
    {
        // Define the connection string from web.config
        private string connectionString = ConfigurationManager.ConnectionStrings["TechFixDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories(); // Load categories into dropdown
                LoadProduct(); // Load the product details if in edit mode
            }
        }

        // Load categories into the dropdown list
        private void LoadCategories()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryID, CategoryName FROM Category", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlCategories.DataSource = reader;
                ddlCategories.DataTextField = "CategoryName";
                ddlCategories.DataValueField = "CategoryID";
                ddlCategories.DataBind();
            }
            ddlCategories.Items.Insert(0, new ListItem("--Select Category--", "0")); // Optional: Default item
        }

        // Load the product details based on ProductID from query string
        private void LoadProduct()
        {
            int productId;
            if (int.TryParse(Request.QueryString["ProductID"], out productId))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT Name, CategoryID, Stock, Price FROM Product WHERE ProductID = @ProductID", conn);
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtProductName.Text = reader["Name"].ToString();
                        ddlCategories.SelectedValue = reader["CategoryID"].ToString();
                        txtStock.Text = reader["Stock"].ToString();
                        txtPrice.Text = reader["Price"].ToString();
                    }
                }
            }
            else
            {
                lblMessage.Text = "Invalid Product ID.";
            }
        }

        // Update product button click event
        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            int productId;
            if (int.TryParse(Request.QueryString["ProductID"], out productId))
            {
                string productName = txtProductName.Text.Trim();
                int categoryId = Convert.ToInt32(ddlCategories.SelectedValue);
                int stock = Convert.ToInt32(txtStock.Text.Trim());
                decimal price = Convert.ToDecimal(txtPrice.Text.Trim());

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Product SET Name = @Name, CategoryID = @CategoryID, Stock = @Stock, Price = @Price WHERE ProductID = @ProductID", conn);
                    cmd.Parameters.AddWithValue("@Name", productName);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMessage.Text = "Product updated successfully!";
            }
            else
            {
                lblMessage.Text = "Invalid Product ID.";
            }
        }
    }

}