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
            string jobRoleDescript;
            foreach (var user in db.Users)
            {
                users.Add(user);
                if (Convert.ToInt32(user.AccessLevel) == 1)
                {
                    jobRoleDescript = "Graphic Designer";
                }
                if (Convert.ToInt32(user.AccessLevel) == 2)
                {
                    jobRoleDescript = "Administrator";
                }
                if (Convert.ToInt32(user.AccessLevel) == 3)
                {
                    jobRoleDescript = "Operator";
                }
            }  
            

        }

        private void MnuNewUser_Click(object sender, RoutedEventArgs e)
        {
            stkNewUser.Visibility = Visibility.Visible;
        }
    }
}
