using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BD
{
    class App
    {
        public String Database { get; set; }
        public String Server { get; set; }
        public String UserID { get; set; }
        public String Password { get; set; }

        protected MySqlConnection connection = null;

        public MySqlConnection Connection { 
            get
            {
                if ((connection == null))
                {
                    Connect();
                }
                return connection;
            }  
        }

        private void Connect()
        {
            var csb = new MySqlConnectionStringBuilder()
            {
                Server = Server,
                Database = Database,
                UserID = UserID,
                Password = Password,
            };
            connection = new MySqlConnection(csb.ConnectionString);
            connection.Open();
        }

        private static App instance;

        private App() 
        {
            Server = "localhost";
            Database = "test-db";
            UserID = "root";
            Password = "1";
        }

        public static App Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new App();
                }
                return instance;
            }
        }
    }
}
