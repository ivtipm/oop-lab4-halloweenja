using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleDataBase
{
    public class GamersData
    {
        ArrayList gamer = new ArrayList();

        /// <summary>
        /// Вернуть список
        /// </summary>
        public ArrayList Gamer
        {
            get { return gamer; }
        }

        /// <summary>
        /// Добавление игрока в список
        /// </summary>
        public void AddGamer(string name, string surname, string login,
            string pass, string mail, ushort date)
        {
            Gamer newGamer = new Gamer(name, surname, login, pass, mail, date);
            gamer.Add(newGamer);
        }

        /// <summary>
        /// Удаление всего списка игроков
        /// </summary>
        public void DeleteGamers() => gamer.Clear();

        /// <summary>
        /// Удаление игрока из списка по индексу
        /// </summary>
        public void DeleteGamer(int index) => gamer.RemoveAt(index);

        /// <summary>
        /// Изменить имени игрока по индексу
        /// </summary>
        public void ChangeName(string name, int index)
        {
            Gamer gm = (Gamer)gamer[index];
            gm.Name = name;
        }

        /// <summary>
        /// Изменить фамилии игрока по индексу
        /// </summary>
        public void ChangeSurname(string surname, int index)
        {
            Gamer gm = (Gamer)gamer[index];
            gm.Surname = surname;
        }

        /// <summary>
        /// Изменить год издания книги по индексу
        /// </summary>
        public void ChangeLogin(string login, int index)
        {
            Gamer gm = (Gamer)gamer[index];
            gm.Login = login;
        }

        /// <summary>
        /// Изменить пароль игрока по индексу
        /// </summary>
        public void ChangePass(string pass, int index)
        {
            Gamer gm = (Gamer)gamer[index];
            gm.Pass = pass;
        }

        /// <summary>
        /// Длбавить геймера
        /// </summary>
        internal void AddGamer(string name, string surname, string login, string pass, string mail, uint date)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Изменить маил игрока по индексу
        /// </summary>
        public void ChangeMail(string mail, int index)
        {
            Gamer gm = (Gamer)gamer[index];
            gm.Mail = mail;
        }

        /// <summary>
        /// Изменить год рождения игрока по индексу
        /// </summary>
        public void ChangeDate(ushort date, int index)
        {
            Gamer gm = (Gamer)gamer[index];
            gm.Date = date;
        }

        /// <summary>
        /// Сохранение списка в файл
        /// </summary>
        public void SaveToFile(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename, false, System.Text.Encoding.Unicode))
            {
                foreach (Gamer s in gamer)
                {
                    sw.WriteLine(s.ToString());
                }
            }
        }

        /// <summary>
        /// Открывает файл, восстанавливая список
        /// </summary>
        public void OpenFile(string filename)
        {
            if (!System.IO.File.Exists(filename))
                throw new Exception("Файл не существует");

            if (gamer.Count != 0)
                DeleteGamers();

            using (StreamReader sw = new StreamReader(filename))
            {
                while (!sw.EndOfStream)
                {
                    string str = sw.ReadLine();
                    String[] dataFromFile = str.Split(new String[] { "|" },
                        StringSplitOptions.RemoveEmptyEntries);

                    string name = dataFromFile[0];
                    string surname = dataFromFile[1];
                    string login = dataFromFile[2];
                    string pass = dataFromFile[3];
                    string mail = dataFromFile[4];
                    ushort date = (ushort)Convert.ToUInt64(dataFromFile[5]);

                    AddGamer(name, surname, login, pass, mail, date);
                }
            }
        }

        /// <summary>
        /// Поиск по имени или логину игрока 
        /// Вернёт индексы найденных элементов
        /// </summary>
        public List<int> SearchFile(string query)
        {
            List<int> cnt = new List<int>();

            query = query.ToLower(); // перевод в нижний регистр
            query = query.Replace(" ", "");

            // Поиск
            for (int i = 0; i < gamer.Count; i++)
            {
                Gamer gm = (Gamer)gamer[i];

                if (gm.Name.ToLower().Replace(" ", "").Contains(query))
                {
                    cnt.Add(i);
                    break; 
                }
                else if (gm.Login.ToLower().Replace(" ", "").Contains(query))
                {
                    cnt.Add(i);
                    break;
                }
            }

            if (cnt.Count == 0) cnt.Add(-1);
            return cnt;
        }

        /// <summary>
        /// Сортировка коллекции
        /// </summary>
        public void Sort(SortDirection direction)
        {
            gamer.Sort(new AuthorComparer(direction));
        }
    }

    /// <summary>
    /// Сортировка коллекции
    /// </summary>
    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public class AuthorComparer : IComparer
    {
        private SortDirection m_direction = SortDirection.Ascending;

        public AuthorComparer() : base() { }

        public AuthorComparer(SortDirection direction)
        {
            this.m_direction = direction;
        }

        int IComparer.Compare(object x, object y)
        {
            Gamer gamer1 = (Gamer)x;
            Gamer gamer2 = (Gamer)y;

            return (this.m_direction == SortDirection.Ascending) ?
                gamer1.Name.CompareTo(gamer2.Name) :
                gamer2.Name.CompareTo(gamer1.Name);
        }
    }
}
