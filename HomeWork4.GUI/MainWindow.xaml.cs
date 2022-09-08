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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeWork4.GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        List<User> Users = new List<User>();

        public MainWindow()
        {
            InitializeComponent();

            ReadFile();

            Users = Storage.users;
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

        private void login_MouseDown(object sender, MouseButtonEventArgs e)
        {
            gridText.Children.Clear();

            TextBox textBoxLogin = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 200,
                Height = 20,
                Background = new SolidColorBrush(Colors.Gray),
                Foreground = new SolidColorBrush(Colors.WhiteSmoke),
                Margin = new Thickness(0, 0, 0, 150),
                Text = "Введите логин",
                FontSize = 15
            };
            textBoxLogin.MouseEnter += textBoxLogin_MouseEnter;
            textBoxLogin.MouseLeave += textBoxLogin_MouseLeave;
            gridText.Children.Add(textBoxLogin);

            TextBox textBoxPassword = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 200,
                Height = 20,
                Background = new SolidColorBrush(Colors.Gray),
                Foreground = new SolidColorBrush(Colors.WhiteSmoke),
                Margin = new Thickness(0, 0, 0, 80),
                Text = "Введите пароль",
                FontSize = 15
            };
            textBoxPassword.MouseEnter += textBoxPassword_MouseEnter;
            textBoxPassword.MouseLeave += textBoxPassword_MouseLeave;
            gridText.Children.Add(textBoxPassword);

            Button buttonEnter = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 100,
                Height = 20,
                Background = new SolidColorBrush(Colors.MediumAquamarine),
                Margin = new Thickness(0, 0, 0, 10),
                Content = "Войти",
                FontSize = 15,
                HorizontalContentAlignment = HorizontalAlignment.Center
            };
            buttonEnter.Click += buttonEnter_Click;
            gridText.Children.Add(buttonEnter);

            void textBoxLogin_MouseEnter(object sender1, MouseEventArgs e1)
            {
                if (textBoxLogin.Text == "Введите логин")
                {
                    textBoxLogin.Clear();
                    textBoxLogin.Foreground = new SolidColorBrush(Colors.White);
                }
            }

            void textBoxLogin_MouseLeave(object sender1, MouseEventArgs e1)
            {
                if (string.IsNullOrWhiteSpace(textBoxLogin.Text))
                {
                    textBoxLogin.Text = "Введите логин";
                    textBoxLogin.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                }
            }

            void textBoxPassword_MouseEnter(object sender1, MouseEventArgs e1)
            {
                if (textBoxPassword.Text == "Введите пароль")
                {
                    textBoxPassword.Clear();
                    textBoxPassword.Foreground = new SolidColorBrush(Colors.White);
                }
            }

            void textBoxPassword_MouseLeave(object sender1, MouseEventArgs e1)
            {
                if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
                {
                    textBoxPassword.Text = "Введите пароль";
                    textBoxPassword.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                }
            }

            void buttonEnter_Click(object sender1, EventArgs e1)
            {
                if (textBoxLogin.Text != "Введите логин" && textBoxPassword.Text != "Введите пароль")
                {
                    if (textBoxLogin.Text == "admin" && textBoxPassword.Text == "qwerty")
                    {
                        AdminInterface adminInterface = new AdminInterface();
                        adminInterface.Show();
                        this.Close();
                    }
                    else if (Storage.users.Count != 0)
                    {
                        int counter = 1;
                        foreach (var user in Users)
                        {
                            if (textBoxLogin.Text == user.Login && textBoxPassword.Text == user.Password)
                            {
                                UserInterface userInterface = new UserInterface(user);
                                userInterface.Show();
                                this.Close();
                                break;
                            }
                            if (counter == Storage.users.Count)
                            {
                                MessageBox.Show("Такого пользователя не существует или вы ввели неккорктные данные.");
                                break;
                            }
                            counter++;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Такого пользователя не существует.");
                    }
                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнены!");
                }
            }
        }

        private void register_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            gridText.Children.Clear();

            TextBox textBoxLogin = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 200,
                Height = 20,
                Background = new SolidColorBrush(Colors.Gray),
                Foreground = new SolidColorBrush(Colors.WhiteSmoke),
                Margin = new Thickness(0, 0, 0, 200),
                Text = "Введите логин",
                FontSize = 15
            };
            textBoxLogin.MouseEnter += textBoxLogin_MouseEnter;
            textBoxLogin.MouseLeave += textBoxLogin_MouseLeave;
            gridText.Children.Add(textBoxLogin);

            TextBox textBoxPassword = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 200,
                Height = 20,
                Background = new SolidColorBrush(Colors.Gray),
                Foreground = new SolidColorBrush(Colors.WhiteSmoke),
                Margin = new Thickness(0, 0, 0, 150),
                Text = "Введите пароль",
                FontSize = 15
            };
            textBoxPassword.MouseEnter += textBoxPassword_MouseEnter;
            textBoxPassword.MouseLeave += textBoxPassword_MouseLeave;
            gridText.Children.Add(textBoxPassword);

            TextBox textBoxName = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 200,
                Height = 20,
                Background = new SolidColorBrush(Colors.Gray),
                Foreground = new SolidColorBrush(Colors.WhiteSmoke),
                Margin = new Thickness(0, 0, 0, 100),
                Text = "Введите имя",
                FontSize = 15
            };
            textBoxName.MouseEnter += textBoxName_MouseEnter;
            textBoxName.MouseLeave += textBoxName_MouseLeave;
            gridText.Children.Add(textBoxName);

            TextBox textBoxSurname = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 200,
                Height = 20,
                Background = new SolidColorBrush(Colors.Gray),
                Foreground = new SolidColorBrush(Colors.WhiteSmoke),
                Margin = new Thickness(0, 0, 0, 50),
                Text = "Введите фамилию",
                FontSize = 15
            };
            textBoxSurname.MouseEnter += textBoxSurname_MouseEnter;
            textBoxSurname.MouseLeave += textBoxSurname_MouseLeave;
            gridText.Children.Add(textBoxSurname);

            Button buttonRegister = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 150,
                Height = 20,
                Background = new SolidColorBrush(Colors.MediumAquamarine),
                Margin = new Thickness(0, 0, 0, -20),
                Content = "Зарегестрироваться",
                FontSize = 13,
                HorizontalContentAlignment = HorizontalAlignment.Center
            };
            buttonRegister.Click += buttonRegister_Click;
            gridText.Children.Add(buttonRegister);

            void textBoxLogin_MouseEnter(object sender1, MouseEventArgs e1)
            {
                if (textBoxLogin.Text == "Введите логин")
                {
                    textBoxLogin.Clear();
                    textBoxLogin.Foreground = new SolidColorBrush(Colors.White);
                }
            }

            void textBoxLogin_MouseLeave(object sender1, MouseEventArgs e1)
            {
                if (string.IsNullOrWhiteSpace(textBoxLogin.Text))
                {
                    textBoxLogin.Text = "Введите логин";
                    textBoxLogin.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                }
            }

            void textBoxPassword_MouseEnter(object sender1, MouseEventArgs e1)
            {
                if (textBoxPassword.Text == "Введите пароль")
                {
                    textBoxPassword.Clear();
                    textBoxPassword.Foreground = new SolidColorBrush(Colors.White);
                }
            }

            void textBoxPassword_MouseLeave(object sender1, MouseEventArgs e1)
            {
                if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
                {
                    textBoxPassword.Text = "Введите пароль";
                    textBoxPassword.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                }
            }

            void textBoxName_MouseEnter(object sender1, MouseEventArgs e1)
            {
                if (textBoxName.Text == "Введите имя")
                {
                    textBoxName.Clear();
                    textBoxName.Foreground = new SolidColorBrush(Colors.White);
                }
            }

            void textBoxName_MouseLeave(object sender1, MouseEventArgs e1)
            {
                if (string.IsNullOrWhiteSpace(textBoxName.Text))
                {
                    textBoxName.Text = "Введите имя";
                    textBoxName.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                }
            }

            void textBoxSurname_MouseEnter(object sender1, MouseEventArgs e1)
            {
                if (textBoxSurname.Text == "Введите фамилию")
                {
                    textBoxSurname.Clear();
                    textBoxSurname.Foreground = new SolidColorBrush(Colors.White);
                }
            }

            void textBoxSurname_MouseLeave(object sender1, MouseEventArgs e1)
            {
                if (string.IsNullOrWhiteSpace(textBoxSurname.Text))
                {
                    textBoxSurname.Text = "Введите фамилию";
                    textBoxSurname.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                }
            }

            void buttonRegister_Click(object sender1, EventArgs e1)
            {
                if (textBoxLogin.Text != "Введите логин" && textBoxPassword.Text != "Введите пароль" &&
                    textBoxName.Text != "Введите имя" && textBoxSurname.Text != "Введите фамилию")
                {
                    if (CheckDigits(textBoxName.Text) && CheckDigits(textBoxSurname.Text))
                    {
                        if (Storage.users.Count != 0)
                        {
                            ReadFile();

                            int counter = 1;
                            foreach (var user in Storage.users)
                            {
                                if (user.Login == textBoxLogin.Text)
                                {
                                    MessageBox.Show("Пользователь с таким логином уже существует, попробуйте снова.");
                                    break;
                                }
                                
                                if (counter == Storage.users.Count)
                                {
                                    string filePath = System.IO.Path.GetFullPath("../../Pictures/avatar.jpeg");
                                    User newUser = new User(textBoxName.Text, textBoxSurname.Text, 0, textBoxLogin.Text, textBoxPassword.Text, filePath);
                                    Storage.users.Add(newUser);

                                    if (MessageBox.Show("Вы зарегестрированы", "Оповещение", MessageBoxButton.OK) == MessageBoxResult.OK)
                                    {
                                        Storage.SaveInforamtion();

                                        UserInterface userInterface = new UserInterface(newUser);
                                        userInterface.Show();
                                        this.Close();
                                        break;
                                    }
                                }
                                counter++;
                            }
                        }
                        else
                        {
                            string filePath = System.IO.Path.GetFullPath("../../Pictures/avatar.jpeg");
                            User newUser = new User(textBoxName.Text, textBoxSurname.Text, 0, textBoxLogin.Text, textBoxPassword.Text, filePath);
                            Storage.users.Add(newUser);

                            if (MessageBox.Show("Вы зарегестрированы", "Оповещение", MessageBoxButton.OK) == MessageBoxResult.OK)
                            {
                                UserInterface userInterface = new UserInterface(newUser);
                                userInterface.Show();
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данные введены неверно: попробуйте снова.");
                    }
                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнены!");
                }
            }

            bool CheckDigits(string textBoxText)
            {
                foreach (char element in textBoxText)
                {
                    if (Char.IsNumber(element))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private void exit_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            gridText.Children.Clear();

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
            gridText.Children.Add(askBlock);

            Button buttonYes = new Button()
            {
                HorizontalAlignment= HorizontalAlignment.Left,
                VerticalAlignment= VerticalAlignment.Center,
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
            gridText.Children.Add(buttonYes);

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
            gridText.Children.Add(buttonNo);

            void buttonYes_Click(object sender1, RoutedEventArgs e1)
            {
                Storage.SaveInforamtion();
                this.Close();
            }

            void buttonNo_Click(object sender1, RoutedEventArgs e1)
            {
                gridText.Children.Clear();
            }
        }

        private void ReadFile()
        {
            Storage.CreatePath();
            Storage.ReadInformation();
        }
    }
}
