using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace transmanage
{
    public partial class frmRegDriver : Form
    {

        private void Clear()
        {
            txtCell.Clear();
            txtEmail.Clear();
            txtLicenseNo.Clear();
            txtNames.Clear();
            txtNationalID.Clear();
            txtxAddress.Clear();
            txtNames.Focus();
        }

        private void loadGrid()
        {
            Dbconnect d = new Dbconnect();
            var c = d.getConnection();
            var select = "SELECT drivernames AS DriverNames,nationalID AS NationalID,licenseNo AS LicenseNo,email AS Email,Status AS Status FROM drivers";
            var dataAdapter = new MySqlDataAdapter(select, c);
            var commandBuilder = new MySqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dgvDrivers.ReadOnly = true;
            dgvDrivers.DataSource = ds.Tables[0];
        }

        public frmRegDriver()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (txtNames.Text == "" || txtCell.Text == "" || txtEmail.Text == "" || txtLicenseNo.Text == "" || txtNationalID.Text == "" || txtxAddress.Text == "")
            {
                MessageBox.Show("Please insert all the details");
            }
            else
            {
                Dbconnect d = new Dbconnect();
                var c = d.getConnection();
                //Security secure = new Security();
                string pass = "password"; // secure.HashPass("password");
                string sql = "INSERT INTO drivers(drivernames,nationalID,licenseNo,contactCell,email,physicalAddress,password,status,regDate) VALUES('" + txtNames.Text + "','" + txtNationalID.Text + "', '" + txtLicenseNo.Text + "','" + txtCell.Text + "','" + txtEmail.Text + "','" + txtxAddress.Text + "','" + pass + "','Active','" + Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd") + "')";
                try
                {
                    MySqlCommand cmdSave = new MySqlCommand(sql, c);
                    int i = cmdSave.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        MessageBox.Show("Driver details has been saved");
                        Clear();
                        loadGrid();
                    }
                    else
                    {
                        MessageBox.Show("An error occured");
                    }
                }
                catch (Exception execption)
                {
                    MessageBox.Show(execption.Message);
                }
            } 
        }

        private void frmRegDriver_Load(object sender, EventArgs e)
        {
            loadGrid();
        }
    }
}
