using GreenSale.Desktop.Companents.Images;
using GreenSale.Desktop.Companents.Products;
using GreenSale.Dtos.Dtos.BuyerPost;
using GreenSale.Integrated.API.Auth;
using GreenSale.Integrated.Interfaces.BuyerPosts;
using GreenSale.Integrated.Services.BuyerPosts;
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

namespace GreenSale.Desktop.Windows.Products
{
    /// <summary>
    /// Interaction logic for BuyerProductFullViewWindow.xaml
    /// </summary>
    public partial class BuyerProductFullViewWindow : Window
    {
        public IBuyerPostService _service;
        public long MainImg_Id { get; set; }
        public int Star_Count { get; set; }
        public int Star_CountUP { get; set; }
        public long PosrtId { get; set; }
        public bool updated = false;
        public int status { get; set; }
        public long updated_Id { get; set; }
        public BuyerProductFullViewWindow()
        {
            InitializeComponent();
            this._service = new BuyerPostService();
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


        private async void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            var result = await _service.UpdateStatusAsync(PosrtId, status);
            var star_result = await _service.UpdateStartAsync(PosrtId, Star_CountUP);
            this.Close();
        }

       /* private void btnPicture_MouseDown(object sender, MouseButtonEventArgs e)
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
            ImgMain.ImageSource = Img4.ImageSource;

        }*/

        private async void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var result = await _service.UpdateStatusAsync(PosrtId, status);
            var star_result = await _service.UpdateStartAsync(PosrtId, Star_CountUP);
            this.Close();
        }
        //  public static Dictionary<long, string> data = new Dictionary<long, string>();
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
            long id = BuyerProductPersonalViewUserControl.buyerId;
            var buyerPost = await _service.GetByIdAsync(id);
            var buyerPostImgSDFv = buyerPost.BuyerPostsImages.OrderBy(item => item.Id).ToList();
             
            txtCapacity.Text = buyerPost.Capacity.ToString();
            txtCapacityMeasure.Text = buyerPost.CapacityMeasure;
            txtDescription.Text = buyerPost.Description;
            txtDistrict.Text = buyerPost.District;
            txtPhoneNumber.Text = buyerPost.PostPhoneNumber;
            txtPrice.Text = buyerPost.Price.ToString();
            txtTitle.Text = buyerPost.Title;
            txtType.Text = buyerPost.Type;
            txtAddress.Text = buyerPost.Address;
            PosrtId = buyerPost.Id;
            Star_Count = Convert.ToInt32(buyerPost.UserStars);

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

