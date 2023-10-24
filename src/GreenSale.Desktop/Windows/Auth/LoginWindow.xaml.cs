using GreenSale.Desktop.Windows.Auth;
using GreenSale.Dtos.Dtos.Auth;
using GreenSale.Integrated.Interfaces.Auth;
using GreenSale.Integrated.Security;
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
    public partial class LoginWindow : Window
    {
        private  IAuthService _authService;
        public static bool CheckEnter { get; set; } = true;

        public LoginWindow()
        {
            InitializeComponent();
            this._authService = new AuthService();
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.TopRight,
                offsetX: 20 ,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            notifier.Dispose();
            var loader = btnLogin.Template.FindName("loader", btnLogin) as FontAwesome.WPF.ImageAwesome;
            loader.Visibility = Visibility.Visible;
            bool succses = true;
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
                    Border_skns.Effect = dropShadowEffect;
                }
                ism_lv_lgn.Visibility = Visibility.Visible;
                succses = false;
            }
            else if(txtPhoneNumber.Text.Length < 9)
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
                    Border_skns.Effect = dropShadowEffect;
                }
                ism_lv_lgn.Visibility = Visibility.Visible;
                succses = false;
            }
            else
            {
                ism_lv_lgn.Visibility = Visibility.Collapsed;
                notifier.Dispose();
            }


            if (txtParol is null || txtParol.SecurePassword.Length < 8)
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
                    Border_pasword.Effect = dropShadowEffect;
                }
                parol_lv_lgn.Visibility= Visibility.Visible;
                succses = false;
            }
            else
            {
                parol_lv_lgn.Visibility = Visibility.Collapsed;
                notifier.Dispose();
            }
            if (IsInternetAvailable())
            {
                if (succses)
                {
                    UserLoginDto dto = new UserLoginDto()
                    {
                        PhoneNumber = ("+998" + txtPhoneNumber.Text.ToString()),
                        password = txtParol.Password.ToString()
                    };
                    CheckEnter = true;
                    var res = await _authService.LoginAsync(dto);

                    if (res.Result)
                    {
                        IdentitySingelton.GetInstance().Token = res.Token;
                        MainWindow window = new MainWindow();
                        window.Show();
                        notifier.Dispose();
                        loader.Visibility = Visibility.Collapsed;
                        this.Close();
                    }
                    else
                    {
                        notifier.ShowWarning("Bunday foydalanuvchi mavjud eams !");
                        
                        loader.Visibility = Visibility.Collapsed;
                    }
                    notifier.Dispose();
                }
                else
                {
                    notifier.ShowInformation("Telefon yoki Parol natogri kiritildi!");
                    loader.Visibility = Visibility.Collapsed;
                }
                notifier.Dispose();
            }
            else
            {
                notifier.ShowError("Internetga ulanmagansiz !");
                loader.Visibility = Visibility.Collapsed;
            }
            loader.Visibility = Visibility.Collapsed;
            notifier.Dispose();
        }

        private void btnRoyxatdanOtish_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            /* * */
            notifier.Dispose();
            this.Close();
        }

        private void txtPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
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

        private void Forgot_password(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ForgotPasswordWindow sendCodeWindow = new ForgotPasswordWindow();
            sendCodeWindow.ShowDialog();
        }

        private void changeCollor(object sender, System.Windows.Input.MouseEventArgs e)
        {
            forgotPassword.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#329DFF"));
        }

        private void old_collor(object sender, System.Windows.Input.MouseEventArgs e)
        {
            forgotPassword.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#083353"));
        }

        private void txtPhoneNumber_MouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Border_skns.BorderBrush = Brushes.Red;
        }

        private void Border_skns_MouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border_skns.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#209240"));
            Border border = sender as Border;


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

        private void Border_skns_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border_skns.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F8F8F"));
            Border_skns = sender as Border;
            if (Border_skns != null)
            {
                Border_skns.BorderThickness = new Thickness(1);
            }


            if (Border_skns != null)
            {
                // Effektni yaratish va sozlash
                DropShadowEffect dropShadowEffect = new DropShadowEffect();
                dropShadowEffect.ShadowDepth = 0;
                dropShadowEffect.BlurRadius = 0;
                dropShadowEffect.Color = Colors.LightGreen;

                // Border ga effektni qo'shish
                Border_skns.Effect = dropShadowEffect;
            }

        }

        private void Border_pasword_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border_pasword.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#209240"));
            Border border = sender as Border;
            /*if (border != null)
            {
                border.BorderThickness = new Thickness(2);
            }*/

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

        private void Border_pasword_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border_pasword.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F8F8F"));
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
                dropShadowEffect.BlurRadius = 0;
                dropShadowEffect.Color = Colors.LightGreen;

                // Border ga effektni qo'shish
                border.Effect = dropShadowEffect;
            }
        }
    }
}
