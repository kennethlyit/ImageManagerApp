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
using System.Windows.Shapes;

namespace ImageManagerUI
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnAdminAdmin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdminUsers_Click(object sender, RoutedEventArgs e)
        {
            Users usersPage = new Users();
            frmMain.Navigate(usersPage);
        }

        private void btnAdminLogs_Click(object sender, RoutedEventArgs e)
        {
            Logs logsPage = new Logs();
            frmMain.Navigate(logsPage);
        }

        private void btnAdminReports_Click(object sender, RoutedEventArgs e)
        {
            Reports reportsPage = new Reports();
            frmMain.Navigate(reportsPage);
        }
    }
}