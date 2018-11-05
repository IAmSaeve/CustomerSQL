using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SQLRestCustomerService.Model;
using MySqlConnector;
using MySql.Data.MySqlClient;
using System.Data.Sql;
using System.Data.SqlClient;

namespace SQLRestCustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // MySQL connection string.
        // private const string connection = "server=192.168.122.105; uid=root; pwd=password; database=CustomerDB";

        // MSSQL connection string.
        private const string connection = "Server=tcp:savecustomerdb.database.windows.net,1433; Initial Catalog=CustomerDB;" +
        "Persist Security Info=False; User ID=Sebastian; Password=!mrjo4gqD$xoYy; MultipleActiveResultSets=False;" +
        "Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // GET: api/Customers
        [HttpGet]
        public List<Customer> Get()
        {
            var result = new List<Customer>();
            var sql = "SELECT * FROM Customer";
            // var db = new MySqlConnection(connection);
            var db = new SqlConnection(connection);
            db.Open();

            // var command = new MySqlCommand(sql, db);
            var command = new SqlCommand(sql, db);
            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)));
                }
            }
            db.Dispose();
            return result;
        }

        // GET: api/Customers/1
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            Customer result = null;
            var sql = $"SELECT * FROM Customer WHERE id = '{id}'";
            // var db = new MySqlConnection(connection);
            var db = new SqlConnection(connection);
            db.Open();

            // var command = new MySqlCommand(sql, db);
            var command = new SqlCommand(sql, db);
            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result = new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                }
            }
            db.Dispose();
            return result;
        }

        // POST api/Customers
        [HttpPost]
        public void InsertCustomer(Customer customer)
        {
            var sql = "INSERT INTO Customer (id, firstName, lastName, cYear)" +
            $"VALUES (NULL, '{customer.FirstName}', '{customer.LastName}', '{customer.Year}')";
            // var db = new MySqlConnection(connection);
            var db = new SqlConnection(connection);
            db.Open();

            // var command = new MySqlCommand(sql, db);
            var command = new SqlCommand(sql, db);
            var reader = command.ExecuteReader();
            db.Dispose();
        }

        // PUT api/Customers/5
        [HttpPut("{id}")]
        public void UpdateCustomer(int id, [FromBody] Customer customer)
        {
            var sql = $"UPDATE Customer SET firstName = '{customer.FirstName}', " +
            $"lastName = '{customer.LastName}', cYear = '{customer.Year}' WHERE id='{id}'";
            // var db = new MySqlConnection(connection);
            var db = new SqlConnection(connection);
            db.Open();

            // var command = new MySqlCommand(sql, db);
            var command = new SqlCommand(sql, db);
            var reader = command.ExecuteReader();
            db.Dispose();
        }

        // DELETE api/Customers/5
        [HttpDelete("{id}")]
        public void DeleteCustomer(int id)
        {
            var sql = $"DELETE FROM Customer WHERE id='{id}'";
            // var db = new MySqlConnection(connection);
            var db = new SqlConnection(connection);

            db.Open();

            // var command = new MySqlCommand(sql, db);
            var command = new SqlCommand(sql, db);
            var reader = command.ExecuteReader();
            db.Dispose();
        }
    }
}