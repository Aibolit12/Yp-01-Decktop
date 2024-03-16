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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Yp_01_Decktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        MainWindow mainWindow;
        public Authorization(MainWindow _mainWindo)
        {
            InitializeComponent();
            mainWindow = _mainWindo;
        }
        public void ToComeIn(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Pages.Main(mainWindow));
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Введите логин" || textBox.Text == "Введите пароль") textBox.Text = "";
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text)) 
            {
                if (textBox == Login) textBox.Text = "Введите логин";
                else if (textBox == Password) textBox.Text = "Введите пароль";
            }
        }


        public void TransitionRegistration(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Pages.Registration(mainWindow));
        }
    }
}
