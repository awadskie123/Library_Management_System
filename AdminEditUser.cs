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
    public partial class AdminEditUser : Form
    {
        public AdminEditUser()
        {
            InitializeComponent();
        }
        textPadding tp = new textPadding();
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            AdminUsersForm form = Application.OpenForms.OfType<AdminUsersForm>().FirstOrDefault();
            string userid = form.dataGridUsers.CurrentRow.Cells[0].Value.ToString();
            string firstname = txtboxFirstname.Text;
            string lastname = txtboxLastname.Text;
            string username = txtboxUsername.Text;

            string connectionString = "Data Source=GOUJO\\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string commandText = "UPDATE tblUsers SET firstname = @firstname, lastname = @lastname, username = @username WHERE userid = @userid";
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@firstname", firstname.Substring(0, 1).ToUpper() + firstname.Substring(1));
                    command.Parameters.AddWithValue("@lastname", lastname.Substring(0, 1).ToUpper() + lastname.Substring(1));
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@userid", userid);
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();
                    if (form != null)
                    {
                        MessageBox.Show($"{rowsAffected} row(s) updated successfully, ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        form.refresh();

                        connection.Close();
                        this.Close();
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
