using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using CapstoneSaleProject.Models;

namespace CapstoneSaleProject.Data
{

    public class SaleRepository
    {
        private readonly String _connectionString;
        public SaleRepository(string connectionString) => _connectionString = connectionString;


        public void RecordSale(int productId, int quantity)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var query = "INSERT INTO Sales (ProductId, Quantity) VALUES (@pid, @qty)";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@pid", productId);
            cmd.Parameters.AddWithValue("@qty", quantity);
            cmd.ExecuteNonQuery();
        }

        public List<Sale> GetAllSales()
        {
            var sales = new List<Sale>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var query = @"SELECT p.Name, s.Quantity, p.Price
                          FROM Sales s
                          JOIN Products p ON s.ProductId = p.Id
                          ORDER BY s.SaleDate DESC";
            using var cmd = new SqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                sales.Add(new Sale
                {
                    ProductName = reader["Name"].ToString(),
                    Quantity = (int)reader["Quantity"],
                    Price = (decimal)reader["Price"]
                });
            }
            return sales;
        }

    }
 
}
