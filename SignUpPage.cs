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
    public partial class SignUpPage : Form
    {
        textPadding tp = new textPadding();
        public SignUpPage()
        {
            InitializeComponent();
            this.Closed += (s, ev) => Application.Exit();
        }
        SqlConnection conn = new SqlConnection("Data Source=GOUJO\\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        private void btnBack_Click(object sender, EventArgs e)
        {
            LoginPage login = new LoginPage();
            login.Show();
            Visible = false;
        }

        private void txtboxFirstname_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxFirstname, new Padding(15, 10, 10, 10));
        }

        private void txtBoxLastname_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtBoxLastname, new Padding(15, 10, 10, 10));
        }

        private void txboxUsername_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txboxUsername, new Padding(15, 10, 10, 10));
        }

        private void txtboxPassword_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxPassword, new Padding(15, 10, 10, 10));
        }

        private void txtboxConfirmPass_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxConfirmPass, new Padding(15, 10, 10, 10));
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (txboxUsername.Text == "" || txtboxPassword.Text == "" || txtboxConfirmPass.Text == "" || txtboxFirstname.Text == "" || txtBoxLastname.Text == "")
            {
                MessageBox.Show("Fields are empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            else if (txtboxPassword.Text == txtboxConfirmPass.Text)
            {
                string FirstName = txtboxFirstname.Text;
                string Password = AesOperation.EncryptString(txtboxPassword.Text);
                string Username = txboxUsername.Text;
                string LastName = txtBoxLastname.Text;

                conn.Open();
                string query = "SELECT COUNT(*) FROM tblUsers WHERE Username='" + Username + "'";
                cmd = new SqlCommand(query, conn);
                int count = (int)cmd.ExecuteScalar();
                conn.Close();

                if (count > 0)
                {
                    MessageBox.Show("Username already exists in our system", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtboxPassword.Text != txtboxConfirmPass.Text)
                {
                    MessageBox.Show("Password do not match, Please re-enter", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtboxPassword.Text = "";
                    txtboxConfirmPass.Text = "";
                    txtboxPassword.Focus();
                    return;
                }

                conn.Open();
                string register = "INSERT INTO tblUsers(Username, LastName, FirstName, Password) VALUES ('" + Username + "','" +
                    LastName.Substring(0, 1).ToUpper() + LastName.Substring(1) + "','" +
                    FirstName.Substring(0, 1).ToUpper() + FirstName.Substring(1) + "', '" + Password + "')";
                cmd = new SqlCommand(register, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Account successfully created", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoginPage form = new LoginPage();
                form.Show();
                
            }

            else
            {
                MessageBox.Show("Password do not match, Please re-enter", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtboxPassword.Text = "";
                txtboxConfirmPass.Text = "";
                txtboxPassword.Focus();
            }
        }

        private void imgHomePage_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
