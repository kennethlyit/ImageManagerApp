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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DBLibrary;

namespace ImageManagerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImageManageEntities db = new ImageManageEntities("metadata=res://*/ImageManagerModel.csdl|res://*/ImageManagerModel.ssdl|res://*/ImageManagerModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.81.100;initial catalog=ImageManage;persist security info=True;user id=ImageManagement;password=Letmein101;MultipleActiveResultSets=True;App=EntityFramework'");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            User validatedUser = new User();
            bool login = false;
            string currentUser = txtUserName.Text;
            string currentPassword = txtPassword.Password;
            //need to find out wtf is going on here, it won't let me in with correct credentials
            foreach (var user in db.Users)
            {
                if (user.Username == currentUser && user.Password == currentPassword)
                {
                    login = true;
                    validatedUser = user;
                }
                else
                {
                    lblloginError.Content = "Please Check your username or Password";
                    //Dashboard dashboard = new Dashboard();
                    //dashboard.ShowDialog();
                    //this.Hide();
                }
                
            }
            if (login)
            {
                CreateLogEntry(validatedUser.UserID, "User Logged In", validatedUser.Username);
                Dashboard dashboard = new Dashboard();
                dashboard.user = validatedUser;
                dashboard.Owner = this;
                this.Hide();
                dashboard.ShowDialog();
                            }
            else
            {
                //TODO:Why does this fail, is it the int not getting passed as a INT?
                CreateLogEntry(2, "Failed Login", "Admin");
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
            registration.ShowDialog();
            this.Hide();
        }



        private void CreateLogEntry(int userID, string description, string userName)
        {
            string comment = $"{description} user = {userName}";
            Log log = new Log();
            log.UserID = userID;
            log.Description = comment;
            log.Date = DateTime.Now;
            SaveLog(log);
        }

        private void SaveLog(Log log)
        {
            db.Entry(log).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }
    }
}
