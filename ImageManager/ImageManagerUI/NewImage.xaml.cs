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

        public User loggedInUser = new User();

        List<Vendor> vendorList = new List<Vendor>();
        List<UseCase> useCase = new List<UseCase>();

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
            bool inputscorrect = ImagesInputCheck();
            if (inputscorrect)
            {
                DBLibrary.Image savedimage = new DBLibrary.Image();
                savedimage.ImageName = txtImageName.Text.Trim();
                savedimage.Notes = txtImageNotes.Text.Trim();
                savedimage.VendorID = cbovendor.SelectedIndex + 1;
                savedimage.UseCaseID = cbousedIn.SelectedIndex + 1;
                savedimage.date = DateTime.Now;
                int saveSuccess = SaveImage(savedimage);
                if (saveSuccess == 1)
                {
                    MessageBox.Show("Image Details Saved", "Image save", MessageBoxButton.OK);
                    CreateLogEntry(loggedInUser.UserID, ("added image" + savedimage.ImageName), loggedInUser.Username);
                    ClearImagesInputs();
                    Dashboard dashboard = new Dashboard();
                    dashboard.ImageFrameChange();
                }
            }            
        }

        private int SaveImage(DBLibrary.Image image)
        {
            db.Entry(image).State = System.Data.Entity.EntityState.Added;
            int saveSuccess2 = db.SaveChanges();
            return saveSuccess2;
            //db.Entry(image).State = System.Data.Entity.EntityState.Added;
            //int saveSuccess = db.SaveChanges();
            //return saveSuccess;
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




        /// <summary>
        /// Populating the Vendors list and the cbo in the xaml
        /// </summary>
        private void getVendors()
        {
            try
            {
                vendorList.Clear();
                foreach (var vendor in db.Vendors)
                {
                    vendorList.Add(vendor);
                }
                cbovendor.ItemsSource = vendorList;
                cbovendor.Items.Refresh();
            }
            catch 
            {
                throw;
            }

        }
        /// <summary>
        /// poulating the use case list and the cbo in the xaml
        /// </summary>
        private void getUseCase()
        {
            try
            {
                useCase.Clear();
                foreach (var usecases in db.UseCases)
                {
                    useCase.Add(usecases);
                }
                cbousedIn.ItemsSource = useCase;
                cbousedIn.Items.Refresh();
            }
            catch
            {
                throw;
            }
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            getUseCase();
            getVendors();
        }

        private bool ImagesInputCheck()
        {
            bool validated = true;

            if (txtImageName.Text.Length == 0 | txtImageName.Text.Length > 120)
            {
                validated = false;
            }

            if (txtImageNotes.Text.Length == 0 | txtImageNotes.Text.Length > 120)
            {
                validated = false;
            }

            if (cbousedIn.SelectedIndex < 0 | cbousedIn.SelectedIndex > useCase.Count -1 )
            {
                validated = false;
            }

            if (cbovendor.SelectedIndex < 0 | cbovendor.SelectedIndex > useCase.Count - 1)
            {
                validated = false;
            }
            return validated;
        }


        private void ClearImagesInputs()
        {
            txtImageName.Clear();
            txtImageNotes.Clear();
            cbousedIn.SelectedIndex = -1;
            cbovendor.SelectedIndex = -1;
        }

    }

}

