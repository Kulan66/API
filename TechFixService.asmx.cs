using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace services
{
    /// <summary>
    /// Summary description for TechFixService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TechFixService : System.Web.Services.WebService
    {
        // Define the connection string from web.config
        private string connectionString = ConfigurationManager.ConnectionStrings["TechFixDB"].ConnectionString;


        // Method to get all products
        [WebMethod]
        public DataSet GetProducts()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT ProductID, Name, Stock, Price, CategoryID FROM Product", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Products");
            }
            return ds;
        }

        // Method to add a new product
        [WebMethod]
        public bool AddProduct(string name, int categoryId, int stock, decimal price)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Product (Name, CategoryID, Stock, Price) VALUES (@Name, @CategoryID, @Stock, @Price)", conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                cmd.Parameters.AddWithValue("@Stock", stock);
                cmd.Parameters.AddWithValue("@Price", price);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0; // Return true if insert was successful
            }
        }

        // Method to delete a product
        [WebMethod]
        public bool DeleteProduct(int productId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Product WHERE ProductID = @ProductID", conn);
                cmd.Parameters.AddWithValue("@ProductID", productId);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0; // Return true if delete was successful
            }
        }

        // Method to get all categories
        [WebMethod]
        public DataSet GetCategories()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryID, CategoryName FROM Category", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Categories");
            }
            return ds;
        }

        // Method to add a new category
        [WebMethod]
        public bool AddCategory(string categoryName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Category (CategoryName) VALUES (@CategoryName)", conn);
                cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0; // Return true if insert was successful
            }
        }

        // Method to delete a category
        [WebMethod]
        public bool DeleteCategory(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Category WHERE CategoryID = @CategoryID", conn);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0; // Return true if delete was successful
            }
        }

        // Method to get all orders
        [WebMethod]
        public DataSet GetOrders()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT OrderID, ProductID, Quantity, TotalPrice, OrderStatus FROM [Order]", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Orders");
            }
            return ds;
        }

        // Method to add a new order
        [WebMethod]
        public bool AddOrder(int customerId, int productId, int quantity, decimal totalPrice, string orderStatus)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [Order] (CustomerID, ProductID, Quantity, TotalPrice, OrderStatus) VALUES (@CustomerID, @ProductID, @Quantity, @TotalPrice, @OrderStatus)", conn);
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                cmd.Parameters.AddWithValue("@ProductID", productId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                cmd.Parameters.AddWithValue("@OrderStatus", orderStatus);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0; // Return true if insert was successful
            }
        }

        // Method to delete an order
        [WebMethod]
        public bool DeleteOrder(int orderId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM [Order] WHERE OrderID = @OrderID", conn);
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0; // Return true if delete was successful
            }
        }

        // Method to get all suppliers
        [WebMethod]
        public DataSet GetSuppliers()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT SupplierID, Username FROM Supplier", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Suppliers");
            }
            return ds;
        }

        // Method to add a new supplier
        [WebMethod]
        public bool AddSupplier(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Supplier (Username, Password) VALUES (@Username, @Password)", conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password); // Hash the password in production
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0; // Return true if insert was successful
            }
        }

        // Method to delete a supplier
        [WebMethod]
        public bool DeleteSupplier(int supplierId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Supplier WHERE SupplierID = @SupplierID", conn);
                cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0; // Return true if delete was successful
            }
        }

        
    }
}
