using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Security.Cryptography;

namespace WinLibrary
{
    public partial class RegisterPage : Form
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        string connectionString = @"Data Source=X-RAY\SQLEXPRESS01;Initial Catalog=LibraryDB;Integrated Security=True";
       /*
        private string ComputeSha256Hash(string raw)
        {
            if (raw == null) return null;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(raw));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
       */
        private void btnSaveUser_Click(object sender, EventArgs e)
        { 
            string username = txtNewUserName.Text.Trim();
            string password = txtNewPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if username already exists
                    using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @u", conn))
                    {
                        checkCmd.Parameters.AddWithValue("@u", username);
                        int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (exists > 0)
                        {
                            MessageBox.Show("Username already exists. Choose another username.");
                            return;
                        }
                    }
                }
                this.Hide();
                LoginPage login = new LoginPage();
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}

