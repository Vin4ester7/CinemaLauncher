using HomeWork4.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для UserInterface.xaml
    /// </summary>
    public partial class UserInterface : Window
    {
        string ShowPassword { get; set; }
        User User { get; set; }
        Film SelectFilm { get; set; }
        CinemaRoom SelectCinemaRoom { get; set; }
        List<Session> FilmsSession { get; set; }
        List<Ticket> UserTickets { get; set; }

        public UserInterface(User user)
        {
            InitializeComponent();
            User = user;

            SetText(user);

            if (User.Login != user.Login || User.Name != user.Name ||
                User.Surname != user.Surname || User.Password != user.Password)
            {
                user = User;
            }

            filmListBox.ItemsSource = Storage.films;

            List<Ticket> userTickets = new List<Ticket>();
            UserTickets = userTickets;
            SortTickets();
            ticketsListBox.ItemsSource = UserTickets;

            List<Session> filmSession = new List<Session>();
            FilmsSession = filmSession;
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

                ///                      НАЧАЛО: Клики и события для панели с лева                     ///
        private void account_MouseDoubleClick(object sender, MouseButtonEventArgs e) // Создает в рабочем поле всю информацию
                                                                                     // о пользователе и позваоляет ее редактировать
        {
            gridForElements.Children.Clear();
            gridBuyTicketGrid.Visibility = Visibility.Hidden;
            gridBoughtTickets.Visibility = Visibility.Hidden;

            Image accountImg = new Image()
            {
                Width = 200,
                Height = 200,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(30, 20, 0, 0),
                Stretch = Stretch.Fill,
            };
            accountImg.Initialized += accountImg_Initialized;
            accountImg.MouseLeftButtonDown += accountAvatar_MouseLeftButtonDown;
            gridForElements.Children.Add(accountImg);

            TextBlock textBlockName = new TextBlock()
            {
                Width = 60,
                Height = 30,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(240, 20, 0, 0),
                Text = "Имя:",
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20
            };
            gridForElements.Children.Add(textBlockName);

            TextBlock textBlockSurname = new TextBlock()
            {
                Width = 95,
                Height = 30,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(240, 50, 0, 0),
                Text = "Фамилия:",
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20
            };
            gridForElements.Children.Add(textBlockSurname);

            TextBlock textBlockLogin = new TextBlock()
            {
                Width = 65,
                Height = 30,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(240, 80, 0, 0),
                Text = "Логин:",
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20
            };
            gridForElements.Children.Add(textBlockLogin);

            TextBlock textBlockPassword = new TextBlock()
            {
                Width = 80,
                Height = 30,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(240, 110, 0, 0),
                Text = "Пароль:",
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20
            };
            gridForElements.Children.Add(textBlockPassword);

            TextBlock textBlockFirstBalance = new TextBlock()
            {
                Width = 80,
                Height = 30,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(240, 140, 0, 0),
                Text = "Баланс:",
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20
            };
            gridForElements.Children.Add(textBlockFirstBalance);

            TextBlock textBlockSecondBalance = new TextBlock()
            {
                Width = 100,
                Height = 30,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(315, 140, 0, 0),
                Text = $"{User.Balance}₽",
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20
            };
            gridForElements.Children.Add(textBlockSecondBalance);

            TextBox textBoxName = new TextBox()
            {
                Width = 400,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(95, 18, 0, 0),
                Background = new SolidColorBrush(Colors.Transparent),
                BorderBrush = new SolidColorBrush(Colors.Transparent),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20,
                Text = User.Name
            };
            gridForElements.Children.Add(textBoxName);

            TextBox textBoxSurname = new TextBox()
            {
                Width = 400,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(180, 48, 0, 0),
                Background = new SolidColorBrush(Colors.Transparent),
                BorderBrush = new SolidColorBrush(Colors.Transparent),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20,
                Text = User.Surname
            };
            gridForElements.Children.Add(textBoxSurname);

            TextBox textBoxLogin = new TextBox()
            {
                Width = 400,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(135, 78, 0, 0),
                Background = new SolidColorBrush(Colors.Transparent),
                BorderBrush = new SolidColorBrush(Colors.Transparent),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20,
                Text = User.Login
            };
            gridForElements.Children.Add(textBoxLogin);

            TextBox textBoxPassword = new TextBox()
            {
                Width = 400,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(160, 108, 0, 0),
                Background = new SolidColorBrush(Colors.Transparent),
                BorderBrush = new SolidColorBrush(Colors.Transparent),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20,
                Text = ShowPassword
            };
            textBoxPassword.MouseEnter += textBoxPassword_MouseEnter;
            textBoxPassword.MouseLeave += textBoxPassword_MouseLeave;
            gridForElements.Children.Add(textBoxPassword);

            Button saveButton = new Button()
            {
                Width = 195,
                Height = 30,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(240, 0, 0, 170),
                Content = "Сохранить изменения",
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Colors.SeaGreen),
                FontSize = 18
            };
            saveButton.Click += saveButton_Click;
            gridForElements.Children.Add(saveButton);

            Button reverseButton = new Button()
            {
                Width = 160,
                Height = 30,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 0, 270, 170),
                Content = "Вернуть обратно",
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Colors.SeaGreen),
                FontSize = 18
            };
            reverseButton.Click += reverseButton_Click;
            gridForElements.Children.Add(reverseButton);


            void accountAvatar_MouseLeftButtonDown(object sender1, MouseButtonEventArgs e1)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Image files (*.jpeg;*.jpg;*.png)|*.jpeg;*.jpg;*.png";

                if (fileDialog.ShowDialog() == true)
                {
                    string avatarPath = System.IO.Path.GetFullPath(fileDialog.FileName);
                    User.ImageSource = avatarPath;
                }
            }

            void accountImg_Initialized(object sender1, EventArgs e1)
            {

                BitmapImage bitmapImage = new BitmapImage(); // Позвоялет работать с картинками

                using (var fileStream = new FileStream(User.ImageSource, FileMode.Open))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = fileStream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                }

                accountImg.Source = bitmapImage;
            }

            void textBoxPassword_MouseEnter(object sender1, MouseEventArgs e1)
            {
                if (textBoxPassword.Text == ShowPassword)
                {
                    textBoxPassword.Text = User.Password;
                }
            }

            void textBoxPassword_MouseLeave(object sender1, MouseEventArgs e1)
            {
                if (textBoxPassword.Text == User.Password)
                {
                    textBoxPassword.Text = ShowPassword;
                }
            }

            void saveButton_Click(object sender1, RoutedEventArgs e1)
            {
                if (textBoxName.Text != User.Name)
                {
                    User.Name = textBoxName.Text;
                }
                if (textBoxSurname.Text != User.Surname)
                {
                    User.Surname = textBoxSurname.Text;
                }
                if (textBoxLogin.Text != User.Login)
                {
                    User.Login = textBoxLogin.Text;
                }
                if (textBoxPassword.Text != User.Password && textBoxPassword.Text != ShowPassword)
                {
                    User.Password = textBoxPassword.Text;
                }
                SetText(User);
            }

            void reverseButton_Click(object sender1, RoutedEventArgs e1)
            {
                if (textBoxPassword.Text != ShowPassword || textBoxPassword.Text != User.Password ||
                    textBoxName.Text != User.Name || textBoxSurname.Text != User.Surname ||
                    textBoxLogin.Text != User.Login)
                {
                    textBoxLogin.Text = User.Login;
                    textBoxName.Text = User.Name;
                    textBoxSurname.Text = User.Surname;
                    textBoxPassword.Text = ShowPassword;
                }
            }
        }

        private void balance_MouseDoubleClick(object sender, MouseButtonEventArgs e) // Создает в рабочем поле текс с
                                                                                     // балансом и кнопку для пополнения
        {
            gridForElements.Children.Clear();
            gridBuyTicketGrid.Visibility = Visibility.Hidden;
            gridBoughtTickets.Visibility = Visibility.Hidden;

            TextBlock checkBalance = new TextBlock()
            {
                Width = 500,
                Height = 40,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 170, 0, 0),
                Text = "ВАШ БАЛАНС СОСТАВЛЯЕТ:",
                Foreground = new SolidColorBrush(Colors.White),
                TextAlignment = TextAlignment.Center,
                FontFamily = new FontFamily("Veranda"),
                FontSize = 30
            };
            gridForElements.Children.Add(checkBalance);

            TextBlock showBalance = new TextBlock()
            {
                Width = 200,
                Height = 40,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 215, 0, 0),
                Text = $"{User.Balance}₽",
                Foreground = new SolidColorBrush(Colors.White),
                TextAlignment = TextAlignment.Center,
                FontFamily = new FontFamily("Veranda"),
                FontSize = 35
            };
            showBalance.Width = MeasureWidth(showBalance.Text, showBalance).Width;
            gridForElements.Children.Add(showBalance);

            Button depositButton = new Button()
            {
                Width = 185,
                Height = 35,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 20, 0, 0),
                Content = "Пополнить",
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Colors.SeaGreen),
                FontFamily = new FontFamily("Veranda"),
                FontSize = 25
            };
            depositButton.Click += depositButton_Click;
            gridForElements.Children.Add(depositButton);

            void depositButton_Click(object sender1, RoutedEventArgs e1)
            {
                DepositWindow depositWindow = new DepositWindow(User);
                depositWindow.Show();
                this.Close();
            }
        }

        private void exit_MouseDoubleClick(object sender, MouseButtonEventArgs e) // Создает в рабочем поле кнопки
                                                                                  // для выхода из интерфейса пользователя
        {
            gridForElements.Children.Clear();
            gridBuyTicketGrid.Visibility = Visibility.Hidden;
            gridBoughtTickets.Visibility = Visibility.Hidden;

            TextBlock askBlock = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 700,
                Height = 60,
                Margin = new Thickness(0, 150, 0, 0),
                Background = new SolidColorBrush(Colors.Transparent),
                Foreground = new SolidColorBrush(Colors.White),
                Text = "Вы точно хотите выйти?",
                TextAlignment = TextAlignment.Center,
                FontSize = 40,
                FontFamily = new FontFamily("Veranda")
            };
            gridForElements.Children.Add(askBlock);

            Button buttonYes = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 200,
                Height = 40,
                Margin = new Thickness(210, 0, 0, 60),
                Background = new SolidColorBrush(Colors.Gray),
                Foreground = new SolidColorBrush(Colors.WhiteSmoke),
                Content = "Да",
                FontSize = 30,
                FontFamily = new FontFamily("Veranda")
            };
            buttonYes.Click += buttonYes_Click;
            gridForElements.Children.Add(buttonYes);

            Button buttonNo = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 200,
                Height = 40,
                Margin = new Thickness(0, 0, 220, 60),
                Background = new SolidColorBrush(Colors.Gray),
                Foreground = new SolidColorBrush(Colors.WhiteSmoke),
                Content = "Нет",
                FontSize = 30,
                FontFamily = new FontFamily("Veranda")
            };
            buttonNo.Click += buttonNo_Click;
            gridForElements.Children.Add(buttonNo);

            void buttonYes_Click(object sender1, RoutedEventArgs e1)
            {
                Storage.SaveInforamtion();

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }

            void buttonNo_Click(object sender1, RoutedEventArgs e1)
            {
                gridForElements.Children.Clear();
            }
        }

        private void buyTicket_MouseDoubleClick(object sender, MouseButtonEventArgs e) // Проверяет наличие всех элементов
                                                                                       // и октрывает видимость списка фильмов
        {
            gridForElements.Children.Clear();
            gridBoughtTickets.Visibility = Visibility.Hidden;

            if (Storage.cinemaRooms.Count > 0)
            {
                if (Storage.films.Count > 0)
                {
                    if (Storage.sesssions.Count > 0)
                    {
                        gridBuyTicketGrid.Visibility = Visibility.Visible;
                        filmListBox.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MessageBox.Show("Сеансов нет. Приходите позже.");
                    }
                }
                else
                {
                    MessageBox.Show("Фильмов нет. Санкции, сами понимаете(", "Оповещение");
                }
            }
            else
            {
                MessageBox.Show("Простите, тут даже кинозалы не построены. Скоро все будет!", "Оповещение");
            }
        }

        private void boughtTickets_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            gridForElements.Children.Clear();
            gridBuyTicketGrid.Visibility = Visibility.Hidden;

            if (Storage.tickets.Count > 0)
            {
                gridBoughtTickets.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Вы еще не покупали билеты у нас((", "Оповещение");
            }
        }
        ///                      КОНЕЦ: Клики и события для панели с лева                       ///


        ///         НАЧАЛО: Обработка событий и сами события для центрального окна              ///
                      //       ПОКАЗ СПИСКА ФИЛЬМОВ ПРИ ПОКУПКЕ БИЛЕТОВ        //
        private void filmListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) // Клик для выбора фильма
        {
            if (MessageBox.Show("Вы хотите купить билет на именно этот фильм?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Film selectedFilm = (Film)filmListBox.SelectedItem;

                SelectFilm = selectedFilm;
                cinemaRoomsListBox.ItemsSource = SelectFilm.Rooms;

                filmListBox.Visibility = Visibility.Hidden;
                cinemaRoomsListBox.Visibility = Visibility.Visible;
            }
        }

        private void filmPoster_Initialized(object sender, EventArgs e) // Постер для фильма
        {
            Image filmPoster = sender as Image;
            Film film = filmPoster.DataContext as Film;

            BitmapImage bitmapImage = new BitmapImage(); // Позвоялет работать с картинками

            using (var fileStream = new FileStream(film.PosterPath, FileMode.Open))
            {
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = fileStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            filmPoster.Source = bitmapImage;
        }

        private void filmRatings_Initialized(object sender, EventArgs e) // Рейтинг фильма
        {
            TextBlock filmRating = sender as TextBlock;
            Film film = filmRating.DataContext as Film;
            filmRating.Text = film.Rating;
        }

        private void filmCompany_Initialized(object sender, EventArgs e) // Компания фильма
        {
            TextBlock filmCompany = sender as TextBlock;
            Film film = filmCompany.DataContext as Film;
            filmCompany.Text = film.Company;
        }

        private void filmName_Initialized(object sender, EventArgs e) // Название фильма
        {
            TextBlock filmName = sender as TextBlock;
            Film film = filmName.DataContext as Film;
            filmName.Text = film.Name;
        }

                      //       ПОКАЗ СПИСКА ЗАЛОВ ПРИ ПОКУПКЕ БИЛЕТОВ        //
        private void cinemaRoomsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) // Клик для выбора зала
        {
            if (MessageBox.Show("Вы хотите cмотреть фильм в этом зале?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                CinemaRoom selectCinemaRoom = (CinemaRoom)cinemaRoomsListBox.SelectedItem;

                SelectCinemaRoom = selectCinemaRoom;
                SortSession();
                sessionsListBox.ItemsSource = FilmsSession;
                cinemaRoomsListBox.Visibility = Visibility.Hidden;
                sessionsListBox.Visibility = Visibility.Visible;
            }
        }

        private void cinemRoomName_Initialized(object sender, EventArgs e) // Показ имени залу в ListBox
        {
            TextBlock cinemaRoomName = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaRoomName.DataContext as CinemaRoom;
            cinemaRoomName.Text = $"{cinemaRoom.Name.ToUpper()}";
        }

        private void cinemRoomColumns_Initialized(object sender, EventArgs e) // Показ рядов залу в ListBox
        {
            TextBlock cinemaRoomColumns = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaRoomColumns.DataContext as CinemaRoom;
            cinemaRoomColumns.Text = $"Рядов: {cinemaRoom.Rows}";
        }

        private void cinemRoomSeatsOnRow_Initialized(object sender, EventArgs e) // Показ сколько мест в ряде залу в ListBox
        {
            TextBlock cinemaSeatsOnRow = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaSeatsOnRow.DataContext as CinemaRoom;
            cinemaSeatsOnRow.Text = $"Мест в ряде: {cinemaRoom.Columns}";
        }

        private void cinemRoomAllSeats_Initialized(object sender, EventArgs e) // Показ и подсчет сколько всего мест в зале в ListBox
        {
            TextBlock cinemaRoomAllSeats = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaRoomAllSeats.DataContext as CinemaRoom;
            int allSeats = cinemaRoom.Rows * cinemaRoom.Columns;
            cinemaRoomAllSeats.Text = $"Всего мест: {allSeats}";
        }

        private void cinemRoomType_Initialized(object sender, EventArgs e) // Показ типа залу в ListBox
        {
            TextBlock cinemaRoomType = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaRoomType.DataContext as CinemaRoom;
            cinemaRoomType.Text = $"Тип кинозала: {cinemaRoom.Type}";
        }

                    //       ПОКАЗ СПИСКА СЕАНСОВ ПРИ ПОКУПКЕ БИЛЕТОВ        //
        private void sessionsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) // Клик для выбора сессии
        {
            if (MessageBox.Show("Вы хотите купить билет на это вермя?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Session selectSession = (Session)sessionsListBox.SelectedItem;

                BuyPlaceWindow buyPlaceWindow = new BuyPlaceWindow(User, SelectFilm, SelectCinemaRoom, selectSession);
                buyPlaceWindow.Show();
                this.Close();
            }
        }

        private void sessionName_Initialized(object sender, EventArgs e) // Показ имени сессии
        {
            TextBlock sessionName = sender as TextBlock;
            Session session = sessionName.DataContext as Session;
            sessionName.Text = $"{session.Name}";
        }

                    //           ПОКАЗ СПИСКА КУПЛЕННЫХ БИЛЕТОВ             //
        private void ticketName_Initialized(object sender, EventArgs e) // Показ имени билета
        {
            TextBlock ticketName = sender as TextBlock;
            Ticket ticket = ticketName.DataContext as Ticket;
            ticketName.Text = $"{ticket.FilmName} - {ticket.Session}";
        }

        private void ticketDate_Initialized(object sender, EventArgs e) // Показ даты сеанса билета
        {
            TextBlock ticketDate = sender as TextBlock;
            Ticket ticket = ticketDate.DataContext as Ticket;
            ticketDate.Text = $"Дата сеанса: {ticket.Session}";
        }

        private void ticketFilm_Initialized(object sender, EventArgs e) // Показ фильма на который куплен билета
        {
            TextBlock ticketFIlm = sender as TextBlock;
            Ticket ticket = ticketFIlm.DataContext as Ticket;
            ticketFIlm.Text = $"Фильм: {ticket.FilmName}";
        }

        private void ticketSeat_Initialized(object sender, EventArgs e) // Показ сиденья билета
        {
            TextBlock ticketSeat = sender as TextBlock;
            Ticket ticket = ticketSeat.DataContext as Ticket;
            ticketSeat.Text = $"Ряд: {ticket.Rows} Место: {ticket.Columns}";
        }

        private void ticketPrice_Initialized(object sender, EventArgs e) // Показ цены билета
        {
            TextBlock ticketPrice = sender as TextBlock;
            Ticket ticket = ticketPrice.DataContext as Ticket;
            ticketPrice.Text = $"Цена билета: {ticket.Price}";
        }
        ///         КОНЕЦ: Обработка событий и сами события для центрального окна              ///


        private Size MeasureWidth(string candidate, TextBlock measuringTextBlock) // Позволяет менять высоту и ширину
                                                                                  // TextBlockа под размер текста в TextBlock
        {
            var formattedText = new FormattedText(
                candidate,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(measuringTextBlock.FontFamily, measuringTextBlock.FontStyle, measuringTextBlock.FontWeight, measuringTextBlock.FontStretch),
                measuringTextBlock.FontSize,
                Brushes.Black,
                new NumberSubstitution(),
                1);

            return new Size(formattedText.Width, formattedText.Height);
        }

        private void SetText(User user) // Устанавливает данные про пользователя
        {
            int length = User.Password.Count();
            ShowPassword = String.Concat(Enumerable.Repeat("*", length));

            textBlockName.Text = user.Name.ToUpper();
            textBlockName.Width = MeasureWidth(textBlockName.Text, textBlockName).Width;
            textBlockName.Height = MeasureWidth(textBlockName.Text, textBlockName).Height;

            textBlockLogin.Text = $"\"{user.Login.ToUpper()}\"";
            textBlockLogin.Width = MeasureWidth(textBlockLogin.Text, textBlockLogin).Width;
            textBlockLogin.Height = MeasureWidth(textBlockLogin.Text, textBlockLogin).Height;

            textBlockSurname.Text = user.Surname.ToUpper();
            textBlockSurname.Width = MeasureWidth(textBlockSurname.Text, textBlockSurname).Width;
            textBlockSurname.Height = MeasureWidth(textBlockSurname.Text, textBlockSurname).Height;
        }

        private void SortSession() // Отбирает сеансы, которые показываются
                                   // в выбранном зале и на выбранный фильм
        {
            foreach (var session in Storage.sesssions)
            {
                if (session.Film == SelectFilm.Name && session.Room.Name == SelectCinemaRoom.Name 
                    && session.StartDate >= DateTime.Now)
                {
                    FilmsSession.Add(session);
                }
            }

            Session[] newOrder = new Session[FilmsSession.Count];
            FilmsSession.CopyTo(newOrder);
            FilmsSession = newOrder.OrderBy(x => x.StartDate).ToList();
        }

        private void SortTickets() // Отбирает билеты, которые приобрел данный пользователь
        {
            if (Storage.tickets.Count > 0)
            {
                foreach (var ticket in Storage.tickets)
                {
                    if (ticket.User.Login == User.Login && 
                        ticket.User.Password == User.Password)
                    {
                        UserTickets.Add(ticket);
                    }
                }
            }
        }
    }
}
