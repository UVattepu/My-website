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
namespace Uday
{
    public partial class Form1 : Form
    {
        DataSet ds = new DataSet();
        SqlConnection cs = new SqlConnection("Data source = SIMBA;initial catalog=Sarah;Persist Security Info=True;User ID=sa;Password=pass");
        
        SqlDataAdapter da = new SqlDataAdapter();

        BindingSource bs = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (textBox2.Text == "")
                errorProvider1.SetError(textBox2, "Please Provide the First Name!");
            else if (textBox3.Text == "")
                errorProvider1.SetError(textBox3, "Please Provide Book Last Name!");
            else if (textBox4.Text == "")
                errorProvider1.SetError(textBox4, "Please Provide Course!");
            else if (textBox5.Text == "")
                errorProvider1.SetError(textBox5, "Please Provide Department!");
            else if (textBox6.Text == "")
                errorProvider1.SetError(textBox6, "Please Provide Address!");
               else
            try
            {
                da.InsertCommand = new SqlCommand("insert into Training values(@FirstName,@LastName,@Course,@Department,@Address)", cs);
                da.InsertCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = textBox2.Text;
                da.InsertCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = textBox3.Text;
                da.InsertCommand.Parameters.Add("@Course", SqlDbType.VarChar).Value = textBox4.Text;
                da.InsertCommand.Parameters.Add("@Department", SqlDbType.VarChar).Value = textBox5.Text;
                da.InsertCommand.Parameters.Add("@Address", SqlDbType.VarChar).Value = textBox5.Text;
                
                cs.Open();
                da.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Successfully Saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cs.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Occur");                 
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            da.SelectCommand = new SqlCommand("select * from Training",cs);
            ds.Clear();
            da.Fill(ds);

            dgv.DataSource = ds.Tables[0];

            bs.DataSource = ds.Tables[0];

            textBox2.DataBindings.Clear();
            textBox2.DataBindings.Add(new Binding("Text",bs,"FirstName"));
            textBox3.DataBindings.Clear();
            textBox3.DataBindings.Add(new Binding("Text",bs, "LastName"));
            textBox4.DataBindings.Clear();
            textBox4.DataBindings.Add(new Binding("Text", bs, "Course"));
            textBox5.DataBindings.Clear();
            textBox5.DataBindings.Add(new Binding("Text", bs, "Department"));
            textBox6.DataBindings.Clear();
            textBox6.DataBindings.Add(new Binding("Text", bs, "Address"));
            ViewRecords();

        }

        private void First_Click(object sender, EventArgs e)
        {
            bs.MoveFirst();
            dgvUpdate();
            ViewRecords();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            bs.MoveNext();
            dgvUpdate();
            ViewRecords();
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
            dgvUpdate();
            ViewRecords();
        }

        private void Last_Click(object sender, EventArgs e)
        {
            bs.MoveLast();
            dgvUpdate();
            ViewRecords();
        }
        private void dgvUpdate()
        {
            dgv.ClearSelection();
            dgv.Rows[bs.Position].Selected = true;
        }
        private void ViewRecords()
        {
            label3.Text = "  ViewRecords  " + bs.Position + " Of " + (bs.Count  -1); 
        
        }
        private void clear()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                da.UpdateCommand = new SqlCommand("Update Training set FirstName =@FirstName, LastName =@LastName where StudentID=@StudentID", cs);
                da.UpdateCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = textBox2.Text;
                da.UpdateCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = textBox3.Text;
                da.UpdateCommand.Parameters.Add("@Course", SqlDbType.VarChar).Value = textBox4.Text;
                da.UpdateCommand.Parameters.Add("@Department", SqlDbType.VarChar).Value = textBox5.Text;
                da.UpdateCommand.Parameters.Add("@Address", SqlDbType.VarChar).Value = textBox6.Text;
                da.UpdateCommand.Parameters.Add("@StudentID", SqlDbType.Int).Value = ds.Tables[0].Rows[bs.Position][0];

                cs.Open();
                da.UpdateCommand.ExecuteNonQuery();
                MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cs.Close();
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr;
                dr = MessageBox.Show("Are U Sure?There is No Undo Once Records is Deleted ", "Confirmation Deletion", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {

                    da.DeleteCommand = new SqlCommand("Delete from Training where StudentID=@StudentID", cs);
                da.DeleteCommand.Parameters.Add("@StudentID", SqlDbType.Int).Value = ds.Tables[0].Rows[bs.Position][0];

                cs.Open();
                da.DeleteCommand.ExecuteNonQuery();
               
                cs.Close();
                ds.Clear();
                da.Fill(ds);
                MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
                  }
                else
                {
                    MessageBox.Show("Deletion Canceled");
                }
           }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            btnSave.Enabled = true;
            //btnDelete.Enabled = false;
            //btnUpdate.Enabled = false;
        }
        bool lr = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (lr)
            {

                lblmoving.Location = new Point(lblmoving.Location.X + 5, lblmoving.Location.Y);

            }
            else
            {
                lblmoving.Location = new Point(lblmoving.Location.X - 5, lblmoving.Location.Y);
            }
            if (lblmoving.Location.X + lblmoving.Width >= this.Width)
            {
                lr = false;
            }
            if(lblmoving.Location.X <= 0)
            {
                lr = true;
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {

            OpenFileDialog OpenFile = new OpenFileDialog();
            try
            {
                OpenFile.FileName = "";
                OpenFile.Title = "Photo:";
                OpenFile.Filter = "Image files: (*.jpg)|*.jpg|(*.jpeg)|*.jpeg|(*.png)|*.png|(*.Gif)|*.Gif|(*.bmp)|*.bmp| All Files (*.*)|*.*";
                DialogResult res = OpenFile.ShowDialog();
                if (res == DialogResult.OK)
                {
                    this.PB.Image = System.Drawing.Image.FromFile(OpenFile.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error!!!");
            }
            
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)

            {
                DataGridViewRow dgvr = this.dgv.Rows[e.RowIndex];

                textBox2.Text = dgvr.Cells["FirstName"].Value.ToString();
                textBox3.Text = dgvr.Cells["LastName"].Value.ToString();
                textBox4.Text = dgvr.Cells["Course"].Value.ToString();
                textBox5.Text = dgvr.Cells["Department"].Value.ToString();
                textBox6.Text = dgvr.Cells["Address"].Value.ToString();
            }
        }

        private void BRemove_Click(object sender, EventArgs e)
        {
            //this.PB.Image = System.Drawing.Image.FromFile(Application.StartupPath.ToString() + "\\Image\\zawadi.jpg");
        }

        private void lblmoving_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label5.Text = DateTime.Now.ToString();
        }

    }
}
