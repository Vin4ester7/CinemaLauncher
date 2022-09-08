using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork4.Core
{
    public class Storage
    {
        static string UserFilePath { get; set; }
        static string FilmFilePath { get; set; }
        static string RoomFilePath { get; set; }
        static string SessionFilePath { get; set; }
        static string TicketFilePath { get; set; }

        static public List<User> users = new List<User>();
        static public List<CinemaRoom> cinemaRooms = new List<CinemaRoom>();
        static public List<Film> films = new List<Film>();
        static public List<Session> sesssions = new List<Session>();
        static public List<Ticket> tickets = new List<Ticket>();

        public static void CreatePath() // Cоздает json файлы, если их нет
        {
            string fileUser = "Users";
            string fileFilm = "Films";
            string fileCinemaRoom = "CinemaRooms";
            string fileSession = "Sessions";
            string fileTicket = "Tickets";

            string filePath = Path.GetFullPath("../../Data/");
            UserFilePath = filePath + fileUser;
            FilmFilePath = filePath + fileFilm;
            RoomFilePath = filePath + fileCinemaRoom;
            SessionFilePath = filePath + fileSession;
            TicketFilePath = filePath + fileTicket;
            //FullFilePath = filePath + fileName;

            if (!File.Exists(UserFilePath))
            {
                var file = File.Create(UserFilePath);
                file.Close();
            }

            if (!File.Exists(FilmFilePath))
            {
                var file = File.Create(FilmFilePath);
                file.Close();
            }

            if (!File.Exists(RoomFilePath))
            {
                var file = File.Create(RoomFilePath);
                file.Close();
            }

            if (!File.Exists(SessionFilePath))
            {
                var file = File.Create(SessionFilePath);
                file.Close();
            }

            if (!File.Exists(TicketFilePath))
            {
                var file = File.Create(TicketFilePath);
                file.Close();
            }
        }

        public static void SaveInforamtion() // Сохраняет данные в нужные файлы
        {
            if (users.Count != 0)
            {
                string jsonUsers = JsonConvert.SerializeObject(users);
                File.WriteAllText(UserFilePath, jsonUsers);
            }

            if (cinemaRooms.Count != 0)
            {
                string jsonCinemaRooms = JsonConvert.SerializeObject(cinemaRooms);
                File.WriteAllText(RoomFilePath, jsonCinemaRooms);
            }

            if (films.Count != 0)
            {
                string jsonFilms = JsonConvert.SerializeObject(films);
                File.WriteAllText(FilmFilePath, jsonFilms);
            }

            if (tickets.Count != 0)
            {
                string jsonTickets = JsonConvert.SerializeObject(tickets);
                File.WriteAllText(TicketFilePath, jsonTickets);
            }

            if (sesssions.Count != 0)
            {
                string jsonSession = JsonConvert.SerializeObject(sesssions);
                File.WriteAllText(SessionFilePath, jsonSession);
            }
        }

        public static void ReadInformation() // Считывает информацию из нужных файлов
        {
            string infUsr = File.ReadAllText(UserFilePath);
            string infFilm = File.ReadAllText(FilmFilePath);
            string infRoom = File.ReadAllText(RoomFilePath);
            string infSess = File.ReadAllText(SessionFilePath);
            string infTicket = File.ReadAllText(TicketFilePath);

            if (infUsr != null)
            {
                List<User> usersInformation = JsonConvert.DeserializeObject<List<User>>(infUsr);
                if (usersInformation != null) { users = usersInformation; }
            }

            if (infFilm != null)
            {
                List<Film> filmsInformation = JsonConvert.DeserializeObject<List<Film>>(infFilm);
                if (filmsInformation != null) { films = filmsInformation; }
            }

            if (infRoom != null)
            {
                List<CinemaRoom> roomInforamtion = JsonConvert.DeserializeObject<List<CinemaRoom>>(infRoom);
                if (roomInforamtion != null) { cinemaRooms = roomInforamtion; }
            }

            if (infSess != null)
            {
                List<Session> sessionInformation = JsonConvert.DeserializeObject<List<Session>>(infSess);
                if (sessionInformation != null) { sesssions = sessionInformation; }
            }

            if (infTicket != null)
            {
                List<Ticket> ticketsInformation = JsonConvert.DeserializeObject<List<Ticket>>(infTicket);
                if (ticketsInformation != null) { tickets = ticketsInformation; }
            }
        }
    }
}
