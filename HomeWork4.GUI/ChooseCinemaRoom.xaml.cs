using HomeWork4.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HomeWork4.GUI
{
    /// <summary>
    /// Логика взаимодействия для ChooseCinemaRoom.xaml
    /// </summary>
    public partial class ChooseCinemaRoom : Window
    {
        string Name { get; set; }
        string Rating { get; set; }
        string Company { get; set; }
        string PosterPath { get; set; }
        List<CinemaRoom> CinemaRooms { get; set; }
        List<CinemaRoom> FilmRooms { get; set; }

        public ChooseCinemaRoom(string name, string rating, string company, string posterPath)
        {
            InitializeComponent();

            List<CinemaRoom> filmRooms = new List<CinemaRoom>();
            cinemaRoomsListBox.ItemsSource = Storage.cinemaRooms;

            CinemaRoom[] cinemaRooms = new CinemaRoom[Storage.cinemaRooms.Count];
            Storage.cinemaRooms.CopyTo(cinemaRooms);
            CinemaRooms = cinemaRooms.ToList();

            FilmRooms = filmRooms;
            Name = name;
            Rating = rating;
            Company = company;
            PosterPath = posterPath;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Storage.SaveInforamtion();
            this.Close();
        }

        private void turnButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void cinemaRoomsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Вы хотите показывать фильм в этом зале?", "Подтверждение", 
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                CinemaRoom selectCinemaRoom = (CinemaRoom)cinemaRoomsListBox.SelectedItem;
                cinemaRoomsListBox.ItemsSource = null;

                CinemaRooms.Remove(selectCinemaRoom);
                cinemaRoomsListBox.ItemsSource = CinemaRooms;
                FilmRooms.Add(selectCinemaRoom);

            }
        }

        private void cinemRoomName_Initialized(object sender, EventArgs e)
        {
            TextBlock cinemaRoomName = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaRoomName.DataContext as CinemaRoom;
            cinemaRoomName.Text = $"{cinemaRoom.Name.ToUpper()}";
        }

        private void cinemRoomSeatsOnRow_Initialized(object sender, EventArgs e)
        {
            TextBlock cinemaSeatsOnRow = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaSeatsOnRow.DataContext as CinemaRoom;
            cinemaSeatsOnRow.Text = $"Мест в ряде: {cinemaRoom.Columns}";
        }

        private void cinemRoomAllSeats_Initialized(object sender, EventArgs e)
        {
            TextBlock cinemaRoomAllSeats = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaRoomAllSeats.DataContext as CinemaRoom;
            int allSeats = cinemaRoom.Rows * cinemaRoom.Columns;
            cinemaRoomAllSeats.Text = $"Всего мест: {allSeats}";
        }

        private void cinemRoomType_Initialized(object sender, EventArgs e)
        {
            TextBlock cinemaRoomType = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaRoomType.DataContext as CinemaRoom;
            cinemaRoomType.Text = $"Тип кинозала: {cinemaRoom.Type}";
        }

        private void cinemRoomColumns_Initialized(object sender, EventArgs e)
        {
            TextBlock cinemaRoomColumns = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaRoomColumns.DataContext as CinemaRoom;
            cinemaRoomColumns.Text = $"Рядов: {cinemaRoom.Rows}";
        }

        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            Film newFilm = new Film(Name, Rating, Company, PosterPath, FilmRooms);
            Storage.films.Add(newFilm);

            AdminInterface adminInterface = new AdminInterface();
            adminInterface.Show();
            this.Close();
        }
    }
}
