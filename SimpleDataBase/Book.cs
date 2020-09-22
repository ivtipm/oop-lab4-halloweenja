using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataBase
{
    class Gamer
    {
        string name;
        string surname;
        string login;
        string pass;
        string mail;
        ushort date;

        public Gamer(string name, string surname, string login, 
            string pass, string mail, ushort date)
        {
            this.name = name;
            this.surname = surname;
            this.login = login;
            this.pass = pass;
            this.mail = mail;
            this.date = date;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }

        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        public ushort Date
        {
            get { return date; }
            set { date = value; }
        }

        public override string ToString()
        {
            return name + "|" + surname + "|" + login + "|" +
                pass + "|" + mail + "|" + date;
        }
    }
}
