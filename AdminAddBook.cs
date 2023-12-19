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
    public partial class AdminAddBook : Form
    {
        public AdminAddBook()
        {
            InitializeComponent();
        }
        textPadding tp = new textPadding();
        SqlConnection conn = new SqlConnection("Data Source=GOUJO\\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        string sql;
        bool Mode = true;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string booktitle = txtboxtitle.Text;
            string bookauthor = txtboxauthor.Text;
            string bookgenre = listboxGenre.Text;
            int quantity;

            AdminForm form = Application.OpenForms.OfType<AdminForm>().FirstOrDefault();

            if (!int.TryParse(txtboxquantity.Text, out quantity))
            {
                MessageBox.Show("Fields are empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Mode == true)
            {
                if (txtboxtitle.Text == "" || txtboxauthor.Text == "" || listboxGenre.Text == "")
                {
                    MessageBox.Show("Fields are empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if(quantity > 0)
                {
                    sql = "INSERT INTO tblBooks(booktitle ,bookauthor, bookgenre, quantity )values(@booktitle,@bookauthor,@bookgenre,@quantity)";
                    conn.Open();
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@booktitle", booktitle.Substring(0, 1).ToUpper() + booktitle.Substring(1));
                    cmd.Parameters.AddWithValue("@bookauthor", bookauthor.Substring(0, 1).ToUpper() + bookauthor.Substring(1));
                    cmd.Parameters.AddWithValue("@bookgenre", bookgenre);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    if (form != null)
                    {
                        form.refresh();
                    }
                }
                else
                {
                    sql = "INSERT INTO tblBooks (booktitle, bookauthor, bookgenre, quantity, status)VALUES(@booktitle, @bookauthor, @bookgenre, @quantity, 'Inactive'); ";
                    conn.Open();
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@booktitle", booktitle.Substring(0, 1).ToUpper() + booktitle.Substring(1));
                    cmd.Parameters.AddWithValue("@bookauthor", bookauthor.Substring(0, 1).ToUpper() + bookauthor.Substring(1));
                    cmd.Parameters.AddWithValue("@bookgenre", bookgenre);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    if (form != null)
                    {
                        form.refresh();
                    }
                }

                MessageBox.Show("Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {

            }
        }
    }
}
