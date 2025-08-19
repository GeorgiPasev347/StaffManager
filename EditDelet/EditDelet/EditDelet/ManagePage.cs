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
namespace EditDelet
{
    public partial class ManagePage : Form
    {
        public ManagePage()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\PC\\Documents\\EditDelet.mdf;Integrated Security=True;Connect Timeout=30";
        private void ManagePage_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Customers", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            {
                string query = "INSERT INTO Customers (FirstName,SecName,CustId) VALUES (@fname,@secname,@custid)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fname", textBox1.Text);
                cmd.Parameters.AddWithValue("@secname", textBox2.Text);
                cmd.Parameters.AddWithValue("@custId", int.Parse(textBox3.Text));
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            LoadData();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string fname = dataGridView1.SelectedRows[0].Cells["FirstName"].Value.ToString();
                string secname = dataGridView1.SelectedRows[0].Cells["SecName"].Value.ToString();
                string custId = dataGridView1.SelectedRows[0].Cells["CustId"].Value.ToString();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Customers WHERE FirstName=@fname AND SecName=@secname AND CustId=@custId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@fname", fname);
                    cmd.Parameters.AddWithValue("@secname", secname);
                    cmd.Parameters.AddWithValue("@custId", custId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
               
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["FirstName"].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells["SecName"].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells["CustId"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Моля, изберете ред за редактиране.");
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            string newFName = textBox1.Text.Trim();
            string newSecName = textBox2.Text.Trim();
            string custId = textBox3.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Customers SET FirstName=@newFName, SecName=@newSecName WHERE CustId=@custId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@newFName", newFName);
                    cmd.Parameters.AddWithValue("@newSecName", newSecName);
                    cmd.Parameters.AddWithValue("@custId", custId);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            MessageBox.Show("Записът е обновен успешно.");
                        else
                            MessageBox.Show("Не е намерен запис за обновяване.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Грешка при обновяване: " + ex.Message);
                    }
                }
            }




            LoadData(); 
        }
    }
}

