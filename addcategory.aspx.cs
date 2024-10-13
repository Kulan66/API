using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace TechFix
{
    public partial class addcategory : System.Web.UI.Page
    {
        // Define the connection string from web.config
        private string connectionString = ConfigurationManager.ConnectionStrings["TechFixDB"].ConnectionString;

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryName.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Category (CategoryName) VALUES (@CategoryName)", conn);
                cmd.Parameters.AddWithValue("@CategoryName", categoryName);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Category added successfully!";
            txtCategoryName.Text = ""; // Clear input field after addition
        }
    }
}