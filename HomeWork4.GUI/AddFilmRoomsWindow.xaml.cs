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
    /// Логика взаимодействия для AddFilmRoomsWindow.xaml
    /// </summary>
    public partial class AddFilmRoomsWindow : Window
    {
        List<CinemaRoom> MayAddRooms { get; set; }
        Film Film { get; set; }

        public AddFilmRoomsWindow(Film film)
        {
            InitializeComponent();

            Film = film;

            CinemaRoom[] copiedCinemaRooms = new CinemaRoom[Storage.cinemaRooms.Count];
            Storage.cinemaRooms.CopyTo(copiedCinemaRooms);
            MayAddRooms = copiedCinemaRooms.ToList();

            SortCinemaRooms();

            cinemaRoomsListBox.ItemsSource = MayAddRooms;
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
            if (MessageBox.Show($"Вы уверены, что хотите показывать {Film.Name} в данном зале?",
                "Оповещение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                CinemaRoom selectCinemaRoom = (CinemaRoom)cinemaRoomsListBox.SelectedItem;
                cinemaRoomsListBox.ItemsSource = null;

                MayAddRooms.Remove(selectCinemaRoom);
                cinemaRoomsListBox.ItemsSource = MayAddRooms;
                Film.Rooms.Add(selectCinemaRoom);
            }
        }

        private void cinemRoomName_Initialized(object sender, EventArgs e)
        {
            TextBlock cinemaRoomName = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaRoomName.DataContext as CinemaRoom;
            cinemaRoomName.Text = $"{cinemaRoom.Name.ToUpper()}";
        }

        private void cinemRoomColumns_Initialized(object sender, EventArgs e)
        {
            TextBlock cinemaRoomColumns = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaRoomColumns.DataContext as CinemaRoom;
            cinemaRoomColumns.Text = $"Рядов: {cinemaRoom.Rows}";
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

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeFilmWindow changeFilmWindow = new ChangeFilmWindow(Film);
            changeFilmWindow.Show();
            this.Close();
        }

        private void SortCinemaRooms() // Удаляет залы в которых уже есть данный фильм
        {
            for (int count = 0; count < Film.Rooms.Count; count++)
            {
                foreach (var room in Storage.cinemaRooms)
                {
                    if (Film.Rooms[count] == room)
                    {
                        MayAddRooms.Remove(room);
                    }
                }
            }
        }
    }
}
