using GreenSale.Desktop.Pages.Storages;
using GreenSale.Desktop.Windows.Products;
using GreenSale.Integrated.API.Auth;
using GreenSale.Integrated.Interfaces.Storages;
using GreenSale.Integrated.Services.Storages;
using GreenSale.ViewModels.Models.BuyerPosts;
using GreenSale.ViewModels.Models.SellerPosts;
using GreenSale.ViewModels.Models.Storages;
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

namespace GreenSale.Desktop.Companents.Products
{
    /// <summary>
    /// Interaction logic for StorageProductViewUserControl.xaml
    /// </summary>
    public partial class StorageProductViewUserControl : UserControl
    {
        private IStorageService _service;

        private long ID { get; set; }
        public static long storageId { get; set; }
        public Func<Task> Refresh { get; set; }

        public StorageProductViewUserControl()
        {
            InitializeComponent();
            this._service = new StorageService();
        }
        public void SetData(Storage post)
        {
            string image = $"{AuthAPI.BASE_URL_IMG}" + post.ImagePath;
            Uri imageUri = new Uri(image, UriKind.Absolute);

            StorageImage.ImageSource = new BitmapImage(imageUri);
            loader.Visibility = Visibility.Collapsed;

            txtbRegion.Text = post.Region;
            txtbDescription.Text = post.Description;
            txtbUpdate.Text = post.UpdatedAt.ToString("hh:mm") + " " + post.UpdatedAt.ToString("dd-MM-yy");
            txtInfo.Text = post.Info;
            txtbUser.Text = post.FullName.Split()[0];
            txtbPhoneNumber.Text = post.PhoneNumber;
            ID = post.Id;
            starAvareg.Content = post.AverageStars;
        }

        public void SetData(StorageViewModel post)
        {
            string image = $"{AuthAPI.BASE_URL_IMG}" + post.ImagePath;
            Uri imageUri = new Uri(image, UriKind.Absolute);

            StorageImage.ImageSource = new BitmapImage(imageUri);
            txtbRegion.Text = post.Region;
            txtbDescription.Text = post.Description;
            txtbUpdate.Text = post.UpdatedAt.ToString();
            txtInfo.Text = post.Info;
            txtbUser.Text = post.FullName.Split()[0];
            txtbPhoneNumber.Text = post.PhoneNumber;
            ID = post.Id;
        }

        private async void btnReadmore_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private async void B_MouseDown(object sender, MouseButtonEventArgs e)
        {
            storageId = ID;
            StorageProductViewWindow window = new StorageProductViewWindow();
            window.ShowDialog();
            await Refresh();
        }
    }
}
