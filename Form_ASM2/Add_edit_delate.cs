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

namespace Form_ASM2
{
    public partial class Add_edit_delete : Form
    {
        string sqlconnect = "server = DESKTOP-3MDRUO1\\SQLEXPRESS;database=Patient_Management;uid=sa;pwd=123456";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        string PatientId;
        public Add_edit_delete()
        {
            InitializeComponent();
        }
        public bool KTThongTin()
        {
            if (txtid.Text == "")
            {
                MessageBox.Show("Please enter id", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtid.Focus();
                return false;
            }
            if (txtname.Text == "")
            {
                MessageBox.Show("Please enter your name", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtname.Focus();
                return false;
            }
            if (txtage.Text == "")
            {
                MessageBox.Show("Please enter age", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtage.Focus();
                return false;
            }
            if (txtphone.Text == "")
            {
                MessageBox.Show("Please enter the phone number", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtphone.Focus();
                return false;
            }
            if (txtaddress.Text == "")
            {
                MessageBox.Show("Please enter your address", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtaddress.Focus();
                return false;
            }
            return true;
        }
        public bool KTThongTin1()
        {
            if (txtid1.Text == "")
            {
                MessageBox.Show("Please enter id", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtid1.Focus();
                return false;
            }
            if (txtname1.Text == "")
            {
                MessageBox.Show("Please enter your name", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtname1.Focus();
                return false;
            }
            if (txtage1.Text == "")
            {
                MessageBox.Show("Please enter age", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtage1.Focus();
                return false;
            }
            if (txtphone1.Text == "")
            {
                MessageBox.Show("Please enter the phone number", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtphone1.Focus();
                return false;
            }
            if (txtaddress1.Text == "")
            {
                MessageBox.Show("Please enter your address", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtaddress1.Focus();
                return false;
            }
            return true;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listdelete_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void updatelist()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = " SELECT Patient.PatientId, Patient.Name, Patient.Address FROM Patient ";
            dr = cmd.ExecuteReader();
            listdelete.Items.Clear();
            while (dr.Read())
            {
                string PatientId = dr.GetInt32(0).ToString();
                string Name = dr.GetString(1);
                string Address = dr.GetString(2);
                string[] arr = new string[3];
                arr[0] = PatientId;
                arr[1] = Name;
                arr[2] = Address;
                ListViewItem itm = new ListViewItem(arr);
                listdelete.Items.Add(itm);
            }
            con.Close();
        }
        private void Add_edit_delete_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(sqlconnect);
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = " SELECT Patient.PatientId, Patient.Name, Patient.Address FROM Patient ";
            dr = cmd.ExecuteReader();
            listdelete.Items.Clear();
            while (dr.Read())
            {
                string PatientId = dr.GetInt32(0).ToString();
                string Name = dr.GetString(1);
                string Address = dr.GetString(2);
                string[] arr = new string[3];
                arr[0] = PatientId;
                arr[1] = Name;
                arr[2] = Address;
                ListViewItem itm = new ListViewItem(arr);
                listdelete.Items.Add(itm);
            }
            con.Close();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (KTThongTin())
            {
                con.Open();
                
                    cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO Patient(PatientId, Name, Age, Phonenumber, Address) VALUES(@PatientId,@Name,@Age,@Phonenumber,@Address)";
                    cmd.Parameters.Add("@PatientId", SqlDbType.Int).Value = Int32.Parse(txtid.Text);
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = txtname.Text;
                    cmd.Parameters.Add("@Age", SqlDbType.Int).Value = Int32.Parse(txtage.Text);
                    cmd.Parameters.Add("@Phonenumber", SqlDbType.Int).Value = Int32.Parse(txtphone.Text);
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = txtaddress.Text;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" successfully Added");
                    clearTxt();
                con.Close();
                updatelist();
            }
        }
        void clearTxt()
        {
            txtid.Text = txtname.Text = txtage.Text = txtphone.Text = txtaddress.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (KTThongTin1())
            {
                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "UPDATE Patient SET Name = @Name, Age = @Age, Phonenumber = @Phonenumber, Address = @Address WHERE PatientId = " + PatientId;
                    cmd.Parameters.Add("@PatientId", SqlDbType.Int).Value = Int32.Parse(txtid1.Text);
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = txtname1.Text;
                    cmd.Parameters.Add("@Age", SqlDbType.Int).Value = Int32.Parse(txtage1.Text);
                    cmd.Parameters.Add("@Phonenumber", SqlDbType.Int).Value = Int32.Parse(txtphone1.Text);
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = txtaddress1.Text;

                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" successfully edited!", "Added");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Insert Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
                updatelist();
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            con.Open();
            PatientId = txtid1.Text;
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Patient WHERE PatientId = " + PatientId;
            
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtid1.Text = PatientId;
                txtname1.Text = dr.GetString(1);
                txtage1.Text = dr.GetInt32(2).ToString();
                txtphone1.Text = dr.GetInt32(3).ToString();
                txtaddress1.Text = dr.GetString(4);
            }
            dr.Close();
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listdelete.SelectedItems.Count > 0)
            {
                con.Open();
                foreach (ListViewItem item in listdelete.SelectedItems)
                {
                    string PatientId = item.Text;
                    cmd = con.CreateCommand();
                    cmd.CommandText = "DELETE FROM Patient WHERE PatientId = " + PatientId;
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                MessageBox.Show(listdelete.SelectedItems.Count + " record(s) deleted", "Deleted");
                updatelist();
            }
            else
            {
                MessageBox.Show("Please select one lesson to be edited", "Select Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            updatelist();
        }
    }
}
