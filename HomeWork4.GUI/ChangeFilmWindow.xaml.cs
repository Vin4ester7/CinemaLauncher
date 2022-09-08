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
    /// Логика взаимодействия для ChangeFilmWindow.xaml
    /// </summary>
    public partial class ChangeFilmWindow : Window
    {
        Film Film { get; set; }
        string NowRating { get; set; }

        public ChangeFilmWindow(Film film)
        {
            Film = film;

            InitializeComponent();

            textBlockFilmName.Text = film.Name;
            textBlockFilmName.Width = MeasureWidth(textBlockFilmName.Text, textBlockFilmName).Width;
            textBlockFilmName.Height = MeasureWidth(textBlockFilmName.Text, textBlockFilmName).Height;

            filmNameTextBox.Text = film.Name;
            filmCompanyTextBox.Text = film.Company;
            NowRating = film.Rating;

            CheckRating(NowRating);
        }

        private Size MeasureWidth(string candidate, TextBlock measuringTextBlock)
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

        private void filmPoster_Initialized(object sender, EventArgs e) // Показывает постер фильма
        {
            BitmapImage bitmapImage = new BitmapImage(); // Позвоялет работать с картинками

            using (var fileStream = new FileStream(Film.PosterPath, FileMode.Open))
            {
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = fileStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            filmPoster.Source = bitmapImage;
        }

        private void filmPoster_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) // Изменение постера
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files (*.jpeg;*.jpg;*.png)|*.jpeg;*.jpg;*.png";

            if (fileDialog.ShowDialog() == true)
            {
                string posterPath = System.IO.Path.GetFullPath(fileDialog.FileName);
                Film.PosterPath = posterPath;
            }
        }

        private void filmNameTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (filmNameTextBox.Text == Film.Name)
            {
                filmNameTextBox.Clear();
                filmNameTextBox.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void filmNameTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(filmNameTextBox.Text))
            {
                filmNameTextBox.Text = Film.Name;
                filmNameTextBox.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            }
        }

        private void filmCompanyTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (filmCompanyTextBox.Text == Film.Company)
            {
                filmCompanyTextBox.Clear();
                filmCompanyTextBox.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void filmCompanyTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(filmCompanyTextBox.Text))
            {
                filmCompanyTextBox.Text = Film.Company;
                filmCompanyTextBox.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) // Проверяет один ли рейтинг задан и
                                                                        // берет его значение
        {
            if (zeroAge.IsChecked == true)
            {
                sixAge.IsChecked = false;
                twelveAge.IsChecked = false;
                sixtheenAge.IsChecked = false;
                eighteenAge.IsChecked = false;
                NowRating = "0+";
            }
            if (sixAge.IsChecked == true)
            {
                zeroAge.IsChecked = false;
                twelveAge.IsChecked = false;
                sixtheenAge.IsChecked = false;
                eighteenAge.IsChecked = false;
                NowRating = "6+";
            }
            if (twelveAge.IsChecked == true)
            {
                zeroAge.IsChecked = false;
                sixAge.IsChecked = false;
                sixtheenAge.IsChecked = false;
                eighteenAge.IsChecked = false;
                NowRating = "12+";
            }
            if (sixtheenAge.IsChecked == true)
            {
                zeroAge.IsChecked = false;
                sixAge.IsChecked = false;
                twelveAge.IsChecked = false;
                eighteenAge.IsChecked = false;
                NowRating = "16+";
            }
            if (eighteenAge.IsChecked == true)
            {
                zeroAge.IsChecked = false;
                sixAge.IsChecked = false;
                twelveAge.IsChecked = false;
                sixtheenAge.IsChecked = false;
                NowRating = "18+";
            }
        }

        private void CheckRating(string rating) // Устанавливает какой рейтинг сейчас у фильма
        {
            if (rating == "0+")
            {
                zeroAge.IsChecked = true;
                sixAge.IsChecked = false;
                twelveAge.IsChecked = false;
                sixtheenAge.IsChecked = false;
                eighteenAge.IsChecked = false;
            }
            else if (rating == "6+")
            {
                sixAge.IsChecked = true;
                zeroAge.IsChecked = false;
                twelveAge.IsChecked = false;
                sixtheenAge.IsChecked = false;
                eighteenAge.IsChecked = false;
            }
            else if (rating == "12+")
            {
                twelveAge.IsChecked = true;
                sixAge.IsChecked = false;
                zeroAge.IsChecked = false;
                sixtheenAge.IsChecked = false;
                eighteenAge.IsChecked = false;
            }
            else if (rating == "16+")
            {
                sixtheenAge.IsChecked = true;
                twelveAge.IsChecked = false;
                sixAge.IsChecked = false;
                zeroAge.IsChecked = false;
                eighteenAge.IsChecked = false;
            }
            else
            {
                eighteenAge.IsChecked = true;
                sixtheenAge.IsChecked = false;
                twelveAge.IsChecked = false;
                sixAge.IsChecked = false;
                zeroAge.IsChecked = false;
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e) // Кнопка для удаления фильма
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить фильм?", "Оповещение", 
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Storage.films.Remove(Film);

                AdminInterface adminInterface = new AdminInterface();
                adminInterface.Show();
                this.Close();
            }
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e) // Подтверждение изменений
        {
            if (filmNameTextBox.Text != Film.Name && filmCompanyTextBox.Text != Film.Company &&
                NowRating != Film.Rating)
            {
                if (!int.TryParse(filmNameTextBox.Text, out int result) && 
                    !int.TryParse(filmCompanyTextBox.Text, out result))
                {
                    Film.Name = filmNameTextBox.Text;
                    Film.Company = filmCompanyTextBox.Text;
                    Film.Rating = NowRating;

                    AdminInterface adminInterface = new AdminInterface();
                    adminInterface.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Название и компания фильма не могут состоять только цифры.", "Оповещение");
                }
            }
            else if (filmNameTextBox.Text != Film.Name && filmCompanyTextBox.Text != Film.Company)
            {
                if (!int.TryParse(filmNameTextBox.Text, out int result) &&
                    !int.TryParse(filmCompanyTextBox.Text, out result))
                {
                    Film.Name = filmNameTextBox.Text;
                    Film.Company = filmCompanyTextBox.Text;

                    AdminInterface adminInterface = new AdminInterface();
                    adminInterface.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Название и компания фильма не могут состоять только цифры.", "Оповещение");
                }
            }
            else if (filmNameTextBox.Text != Film.Name && NowRating != Film.Rating)
            {
                if (!int.TryParse(filmNameTextBox.Text, out int result))
                {
                    Film.Name = filmNameTextBox.Text;
                    Film.Rating = NowRating;

                    AdminInterface adminInterface = new AdminInterface();
                    adminInterface.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Название фильма не может состоять только цифры.", "Оповещение");
                }
            }
            else if (filmCompanyTextBox.Text != Film.Company && NowRating != Film.Rating)
            {
                if (!int.TryParse(filmCompanyTextBox.Text, out int result))
                {
                    Film.Company = filmCompanyTextBox.Text;
                    Film.Rating = NowRating;

                    AdminInterface adminInterface = new AdminInterface();
                    adminInterface.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Компания фильма не может состоять только цифры.", "Оповещение");
                }
            }
            else if (filmNameTextBox.Text != Film.Name)
            {
                if (!int.TryParse(filmNameTextBox.Text, out int result))
                {
                    Film.Name = filmNameTextBox.Text;

                    AdminInterface adminInterface = new AdminInterface();
                    adminInterface.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Название фильма не может состоять только цифры.", "Оповещение");
                }
            }
            else if (filmCompanyTextBox.Text != Film.Company)
            {
                if (!int.TryParse(filmCompanyTextBox.Text, out int result))
                {
                    Film.Company = filmCompanyTextBox.Text;

                    AdminInterface adminInterface = new AdminInterface();
                    adminInterface.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Компания фильма не может состоять только цифры.", "Оповещение");
                }
            }
            else if (NowRating != Film.Rating)
            {
                Film.Rating = NowRating;

                AdminInterface adminInterface=new AdminInterface();
                adminInterface.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы ничего не изменили. Если вы хотите выйти без изменений, " +
                    "то нажмите соответсвующую кнопку", "Оповещение");
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e) // Возвращает на интерфейс Админа
        {
            AdminInterface adminInterface = new AdminInterface();
            adminInterface.Show();
            this.Close();
        }

        private void deleteRoomsButton_Click(object sender, RoutedEventArgs e) // Переносит пользователя на окно со
                                                                               // списком залов, которые можно удалить
        {
            DeleteFilmRoomsWindow deleteFilmRooms = new DeleteFilmRoomsWindow(Film);
            deleteFilmRooms.Show();
            this.Close();
        }

        private void addRoomsButton_Click(object sender, RoutedEventArgs e) // Переносит пользователя на окно со
                                                                            // списком залов, которые можно добавить
        {
            AddFilmRoomsWindow addFilmRooms = new AddFilmRoomsWindow(Film);
            addFilmRooms.Show();
            this.Close();
        }
    }
}
