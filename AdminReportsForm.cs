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
    public partial class AdminReportsForm : Form
    {
        public AdminReportsForm()
        {
            InitializeComponent();
            load();
        }
        textPadding tp = new textPadding();
        SqlConnection conn = new SqlConnection(@"Data Source=GOUJO\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True");
        public void load()
        {
            string query = "SELECT r.quantity, r.reportsid, r.booktitle, r.dateTimeBorrow, r.dateTimeReturn, r.status, u.username " +
                           "FROM tblReports r " +
                           "LEFT JOIN tblUsers u ON r.userid = u.userid"; 
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridBooksBorrowed.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dataGridBooksBorrowed.Rows.Add(row[5], row[1], row[6], row[2], row[0], row[3], row[4]);
            }
            //fetch username to display in label
            lblfirstnameDisplay.Text = LoggedInUser.Firstname;
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            AdminUsersForm users = new AdminUsersForm();
            users.Show();
            this.Hide();
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            AdminForm books = new AdminForm();
            books.Show();
            this.Hide();
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

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxSearch, new Padding(15, 10, 10, 10));
            string query = ("SELECT * FROM tblReports WHERE booktitle LIKE @search OR status LIKE @search OR dateTimeBorrow LIKE @search OR dateTimeReturn LIKE @search OR quantity LIKE @search");
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@search", "%" + txtboxSearch.Text + "%");
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridBooksBorrowed.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dataGridBooksBorrowed.Rows.Add(row[5], row[0], row[0], row[1], row[4], row[2], row[3]);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printPrevDialog.Document = printDoc;
            printPrevDialog.ShowDialog();
        }

        private void printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Set up the font and the starting position
            Font font = new Font("Product Sans", 9, FontStyle.Regular);
            Font headerFont = new Font("Product Sans", 12, FontStyle.Bold);
            int y = 50;

            // Set the print area of the page
            Rectangle printArea = new Rectangle(50, 50, e.PageBounds.Width - 100, e.PageBounds.Height - 100);
            e.Graphics.SetClip(printArea);

            // Calculate the total width of the columns and the ratio to the print area
            float totalColumnWidth = dataGridBooksBorrowed.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            float ratio = totalColumnWidth / printArea.Width;

            // Adjust the width of the columns to fit within the print area
            foreach (DataGridViewColumn column in dataGridBooksBorrowed.Columns)
            {
                column.Width = (int)(column.Width / ratio);
            }

            // Add the "Printed by Admin" text to the left
            string printedBy = "Printed by Admin";
            e.Graphics.DrawString(printedBy, headerFont, Brushes.Black, printArea.Left, y);

            // Move the data below the printed text
            y += (int)(headerFont.GetHeight() + 30);

            // Draw the headers
            int x = 50;
            foreach (DataGridViewColumn column in dataGridBooksBorrowed.Columns)
            {
                e.Graphics.DrawString(column.HeaderText, headerFont, Brushes.Black, x, y);
                x += column.Width;
            }
            y += dataGridBooksBorrowed.ColumnHeadersHeight;

            // Iterate over the rows of the DataGridView
            foreach (DataGridViewRow row in dataGridBooksBorrowed.Rows)
            {
                // Skip the header row
                if (row.IsNewRow)
                {
                    continue;
                }

                // Iterate over the cells of the row and print their contents
                x = 50;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    e.Graphics.DrawString(cell.Value.ToString(), font, Brushes.Black, x, y);
                    x += cell.OwningColumn.Width;
                }

                y += row.Height;
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changepass = new ChangePasswordForm();
            changepass.Show();
        }

        private void btnSearchDate_Click(object sender, EventArgs e)
        {
            string dateFrom = dateTimeFrom.Value.ToString("yyyy-MM-dd hh:mm"); 
            string dateTo = dateTimeTo.Value.ToString("yyyy-MM-dd hh:mm");

            string query = "SELECT * FROM tblReports WHERE dateTimeBorrow BETWEEN @dateFrom AND @dateTo";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@dateTo", dateTo);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridBooksBorrowed.Rows.Clear();

            foreach (DataRow row in dt.Rows)
            {
                dataGridBooksBorrowed.Rows.Add(row[5], row[0], row[0], row[1], row[4], row[2], row[3]);
            }
        }

        private void btnReports_Click(object sender, EventArgs e)
        {

        }
    }
}
