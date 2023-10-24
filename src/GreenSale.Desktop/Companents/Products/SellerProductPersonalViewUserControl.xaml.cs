using GreenSale.Desktop.Windows.Products;
using GreenSale.Integrated.API.Auth;
using GreenSale.Integrated.Interfaces.SellerPosts;
using GreenSale.Integrated.Interfaces.Storages;
using GreenSale.Integrated.Services.SellerPosts;
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
    /// Interaction logic for SellerProductPersonalViewUserControl.xaml
    /// </summary>
    public partial class SellerProductPersonalViewUserControl : UserControl
    {
        private ISellerPost _service;
        private long ID { get; set; }
        public static long sellerId { get; set; }
        public Func<Task> Refresh { get; set; }



        public SellerProductPersonalViewUserControl()
        {
            InitializeComponent();
            this._service = new SellerPostService();

        }
        public void SetData(SellerPost post)
        {
            string image = $"{AuthAPI.BASE_URL_IMG}" + post.mainImage;
            Uri imageUri = new Uri(image, UriKind.Absolute);

            SellePostImage.ImageSource = new BitmapImage(imageUri);
            loader.Visibility = Visibility.Collapsed;

            txtbRegion.Text = post.region;
            txtbDescription.Text = post.description;
            txtbPrice.Text = post.price.ToString();
            txtbUpdate.Text = post.updatedAt.ToString("hh:mm") + " " + post.updatedAt.ToString("dd-MM-yy");
            txtTitle.Text = post.title;
            txtbCapacity.Text = post.capacity.ToString();
            txtbCapacityMeasure.Text = post.capacityMeasure.ToString();
            ID = post.Id;
        }


        private async void btnSellerDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = await _service.DeleteAsync(ID);
            Sellercom.Visibility = Visibility.Collapsed;
        }

        private async void btnReadMore_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private async void B_MouseDown(object sender, MouseButtonEventArgs e)
        {
            sellerId = ID;
            SellerProductFullViewWindow seller = new SellerProductFullViewWindow();
            await Refresh();
            seller.ShowDialog();
        }
    }
}
