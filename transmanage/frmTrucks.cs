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
    public partial class frmTrucks : Form
    {
        private void Clear()
        {
            txtDescription.Clear();
            txtRegno.Clear();
            txtWeight.Clear();
            txtRegno.Focus();
        }

        private void loadGrid()
        {
            Dbconnect d = new Dbconnect();
            var c = d.getConnection();
            var select = "SELECT regno AS RegNo,weight AS Weight,truckdescription AS Description,truckstatus AS Status FROM trucks";
            var dataAdapter = new MySqlDataAdapter(select, c);
            var commandBuilder = new MySqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dgvTrucks.ReadOnly = true;
            dgvTrucks.DataSource = ds.Tables[0];
        }
        public frmTrucks()
        {
            InitializeComponent();
        }

        private void frmTrucks_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtRegno.Text == "" || txtWeight.Text == "" || txtDescription.Text == "")
            {
                MessageBox.Show("Please insert all the details", "Error");
            }
            else
            {
                Dbconnect d = new Dbconnect();
                var c = d.getConnection();
                string sql = "INSERT INTO trucks(regno,truckdescription,weight,truckRegDate,truckStatus) VALUES('" + txtRegno.Text + "','" + txtDescription.Text + "','" + txtWeight.Text + "','" + DateTime.Now +"','Available')";
                try
                {
                    MySqlCommand cmdSave = new MySqlCommand(sql, c);
                    int i = cmdSave.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        MessageBox.Show("Truck details has been saved");
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
    }
}
