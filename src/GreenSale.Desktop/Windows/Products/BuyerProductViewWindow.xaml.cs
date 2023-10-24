using GreenSale.Desktop.Companents.Images;
using GreenSale.Desktop.Companents.Products;
using GreenSale.Integrated.API.Auth;
using GreenSale.Integrated.Interfaces.BuyerPosts;
using GreenSale.Integrated.Services.BuyerPosts;
using GreenSale.Integrated.Services.SellerPosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static GreenSale.Desktop.Windows.Auth.BlurWindow.BlurEffect;

namespace GreenSale.Desktop.Windows.Products
{
    /// <summary>
    /// Interaction logic for BuyerProductViewWindow.xaml
    /// </summary>
    public partial class BuyerProductViewWindow : Window
    {
        public int Star_Count { get; set; }
        public long MainImg_Id { get; set; }
        public bool updated = false;
        public long PostId { get; set; }
        public long updated_Id { get; set; }
        public int Star_CountUP { get; set; }
        private MainWindow mainWindow;
        public Func<Task> Refresh { get; set; }

        private IBuyerPostService _service;

        public BuyerProductViewWindow()
        {
            InitializeComponent();
            this._service = new BuyerPostService();

        }

        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        public async Task RefreshWindow()
        {
            long id = BuyerProductViewUserControl.storageId;
            var buyerPost = await _service.GetByIdAsync(id);
            var buyerPostImage = buyerPost.BuyerPostsImages.OrderBy(item => item.Id).ToList();
            lbCapacity.Content = buyerPost.Capacity.ToString();
            lbCM.Content = buyerPost.CapacityMeasure;
            txbDescription.Text = buyerPost.Description;
            lbDistrict.Content = buyerPost.District;
            lbPhone.Content = buyerPost.PostPhoneNumber;
            lbPrice.Content = buyerPost.Price.ToString();
            lbType.Content = buyerPost.Type;
            PostId = buyerPost.Id;
            Star_Count = buyerPost.UserStars;

            if (Star_Count == 1)
            {
                star_2.Fill = new SolidColorBrush(Colors.Transparent);
                star_3.Fill = new SolidColorBrush(Colors.Transparent);
                star_4.Fill = new SolidColorBrush(Colors.Transparent);
                star_5.Fill = new SolidColorBrush(Colors.Transparent);
                //-------
                star_1.Fill = new SolidColorBrush(Colors.Yellow);
            }
            else if (Star_Count == 2)
            {
                star_2.Fill = new SolidColorBrush(Colors.Yellow);
                star_3.Fill = new SolidColorBrush(Colors.Transparent);
                star_4.Fill = new SolidColorBrush(Colors.Transparent);
                star_5.Fill = new SolidColorBrush(Colors.Transparent);
                //-------
                star_1.Fill = new SolidColorBrush(Colors.Yellow);
            }
            else if (Star_Count == 3)
            {
                star_2.Fill = new SolidColorBrush(Colors.Yellow);
                star_3.Fill = new SolidColorBrush(Colors.Yellow);
                star_4.Fill = new SolidColorBrush(Colors.Transparent);
                star_5.Fill = new SolidColorBrush(Colors.Transparent);
                //-------
                star_1.Fill = new SolidColorBrush(Colors.Yellow);
            }
            else if (Star_Count == 4)
            {
                star_2.Fill = new SolidColorBrush(Colors.Yellow);
                star_3.Fill = new SolidColorBrush(Colors.Yellow);
                star_4.Fill = new SolidColorBrush(Colors.Yellow);
                star_5.Fill = new SolidColorBrush(Colors.Transparent);
                //-------
                star_1.Fill = new SolidColorBrush(Colors.Yellow);
            }
            else if (Star_Count == 5)
            {
                star_2.Fill = new SolidColorBrush(Colors.Yellow);
                star_3.Fill = new SolidColorBrush(Colors.Yellow);
                star_4.Fill = new SolidColorBrush(Colors.Yellow);
                star_5.Fill = new SolidColorBrush(Colors.Yellow);
                //-------
                star_1.Fill = new SolidColorBrush(Colors.Yellow);
            }
            else if (Star_Count == 0)
            {
                star_2.Fill = new SolidColorBrush(Colors.Transparent);
                star_3.Fill = new SolidColorBrush(Colors.Transparent);
                star_4.Fill = new SolidColorBrush(Colors.Transparent);
                star_5.Fill = new SolidColorBrush(Colors.Transparent);
                //-------
                star_1.Fill = new SolidColorBrush(Colors.Transparent);
            }
            int i = 0;

            foreach (var item in buyerPostImage)
            {

                BuyerUpdateImageComponent buyerresfresh = new BuyerUpdateImageComponent();
                buyerresfresh.Refresh = RefreshAsync;
                buyerresfresh.SetData(item);
                wrpImg.Children.Add(buyerresfresh);

                //  data.Add(item.Id, item.ImagePath);
                if (i == 0)
                {
                    string image = $"{AuthAPI.BASE_URL_IMG}" + item.ImagePath;
                    MainImg_Id = item.Id;
                    Uri imageUri = new Uri(image, UriKind.Absolute);
                    ImgMain.ImageSource = new BitmapImage(imageUri);
                    i++;
                }
            }
        }
        public Task RefreshAsync(long id, string ImagePath)
        {
            string image = $"{AuthAPI.BASE_URL_IMG}" + ImagePath;
            if (updated == false)
            {
                MainImg_Id = id;
            }
            else if (updated == true)
            {
                MainImg_Id = updated_Id;
            }

            Uri imageUri = new Uri(image, UriKind.Absolute);
            ImgMain.ImageSource = new BitmapImage(imageUri);
            return Task.CompletedTask;
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
            await RefreshWindow();
        }

        private async void click_star_1(object sender, MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Transparent);
            star_3.Fill = new SolidColorBrush(Colors.Transparent);
            star_4.Fill = new SolidColorBrush(Colors.Transparent);
            star_5.Fill = new SolidColorBrush(Colors.Transparent);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            var res = await _service.UpdateStartAsync(PostId, 1);
        }

        private async void click_star_2(object sender, MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Yellow);
            star_3.Fill = new SolidColorBrush(Colors.Transparent);
            star_4.Fill = new SolidColorBrush(Colors.Transparent);
            star_5.Fill = new SolidColorBrush(Colors.Transparent);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            var res = await _service.UpdateStartAsync(PostId, 2);
        }

        private async void click_star_3(object sender, MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Yellow);
            star_3.Fill = new SolidColorBrush(Colors.Yellow);
            star_4.Fill = new SolidColorBrush(Colors.Transparent);
            star_5.Fill = new SolidColorBrush(Colors.Transparent);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            var res = await _service.UpdateStartAsync(PostId, 3);
        }

        private async void click_star_4(object sender, MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Yellow);
            star_3.Fill = new SolidColorBrush(Colors.Yellow);
            star_4.Fill = new SolidColorBrush(Colors.Yellow);
            star_5.Fill = new SolidColorBrush(Colors.Transparent);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            var res = await _service.UpdateStartAsync(PostId, 4);
        }

        private async void click_star_5(object sender, MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Yellow);
            star_3.Fill = new SolidColorBrush(Colors.Yellow);
            star_4.Fill = new SolidColorBrush(Colors.Yellow);
            star_5.Fill = new SolidColorBrush(Colors.Yellow);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            var res = await _service.UpdateStartAsync(PostId, 5);
        }
    }
}
