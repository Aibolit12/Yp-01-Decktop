﻿using System;
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
using Yp_01_Decktop.Classes;
using Yp_01_Decktop.Items;

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
            mainWindow.LoadItem();
            Load();
            if (Users.Role == "Клиент")
            {
                AddRequstClient.Visibility = Visibility.Visible;
                QRCodeClient.Visibility = Visibility.Visible;
            }
            else if (Users.Role == "Менеджер")
            {
                StatisticManager.Visibility = Visibility.Visible;
            } 
        }
        public void Load()
        {
            pagesListBox.Items.Clear();
            foreach (var request in mainWindow.RequestItem)
            {
                RequestItem requestControl = new RequestItem(mainWindow, request);
                requestControl.DataContext = request;
                pagesListBox.Items.Add(requestControl);
                if(Users.Role == "Клиент")
                {
                    DateTime StartDatee = DateTime.Today;
                    string formattedDate = StartDatee.ToShortDateString();
                    if (request.Status == "Готово") MessageBox.Show(request.Number + " Заявка готова");
                    else if (request.EndDate == formattedDate) MessageBox.Show(request.Number + " Заявка готова");
                }
            }
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
            mainWindow.LoadItem();
            Load();
            if (SearchText.Text.Length > 0 && SearchText.Text != "По номеру заявки или параметрам")
            {
                try
                {
                    mainWindow.RequestItem = mainWindow.RequestItem.Where(x => x.Number.ToLower().Contains(SearchText.Text.ToLower())).ToList();
                    pagesListBox.Items.Clear();
                    foreach (var item in mainWindow.RequestItem)
                    {
                        RequestItem pageControl = new RequestItem(mainWindow, item);
                        pageControl.DataContext = item;
                        pagesListBox.Items.Add(pageControl);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void TransitionAddComment(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Pages.RequestEdit(mainWindow));
        }

        public void TransitionQRCode(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Pages.QRCode(mainWindow));
        }

        public void TransitionStatistic(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Pages.Statistic(mainWindow));
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
