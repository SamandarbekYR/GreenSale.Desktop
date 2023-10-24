using GreenSale.Desktop.Helper;
using GreenSale.Dtos.Dtos.Storages;
using GreenSale.Integrated.Interfaces.Storages;
using GreenSale.Integrated.Services.Storages;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Runtime.Serialization;
using System.Text;
using System.Net.Http;
using GreenSale.Desktop.Companents.Products;
using System.Threading.Tasks;
using GreenSale.Desktop.Pages.CreateAd;
using System.Windows.Navigation;

namespace GreenSale.Desktop.Windows.Products
{
    /// <summary>
    /// Interaction logic for StorageProductCreateWindow.xaml
    /// </summary>
    public partial class StorageProductCreateWindow : Window
    {
        private IStorageService _service;

        public StorageProductCreateWindow()
        {
            InitializeComponent();

            this._service = new StorageService();
        }

        public string ToBin (string imagePaht)
        {
            StringBuilder @string = new StringBuilder();
            foreach (char item in imagePaht.ToCharArray())
            {
                @string.Append(Convert.ToString(item, 2).PadLeft(8, '0'));
            }
            return @string.ToString();
        }


        public bool IsValid()
        {
            bool isValid = false;
            if (txtbName.Text.Length == 0)
            {
                lb_nomi.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (txtbName.Text.Length < 3)
            {
                lb_nomi.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lb_nomi.Visibility = Visibility.Collapsed;
                isValid = true;
            }

            if (txtbInfo.Text.Length == 0)
            {
                lb_malumoti.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (txtbInfo.Text.Length < 3)
            {
                lb_malumoti.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lb_malumoti.Visibility = Visibility.Collapsed;
                isValid = true;
            }

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

            if (txtbAddress.Text.Length == 0)
            {
                lb_manzil.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (txtbAddress.Text.Length < 3)
            {
                lb_manzil.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lb_manzil.Visibility = Visibility.Hidden;
                isValid = true;
            }


            if (txtbDescription.Text.Length == 0)
            {
                lb_taasnif.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (txtbDescription.Text.Length < 3)
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
                StorageCreateAd storageCreateAd = new StorageCreateAd();
                StorageDto storage = new StorageDto()
                {
                    Name = txtbName.Text.ToString(),
                    Info = txtbInfo.Text.ToString(),
                    Region = cmbRegion.Text.ToString(),
                    District = cmbDistrict.Text.ToString(),
                    Address = txtbAddress.Text.ToString(),
                    Description = txtbDescription.Text.ToString(),
                };

                string imagePath = ImgStorage.ImageSource.ToString();

                storage.ImagePath = imagePath;
                //storage.ImagePath = fileContent;
                var result = await _service.CreateAsync(storage);
                //await storageCreateAd.RefreshAsync();
                storageCreateAd.ShowsNavigationUI = true;
                if (result)
                {
                    MessageBox.Show("E'loningiz yaratildi");
                }
                else
                {
                    MessageBox.Show("Qayerdadur xatolik ketdi");
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("xaro");
            }
        }

        

        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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


        private void btnPicture_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
                if (openFileDialog.ShowDialog() == true)
                {
                    string imgPath = openFileDialog.FileName;
                    ImgStorage.ImageSource = new BitmapImage(new Uri(imgPath, UriKind.Relative));
                    ImgIcon.Visibility = Visibility.Hidden;
                }
                ImgIcon.Visibility = Visibility.Hidden;
            
        }

        private void btnCreate_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}

