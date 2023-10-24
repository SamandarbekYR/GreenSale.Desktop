using GreenSale.Desktop.Companents.Products;
using GreenSale.Integrated.Interfaces.Storages;
using GreenSale.Integrated.Services.Storages;
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

namespace GreenSale.Desktop.Pages.Storages
{
    /// <summary>
    /// Interaction logic for StoragePage.xaml
    /// </summary>
    public partial class StoragePage : Page
    {
        private IStorageService _service;

        public StoragePage()
        {
            InitializeComponent();
            this._service = new StorageService();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }

        public async Task RefreshAsync()
        {
            wrpStorage.Children.Clear();
            loader.Visibility = Visibility.Visible;
            var storagepost = await _service.GetAllAsync();
            foreach (var post in storagepost)
            {
                StorageProductViewUserControl control = new StorageProductViewUserControl();
                control.Refresh = RefreshAsync;
                control.SetData(post);
                wrpStorage.Children.Add(control);
            }
            loader.Visibility = Visibility.Collapsed;
        }

        private async void By_Pst_TextBoxSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && TextBoxSearch.Text.Length > 0)
            {
                wrpStorage.Children.Clear();
                loader.Visibility = Visibility.Visible;
                var buyerpost = await _service.SearchAsync(TextBoxSearch.Text.ToString());
                
                foreach (var post in buyerpost.item2)
                {
                    StorageProductViewUserControl buyerProductViewUserControl = new StorageProductViewUserControl();
                    buyerProductViewUserControl.SetData(post);
                    wrpStorage.Children.Add(buyerProductViewUserControl);
                }
                loader.Visibility = Visibility.Collapsed;
            }
        }

        private  async void By_Pst_TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxSearch.Text.Length == 0)
            {
                await RefreshAsync();
            }
               
        }
    }
}
