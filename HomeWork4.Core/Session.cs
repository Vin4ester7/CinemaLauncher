using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork4.Core
{
    public class Session
    {
        private DateTime startDate;
        private CinemaRoom room;
        private string film;
        private string name;
        bool[,] orderedPlaces;

        [JsonProperty("SessionName")]
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

        [JsonProperty("SessionDate")]
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        [JsonProperty("SessionRoom")]
        public CinemaRoom Room
        {
            get { return room; }
            set { room = value; }
        }

        [JsonProperty("SessionFilm")]
        public string Film
        {
            get { return film; }
            set { film = value; }
        }

        [JsonProperty("SessionOrderedPlaces")]
        public bool[,] OrderedPlaces
        {
            get
            {
                return orderedPlaces;
            }
            set
            {
                orderedPlaces = value;
            }
        }

        public Session(string name, DateTime startDate, CinemaRoom room,
                       string film, int rows, int columns)
        {
            this.name = name;
            this.startDate = startDate;
            this.room = room;
            this.film = film;
            this.orderedPlaces = new bool[rows, columns];
        }

        public bool CheckEquals(CinemaRoom other)
        {
            return room == other;
        }
    }
}
