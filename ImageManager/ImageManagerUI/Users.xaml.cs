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
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Page
    {
        
        ImageManageEntities db = new ImageManageEntities("metadata=res://*/ImageManagerModel.csdl|res://*/ImageManagerModel.ssdl|res://*/ImageManagerModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.81.100;initial catalog=ImageManage;persist security info=True;user id=ImageManagement;password=Letmein101;MultipleActiveResultSets=True;App=EntityFramework'");

        List<User> users = new List<User>();

        
        public Users()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lstUsers.ItemsSource = users;
            /*string jobRoleDescript;*/
            foreach (var user in db.Users) 
            {
                users.Add(user);
                //todo: trying to populate access role in list
                //if (Convert.ToInt32(user.AccessLevel) == 1)
                //{
                //    jobRoleDescript = "Graphic Designer";
                //}
                //if (Convert.ToInt32(user.AccessLevel) == 2)
                //{
                //    jobRoleDescript = "Administrator";
                //}
                //if (Convert.ToInt32(user.AccessLevel) == 3)
                //{
                //    jobRoleDescript = "Operator";
                //}
            }  
            

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnaddUser_Click(object sender, RoutedEventArgs e)
        {
            //Create and populate user info into User class
            User user = new User();
            user.Username = txtUserName.Text.Trim();
            user.Password = txtPassword.Text.Trim();
            user.Email = txtEmail.Text.Trim();
            //Index Level matches Access Level values
            user.LevelID = cboAccessLevel.SelectedIndex;
            //Save the user
            SaveUser(user);
            stkNewUser.Visibility = Visibility.Collapsed;
            stkUsersList.Visibility = Visibility.Visible;

        }

        private void btncancelUser_Click(object sender, RoutedEventArgs e)
        {
            stkNewUser.Visibility = Visibility.Collapsed;
            stkUsersList.Visibility = Visibility.Visible;
        }


        private void mnuModifyUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnudDeleteUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void muNewUser_Click(object sender, RoutedEventArgs e)
        {
            //Make User table Visible and collaspe the add edit stuff
            stkNewUser.Visibility = Visibility.Visible;
            stkUsersList.Visibility = Visibility.Collapsed;
        }

        public void SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }
    }
}
