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
    public partial class AllManager : Form
    {
        string sqlconnect = "server = DESKTOP-3MDRUO1\\SQLEXPRESS;database=Patient_Management;uid=sa;pwd=123456";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public AllManager()
        {
            InitializeComponent();
        }

        private void AllManager_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            list_manager listPage = new list_manager(this);
            listPage.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            list_manager listPage = new list_manager(this);
            listPage.Show();
            this.Hide();
        }
    }
}
