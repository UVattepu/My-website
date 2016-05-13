using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Uday
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
         
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString();
        }

        private void CmdLogin_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
                if (txtUserName.Text == "")
                    errorProvider1.SetError(txtUserName, "Please Provide User Name!");
                else if (txtPassword.Text == "")
                    errorProvider1.SetError(txtPassword, "Please Provide Password!");
                else
            try
            {
                SqlConnection cn = new SqlConnection("Data Source=SIMBA;Initial Catalog=Sarah;Persist Security Info=True;User ID=sa;Password=pass");

                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.Open();
                SqlDataReader dr1 = null;
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Failed!");
            }

        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
