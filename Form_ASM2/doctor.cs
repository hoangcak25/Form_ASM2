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
    public partial class doctor : Form
    {
        private AllManager managerPage;
        string sqlconnect = "server = DESKTOP-3MDRUO1\\SQLEXPRESS;database=Patient_Management;uid=sa;pwd=123456";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public doctor()
        {
            InitializeComponent();
        }

        private void doctor_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(sqlconnect);
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = " SELECT Prescription.PrescriptionId , Prescription.NameOfMedicine , Prescription.Type FROM Prescription ";
            dr = cmd.ExecuteReader();
            list.Items.Clear();
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
                list.Items.Add(itm);
            }
            con.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            manager_of_doctor managerdoctorPage = new manager_of_doctor();
            managerdoctorPage.Show();
            this.Hide();
        }
    }
}