            //--- sattus
            if (buyerPost.Status == ViewModels.Enums.BuyerPost.BuyerPostEnums.New)
            {
                new_border.Background = new SolidColorBrush(Colors.Green);
                lb_new.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (buyerPost.Status == ViewModels.Enums.BuyerPost.BuyerPostEnums.Agreed)
            {
                agree_border.Background = new SolidColorBrush(Colors.Green);
                lb_kelishilgan.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (buyerPost.Status == ViewModels.Enums.BuyerPost.BuyerPostEnums.Buyed)
            {
                byed_border.Background = new SolidColorBrush(Colors.Green);
                lb_sotilgan.Foreground = new SolidColorBrush(Colors.White);
            }
            

            int i = 0;
            foreach (var item in buyerPostImgSDFv)
            {
                BuyerUpdateImageComponent buyerUpdateImageComponent = new BuyerUpdateImageComponent();
                buyerUpdateImageComponent.Refresh = RefreshAsync;
                buyerUpdateImageComponent.SetData(item);
                wrpImg.Children.Add(buyerUpdateImageComponent);


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

        private async void ImgUpdateMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            /// string path = Path.GetFileName(ImgMain.ImageSource.ToString());

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {

                string imgPath = openFileDialog.FileName;
                //Img.ImageSource = new BitmapImage(new Uri(imgPath, UriKind.Relative));
                ImgIcon.Visibility = Visibility.Hidden;
                ImgUpdateMain.BorderThickness = new Thickness(0);

                long id = MainImg_Id;
                var result = await _service.UpdateImageAsync(id, imgPath);

                if (result)
                {
                    wrpImg.Children.Clear();
                    await RefreshWindow();
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



        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            BuyerPostUpdateDto dto = new BuyerPostUpdateDto();
            dto.Address = txtAddress.Text;
            dto.Description = txtDescription.Text;
            dto.District = txtDistrict.Text;
            dto.Capacity = int.Parse(txtCapacity.Text);
            dto.CapacityMeasure = txtCapacityMeasure.Text;
            dto.Title = txtTitle.Text;
            dto.Region = txtRegion.Text;
            dto.PhoneNumber = txtPhoneNumber.Text;
            dto.Price = double.Parse(txtPrice.Text);
            dto.Type = txtType.Text;

            long id = BuyerProductPersonalViewUserControl.buyerId;
            var res = await _service.UpdateAsync(id, dto);

            var result = await _service.UpdateStatusAsync(PosrtId, status);
            var star_result = await _service.UpdateStartAsync(PosrtId, Star_CountUP);


            if (res)
            {
                MessageBox.Show("Malumotlar muvafaqqiyatli o'zgartirildi");
            }
            else MessageBox.Show("Qayerdadur xatolik yuz berdi, qayta urunib koring");
        }


        private async void btnImageUpdate_Click(object sender, RoutedEventArgs e)
        {
            //string path = ImgMain.ImageSource.ToString();

            //foreach (var item in data)
            //{
            //    if (item.Value == path)
            //    {
            //        long id = item.Key;
            //        var result = await _service.UpdateImageAsync(id, path);
            //    }
            //}
        }

        private async void buyer_image_delete(object sender, RoutedEventArgs e)
        {
            var result = await _service.DeleteImageAsync(MainImg_Id);

            if (result)
            {
                wrpImg.Children.Clear();
                await RefreshWindow();
            }
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
            //var res = await _service.UpdateStatusAsync(PosrtId, 0);
        }

        private async void agree_status(object sender, MouseButtonEventArgs e)
        {
            agree_border.Background = new SolidColorBrush(Colors.Green);
            lb_kelishilgan.Foreground = new SolidColorBrush(Colors.White);
            //-----
            byed_border.Background = new SolidColorBrush(Colors.Transparent);
            lb_sotilgan.Foreground = new SolidColorBrush(Colors.Green);

            new_border.Background = new SolidColorBrush(Colors.Transparent);
            lb_new.Foreground = new SolidColorBrush(Colors.Green);
            status = 1;
            //var res = await _service.UpdateStatusAsync(PosrtId, 1);
        }

        private async void byed_status(object sender, MouseButtonEventArgs e)
        {
            agree_border.Background = new SolidColorBrush(Colors.Transparent);
            lb_kelishilgan.Foreground = new SolidColorBrush(Colors.Green);
            //-----
            byed_border.Background = new SolidColorBrush(Colors.Green);
            lb_sotilgan.Foreground = new SolidColorBrush(Colors.White);

            new_border.Background = new SolidColorBrush(Colors.Transparent);
            lb_new.Foreground = new SolidColorBrush(Colors.Green);
            status = 2;
            //var res = await _service.UpdateStatusAsync(PosrtId, 2);
        }

       
        int star_count = 0;
        private void click_star_1(object sender, MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Transparent);
            star_3.Fill = new SolidColorBrush(Colors.Transparent);
            star_4.Fill = new SolidColorBrush(Colors.Transparent);
            star_5.Fill = new SolidColorBrush(Colors.Transparent);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            Star_CountUP = 1;
        }

        private void click_star_2(object sender, MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Yellow);
            star_3.Fill = new SolidColorBrush(Colors.Transparent);
            star_4.Fill = new SolidColorBrush(Colors.Transparent);
            star_5.Fill = new SolidColorBrush(Colors.Transparent);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            Star_CountUP = 2;
        }

        private void click_star_3(object sender, MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Yellow);
            star_3.Fill = new SolidColorBrush(Colors.Yellow);
            star_4.Fill = new SolidColorBrush(Colors.Transparent);
            star_5.Fill = new SolidColorBrush(Colors.Transparent);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            Star_CountUP = 3;
        }

        private void click_star_4(object sender, MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Yellow);
            star_3.Fill = new SolidColorBrush(Colors.Yellow);
            star_4.Fill = new SolidColorBrush(Colors.Yellow);
            star_5.Fill = new SolidColorBrush(Colors.Transparent);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            Star_CountUP = 4;
        }

        private void click_star_5(object sender, MouseButtonEventArgs e)
        {
            star_2.Fill = new SolidColorBrush(Colors.Yellow);
            star_3.Fill = new SolidColorBrush(Colors.Yellow);
            star_4.Fill = new SolidColorBrush(Colors.Yellow);
            star_5.Fill = new SolidColorBrush(Colors.Yellow);
            //-------
            star_1.Fill = new SolidColorBrush(Colors.Yellow);
            Star_CountUP = 5;
        }
    }
}
