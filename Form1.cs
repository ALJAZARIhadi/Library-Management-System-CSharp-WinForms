using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WinLibrary
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        string connectionString = @"Data Source=X-RAY\SQLEXPRESS01;Initial Catalog=LibraryDB;Integrated Security=True";
        
        private void LoadCategories()
        {
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("History");
            treeView1.Nodes.Add("Relogin");
            treeView1.Nodes.Add("Math");
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadData();
        }
        private void Emptyingٍpaces()
        {
            comboBox1.Text = string.Empty;
            txtBookName.Text = string.Empty;
            txtAuthor.Text = string.Empty;
            txtIsbn.Text = string.Empty;
            txtPage.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string category = comboBox1.Text.Trim();
            string name = txtBookName.Text.Trim();
            string author = txtAuthor.Text.Trim();
            string isbn = txtIsbn.Text.Trim();
            int pages;

            if (string.IsNullOrWhiteSpace(category) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(author) ||
                string.IsNullOrWhiteSpace(isbn) ||
                !int.TryParse(txtPage.Text, out pages))
            {
                MessageBox.Show("Please fill in all fields correctly.");
                return;
            }

            // Insert into SQL database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Books (Category, BookName, Author, Pages, ISBN) VALUES (@c, @n, @a, @p, @i)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@c", category);
                    cmd.Parameters.AddWithValue("@n", name);
                    cmd.Parameters.AddWithValue("@a", author);
                    cmd.Parameters.AddWithValue("@p", pages);
                    cmd.Parameters.AddWithValue("@i", isbn);
                    cmd.ExecuteNonQuery();
                }
            }

            // Find the TreeView node with the selected category
            TreeNode categoryNode = null;
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Text.Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    categoryNode = node;
                    break;
                }
            }

            // If found, add book as a child node
            if (categoryNode != null)
            {
                categoryNode.Nodes.Add(name);
                categoryNode.Expand();
            }

            MessageBox.Show("Book added successfully!");
            LoadData();
            Emptyingٍpaces();
        }
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Books", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Prevent error if user clicks on header row
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                comboBox1.Text = row.Cells[1].Value.ToString();  // Category
                txtBookName.Text = row.Cells[2].Value.ToString(); // BookName
                txtAuthor.Text = row.Cells[3].Value.ToString();   // Author
                txtPage.Text = row.Cells[4].Value.ToString();     // Pages
                txtIsbn.Text = row.Cells[5].Value.ToString();     // ISBN
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get values from textboxes
            string category = comboBox1.Text;
            string name = txtBookName.Text;
            string author = txtAuthor.Text;
            int pages = int.Parse(txtPage.Text);
            string isbn = txtIsbn.Text;

            // Get the selected book Id (assuming first column is Id)
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("Please select a book to update.");
                return;
            }

            int id = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Books SET Category=@c, BookName=@n, Author=@a, Pages=@p, ISBN=@i WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@c", category);
                    cmd.Parameters.AddWithValue("@n", name);
                    cmd.Parameters.AddWithValue("@a", author);
                    cmd.Parameters.AddWithValue("@p", pages);
                    cmd.Parameters.AddWithValue("@i", isbn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Book updated successfully!");
            LoadData(); // Refresh the DataGridView
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Only run if the user double-clicks a top-level category node
            if (e.Node.Parent == null)
            {
                string category = e.Node.Text;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT BookName FROM Books WHERE Category = @c";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@c", category);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            e.Node.Nodes.Clear(); // Clear old children first
                            while (reader.Read())
                            {
                                string bookName = reader["BookName"].ToString();
                                e.Node.Nodes.Add(bookName);
                            }
                        }
                    }
                }

                e.Node.Expand();
            }
        }
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("Please select a book to delete.");
                return;
            }

            // Get the selected book's ID
            int id = Convert.ToInt32(dataGridView2.CurrentRow.Cells["ID"].Value);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this book?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Books WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Book deleted successfully!");
                LoadData();
                Emptyingٍpaces();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string name = txtBookName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a book name to search.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Books WHERE BookName LIKE '%' + @name + '%'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView2.DataSource = dt;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Close the current form (this Form1)
                this.Hide(); // or this.Close();

                // Show login page again
                LoginPage login = new LoginPage();
                login.Show();
            }
        }
         private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to Add New User?",
                "Confirm Adding",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Close the current form (this Form1)
                this.Hide(); // or this.Close();

                // Show login page again
                RegisterPage regist = new RegisterPage();
                regist.Show();
            }
        }

        
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData(); // Reload the data grid
        }

        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
