using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork4.Core
{
    public class Ticket
    {
        User user;
        int price;
        string filmName;
        DateTime session;
        int rows;
        int columns;

        [JsonProperty("TicketUser")]
        public User User
        {
            get { return user; }
            set { user = value; }
        }

        [JsonProperty("TicketPrice")]
        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        [JsonProperty("TicketFilm")]
        public string FilmName
        {
            get { return filmName; }
            set { filmName = value; }
        }

        [JsonProperty("TicketSession")]
        public DateTime Session
        {
            get { return session; }
            set { session = value; }
        }

        [JsonProperty("TikcetRow")]
        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }

        [JsonProperty("TicketColumn")]
        public int Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        public string TypeOfTicket { get; set; }

        public Ticket(User user, string filmName, DateTime session, int price,
                      int rows, int columns, string typeOfTicket)
        {
            this.user = user;
            this.filmName = filmName;
            this.session = session;
            this.price = price;
            this.rows = rows;
            this.columns = columns;
            TypeOfTicket = typeOfTicket;
        }
    }
}

//    public class GoldTicket : Ticket
//    {
//        string[] food;

//        [JsonProperty("BonusFood")]
//        public string[] Food { get; set; }

//        [JsonConstructor]
//        public GoldTicket(User user, string filmName, DateTime session, int price,
//                      int rows, int columns, string typeOfTicket, string[] food)
//            : base(user, filmName, session, price, rows, columns, typeOfTicket)
//        {
//            Food = food;
//        }
//    }

//    public class DiamondTicket : GoldTicket
//    {
//        Ticket bonusTicket;

//        [JsonProperty("BonusTicket")]
//        public Ticket BonusTicket { get; set; }

//        [JsonConstructor]
//        public DiamondTicket(User user, string filmName, DateTime session, int price,
//                      int rows, int columns, string typeOfTicket, string[] food,
//                      Ticket bonusTicket)
//            : base(user, filmName, session, price, rows, columns, typeOfTicket,
//                   food)
//        {
//            BonusTicket = bonusTicket;
//        }
//    }
//}

