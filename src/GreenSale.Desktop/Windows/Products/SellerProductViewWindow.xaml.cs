using GreenSale.Desktop.Companents.Images;
using GreenSale.Desktop.Companents.Products;
using GreenSale.Desktop.Pages.Sellers;
using GreenSale.Integrated.API.Auth;
using GreenSale.Integrated.Interfaces.SellerPosts;
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
    /// Interaction logic for ProductViewWindow.xaml
    /// </summary>
    public partial class SellerProductViewWindow : Window
    {
        private ISellerPost _service;
        public int Star_Count { get; set; }
        public long MainImg_Id { get; set; }
        public bool updated = false;
        public long PostId { get; set; }
        public long updated_Id { get; set; }
        public int Star_CountUP { get; set; }
        private MainWindow mainWindow;
        public Func<Task> Refresh { get; set; }
        public SellerProductViewWindow()
        {
            InitializeComponent();
            this._service=new SellerPostService ();

            this.mainWindow = new MainWindow ();
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


        private async  void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
        public async Task RefreshWindow()
        {
            long id = SellerProductViewUserControl.sellerId;
            var sellerPost = await _service.GetByIdAsync(id);
            var buyerPostImgSDFv = sellerPost.PostImages.OrderBy(item => item.Id).ToList();
            txtCapacity.Text = sellerPost.Capacity.ToString();
            txtCM.Text = sellerPost.CapacityMeasure;
            txtDescription.Text = sellerPost.Description;
            txtDistrict.Text = sellerPost.District;
            txtPhone.Text = sellerPost.PostPhoneNumber;
            txtPrice.Text = sellerPost.Price.ToString();
            txtname.Text = sellerPost.Title;
            txtType.Text = sellerPost.Type;
            PostId = sellerPost.Id;
            txt_regin.Text = sellerPost.UserRegion.ToString();
            Star_Count = Convert.ToInt32(sellerPost.UserStars);

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
            foreach (var item in buyerPostImgSDFv)
            {
                SellerUpdateImageComponent sellerUpdateImageComponent = new SellerUpdateImageComponent();
                sellerUpdateImageComponent.Refresh = RefreshAsync;
                sellerUpdateImageComponent.SetData(item);
                wrpImg.Children.Add(sellerUpdateImageComponent);

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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshWindow();
            EnableBlur();
           
        }

        int star_count = 0;
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
