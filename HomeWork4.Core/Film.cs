using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork4.Core
{
    public class Film
    {
        [JsonProperty]
        public static string[] ageRating = new string[] { "0+", "6+", "12+", "16+", "18+" };

        public List<CinemaRoom> rooms = new List<CinemaRoom>();
        string rating;
        string name;
        string company;
        string posterPath;

        [JsonProperty("FilmRating")]
        public string Rating
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value;
            }
        }

        [JsonProperty("FilmName")]
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

        [JsonProperty("FilmCompany")]
        public string Company
        {
            get
            {
                return company;
            }
            set
            {
                company = value;
            }
        }

        [JsonProperty("FilmPosterPath")]
        public string PosterPath
        {
            get { return posterPath; }
            set { posterPath = value; }
        }

        [JsonProperty("FilmRoom")]
        public List<CinemaRoom> Rooms
        {
            get
            {
                return rooms;
            }
            set
            {
                rooms = value;
            }
        }


        public Film(string name, string rating, string company, string posterPath,
                    List<CinemaRoom> rooms)
        {
            this.name = name;
            this.rating = rating;
            this.company = company;
            this.rooms = rooms;
            this.posterPath = posterPath;
        }

        //public void SetCinemaRooms(CinemaRoom room)
        //{
        //    myRoom = room;
        //}
    }
}
