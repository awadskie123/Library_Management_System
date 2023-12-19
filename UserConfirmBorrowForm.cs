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
    public partial class UserConfirmBorrowForm : Form
    {
        public UserConfirmBorrowForm()
        {
            InitializeComponent();
        }
        textPadding tp = new textPadding();
        private void txtboxquantity_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxquantity, new Padding(15, 10, 10, 10));
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string status = txtboxStatusBorrow.Text;
            string bookTitle = txtboxtitle.Text;
            string bookAuthor = txtboxauthor.Text;
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
                    if (quantityObj != null)
                    {
                        // Book already exists in data source, update quantity
                        int existingQuantity = int.Parse(quantityObj.ToString());
                        string updateQuery = ("UPDATE tblBooksBorrowed SET quantity = @newQuantity WHERE booktitle = @booktitle AND bookauthor = @bookauthor");
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@booktitle", bookTitle);
                            updateCmd.Parameters.AddWithValue("@bookauthor", bookAuthor);
                            updateCmd.Parameters.AddWithValue("@newQuantity", existingQuantity + quantity);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Book not found in data source, insert new row
                        string insertQuery = ("INSERT INTO tblBooksBorrowed(booktitle, bookauthor, dateTimeBorrow, dateTimeReturn, quantity) VALUES (@booktitle, @bookauthor, @dateTimeBorrow, @dateTimeReturn, @quantity)");
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                        {
                            insertCmd.Parameters.AddWithValue("@booktitle", bookTitle.Substring(0, 1).ToUpper() + bookTitle.Substring(1));
                            insertCmd.Parameters.AddWithValue("@bookauthor", bookAuthor.Substring(0, 1).ToUpper() + bookAuthor.Substring(1));
                            insertCmd.Parameters.AddWithValue("@dateTimeBorrow", dateTimeBorrow);
                            insertCmd.Parameters.AddWithValue("@dateTimeReturn", dateTimeReturn);
                            insertCmd.Parameters.AddWithValue("@quantity", quantity);
                            int rowsUpdated = insertCmd.ExecuteNonQuery();
                            if (rowsUpdated == 0)
                            {
                                // handle the case where no rows were updated, e.g. book not found
                            }
                        }
                    }
                    if(quantity > 0)
                    {
                        // Update tblBooks table subtracting quantity
                        string queryDB = ("UPDATE tblBooks SET quantity = quantity - @quantity WHERE bookTitle = @bookTitle AND bookAuthor = @bookAuthor");
                        using (SqlCommand updateDB = new SqlCommand(queryDB, connection))
                        {
                            updateDB.Parameters.AddWithValue("@quantity", quantity);
                            updateDB.Parameters.AddWithValue("@bookTitle", bookTitle.Substring(0, 1).ToUpper() + bookTitle.Substring(1));
                            updateDB.Parameters.AddWithValue("@bookAuthor", bookAuthor.Substring(0, 1).ToUpper() + bookAuthor.Substring(1));
                            //ACTION updateDB.Parameters.AddWithValue("@status", statusBorrow.Substring(0, 1).ToUpper() + statusBorrow.Substring(1));
                            updateDB.ExecuteNonQuery();
                        }
                        
                    }
                    else
                    {
                        //Update tblBooks table if quantity reaches 0 
                        string queryDB2 = ("UPDATE tblBooks SET quantity = quantity - @quantity, status = 'Inactive' WHERE bookTitle = @bookTitle AND bookAuthor = @bookAuthor");
                        using(SqlCommand updatetblBooks = new SqlCommand(queryDB2, connection))
                        {
                            updatetblBooks.Parameters.AddWithValue("@quantity", quantity);
                            updatetblBooks.Parameters.AddWithValue("@bookTitle", bookTitle.Substring(0, 1).ToUpper() + bookTitle.Substring(1));
                            updatetblBooks.Parameters.AddWithValue("@bookAuthor", bookAuthor.Substring(0, 1).ToUpper() + bookAuthor.Substring(1));
                            updatetblBooks.ExecuteNonQuery();
                        }
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

                    UserBooksForm form = Application.OpenForms.OfType<UserBooksForm>().FirstOrDefault();
                    MessageBox.Show("Success! Check your bag", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    form.refresh();
                    connection.Close();
                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
