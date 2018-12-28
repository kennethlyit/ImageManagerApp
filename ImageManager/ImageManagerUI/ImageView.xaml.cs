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
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView : Page
    {
        //public user from dashboard to check user access level
        public User loggedinUser = new User();

        //string is populated from dashboard
        public string ImageSearchString;

        //database string
        ImageManageEntities db = new ImageManageEntities("metadata=res://*/ImageManagerModel.csdl|res://*/ImageManagerModel.ssdl|res://*/ImageManagerModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.81.100;initial catalog=ImageManage;persist security info=True;user id=ImageManagement;password=Letmein101;MultipleActiveResultSets=True;App=EntityFramework'");

        //Lists for listview of images, pulled from database
        List<DBLibrary.Image> imageList = new List<DBLibrary.Image>();
        List<Vendor> vendorslist = new List<Vendor>();
        List<UseCase> useCaselist = new List<UseCase>();

        //Image for selection specific context menu
        DBLibrary.Image selectedImage = new DBLibrary.Image();


        /// <summary>
        /// This class creates the listview of images, popualting 3 lists, then do 2 inner joins to create a final list which is used for the source
        /// </summary>
        private void RefreshImages()
        {
            imageList.Clear();
            vendorslist.Clear();
            useCaselist.Clear();
            foreach (var image in db.Images)
            {
                imageList.Add(image);
            }
            foreach (var vendor in db.Vendors)
            {
                vendorslist.Add(vendor);
            }
            foreach (var usecase in db.UseCases)
            {
                useCaselist.Add(usecase);
            }
            var finalImageList = from p in imageList
                                  join e in vendorslist on p.VendorID equals e.VendorID
                                  join s in useCaselist on p.UseCaseID equals s.UseCaseID
                                  select new
                                  {
                                      p.ImageName,
                                      p.Notes,
                                      p.date,
                                      s.UseCaseDescription,
                                      e.VendorName
                                  };
            lstimages.ItemsSource = finalImageList;
            lstimages.Items.Refresh();
        }

        /// <summary>
        /// This function returns the same as the Refresh image function, but checks it against a search string and populates only the results
        /// </summary>
        private void SearchCheck()
        {
            imageList.Clear();
            vendorslist.Clear();
            useCaselist.Clear();
            foreach (var image in db.Images)
            {
                if (image.ImageName.Contains(ImageSearchString) || image.Notes.Contains(ImageSearchString))
                {
                    imageList.Add(image);
                }
            }
            foreach (var vendor in db.Vendors)
            {
                vendorslist.Add(vendor);
            }
            foreach (var usecase in db.UseCases)
            {
                useCaselist.Add(usecase);
            }
            var finalImageList = from p in imageList
                                 join e in vendorslist on p.VendorID equals e.VendorID
                                 join s in useCaselist on p.UseCaseID equals s.UseCaseID
                                 select new
                                 {
                                     p.ImageName,
                                     p.Notes,
                                     p.date,
                                     s.UseCaseDescription,
                                     e.VendorName
                                 };
            lstimages.ItemsSource = finalImageList;
            lstimages.Items.Refresh();
        }

        public ImageView()
        {
            InitializeComponent();
        }


        //Functions on page load
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Gives Graphic desingers ability to delete
            CheckUserAccess(loggedinUser);
            //checks if the search string was populated and sent to this page
            bool searchimagecheck = string.IsNullOrEmpty(ImageSearchString);
            //if it did, put the normal list there, if not then the list with the search entries
            if (searchimagecheck)
            {
                RefreshImages();
            }
            else
            {
                SearchCheck();
                ImageSearchString = null;
            }
        }

        //allows me to delete images via menu, creates a log entry when the image is deleted
        private void mnudeleteImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.Images.RemoveRange(db.Images.Where(t => t.ImageID == selectedImage.ImageID));
                int saveSucess = db.SaveChanges();
                if (saveSucess == 1)
                {
                    CreateLogEntry(loggedinUser.UserID, (selectedImage.ImageName + "deleted by"), loggedinUser.Username);
                    RefreshImages();
                    MessageBox.Show("Image Deleted", "Save to Database");
                }
                else
                {
                    MessageBox.Show("Delete Failed", "Save to Database");
                }
            }
            catch 
            {
                MessageBox.Show("Unable to Delete Image", "Save to Database");
            }

        }


        /// <summary>
        /// Checsk the local user access level
        /// </summary>
        /// <param name="user"></param>
        private void CheckUserAccess(User user)
        {
            if (user.LevelID == 1) //graphic designer
            {
                mnudeleteImage.IsEnabled = true;
            }
        }

        private void lstimages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstimages.SelectedIndex > 0)
            {
                selectedImage = imageList.ElementAt(lstimages.SelectedIndex);
            }
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


        //private void cbousedIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        //private void cbovendor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void btnaddImage_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void btncancelImage_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
