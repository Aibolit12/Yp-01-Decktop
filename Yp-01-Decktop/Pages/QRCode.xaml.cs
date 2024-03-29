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
    /// Логика взаимодействия для QRCode.xaml
    /// </summary>
    public partial class QRCode : Page
    {
        MainWindow mainWindow;
        public QRCode(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            LoadQRCOde();
        }
        private void LoadQRCOde()
        {
            try
            {
                ImageQRCode.Source = Classes.QRCode.CreateBitmapCode("https://forms.gle/a3tZf9fCMtEh57Lu7");
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
