using CapstoneSaleProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CapstoneSaleProject.Data
{
    public class ProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(string connectionString) => _connectionString = connectionString;

        public void AddProduct(Product product)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var query = "INSERT INTO Products (Name, Price, Stock) VALUES (@name, @price, @stock)";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@stock", product.Stock);
            cmd.ExecuteNonQuery();
        }

        public int GetProductId(string productName)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var query = "SELECT Id FROM Products WHERE Name = @name";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", productName);
            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : -1;
        }
        public int GetProductStock(int productId)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var query = "SELECT Stock FROM Products WHERE Id = @id";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", productId);
            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }



        public void UpdateProductStock(int productId, int newStock)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var query = "UPDATE Products SET Stock = @stock WHERE Id = @id";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@stock", newStock);
            cmd.Parameters.AddWithValue("@id", productId);
            cmd.ExecuteNonQuery();
        }


        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var query = "SELECT Name, Price, Stock FROM Products";
            using var cmd = new SqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Name = reader["Name"].ToString(),
                    Price = (decimal)reader["Price"],
                    Stock = (int)reader["Stock"]
                });
            }
            return products;
        }


    }
}
