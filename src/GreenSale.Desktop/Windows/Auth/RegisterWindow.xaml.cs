using GreenSale.Desktop.Windows.Auth;
using GreenSale.Dtos.Dtos.Auth;
using GreenSale.Integrated.Interfaces.Auth;
using GreenSale.Integrated.Services.Auth;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace GreenSale.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private IAuthService _service;
        public static string phoneNum { get; set; } = string.Empty;
        public RegisterWindow()
        {
            InitializeComponent();
            this._service = new AuthService();

        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 50);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(1.5),
                maximumNotificationCount: MaximumNotificationCount.FromCount(1));

            cfg.Dispatcher = Application.Current.Dispatcher;    
        });

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnShaxsiykabinet_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            
            notifier.Dispose();
            this.Close();
        }
        private bool IsInternetAvailable()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send("www.google.com");
                    return (reply.Status == IPStatus.Success);
                }
            }
            catch
            {
                return false;
            }
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            notifier.Dispose();
            btnRegister.IsEnabled = false;
            var loader = btnRegister.Template.FindName("loader", btnRegister) as FontAwesome.WPF.ImageAwesome;
            loader.Visibility = Visibility.Visible;
            notifier.Dispose();
            bool IIsValid = true;
            if (txtFirstName.Text.Length == 0)
            {
                Border border = sender as Border;
                if (border == null)
                {
                    // Effektni yaratish va sozlash
                    DropShadowEffect dropShadowEffect = new DropShadowEffect();
                    dropShadowEffect.ShadowDepth = 0;
                    dropShadowEffect.BlurRadius = 10;
                    dropShadowEffect.Color = Colors.Red;

                    // Border ga effektni qo'shish
                    name_regs.Effect = dropShadowEffect;
                }
                /*  //string myColor = "Red";
                  Color myColor = Colors.Red;
                  string myColorString = myColor.ToString();*/
                ism_lv_rgs.Visibility = Visibility.Visible;
                //notifier.ShowInformation("Ism Bo'sh bo'lmasligi kerek!");
                IIsValid = false;
            }
            else if (txtFirstName.Text.Length < 3)
            {
                Border border = sender as Border;
                if (border == null)
                {
                    // Effektni yaratish va sozlash
                    DropShadowEffect dropShadowEffect = new DropShadowEffect();
                    dropShadowEffect.ShadowDepth = 0;
                    dropShadowEffect.BlurRadius = 10;
                    dropShadowEffect.Color = Colors.Red;

                    // Border ga effektni qo'shish
                    ism_lv_rgs.Visibility = Visibility.Visible;
                    name_regs.Effect = dropShadowEffect;
                }
                notifier.ShowInformation("Ism uzunligi 3 dan katta bo'lishi kerek!");
                IIsValid = false;
            }
            else
            {
                ism_lv_rgs.Visibility = Visibility.Collapsed;
                IIsValid = true;
                notifier.Dispose();
            }

            // familya

            if (txtLastName.Text.Length == 0)
            {
                Border border = sender as Border;
                if (border == null)
                {
                    // Effektni yaratish va sozlash
                    DropShadowEffect dropShadowEffect = new DropShadowEffect();
                    dropShadowEffect.ShadowDepth = 0;
                    dropShadowEffect.BlurRadius = 10;
                    dropShadowEffect.Color = Colors.Red;

                    // Border ga effektni qo'shish
                    sure_br.Effect = dropShadowEffect;
                }
                /*  //string myColor = "Red";
                  Color myColor = Colors.Red;
                  string myColorString = myColor.ToString();*/
                sure_lv_rgs.Visibility = Visibility.Visible;
                //notifier.ShowInformation("Familya bo'sh bo'lmasligi kerek!");
                IIsValid = false;
            }
            else if (txtLastName.Text.Length < 3)
            {
                Border border = sender as Border;
                if (border == null)
                {
                    // Effektni yaratish va sozlash
                    DropShadowEffect dropShadowEffect = new DropShadowEffect();
                    dropShadowEffect.ShadowDepth = 0;
                    dropShadowEffect.BlurRadius = 10;
                    dropShadowEffect.Color = Colors.Red;

                    // Border ga effektni qo'shish
                    sure_lv_rgs.Visibility = Visibility.Visible;
                    sure_br.Effect = dropShadowEffect;
                }
                //notifier.ShowInformation("Familya uzunligi 3 dan katta bo'lishi kerek!");
                IIsValid = false;
            }
            else
            {
                sure_lv_rgs.Visibility = Visibility.Collapsed;
                IIsValid = true;
                notifier.Dispose();
            }

            //phone

            if (txtPhoneNumber.Text.Length == 0)
            {
                Border border = sender as Border;
                if (border == null)
                {
                    // Effektni yaratish va sozlash
                    DropShadowEffect dropShadowEffect = new DropShadowEffect();
                    dropShadowEffect.ShadowDepth = 0;
                    dropShadowEffect.BlurRadius = 10;
                    dropShadowEffect.Color = Colors.Red;

                    // Border ga effektni qo'shish
                    phone_br.Effect = dropShadowEffect;
                }
                /*  //string myColor = "Red";
                  Color myColor = Colors.Red;
                  string myColorString = myColor.ToString();*/
                phone_lv_rgs.Visibility = Visibility.Visible;
                //notifier.ShowInformation("Telefon nomer bo'sh bo'lmasligi kerek!");
                IIsValid = false;
            }
            else if (txtPhoneNumber.Text.Length < 3)
            {
                Border border = sender as Border;
                if (border == null)
                {
                    // Effektni yaratish va sozlash
                    DropShadowEffect dropShadowEffect = new DropShadowEffect();
                    dropShadowEffect.ShadowDepth = 0;
                    dropShadowEffect.BlurRadius = 10;
                    dropShadowEffect.Color = Colors.Red;

                    // Border ga effektni qo'shish
                    phone_lv_rgs.Visibility = Visibility.Visible;
                    phone_br.Effect = dropShadowEffect;
                }
                //notifier.ShowInformation("Telefon nomer 3 dan katta bo'lishi kerek!");
                IIsValid = false;
                notifier.Dispose();
            }
            else
            {
                phone_lv_rgs.Visibility = Visibility.Collapsed;
                IIsValid = true;
            }

            // parol

            if (txtPassword.Password.Length == 0)
            {
                Border border = sender as Border;
                if (border == null)
                {
                    // Effektni yaratish va sozlash
                    DropShadowEffect dropShadowEffect = new DropShadowEffect();
                    dropShadowEffect.ShadowDepth = 0;
                    dropShadowEffect.BlurRadius = 10;
                    dropShadowEffect.Color = Colors.Red;

                    // Border ga effektni qo'shish
                    paswr_br.Effect = dropShadowEffect;
                }
                /*  //string myColor = "Red";
                  Color myColor = Colors.Red;
                  string myColorString = myColor.ToString();*/
                password_lv_rgs.Visibility = Visibility.Visible;
                //notifier.ShowInformation("Telefon nomer bo'sh bo'lmasligi kerek!");
                IIsValid = false;
            }
            else if (txtPassword.Password.Length < 3)
            {
                Border border = sender as Border;
                if (border == null)
                {
                    // Effektni yaratish va sozlash
                    DropShadowEffect dropShadowEffect = new DropShadowEffect();
                    dropShadowEffect.ShadowDepth = 0;
                    dropShadowEffect.BlurRadius = 10;
                    dropShadowEffect.Color = Colors.Red;

                    // Border ga effektni qo'shish
                    password_lv_rgs.Visibility = Visibility.Visible;
                    paswr_br.Effect = dropShadowEffect;
                }
                //notifier.ShowInformation("Telefon nomer 3 dan katta bo'lishi kerek!");
                IIsValid = false;
            }
            else
            {
                password_lv_rgs.Visibility = Visibility.Collapsed;
                IIsValid = true;
                notifier.Dispose();
            }


            if (IsInternetAvailable())
            {
                if (IIsValid)
                {
                    UserRegisterDto dto = new UserRegisterDto()
                    {
                        FirstName = txtFirstName.Text.ToString(),
                        LastName = txtLastName.Text.ToString(),
                        PhoneNumber = "+998" + txtPhoneNumber.Text.ToString(),
                        Password = txtPassword.Password.ToString()
                    };

                    var res = await _service.RegisterAsync(dto);

                    if (res == true)
                    {
                        var result = await _service.SendCodeForRegisterAsync(dto.PhoneNumber);
                        phoneNum = dto.PhoneNumber;
                        SendCodeWindow sendCodeWindow = new SendCodeWindow();
                        sendCodeWindow.ShowDialog();
                        /* * */
                        notifier.Dispose();
                        this.Close();
                    }
                    else
                    {
                        notifier.ShowInformation("Bu nomerdan avval ro'yxatdan o'tgan!");
                        loader.Visibility = Visibility.Collapsed;
                        btnRegister.IsEnabled = true;
                    }
                }
                else
                {
                    notifier.ShowInformation("Malumotlarni to'dirishda xatolik bor!");
                    loader.Visibility = Visibility.Collapsed;
                    btnRegister.IsEnabled = true;
                    notifier.Dispose();
                }
            }
            else if (IsInternetAvailable())
            {
                notifier.ShowError("Internetga ulanmagansiz!");
                loader.Visibility = Visibility.Collapsed;
                btnRegister.IsEnabled = true;
            }
            notifier.Dispose();
        }

        private void name_regs_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            name_regs.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#209240"));
            Border border = sender as Border;
            if (border != null)
            {
                border.BorderThickness = new Thickness(1);
            }


            if (border != null)
            {
                // Effektni yaratish va sozlash
                DropShadowEffect dropShadowEffect = new DropShadowEffect();
                dropShadowEffect.ShadowDepth = 0;
                dropShadowEffect.BlurRadius = 20;
                dropShadowEffect.Color = Colors.LightGreen;

                // Border ga effektni qo'shish
                border.Effect = dropShadowEffect;
            }
        }
        private void name_regs_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            name_regs.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F8F8F"));
            name_regs = sender as Border;
            if (name_regs != null)
            {
                name_regs.BorderThickness = new Thickness(1);
            }


            if (name_regs != null)
            {
                // Effektni yaratish va sozlash
                DropShadowEffect dropShadowEffect = new DropShadowEffect();
                dropShadowEffect.ShadowDepth = 0;
                dropShadowEffect.BlurRadius = 0;
                dropShadowEffect.Color = Colors.LightGreen;

                // Border ga effektni qo'shish
                name_regs.Effect = dropShadowEffect;
            }
        }


        private void sure_regs_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sure_br.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F8F8F"));
            sure_br = sender as Border;
            if (sure_br != null)
            {
                sure_br.BorderThickness = new Thickness(1);
            }


            if (sure_br != null)
            {
                // Effektni yaratish va sozlash
                DropShadowEffect dropShadowEffect = new DropShadowEffect();
                dropShadowEffect.ShadowDepth = 0;
                dropShadowEffect.BlurRadius = 0;
                dropShadowEffect.Color = Colors.LightGreen;

                // Border ga effektni qo'shish
                sure_br.Effect = dropShadowEffect;
            }
        }
        private void sure_regs_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sure_br.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#209240"));
            Border border = sender as Border;
            if (border != null)
            {
                border.BorderThickness = new Thickness(1);
            }


            if (border != null)
            {
                // Effektni yaratish va sozlash
                DropShadowEffect dropShadowEffect = new DropShadowEffect();
                dropShadowEffect.ShadowDepth = 0;
                dropShadowEffect.BlurRadius = 20;
                dropShadowEffect.Color = Colors.LightGreen;

                // Border ga effektni qo'shish
                border.Effect = dropShadowEffect;
            }
        }

        private void phone_Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            phone_br.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#209240"));
            Border border = sender as Border;
            if (border != null)
            {
                border.BorderThickness = new Thickness(1);
            }


            if (border != null)
            {
                // Effektni yaratish va sozlash
                DropShadowEffect dropShadowEffect = new DropShadowEffect();
                dropShadowEffect.ShadowDepth = 0;
                dropShadowEffect.BlurRadius = 20;
                dropShadowEffect.Color = Colors.LightGreen;

                // Border ga effektni qo'shish
                border.Effect = dropShadowEffect;
            }
        }

        private void phone_br_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            phone_br.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F8F8F"));
            phone_br = sender as Border;
            if (phone_br != null)
            {
                phone_br.BorderThickness = new Thickness(1);
            }


            if (phone_br != null)
            {
                // Effektni yaratish va sozlash
                DropShadowEffect dropShadowEffect = new DropShadowEffect();
                dropShadowEffect.ShadowDepth = 0;
                dropShadowEffect.BlurRadius = 0;
                dropShadowEffect.Color = Colors.LightGreen;

                // Border ga effektni qo'shish
                phone_br.Effect = dropShadowEffect;
            }
        }

        private void paswr_br_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            paswr_br.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#209240"));
            Border border = sender as Border;
            if (border != null)
            {
                border.BorderThickness = new Thickness(1);
            }


            if (border != null)
            {
                // Effektni yaratish va sozlash
                DropShadowEffect dropShadowEffect = new DropShadowEffect();
                dropShadowEffect.ShadowDepth = 0;
                dropShadowEffect.BlurRadius = 20;
                dropShadowEffect.Color = Colors.LightGreen;

                // Border ga effektni qo'shish
                border.Effect = dropShadowEffect;
            }
        }

        private void paswr_br_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            paswr_br.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F8F8F"));
            paswr_br = sender as Border;
            if (paswr_br != null)
            {
                paswr_br.BorderThickness = new Thickness(1);
            }


            if (paswr_br != null)
            {
                // Effektni yaratish va sozlash
                DropShadowEffect dropShadowEffect = new DropShadowEffect();
                dropShadowEffect.ShadowDepth = 0;
                dropShadowEffect.BlurRadius = 0;
                dropShadowEffect.Color = Colors.LightGreen;

                // Border ga effektni qo'shish
                paswr_br.Effect = dropShadowEffect;
            }
        }

        private void loc_br_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            object resource = Application.Current.TryFindResource("unpassword");
            if (resource != null)
            {
                loc_icon.Data = resource as Geometry;
            }
        }

        private void txtPhoneNumber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
