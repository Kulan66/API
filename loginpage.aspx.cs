using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechFix
{
    public partial class loginpage : System.Web.UI.Page
    {
        // Hardcoded login credentials for Admin and Supplier
        private string adminUsername = "admin";
        private string adminPassword = "admin123";
        private string supplierUsername = "supplier";
        private string supplierPassword = "supplier123";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Method to handle the login button click event
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Check if the login credentials match the admin account
            if (username == adminUsername && password == adminPassword)
            {
                // Redirect to Admin homepage
                Response.Redirect("~/adminhomepage.aspx");
            }
            // Check if the login credentials match the supplier account
            else if (username == supplierUsername && password == supplierPassword)
            {
                // Redirect to Supplier homepage
                Response.Redirect("~/supplierhomepage.aspx");
            }
            else
            {
                // Display an error message if login fails
                lblMessage.Text = "Invalid username or password. Please try again.";
            }
        }
    }
}
