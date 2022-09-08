using HomeWork4.Core;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для BuyPlaceWindow.xaml
    /// </summary>
    public partial class BuyPlaceWindow : Window
    {
        CinemaRoom Room { get; set; }
        User CurrentUser { get; set; }
        Film SelectFilm { get; set; }
        CinemaRoom SelectRoom { get; set; }
        Session SelectSession { get; set; }
        List<Image> TakenSeats { get; set; }

        public BuyPlaceWindow(User user, Film film, CinemaRoom cinemaRoom, Session session)
        {
            InitializeComponent();

            Room = cinemaRoom;
            CurrentUser = user;
            SelectFilm = film;
            SelectRoom = cinemaRoom;
            SelectSession = session;

            List<Image> takenSeats = new List<Image>();
            TakenSeats = takenSeats;

            CreateCinemaRoom(Room.Rows, Room.Columns);
            CreateTextBox(Room.Rows, Room.Columns);
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

        private void CreateCinemaRoom(int rows, int columns) // Строит матрицу кинотеатра
        {
            int countRows = 1;
            int countColumns = 1;

            do
            {
                var rowDefiniton = new RowDefinition();
                gridCinemaPlace.RowDefinitions.Add(rowDefiniton);
                countRows++;
            }
            while (countRows <= rows);

            do
            {
                var columnDefinition = new ColumnDefinition();
                gridCinemaPlace.ColumnDefinitions.Add(columnDefinition);
                countColumns++;
            }
            while (countColumns <= columns);
        }

        private void CreateTextBox(int rows, int columns) // Наполняет потсроенную матрицу
        {
            var rowDefinition = new RowDefinition()
            {
                Height = new GridLength(40),
            };
            gridCinemaPlace.RowDefinitions.Add(rowDefinition);

            for (int countRows = 0; countRows < gridCinemaPlace.RowDefinitions.Count - 1; countRows++)
            {
                for (int countColumns = 0; countColumns < gridCinemaPlace.ColumnDefinitions.Count; countColumns++)
                {
                    Image cinemaSeat = new Image()
                    {
                        Width = 60,
                        Height = 60,
                        Stretch = Stretch.Fill,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    cinemaSeat.Initialized += seatImg_Initialized;
                    cinemaSeat.MouseLeftButtonDown += seatImg_MouseLeftButtonDown;
                    Grid.SetRow(cinemaSeat, countRows);
                    Grid.SetColumn(cinemaSeat, countColumns);
                    gridCinemaPlace.Children.Add(cinemaSeat);

                    TextBlock cinemaPlace = new TextBlock
                    {
                        Width = 80,
                        Height = 35,
                        Foreground = new SolidColorBrush(Colors.White),
                        Background = new SolidColorBrush(Colors.Transparent),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(0, 100, 0, 0),
                        TextAlignment = TextAlignment.Center,
                        FontSize = 18,
                        FontFamily = new FontFamily("Veranda"),
                        Text = $"{Room.TicketCost[countRows, countColumns]}₽"
                    };
                    Grid.SetRow(cinemaPlace, countRows);
                    Grid.SetColumn(cinemaPlace, countColumns);
                    gridCinemaPlace.Children.Add(cinemaPlace);

                    void seatImg_Initialized(object sender, EventArgs e) // Картинка сиденья
                    {
                        BitmapImage bitmapImage = new BitmapImage(); // Позвоялет работать с картинками

                        using (var fileStream = new FileStream("../../Pictures/cinemaSeat.png", FileMode.Open))
                        {
                            bitmapImage.BeginInit();
                            bitmapImage.StreamSource = fileStream;
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.EndInit();
                        }

                        cinemaSeat.Source = bitmapImage;
                    }

                    void seatImg_MouseLeftButtonDown(object sender1, MouseButtonEventArgs e1)
                    {
                        int price = Room.TicketCost[Grid.GetRow(cinemaSeat), Grid.GetColumn(cinemaSeat)];

                        if (!TakenSeats.Contains(cinemaSeat))
                        {
                            if (MessageBox.Show("Вы хотите купить данное место?", "Подтверждение",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                if (CurrentUser.Balance >= price)
                                {
                                    Ticket newTicket = new Ticket(CurrentUser, SelectFilm.Name,
                                                                  SelectSession.StartDate, price, Grid.GetRow(cinemaSeat),
                                                                  Grid.GetColumn(cinemaSeat), "Золотой");
                                    Storage.tickets.Add(newTicket);
                                    TakenSeats.Add(cinemaSeat);
                                    CurrentUser.Balance = -price;
                                    cinemaSeat.Source = TakenSeat();

                                    MessageBox.Show("Вы успешно купили билет.", "Оповещение");
                                }
                                else
                                {
                                    MessageBox.Show("На вашем балансе недостаточно средств. Пополните баланс.", "Оповещение");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Извините, но это место уже занято, выберите другое.","Оповещение");
                        }
                    }
                }
            }

            Button acceptButton = new Button
            {
                Width = 210,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 0, 10),
                Background = new SolidColorBrush(Colors.SeaGreen),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 22,
                FontFamily = new FontFamily("Veranda"),
                Content = "Подтверить покупку"
            };
            Grid.SetRow(acceptButton, gridCinemaPlace.RowDefinitions.Count);
            Grid.SetColumnSpan(acceptButton, gridCinemaPlace.ColumnDefinitions.Count);
            acceptButton.Click += acceptButton_Click;
            gridCinemaPlace.Children.Add(acceptButton);

            void acceptButton_Click(object sender, RoutedEventArgs e)
            {
                UserInterface userInterface = new UserInterface(CurrentUser);
                userInterface.Show();
                this.Close();
            }

            CheckTakenPalces();
        }

        BitmapImage TakenSeat() // Картинка занятого сиденья
        {
            BitmapImage bitmapImage = new BitmapImage(); // Позвоялет работать с картинками

            using (var fileStream = new FileStream("../../Pictures/takenSeat.png", FileMode.Open))
            {
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = fileStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }

        private void CheckTakenPalces() // Проверяет занятое место
        {
            int count = 0;

            for (int countRows = 0; countRows < gridCinemaPlace.RowDefinitions.Count - 1; countRows++)
            {
                for (int countColumns = 0; countColumns < gridCinemaPlace.ColumnDefinitions.Count; countColumns++)
                {
                    foreach (var ticket in Storage.tickets)
                    {
                        var checkingSeat = gridCinemaPlace.Children[count];

                        if (checkingSeat.GetType() == typeof(Image) && countRows == ticket.Rows &&
                            countColumns == ticket.Columns && SelectSession.StartDate == ticket.Session)
                        {
                            ((Image)checkingSeat).Source = TakenSeat();
                        }
                    }
                    count = count + 2;
                }
            }
        }
    }
}
