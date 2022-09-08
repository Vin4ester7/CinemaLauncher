using HomeWork4.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для ChangeCinemaRoomWindow.xaml
    /// </summary>
    public partial class ChangeCinemaRoomWindow : Window
    {
        CinemaRoom CinemaRoom { get; set; }
        string NowType { get; set; }

        public ChangeCinemaRoomWindow(CinemaRoom cinemaRoom)
        {
            InitializeComponent();

            CinemaRoom = cinemaRoom;
            textBlockCinemaRoom.Text = cinemaRoom.Name;
            changeCinemaRoomName.Text = cinemaRoom.Name;

            textBlockCinemaRoom.Width = MeasureWidth(textBlockCinemaRoom.Text, textBlockCinemaRoom).Width;
            textBlockCinemaRoom.Height = MeasureWidth(textBlockCinemaRoom.Text, textBlockCinemaRoom).Height;
            NowType = cinemaRoom.Type;

            if (cinemaRoom.Type == "Стандарт")
            {
                standartCheckBox.IsChecked = true;
                vipCheckBox.IsChecked = false;
                imaxCheckBox.IsChecked = false;
            }
            else if (cinemaRoom.Type == "VIP")
            {
                vipCheckBox.IsChecked = true;
                standartCheckBox.IsChecked = false;
                imaxCheckBox.IsChecked = false;
            }
            else
            {
                imaxCheckBox.IsChecked = true;
                standartCheckBox.IsChecked = false;
                vipCheckBox.IsChecked = false;
            }
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

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите вернуться без изменений", "Оповещение", 
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                AdminInterface adminInterface = new AdminInterface();
                adminInterface.Show();
                this.Close();
            }
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (changeCinemaRoomName.Text != CinemaRoom.Name && NowType != CinemaRoom.Type)
            {
                CinemaRoom.Name = changeCinemaRoomName.Text;
                CinemaRoom.Type = NowType;

                AdminInterface adminInterface = new AdminInterface();
                adminInterface.Show();
                this.Close();
            }
            else if (NowType != CinemaRoom.Type)
            {
                CinemaRoom.Type = NowType;

                AdminInterface adminInterface = new AdminInterface();
                adminInterface.Show();
                this.Close();
            }
            else if (changeCinemaRoomName.Text != CinemaRoom.Name)
            {
                CinemaRoom.Name = changeCinemaRoomName.Text;

                AdminInterface adminInterface = new AdminInterface();
                adminInterface.Show();
                this.Close();
            }
            else if (changeCinemaRoomName.Text == CinemaRoom.Name && NowType == CinemaRoom.Type)
            {
                MessageBox.Show("Вы ничего не изменили. Измените какие-либо данные или нажмите кнопку \"Назад\"",
                                "Оповещение");
            }
        }

        private void CheckBox_Checked(object sender1, RoutedEventArgs e1)
        {
            if (vipCheckBox.IsChecked == true)
            {
                imaxCheckBox.IsChecked = false;
                standartCheckBox.IsChecked = false;
                NowType = "VIP";
            }
            if (imaxCheckBox.IsChecked == true)
            {
                vipCheckBox.IsChecked = false;
                standartCheckBox.IsChecked = false;
                NowType = "IMAX";
            }
            if (standartCheckBox.IsChecked == true)
            {
                imaxCheckBox.IsChecked = false;
                vipCheckBox.IsChecked = false;
                NowType = "Стандарт";
            }
        }

        private void changeCinemaRoomName_MouseEnter(object sender, MouseEventArgs e)
        {
            if (changeCinemaRoomName.Text == CinemaRoom.Name)
            {
                changeCinemaRoomName.Clear();
                changeCinemaRoomName.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void changeCinemaRoomName_MouseLeave(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(changeCinemaRoomName.Text))
            {
                changeCinemaRoomName.Text = CinemaRoom.Name;
                changeCinemaRoomName.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            }
        }

        private void changePrices_Click(object sender, RoutedEventArgs e)
        {
            ChangeCinemaRoomPricesWindow changeCinemaRoomPrices = new ChangeCinemaRoomPricesWindow(CinemaRoom);
            changeCinemaRoomPrices.Show();
            this.Close();
        }
    }
}
