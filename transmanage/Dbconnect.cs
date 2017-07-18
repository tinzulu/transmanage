using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace transmanage
{
    public class Dbconnect
    {
        public MySqlConnection cn;

        public MySqlConnection getConnection()
        {
            cn = new MySqlConnection("server=127.0.0.1;user=root;password=;database=zimascodb");
            cn.Open();
            return cn;
        }
    }
}
