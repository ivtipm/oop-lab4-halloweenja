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
    public partial class MainForm : Form
    {
        Point newPoint;
        public GamersData data = new GamersData();
        public string oldValue = "";
        public string filename = "";

        public MainForm()
        {
            InitializeComponent();

            dataGridViewTable.Rows[0].ReadOnly = true;
            dataGridViewTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            InitializeTimers();

            
        }

        private void InitializeTimers()
        {
            // Таймер для ручного сохранения
            saveTimer.Interval = 3000;
            saveTimer.Tick += new EventHandler(saveTimer_Tick);

            // Таймер для автосохранения
            autoSaveTimer.Interval = 60000;
            autoSaveTimer.Tick += new EventHandler(autoSaveTimer_Tick);
        }

        private void saveTimer_Tick(object sender, EventArgs e)
        {
            saveTimer.Enabled = false;
            saveTimer.Stop();
        }

        private void autoSaveTimer_Tick(object sender, EventArgs e)
        {
            data.SaveToFile(filename);
            saveTimer.Enabled = true;
            saveTimer.Start();
        }

        // Запретить редактировать столбец по указанному индексу
        public void BanChangeColumn(int index) =>
            dataGridViewTable.Rows[index].Cells[0].ReadOnly = true;

        private void WriteToDataGrid()
        {
            for (int i = 0; i < data.Gamer.Count; i++)
            {
                Gamer b = (Gamer)data.Gamer[i];
                dataGridViewTable.Rows.Add(b.Name, b.Surname, b.Login, b.Pass, b.Mail, b.Date);
                BanChangeColumn(i);
            }
            // Запрещаем редактировать последнюю строку
            dataGridViewTable.Rows[data.Gamer.Count].ReadOnly = true;
        }

        private void dataGridViewTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int indRow = dataGridViewTable.Rows[e.RowIndex].Index;
            int indColumn = dataGridViewTable.Columns[e.ColumnIndex].Index;
            object value = dataGridViewTable.Rows[indRow].Cells[indColumn].Value;

            // Если значение не было введено, то оставляем старое
            if (value is null)
            {
                MessageBox.Show("Вы не ввели новое значение!", "Ошибка");
                dataGridViewTable.Rows[indRow].Cells[indColumn].Value = oldValue;
                return;
            }

            if (indColumn == 0)
                data.ChangeName((string)value, indRow);

            else if (indColumn == 1)
                data.ChangeSurname((string)value, indRow);

            else if (indColumn == 2)
                data.ChangeLogin((string)value, indRow);

            else if (indColumn == 3)
                data.ChangePass((string)value, indRow);

            else if (indColumn == 4)
                data.ChangeMail((string)value, indRow);

            else if (indColumn == 5)
                data.ChangeDate((ushort)Convert.ToUInt64(value), indRow);
        }

        private void dataGridViewTable_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                oldValue = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((data.Gamer.Count != 0) || (filename != ""))
            {
                DialogResult dialogResult = MessageBox.Show("Вы уверены, " +
                    "что хотите создать новый файл?", "Подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    this.Text = "База данных игроков";
                    filename = "";
                    data.DeleteGamers();
                    dataGridViewTable.Rows.Clear();
                    saveTimer.Stop();
                    saveTimer.Enabled = false;
                    autoSaveTimer.Stop();
                    autoSaveTimer.Enabled = false;
                }
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;

            filename = openFileDialog.FileName;
            this.Text = filename + " - База данных игроков";

            dataGridViewTable.Rows.Clear();
            data.OpenFile(filename);

            WriteToDataGrid();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filename == "")
            {
                if (saveFileDialog.ShowDialog() == DialogResult.Cancel) return;
                filename = saveFileDialog.FileName;
                this.Text = filename + " - База данных игроков";
            }

            if (!autoSaveTimer.Enabled)
            {
                autoSaveTimer.Enabled = true;
                autoSaveTimer.Start();
            }

            saveTimer.Enabled = true;
            saveTimer.Start();

            data.SaveToFile(filename);
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Info = "База данных созданная дляхраниния информации об играках\nВерсия 1.0\n\n" +
                "Документация доступна в папке проекта в формате markdown\n\n" +
                "Create by Mikhail Khatkov © 2020";
            MessageBox.Show(Info, "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void оРазработчикеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Info = "Разработчик: Хатьков.М.С, студент группы ИВТ-18-2\n\n" +
                "GitHub: halloweenja \n";
            MessageBox.Show(Info, "О разработчике", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void addButton_MouseUp(object sender, MouseEventArgs e)
        {
            AddForm addform = new AddForm();
            addform.Owner = this;
            addform.Show();
            addButton.BackColor = Color.FromArgb(32, 33, 40);
        }

        private void addButton_MouseDown(object sender, MouseEventArgs e)
        {
            addButton.BackColor = Color.FromArgb(54, 58, 77);
        }

        private void searchButton_MouseUp(object sender, MouseEventArgs e)
        {
            SearchForm sf = new SearchForm();
            sf.Owner = this;
            sf.Show();
            searchButton.BackColor = Color.FromArgb(32, 33, 40);
        }

        private void searchButton_MouseDown(object sender, MouseEventArgs e)
        {
            searchButton.BackColor = Color.FromArgb(54, 58, 77);
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (data.Gamer.Count != 0)
            {
                dataGridViewTable.Rows.Clear();
                data.Sort(SortDirection.Ascending);
                WriteToDataGrid();
            }
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            if (data.Gamer.Count != 0)
            {
                dataGridViewTable.Rows.Clear();
                data.Sort(SortDirection.Descending);
                WriteToDataGrid();
            }
        }

        private void deleteButton1_MouseUp(object sender, MouseEventArgs e)
        {
            int count = dataGridViewTable.Rows.Count;
            foreach (DataGridViewRow row in dataGridViewTable.SelectedRows)
            {
                int index = row.Index;
                if (index == count - 1) return;
                data.DeleteGamer(index);
                dataGridViewTable.Rows.RemoveAt(index);
            }
        }

        private void deleteAllButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (data.Gamer.Count != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Вы уверены, " +
                    "что хотите стереть все данные?\nОтменить это действие будет невозможно!",
                    "Подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    data.DeleteGamers();
                    dataGridViewTable.Rows.Clear();
                }
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            newPoint = new Point(e.X, e.Y);
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
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

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            newPoint = new Point(e.X, e.Y);
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - newPoint.X;
                this.Top += e.Y - newPoint.Y;
            }
        }

        private void addButton_MouseEnter(object sender, EventArgs e)
        {
            addButton.BackColor = Color.FromArgb(42, 48, 60);
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.BackColor = Color.FromArgb(42, 48, 60);
        }

        private void searchButton_MouseEnter(object sender, EventArgs e)
        {
            searchButton.BackColor = Color.FromArgb(42, 48, 60);
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(42, 48, 60);
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.FromArgb(42, 48, 60);
        }

        private void deleteButton1_MouseEnter(object sender, EventArgs e)
        {
            deleteButton1.BackColor = Color.FromArgb(42, 48, 60);
        }

        private void deleteAllButton_MouseEnter(object sender, EventArgs e)
        {
            deleteAllButton.BackColor = Color.FromArgb(42, 48, 60);
        }

        private void addButton_MouseLeave(object sender, EventArgs e)
        {
            addButton.BackColor = Color.FromArgb(32, 33, 40);
        }

        private void searchButton_MouseLeave(object sender, EventArgs e)
        {
            searchButton.BackColor = Color.FromArgb(32, 33, 40);
        }

        private void deleteButton1_MouseLeave(object sender, EventArgs e)
        {
            deleteButton1.BackColor = Color.FromArgb(32, 33, 40);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(32, 33, 40);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.FromArgb(32, 33, 40);
        }

        private void deleteAllButton_MouseLeave(object sender, EventArgs e)
        {
            deleteAllButton.BackColor = Color.FromArgb(32, 33, 40);
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.BackColor = Color.FromArgb(54, 58, 77);
        }
    }
}