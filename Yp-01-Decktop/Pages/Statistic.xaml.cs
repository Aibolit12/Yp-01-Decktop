using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Statistic.xaml
    /// </summary>
    public partial class Statistic : Page
    {
        MainWindow mainWindow;
        public Statistic(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            DataTable query = Classes.DataBase.Select("SELECT COUNT(*) FROM Requests;");
            CountRequest.Content = "Количество выполненных заявок: " + query.Rows[0][0].ToString();
        }
        public void TransitionBack(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Pages.Main(mainWindow));
        }
    }

}
