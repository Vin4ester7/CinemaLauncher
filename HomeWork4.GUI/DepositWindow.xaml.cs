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
    /// Логика взаимодействия для DepositWindow.xaml
    /// </summary>
    public partial class DepositWindow : Window
    {
        User User { get; set; }
        public DepositWindow(User user)
        {
            InitializeComponent();
            User = user;
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

        private void Image_Initialized(object sender, EventArgs e)
        {
            BitmapImage bitmapImage = new BitmapImage(); // Позвоялет работать с картинками

            using (var fileStream = new FileStream("../../Pictures/qr-code.gif", FileMode.Open))
            {
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = fileStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            qrImage.Source = bitmapImage;
        }

        private void depositButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxBalance.Text != null && int.TryParse(textBoxBalance.Text, out int result))
            {
                User.Balance = result;

                UserInterface userInterface = new UserInterface(User);
                userInterface.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Данные введены неверно. Попробуйте снова.");
            }
        }
    }
}
