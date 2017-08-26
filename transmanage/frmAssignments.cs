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
    public partial class frmAssignments : Form
    {
        private void Clear()
        {
            cmbDriver.Text = string.Empty;
            cmbRoute.Text = string.Empty;
            cmbTruck.Text = string.Empty;
            txtDate.Text = DateTime.Today.ToString();
            cmbDriver.Focus();
        }

        private void loadDrivers()
        {
            Dbconnect d = new Dbconnect();
            var c = d.getConnection();
            var select = "SELECT driverID, drivernames FROM drivers WHERE status = 'Active'";
            MySqlCommand cmd = new MySqlCommand(select, c);
            MySqlDataAdapter da = new MySqlDataAdapter(select, c);
            DataSet ds = new DataSet();
            
            cmbDriver.DisplayMember = "drivernames";
            cmbDriver.ValueMember = "driverID";
            da.Fill(ds);
            cmbDriver.DataSource = ds.Tables[0];

            c.Close();
        }

        private void loadTrucks()
        {
            Dbconnect d = new Dbconnect();
            var c = d.getConnection();
            var select = "SELECT truckID, regno FROM trucks WHERE truckStatus = 'Available'";
            MySqlCommand cmd = new MySqlCommand(select, c);
            MySqlDataAdapter da = new MySqlDataAdapter(select, c);
            DataSet ds = new DataSet();
           
            cmbTruck.DisplayMember = "regno";
            cmbTruck.ValueMember = "truckID";
            da.Fill(ds);
            cmbTruck.DataSource = ds.Tables[0];
            c.Close();
        }

        private void loadRoutes()
        {
            Dbconnect d = new Dbconnect();
            var c = d.getConnection();
            var select = "SELECT routeID, routeDescription FROM routes";
            MySqlCommand cmd = new MySqlCommand(select, c);
            MySqlDataAdapter da = new MySqlDataAdapter(select, c);
            DataSet ds = new DataSet();
            cmbRoute.DisplayMember = "routeDescription";
            cmbRoute.ValueMember = "routeID";
            da.Fill(ds);
            cmbRoute.DataSource = ds.Tables[0];
            c.Close();
        }
        private void loadGrid()
        {
            Dbconnect d = new Dbconnect();
            var c = d.getConnection();
            var select = "SELECT drivernames AS Driver,CONCAT(regno,' ',truckDescription) AS Vehicle, assignmentDate AS Date, assignmentStatus AS Status FROM assignments INNER JOIN drivers ON drivernum = driverID INNER JOIN trucks ON truckNum = truckID";
            var dataAdapter = new MySqlDataAdapter(select, c);
            var commandBuilder = new MySqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dgvAssignments.ReadOnly = true;
            dgvAssignments.DataSource = ds.Tables[0];
        }
        public frmAssignments()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(cmbDriver.Text == string.Empty || cmbRoute.Text == string.Empty || cmbTruck.Text == string.Empty)
            {
                MessageBox.Show("Insert all the details", "Error");
            } else
            {
                Dbconnect d = new Dbconnect();
                var c = d.getConnection();
                string sql = "INSERT INTO assignments(driverNum,truckNum,route,assignmentDate,assignmentStatus) VALUES('" + (cmbDriver.SelectedValue) + "','" + (cmbTruck.SelectedValue) + "','" + (cmbRoute.SelectedValue) + "','" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "','Active')";
                string sqlUser = "UPDATE drivers SET status = 'Out' WHERE driverID = '" + cmbDriver.SelectedValue + "'";
                string sqlTruck = "UPDATE trucks SET truckStatus = 'Out' WHERE truckID = '" + cmbTruck.SelectedValue + "'";
                string msg = "You have been assigned " + cmbTruck.Text + " to route " + cmbRoute.Text + " on "+ Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd")+ " have a safe journey.";
                string sqlMesg = "INSERT INTO messages(receiver,messageBody,sender,isRead,mDate) VALUES('" + cmbDriver.SelectedValue + "','" + msg + "','admin',0,'" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "')";
                try
                {
                    MySqlCommand cmdSave = new MySqlCommand(sql, c);
                    MySqlCommand cmdUpdateD = new MySqlCommand(sqlUser, c);
                    MySqlCommand cmdSavemessage = new MySqlCommand(sqlMesg, c);
                    int j = cmdUpdateD.ExecuteNonQuery();
                    
                    MySqlCommand cmdTruck = new MySqlCommand(sqlTruck, c);
                    int k = cmdTruck.ExecuteNonQuery();
                    int i = cmdSave.ExecuteNonQuery();
                    int z = cmdSavemessage.ExecuteNonQuery();
                    if (i >= 1 && j >=1 && k >=1)
                    {
                        MessageBox.Show("Assignment details has been saved");
                        Clear();
                        loadGrid();
                        loadDrivers();
                        loadTrucks();
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

        private void frmAssignments_Load(object sender, EventArgs e)
        {
            loadTrucks();
            loadDrivers();
            loadRoutes();
            loadGrid();
        }
    }
}
