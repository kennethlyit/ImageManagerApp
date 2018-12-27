using DBLibrary;
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
using System.Windows.Shapes;

namespace ImageManagerUI
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public User user = new User();
        public Dashboard()
        {
            InitializeComponent();
        }

        //private void btnAdminAdmin_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void btnAdminUsers_Click(object sender, RoutedEventArgs e)
        {
            Users usersPage = new Users();
            usersPage.validateduser = user;
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

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();

            
        }

        private void btnNewImage_Click(object sender, RoutedEventArgs e)
        {
            NewImage newImage = new NewImage();
            newImage.loggedInUser = user;
            frmMain.Navigate(newImage);
        }

        //Check User access level and make buttons visible, runs in XAML in dashboard app in Window Loaded
        private void CheckUserAccess(User user)
        {
            if (user.LevelID == 1) //graphic designer
            {
                btnNewImage.Visibility = Visibility.Visible;
            }
            if (user.LevelID == 2)  //admin user 
            {
                //btnAdminAdmin.Visibility = Visibility.Visible;
                btnAdminLogs.Visibility = Visibility.Visible;
                btnAdminUsers.Visibility = Visibility.Visible;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckUserAccess(user);
            ImageView imageView = new ImageView();
            frmMain.Navigate(imageView);
        }

        private void btnStartSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchRequest = txtSearchInput.Text.Trim();
                if (String.IsNullOrEmpty(searchRequest))
                {
                    MessageBox.Show("Nothing in SearchBox", "Dashboard Search", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    ImageView imagesearchview = new ImageView();
                    imagesearchview.ImageSearchString = searchRequest;
                    txtSearchInput.Clear();
                    frmMain.Navigate(imagesearchview);
                }
            }
            catch
            {

                MessageBox.Show("Search went wrong", "Dashboard Search", MessageBoxButton.OK);
            }

    

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            ImageView imageView = new ImageView();
            frmMain.Navigate(imageView);
        }

        public void ImageFrameChange()
        {
            ImageView imageView = new ImageView();
            frmMain.Navigate(imageView);
        }
    }
}
