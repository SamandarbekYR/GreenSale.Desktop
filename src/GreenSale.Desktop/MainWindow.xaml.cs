using GreenSale.Desktop.Pages;
using GreenSale.Desktop.Pages.Accunt;
using GreenSale.Desktop.Pages.Buyers;
using GreenSale.Desktop.Pages.CreateAd;
using GreenSale.Desktop.Pages.Dashbord;
using GreenSale.Desktop.Pages.Sellers;
using GreenSale.Desktop.Pages.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace GreenSale.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public  bool suscs { get; set; } = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.BottomRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        bool IsMaximized = false;

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    IsMaximized = false;
                }

                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AsosiyButton_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            FrameFilter.Content = dashboard;
        }

        private void btnCreateAd_Click(object sender, RoutedEventArgs e)
        {
            ChooseAd chooseAd = new ChooseAd();
            FrameFilter.Content=chooseAd;
        }

        private void btnSeller_Click(object sender, RoutedEventArgs e)
        {
            SellerPage sellerPage = new SellerPage();
            FrameFilter.Content = sellerPage;
        }

        public  void refresh()
        {
            SellerPage sellerPage = new SellerPage();
            FrameFilter.Content = sellerPage;
        }
        private void btnStorage_Click(object sender, RoutedEventArgs e)
        {
            StoragePage storagePage = new StoragePage();
            FrameFilter.Content = storagePage;
        }

        private void btnBuyer_Click(object sender, RoutedEventArgs e)
        {
            BuyerPage buyerPage = new BuyerPage();
            FrameFilter.Content = buyerPage;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMaximized_Click(object sender, RoutedEventArgs e)
        {
            
            if(WindowState == WindowState.Maximized )
            {
                WindowState = WindowState.Normal;
            }
            else WindowState = WindowState.Maximized;
        }

        private void btnMinimized_Click(object sender, RoutedEventArgs e)
        {
            WindowState= WindowState.Minimized;
        }

        private void brdUser_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_Loaded_Main(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            FrameFilter.Content = dashboard;
            notifier.ShowSuccess("Mufaqiyatli kirdingiz.");
        }
        

        public void Succses()
        {
            notifier.ShowSuccess("Mufaqiyatli kirdingiz.");
        }

        private void setting_Click(object sender, RoutedEventArgs e)
        {
            UserAccaunt userAccaunt = new UserAccaunt();
            FrameFilter.Content = userAccaunt;
        }
    }
}
