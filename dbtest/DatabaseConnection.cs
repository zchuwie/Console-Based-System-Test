using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace dbtest {
    class DatabaseConnection {
        static string server = "127.0.0.1";
        static string database = "finals";
        static string username = "root";
        static string password = $"{Environment.GetEnvironmentVariable("db_password")}";
        private static string connectionString = $"Server={server};Database={database};User ID={username};Password={password};Port=3306;";

        public static MySqlConnection GetConnection() {
            return new MySqlConnection(connectionString);
        }


    }
}
