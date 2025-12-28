using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinLibrary
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        string connectionString = @"Data Source=X-RAY\SQLEXPRESS01;Initial Catalog=LibraryDB;Integrated Security=True";

        private void LoginPage_Load(object sender, EventArgs e)
        {
            // Optional: Add any initialization logic here
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check if the user exists and retrieve their admin status
                using (SqlCommand cmd = new SqlCommand("SELECT FROM Users WHERE Username = @u AND PasswordHash = @p", conn))
                {
                    try
                    {cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        // ✅ Login success
                        Form1 main = new Form1();
                        main.Show();
                        this.Hide();
                    }
                    else
                    {
                        // ❌ Login failed
                        MessageBox.Show("Invalid username or password.");
                    } }
                    catch
                    { }
                    
                }
            }
        }


        private void NewAccount_Click(object sender, EventArgs e)
        {
            RegisterPage reg = new RegisterPage();
            reg.Show();
            this.Close(); // Close the current LoginPage
        }
    }
}
