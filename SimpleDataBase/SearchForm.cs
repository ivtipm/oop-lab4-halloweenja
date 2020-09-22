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
    public partial class SearchForm : Form
    {
        Point newPoint;
        public SearchForm()
        {
            InitializeComponent();

            enterNameOrLoginTextBox.Text = "Введите Имя или Логин";
        }

        private void searchButton_MouseUp(object sender, MouseEventArgs e)
        {
            MainForm frm = this.Owner as MainForm;

            if ((frm.data.Gamer.Count == 0) || (enterNameOrLoginTextBox.Text == ""))
                return;

            frm.dataGridViewTable.ClearSelection();
            List<int> foundElements = frm.data.SearchFile(enterNameOrLoginTextBox.Text);

            if (foundElements[0] == -1)
            {
                MessageBox.Show("Ничего не удалось найти!");
                return;
            }

            frm.dataGridViewTable.CurrentCell = frm.dataGridViewTable[0, foundElements[0]];

            for (int i = 0; i < foundElements.Count; i++)
            {
                frm.dataGridViewTable.Rows[foundElements[i]].Selected = true;
            }
        }

        private void CloseButton_MouseUp(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void SearchForm_MouseDown(object sender, MouseEventArgs e)
        {
            newPoint = new Point(e.X, e.Y);
        }

        private void SearchForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - newPoint.X;
                this.Top += e.Y - newPoint.Y;
            }
        }

        private void enterNameOrLoginTextBox_Enter(object sender, EventArgs e)
        {
            if (enterNameOrLoginTextBox.Text == "Введите Имя или Логин")
                enterNameOrLoginTextBox.Text = "";
        }

        private void enterNameOrLoginTextBox_Leave(object sender, EventArgs e)
        {
            if (enterNameOrLoginTextBox.Text == "")
                enterNameOrLoginTextBox.Text = "Введите Имя или Логин";
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

        private void CloseButton_MouseDown(object sender, MouseEventArgs e)
        {
            CloseButton.BackColor = Color.FromArgb(42, 48, 60);
        }

        private void enterNameOrLoginTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
