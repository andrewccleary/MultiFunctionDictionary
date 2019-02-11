using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;

namespace MultifunctionalDictionary.Helper
{
    class DatabaseHelper
    {
        private String server;
        private String port;
        private String user;
        private String password;
        private String database;

        private NpgsqlConnection connection;

        public DatabaseHelper(String server, String port, String user, String password, String database)
        {
            this.server = server;
            this.port = port;
            this.user = user;
            this.password = password;
            this.database = database;
        }

        /// <summary>
        /// Acquire Connection to PostgreSQL Database
        /// </summary>
        public void AcquireConnection()
        {
            string connectionString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", server, port, user, password, database);
            connection = new NpgsqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public NpgsqlConnection GetConnection()
        {
            return this.connection;
        }

    }
}
