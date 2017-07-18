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
            var select = "SELECT driverID, drivernames FROM drivers";
            MySqlCommand cmd = new MySqlCommand(select, c);
            MySqlDataAdapter da = new MySqlDataAdapter(select, c);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmbDriver.DisplayMember = "drivernames";
            cmbDriver.ValueMember = "driverID";
            cmbDriver.DataSource = ds.Tables[0];
            c.Close();
        }

        private void loadTrucks()
        {
            Dbconnect d = new Dbconnect();
            var c = d.getConnection();
            var select = "SELECT truckID, regno FROM trucks";
            MySqlCommand cmd = new MySqlCommand(select, c);
            MySqlDataAdapter da = new MySqlDataAdapter(select, c);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmbTruck.DisplayMember = "regno";
            cmbTruck.ValueMember = "truckID";
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
            da.Fill(ds);
            cmbRoute.DisplayMember = "routeDescription";
            cmbRoute.ValueMember = "routeID";
            cmbRoute.DataSource = ds.Tables[0];
            c.Close();
        }
        private void loadGrid()
        {
            Dbconnect d = new Dbconnect();
            var c = d.getConnection();
            var select = "SELECT drivernames AS Driver, trucknum AS Truck, assignmentDate AS Date, assignmentStatus AS Status FROM assignments INNER JOIN drivers ON drivernum = driverID";
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
                string sql = "INSERT INTO assignments(driverNum,truckNum,route,assignmentDate,assignmentStatus) VALUES('" + (cmbDriver.Text) + "','" + (cmbTruck.Text) + "','" + (cmbRoute.Text) + "','" + txtDate.Text + "','Active')";
                try
                {
                    MySqlCommand cmdSave = new MySqlCommand(sql, c);
                    int i = cmdSave.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        MessageBox.Show("Assignment details has been saved");
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

        private void frmAssignments_Load(object sender, EventArgs e)
        {
            loadTrucks();
            loadDrivers();
            loadRoutes();
            loadGrid();
        }
    }
}
