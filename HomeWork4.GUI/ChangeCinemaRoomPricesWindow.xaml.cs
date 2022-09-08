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
    /// Логика взаимодействия для ChangeCinemaRoomPricesWindow.xaml
    /// </summary>
    public partial class ChangeCinemaRoomPricesWindow : Window
    {
        CinemaRoom CinemaRoom { get; set; }

        public ChangeCinemaRoomPricesWindow(CinemaRoom cinemaRoom)
        {
            InitializeComponent();

            CinemaRoom = cinemaRoom;
            CreateCinemaRoom(cinemaRoom.Rows, cinemaRoom.Columns);
            CreateTextBox(cinemaRoom.Name, cinemaRoom.Rows, cinemaRoom.Columns, cinemaRoom.Type);
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

        private void CreateCinemaRoom(int rows, int columns)
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

        private void CreateTextBox(string name, int rows, int columns, string typeOfRoom)
        {
            int[,] ticketCost = CinemaRoom.TicketCost;

            var rowDefinition = new RowDefinition()
            {
                Height = new GridLength(40),
            };
            gridCinemaPlace.RowDefinitions.Add(rowDefinition);

            for (int countRows = 0; countRows < gridCinemaPlace.RowDefinitions.Count - 1; countRows++)
            {
                for (int countColumns = 0; countColumns < gridCinemaPlace.ColumnDefinitions.Count; countColumns++)
                {
                    TextBox cinemaPlace = new TextBox
                    {
                        Width = 80,
                        Height = 35,
                        Foreground = new SolidColorBrush(Colors.WhiteSmoke),
                        Background = new SolidColorBrush(Colors.Transparent),
                        BorderBrush = new SolidColorBrush(Colors.WhiteSmoke),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        TextAlignment = TextAlignment.Center,
                        FontSize = 20,
                        FontFamily = new FontFamily("Veranda")
                    };

                    cinemaPlace.Text = CinemaRoom.TicketCost[countRows, countColumns].ToString();
                    Grid.SetRow(cinemaPlace, countRows);
                    Grid.SetColumn(cinemaPlace, countColumns);
                    gridCinemaPlace.Children.Add(cinemaPlace);
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
                Content = "Подтвердить цены"
            };
            Grid.SetRow(acceptButton, gridCinemaPlace.RowDefinitions.Count);
            Grid.SetColumnSpan(acceptButton, gridCinemaPlace.ColumnDefinitions.Count);
            acceptButton.Click += acceptButton_Click;
            gridCinemaPlace.Children.Add(acceptButton);

            void acceptButton_Click(object sender, RoutedEventArgs e)
            {
                int count = 0;
                int result;
                bool flag = true;
                bool flag2 = true;

                for (int countRows = 0; countRows < gridCinemaPlace.RowDefinitions.Count - 1 && flag; countRows++)
                {
                    for (int countColumns = 0; countColumns < gridCinemaPlace.ColumnDefinitions.Count; countColumns++)
                    {
                        string fullInformationPlace = gridCinemaPlace.Children[count].ToString();
                        int priceLength = fullInformationPlace.Length - 33;
                        string price = fullInformationPlace.Substring(33, priceLength);

                        if (!int.TryParse(price, out result) || result == 0)
                        {
                            MessageBox.Show("Введенные данные некорректны.");
                            flag = false;
                            break;
                        }
                        else
                        {
                            ticketCost[countRows, countColumns] = result;
                        }
                        count++;
                    }
                }

                foreach (int i in ticketCost)
                {
                    if (i == 0)
                    {
                        flag2 = false;
                        break;
                    }
                }

                if (flag2)
                {
                    CinemaRoom.TicketCost = ticketCost;

                    if (MessageBox.Show("Цены сохранены", "Оповещение", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {
                        ChangeCinemaRoomWindow changeCinemaRoomWindow = new ChangeCinemaRoomWindow(CinemaRoom);
                        changeCinemaRoomWindow.Show();
                        this.Close();
                    }
                }
            }
        }
    }
}
