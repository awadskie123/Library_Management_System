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
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
        }
        SqlCommand cmd = new SqlCommand();
        textPadding tp = new textPadding();
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtboxOldPassword_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxOldPassword, new Padding(15, 10, 10, 10));
        }

        private void txtboxNewPassword_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxNewPassword, new Padding(15, 10, 10, 10));
        }

        private void txtboxConfirmNewPass_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxConfirmNewPass, new Padding(15, 10, 10, 10));
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // retrieve the old password from the database
            string connectionString = @"Data Source=GOUJO\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True";
            string query = "SELECT password FROM tblUsers WHERE username = @username";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", LoggedInUser.Username);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string storedEncryptedPassword = dt.Rows[0]["password"].ToString();

                    // compare old password with the stored encrypted password
                    if (AesOperation.EncryptString(txtboxOldPassword.Text) == storedEncryptedPassword)
                    {
                        // check if new password matches the confirmation password
                        if (txtboxNewPassword.Text == txtboxConfirmNewPass.Text)
                        {
                            // update the password in the database
                            string updateQuery = "UPDATE tblUsers SET password = @password WHERE username = @username";
                            SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                            updateCmd.Parameters.AddWithValue("@password", AesOperation.EncryptString(txtboxNewPassword.Text));
                            updateCmd.Parameters.AddWithValue("@username", LoggedInUser.Username);
                            conn.Open();
                            updateCmd.ExecuteNonQuery();
                            conn.Close();

                            MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("The new password and confirmation password do not match.", "Password Change Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtboxNewPassword.Text = "";
                            txtboxConfirmNewPass.Text = "";
                            txtboxNewPassword.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("The old password you entered is incorrect!", "Password Change Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtboxOldPassword.Text = "";
                        txtboxNewPassword.Text = "";
                        txtboxConfirmNewPass.Text = "";
                        txtboxOldPassword.Focus();
                    }
                }
            }

        }
    }
}
