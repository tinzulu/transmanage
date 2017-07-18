using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Models;

namespace Data
{
    public class DADriver
    {
        public int InsertDriver(Driver driver)
        {
            Database d = new Database();
            var cn = d.cn;
            string sql = "INSERT INTO drivers(drivernames,nationalID,licenseNo,contactcell,email,physicalAddress,password,status,reddate) VALUES('" + driver.names + "','" + driver.nationalID + "','" + driver.licenseNo + "','" + driver.contactCell + "', '" + driver.email + "','" + driver.physcialAddress + "','" + driver.password + "','" + driver.status + "','" + driver.regDate + "')";
            try
            {
                cn.Open();
                var command = new MySqlCommand(sql, cn);
                return 1;
            }
            catch (Exception)
            {
                throw ;
            }
            
        }
    }
}
