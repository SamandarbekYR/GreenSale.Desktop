using GreenSale.Desktop.Companents.Products;
using GreenSale.Integrated.API.Auth;
using GreenSale.Integrated.Interfaces.Storages;
using GreenSale.Integrated.Services.Storages;
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
    /// Interaction logic for StorageProductViewWindow.xaml
    /// </summary>
    public partial class StorageProductViewWindow : Window
    {
        private IStorageService _service;
        public int Star_CountUP { get; set; }
        public int Star_Count { get; set; }
        public long PostId { get; set; }
        public StorageProductViewWindow()
        {
            InitializeComponent();
            this._service = new StorageService();
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
        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public async void ResfreshStorageWindow()
        {
            EnableBlur();
            long id = StorageProductViewUserControl.storageId;
            var storagePost = await _service.GetByIdAsync(id);

            txbStorageName.Text = storagePost.StorageName;
            txbInfo.Text = storagePost.Info;
            txbFullName.Text = storagePost.FullName.Split()[0];
            txbPhone.Text = storagePost.PhoneNumber;
            txbRegion.Text = storagePost.Region;
            txbDistrict.Text = storagePost.District;
            txbAddress.Text = storagePost.Address;
            txbDescription.Text = storagePost.Description;
            PostId = storagePost.Id;
            string image = $"{AuthAPI.BASE_URL_IMG}" + storagePost.ImagePath;
            Uri imageUri = new Uri(image, UriKind.Absolute);
            Img.ImageSource = new BitmapImage(imageUri);
            Star_Count = Convert.ToInt32(storagePost.UserStars);

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
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResfreshStorageWindow();
        }

        private async void click_star_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Transparent);
            star_3.Fill = new SolidColorBrush(Colors.Transparent);
            star_4.Fill = new SolidColorBrush(Colors.Transparent);
            star_5.Fill = new SolidColorBrush(Colors.Transparent);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            Star_CountUP = 1;
            await _service.UpdateStartAsync(PostId, Star_CountUP);
        }

        private async void click_star_2(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Yellow);
            star_3.Fill = new SolidColorBrush(Colors.Transparent);
            star_4.Fill = new SolidColorBrush(Colors.Transparent);
            star_5.Fill = new SolidColorBrush(Colors.Transparent);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            Star_CountUP = 2;
            await _service.UpdateStartAsync(PostId, Star_CountUP);
        }

        private async void click_star_3(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Yellow);
            star_3.Fill = new SolidColorBrush(Colors.Yellow);
            star_4.Fill = new SolidColorBrush(Colors.Transparent);
            star_5.Fill = new SolidColorBrush(Colors.Transparent);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            Star_CountUP = 3;
            await _service.UpdateStartAsync(PostId, Star_CountUP);
        }

        private async void click_star_4(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Yellow);
            star_3.Fill = new SolidColorBrush(Colors.Yellow);
            star_4.Fill = new SolidColorBrush(Colors.Yellow);
            star_5.Fill = new SolidColorBrush(Colors.Transparent);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            Star_CountUP = 4;
            await _service.UpdateStartAsync(PostId, Star_CountUP);
        }

        private async void click_star_5(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Yellow);
            star_3.Fill = new SolidColorBrush(Colors.Yellow);
            star_4.Fill = new SolidColorBrush(Colors.Yellow);
            star_5.Fill = new SolidColorBrush(Colors.Yellow);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            Star_CountUP = 5;
            await _service.UpdateStartAsync(PostId, Star_CountUP);
        }
    }
}
