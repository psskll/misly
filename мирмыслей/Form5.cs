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
    public partial class Form5 : Form
    {
        private ManegerForm Menegerform; // Экземпляр Form4
        public Form5()
        {
            InitializeComponent();
            Menegerform = new ManegerForm();
            // Добавление обработчика события при выборе строки в DataGridView1
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

            string p = "SELECT * FROM books"; // Используйте имя таблицы "q"

            // Создаем объект MySqlCommand
            using (MySqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(p, connection))
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
                    dataGridView1.Columns[3].HeaderText = "Количество страниц";
                    dataGridView1.Columns[4].HeaderText = "Жанр";
                    dataGridView1.Columns[5].HeaderText = "Цена";

                    connection.Close();
                }
            }
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
        private void Form5_Load(object sender, EventArgs e)
        {
            string poo = "SELECT * FROM books"; // Исправленное название таблицы
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
                    dataGridView1.Columns[3].HeaderText = "Количество страниц";
                    dataGridView1.Columns[4].HeaderText = "Жанр";
                    dataGridView1.Columns[5].HeaderText = "Цена";

                    connection.Close();
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для добавления в базу данных.", "Ошибка");
                return;
            }

            // Получаем значения из выбранной строки
            string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string title = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            string author = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            string numberofpages = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            string genre = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            string cost = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();


            // Формируем SQL-запрос для вставки
            string poo = "INSERT INTO books (title, author, numberofpages, genre, cost) VALUES (@title, @author, @numberofpages, @genre, @cost)";

            using (MySqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(poo, connection))
                {
                    // Добавляем параметры
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@author", author);
                    command.Parameters.AddWithValue("@numberofpages", numberofpages);
                    command.Parameters.AddWithValue("@genre", genre);
                    command.Parameters.AddWithValue("@cost", cost);


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Данные успешно добавлены в базу данных!", "Успешно");


                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка в DataGridView
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка");
                return;
            }

            // Получаем ID выбранной строки из DataGridView (предполагается, что ID находится в первом столбце)
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            // Формируем SQL-запрос для удаления строки
            string p = "DELETE FROM books WHERE ID = @id"; // Используйте имя таблицы "q" 

            // Создаем объект MySqlCommand
            using (MySqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(p, connection))
                {
                    // Добавляем параметр для защиты от SQL-инъекций
                    command.Parameters.AddWithValue("@id", id); // Передайте ID

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Строка успешно удалена из базы данных!", "Успешно");
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
            }
        }
    }
}
