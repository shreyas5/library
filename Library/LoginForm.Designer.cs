namespace Library
{
    partial class loginForm
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
            this.name_TB_login = new System.Windows.Forms.TextBox();
            this.password_TB_login = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.login_b_login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // name_TB_login
            // 
            this.name_TB_login.Location = new System.Drawing.Point(314, 166);
            this.name_TB_login.Name = "name_TB_login";
            this.name_TB_login.Size = new System.Drawing.Size(148, 20);
            this.name_TB_login.TabIndex = 0;
            // 
            // password_TB_login
            // 
            this.password_TB_login.Location = new System.Drawing.Point(314, 206);
            this.password_TB_login.Name = "password_TB_login";
            this.password_TB_login.PasswordChar = '*';
            this.password_TB_login.Size = new System.Drawing.Size(148, 20);
            this.password_TB_login.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(270, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(350, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 29);
            this.label3.TabIndex = 5;
            this.label3.Text = "LOGIN";
            // 
            // login_b_login
            // 
            this.login_b_login.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.login_b_login.Location = new System.Drawing.Point(351, 243);
            this.login_b_login.Name = "login_b_login";
            this.login_b_login.Size = new System.Drawing.Size(75, 23);
            this.login_b_login.TabIndex = 6;
            this.login_b_login.Text = "Login";
            this.login_b_login.UseVisualStyleBackColor = true;
            this.login_b_login.Click += new System.EventHandler(this.login_b_login_Click);
            // 
            // loginForm
            // 
            this.AcceptButton = this.login_b_login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.login_b_login);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.password_TB_login);
            this.Controls.Add(this.name_TB_login);
            this.Name = "loginForm";
            this.Text = "Library";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox name_TB_login;
        private System.Windows.Forms.TextBox password_TB_login;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button login_b_login;
    }
}

