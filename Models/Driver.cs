using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Driver
    {
        public int DriverID { get; set; }
        public string names { get; set; }
        public string surname { get; set; }
        public string nationalID { get; set; }
        public string licenseNo { get; set; }
        public string contactCell { get; set; }
        public string email { get; set; }
        public string physcialAddress { get; set; }
        public DateTime regDate { get; set; }
        public string status { get; set; }
        public string password { get; set; }
    }
}
