using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace мирмыслей
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }
        public class DatabaseConnection
        {
            public static MySqlConnection GetConnection()
            {
                string connectionString = "server=localhost;database=userss;username=root;password=pussykiller21!;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                return connection;
            }

        }
        private void Form6_Load(object sender, EventArgs e)
        {
            string poo = "SELECT * FROM zakaz"; // Исправленное название таблицы
            using (MySqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(poo, connection))
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;

                    // Устанавливаем русские заголовки для столбцов
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "Название";
                    dataGridView1.Columns[2].HeaderText = "Автор";
                    dataGridView1.Columns[3].HeaderText = "Получатель";
                    dataGridView1.Columns[4].HeaderText = "Адрес";

                    connection.Close();
                }
            }


        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
