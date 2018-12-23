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
using System.Windows.Shapes;

namespace ImageManagerUI
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {

        ImageManageEntities db = new ImageManageEntities("metadata=res://*/ImageManagerModel.csdl|res://*/ImageManagerModel.ssdl|res://*/ImageManagerModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.81.100;initial catalog=ImageManage;persist security info=True;user id=ImageManagement;password=Letmein101;MultipleActiveResultSets=True;App=EntityFramework'");

        enum DBOperation
        {
            Add,
            Modify
        }


        
        public Registration()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //creates the variables needed for login
            bool checkEmailAdd = false;
            bool checkUserInput = false;
            string emailInput = txtEmail.Text.Trim();
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Password.Trim();
            bool validUser = false;

                   
            try
            {
                checkEmailAdd = CheckEmail(emailInput);
                checkUserInput = ValidateUserInput(username, password);
                //First check, if email is valid
                if (checkEmailAdd)
                {
                    validUser = true;
                }
                else
                {
                    MessageBox.Show("Not a valid Email Address", "User Registration", MessageBoxButton.OK);
                    validUser = false;
                }
                //then check if inputs are correct
                if (checkUserInput)
                {
                    validUser = true;
                }
                else
                {
                    MessageBox.Show("Problem with username or password", "User Registration", MessageBoxButton.OK);
                    validUser = false;
                }
                //if it passes both tests, create entry in db with low level access
                if (validUser)
                {
                    User user = new User();
                    user.Username = txtUserName.Text.Trim();
                    user.Password = txtPassword.Password.Trim();
                    user.Email = txtEmail.Text.Trim();
                    user.LevelID = 3;
                    int saveSuccess = SaveUser(user);
                    if (saveSuccess == 1)
                    {
                        MessageBox.Show("User added, please login", "Save to Database");
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        ClearUserDetails();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("User Not added", "Save to Database");
                    }
                }
            }
            catch 
            {
                MessageBox.Show("Issue with user registration", "User Registration", MessageBoxButton.OK);
            }
        }

        private void btnLoginCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            ClearUserDetails();
            this.Hide();
        }

        private void ClearUserDetails()
        {
            txtEmail.Text = "";
            txtUserName.Text = "";
            txtPassword.Clear();
            txtPassword2.Clear();
        }

        public int SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }
        /// <summary>
        /// Checks string for email address and returns a false value if its not
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Returns True if email, false if not</returns>
        private bool CheckEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch 
            {
                return false;                
            }
        }

        private bool ValidateUserInput(string username, string password)
        {
            //submitting a value of false, then applying true if it passes input checks
            bool validated = false;
            if (username.Length == 0 || username.Length > 30)
            {
                validated = true;
            }
            //checks each character in the username, looking for numbers and returning true
            foreach (char character in username)
            {
                if (character >= '0' && character <= '9')
                {
                    validated = true;
                }
            }
            //checking if the password length is greater then 6 and less then 30
            if (password.Length <= 6 || password.Length > 30)
            {
                validated = true;
            }
            return validated;
        }

    }
}
