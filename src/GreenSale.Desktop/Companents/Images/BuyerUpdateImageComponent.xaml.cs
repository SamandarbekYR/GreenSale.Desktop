using GreenSale.Integrated.API.Auth;
using GreenSale.ViewModels.Models.BuyerPosts;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GreenSale.Desktop.Companents.Images;

/// <summary>
/// Interaction logic for BuyerUpdateImageComponent.xaml
/// </summary>
public partial class BuyerUpdateImageComponent : UserControl
{
    public Func<long, string, Task> Refresh { get; set; }
    private BuyerPostImage buyerPost = new BuyerPostImage();
    public long Id { get; private set; }
    public BuyerUpdateImageComponent()
    {
        InitializeComponent();
    }

    public void SetData(BuyerPostImage buyerPostImage)
    {
        this.buyerPost = buyerPostImage;
        string image = $"{AuthAPI.BASE_URL_IMG}" + buyerPostImage.ImagePath;

        Uri imageUri = new Uri(image, UriKind.Absolute);
        ImgBuyer.ImageSource = new BitmapImage(imageUri);
        Id = buyerPostImage.Id;
    }

    private async void btnPicture_MouseDown(object sender, MouseButtonEventArgs e)
    {
        await Refresh(buyerPost.Id, buyerPost.ImagePath);
    }
}
