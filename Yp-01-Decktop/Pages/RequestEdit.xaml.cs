﻿using PdfSharp.Pdf.Content.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
    /// Логика взаимодействия для RequestEdit.xaml
    /// </summary>
    public partial class RequestEdit : Page
    {
        Classes.Request request;
        MainWindow mainWindow;
        public RequestEdit(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            if (Users.Role == "Клиент")
            {
                RequestClientEdit.Visibility = Visibility.Visible;
                Add.Visibility = Visibility.Visible;
                LoadTypeOfFaul();
            }
            else if (Users.Role == "Менеджер")
            {
                RequestManagerEdit.Visibility = Visibility.Visible;
                LoadPerformer();
            }
            else if (Users.Role == "Сотрудник")
            {
                RequestPerformerEdit.Visibility = Visibility.Visible;
                LoadStatus();
            }
        }
        public RequestEdit(MainWindow _mainWindow, Classes.Request _request)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            request = _request;
            name.Content = "Изменение заявки";
            if (Users.Role == "Клиент")
            {
                RequestClientEdit.Visibility = Visibility.Visible;
                UpDate.Visibility = Visibility.Visible;
                LoadTypeOfFaul();
                try
                {
                    string TypesOfFaults = request.TypeOfFault;
                    Equipment.Text = request.Equipment;
                    comboBoxTypesOfFaults.SelectedItem = TypesOfFaults;
                    Description.Text = request.Description;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (Users.Role == "Менеджер")
            {
                RequestManagerEdit.Visibility = Visibility.Visible;
                LoadPerformer();
                try
                {
                    string Performer = request.Performer;
                    comboBoxPerformer.SelectedItem = Performer;
                    EndDate.Text = request.EndDate;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (Users.Role == "Сотрудник")
            {
                RequestPerformerEdit.Visibility = Visibility.Visible;
                LoadStatus();
                try
                {
                    string Status = request.Status;
                    comboBoxStatus.SelectedItem = Status;
                    CommentPerformer.Text = request.PerformerComment;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void LoadTypeOfFaul()
        {
            try
            {
                DataTable item = Classes.DataBase.Select($"select * from [TypeOfFault]");
                foreach (DataRow row in item.Rows)
                {
                    comboBoxTypesOfFaults.Items.Add(row[1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadStatus()
        {
            try
            {
                DataTable item = Classes.DataBase.Select($"select * from [Status]");
                foreach (DataRow row in item.Rows)
                {
                    comboBoxStatus.Items.Add(row[1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadPerformer()
        {
            try
            {
                DataTable item = Classes.DataBase.Select($"select FIO from [Users] where Role = '{"Сотрудник"}'");
                foreach (DataRow row in item.Rows)
                {
                    comboBoxPerformer.Items.Add(row[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Введите оборудование" || textBox.Text == "Введите описание проблемы" || textBox.Text == "Введите комментарий") textBox.Text = "";
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox == Equipment) textBox.Text = "Введите оборудование";
                else if (textBox == Description) textBox.Text = "Введите описание проблемы";
                else if (textBox == CommentPerformer) textBox.Text = "Введите комментарий";
            }
        }
        public void AddRequestClient(object sender, RoutedEventArgs e)
        {
            if (Equipment.Text.Length != 0 && comboBoxTypesOfFaults.SelectedIndex != -1 && Description.Text.Length != 0)
            {
                try
                {
                    DateTime StartDate = DateTime.Today;
                    string formattedDate = StartDate.ToShortDateString();
                    Random rand = new Random();
                    const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    Random random = new Random();
                    string Number = new string(Enumerable.Repeat(allowedChars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
                    int ID = 0;
                    string TypeOfFaults;
                    TypeOfFaults = comboBoxTypesOfFaults.SelectedItem.ToString();
                    DataTable item = Classes.DataBase.Select($"select Id from [TypeOfFault] where Name = '{TypeOfFaults}'");
                    foreach (DataRow row in item.Rows)
                    {
                        ID = Convert.ToInt32(row[0]);
                    }
                    DataTable result = Classes.DataBase.Select($"insert into [Requests] (Number,StartDate,Equipment,TypeOfFault,Description,Client,Status) values ('{Number}','{formattedDate}','{Equipment.Text}','{ID}','{Description.Text}','{Users.Id}','{1}')");
                    mainWindow.LoadItem();
                    MessageBox.Show("Заявка успешно добавлена");
                    mainWindow.frame.Navigate(new Pages.Main(mainWindow));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("Заполните все поля");
            
        }
        public void UpDateRequestClient(object sender, RoutedEventArgs e)
        {
            if (Equipment.Text.Length != 0 && comboBoxTypesOfFaults.SelectedIndex != -1 && Description.Text.Length != 0)
            {
                try
                {
                    int ID = 0;
                    string TypeOfFaults;
                    TypeOfFaults = comboBoxTypesOfFaults.SelectedItem.ToString();
                    DataTable item = Classes.DataBase.Select($"select Id from [TypeOfFault] where Name = '{TypeOfFaults}'");
                    foreach (DataRow row in item.Rows)
                    {
                        ID = Convert.ToInt32(row[0]);
                    }
                    DataTable result = Classes.DataBase.Select($"UPDATE [Requests] SET Equipment = '{Equipment.Text}',[TypeOfFault] = '{ID}', Description = '{Description.Text}' where Id = '{request.Id}'");
                    MessageBox.Show("Заявка успешно обновлена");
                    mainWindow.LoadItem();
                    mainWindow.frame.Navigate(new Pages.Main(mainWindow));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("Заполните все поля");
        }

        public void UpDateRequestManager(object sender, RoutedEventArgs e)
        {
            try
            {
                string Performer;
                int ID = 0;
                Performer = comboBoxPerformer.SelectedItem.ToString();
                DataTable item = Classes.DataBase.Select($"select Id from [Users] where FIO = '{Performer}'");
                foreach (DataRow row in item.Rows)
                {
                    ID = Convert.ToInt32(row[0]);
                }
                DataTable result = Classes.DataBase.Select($"UPDATE [Requests] SET Performer = '{ID}',[EndDate] = '{EndDate.Text}' where Id = '{request.Id}'");
                MessageBox.Show("Заявка успешно обновлена");
                mainWindow.LoadItem();
                mainWindow.frame.Navigate(new Pages.Main(mainWindow));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void UpDateRequestPerformer(object sender, RoutedEventArgs e)
        {
            try
            {
                string Status;
                int ID = 0;
                Status = comboBoxStatus.SelectedItem.ToString();
                DataTable item = Classes.DataBase.Select($"select Id from [Status] where Name = '{Status}'");
                foreach (DataRow row in item.Rows)
                {
                    ID = Convert.ToInt32(row[0]);
                }
                DataTable result = Classes.DataBase.Select($"UPDATE [Requests] SET Status = '{ID}',[PerformerComment] = '{CommentPerformer.Text}' where Id = '{request.Id}'");
                MessageBox.Show("Заявка успешно обновлена");
                mainWindow.LoadItem();
                mainWindow.frame.Navigate(new Pages.Main(mainWindow));
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
