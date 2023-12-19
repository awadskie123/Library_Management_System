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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            load();
        }
        textPadding tp = new textPadding();
        SqlConnection conn = new SqlConnection(@"Data Source=GOUJO\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True");
 
        public void load()
        {

            string query = "SELECT * FROM tblBooks WHERE status = 'Active' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridBooks.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dataGridBooks.Rows.Add(row[0], row[1], row[2], row[3], row[4]);
            }
            //fetch username to display in label
            lblfirstnameDisplay.Text = LoggedInUser.Firstname;
        }
        public void refresh()
        {
            load();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            AdminReportsForm reports = new AdminReportsForm();
            reports.Show();
            this.Hide();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            AdminUsersForm users = new AdminUsersForm();
            users.Show();
            this.Hide();
        }

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxSearch, new Padding(15, 10, 10, 10));
            string query = "SELECT * FROM tblBooks WHERE status = 'Active' AND (bookid LIKE @search OR booktitle LIKE @search OR bookauthor LIKE @search OR bookgenre LIKE @search OR quantity LIKE @search)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@search", "%" + txtboxSearch.Text + "%");
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridBooks.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dataGridBooks.Rows.Add(row[0], row[1], row[2], row[3], row[4]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminAddBook addbook = new AdminAddBook();
            addbook.Show();
        }

        private void dataGridBooks_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hitTest = dataGridBooks.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    dataGridBooks.ClearSelection();
                    dataGridBooks.Rows[hitTest.RowIndex].Selected = true;
                    menustripBooks.Show(dataGridBooks, e.Location);

                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = null;
            if (dataGridBooks.SelectedRows.Count > 0)
            {
                selectedRow = dataGridBooks.SelectedRows[0];
            }
            else if (dataGridBooks.SelectedCells.Count > 0)
            {
                selectedRow = dataGridBooks.SelectedCells[0].OwningRow;
            }

                string bookTitle = selectedRow.Cells["booktitle"].Value.ToString();
                string bookAuthor = selectedRow.Cells["bookauthor"].Value.ToString();
                string bookGenre = selectedRow.Cells["bookgenre"].Value.ToString();
                int quantity = (int)selectedRow.Cells["quantity"].Value;

                AdminEditBook form = new AdminEditBook();
                form.txtboxtitle.Text = bookTitle;
                form.txtboxauthor.Text = bookAuthor;
                form.listboxGenre.Text = bookGenre;
                form.txtboxquantity.Text = quantity.ToString();
                form.Show();
        
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = dataGridBooks.SelectedRows;

            string id = dataGridBooks.CurrentRow.Cells[0].Value.ToString();
            DialogResult result = MessageBox.Show("Are you sure you want to delete this? Once deleted the action cannot be undone", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                conn.Open();
                string command = "UPDATE tblBooks SET status = 'Inactive',quantity = 0 WHERE bookid = @bookid;";
                using (SqlCommand cmd = new SqlCommand(command, conn))
                {
                    cmd.Parameters.AddWithValue("@bookid", id);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Book has been deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnradioInactive_CheckedChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM tblBooks WHERE status = 'Inactive' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridBooks.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dataGridBooks.Rows.Add(row[0], row[1], row[2], row[3], row[4]);
            }
        }

        private void btnradioActive_CheckedChanged(object sender, EventArgs e)
        {
            load();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changepass = new ChangePasswordForm();
            changepass.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
