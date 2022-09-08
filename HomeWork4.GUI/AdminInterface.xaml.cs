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
    /// Логика взаимодействия для AdminInterface.xaml
    /// </summary>
    public partial class AdminInterface : Window
    {
        string PosterPath { get; set; }

        public AdminInterface()
        {
            InitializeComponent();

            textBlockAdmin.Text = "Admin".ToUpper();
            textBlockAdmin.Width = MeasureWidth(textBlockAdmin.Text, textBlockAdmin).Width;
            textBlockAdmin.Height = MeasureWidth(textBlockAdmin.Text, textBlockAdmin).Height;

            cinemaRoomsList.ItemsSource = Storage.cinemaRooms;
            filmListBox.ItemsSource = Storage.films;
        }

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
        private void addHall_MouseDoubleClick(object sender, MouseButtonEventArgs e) // Создает в рабочем поле все нужные
                                                                                     // компоненты для создания зала
        {
            gridForElements.Children.Clear();
            gridForCinemaList.Visibility = Visibility.Hidden;
            gridForFilmList.Visibility = Visibility.Hidden;
            gridAnalytic.Visibility = Visibility.Hidden;

            TextBlock nameTextBlock = new TextBlock()
            {
                Width = 300,
                Height = 35,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 20, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 22,
                FontFamily = new FontFamily("Veranda"),
                Text = "Введите название зала:",
                TextAlignment = TextAlignment.Center,
            };
            gridForElements.Children.Add(nameTextBlock);

            TextBlock rowsTextBlock = new TextBlock()
            {
                Width = 400,
                Height = 35,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 110, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 22,
                FontFamily = new FontFamily("Veranda"),
                Text = "Введите количество рядов в зале:",
                TextAlignment = TextAlignment.Center,
            };
            gridForElements.Children.Add(rowsTextBlock);

            TextBlock columnsTextBlock = new TextBlock()
            {
                Width = 400,
                Height = 35,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 200, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 22,
                FontFamily = new FontFamily("Veranda"),
                Text = "Введите количество мест в ряду:",
                TextAlignment = TextAlignment.Center,
            };
            gridForElements.Children.Add(columnsTextBlock);

            TextBox putNameTextBox = new TextBox()
            {
                Width = 400,
                Height = 35,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 50, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 22,
                FontFamily = new FontFamily("Veranda"),
                TextAlignment = TextAlignment.Center,
                Background = new SolidColorBrush(Colors.Transparent)
            };
            gridForElements.Children.Add(putNameTextBox);

            TextBox putRowsTextBox = new TextBox()
            {
                Width = 400,
                Height = 35,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 140, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 22,
                FontFamily = new FontFamily("Veranda"),
                TextAlignment = TextAlignment.Center,
                Background = new SolidColorBrush(Colors.Transparent)
            };
            gridForElements.Children.Add(putRowsTextBox);

            TextBox putColumnsTextBox = new TextBox()
            {
                Width = 400,
                Height = 35,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 230, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 22,
                FontFamily = new FontFamily("Veranda"),
                TextAlignment = TextAlignment.Center,
                Background = new SolidColorBrush(Colors.Transparent)
            };
            gridForElements.Children.Add(putColumnsTextBox);

            StackPanel chooseType = new StackPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 170,
                Width = 400,
                Orientation = Orientation.Vertical,
                Margin = new Thickness(0, 290, 0, 0)
            };

            TextBlock typeTextBlock = new TextBlock()
            {
                Width = 400,
                Height = 35,
                FontSize = 24,
                FontFamily = new FontFamily("Veranda"),
                Text = "Выберите тип зала:",
                Foreground = new SolidColorBrush(Colors.White),
                TextAlignment = TextAlignment.Center
            };
            chooseType.Children.Add(typeTextBlock);

            CheckBox standartCheckBox = new CheckBox()
            {
                Width = 400,
                Height = 50,
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 24,
                FontFamily = new FontFamily("Veranda"),
                Content = "Стандартный",
                IsThreeState = false,
            };
            chooseType.Children.Add(standartCheckBox);

            CheckBox vipCheckBox = new CheckBox()
            {
                Width = 400,
                Height = 50,
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 24,
                FontFamily = new FontFamily("Veranda"),
                Content = "VIP",
                IsThreeState = false,
            };
            chooseType.Children.Add(vipCheckBox);

            CheckBox imaxCheckBox = new CheckBox()
            {
                Width = 400,
                Height = 50,
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 24,
                FontFamily = new FontFamily("Veranda"),
                Content = "IMAX",
                IsThreeState = false,
            };
            chooseType.Children.Add(imaxCheckBox);
            gridForElements.Children.Add(chooseType);

            Button acceptButton = new Button()
            {
                Width = 200,
                Height = 35,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 40),
                Background = new SolidColorBrush(Colors.SeaGreen),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 24,
                FontFamily = new FontFamily("Veranda"),
                Content = "Cоздать зал"
            };
            acceptButton.Click += acceptButton_Click;
            gridForElements.Children.Add(acceptButton);

            standartCheckBox.Checked += CheckBox_Checked;
            vipCheckBox.Checked += CheckBox_Checked;
            imaxCheckBox.Checked += CheckBox_Checked;

            void CheckBox_Checked(object sender1, RoutedEventArgs e1)
            {
                if (vipCheckBox.IsChecked == true)
                {
                    imaxCheckBox.IsChecked = false;
                    standartCheckBox.IsChecked = false;
                }
                if (imaxCheckBox.IsChecked == true)
                {
                    vipCheckBox.IsChecked = false;
                    standartCheckBox.IsChecked = false;
                }
                if (standartCheckBox.IsChecked == true)
                {
                    imaxCheckBox.IsChecked = false;
                    vipCheckBox.IsChecked = false;
                }
            }

            void acceptButton_Click(object sender1, RoutedEventArgs e1)
            {
                int columns;
                int rows;

                if (putRowsTextBox.Text != null && putNameTextBox.Text != null
                    && putColumnsTextBox.Text != null)
                {
                    if (int.TryParse(putRowsTextBox.Text, out rows) &&
                        !int.TryParse(putNameTextBox.Text, out int checkString) &&
                        int.TryParse(putColumnsTextBox.Text, out columns))
                    {
                        if (standartCheckBox.IsChecked == true)
                        {
                            PutCostWindow put = new PutCostWindow(putNameTextBox.Text, rows,
                                                                  columns, CinemaRoom.typeOfRoom[0]);
                            put.Show();
                            this.Close();
                        }
                        else if (vipCheckBox.IsChecked == true)
                        {
                            PutCostWindow put = new PutCostWindow(putNameTextBox.Text, rows,
                                                                  columns, CinemaRoom.typeOfRoom[1]);
                            put.Show();
                            this.Close();
                        }
                        else if (imaxCheckBox.IsChecked == true)
                        {
                            PutCostWindow put = new PutCostWindow(putNameTextBox.Text, rows,
                                                                  columns, CinemaRoom.typeOfRoom[2]);
                            put.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Тип зала должен быть выбран.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введенные данные некорректный. Попробуйте еще раз.");
                    }
                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнены");
                }
            }
        }

        private void addFilm_MouseDoubleClick(object sender, MouseButtonEventArgs e) // Создает в рабочем поле все нужные
                                                                                     // компоненты для создания фильма
        {
            gridForElements.Children.Clear();
            gridForCinemaList.Visibility = Visibility.Hidden;
            gridForFilmList.Visibility = Visibility.Hidden;
            gridAnalytic.Visibility = Visibility.Hidden;

            if (Storage.cinemaRooms.Count > 0)
            {

                TextBlock nameTextBlock = new TextBlock()
                {
                    Width = 300,
                    Height = 35,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(0, 20, 0, 0),
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 22,
                    FontFamily = new FontFamily("Veranda"),
                    Text = "Введите название фильма:",
                    TextAlignment = TextAlignment.Center,
                };
                gridForElements.Children.Add(nameTextBlock);

                TextBlock companyTextBlock = new TextBlock()
                {
                    Width = 400,
                    Height = 35,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(0, 110, 0, 0),
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 22,
                    FontFamily = new FontFamily("Veranda"),
                    Text = "Введите кинокомпанию:",
                    TextAlignment = TextAlignment.Center,
                };
                gridForElements.Children.Add(companyTextBlock);

                TextBox putNameTextBox = new TextBox()
                {
                    Width = 400,
                    Height = 35,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(0, 50, 0, 0),
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 22,
                    FontFamily = new FontFamily("Veranda"),
                    TextAlignment = TextAlignment.Center,
                    Background = new SolidColorBrush(Colors.Transparent)
                };
                gridForElements.Children.Add(putNameTextBox);

                TextBox putCompanyTextBox = new TextBox()
                {
                    Width = 400,
                    Height = 35,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(0, 140, 0, 0),
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 22,
                    FontFamily = new FontFamily("Veranda"),
                    TextAlignment = TextAlignment.Center,
                    Background = new SolidColorBrush(Colors.Transparent)
                };
                gridForElements.Children.Add(putCompanyTextBox);

                StackPanel chooseType = new StackPanel()
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Height = 180,
                    Width = 400,
                    Orientation = Orientation.Vertical,
                    Margin = new Thickness(0, 200, 0, 0)
                };

                TextBlock typeTextBlock = new TextBlock()
                {
                    Width = 400,
                    Height = 35,
                    FontSize = 24,
                    FontFamily = new FontFamily("Veranda"),
                    Text = "Выберите рейтинг фильма:",
                    Foreground = new SolidColorBrush(Colors.White),
                    TextAlignment = TextAlignment.Center
                };
                chooseType.Children.Add(typeTextBlock);

                CheckBox zeroAgeCheckBox = new CheckBox()
                {
                    Width = 400,
                    Height = 30,
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 24,
                    FontFamily = new FontFamily("Veranda"),
                    Content = "0+",
                    IsThreeState = false,
                };
                chooseType.Children.Add(zeroAgeCheckBox);

                CheckBox sixAgeCheckBox = new CheckBox()
                {
                    Width = 400,
                    Height = 30,
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 24,
                    FontFamily = new FontFamily("Veranda"),
                    Content = "6+",
                    IsThreeState = false,
                };
                chooseType.Children.Add(sixAgeCheckBox);

                CheckBox twelveAgeCheckBox = new CheckBox()
                {
                    Width = 400,
                    Height = 30,
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 24,
                    FontFamily = new FontFamily("Veranda"),
                    Content = "12+",
                    IsThreeState = false,
                };
                chooseType.Children.Add(twelveAgeCheckBox);

                CheckBox sixtheenAgeCheckBox = new CheckBox()
                {
                    Width = 400,
                    Height = 30,
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 24,
                    FontFamily = new FontFamily("Veranda"),
                    Content = "16+",
                    IsThreeState = false,
                };
                chooseType.Children.Add(sixtheenAgeCheckBox);

                CheckBox eighteenAgeCheckBox = new CheckBox()
                {
                    Width = 400,
                    Height = 30,
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 24,
                    FontFamily = new FontFamily("Veranda"),
                    Content = "18+",
                    IsThreeState = false,
                };
                chooseType.Children.Add(eighteenAgeCheckBox);
                gridForElements.Children.Add(chooseType);

                Button continueButton = new Button()
                {
                    Width = 240,
                    Height = 35,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 20),
                    Background = new SolidColorBrush(Colors.SeaGreen),
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 24,
                    FontFamily = new FontFamily("Veranda"),
                    Content = "Продолжить"
                };
                continueButton.Click += continueButton_Click;
                gridForElements.Children.Add(continueButton);

                Button imageButton = new Button()
                {
                    Width = 240,
                    Height = 35,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 90),
                    Background = new SolidColorBrush(Colors.SeaGreen),
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 24,
                    FontFamily = new FontFamily("Veranda"),
                    Content = "Выбрать потсер"
                };
                imageButton.Click += imageButton_Click;
                gridForElements.Children.Add(imageButton);

                zeroAgeCheckBox.Checked += CheckBox_Checked;
                sixAgeCheckBox.Checked += CheckBox_Checked;
                twelveAgeCheckBox.Checked += CheckBox_Checked;
                sixtheenAgeCheckBox.Checked += CheckBox_Checked;
                eighteenAgeCheckBox.Checked += CheckBox_Checked;

                void CheckBox_Checked(object sender1, RoutedEventArgs e1)
                {
                    if (zeroAgeCheckBox.IsChecked == true)
                    {
                        sixAgeCheckBox.IsChecked = false;
                        twelveAgeCheckBox.IsChecked = false;
                        sixtheenAgeCheckBox.IsChecked = false;
                        eighteenAgeCheckBox.IsChecked = false;
                    }
                    if (sixAgeCheckBox.IsChecked == true)
                    {
                        zeroAgeCheckBox.IsChecked = false;
                        twelveAgeCheckBox.IsChecked = false;
                        sixtheenAgeCheckBox.IsChecked = false;
                        eighteenAgeCheckBox.IsChecked = false;
                    }
                    if (twelveAgeCheckBox.IsChecked == true)
                    {
                        zeroAgeCheckBox.IsChecked = false;
                        sixAgeCheckBox.IsChecked = false;
                        sixtheenAgeCheckBox.IsChecked = false;
                        eighteenAgeCheckBox.IsChecked = false;
                    }
                    if (sixtheenAgeCheckBox.IsChecked == true)
                    {
                        zeroAgeCheckBox.IsChecked = false;
                        sixAgeCheckBox.IsChecked = false;
                        twelveAgeCheckBox.IsChecked = false;
                        eighteenAgeCheckBox.IsChecked = false;
                    }
                    if (eighteenAgeCheckBox.IsChecked == true)
                    {
                        zeroAgeCheckBox.IsChecked = false;
                        sixAgeCheckBox.IsChecked = false;
                        twelveAgeCheckBox.IsChecked = false;
                        sixtheenAgeCheckBox.IsChecked = false;
                    }
                }

                void imageButton_Click(object sender1, RoutedEventArgs e1)
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "Image files (*.jpeg;*.jpg;*.png)|*.jpeg;*.jpg;*.png";

                    if (fileDialog.ShowDialog() == true)
                    {
                        string posterPath = System.IO.Path.GetFullPath(fileDialog.FileName);
                        PosterPath = posterPath;
                    }
                }

                void continueButton_Click(object sender1, RoutedEventArgs e1)
                {
                    if (PosterPath == null)
                    {
                        PosterPath = "../../Pictures/avatar.jpeg";
                    }

                    if (putNameTextBox.Text != null && putCompanyTextBox.Text != null)
                    {
                        if (!int.TryParse(putNameTextBox.Text, out int value) &&
                            !int.TryParse(putCompanyTextBox.Text, out value))
                        {
                            if (zeroAgeCheckBox.IsChecked == true)
                            {
                                ChooseCinemaRoom chooseCinemaRoom = new ChooseCinemaRoom(putNameTextBox.Text,
                                                                                          Film.ageRating[0],
                                                                                          putCompanyTextBox.Text, 
                                                                                          PosterPath);
                                chooseCinemaRoom.Show();
                                this.Close();
                            }
                            else if (sixAgeCheckBox.IsChecked == true)
                            {
                                ChooseCinemaRoom chooseCinemaRoom = new ChooseCinemaRoom(putNameTextBox.Text,
                                                                                         Film.ageRating[1],
                                                                                         putCompanyTextBox.Text,
                                                                                         PosterPath);
                                chooseCinemaRoom.Show();
                                this.Close();
                            }
                            else if (twelveAgeCheckBox.IsChecked == true)
                            {
                                ChooseCinemaRoom chooseCinemaRoom = new ChooseCinemaRoom(putNameTextBox.Text,
                                                                                         Film.ageRating[2],
                                                                                         putCompanyTextBox.Text,
                                                                                         PosterPath);
                                chooseCinemaRoom.Show();
                                this.Close();
                            }
                            else if (sixtheenAgeCheckBox.IsChecked == true)
                            {
                                ChooseCinemaRoom chooseCinemaRoom = new ChooseCinemaRoom(putNameTextBox.Text,
                                                                                         Film.ageRating[3],
                                                                                         putCompanyTextBox.Text,
                                                                                         PosterPath);
                                chooseCinemaRoom.Show();
                                this.Close();
                            }
                            else if (eighteenAgeCheckBox.IsChecked == true)
                            {
                                ChooseCinemaRoom chooseCinemaRoom = new ChooseCinemaRoom(putNameTextBox.Text,
                                                                                         Film.ageRating[4],
                                                                                         putCompanyTextBox.Text,
                                                                                         PosterPath);
                                chooseCinemaRoom.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Рейтинг фильма должен быть указан.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Данные введены некорректно.Попробуйте еще раз");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Все поля должны быть заполнены");
                    }
                }
            }
            else
            {
                MessageBox.Show("Фильмы нельзя показывать без залов. Добавте сперва зал.");
            }
        }

        private void addSession_MouseDoubleClick(object sender, MouseButtonEventArgs e) // Создает в рабочем поле все нужные
                                                                                        // компоненты для создания сеанса
        {
            gridForElements.Children.Clear();
            gridForCinemaList.Visibility = Visibility.Hidden;
            gridForFilmList.Visibility = Visibility.Hidden;
            gridAnalytic.Visibility = Visibility.Hidden;

            if (Storage.cinemaRooms.Count > 0)
            {
                if (Storage.films.Count > 0)
                {
                    TextBlock goalOfPage = new TextBlock()
                    {
                        Width = 650,
                        Height = 35,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(0, 0, 0, 100),
                        Foreground = new SolidColorBrush(Colors.White),
                        FontFamily = new FontFamily("Veranda"),
                        FontSize = 24,
                        TextAlignment = TextAlignment.Center,
                        Text = "Введите дату сеанса:"
                    };
                    gridForElements.Children.Add(goalOfPage);

                    TextBox dateInputTextBox = new TextBox()
                    {
                        Width = 500,
                        Height = 35,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Background = new SolidColorBrush(Colors.Transparent),
                        Foreground = new SolidColorBrush(Colors.WhiteSmoke),
                        FontSize = 24,
                        FontFamily = new FontFamily("Veranda"),
                        Margin = new Thickness(0, 0, 0, 30),
                        TextAlignment = TextAlignment.Center,
                        Text = "ГГГГ-ММ-ДД-ЧЧ-ММ"
                    };
                    dateInputTextBox.MouseEnter += dateInput_MouseEnter;
                    dateInputTextBox.MouseLeave += dateInput_MouseLeave;
                    gridForElements.Children.Add(dateInputTextBox);

                    Button continueButton = new Button()
                    {
                        Width = 200,
                        Height = 35,
                        Background = new SolidColorBrush(Colors.SeaGreen),
                        Foreground = new SolidColorBrush(Colors.White),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment= VerticalAlignment.Center,
                        Margin = new Thickness(0, 70, 0, 0),
                        FontFamily = new FontFamily("Veranda"),
                        FontSize = 24,
                        Content = "Продолжить"
                    };
                    continueButton.Click += continueButton_Click;
                    gridForElements.Children.Add(continueButton);

                    void continueButton_Click(object sender1, RoutedEventArgs e1)
                    {
                        DateTime dateShow = new DateTime();

                        if (dateInputTextBox.Text != "" && ChekDate(dateInputTextBox.Text) &&
                            dateInputTextBox.Text != "ГГГГ-ММ-ДД-ЧЧ-ММ")
                        {

                            string[] dateElements = dateInputTextBox.Text.Split('-');
                            string dateInputStr = $"{dateElements[2]}/{dateElements[1]}" +
                                             $"/{dateElements[0]} {dateElements[3]}" +
                                             $":{dateElements[4]}:00.0";

                            try { dateShow = Convert.ToDateTime(dateInputStr); }
                            catch { }

                            DateTime startDate = Convert.ToDateTime(dateInputStr);

                            ChooseFilmWindow chooseFilmWindow = new ChooseFilmWindow(startDate, dateInputStr);
                            chooseFilmWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Введенные данные некорректны.");
                        }
                    }

                    void dateInput_MouseEnter(object sender1, MouseEventArgs e1)
                    {
                        if (dateInputTextBox.Text == "ГГГГ-ММ-ДД-ЧЧ-ММ")
                        {
                            dateInputTextBox.Clear();
                            dateInputTextBox.Foreground = new SolidColorBrush(Colors.White);
                        }
                    }

                    void dateInput_MouseLeave(object sender1, MouseEventArgs e1)
                    {
                        if (string.IsNullOrWhiteSpace(dateInputTextBox.Text))
                        {
                            dateInputTextBox.Text = "ГГГГ-ММ-ДД-ЧЧ-ММ";
                            dateInputTextBox.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Нельзя создать сеанс без фильмов.");
                }
            }
            else
            {
                MessageBox.Show("Нельзя создать сеанс без кинозалов.");
            } 

            bool ChekDate(string dateInput)
            {
                DateTime valueDate;
                var result = DateTime.TryParseExact(dateInput, "yyyy-MM-dd-HH-mm",
                        CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out valueDate);
                return result;
            }

        }

        private void changeCinemaRoom_MouseDoubleClick(object sender, MouseButtonEventArgs e) // Выводит из режима невидимости
                                                                                              // лист из залов в рабочем поле
        {
            gridForElements.Children.Clear();
            gridForFilmList.Visibility = Visibility.Hidden;
            gridAnalytic.Visibility = Visibility.Hidden;

            if (Storage.cinemaRooms.Count > 0)
            {
                gridForCinemaList.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("У вас нету залов для изменения. Создайте зал.", "Оповещение");
            }
        }

        private void changeFilm_MouseDoubleClick(object sender, MouseButtonEventArgs e) // Выводит из режима невидимки
                                                                                        // лист из фильмов в рабочем поле
        {
            gridForElements.Children.Clear();
            gridForCinemaList.Visibility= Visibility.Hidden;
            gridAnalytic.Visibility = Visibility.Hidden;


            if (Storage.cinemaRooms.Count > 0)
            {
                if (Storage.films.Count > 0)
                {
                    gridForFilmList.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("У вас нет фильмов для изменения.Создайте фильм.", "Оповещение");
                }
            }
            else
            {
                MessageBox.Show("У вас нет зало, где показываются фильмы. Создайте зал и фильм.", "Оповещение");
            }

        }

        private void analytic_MouseDoubleClick(object sender, MouseButtonEventArgs e) // Выводит из режима невидимки
                                                                                      // панель для выбора топа в рабочем поле
        {
            gridForElements.Children.Clear();
            gridForCinemaList.Visibility = Visibility.Hidden;
            gridForFilmList.Visibility= Visibility.Hidden;
            
            if (Storage.users.Count > 0)
            {
                gridAnalytic.Visibility = Visibility.Visible;
                proccesing.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Пока в нашем кинотеатре нет пользоваетелей для топа(((");
            }
        }

        private void exit_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            gridForElements.Children.Clear();

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
        ///                      КОНЕЦ: Клики и события для панели слева                   ///


        ///         НАЧАЛО: Обработка событий и сами события для центрального окна        ///
                                //         ИЗМЕНЕНИЕ ЗАЛА           //
        private void cinemaRoomsList_MouseDoubleClick(object sender, MouseButtonEventArgs e) // Клик изменения зала
        {
            if (MessageBox.Show("Вы хотите изменить этот зал?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                CinemaRoom selectCinemaRoom = (CinemaRoom)cinemaRoomsList.SelectedItem;

                ChangeCinemaRoomWindow changeCinemaRoomWindow = new ChangeCinemaRoomWindow(selectCinemaRoom);
                changeCinemaRoomWindow.Show();
                this.Close();
            }
        }

        private void cinemRoomName_Initialized(object sender, EventArgs e) // Присвоение имени залу в ListBox
        {
            TextBlock cinemaRoomName = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaRoomName.DataContext as CinemaRoom;
            cinemaRoomName.Text = $"{cinemaRoom.Name.ToUpper()}";
        } 

        private void cinemRoomColumns_Initialized(object sender, EventArgs e) // Присвоение рядов залу в ListBox
        {
            TextBlock cinemaRoomColumns = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaRoomColumns.DataContext as CinemaRoom;
            cinemaRoomColumns.Text = $"Рядов: {cinemaRoom.Rows}";
        }

        private void cinemRoomSeatsOnRow_Initialized(object sender, EventArgs e) // Присвоение сколько мест в ряде залу в ListBox
        {
            TextBlock cinemaSeatsOnRow = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaSeatsOnRow.DataContext as CinemaRoom;
            cinemaSeatsOnRow.Text = $"Мест в ряде: {cinemaRoom.Columns}";
        }

        private void cinemRoomAllSeats_Initialized(object sender, EventArgs e) // Присвоение и подсчет сколько всего мест в зале в ListBox
        {
            TextBlock cinemaRoomAllSeats = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaRoomAllSeats.DataContext as CinemaRoom;
            int allSeats = cinemaRoom.Rows * cinemaRoom.Columns;
            cinemaRoomAllSeats.Text = $"Всего мест: {allSeats}";
        }

        private void cinemRoomType_Initialized(object sender, EventArgs e) // Присвоение типа залу в ListBox
        {
            TextBlock cinemaRoomType = sender as TextBlock;
            CinemaRoom cinemaRoom = cinemaRoomType.DataContext as CinemaRoom;
            cinemaRoomType.Text = $"Тип кинозала: {cinemaRoom.Type}";
        }

                              //         ИЗМЕНЕНИЕ ФИЛЬМА          //
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

        private void filmListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) // Клик для изменения фильма
        {
            if (MessageBox.Show("Вы хотите изменить именно этот фильм?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Film selectedFilm = (Film)filmListBox.SelectedItem;

                ChangeFilmWindow changeFilmWindow = new ChangeFilmWindow(selectedFilm);
                changeFilmWindow.Show();
                this.Close();
            }
        }

                              //        ПРОСМОТР АНАЛИТИКИ      //
        private void ticketTop_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            proccesing.Visibility = Visibility.Visible;
        }

        private void spendTop_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            proccesing.Visibility = Visibility.Visible;
        }
        ///         КОНЕЦ: Обработка событий и сами события для центрального окна        ///
    }
}
