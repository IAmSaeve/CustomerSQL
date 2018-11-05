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
    public class OrderController : ControllerBase
    {
        // MySQL connection string.
        // private const string connection = "server=192.168.122.105; uid=root; pwd=password; database=CustomerDB";

        // MSSQL connection string.
        private const string connection = "Server=tcp:savecustomerdb.database.windows.net,1433; Initial Catalog=CustomerDB;" +
        "Persist Security Info=False; User ID=Sebastian; Password=!mrjo4gqD$xoYy; MultipleActiveResultSets=False;" +
        "Encrypt=True; TrustServerCertificate=False; Connection Timeout=30;";

        // GET: api/Order
        [HttpGet]
        public List<Order> Get()
        {
            var result = new List<Order>();
            var sql = "SELECT * FROM Orders";
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
                    result.Add(new Order(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2)));
                }
            }
            db.Dispose();
            return result;
        }

        // GET: api/Order/1
        [HttpGet("{id}")]
        public Order Get(int id)
        {
            Order result = null;
            var sql = $"SELECT * FROM Orders WHERE Orderid = '{id}'";
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
                    result = new Order(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2));
                }
            }
            db.Dispose();
            return result;
        }

        // GET: api/Order/Customer/1
        [HttpGet("/api/Order/Customer/{id}")]
        public List<OrderForCustomer> GetCustomersOrders([FromRoute]int id)
        {
            var result = new List<OrderForCustomer>();
            var sql = "SELECT Kundeid, FirstName, LastName, Orderid " +
                        "FROM Orders " +
                        "RIGHT JOIN Customer C on Orders.Kundeid = C.Id " + 
                        $"WHERE C.Id = '{id}'";
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
                    result.Add(new OrderForCustomer(reader.GetInt32(0), reader.GetInt32(3), reader.GetString(1) + " " + reader.GetString(2)));
                }
            }
            db.Dispose();
            return result;
        }

        // POST api/Order
        [HttpPost]
        public void InsertOrder(Order Order)
        {
            var sql = "INSERT INTO Orders (Kundeid, Dato)" +
            $"VALUES ('{Order.Kundeid}', '{Order.Dato}')";
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