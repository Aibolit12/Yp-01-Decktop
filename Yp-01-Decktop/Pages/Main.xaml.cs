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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        MainWindow mainWindow;
        public Main(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "По номеру заявки или параметрам") textBox.Text = "";
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text)) if (textBox == SearchText) textBox.Text = "По номеру заявки или параметрам";
        }


        public void Search(object sender, RoutedEventArgs e)
        {

        }


        public void Exit(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Pages.Authorization(mainWindow));
        }


        public void AddRequest(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Pages.RequestEdit(mainWindow));
        }
    }
}
