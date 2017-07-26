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
    public partial class frmFaultsCategories : Form
    {
        public frmFaultsCategories()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            txtCategory.Clear();
            txtCategory.Focus();
        }

        private int saveData(string name)
        {
            int i = 0;
            Dbconnect cn = new Dbconnect();
            try
            {
                using (var c = cn.getConnection())
                {
                    string save = "INSERT INTO faultscategories VALUES('" + name + "')";
                    MySqlCommand cmdSave = new MySqlCommand(save, c);
                     i = cmdSave.ExecuteNonQuery();
                }
            } catch(Exception message)
            {
                throw message;
            }
            return i;
        }

        private void loadgrid()
        {
            Dbconnect d = new Dbconnect();
            using (var c = d.getConnection())
            {
                var select = "SELECT fault FROM faultscategories";
                var dataAdapter = new MySqlDataAdapter(select, c);
                var commandBuilder = new MySqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                dgvCategory.ReadOnly = true;
                dgvCategory.DataSource = ds.Tables[0];
            }
               
        }
        private void frmFaultsCategories_Load(object sender, EventArgs e)
        {
            loadgrid();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtCategory.Text == "")
            {
                MessageBox.Show("Please insert category name");
            }
            else
            {
                int j = saveData(txtCategory.Text);
                if (j <= 1)
                {
                    MessageBox.Show("Category has been successfully saved.");
                    Clear();
                    loadgrid();
                }
            }
        }
    }
}
