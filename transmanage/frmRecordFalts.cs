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
    public partial class frmRecordFalts : Form
    {
        public frmRecordFalts()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            cmbCategory.Text = string.Empty;
            cmbTruck.Text = string.Empty;
            txtxDate.Text = DateTime.Now.ToShortDateString();
            txtDescription.Clear();
        }

        private int SaveDetails()
        {
        
                int i = 0;
                Dbconnect cn = new Dbconnect();
                try
                {
                    using (var c = cn.getConnection())
                    {
                        string save = "INSERT INTO faults(faultcategory,faultDescription,truck,driver,assesedDate,faultStatus) VALUES('" + cmbCategory.Text + "','" + txtDescription.Text + "',1,1,'" + Convert.ToDateTime(txtxDate.Text).ToString("yyyy-MM-dd") + "','Pending')";
                        MySqlCommand cmdSave = new MySqlCommand(save, c);
                        i = cmdSave.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return i;
        }

       private void loadGrid()
        {
            Dbconnect d = new Dbconnect();
            try
            {
                using (var c = d.getConnection())
                {
                    var select = "SELECT drivernames AS Driver,CONCAT(regno,' ',truckDescription) AS Vehicle, assesedDate AS Date, faultStatus AS Status FROM faults INNER JOIN drivers ON driver = driverID INNER JOIN trucks ON truck = truckID";
                    var dataAdapter = new MySqlDataAdapter(select, c);
                    var commandBuilder = new MySqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    dgvFaults.ReadOnly = true;
                    dgvFaults.DataSource = ds.Tables[0];
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadTrucks()
        {
            Dbconnect d = new Dbconnect();

            using (var c = d.getConnection())
            {
                try
                {
                    var select = "SELECT truckID, regno FROM trucks";
                    MySqlCommand cmd = new MySqlCommand(select, c);
                    MySqlDataAdapter da = new MySqlDataAdapter(select, c);
                    DataSet ds = new DataSet();

                    cmbTruck.DisplayMember = "regno";
                    cmbTruck.ValueMember = "truckID";
                    da.Fill(ds);
                    cmbTruck.DataSource = ds.Tables[0];
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
               
        }

        private void loadCategories()
        {
            Dbconnect d = new Dbconnect();

            using (var c = d.getConnection())
            {
                try
                {
                    var select = "SELECT fault FROM faultscategories";
                    MySqlCommand cmd = new MySqlCommand(select, c);
                    MySqlDataAdapter da = new MySqlDataAdapter(select, c);
                    DataSet ds = new DataSet();

                    cmbCategory.DisplayMember = "fault";
                    da.Fill(ds);
                    cmbCategory.DataSource = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }



        private void frmRecordFalts_Load(object sender, EventArgs e)
        {
            loadGrid();
            loadTrucks();
            loadCategories();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(cmbCategory.Text == string.Empty || cmbTruck.Text == string.Empty || txtDescription.Text == string.Empty)
            {
                MessageBox.Show("Please insert all the details");
            } else
            {
                int i = SaveDetails();
                if (i >= 1)
                {
                    MessageBox.Show("Fault has been recorded");
                    Clear();
                    loadGrid();
                }
            }
        }
    }
}
