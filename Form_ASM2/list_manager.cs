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
    public partial class list_manager : Form
    {
        private AllManager managerPage;
        string sqlconnect = "server = DESKTOP-3MDRUO1\\SQLEXPRESS;database=Patient_Management;uid=sa;pwd=123456";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public list_manager(AllManager managerPage1)
        {
            InitializeComponent();
            managerPage = managerPage1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_edit_delete managerPage = new Add_edit_delete();
            managerPage.Show();
            this.Hide();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void list_manager_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(sqlconnect);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = " SELECT Patient.PatientId, Patient.Name, Patient.Age, Patient.Phonenumber, Patient.Address FROM Patient ";
                dr = cmd.ExecuteReader();
                list.Items.Clear();
                while (dr.Read())
                {
                    string PatientId = dr.GetInt32(0).ToString();
                    string Name = dr.GetString(1);
                    string Age = dr.GetInt32(2).ToString();
                    string Phonenumber = dr.GetInt32(3).ToString();
                    string Address = dr.GetString(4);
                    string[] arr = new string[5];
                    arr[0] = PatientId;
                    arr[1] = Name;
                    arr[2] = Age;
                    arr[3] = Phonenumber;
                    arr[4] = Address;
                    ListViewItem itm = new ListViewItem(arr);
                    list.Items.Add(itm);
                }
                con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            managerPage.Show();
            this.Close();
        }
    }
}
