using GreenSale.Dtos.Dtos.BuyerPost;
using GreenSale.Integrated.Interfaces.BuyerPosts;
using GreenSale.Integrated.Interfaces.Categories;
using GreenSale.Integrated.Services.BuyerPosts;
using GreenSale.Integrated.Services.Categories;
using GreenSale.ViewModels.Models.Categories;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GreenSale.Desktop.Windows.Products
{
    /// <summary>
    /// Interaction logic for BuyerProductCreateWindow.xaml
    /// </summary>
    public partial class BuyerProductCreateWindow : Window
    {
        public IBuyerPostService _service;
        public ICategoryGetAll _servicecategory;
        List<CategoryViewModel> list = new List<CategoryViewModel>();
        public BuyerProductCreateWindow()
        {
            InitializeComponent();
            this._service = new BuyerPostService();
            this._servicecategory = new Category();
        }

        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            BuyerProductCreateWindow buyerProductCreateWindow = new BuyerProductCreateWindow();
            this.Close();
        }

        private void btnPicture_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Img1.ImageSource == null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
                if (openFileDialog.ShowDialog() == true)
                {
                    string imgPath = openFileDialog.FileName;
                    Img1.ImageSource = new BitmapImage(new Uri(imgPath, UriKind.Relative));
                    ImgIcon.Visibility = Visibility.Hidden;
                    btnPicture2.Visibility = Visibility.Visible;
                }
            }

        }

        private void btnPicture2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Img2.ImageSource == null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
                if (openFileDialog.ShowDialog() == true)
                {
                    string imgPath = openFileDialog.FileName;
                    Img2.ImageSource = new BitmapImage(new Uri(imgPath, UriKind.Relative));
                    ImgIcon2.Visibility = Visibility.Hidden;
                    btnPicture3.Visibility = Visibility.Visible;
                }
            }
        }

        private void btnPicture3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Img3.ImageSource == null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
                if (openFileDialog.ShowDialog() == true)
                {
                    string imgPath = openFileDialog.FileName;
                    Img3.ImageSource = new BitmapImage(new Uri(imgPath, UriKind.Relative));
                    ImgIcon3.Visibility = Visibility.Hidden;
                    btnPicture4.Visibility = Visibility.Visible;
                }
            }
        }

        private void btnPicture4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Img4.ImageSource == null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
                if (openFileDialog.ShowDialog() == true)
                {
                    string imgPath = openFileDialog.FileName;
                    Img4.ImageSource = new BitmapImage(new Uri(imgPath, UriKind.Relative));
                    ImgIcon4.Visibility = Visibility.Hidden;
                    btnPicture5.Visibility = Visibility.Visible;
                }
            }
        }

        private void btnPicture5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Img5.ImageSource == null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
                if (openFileDialog.ShowDialog() == true)
                {
                    string imgPath = openFileDialog.FileName;
                    Img5.ImageSource = new BitmapImage(new Uri(imgPath, UriKind.Relative));
                    ImgIcon5.Visibility = Visibility.Hidden;
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string text = textBox.Text;
            string filteredText = Regex.Replace(text, "[^0-9]+", "");

            if (text != filteredText)
            {
                int caretIndex = textBox.CaretIndex;
                textBox.Text = filteredText;
                textBox.CaretIndex = caretIndex > 0 ? caretIndex - 1 : 0;
            }
        }

        private void txtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string text = textBox.Text;
            string filteredText = Regex.Replace(text, "[^0-9]+", "");

            if (text != filteredText)
            {
                int caretIndex = textBox.CaretIndex;
                textBox.Text = filteredText;
                textBox.CaretIndex = caretIndex > 0 ? caretIndex - 1 : 0;
            }
        }

        private void cmbRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> lstToshkent = new List<string>() { "Bektemir", "Chilonzor", "Mirobod", "Mirzo Ulug'bek", "Olmazor", "Sergili", "Shayhontohur", "Uchtepa", "Yunusobod", "Yakkasaroy", "Yashnabod" };
            List<string> lstAndijon = new List<string>() { "Andijon shahri", "Asaka", "Baliqchi", "Bo'z", "Bulung'u", "Izboskan", "Jalaquduq", "Marhamat", "Oltinko'l", "Paxtaobod", "Qo'rg'onchi", "Shahrixon", "Ulug'nor", "Xonobod" };
            List<string> lstNamangan = new List<string>() { "Namangan shahri", "Chortoq", "Pop", "Norin", "Uychi", "Uchqo'rg'on", "Chust", "Kosonsoy", "Mingbuloq", "To'raqo'rg'on", "Yangiqo'rg'on", "Ulug'nor" };
            List<string> lstFargona = new List<string> { "Farg'ona shahri", "Beshariq tumani", "Buvayda tumani", "Dang'ara tumani", "Farg'ona tumani", "Furqat tumani", "Quva tumani", "Rishton tumani", "So'x tumani", "Toshloq tumani", "Uchko'prik tumani", "Yozyovon tumani", "Oltiariq tumani", "Bag'dod tumani", "O'tkirik tumani", "Qo'shtepa tumani", "Marg'ilon shahri" };
            List<string> lstSirdaryo = new List<string>() { "Boyovut", "Guliston", "Oqoltin", "Sardoba", "Sayxunobod", "Shirin", "Sirdayro", "Xovos", "Yangiyer" };
            List<string> lstXorazm = new List<string>() { "Urganch shahri", "Bog'ot", "Gurlan", "Qo'shko'pik", "Shovot", "Xazprasp", "Xiva", "Xonqa", "Yangiariq", "Yangibozor" };
            List<string> lstBuxoro = new List<string>() { "Buxoro shahri", "G'ijduvon", "Jondor", "Kogon", "Olot", "Peshku", "Qorako'l", "Qoraovulbozor", "Romitan", "Shofirkon", "Vobkent" };
            List<string> lstSamarqand = new List<string>() { "Samarqand shahri", "Bulung'ur", "Ishtixon", "Jomboy", "Katta Qo'rg'on", "Narpay", "Nurobod", "Oqdaryo", "Past darg'om", "Paxtachi", "Poyariq", "Urgut", "To'yloq", "Qo'shrabot" };
            List<string> lstSurxondaryo = new List<string>() { "Angor", "Bandixon", "Boysun", "Denov", "Jarqo'rg'on", "Muzrobot", "Oltinsoy", "Qiziriq", "Qumqo'rg'on", "Sariosiyo", "Sherobod", "Sho'chi", "Termiz", "Uzun" };
            List<string> lstQashqadaryo = new List<string>() { "Qarshi", "Chiroqchi", "Dehqonobod", "G'usor", "Kasbi", "Kitob", "Koson", "Mirishkor", "Muborak", "Nushon", "Qamashi", "Shahrisabz", "Yakkabog'" };
            List<string> lstNavoiy = new List<string>() { "Navoiy shahri", "Karmara", "Kanimex", "Navbohor", "Nurota", "Qiziltepa", "Tomdi", "Uchquduq", "Xatirchi", "Zarafshon" };
            List<string> lstJizzax = new List<string>() { "Jizzax shahri", "Arnasoy", "Do'stlik", "Forish", "G'allaorol", "Mirzacho'l", "Paxtakor", "Yangiobod", "Zafarobod", "Zarband", "Zomin" };


            if (cmbRegion.SelectedIndex == 0)
            {
                cmbDistrict.ItemsSource = lstToshkent;
            }
            else if (cmbRegion.SelectedIndex == 1)
            {
                cmbDistrict.ItemsSource = lstAndijon;
            }
            else if (cmbRegion.SelectedIndex == 2)
            {
                cmbDistrict.ItemsSource = lstNamangan;
            }
            else if (cmbRegion.SelectedIndex == 3)
            {
                cmbDistrict.ItemsSource = lstFargona;
            }
            else if (cmbRegion.SelectedIndex == 4)
            {
                cmbDistrict.ItemsSource = lstSirdaryo;
            }
            else if (cmbRegion.SelectedIndex == 5)
            {
                cmbDistrict.ItemsSource = lstXorazm;
            }
            else if (cmbRegion.SelectedIndex == 6)
            {
                cmbDistrict.ItemsSource = lstBuxoro;
            }
            else if (cmbRegion.SelectedIndex == 7)
            {
                cmbDistrict.ItemsSource = lstSamarqand;
            }
            else if (cmbRegion.SelectedIndex == 8)
            {
                cmbDistrict.ItemsSource = lstSurxondaryo;
            }
            else if (cmbRegion.SelectedIndex == 9)
            {
                cmbDistrict.ItemsSource = lstQashqadaryo;
            }
            else if (cmbRegion.SelectedIndex == 10)
            {
                cmbDistrict.ItemsSource = lstNavoiy;
            }
            else if (cmbRegion.SelectedIndex == 11)
            {
                cmbDistrict.ItemsSource = lstJizzax;
            }
        }


        public bool IsValid()
        {
            bool isValid = false;
            if (txtTitle.Text.Length == 0)
            {
                lb_nomi.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (txtTitle.Text.Length < 3)
            {
                lb_nomi.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lb_nomi.Visibility = Visibility.Collapsed;
                isValid = true;
            }

            if (txtType.Text.Length == 0)
            {
                lb_turi.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (txtType.Text.Length < 3)
            {
                lb_turi.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lb_turi.Visibility = Visibility.Collapsed;
                isValid = true;
            }

            if (txtPhoneNumber.Text.Length < 9 || txtPhoneNumber.Text.Length == 0)
            {
                lb_phone.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lb_phone.Visibility = Visibility.Collapsed;
                isValid = true;
            }

            if (txtPrice.Text.Length == 0)
            {
                lb_narxi.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (txtPrice.Text.Length < 3)
            {
                lb_narxi.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lb_narxi.Visibility = Visibility.Collapsed;
                isValid = true;
            }


            //----------------------------------------

            if (txtCapacity.Text.Length == 0)
            {
                lb_miqdori.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (txtCapacity.Text.Length < 3)
            {
                lb_miqdori.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lb_miqdori.Visibility = Visibility.Collapsed;
                isValid = true;
            }


            //----------------------------------------

            if (cmbRegion.Text.Length == 0)
            {
                lb_regn.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lb_regn.Visibility = Visibility.Hidden;
                isValid = true;
            }

            if (cmbDistrict.Text.Length == 0)
            {
                lb_disk.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lb_disk.Visibility = Visibility.Hidden;
                isValid = true;
            }

            if (txtAddress.Text.Length == 0)
            {
                lb_manzil.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (txtAddress.Text.Length < 3)
            {
                lb_manzil.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lb_manzil.Visibility = Visibility.Hidden;
                isValid = true;
            }


            if (txtDescription.Text.Length == 0)
            {
                lb_taasnif.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (txtDescription.Text.Length < 3)
            {
                lb_taasnif.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lb_taasnif.Visibility = Visibility.Hidden;
                isValid = true;
            }

            return isValid;
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if(IsValid() != false)
            {
                BuyerPostCreateDto dto = new BuyerPostCreateDto();
                dto.Capacity = int.Parse(txtCapacity.Text);
                dto.CapacityMeasure = cmbCapacityMeasure.Text;
                dto.Address = txtAddress.Text;
                dto.District = cmbDistrict.Text;
                dto.Region = cmbRegion.Text;
                dto.Price = double.Parse(txtPrice.Text);
                dto.Title = txtTitle.Text;
                dto.Description = txtDescription.Text;
                dto.Type = txtType.Text;
                //dto.CategoryID = cmbCategory.SelectedIndex;
                dto.PhoneNumber = "+998" + txtPhoneNumber.Text;

                dto.ImagePath = new List<string>();

                if (Img1.ImageSource is not null)
                    dto.ImagePath.Add(Img1.ImageSource.ToString());

                if (Img2.ImageSource is not null)
                    dto.ImagePath.Add(Img2.ImageSource.ToString());

                if (Img3.ImageSource is not null)
                    dto.ImagePath.Add(Img3.ImageSource.ToString());

                if (Img4.ImageSource is not null)
                    dto.ImagePath.Add(Img4.ImageSource.ToString());

                if (Img5.ImageSource is not null)
                    dto.ImagePath.Add(Img5.ImageSource.ToString());

                foreach (var item in list)
                {
                    if (item.Name == cmbCategory.SelectedValue.ToString())
                    {
                        dto.CategoryID = item.Id;
                        break;
                    }
                }

                var res = await _service.CreateAsync(dto);
                this.Close();
            }
            else
            {
                MessageBox.Show("zdsgsdfb");
            }
            
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            var result = await _servicecategory.GetAllAsync();

            foreach (var item in result)
            {
                list.Add(item);
                cmbCategory.Items.Add(item.Name);
            }
        }
    }
}
