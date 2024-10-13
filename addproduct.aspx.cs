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
    public partial class addproduct : System.Web.UI.Page
    {
        // Define the connection string from web.config
        private string connectionString = ConfigurationManager.ConnectionStrings["TechFixDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories(); // Load categories into dropdown on initial load
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

        // Add product button click event
        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            string productName = txtProductName.Text.Trim();
            int categoryId = Convert.ToInt32(ddlCategories.SelectedValue);
            int stock = Convert.ToInt32(txtStock.Text.Trim());
            decimal price = Convert.ToDecimal(txtPrice.Text.Trim());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Product (Name, CategoryID, Stock, Price) VALUES (@Name, @CategoryID, @Stock, @Price)", conn);
                cmd.Parameters.AddWithValue("@Name", productName);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                cmd.Parameters.AddWithValue("@Stock", stock);
                cmd.Parameters.AddWithValue("@Price", price);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Product added successfully!";
            ClearFields(); // Optionally clear fields after addition
        }

        // Optional: Clear input fields after adding a product
        private void ClearFields()
        {
            txtProductName.Text = "";
            ddlCategories.SelectedIndex = 0;
            txtStock.Text = "";
            txtPrice.Text = "";
        }
    }
}