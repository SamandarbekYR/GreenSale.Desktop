using GreenSale.Desktop.Companents.Products;
using GreenSale.Desktop.Windows.Products;
using GreenSale.Integrated.Services.BuyerPosts;
using GreenSale.Integrated.Services.Storages;
using GreenSale.Integrated.Services.Users;
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

namespace GreenSale.Desktop.Pages.CreateAd
{
    /// <summary>
    /// Interaction logic for BuyerCreateAdd.xaml
    /// </summary>
    public partial class BuyerCreateAdd : Page
    {
        private BuyerPostService _service;
        private UserService _serviceUser;

        public BuyerCreateAdd()
        {
            InitializeComponent();
            this._service = new BuyerPostService();
            this._serviceUser = new UserService();

        }

        private async void btnBuyerCreate_Click(object sender, RoutedEventArgs e)
        {
            BuyerProductCreateWindow buyerProductCreateWindow = new BuyerProductCreateWindow();
            buyerProductCreateWindow.ShowDialog();
            await RefreshAsync();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }
        public async Task RefreshAsync()
        {
            wrpCourses.Children.Clear();
            var user = await _serviceUser.GetAsync();
            long id = user.Id;
            var buyerPost = await _service.GetAllUserId(id);
            loader.Visibility = Visibility.Collapsed;

            foreach (var post in buyerPost)
            {
                BuyerProductPersonalViewUserControl control = new BuyerProductPersonalViewUserControl();
                control.SetData(post);
                control.Refresh = RefreshAsync;
                wrpCourses.Children.Add(control);
            }
        }

        private void By_Pst_TextBoxSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void By_Pst_TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
