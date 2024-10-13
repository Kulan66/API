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
    public partial class addsupplier : System.Web.UI.Page
    {
        // Define the connection string from web.config
        private string connectionString = ConfigurationManager.ConnectionStrings["TechFixDB"].ConnectionString;

        // Add supplier button click event
        protected void btnAddSupplier_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Supplier (Username, Password) VALUES (@Username, @Password)", conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password); // In production, ensure password is hashed

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Supplier added successfully!";
        }

        // Cancel button click event
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/adminhomepage.aspx"); // Redirect back to admin homepage
        }
    }
}