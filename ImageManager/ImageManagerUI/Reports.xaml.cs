using System;
using DBLibrary;
using System.Globalization;
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
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        ImageManageEntities db = new ImageManageEntities("metadata=res://*/ImageManagerModel.csdl|res://*/ImageManagerModel.ssdl|res://*/ImageManagerModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.81.100;initial catalog=ImageManage;persist security info=True;user id=ImageManagement;password=Letmein101;MultipleActiveResultSets=True;App=EntityFramework'");

        List<User> userlist = new List<User>();
        List<DBLibrary.Image> imagelist = new List<DBLibrary.Image>();
        List<Log> logs = new List<Log>();

        enum ReportsType
        {
            Summary,
            Count,
            Statistics
        }

        ReportsType reportsType = new ReportsType();

        enum DatabaseType
        {
            Image,
            Logs,
            Users,
        }

        DatabaseType databaseType = new DatabaseType();

        public Reports()
        {
            InitializeComponent();
        }

        private void btnAnalyse_Click(object sender, RoutedEventArgs e)
        {
            //Variables for users
            int graphicCount = 0;
            int adminCount = 0;
            int opCount = 0;
            //Variables for Images
            float gettyCount = 0;
            float iStockCount = 0;
            float shutterCount = 0;
            //Total Record count
            int recordcount = 0;
            //eventual output
            string output = "";
            tbkResultsOutput.Text = "";
            //Images Sumary 1 of 9
            if (reportsType == ReportsType.Summary && databaseType == DatabaseType.Image)
            {
                foreach (var item in imagelist)
                {
                    recordcount++;
                    if (item.VendorID == 1)
                    {
                        gettyCount++;
                    }
                    if (item.VendorID == 2)
                    {
                        iStockCount++;
                    }
                    if (item.VendorID == 3)
                    {
                        shutterCount++;
                    }
                     
                }
                output = output + Environment.NewLine + $"There are {gettyCount} images purchased from Getty" + Environment.NewLine + $"" +
                    $"There are {iStockCount} images purchased from IStock Photos " + Environment.NewLine + $"" +
                    $"There are {shutterCount} images purchased from ShutterStock" + Environment.NewLine;
                output = output + Environment.NewLine + $"Total number of images is {recordcount}" + Environment.NewLine;
                tbkResultsOutput.Text = output;
            }

            //Images Count 2 of 9
            if (reportsType == ReportsType.Count && databaseType == DatabaseType.Image)
            {
                foreach (var item in imagelist)
                {
                    recordcount++;
                    output = output + Environment.NewLine + $"Record {recordcount} is for image {item.ImageName} which is used in {item.UseCase.UseCaseDescription} " + Environment.NewLine;
                }
                tbkResultsOutput.Text = output;
            }
            //Images Stats 3 of 9
            if (reportsType == ReportsType.Statistics && databaseType == DatabaseType.Image)
            {
                foreach (var item in imagelist)
                {
                    recordcount++;
                    if (item.VendorID == 1)
                    {
                        gettyCount++;
                    }
                    if (item.VendorID == 2)
                    {
                        iStockCount++;
                    }
                    if (item.VendorID == 3)
                    {
                        shutterCount++;
                    }
                }
                gettyCount = (gettyCount / recordcount) * 100;
                iStockCount = (iStockCount / recordcount) * 100;
                shutterCount = (shutterCount / recordcount) * 100;
                output = $"Percentage of total images bought from services" + Environment.NewLine + $"" +
                    $"Getty Images is {gettyCount}% of the total" + Environment.NewLine +
                    $"iStockPhoto is {iStockCount}% of the total" + Environment.NewLine +
                    $"Shutter Stock is {shutterCount}% of the total" + Environment.NewLine;
                tbkResultsOutput.Text = output;
            }

            //Users Summary 4 of 9 options
            if (reportsType == ReportsType.Summary && databaseType == DatabaseType.Users)
            {
                foreach (var item in userlist)
                {
                    
                    recordcount++;
                    if (item.LevelID == 1)
                    {
                        graphicCount++;
                    }
                    if (item.LevelID == 2)
                    {
                        adminCount++;
                    }
                    if (item.LevelID == 3)
                    {
                        opCount++;
                    }
                    
                }
                output = output + Environment.NewLine + $"There are {graphicCount} graphic designers registered" + Environment.NewLine +
                        $"There are {adminCount} Admins Registered" + Environment.NewLine + $"There are {opCount} Operations Users registered" + Environment.NewLine + 
                        $"There are {recordcount} users in total"
                        ;
                tbkResultsOutput.Text = output;

            }
            //Users Count, 5 of 9 options
            if (reportsType == ReportsType.Count && databaseType == DatabaseType.Users)
            {
                foreach (var item in userlist)
                {

                    recordcount++;
                    output = output + Environment.NewLine + $"Record{recordcount}: {item.Username} has been assigned as a {item.AccessLevel.JobRole}";
                    if (item.LevelID == 1)
                    {
                        graphicCount++;
                    }
                    if (item.LevelID == 2)
                    {
                        adminCount++;
                    }
                    if (item.LevelID == 3)
                    {
                        opCount++;
                    }

                }
                tbkResultsOutput.Text = output;

            }
            //Users stats, 6 of 9
            if (reportsType == ReportsType.Statistics && databaseType == DatabaseType.Users)
            {

            }
            //Logs Summary, 7 of 9
            if (reportsType == ReportsType.Summary && databaseType == DatabaseType.Logs)
            {
                //TODO:
                //foreach (var item in logs)
                //{
                //    var month = TimeSpan.FromDays(30);
                //    var monthcount = 0;
                //    var week = TimeSpan.FromDays(7);
                //    var weekcount = 0;
                //    recordcount++;
                //    if (item.Date.GetDateTimeFormats < TimeSpan.FromDays(30)) 
                //    {

                //    }
                //    if (item.VendorID == 2)
                //    {
                //        iStockCount++;
                //    }
                //    if (item.VendorID == 3)
                //    {
                //        shutterCount++;
                //    }

                //}
                output = output + Environment.NewLine + $"There are {gettyCount} images purchased from Getty" + Environment.NewLine + $"" +
                    $"There are {iStockCount} images purchased from IStock Photos " + Environment.NewLine + $"" +
                    $"There are {shutterCount} images purchased from ShutterStock" + Environment.NewLine;
                output = output + Environment.NewLine + $"Total number of images is {recordcount}" + Environment.NewLine;
                tbkResultsOutput.Text = output;
            }
            //Logs Summary, 8 of 9
            if (reportsType == ReportsType.Count && databaseType == DatabaseType.Logs)
            {

            }
            //Logs Summary, 9 of 9
            if (reportsType == ReportsType.Statistics && databaseType == DatabaseType.Logs)
            {

            }

        }

        private void cboDatebaseType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboDatebaseType.SelectedIndex > 0)
            {
                if (cboDatebaseType.SelectedIndex == 1)
                {
                    databaseType = DatabaseType.Image;
                }
                if (cboDatebaseType.SelectedIndex == 2)
                {
                    databaseType = DatabaseType.Logs;
                }
                if (cboDatebaseType.SelectedIndex == 3)
                {
                    databaseType = DatabaseType.Users;
                }
            }
        }

        private void cboReport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboReport.SelectedIndex > 0)
            {
                if (cboReport.SelectedIndex == 1)
                {
                    reportsType = ReportsType.Summary;
                }
                if (cboReport.SelectedIndex == 2)
                {
                    reportsType = ReportsType.Count;
                }
                if (cboReport.SelectedIndex == 3)
                {
                    reportsType = ReportsType.Statistics;
                }
            }
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLists();
        }

        private void LoadLists()
        {
            foreach (var image in db.Images)
            {
                imagelist.Add(image);
            }
            foreach (var user in db.Users)
            {
                userlist.Add(user);
            }
            foreach (var log in db.Logs)
            {
                logs.Add(log);
            }
        }
    }
}
