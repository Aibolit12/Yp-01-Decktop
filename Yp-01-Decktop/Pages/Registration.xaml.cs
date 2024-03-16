using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Yp_01_Decktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        MainWindow mainWindow;
        public Registration(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Введите логин" || textBox.Text == "Введите пароль" || textBox.Text == "Введите ФИО") textBox.Text = "";
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox == Login) textBox.Text = "Введите логин";
                else if (textBox == Password) textBox.Text = "Введите пароль";
                else if (textBox == FIO) textBox.Text = "Введите ФИО";
            }
        }
        public void Registr(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Length != 0 && Password.Text.Length != 0 && FIO.Text.Length != 0)
            {
                try
                {
                    Classes.DataBase.Select($"insert into [Users] (Login,Password, FIO, Role ) values ('{Login.Text}','{Password.Text}','{FIO.Text}', '{"Клиент"}')");
                    mainWindow.frame.Navigate(new Pages.Authorization(mainWindow));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("Заполните все поля");
        }
        public void TransitionBack(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Pages.Authorization(mainWindow));
        }
    }
}
