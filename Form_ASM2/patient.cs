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
    public partial class patient : Form
    {
        string sqlconnect = "server = DESKTOP-3MDRUO1\\SQLEXPRESS;database=Patient_Management;uid=sa;pwd=123456";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public patient()
        {
            InitializeComponent();
        }
        private void patient_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(sqlconnect);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            string searchInput = txtSearch.Text;
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM patient WHERE Name LIKE '%" + searchInput + "%'";
            dr = cmd.ExecuteReader();
            listinformation.Items.Clear();
            while (dr.Read())
            {
                string patientName = dr.GetString(1);
                string patientAge = dr.GetInt32(2).ToString();
                string phoneNumber = dr.GetInt32(3).ToString();
                string address = dr.GetString(4);
                string[] arr = new string[4];
                arr[0] = patientName;
                arr[1] = patientAge;
                arr[2] = phoneNumber;
                arr[3] = address;
                ListViewItem itm = new ListViewItem(arr);
                listinformation.Items.Add(itm);
            }
            dr.Close();
            con.Close();
        }

        private void listinformation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
