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
    public partial class editsupplier : System.Web.UI.Page
    {
        // Define the connection string from web.config
        private string connectionString = ConfigurationManager.ConnectionStrings["TechFixDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int supplierId;

                // Check if SupplierID is provided in the query string
                if (int.TryParse(Request.QueryString["SupplierID"], out supplierId))
                {
                    LoadSupplierDetails(supplierId); // Load the supplier details
                }
                else
                {
                    lblMessage.Text = "Invalid Supplier ID.";
                }
            }
        }

        // Load supplier details from the database
        private void LoadSupplierDetails(int supplierId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Username, Password FROM Supplier WHERE SupplierID = @SupplierID", conn);
                cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtUsername.Text = reader["Username"].ToString();
                    txtPassword.Text = reader["Password"].ToString(); // Consider hashing in production
                }
                else
                {
                    lblMessage.Text = "Supplier not found.";
                }
            }
        }

        // Update supplier button click event
        protected void btnUpdateSupplier_Click(object sender, EventArgs e)
        {
            int supplierId;
            if (int.TryParse(Request.QueryString["SupplierID"], out supplierId))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Supplier SET Username = @Username, Password = @Password WHERE SupplierID = @SupplierID", conn);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim()); // In production, ensure password is hashed
                    cmd.Parameters.AddWithValue("@SupplierID", supplierId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                lblMessage.Text = "Supplier updated successfully!";
            }
        }

        // Cancel button click event
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/adminhomepage.aspx"); // Redirect back to admin homepage
        }
    }
}