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
    public partial class AdminEditBook : Form
    {
        public AdminEditBook()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=GOUJO\\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True");
        textPadding tp = new textPadding();
        private void txtboxquantity_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxquantity, new Padding(15, 10, 10, 10));
        }

        private void txtboxtitle_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxtitle, new Padding(15, 10, 10, 10));
        }

        private void txtboxauthor_TextChanged(object sender, EventArgs e)
        {
            tp.SetPadding(txtboxauthor, new Padding(15, 10, 10, 10));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            AdminForm form = Application.OpenForms.OfType<AdminForm>().FirstOrDefault();
            string bookTitle = txtboxtitle.Text;
            string bookAuthor = txtboxauthor.Text;
            string bookGenre = listboxGenre.Text;
            int quantity;

            if (!int.TryParse(txtboxquantity.Text, out quantity))
            {
                MessageBox.Show("Quantity must be a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Data Source=GOUJO\\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if(quantity > 0)
                {
                    string commandText = "UPDATE tblBooks SET BookTitle = @BookTitle, BookAuthor = @BookAuthor, BookGenre = @BookGenre, Quantity = @Quantity, status = 'Active' WHERE BookID = @BookID";
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        int bookID = Convert.ToInt32(form.dataGridBooks.SelectedRows[0].Cells["bookid"].Value);
                        command.Parameters.AddWithValue("@BookTitle", bookTitle.Substring(0, 1).ToUpper() + bookTitle.Substring(1));
                        command.Parameters.AddWithValue("@BookAuthor", bookAuthor.Substring(0, 1).ToUpper() + bookAuthor.Substring(1));
                        command.Parameters.AddWithValue("@BookGenre", bookGenre);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@BookID", bookID);
                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();
                        if (form != null)
                        {
                            MessageBox.Show($"{rowsAffected} row(s) updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            form.refresh();
                        }
                    }
                }
                else
                {
                    string commandText = "UPDATE tblBooks SET BookTitle = @BookTitle, BookAuthor = @BookAuthor, BookGenre = @BookGenre, Quantity = @Quantity, status = 'Inactive' WHERE BookID = @BookID";
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        int bookID = Convert.ToInt32(form.dataGridBooks.SelectedRows[0].Cells["bookid"].Value);
                        command.Parameters.AddWithValue("@BookTitle", bookTitle.Substring(0, 1).ToUpper() + bookTitle.Substring(1));
                        command.Parameters.AddWithValue("@BookAuthor", bookAuthor.Substring(0, 1).ToUpper() + bookAuthor.Substring(1));
                        command.Parameters.AddWithValue("@BookGenre", bookGenre);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@BookID", bookID);
                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();
                        if (form != null)
                        {
                            MessageBox.Show($"{rowsAffected} row(s) updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            form.refresh();
                        }
                    }
                }
                
            }

            this.Close();
        }
    }
}
