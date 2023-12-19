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
    public partial class AdminUsersForm : Form
    {
        public AdminUsersForm()
        {
            InitializeComponent();
            load();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=GOUJO\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True");
        textPadding tp = new textPadding();

        public void load()
        {

            string query = "SELECT * FROM tblUsers";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridUsers.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dataGridUsers.Rows.Add(row[0], row[1], row[2], row[3], row[4]);
            }
            //fetch username to display in label
            lblfirstnameDisplay.Text = LoggedInUser.Firstname;

        }
        public void refresh()
        {
            load();
        }
        private void btnBooks_Click(object sender, EventArgs e)
        {
            AdminForm books = new AdminForm();
            books.Show();
            this.Hide();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            AdminReportsForm reports = new AdminReportsForm();
            reports.Show();
            this.Hide();
        }

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxSearch, new Padding(15, 10, 10, 10));
            string query = "SELECT * FROM tblUsers WHERE userid LIKE @search OR firstname LIKE @search OR lastname LIKE @search OR username LIKE @search";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@search", "%" + txtboxSearch.Text + "%");
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridUsers.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dataGridUsers.Rows.Add(row[0], row[1], row[2], row[3], row[4]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminAddUser adduser = new AdminAddUser();
            adduser.Show();
        }

        private void dataGridBooks_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hitTest = dataGridUsers.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    dataGridUsers.ClearSelection();
                    dataGridUsers.Rows[hitTest.RowIndex].Selected = true;
                    menustripUsers.Show(dataGridUsers, e.Location);

                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = null;
            if (dataGridUsers.SelectedRows.Count > 0)
            {
                selectedRow = dataGridUsers.SelectedRows[0];
            }
            else if (dataGridUsers.SelectedCells.Count > 0)
            {
                selectedRow = dataGridUsers.SelectedCells[0].OwningRow;
            }

            string firstname = selectedRow.Cells["firstname"].Value.ToString();
            string lastname = selectedRow.Cells["lastname"].Value.ToString();
            string username = selectedRow.Cells["username"].Value.ToString();

            AdminEditUser form = new AdminEditUser();
            form.txtboxFirstname.Text = firstname;
            form.txtboxLastname.Text = lastname;
            form.txtboxUsername.Text = username;
            form.Show();

        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = dataGridUsers.SelectedRows;

            string id = dataGridUsers.CurrentRow.Cells[0].Value.ToString();
            DialogResult result = MessageBox.Show("Are you sure you want to delete this? Once deleted the action cannot be undone", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                conn.Open();
                string command = "DELETE FROM tblUsers WHERE userid = @userid";
                using (SqlCommand cmd = new SqlCommand(command, conn))
                {
                    cmd.Parameters.AddWithValue("@userid", id);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("User deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
                refresh();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                LoginPage login = new LoginPage();
                login.Show();
                this.Close();
            }
            else
            {

            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changepass = new ChangePasswordForm();
            changepass.Show();
        }

        private void AdminUsersForm_Load(object sender, EventArgs e)
        {

        }
    }
}
