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
    /// Логика взаимодействия для ChooseFilmWindow.xaml
    /// </summary>
    public partial class ChooseFilmWindow : Window
    {
        DateTime DateShow { get; set; }
        string DateInputStr { get; set; }

        public ChooseFilmWindow(DateTime dateShow, string dateInputStr)
        {
            InitializeComponent();

            filmsListBox.ItemsSource = Storage.films;
            DateShow = dateShow;
            DateInputStr = dateInputStr;
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

        private void filmPoster_Initialized(object sender, EventArgs e)
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

        private void filmName_Initialized(object sender, EventArgs e)
        {
            TextBlock filmName = sender as TextBlock;
            Film film = filmName.DataContext as Film;
            filmName.Text = $"Фильм: {film.Name}";

        }

        private void filmRatings_Initialized(object sender, EventArgs e)
        {
            TextBlock filmRating = sender as TextBlock;
            Film film = filmRating.DataContext as Film;
            filmRating.Text = $"Возрастной рейтинг фильма: {film.Rating}";
        }

        private void filmCompany_Initialized(object sender, EventArgs e)
        {
            TextBlock filmCompany = sender as TextBlock;
            Film film = filmCompany.DataContext as Film;
            filmCompany.Text = $"Кинокомпания: {film.Company}";
        }

        private void filmsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Вы хотите добавить сеанс на этот фильм?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Film selectFilm = (Film)filmsListBox.SelectedItem;

                int indexFilm = Storage.films.IndexOf(selectFilm);
                string sessionFilm = Storage.films[indexFilm].Name;

                string sessionName = $"{sessionFilm} - {DateInputStr}";

                SessionCinemaRoomWindow window = new SessionCinemaRoomWindow(DateShow, selectFilm,
                                                                             sessionFilm, sessionName);
                window.Show();
                this.Close();
            }
        }
    }
}
