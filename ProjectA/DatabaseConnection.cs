using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectA
{
    class DatabaseConnection
    {
        private SqlConnection connection;
        public string conStr;
        private static DatabaseConnection Instance;
        private DatabaseConnection()
        {
            conStr = "Data Source=MALIK\\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True";
        }

        public static DatabaseConnection getInstance()
        {
            if (Instance == null)
            {
                Instance = new DatabaseConnection();
            }
            return Instance;
        }

        public SqlConnection getConnection()
        {
            connection = new SqlConnection(conStr);
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }

        public int executeQuery(string query) ///quey for insert update delete
        {
            connection = getConnection();
            SqlCommand cmd = new SqlCommand(query, connection);

            //  int rows = int.Parse(cmd.ExecuteScalar().ToString());
            int rows = cmd.ExecuteNonQuery();
            return rows;
        }
        public int getRowsCount(string query)
        {
            connection = getConnection();
            SqlCommand cmd = new SqlCommand(query, connection);

            int rows = int.Parse(cmd.ExecuteScalar().ToString());
            //  int rows = cmd.ExecuteNonQuery();
            return rows;
        }

        public SqlDataAdapter getAllData(string query)
        {
            SqlDataAdapter data = new SqlDataAdapter(query, getConnection());
            return data;
        }

        public SqlDataReader readData(string query)
        {
            connection = getConnection();
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader read = cmd.ExecuteReader();
            return read;
        }

        public void closeConnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}
