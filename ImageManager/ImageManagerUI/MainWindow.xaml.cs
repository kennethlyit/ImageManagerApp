using System;
using DBLibrary;
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


namespace ImageManagerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImageManagementDBEntities db = new ImageManagementDBEntities("metadata=res://*/ImageAppModel.csdl|res://*/ImageAppModel.ssdl|res://*/ImageAppModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.81.100;initial catalog=ImageManagementDB;persist security info=True;user id=ImageManagement;password=Letmein101;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");

        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string currentUser = txtUserName.Text;
            string currentPassword = txtPassword.Password;
            foreach (var user in db.User)
                {
                    if (user.Username == currentUser && user.Password == currentPassword)
                    {
                        Dashboard dashboard = new Dashboard();
                        dashboard.Owner = this;
                        dashboard.user = user;
                        dashboard.ShowDialog();
                        this.Hide();
                    }
                    else
                    {
                        lvlErrorMessage.Content = "Username or password is incorrect";
                    }
                }


        }

        private void btnLoginCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Owner = this;
            registration.ShowDialog();
            this.Hide();
        }
    }
}
