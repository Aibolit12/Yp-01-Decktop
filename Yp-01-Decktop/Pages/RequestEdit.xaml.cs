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

namespace Yp_01_Decktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для RequestEdit.xaml
    /// </summary>
    public partial class RequestEdit : Page
    {
        MainWindow mainWindow;
        public RequestEdit(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Введите оборудование" || textBox.Text == "Введите описание проблемы") textBox.Text = "";
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox == Equipment) textBox.Text = "Введите оборудование";
                else if (textBox == Description) textBox.Text = "Введите описание проблемы";
            }
        }
        public void AddRequest(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Pages.Main(mainWindow));
        }
        public void TransitionBack(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Pages.Main(mainWindow));
        }
    }
}
