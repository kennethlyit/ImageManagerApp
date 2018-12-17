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

        

        public Registration()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Username = txtUserName.Text.Trim();
            user.Password = txtPassword.Text.Trim();
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
    }
}
