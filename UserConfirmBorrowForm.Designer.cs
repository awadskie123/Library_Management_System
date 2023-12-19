
namespace LibSysFINALFINAL
{
    partial class UserConfirmBorrowForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserConfirmBorrowForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtboxStatusBorrow = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.listboxGenre = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtboxauthor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtboxtitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtboxquantity = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RosyBrown;
            this.panel1.Controls.Add(this.txtboxStatusBorrow);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnConfirm);
            this.panel1.Controls.Add(this.listboxGenre);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtboxauthor);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtboxtitle);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtboxquantity);
            this.panel1.Location = new System.Drawing.Point(16, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1035, 407);
            this.panel1.TabIndex = 2;
            // 
            // txtboxStatusBorrow
            // 
            this.txtboxStatusBorrow.Location = new System.Drawing.Point(832, 43);
            this.txtboxStatusBorrow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtboxStatusBorrow.Name = "txtboxStatusBorrow";
            this.txtboxStatusBorrow.Size = new System.Drawing.Size(132, 22);
            this.txtboxStatusBorrow.TabIndex = 21;
            this.txtboxStatusBorrow.Text = "Borrowed";
            this.txtboxStatusBorrow.Visible = false;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(115)))), ((int)(((byte)(232)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(104, 302);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 52);
            this.button1.TabIndex = 20;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(115)))), ((int)(((byte)(232)))));
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(311, 302);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(192, 52);
            this.btnConfirm.TabIndex = 19;
            this.btnConfirm.Text = "Borrow";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // listboxGenre
            // 
            this.listboxGenre.BackColor = System.Drawing.SystemColors.Info;
            this.listboxGenre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listboxGenre.FormattingEnabled = true;
            this.listboxGenre.ItemHeight = 25;
            this.listboxGenre.Items.AddRange(new object[] {
            "Fiction",
            "Non-fiction",
            "Romance",
            "Mystery/Thriller",
            "Science Fiction/Fantasy",
            "Horror",
            "Young Adult (YA)"});
            this.listboxGenre.Location = new System.Drawing.Point(536, 177);
            this.listboxGenre.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listboxGenre.Name = "listboxGenre";
            this.listboxGenre.Size = new System.Drawing.Size(397, 154);
            this.listboxGenre.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(531, 150);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "Genre";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(99, 150);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "Author";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtboxauthor
            // 
            this.txtboxauthor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtboxauthor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtboxauthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtboxauthor.Location = new System.Drawing.Point(104, 177);
            this.txtboxauthor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtboxauthor.Multiline = true;
            this.txtboxauthor.Name = "txtboxauthor";
            this.txtboxauthor.Size = new System.Drawing.Size(399, 49);
            this.txtboxauthor.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(424, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "Title";
            // 
            // txtboxtitle
            // 
            this.txtboxtitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtboxtitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtboxtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtboxtitle.Location = new System.Drawing.Point(429, 73);
            this.txtboxtitle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtboxtitle.Multiline = true;
            this.txtboxtitle.Name = "txtboxtitle";
            this.txtboxtitle.Size = new System.Drawing.Size(505, 49);
            this.txtboxtitle.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(99, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Quantity";
            // 
            // txtboxquantity
            // 
            this.txtboxquantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtboxquantity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtboxquantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtboxquantity.Location = new System.Drawing.Point(104, 73);
            this.txtboxquantity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtboxquantity.Multiline = true;
            this.txtboxquantity.Name = "txtboxquantity";
            this.txtboxquantity.Size = new System.Drawing.Size(265, 49);
            this.txtboxquantity.TabIndex = 2;
            this.txtboxquantity.Text = "1";
            this.txtboxquantity.TextChanged += new System.EventHandler(this.txtboxquantity_TextChanged);
            // 
            // UserConfirmBorrowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1067, 433);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UserConfirmBorrowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confirm your borrow";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnConfirm;
        public System.Windows.Forms.ListBox listboxGenre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtboxauthor;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtboxtitle;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtboxquantity;
        public System.Windows.Forms.TextBox txtboxStatusBorrow;
    }
}