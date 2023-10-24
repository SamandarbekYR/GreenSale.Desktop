using GreenSale.Desktop.Companents.Products;
using GreenSale.Integrated.Interfaces.BuyerPosts;
using GreenSale.Integrated.Services.BuyerPosts;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GreenSale.Desktop.Pages.Buyers
{
    /// <summary>
    /// Interaction logic for BuyerPage.xaml
    /// </summary>
    public partial class BuyerPage : Page
    {
        private IBuyerPostService _service;

        public BuyerPage()
        {
            InitializeComponent();
            this._service = new BuyerPostService();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Refresh();
        }

        public async Task Refresh()
        {
            wrpCourses.Children.Clear();
            loader.Visibility = Visibility.Visible;
            var buyerpost = await _service.GetAllAsync();
            foreach (var post in buyerpost)
            {
                BuyerProductViewUserControl buyerProductViewUserControl = new BuyerProductViewUserControl();
                buyerProductViewUserControl.SetData(post);
                buyerProductViewUserControl.Refresh = Refresh;
                wrpCourses.Children.Add(buyerProductViewUserControl);
            }
            loader.Visibility = Visibility.Collapsed;
        }
  
        private async void By_Pst_TextBoxSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if( e.Key == Key.Enter && By_Pst_TextBoxSearch.Text.Length > 0)
            {
                wrpCourses.Children.Clear();
                loader.Visibility = Visibility.Visible;
                var buyerpost = await _service.SearchAsync(By_Pst_TextBoxSearch.Text.ToString());
                foreach (var post in buyerpost.item2)
                {
                    BuyerProductViewUserControl buyerProductViewUserControl = new BuyerProductViewUserControl();
                    buyerProductViewUserControl.SetData(post);
                    wrpCourses.Children.Add(buyerProductViewUserControl);
                }
                loader.Visibility = Visibility.Collapsed;
            }
        }

        private async void By_Pst_TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(By_Pst_TextBoxSearch.Text.Length == 0)
            {
                wrpCourses.Children.Clear();
                wrpCourses.Visibility = Visibility.Collapsed;
                loader.Visibility = Visibility.Visible;
                var buyerpost = await _service.GetAllAsync();
                foreach (var post in buyerpost)
                {
                    BuyerProductViewUserControl buyerProductViewUserControl = new BuyerProductViewUserControl();
                    buyerProductViewUserControl.SetData(post);
                    wrpCourses.Children.Add(buyerProductViewUserControl);

                }
                loader.Visibility = Visibility.Collapsed;
                wrpCourses.Visibility = Visibility.Visible;
            }
            
        }
    }
}
