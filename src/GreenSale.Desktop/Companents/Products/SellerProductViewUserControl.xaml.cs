using DocumentFormat.OpenXml.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using GreenSale.Desktop.Companents.Loader;
using GreenSale.Desktop.Windows;
using GreenSale.Desktop.Windows.Products;
using GreenSale.Integrated.API.Auth;
using GreenSale.Integrated.Services.Auth;
using GreenSale.ViewModels.Models.SellerPosts;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GreenSale.Desktop.Companents.Products
{
    /// <summary>
    /// Interaction logic for ProductViewUserControl.xaml
    /// </summary>
    public partial class SellerProductViewUserControl : UserControl
    {
        private AuthService _authservice;

        private long ID { get; set; }

        public Func<Task> Refresh { get; set; }

        public static long sellerId { get; set; }
        public string imageUrl { get; set; }

        public SellerProductViewUserControl()
        {
            this._authservice = new AuthService();
            InitializeComponent();
        }


        public void SetData(SellerPost post)
        {
            string image = $"{AuthAPI.BASE_URL_IMG}" + post.mainImage;
            imageUrl = image;
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
            starAvareg.Content = post.AverageStars;
            /*if(post.AverageStars.ToString().Length > 1)
            {
                var str = post.AverageStars.ToString("0.0");
                starAvareg.Content =str;
            }
            else
            {
                starAvareg.Content = post.AverageStars.ToString();
            }*/


            if (post.status == 0)
            {
                txtbStatus.Text = "Yangi";
                statusPost.Background = new System.Windows.Media. SolidColorBrush( Colors.Green);
                txtbStatus.Foreground = new SolidColorBrush(Colors.Green);
            }
            else if (post.status == 1)
            {
                txtbStatus.Text = "Kelishilgan";
                statusPost.Background = new SolidColorBrush(Colors.Yellow);
                txtbStatus.Foreground = new SolidColorBrush(Colors.Yellow);

            }
            else if (post.status == 2)
            {
                txtbStatus.Text = " Sotib olingan";
                statusPost.Background = new SolidColorBrush(Colors.Red);
                txtbStatus.Foreground = new SolidColorBrush(Colors.Red);

            }
            ID = post.Id;
        }


        public void SetData(SellerPostViewModelSearch post)
        {
            string image = $"{AuthAPI.BASE_URL_IMG}" + post.MainImage;
            Uri imageUri = new Uri(image, UriKind.Absolute);

            SellePostImage.ImageSource = new BitmapImage(imageUri);
            txtbRegion.Text = post.Region;
            txtbDescription.Text = post.Description;
            txtbPrice.Text = post.Price.ToString();
            txtbUpdate.Text = post.UpdatedAt.ToString();
            txtTitle.Text = post.Title;
            txtbCapacity.Text = post.Capacity.ToString();

            txtbCapacityMeasure.Text = post.CapacityMeasure.ToString();
            ID = post.Id;
        }

        private void btnReadmore_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PictureLoader loader = new PictureLoader();
        }

        private void B_MouseDown(object sender, MouseButtonEventArgs e)
        {
            sellerId = ID;
            SellerProductViewWindow seller = new SellerProductViewWindow();
            seller.ShowDialog();
            Refresh();
        }
    }
}
