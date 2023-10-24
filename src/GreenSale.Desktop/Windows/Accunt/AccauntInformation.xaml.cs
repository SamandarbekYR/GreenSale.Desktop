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
using System.Windows.Shapes;

namespace GreenSale.Desktop.Windows.Accunt
{
    /// <summary>
    /// Interaction logic for AccauntInformation.xaml
    /// </summary>
    public partial class AccauntInformation : Window
    {
        public static bool info { get; set; } = false;
        public static bool location_info { get; set; } = false;
        public AccauntInformation()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (info == false)
            {
                information.Visibility = Visibility.Visible;
                info = true;
            }
            else if (info == true)
            {
                information.Visibility = Visibility.Collapsed;
                info = false;
            }
        }

        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (location_info == false)
            {
                Location_information.Visibility = Visibility.Collapsed;
                location_info = true;
            }
            else if (location_info == true)
            {
                Location_information.Visibility = Visibility.Visible;
                location_info = false;
            }
        }
    }
}
