﻿using System;
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
using Yp_01_Decktop.Classes;

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
            try
            {
                DataTable query = Classes.DataBase.Select($"SELECT COUNT(*) FROM Requests where Status = '{2}';");
                CountRequest.Content = "Количество выполненных заявок: " + query.Rows[0][0].ToString();
                int complete = 0;
                TimeSpan totalTime = new TimeSpan();
                foreach (var row in mainWindow.RequestItem)
                {
                    if (row.Status == "Готово")
                    {
                        complete++;
                        totalTime += DateTime.Parse(row.EndDate) - DateTime.Parse(row.StartDate);
                    }
                }
                if (totalTime.Days == 0) AvarageTime.Content = "Среднее время выполнения заявки: " + totalTime.Days;
                else AvarageTime.Content = "Среднее время выполнения заявки: " + totalTime.Days / complete + " дней";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void TransitionBack(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Pages.Main(mainWindow));
        }
    }

}
