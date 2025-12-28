namespace WinLibrary
{
    partial class RegisterPage
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
            this.btnSaveUser = new System.Windows.Forms.Button();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSaveUser
            // 
            this.btnSaveUser.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSaveUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveUser.FlatAppearance.BorderColor = System.Drawing.Color.SlateGray;
            this.btnSaveUser.FlatAppearance.BorderSize = 2;
            this.btnSaveUser.Font = new System.Drawing.Font("Tahoma", 20F);
            this.btnSaveUser.ForeColor = System.Drawing.Color.Cornsilk;
            this.btnSaveUser.Location = new System.Drawing.Point(120, 148);
            this.btnSaveUser.Name = "btnSaveUser";
            this.btnSaveUser.Size = new System.Drawing.Size(182, 51);
            this.btnSaveUser.TabIndex = 13;
            this.btnSaveUser.Text = "Save User";
            this.btnSaveUser.UseVisualStyleBackColor = false;
            this.btnSaveUser.Click += new System.EventHandler(this.btnSaveUser_Click);
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewPassword.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtNewPassword.Location = new System.Drawing.Point(211, 100);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(172, 32);
            this.txtNewPassword.TabIndex = 12;
            this.txtNewPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 20F);
            this.label2.Location = new System.Drawing.Point(34, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 33);
            this.label2.TabIndex = 11;
            this.label2.Text = "Password :";
            // 
            // txtNewUserName
            // 
            this.txtNewUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewUserName.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtNewUserName.Location = new System.Drawing.Point(211, 49);
            this.txtNewUserName.Name = "txtNewUserName";
            this.txtNewUserName.Size = new System.Drawing.Size(172, 32);
            this.txtNewUserName.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 20F);
            this.label1.Location = new System.Drawing.Point(34, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 33);
            this.label1.TabIndex = 9;
            this.label1.Text = "Username :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label3.Font = new System.Drawing.Font("Tahoma", 15F);
            this.label3.Location = new System.Drawing.Point(116, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 24);
            this.label3.TabIndex = 14;
            this.label3.Text = "Create New User ";
            // 
            // RegisterPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 211);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSaveUser);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNewUserName);
            this.Controls.Add(this.label1);
            this.Name = "RegisterPage";
            this.Text = "RegisterPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveUser;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNewUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}