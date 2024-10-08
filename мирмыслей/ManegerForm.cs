using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace мирмыслей
{
    public partial class ManegerForm : Form
    {
        public ManegerForm()
        {
            InitializeComponent();
        }
        private Color activeBackroundColor = Color.FromArgb(199, 159, 239);
        private Color activeForegroundColor = Color.FromArgb(106, 90, 205);

        private Color defaultBackgroundColor = Color.FromArgb(230, 230, 250);
        private Color defaultForekgroundColor = Color.FromArgb(200, 200, 200);

        //для перетаскивания формы
        bool dragging = false;
        Point dragCursorPoint;
        Point dragFormPoint;
      

        private void SetButtonColors(IconButton button, Color backcolor, Color forecolor)
        {
            button.BackColor = backcolor;
            button.ForeColor = forecolor;
            button.IconColor = forecolor;
        }
        private void ManegerForm_Load(object sender, EventArgs e)
        {
           

        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {

            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                this.WindowState |= FormWindowState.Maximized;
            }
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            IconButton activeButton = (IconButton)sender;
            SetButtonColors(activeButton, activeBackroundColor, activeForegroundColor);

            leftPanel1.Visible = true;

            SetButtonColors(iconButton2, defaultBackgroundColor, defaultForekgroundColor);
            SetButtonColors(iconButton1, defaultBackgroundColor, defaultForekgroundColor);

            leftPanel2.Visible = false;
            leftPanel1.Visible = false;
            // Создаем и открываем Form5
            Form5 form5 = new Form5();

            // Устанавливаем форму как безрамочную (если требуется)
            form5.FormBorderStyle = FormBorderStyle.None;
            form5.TopLevel = false; // Убираем верхний уровень
            form5.Dock = DockStyle.Fill; // Заполняем панель

            // Очищаем панель3 перед добавлением новой формы (если нужно)
            panel3.Controls.Clear();

            // Добавляем Form5 в панель3
            panel3.Controls.Add(form5);

            // Показываем форму
            form5.Show();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            IconButton activeButton = (IconButton)sender;
            SetButtonColors(activeButton, activeBackroundColor, activeForegroundColor);

            leftPanel2.Visible = true;

            SetButtonColors(iconButton1, defaultBackgroundColor, defaultForekgroundColor);
            SetButtonColors(iconButton2, defaultBackgroundColor, defaultForekgroundColor);

            leftPanel2.Visible = false;
            leftPanel1.Visible = false;

            // Создаем и открываем Form5
            Form6 form6 = new Form6();

            // Устанавливаем форму как безрамочную (если требуется)
            form6.FormBorderStyle = FormBorderStyle.None;
            form6.TopLevel = false; // Убираем верхний уровень
            form6.Dock = DockStyle.Fill; // Заполняем панель

            // Очищаем панель3 перед добавлением новой формы (если нужно)
            panel3.Controls.Clear();

            // Добавляем Form5 в панель3
            panel3.Controls.Add(form6);

            // Показываем форму
            form6.Show();
        }
    }
}
