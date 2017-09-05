using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace transmanage
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if(txtUsername.Text != "" || txtPassword.Text != "")
            {
                if(txtUsername.Text =="admin" && txtPassword.Text == "admin")
                {
                    this.Hide();
                    frmMainform m = new frmMainform();
                    m.Show();
                } else
                {
                    MessageBox.Show("Incorrect details. retry");
                }
                
            }
            else{
                MessageBox.Show("Please enter login details");
            }
           

            
        }
    }
}
