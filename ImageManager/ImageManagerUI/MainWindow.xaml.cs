using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
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

        LoginProcess loginProcess = new LoginProcess();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Create the user 
            User validatedUser = new User();
            //Intialses the checks
            bool credentialsValidated = false;
            //Create two strings for the username and password for use later
            string currentUser = txtUserName.Text;
            string currentPassword = txtPassword.Password;
            //Puts both strings into bool credentials validated, false is a problem.
            credentialsValidated = loginProcess.ValidateUserInput(currentUser, currentPassword);
            //checks the bool value and returns error in the bool is false, caused by no meeting requirements
            if (credentialsValidated)
            {
                //checks validated user against the user db and returns that user if it matches
                validatedUser = GetUserRecord(currentUser, currentPassword);
                if (validatedUser.UserID > 0)
                {
                    //creates a log, moves to the dashboard window, puts validated user into the dashboard.user User, hides the window 
                    CreateLogEntry(validatedUser.UserID, "User Logged In", validatedUser.Username);
                    ShowDashboard(validatedUser);
                }
                else
                {
                    //TODO: I'd like a log message here, but its tied to userID. 
                    lblloginError.Content = "Please Check your username or Password";
                }
            }
            else
            {
                lblloginError.Content = "Username or Password does not meet minumum requirements";
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


        private User GetUserRecord(string username, string password)
        {
            User validatedUser = new User();
            try
            {
                
                foreach (var user in db.Users.Where(t => t.Username == username && t.Password == password))
                {
                    validatedUser = user;
                }
            }
            catch (EntityException)
            {
                MessageBox.Show("Problem Connecting to Database. Contact your it administrator", "Connect to Datase", MessageBoxButton.OK);
                this.Close();
                Environment.Exit(0);
                throw;
            }
            return validatedUser;
        }

        private void ShowDashboard(User validatedUser)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.user = validatedUser;
            dashboard.Owner = this;
            this.Hide();
            dashboard.ShowDialog();
        }
    }
}
