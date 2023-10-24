using GreenSale.Integrated.Interfaces.Users;
using GreenSale.Integrated.Services.Users;
using GreenSale.ViewModels.Models.Users;
using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

namespace GreenSale.Desktop.Pages.Accunt
{
    /// <summary>
    /// Interaction logic for UserAccaunt.xaml
    /// </summary>
    public partial class UserAccaunt : Page
    {
        private IUserService _service;
        List<List<string>> lists = new List<List<string>>();

        public UserAccaunt()
        {
            InitializeComponent();
            this._service = new UserService();
        }

        private void User_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        public void cmbRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            List<string> lstToshkentViloyati = new List<string>() { "Bekobod", "Boʻka", "Boʻstonliq", "Zangiota", "Oqqoʻrgʻon", "Ohangaron", "Parkent", "Piskent", "Chinoz", "Yuqori Chirchiq", "Yangiyoʻl", "Oʻrta Chirchiq", "Qibray", "Quyi Chirchiq" };
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

            lists.Add(lstToshkent);
            lists.Add(lstAndijon);
            lists.Add(lstNamangan);
            lists.Add(lstFargona);
            lists.Add(lstSirdaryo);
            lists.Add(lstXorazm);
            lists.Add(lstBuxoro);
            lists.Add(lstQashqadaryo);
            lists.Add(lstSurxondaryo);
            lists.Add(lstSamarqand);
            lists.Add(lstNavoiy);
            lists.Add(lstJizzax);


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

        public async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var result = await _service.GetAsync();
            if (result != null)
            {
                txt_name.Text = result.FirstName;
                txt_lastname.Text = result.LastName;
                txt_phone.Text = result.PhoneNumber.Substring(4);
                txt_adres.Text = result.Address.ToString();
                cmbRegion.Text = result.Region;

                foreach(var item in lists) 
                {
                    for(int i  = 0; i < item.Count; i++)
                    {
                        if (item[i].ToString().Contains(result.District))
                        {
                            cmbDistrict.SelectedIndex = i;
                        }
                    }
                } 
            }
        }

        public bool IsValid()
        {
            bool isValid = false;
            if(txt_name.Text.Length == 0)
            {
                ism_lv_lgn.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (txt_name.Text.Length < 3)
            {
                ism_lv_lgn.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                ism_lv_lgn.Visibility = Visibility.Collapsed;
                isValid = true;
            }

            if (txt_lastname.Text.Length == 0)
            {
                ism_lv_stting.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (txt_lastname.Text.Length < 3)
            {
                ism_lv_stting.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                ism_lv_stting.Visibility = Visibility.Collapsed;
                isValid = true;
            }

            if (txt_phone.Text.Length < 9 || txt_phone.Text.Length == 0)
            {
                ism_lv_phone.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                ism_lv_phone.Visibility = Visibility.Collapsed;
                isValid = true;
            }

            //----------------------------------------

            if(cmbRegion.Text.Length == 0)
            {
                regn_lv_lgn.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                regn_lv_lgn.Visibility = Visibility.Hidden;
                isValid = true;
            }

            if(cmbDistrict.Text.Length == 0)
            {
                distrkt_lv_lgn.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                distrkt_lv_lgn.Visibility = Visibility.Hidden;
                isValid = true;
            }

            if(txt_adres.Text.Length == 0)
            {
                addres_lv_lgn.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (txt_adres.Text.Length  < 3)
            {
                addres_lv_lgn.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                addres_lv_lgn.Visibility = Visibility.Hidden;
                isValid = true;
            }

            return isValid;
        }



        private async void shaxsiy_info(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                UserDto userDto = new UserDto()
                {
                    FirstName = txt_name.Text,
                    LastName = txt_lastname.Text,
                    PhoneNumber = "+998" + txt_phone.Text,
                    Region = cmbRegion.Text,
                    District = cmbDistrict.Text,
                    Address = txt_adres.Text
                };

                var result = await _service.UpdateAsync(userDto);
                if (result)
                {
                    MessageBox.Show("oxshadi");

                }
                else
                {
                    MessageBox.Show("oxshamadi");
                }
            }
            else
            {
                MessageBox.Show("validator");
            }
        }


        private async void saqlsh_location_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                UserDto userDto = new UserDto()
                {
                    FirstName = txt_name.Text,
                    LastName = txt_lastname.Text,
                    PhoneNumber = "+998" + txt_phone.Text,
                    Region = cmbRegion.Text,
                    District = cmbDistrict.Text,
                    Address = txt_adres.Text
                };

                var result = await _service.UpdateAsync(userDto);
                if (result)
                {
                    MessageBox.Show("oxshadi");

                }
                else
                {
                    MessageBox.Show("oxshamadi");
                }
            }
            else
            {
                MessageBox.Show("validator");
            }
        }

        private async void saqlsh_security_Click(object sender, RoutedEventArgs e)
        {
            UsersecurtyDto usersecurtyDto = new UsersecurtyDto()
            {
                OldPassword = txt_oldpassword.Password.ToString(),
                NewPassword = txt_newpassword.Password.ToString(),
                ReturnNewPassword = txt_returnpassword.Password.ToString()
            };

            var result = await _service.UpdateSecurityAsync(usersecurtyDto);

            if (result)
            {
                MessageBox.Show("oxshadi");

            }
            else
            {
                MessageBox.Show("oxshamadi");
            }

        }

        
    }
}
