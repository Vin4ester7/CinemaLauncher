using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork4.Core
{
    public class User
    {
        int balance;
        string name;
        string surname;
        string login;
        string password;
        //List<Ticket> tickets = new List<Ticket>();

        [JsonProperty("UserBalance")]
        public int Balance
        {
            get
            {
                return balance;
            }
            set
            {
                balance += value;
            }
        }

        [JsonProperty("UserName")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        [JsonProperty("UserSurname")]
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }

        [JsonProperty("UserLogin")]
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
            }
        }

        [JsonProperty("UserPassword")]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public string ImageSource { get; set; }


        //public Ticket Ticket
        //{
        //    set { tickets.Add(value); }
        //}

        //public List<Ticket> Tickets
        //{
        //    get { return tickets; }
        //}

        public User(string name, string surname, int balance,
                    string login, string password, string imageSource) //List<Ticket> tickets
        {
            this.name = name;
            this.surname = surname;
            this.balance = balance;
            this.login = login;
            this.password = password;
            this.ImageSource = imageSource;
            //this.tickets = tickets;
        }

        public bool CheckEquals(User other)
        {
            return name == other.Name && surname == other.surname
                && balance == other.balance && login == other.login
                && password == other.password;
        }
    }

}
