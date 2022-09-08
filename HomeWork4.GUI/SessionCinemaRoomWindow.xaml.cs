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
    /// Логика взаимодействия для SessionCinemaRoomWindow.xaml
    /// </summary>
    public partial class SessionCinemaRoomWindow : Window
    {
        DateTime DateShow { get; set; }
        string SessionFilm { get; set; }
        string SessionName { get; set; }

        public SessionCinemaRoomWindow(DateTime dateShow,
                                        Film selectFilm,
                                        string sessionFilm, 
                                        string sessionName)
        {
            InitializeComponent();

            DateShow = dateShow;
            SessionFilm = sessionFilm;
            SessionName = sessionName;

            cinemaRoomsListBox.ItemsSource = selectFilm.Rooms;
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
            if (MessageBox.Show("Вы хотите добавить сеанс на фильм в этот зал?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                CinemaRoom selectCinemaRoom = (CinemaRoom)cinemaRoomsListBox.SelectedItem;

                Session newSession = new Session(SessionName, DateShow, selectCinemaRoom, SessionFilm,
                                                 selectCinemaRoom.Rows, selectCinemaRoom.Columns);
                Storage.sesssions.Add(newSession);

                AdminInterface adminInterface = new AdminInterface();
                adminInterface.Show();
                this.Close();
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
    }
}
