using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Data
{
    class Database
    {
        public MySqlConnection cn;
        public MySqlConnection connect()
        {
             cn = new MySqlConnection("server=127.0.0.1;user=root;password=;database=zimascodb");
             return cn;
        }

        public void disconnect()
        {
            cn.Close();
        }

        public bool initialise()
        {
            cn.Open();
            return true;
        }
    }
}
