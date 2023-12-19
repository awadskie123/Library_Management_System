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
    public partial class UserConfirmReturnForm : Form
    {
        public UserConfirmReturnForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string bookTitle = txtboxtitle.Text;
            string bookAuthor = txtboxauthor.Text;
            string status = txtboxStatusBorrow.Text;
            int quantity = int.Parse(txtboxquantity.Text);
            string dateTimeBorrow = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
            DateTime returnDate = DateTime.Now.AddDays(7);
            string dateTimeReturn = returnDate.ToString("yyyy-MM-dd hh:mm tt");

            string connectionString = ("Data Source=GOUJO\\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Check if the book is already in the data source
                string selectQuery = ("SELECT quantity FROM tblBooksBorrowed WHERE booktitle = @booktitle AND bookauthor = @bookauthor");
                using (SqlCommand selectCmd = new SqlCommand(selectQuery, connection))
                {
                    selectCmd.Parameters.AddWithValue("@booktitle", bookTitle);
                    selectCmd.Parameters.AddWithValue("@bookauthor", bookAuthor);
                    connection.Open();
                    object quantityObj = selectCmd.ExecuteScalar();

                    // Book already exists in data source, update quantity
                    UserBackpackForm form = Application.OpenForms.OfType<UserBackpackForm>().FirstOrDefault();
                    int existingQuantity = int.Parse(quantityObj.ToString());
                    int newQuantity = existingQuantity - quantity;
                    if (newQuantity > 0)
                    {
                        string updateQuery = ("UPDATE tblBooksBorrowed SET quantity = @newQuantity WHERE booktitle = @booktitle AND bookauthor = @bookauthor");
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@booktitle", bookTitle.Substring(0, 1).ToUpper() + bookTitle.Substring(1));
                            updateCmd.Parameters.AddWithValue("@bookauthor", bookAuthor.Substring(0, 1).ToUpper() + bookAuthor.Substring(1));
                            updateCmd.Parameters.AddWithValue("@newQuantity", newQuantity);
                            updateCmd.ExecuteNonQuery();
                            form.refresh();
                        }
                    }
                    else
                    {
                        // Delete the book if the quantity reaches 0
                        UserBackpackForm backpack = Application.OpenForms.OfType<UserBackpackForm>().FirstOrDefault();
                        string borrowid = backpack.dataGridBooksBorrowed.CurrentRow.Cells[0].Value.ToString();
                        string deleteQuery = ("DELETE FROM tblBooksBorrowed WHERE borrowid = @borrowid;");
                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection))
                        {
                            deleteCmd.Parameters.AddWithValue("@borrowid", borrowid);
                            deleteCmd.ExecuteNonQuery();
                            backpack.refresh();
                        }
                    }
                    string queryDB = ("UPDATE tblBooks SET quantity = quantity + @quantity, status = 'Active' WHERE bookTitle = @bookTitle AND bookAuthor = @bookAuthor");
                    using (SqlCommand updateDB = new SqlCommand(queryDB, connection))
                    {
                        updateDB.Parameters.AddWithValue("@quantity", quantity);
                        updateDB.Parameters.AddWithValue("@bookTitle", bookTitle.Substring(0, 1).ToUpper() + bookTitle.Substring(1));
                        updateDB.Parameters.AddWithValue("@bookAuthor", bookAuthor.Substring(0, 1).ToUpper() + bookAuthor.Substring(1));
                        //CHANGE TO ACTION updateDB.Parameters.AddWithValue("@status", statusBorrow.Substring(0, 1).ToUpper() + statusBorrow.Substring(1));
                        updateDB.ExecuteNonQuery();
                    }

                    string queryreport = ("INSERT INTO tblReports(booktitle, dateTimeBorrow, dateTimeReturn, quantity, status) VALUES (@booktitle, @dateTimeBorrow, @dateTimeReturn, @quantity, @status)");
                    using (SqlCommand insertReport = new SqlCommand(queryreport, connection))
                    {
                        insertReport.Parameters.AddWithValue("@booktitle", bookTitle.Substring(0, 1).ToUpper() + bookTitle.Substring(1));
                        insertReport.Parameters.AddWithValue("@dateTimeBorrow", dateTimeBorrow);
                        insertReport.Parameters.AddWithValue("@dateTimeReturn", dateTimeReturn);
                        insertReport.Parameters.AddWithValue("@quantity", quantity);
                        insertReport.Parameters.AddWithValue("@status", status);
                        insertReport.ExecuteNonQuery();
                    }

                    MessageBox.Show(txtboxtitle.Text + " has been returned successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    connection.Close();
                    this.Close();
                }
            }
        }
    }
}
