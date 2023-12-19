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

namespace LibSysFINALFINAL
{
    public partial class AdminAddUser : Form
    {
        public AdminAddUser()
        {
            InitializeComponent();
        }
        textPadding tp = new textPadding();
        SqlConnection conn = new SqlConnection("Data Source=GOUJO\\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtboxFirstname_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxFirstname, new Padding(15, 10, 10, 10));
        }

        private void txtboxLastname_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxLastname, new Padding(15, 10, 10, 10));
        }

        private void txtboxUsername_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxUsername, new Padding(15, 10, 10, 10));
        }

        private void txtboxPassword_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxPassword, new Padding(15, 10, 10, 10));
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            AdminUsersForm form = Application.OpenForms.OfType<AdminUsersForm>().FirstOrDefault();
            if (txtboxUsername.Text == "" || txtboxPassword.Text == "" || txtboxFirstname.Text == "" || txtboxLastname.Text == "")
            {
                MessageBox.Show("Fields are empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (!txtboxPassword.Text.Any(c => !char.IsLetterOrDigit(c)))
            {
                MessageBox.Show("Password must contain one or more special characters!", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtboxPassword.Text = "";
                txtboxPassword.Focus();
                return;
            }

            else if (txtboxPassword.Text.Length < 8)
            {
                MessageBox.Show("Password must be 8 or more characters long!", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtboxPassword.Text = "";
                txtboxPassword.Focus();
                return;
            }

            else if (txtboxPassword.Text == txtboxPassword.Text)
            {
                string FirstName = txtboxFirstname.Text;
                string Password = AesOperation.EncryptString(txtboxPassword.Text);
                string Username = txtboxUsername.Text;
                string LastName = txtboxLastname.Text;

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

                conn.Open();
                string register = "INSERT INTO tblUsers(Username, LastName, FirstName, Password) VALUES ('" + Username + "','" +
                    LastName.Substring(0, 1).ToUpper() + LastName.Substring(1) + "','" +
                    FirstName.Substring(0, 1).ToUpper() + FirstName.Substring(1) + "', '" + Password + "')";
                cmd = new SqlCommand(register, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Successfully Added", "Add User Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                form.refresh();
            }
        }
    }
}
