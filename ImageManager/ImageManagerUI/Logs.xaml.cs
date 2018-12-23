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
    /// Interaction logic for Logs.xaml
    /// </summary>
    public partial class Logs : Page
    {

        ImageManageEntities db = new ImageManageEntities("metadata=res://*/ImageManagerModel.csdl|res://*/ImageManagerModel.ssdl|res://*/ImageManagerModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.81.100;initial catalog=ImageManage;persist security info=True;user id=ImageManagement;password=Letmein101;MultipleActiveResultSets=True;App=EntityFramework'");

        List<Log> logs = new List<Log>();
        List<User> users = new List<User>();
        


        public Logs()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //this cause the list to populate on page load, might consider adding a rightclick refresh sub function if I have time
            RefreshLogsList();
        }

        private void RefreshLogsList()
        {
            try
            {
                //Populates logs list
                logs.Clear();
                foreach (var log in db.Logs)
                {
                    logs.Add(log);
                }
                //populates users field
                users.Clear();
                foreach (var user in db.Users)
                {
                    users.Add(user);
                }
                //Does a inner join, creating joined values using the userID as the common point
                var getUsername = from id in logs
                                  join name in users on id.UserID equals name.UserID
                                  select new
                                  {
                                      id.UserID,
                                      id.Description,
                                      id.Date,
                                      userName = name.Username
                                  };
                //Tells the lst in XAML to use the new variable as the source
                lstLogs.ItemsSource = getUsername;
                //Populates it
                lstLogs.Items.Refresh();
            }
            catch 
            {
                MessageBox.Show("Something went wrong with logs retrieval", "Logs Retrieval", MessageBoxButton.OK);
            }
            
        }

        
    }


}
