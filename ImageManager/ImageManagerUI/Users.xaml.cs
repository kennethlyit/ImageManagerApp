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

        User selectedUser = new User();

        enum DBOperation
        {
            Add,
            Modify
        }


        DBOperation dbOperation = new DBOperation();




        public Users()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUserList();
            //sets access level to 0 on page load
            cboAccessLevel.SelectedIndex = 0;

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnaddUser_Click(object sender, RoutedEventArgs e)
        {

            //Create and populate user info into User class
            if (dbOperation == DBOperation.Add)
            {
                User user = new User();
                user.Username = txtUserName.Text.Trim();
                user.Password = txtPassword.Text.Trim();
                user.Email = txtEmail.Text.Trim();
                //Index Level matches Access Level values
                user.LevelID = cboAccessLevel.SelectedIndex;
                //Save the user
                //SaveUser(user);

                //Feedback on button click
                int saveSuccess = SaveUser(user);
                if (saveSuccess == 1)
                {
                    MessageBox.Show("User saved", "Save to Database");

                    ClearUserDetails();
                    lstUsers.Visibility = Visibility.Visible;
                    stkNewUser.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Save Failed", "Save to Database");
                }
                RefreshUserList();
                //TODO: Need to add logging here too
            }
            if (dbOperation == DBOperation.Modify)
            {
                foreach (var user in db.Users.Where(t => t.UserID == selectedUser.UserID))
                {
                    user.Username = txtUserName.Text.Trim();
                    user.Email = txtEmail.Text.Trim();
                    user.Password = txtPassword.Text.Trim();
                    user.LevelID = cboAccessLevel.SelectedIndex;
                }
                int saveSuccess = db.SaveChanges();
                if (saveSuccess == 1)
                {
                    MessageBox.Show("User saved", "Save to Database");

                    ClearUserDetails();
                    stkNewUser.Visibility = Visibility.Collapsed;
                    lstUsers.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Save Failed", "Save to Database");
                }
                ClearUserDetails();
            }

        }

        private void btncancelUser_Click(object sender, RoutedEventArgs e)
        {
            stkNewUser.Visibility = Visibility.Collapsed;
            lstUsers.Visibility = Visibility.Visible;
            ClearUserDetails();
        }


        private void mnuModifyUser_Click(object sender, RoutedEventArgs e)
        {

            stkNewUser.Visibility = Visibility.Visible;
            lstUsers.Visibility = Visibility.Collapsed;
            dbOperation = DBOperation.Modify;
        }

        private void mnudDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            db.Users.RemoveRange(db.Users.Where(t => t.UserID == selectedUser.UserID));
            int saveSucess = db.SaveChanges();
            if (saveSucess == 1)
            {
                RefreshUserList();
                ClearUserDetails();
                MessageBox.Show("User Deleted", "Save to Database");
            }
            else
            {
                MessageBox.Show("Save Failed", "Save to Database");
            }



        }

        private void muNewUser_Click(object sender, RoutedEventArgs e)
        {
            //Make User table Visible and collaspe the add edit stuff
            dbOperation = DBOperation.Add;
            lstUsers.Visibility = Visibility.Collapsed;
            stkNewUser.Visibility = Visibility.Visible;

        }

        public int SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }

        private void RefreshUserList()
        {
            lstUsers.ItemsSource = users;
            users.Clear();
            foreach (var user in db.Users)
            {
                users.Add(user);
            }
            lstUsers.Items.Refresh();
        }

        private void ClearUserDetails()
        {
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";
            cboAccessLevel.SelectedIndex = 0;
        }

        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUsers.SelectedIndex > 0)
            {
                selectedUser = users.ElementAt(lstUsers.SelectedIndex);
                if (selectedUser.UserID > 0)
                {
                    mnudDeleteUser.IsEnabled = true;
                    mnuModifyUser.IsEnabled = true;
                    txtEmail.Text = selectedUser.Email;
                    txtPassword.Text = selectedUser.Password;
                    txtUserName.Text = selectedUser.Username;
                    cboAccessLevel.SelectedIndex = selectedUser.LevelID;
                }
            }


        }

        private void cboAccessLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var theComboBox = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)theComboBox.SelectedItem;
            string value = item.Content.ToString();
            MessageBox.Show("Content of combobox is " + value);
        }
    }
}
