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
using System.Runtime.InteropServices;

namespace LibSysFINALFINAL
{
    public partial class LoginPage : Form
    {
        textPadding tp = new textPadding();
        public LoginPage()
        {
            InitializeComponent();
            this.Closed += (s, ev) => Application.Exit();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=GOUJO\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True");

        private void txboxLogin_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txboxLogin, new Padding(15, 10, 10, 10));
        }

        private void txtboxPassword_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxPassword, new Padding(15, 10, 10, 10));
        }

        private void linklblSignup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUpPage signup = new SignUpPage();
            signup.Show();
            Visible = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=GOUJO\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True";
            string query = "SELECT * FROM tblUsers where username = @username";

            

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", txboxLogin.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string storedEncryptedPassword = dt.Rows[0]["password"].ToString();
                    string enteredEncryptedPassword = AesOperation.EncryptString(txtboxPassword.Text);
                    
                    //fetch username and firstname to display in label
                    DataRow firstname = dt.Rows[0];
                    DataRow username = dt.Rows[0];
                    LoggedInUser.Username = username["username"].ToString();
                    LoggedInUser.Firstname = firstname["firstname"].ToString();
                    
                    if (storedEncryptedPassword == enteredEncryptedPassword)
                    {
                        string role = dt.Rows[0]["username"].ToString();
                        if (role == "admin") 
                        {
                            AdminForm dashboard = new AdminForm();
                            dashboard.Show();
                            Visible = false;
                        }
                        else
                        {
                            UserForm userForm = new UserForm();
                            userForm.Show();
                            Visible = false;
                           
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txboxLogin.Text = "";
                        txtboxPassword.Text = "";
                        txboxLogin.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txboxLogin.Text = "";
                    txtboxPassword.Text = "";
                    txboxLogin.Focus();
                }
                
            }
        }
    }
}
