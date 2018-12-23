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
        List<AccessLevel> accessLevels = new List<AccessLevel>();

        User selectedUser = new User();
        public User validateduser = new User();

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
            try
            {
                if (dbOperation == DBOperation.Add)
                {
                    User user = new User();
                    user.Username = txtUserName.Text.Trim();
                    user.Password = txtPassword.Text.Trim();
                    user.Email = txtEmail.Text.Trim();
                    //Index Level matches Access Level values
                    user.LevelID = cboAccessLevel.SelectedIndex;
                    //Feedback on button click
                    int saveSuccess = SaveUser(user);
                    if (saveSuccess == 1)
                    {
                        MessageBox.Show("User saved", "Save to Database",MessageBoxButton.OK);
                        //Create a log Record
                        CreateLogEntry(validateduser.UserID, (user.Username + "added by"), validateduser.Username);
                        //Reset the users list, bring back the list view
                        ClearUserDetails();
                        lstUsers.Visibility = Visibility.Visible;
                        stkNewUser.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MessageBox.Show("Save Failed", "Save to Database");
                    }
                    RefreshUserList();
                    
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
                        CreateLogEntry(validateduser.UserID, (selectedUser.Username + "modified by"), validateduser.Username);
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
            catch
            {
                MessageBox.Show("Issue with User Tables", "User Tables", MessageBoxButton.OK);
                
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
                CreateLogEntry(validateduser.UserID, (selectedUser.Username + "deleted by"), validateduser.Username);
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
            try
            {
                //clear both lists
                users.Clear();
                accessLevels.Clear();
                //poulate both lists
                foreach (var user in db.Users)
                {
                    users.Add(user);
                }
                foreach (var level in db.AccessLevels)
                {
                    accessLevels.Add(level);
                }
                //Do the join 
                var accessLevelName = from details in users
                                      join level in accessLevels on details.LevelID equals level.LevelID
                                      select new
                                      {
                                          details.Username,
                                          details.Password,
                                          details.Email,
                                          levelID = level.JobRole
                                      };
                lstUsers.ItemsSource = accessLevelName;
                lstUsers.Items.Refresh();
            }

            catch
            {
                MessageBox.Show("Something went wrong with logs retrieval", "Logs Retrieval", MessageBoxButton.OK);
            }
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
