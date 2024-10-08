using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;

namespace мирмыслей
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();

            // Заполнение комбобоксов
            guna2ComboBox1.Items.Add("Самовывоз");
            guna2ComboBox1.Items.Add("Курьерская доставка");

            // Установка начального выбранного элемента
            guna2ComboBox1.SelectedIndex = 0; // "Самовывоз" по умолчанию
            guna2ComboBox1.SelectedIndexChanged += guna2ComboBox1_SelectedIndexChanged;

            // Делаем поле ввода адреса видимым по умолчанию
            guna2TextBox2.Visible = true;

        }

        // Строка подключения к базе данных MySQL
        string connectionString = "server=localhost;database=userss;username=root;password=pussykiller21!;";

        public class DatabaseConnection
        {
            public static MySqlConnection GetConnection()
            {
                string connectionString = "server=localhost;database=userss;username=root;password=pussykiller21!;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                return connection;
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Изменяем видимость поля ввода адреса в зависимости от выбранного метода доставки
            if (guna2ComboBox1.SelectedIndex == 1) // Курьерская доставка
            {
                guna2TextBox2.Visible = true;
            }
            else // Самовывоз
            {
                guna2TextBox2.Visible = false;
                guna2TextBox2.Text = ""; // Очищаем поле адреса, если самовывоз
            }
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            // Получаем данные из текстовых полей
            string title = guna2TextBox1.Text;
            string author = guna2TextBox3.Text;
            string polz = guna2TextBox4.Text; // Предполагаю, что у вас есть поле для жанра
            string adress = guna2TextBox2.Text;
           

            // Проверяем, заполнены ли поля
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) ||
                string.IsNullOrEmpty(polz) || string.IsNullOrEmpty(adress))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            try
            {
                // Создаем соединение с базой данных
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Открываем соединение
                    connection.Open();

                    // Проверяем, существует ли книга в таблице "books"
                    string checkQuery = "SELECT COUNT(*) FROM books WHERE title = @title AND author = @author";
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@title", title);
                        checkCommand.Parameters.AddWithValue("@author", author);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        // Если книга не найдена, выводим сообщение и выходим из метода
                        if (count == 0)
                        {
                            MessageBox.Show("Книга с таким названием и автором не найдена в базе данных!");
                            return;
                        }
                    }

                    // Если книга найдена, добавляем заказ
                    string insertQuery = "INSERT INTO zakaz (title, author, polz, adress) VALUES (@title, @author, @polz, @adress)";
                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@title", title);
                        insertCommand.Parameters.AddWithValue("@author", author);
                        insertCommand.Parameters.AddWithValue("@polz", polz);
                        insertCommand.Parameters.AddWithValue("@adress", adress);
                      
                        insertCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Заказ успешно добавлен!");

                    // Очищаем текстовые поля
                    guna2TextBox1.Text = "";
                    guna2TextBox3.Text = "";
                    guna2TextBox4.Text = "";
                    guna2TextBox2.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении заказа: " + ex.Message);
            }
        }
    }
}
