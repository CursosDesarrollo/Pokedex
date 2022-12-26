using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex2.config
{
    class ConnectDataBase
    {
        private static ConnectDataBase instance = null;
        private readonly string host = "localhost";
        private readonly string port = "3307";
        private readonly string bd = "pokedex";
        private readonly string user = "root";
        private readonly string password = "";
        private readonly string connect;
        public readonly MySqlConnection connectDb;

        private ConnectDataBase() 
        {
            this.connect = "server="
                + this.host
                + ";port="
                + this.port
                + ";uid="
                + this.user
                + ";pwd="
                + this.password
                + ";database="
                + this.bd ;
            this.connectDb = new MySqlConnection(connect);
        }

        public static ConnectDataBase GetObject()
        {
            if (instance is null) instance = new ConnectDataBase();
            return instance;
        }

        public Object ExecComand(string comand)
        {
            Object result;
            try
            {
                MySqlCommand newComand = new MySqlCommand(comand);
                newComand.Connection = this.connectDb;
                this.connectDb.Open();
                result = newComand;
            }
            catch(MySqlException ex) 
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
