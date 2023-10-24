using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using GreenSale.Desktop;
namespace GreenSale.Desktop.Pages.CreateAd
{
    /// <summary>
    /// Interaction logic for ChooseAd.xaml
    /// </summary>
    public partial class ChooseAd : Page
    {
        public ChooseAd()
        {
            InitializeComponent();
        }
        private void btnSellerAd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new CreateAd());
        }

        private void btnBuyerAd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new BuyerCreateAdd());

        }

        private void btnStorageAd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new StorageCreateAd());

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
