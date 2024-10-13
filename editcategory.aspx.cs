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
    public partial class editcategory : System.Web.UI.Page
    {
        // Define the connection string from web.config
        private string connectionString = ConfigurationManager.ConnectionStrings["TechFixDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int categoryId;
                if (int.TryParse(Request.QueryString["CategoryID"], out categoryId))
                {
                    LoadCategory(categoryId); // Load the category details
                }
                else
                {
                    lblMessage.Text = "Invalid category ID.";
                }
            }
        }

        // Load category details into the text box
        private void LoadCategory(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryName FROM Category WHERE CategoryID = @CategoryID", conn);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtCategoryName.Text = reader["CategoryName"].ToString();
                    hdnCategoryID.Value = categoryId.ToString();
                }
                else
                {
                    lblMessage.Text = "Category not found.";
                }
            }
        }

        // Update category button click event
        protected void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryName.Text.Trim();
            int categoryId = Convert.ToInt32(hdnCategoryID.Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Category SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID", conn);
                cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Category updated successfully!";
        }

        // Cancel button click event
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/adminhomepage.aspx"); // Redirect back to admin homepage
        }
    }
}