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
    public partial class UserBackpackForm : Form
    {
        public UserBackpackForm()
        {
            InitializeComponent();
            load();
        }
        textPadding tp = new textPadding();
        SqlConnection conn = new SqlConnection(@"Data Source=GOUJO\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True");
        public void load()
        {

            string query = "SELECT * FROM tblBooksBorrowed";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridBooksBorrowed.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dataGridBooksBorrowed.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5]);
            }
            //fetch username to display in label
            lblfirstnameDisplay.Text = LoggedInUser.Firstname;

        }
        public void refresh()
        {
            load();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            UserForm home = new UserForm();
            home.Show();
            this.Hide();
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            UserBooksForm books = new UserBooksForm();
            books.Show();
            this.Hide();
        }

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxSearch, new Padding(15, 10, 10, 10));
            string searchValue = txtboxSearch.Text.Trim();
            string query = "SELECT * FROM tblBooksBorrowed WHERE borrowid LIKE @search OR booktitle LIKE @search OR bookauthor LIKE @search OR quantity LIKE @search";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridBooksBorrowed.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dataGridBooksBorrowed.Rows.Add(row[1], row[2], row[3], row[4], row[5]);
            }
        }

        private void dataGridBooksBorrowed_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hitTest = dataGridBooksBorrowed.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    dataGridBooksBorrowed.ClearSelection();
                    dataGridBooksBorrowed.Rows[hitTest.RowIndex].Selected = true;
                    menustripBooksBorrowed.Show(dataGridBooksBorrowed, e.Location);

                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = null;
            if (dataGridBooksBorrowed.SelectedRows.Count > 0)
            {
                selectedRow = dataGridBooksBorrowed.SelectedRows[0];
            }
            else if (dataGridBooksBorrowed.SelectedCells.Count > 0)
            {
                selectedRow = dataGridBooksBorrowed.SelectedCells[0].OwningRow;
            }
            string bookTitle = selectedRow.Cells["booktitle"].Value.ToString();
            string bookAuthor = selectedRow.Cells["bookauthor"].Value.ToString();
            string dateTimeBorrow = selectedRow.Cells["dateTimeBorrow"].Value.ToString();
            string dateTimeReturn = selectedRow.Cells["dateTimeReturn"].Value.ToString();
            string statusBorrow = "RETURNED";

            UserConfirmReturnForm rf = new UserConfirmReturnForm();
            rf.txtboxtitle.Text = bookTitle;
            rf.txtboxauthor.Text = bookAuthor;
            rf.txtboxBorrowdate.Text = dateTimeBorrow;
            rf.txtboxReturndate.Text = dateTimeReturn;
            rf.txtboxStatusBorrow.Text = statusBorrow;
            rf.Show();
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

        private void lblfirstnameDisplay_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
