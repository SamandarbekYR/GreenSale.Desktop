using GreenSale.Desktop.Windows.Products;
using GreenSale.Integrated.API.Auth;
using GreenSale.Integrated.Interfaces.BuyerPosts;
using GreenSale.Integrated.Services.BuyerPosts;
using GreenSale.Integrated.Services.Storages;
using GreenSale.ViewModels.Models.BuyerPosts;
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
    /// Interaction logic for BuyerProductPersonalViewUserControl.xaml
    /// </summary>
    public partial class BuyerProductPersonalViewUserControl : UserControl
    {
        private IBuyerPostService _service;

        private long ID { get; set; }
        public static long buyerId { get; set; }

        public Func<Task> Refresh { get; set; }


        public BuyerProductPersonalViewUserControl()
        {
            InitializeComponent();
            this._service = new BuyerPostService();

        }
        public void SetData(BuyerPost post)
        {
            string image = $"{AuthAPI.BASE_URL_IMG}" + post.mainImage;
            Uri imageUri = new Uri(image, UriKind.Absolute);

            BuyerPostImage.ImageSource = new BitmapImage(imageUri);

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

        private async void btnBuyerDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = await _service.DeleteAsync(ID);

            Buyercom.Visibility = Visibility.Collapsed;
            
        }

        private async void btnReadmore_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private async void B_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buyerId = ID;
            BuyerProductFullViewWindow buyer = new BuyerProductFullViewWindow();
            await Refresh();
            buyer.ShowDialog();
        }
    }
}
