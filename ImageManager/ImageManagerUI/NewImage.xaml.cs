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
    /// Interaction logic for NewImage.xaml
    /// </summary>
    public partial class NewImage : Page
    {
        ImageManageEntities db = new ImageManageEntities("metadata=res://*/ImageManagerModel.csdl|res://*/ImageManagerModel.ssdl|res://*/ImageManagerModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.81.100;initial catalog=ImageManage;persist security info=True;user id=ImageManagement;password=Letmein101;MultipleActiveResultSets=True;App=EntityFramework'");

        


        public NewImage()
        {
            InitializeComponent();
        }

        private void cbovendor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbousedIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnaddImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBLibrary.Image images = new DBLibrary.Image();
                images.ImageName = txtImageName.Text.Trim();
                images.Notes = txtImageNotes.Text.Trim();


            }
            catch 
            {

                
            }
        }

        private void btncancelImage_Click(object sender, RoutedEventArgs e)
        {

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

