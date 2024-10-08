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
using MySql.Data.MySqlClient;

namespace мирмыслей
{
    public partial class Form1 : Form
    {
        // Строка подключения к базе данных MySQL
        string connectionString = "server=localhost;database=userss;username=root;password=pussykiller21!;";

        // Объект DataTable для хранения данных из таблицы
        private DataTable productDataTable = new DataTable();

        // Текущая строка, редактируемая в TextBox
        private int currentRow = -1;

        public Form1()
        {
            InitializeComponent();
        }

//пустые методы
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Получаем данные из текстовых полей
            string name = guna2TextBox1.Text;
            string surname = guna2TextBox2.Text;
            string login = guna2TextBox6.Text;
            string phone = guna2TextBox3.Text;
            string password = guna2TextBox4.Text;
            string kod = guna2TextBox8.Text;

            // Проверяем, заполнены ли поля
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(kod))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            try
            {
                // Создаем соединение с базой данных
                using (MySqlConnection connection = new MySqlConnection(connectionString)) // Используем MySqlConnection
                {
                    // Открываем соединение
                    connection.Open();

                    //(дополнение: string query = "INSERT INTO таблица (все нужные столбцы) VALUES (@name, @surname, @email, @phone, @password, @kod)";

                    // Создаем команду INSERT для вставки данных
                    string query = "INSERT INTO us (name, surname, login, phone, password, kod) VALUES (@name, @surname, @login, @phone, @password, @kod)";
                    using (MySqlCommand command = new MySqlCommand(query, connection)) // Используем MySqlCommand
                    {
                        // Добавляем параметры для команды
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@surname", surname);
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@phone", phone);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@kod", kod);

                        // Выполняем команду INSERT
                        command.ExecuteNonQuery();

                        MessageBox.Show("Регистрация прошла успешно!");

                        // Очищаем текстовые поля
                        guna2TextBox1.Text = "";
                        guna2TextBox2.Text = "";
                        guna2TextBox3.Text = "";
                        guna2TextBox4.Text = "";
                        guna2TextBox5.Text = "";
                        guna2TextBox6.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при регистрации: " + ex.Message);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string login = guna2TextBox7.Text;
            string password = guna2TextBox5.Text;
            string kod = guna2TextBox9.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(kod))
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя и код доступа.");
                return;
            }

            try
            {
                // Создаем соединение с базой данных
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Открываем соединение
                    connection.Open();

                    // Создаем команду SELECT для проверки пользователя
                    string query = "SELECT COUNT(*) FROM us WHERE login = @login AND password = @password AND kod = @kod";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Добавляем параметры для команды
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@kod", kod);

                        // Выполняем команду SELECT
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        if (count > 0)
                        {
                            // Пользователь найден, авторизация успешна
                            MessageBox.Show("Авторизация успешна!");

                            // Очистка полей ввода
                            guna2TextBox7.Text = "";
                            guna2TextBox5.Text = "";
                            guna2TextBox9.Text = "";

                            // Проверка кода доступа
                            if (kod == "книга") // Замените "определённый_код_доступа" на нужный код
                            {
                                // Переход на вкладку менеджера
                                ManegerForm managerForm = new ManegerForm(); // Открытие формы для менеджера
                                managerForm.Show();
                            }
                            else
                            {
                                // Если код доступа не соответствует, открываем стандартную форму
                                Form2 form2 = new Form2(); // Передача логина и пароля в конструктор Form2
                                form2.Show();
                            }

                            this.Hide(); // Скрываем текущую форму
                        }
                        else
                        {
                            // Пользователь не найден, авторизация неуспешна
                            MessageBox.Show("Неверный логин, код или пароль.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при авторизации: " + ex.Message);
            }

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
