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
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Page
    {
        ImageManagementDBEntities db = new ImageManagementDBEntities("metadata=res://*/ImageAppModel.csdl|res://*/ImageAppModel.ssdl|res://*/ImageAppModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=192.168.81.100;initial catalog=ImageManagementDB;persist security info=True;user id=ImageManagement;password=Letmein101;pooling=False;MultipleActiveResultSets=True;App=EntityFramework&quot;");
        List<User> users = new List<User>();

        public Users()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lstUsers.ItemsSource = users;
            foreach (var user in db.Users)
            {
                users.Add(user);
            }


        }
    }
}
