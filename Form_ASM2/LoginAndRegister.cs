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
    public partial class RegistrationAndLoginForm : Form
    {
        string sqlconnect = "server = DESKTOP-3MDRUO1\\SQLEXPRESS;database=Patient_Management;uid=sa;pwd=123456";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public RegistrationAndLoginForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String username = txtUsername.Text;
            String password = txtPassword.Text;
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "select * from Users where username = '" + username + "' and Password = '" + password + "'";
            dr = cmd.ExecuteReader();
              
                if (dr.Read())
                {

                        string role = dr.GetString(3);
                        if (role == "admin")
                        {
                            MessageBox.Show("Login successful as admin!", "Success", MessageBoxButtons.OK);
                            clearTxt();
                            AllManager adminPage = new AllManager();
                            adminPage.Show();
                            this.Hide();
                        }
                        else if (role == "doctor")
                        {
                            MessageBox.Show("Login successful as doctor!", "Success", MessageBoxButtons.OK);
                            clearTxt();
                            doctor doctorPage = new doctor();
                            doctorPage.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Login successful as Patient!", "Success", MessageBoxButtons.OK);
                            clearTxt();
                            patient patientPage = new patient();
                            patientPage.Show();
                            this.Hide();
                        }
                    
                }
                else
                {
                    clearTxt();
                    MessageBox.Show("Given username or password is incorrect!", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
            con.Close();
        }

        private void RegistrationAndLoginForm_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(sqlconnect);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        void clearTxt()
        {
            txtPassword.Text = txtUsername.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO Users(username, password) VALUES(@username,@password)";
            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = txtUsername.Text;
            cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = txtPassword.Text;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Register successfully");
            clearTxt();
            con.Close();
        }
    }

}
