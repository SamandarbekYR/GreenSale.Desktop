using GreenSale.Dtos.Dtos.Auth;
using GreenSale.Integrated.Interfaces.Auth;
using GreenSale.Integrated.Services.Auth;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Effects;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static GreenSale.Integrated.Services.UseWindowServices.BlurBehindDemo;

namespace GreenSale.Desktop.Windows.Auth
{



    /// <summary>
    /// Interaction logic for ForgotPasswordWindow.xaml
    /// </summary>
    public partial class ForgotPasswordWindow : Window
    {
        public static string number { get; set; }
        private IAuthService _service;
        public ForgotPasswordWindow()
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
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        private void btnCloseResetPassword_Click(object sender, RoutedEventArgs e)
        {
            notifier.Dispose();
            this.Close();
        }

        private void btnBackResetPassword_Click(object sender, RoutedEventArgs e)
        {
            notifier.Dispose();
            this.Close();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
        }

        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private async void btnResetPasswordSend(object sender, RoutedEventArgs e)
        {
            bool Isvalid = true;

            notifier.Dispose();
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
                    telForgotPasword.Effect = dropShadowEffect;
                }
                /*  //string myColor = "Red";
                  Color myColor = Colors.Red;
                  string myColorString = myColor.ToString();*/
                phone_lv_rgs.Visibility = Visibility.Visible;
                //notifier.ShowInformation("Telefon nomer bo'sh bo'lmasligi kerek!");
                Isvalid = false;
            }
            else if (txtPhoneNumber.Text.Length < 9)
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
                    telForgotPasword.Effect = dropShadowEffect;
                }
                //notifier.ShowInformation("Telefon nomer 3 dan katta bo'lishi kerek!");
                Isvalid = false;
            }
            else
            {
                phone_lv_rgs.Visibility = Visibility.Collapsed;
                notifier.Dispose();
                Isvalid = true;
            }

            // parol

            if (txtParol.Password.Length == 0)
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
                    nepasword.Effect = dropShadowEffect;
                }
                /*  //string myColor = "Red";
                  Color myColor = Colors.Red;
                  string myColorString = myColor.ToString();*/
                password_lv_rgs.Visibility = Visibility.Visible;
                notifier.ShowInformation("Telefon nomer bo'sh bo'lmasligi kerek!");
                Isvalid = false;
            }
            else if (txtParol.Password.Length < 8)
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
                    nepasword.Effect = dropShadowEffect;
                }
                notifier.ShowInformation("Password 8 belgidan oshmasligi kerek!");
                Isvalid = false;
            }
            else
            {
                notifier.Dispose();
                password_lv_rgs.Visibility = Visibility.Collapsed;
                Isvalid = true;
            }

            if (Isvalid)
            {
                LoginWindow.CheckEnter = false;
                number = "+998" + txtPhoneNumber.Text.ToString();
                ForgotPassword forgotPassword = new ForgotPassword()
                {
                    PhoneNumber = number,
                    NewPassword = txtParol.Password.ToString()
                };

                var result = await _service.ResetPasswordAsync(forgotPassword);

                if (result)
                {
                    SendCodeWindow sendCodeWindow = new SendCodeWindow();
                    sendCodeWindow.ShowDialog();
                    notifier.Dispose();
                    this.Close();
                }
                else
                {
                    notifier.ShowInformation("Code Natogri kiritildi !");
                    notifier.Dispose();
                }
                notifier.Dispose();
            }

        }

        private void Border_skns_MouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            telForgotPasword.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#209240"));
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
            telForgotPasword.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F8F8F"));
            telForgotPasword = sender as Border;
            if (telForgotPasword != null)
            {
                telForgotPasword.BorderThickness = new Thickness(1);
            }


            if (telForgotPasword != null)
            {
                // Effektni yaratish va sozlash
                DropShadowEffect dropShadowEffect = new DropShadowEffect();
                dropShadowEffect.ShadowDepth = 0;
                dropShadowEffect.BlurRadius = 0;
                dropShadowEffect.Color = Colors.LightGreen;

                // Border ga effektni qo'shish
                telForgotPasword.Effect = dropShadowEffect;
            }

        }

        private void Border_pasword_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            nepasword.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#209240"));
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
            nepasword.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F8F8F"));
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

