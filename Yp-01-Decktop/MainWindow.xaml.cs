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
using static System.Net.Mime.MediaTypeNames;

namespace Yp_01_Decktop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Request> RequestItem = new List<Request>();
        public List<TypeOfFault> TypeOfFaultList = new List<TypeOfFault>();
        public MainWindow()
        {
            InitializeComponent();
            this.frame.Navigate(new Pages.Authorization(this));
        }
        public void LoadItem()
        {
            try
            {
                if(Users.Role == "Клиент")
                {
                    try
                    {
                        RequestItem.Clear();
                        DataTable itemuser = Classes.DataBase.Select($"SELECT Requests.Id, Requests.Number, Requests.StartDate, Requests.EndDate, Requests.Equipment, TypeOfFault.Name AS TypeOfFault, Requests.Description, Users.FIO AS Client, Users2.FIO AS Performer, Status.Name AS Status, Requests.PerformerComment FROM Requests JOIN TypeOfFault ON Requests.TypeOfFault = TypeOfFault.Id JOIN Users ON Requests.Client = Users.Id JOIN Users Users2 ON Requests.Performer = Users2.Id JOIN Status ON Requests.Status = Status.Id WHERE Requests.Client = '{Users.Id}';");
                        foreach (DataRow row in itemuser.Rows)
                        {
                            Classes.Request request = new Classes.Request
                            {
                                Id =  Convert.ToInt32(row["Id"]),
                                Number = row["Number"].ToString(),
                                StartDate = row["StartDate"].ToString(),
                                EndDate = row["EndDate"].ToString(),
                                Equipment = row["Equipment"].ToString(),
                                TypeOfFault = row["TypeOfFault"].ToString(),
                                Description = row["Description"].ToString(),
                                Client = row["Client"].ToString(),
                                Performer = row["Performer"].ToString(),
                                Status = row["Status"].ToString(),
                                PerformerComment = row["PerformerComment"].ToString()
                            };
                            RequestItem.Add(request);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if(Users.Role == "Менеджер")
                {
                    try
                    {
                        RequestItem.Clear();
                        DataTable item = Classes.DataBase.Select($"SELECT Requests.Id, Requests.Number, Requests.StartDate, Requests.EndDate, Requests.Equipment, TypeOfFault.Name AS TypeOfFault, Requests.Description, Users.FIO AS Client, Users2.FIO AS Performer, Status.Name AS Status, Requests.PerformerComment FROM Requests JOIN TypeOfFault ON Requests.TypeOfFault = TypeOfFault.Id JOIN Users ON Requests.Client = Users.Id LEFT JOIN Users Users2 ON Requests.Performer = Users2.Id JOIN Status ON Requests.Status = Status.Id;");

                        foreach (DataRow row in item.Rows)
                        {
                            Classes.Request request = new Classes.Request
                            {
                                Id = Convert.ToInt32(row["Id"]),
                                Number = row["Number"].ToString(),
                                StartDate = row["StartDate"].ToString(),
                                EndDate = row["EndDate"].ToString(),
                                Equipment = row["Equipment"].ToString(),
                                TypeOfFault = row["TypeOfFault"].ToString(),
                                Description = row["Description"].ToString(),
                                Client = row["Client"].ToString(),
                                Performer = row["Performer"].ToString(),
                                Status = row["Status"].ToString(),
                                PerformerComment = row["PerformerComment"].ToString()
                            };
                            RequestItem.Add(request);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if(Users.Role == "Сотрудник")
                {
                    try
                    {
                        RequestItem.Clear();
                        DataTable item = Classes.DataBase.Select($"SELECT Requests.Id, Requests.Number, Requests.StartDate, Requests.EndDate, Requests.Equipment, TypeOfFault.Name AS TypeOfFault, Requests.Description, Users.FIO AS Client, Users2.FIO AS Performer, Status.Name AS Status, Requests.PerformerComment FROM Requests JOIN TypeOfFault ON Requests.TypeOfFault = TypeOfFault.Id JOIN Users ON Requests.Client = Users.Id JOIN Users Users2 ON Requests.Performer = Users2.Id JOIN Status ON Requests.Status = Status.Id where Performer = '{Users.Id}'");

                        foreach (DataRow row in item.Rows)
                        {
                            Classes.Request request = new Classes.Request
                            {
                                Id = Convert.ToInt32(row["Id"]),
                                Number = row["Number"].ToString(),
                                StartDate = row["StartDate"].ToString(),
                                EndDate = row["EndDate"].ToString(),
                                Equipment = row["Equipment"].ToString(),
                                TypeOfFault = row["TypeOfFault"].ToString(),
                                Description = row["Description"].ToString(),
                                Client = row["Client"].ToString(),
                                Performer = row["Performer"].ToString(),
                                Status = row["Status"].ToString(),
                                PerformerComment = row["PerformerComment"].ToString()
                            };
                            RequestItem.Add(request);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
