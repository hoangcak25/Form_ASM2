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
    public partial class manager_of_doctor : Form
    {
        string sqlconnect = "server = DESKTOP-3MDRUO1\\SQLEXPRESS;database=Patient_Management;uid=sa;pwd=123456";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        string PrescriptionId;
        public manager_of_doctor()
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
            if (txttype.Text == "")
            {
                MessageBox.Show("Please enter age", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txttype.Focus();
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
            if (txttype1.Text == "")
            {
                MessageBox.Show("Please enter age", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txttype1.Focus();
                return false;
            }
            return true;
        }
        public void updatelist()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = " SELECT Prescription.PrescriptionId, Prescription.NameOfMedicine, Prescription.Type FROM Prescription ";
            dr = cmd.ExecuteReader();
            listdelete.Items.Clear();
            while (dr.Read())
            {
                string PrescriptionId = dr.GetInt32(0).ToString();
                string NameOfMedicine = dr.GetString(1);
                string Type = dr.GetString(2);
                string[] arr = new string[3];
                arr[0] = PrescriptionId;
                arr[1] = NameOfMedicine;
                arr[2] = Type;
                ListViewItem itm = new ListViewItem(arr);
                listdelete.Items.Add(itm);
            }
            con.Close();
        }
        void clearTxt()
        {
            txtid.Text = txtname.Text = txttype.Text = "";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            search_of_doctor searchdoctorPage = new search_of_doctor();
            searchdoctorPage.Show();
            this.Hide();
        }

        private void manager_of_doctor_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(sqlconnect);
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = " SELECT Prescription.PrescriptionId , Prescription.NameOfMedicine , Prescription.Type  FROM Prescription ";
            dr = cmd.ExecuteReader();
            listdelete.Items.Clear();
            while (dr.Read())
            {
                string PrescriptionId = dr.GetInt32(0).ToString();
                string NameOfMedicine = dr.GetString(1);
                string Type = dr.GetString(2);
                string[] arr = new string[3];
                arr[0] = PrescriptionId;
                arr[1] = NameOfMedicine;
                arr[2] = Type;
                ListViewItem itm = new ListViewItem(arr);
                listdelete.Items.Add(itm);
            }
            con.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (KTThongTin())
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO Prescription(PrescriptionId, NameOfMedicine, Type) VALUES(@PrescriptionId,@NameOfMedicine,@Type)";
                cmd.Parameters.Add("@PrescriptionId", SqlDbType.Int).Value = Int32.Parse(txtid.Text);
                cmd.Parameters.Add("@NameOfMedicine", SqlDbType.VarChar).Value = txtname.Text;
                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = txttype.Text;
                cmd.ExecuteNonQuery();
                MessageBox.Show(" successfully Added");
                clearTxt();
                con.Close();
                updatelist();
            }
        }

        private void search_Click(object sender, EventArgs e)
        {
            con.Open();
            PrescriptionId = txtid1.Text;
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Prescription WHERE PrescriptionId = " + PrescriptionId;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtid1.Text = PrescriptionId;
                txtname1.Text = dr.GetString(1);
                txttype1.Text = dr.GetString(2);
            }
            dr.Close();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (KTThongTin1())
            {
                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "UPDATE Prescription SET Name = @Name, NameOfMedicine = @NameOfMedicine, Type = @Type WHERE PrescriptionId = " + PrescriptionId;
                    cmd.Parameters.Add("@PrescriptionId", SqlDbType.Int).Value = Int32.Parse(txtid.Text);
                    cmd.Parameters.Add("@NameOfMedicine", SqlDbType.VarChar).Value = txtname.Text;
                    cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = txttype.Text;

                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" successfully edited!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Insert Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
                updatelist();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listdelete.SelectedItems.Count > 0)
            {
                con.Open();
                foreach (ListViewItem item in listdelete.SelectedItems)
                {
                    string PrescriptionId = item.Text;
                    cmd = con.CreateCommand();
                    cmd.CommandText = "DELETE FROM Prescription WHERE PrescriptionId = " + PrescriptionId;
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
