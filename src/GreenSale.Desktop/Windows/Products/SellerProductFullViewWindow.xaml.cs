using GreenSale.Desktop.Companents.Images;
using GreenSale.Desktop.Companents.Products;
using GreenSale.Dtos.Dtos.SellerPost;
using GreenSale.Integrated.API.Auth;
using GreenSale.Integrated.Interfaces.SellerPosts;
using GreenSale.Integrated.Services.SellerPosts;
using GreenSale.ViewModels.Models.BuyerPosts;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static GreenSale.Desktop.Windows.Auth.BlurWindow.BlurEffect;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GreenSale.Desktop.Windows.Products
{
    /// <summary>
    /// Interaction logic for SellerProductFullViewWindow.xaml
    /// </summary>
    public partial class SellerProductFullViewWindow : System.Windows.Window
    {
        private ISellerPost _service;
        public long MainImg_Id { get; set; }
        public bool updated = false;
        public long updated_Id { get; set; }
        public long PostId { get; set; }
        public int Star_Count { get; set; }
        public int status { get; set; }
        public int Star_CountUP { get; set; }
        public SellerProductFullViewWindow()
        {
            InitializeComponent();
            this._service = new SellerPostService();
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
        /*        private void btnPicture_MouseDown(object sender, MouseButtonEventArgs e)
                {
                    ImgMain.ImageSource = Img.ImageSource;
                }

                private void btnPicture1_MouseDown(object sender, MouseButtonEventArgs e)
                {
                    ImgMain.ImageSource = Img1.ImageSource;

                }

                private void btnPicture2_MouseDown(object sender, MouseButtonEventArgs e)
                {
                    ImgMain.ImageSource = Img2.ImageSource;

                }

                private void btnPicture3_MouseDown(object sender, MouseButtonEventArgs e)
                {
                    ImgMain.ImageSource = Img3.ImageSource;

                }

                private void btnPicture4_MouseDown(object sender, MouseButtonEventArgs e)
                {
                    ImgMain.ImageSource = Img.ImageSource;

                }
        */
        private void btnBack_Click(object sender, RoutedEventArgs e)
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

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            SellerPostUpdateDto dto = new SellerPostUpdateDto();
            dto.Capacity = int.Parse(txtCapacity.Text);
            dto.CapacityMeasure = txtCapacityMeasure.Text;
            dto.Title = txtTitle.Text;
            dto.Type = txtType.Text;
            dto.Price = double.Parse(txtPrice.Text);
            dto.District = txtDistrict.Text;
            dto.Region = txtRegion.Text;
            dto.PhoneNumber = txtPhoneNumber.Text;
            dto.Description = txtDescription.Text;

            long id = SellerProductPersonalViewUserControl.sellerId;
            var result = await _service.UpdateAsync(id, dto);
            var res = await _service.UpdateStatusAsync(PostId, status);

            if (result)
            {
                MessageBox.Show("Malumotlar muvafaqqiyatli o'zgartirildi");
            }
            else MessageBox.Show("Qayerdadur xatolik yuz berdi, qayta urunib koring");

        }
        // private Dictionary<long, string> data = new Dictionary<long, string>();


        public async Task RefreshWindowSeller()
        {
            long id = SellerProductPersonalViewUserControl.sellerId;
            var sellerPost = await _service.GetByIdAsync(id);
            var sellerAllImg = sellerPost.PostImages.OrderBy(item => item.Id).ToList();

            txtCapacity.Text = sellerPost.Capacity.ToString();
            txtCapacityMeasure.Text = sellerPost.CapacityMeasure;
            txtDescription.Text = sellerPost.Description;
            txtDistrict.Text = sellerPost.District;
            txtPhoneNumber.Text = sellerPost.PostPhoneNumber;
            txtPrice.Text = sellerPost.Price.ToString();
            txtTitle.Text = sellerPost.Title;
            txtType.Text = sellerPost.Type;
            PostId = sellerPost.Id;
            Star_Count = Convert.ToInt32(sellerPost.UserStars);

            if (sellerPost.Status == ViewModels.Enums.SellerPost.SellerPostEnum.Nosold)
            {
                new_border.Background = new SolidColorBrush(Colors.Green);
                lb_new.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (sellerPost.Status == ViewModels.Enums.SellerPost.SellerPostEnum.AgreedUpon)
            {
                agree_border.Background = new SolidColorBrush(Colors.Green);
                lb_kelishilgan.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (sellerPost.Status == ViewModels.Enums.SellerPost.SellerPostEnum.Sold)
            {
                byed_border.Background = new SolidColorBrush(Colors.Green);
                lb_sotilgan.Foreground = new SolidColorBrush(Colors.White);
            }

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
            foreach (var item in sellerAllImg)
            {
                // data.Add(item.Id, item.ImagePath);

                SellerUpdateImageComponent sellerUpdate = new SellerUpdateImageComponent();
                sellerUpdate.Refresh = RefreshAsync;
                sellerUpdate.SetData(item);
                wrpImgSeller.Children.Add(sellerUpdate);

                if (i == 0)
                {
                    string image = $"{AuthAPI.BASE_URL_IMG}" + item.ImagePath;
                    MainImg_Id = item.Id;
                    Uri imageUri = new Uri(image, UriKind.Absolute);
                    ImgMain.ImageSource = new BitmapImage(imageUri);
                    i++;
                }
                /*else if (i == 1)
                {
                    string image = $"{AuthAPI.BASE_URL_IMG}" + item.ImagePath;

                    Uri imageUri = new Uri(image, UriKind.Absolute);
                    Img1.ImageSource = new BitmapImage(imageUri);
                }
                else if (i == 2)
                {
                    string image = $"{AuthAPI.BASE_URL_IMG}" + item.ImagePath;

                    Uri imageUri = new Uri(image, UriKind.Absolute);
                    Img2.ImageSource = new BitmapImage(imageUri);
                }
                else if (i == 3)
                {
                    string image = $"{AuthAPI.BASE_URL_IMG}" + item.ImagePath;

                    Uri imageUri = new Uri(image, UriKind.Absolute);
                    Img3.ImageSource = new BitmapImage(imageUri);
                }
                else if (i == 4)
                {
                    string image = $"{AuthAPI.BASE_URL_IMG}" + item.ImagePath;

                    Uri imageUri = new Uri(image, UriKind.Absolute);
                    Img4.ImageSource = new BitmapImage(imageUri);
                }*/
                
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
            await RefreshWindowSeller();
        }

        private async void ImgUpdateMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            wrpImgSeller.Children.Clear();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string imgPath = openFileDialog.FileName;
               // Img.ImageSource = new BitmapImage(new Uri(imgPath, UriKind.Relative));
                ImgIcon.Visibility = Visibility.Hidden;
                ImgUpdateMain.BorderThickness = new Thickness(0);

                long id = MainImg_Id;
                var result = await _service.ImageUpdateAsync(id, imgPath);

                if (result)
                {
                    await RefreshWindowSeller();

                    updated_Id = id;
                }
            }


        }

        private void ImgUpdateMain_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ImgUpdateMain.IsMouseOver == true)
            {
                ImgIcon.Visibility = Visibility.Visible;
                ImgUpdateMain.BorderThickness = new Thickness(2);

            }
            else
            {
                ImgIcon.Visibility = Visibility.Hidden;
                ImgUpdateMain.BorderThickness = new Thickness(0);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void click_star_1(object sender, MouseButtonEventArgs e)
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

        private async void click_star_2(object sender, MouseButtonEventArgs e)
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

        private async void click_star_3(object sender, MouseButtonEventArgs e)
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

        private async void click_star_4(object sender, MouseButtonEventArgs e)
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

        private async void click_star_5(object sender, MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Yellow);
            star_3.Fill = new SolidColorBrush(Colors.Yellow);
            star_4.Fill = new SolidColorBrush(Colors.Yellow);
            star_5.Fill = new SolidColorBrush(Colors.Yellow);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            Star_CountUP = 5;
            var result =  await _service.UpdateStartAsync(PostId, Star_CountUP);
        }

        private async void new_status(object sender, MouseButtonEventArgs e)
        {
            agree_border.Background = new SolidColorBrush(Colors.Transparent);
            lb_kelishilgan.Foreground = new SolidColorBrush(Colors.Green);
            //-----
            byed_border.Background = new SolidColorBrush(Colors.Transparent);
            lb_sotilgan.Foreground = new SolidColorBrush(Colors.Green);

            new_border.Background = new SolidColorBrush(Colors.Green);
            lb_new.Foreground = new SolidColorBrush(Colors.White);
            status = 0;
        }

        private void agree_status(object sender, MouseButtonEventArgs e)
        {
            agree_border.Background = new SolidColorBrush(Colors.Green);
            lb_kelishilgan.Foreground = new SolidColorBrush(Colors.White);
            //-----
            byed_border.Background = new SolidColorBrush(Colors.Transparent);
            lb_sotilgan.Foreground = new SolidColorBrush(Colors.Green);

            new_border.Background = new SolidColorBrush(Colors.Transparent);
            lb_new.Foreground = new SolidColorBrush(Colors.Green);
            status = 1;
        }

        private void byed_status(object sender, MouseButtonEventArgs e)
        {
            agree_border.Background = new SolidColorBrush(Colors.Transparent);
            lb_kelishilgan.Foreground = new SolidColorBrush(Colors.Green);
            //-----
            byed_border.Background = new SolidColorBrush(Colors.Green);
            lb_sotilgan.Foreground = new SolidColorBrush(Colors.White);

            new_border.Background = new SolidColorBrush(Colors.Transparent);
            lb_new.Foreground = new SolidColorBrush(Colors.Green);
            status = 2;
            //var res = await _service.UpdateStatusAsync(PosrtId, 1);
        }

        private async void buyer_image_delete(object sender, RoutedEventArgs e)
        {
            var result = await _service.DeleteImageAsync(MainImg_Id);

            if (result)
            {
                wrpImgSeller.Children.Clear();
                await RefreshWindowSeller();
            }
        }
    }
}
