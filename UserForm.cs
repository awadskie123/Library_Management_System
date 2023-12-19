using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibSysFINALFINAL
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
            //this.Closed += (s, ev) => Application.Exit();
            
            //fetch username to display in label
            lblfirstnameDisplay.Text = LoggedInUser.Firstname;
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            UserBooksForm books = new UserBooksForm();
            books.Show();
            this.Hide();
        }

        private void btnBag_Click(object sender, EventArgs e)
        {
            UserBackpackForm bag = new UserBackpackForm();
            bag.Show();
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

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changepass = new ChangePasswordForm();
            changepass.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
