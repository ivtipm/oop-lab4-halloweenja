using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleDataBase
{
    public partial class AddForm : Form
    {
        Point newPoint;
        public AddForm()
        {
            InitializeComponent();

            nameTextBox.Text = "Введите Имя";
            surnameTextBox.Text = "Введите Фамилию";
            loginTextBox.Text = "Введите Логин";
            passTextBox.Text = "Введите Пароль";
            mailTextBox.Text = "Введите Маил";
            dateTextBox.Text = "Введите Год Рождения";
        }

        private void addGamerButton_MouseUp(object sender, MouseEventArgs e)
        {
            MainForm mForm = this.Owner as MainForm;
            try
            {
                string name = nameTextBox.Text;
                string surname = surnameTextBox.Text;
                string login = loginTextBox.Text;
                string pass = passTextBox.Text;
                string mail = mailTextBox.Text;
                ushort date = (ushort)Convert.ToUInt64(dateTextBox.Text);

                //uint date = (uint)Convert.ToUInt64(dateTextBox.Text);
                //ushort login = (ushort)Convert.ToUInt64(loginTextBox.Text);

                nameTextBox.Text = "";
                surnameTextBox.Text = "";
                loginTextBox.Text = "";
                passTextBox.Text = "";
                mailTextBox.Text = "";
                dateTextBox.Text = "";

                mForm.data.AddGamer(name, surname, login, pass, mail, date);
                //int n = mForm.data.Book.mail;
                mForm.dataGridViewTable.Rows.Add(name, surname, login, pass, mail, date);
                //mForm.BanChangeColumn(n - 1);
            }
            catch
            {
                MessageBox.Show("Некорректные данные!");
            }
        }

        private void AddForm_MouseDown(object sender, MouseEventArgs e)
        {
            newPoint = new Point(e.X, e.Y);
        }

        private void AddForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - newPoint.X;
                this.Top += e.Y - newPoint.Y;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            newPoint = new Point(e.X, e.Y);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - newPoint.X;
                this.Top += e.Y - newPoint.Y;
            }
        }

        private void CloseButton_MouseUp(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void CloseButton_MouseDown(object sender, MouseEventArgs e)
        {
            CloseButton.BackColor = Color.FromArgb(42, 48, 60);
        }

        private void nameTextBox_Enter(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "Введите Имя")
                nameTextBox.Text = "";
        }

        private void nameTextBox_Leave(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
                nameTextBox.Text = "Введите Имя";
        }

        private void surnameTextBox_Enter(object sender, EventArgs e)
        {
            if (surnameTextBox.Text == "Введите Фамилию")
                surnameTextBox.Text = "";
        }

        private void surnameTextBox_Leave(object sender, EventArgs e)
        {
            if (surnameTextBox.Text == "")
                surnameTextBox.Text = "Введите Фамилию";
        }

        private void loginTextBox_Enter(object sender, EventArgs e)
        {
            if (loginTextBox.Text == "Введите Логин")
                loginTextBox.Text = "";
        }

        private void loginTextBox_Leave(object sender, EventArgs e)
        {
            if (loginTextBox.Text == "")
                loginTextBox.Text = "Введите Логин";
        }

        private void passTextBox_Enter(object sender, EventArgs e)
        {
            if (passTextBox.Text == "Введите Пароль")
                passTextBox.Text = "";
        }

        private void passTextBox_Leave(object sender, EventArgs e)
        {
            if (passTextBox.Text == "")
                passTextBox.Text = "Введите Пароль";
        }

        private void mailTextBox_Enter(object sender, EventArgs e)
        {
            if (mailTextBox.Text == "Введите Маил")
                mailTextBox.Text = "";
        }

        private void mailTextBox_Leave(object sender, EventArgs e)
        {
            if (mailTextBox.Text == "")
                mailTextBox.Text = "Введите Маил";
        }

        private void dateTextBox_Enter(object sender, EventArgs e)
        {
            if (dateTextBox.Text == "Введите Год Рождения")
                dateTextBox.Text = "";
        }

        private void dateTextBox_Leave(object sender, EventArgs e)
        {
            if (dateTextBox.Text == "")
                dateTextBox.Text = "Введите Год Рождения";
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

