using GreenSale.Integrated.API.Auth;
using GreenSale.ViewModels.Models.SellerPosts;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GreenSale.Desktop.Companents.Images
{
    /// <summary>
    /// Interaction logic for SellerUpdateImageComponent.xaml
    /// </summary>
    public partial class SellerUpdateImageComponent : UserControl
    {
        public Func<long, string, Task> Refresh { get; set; }
        private SellerPostImage sellerPostImage = new SellerPostImage();
        public long Id { get; private set; }
        public SellerUpdateImageComponent()
        {
            InitializeComponent();
        }

        public void SetData(SellerPostImage sellerPostImage)
        {
            this.sellerPostImage = sellerPostImage;
            string image = $"{AuthAPI.BASE_URL_IMG}" + sellerPostImage.ImagePath;

            Uri imageUri = new Uri(image, UriKind.Absolute);
            ImgBuyer.ImageSource = new BitmapImage(imageUri);
            Id = sellerPostImage.Id;
        }

        private async void btnPicture_MouseDown(object sender, MouseButtonEventArgs e)
        {
            await Refresh(sellerPostImage.Id, sellerPostImage.ImagePath);
        }
    }
}
