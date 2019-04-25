using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace WPFTestAPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string mako;
        public MainWindow()
        {
            InitializeComponent();
            mako = string.Empty;
        }


        private async void BtnAPIGetToken_ClickAsync(object sender, RoutedEventArgs e)
        {            
            await Task.Run(() => KIOSKAPI.GetTokenAsync("4d60b161721d342799aeda009f2db16a", mako));
            txbToken.Text = KIOSKAPI.Token;
        }

        private async void BtnAPIClearToken_ClickAsync(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => KIOSKAPI.ClearTokenAsync(mako, KIOSKAPI.Token));
            txbToken.Clear();
        }

        private async void BtnAPICheckKIOSK_ClickAsync(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => KIOSKAPI.IsValidAsync(mako, KIOSKAPI.Token));
        }

        private void TxbMaKO_GotFocus(object sender, RoutedEventArgs e)
        {
            txbMaKO.SelectAll();
        }

        private void TxbMaKO_TextChanged(object sender, TextChangedEventArgs e)
        {
            mako = txbMaKO.Text;
        }
    }
}
