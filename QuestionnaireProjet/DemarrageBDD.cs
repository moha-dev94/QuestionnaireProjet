using MySql.Data.MySqlClient;
using System;

namespace DemarrageBDD
{
    public class DBConnection
    {
        private static DBConnection _instance = null;
        public MySqlConnection Connection { get; set; }

        public string Server { get; private set; }
        public string DatabaseName { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }

        private DBConnection(string server, string databaseName, string userName, string password)
        {
            Server = server;
            DatabaseName = databaseName;
            UserName = userName;
            Password = password;
        }

        public static DBConnection Instance(string server, string databaseName, string userName, string password)
        {
            if (_instance == null)
                _instance = new DBConnection(server, databaseName, userName, password);
            return _instance;
        }
        public bool IsConnect()
        {
            try
            {
                if (Connection == null)
                {
                    if (string.IsNullOrEmpty(DatabaseName))
                        return false;

                    string connstring = new MySqlConnectionStringBuilder
                    {
                        Server = Server,
                        Database = DatabaseName,
                        UserID = UserName,
                        Password = Password,
                    }.ConnectionString;

                    Connection = new MySqlConnection(connstring);
                    Connection.Open();
                }
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error connecting to the database: " + ex.Message);
                return false;
            }
        }

        public void Close()
        {
            if (Connection != null)
            {
                try
                {
                    Connection.Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error closing the connection: " + ex.Message);
                }
                finally
                {
                    Connection = null;
                }
            }
        }
    }
}