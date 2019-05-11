using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
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


        private void TxbMaKO_GotFocus(object sender, RoutedEventArgs e)
        {
            txbMaKO.SelectAll();
        }

        private void TxbMaKO_TextChanged(object sender, TextChangedEventArgs e)
        {
            mako = txbMaKO.Text;
        }

        private void BtnTestProc_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                using (QLKIOSKEntities db = new QLKIOSKEntities())
                {
                    var proc = new SP_LapHoaDon()
                    {
                        NgayLap = DateTime.Now,
                        GioLap = DateTime.Now.TimeOfDay,
                        MaSSC = "",
                        MaKO = "kiosk1",
                        MaHTTT = 1,
                        CTHDTableTypes = new List<CTHDTableType>()
                    {
                        new CTHDTableType() {MaMH = 1, SoLuong = 12, DonGiaBan = 13000, MangVe = true}
                    }
                    };

                    db.Database.ExecuteStoredProcedure(proc);
                    MessageBox.Show("Success");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void BtnGetImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (testEntities db = new testEntities())
                {
                    storeimage o = db.storeimages.FirstOrDefault(x => x.id == 1);
                    if (o != null)
                    {
                        byte[] binaryPhoto = o.image;
                        Stream stream = new MemoryStream(binaryPhoto);
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = stream;
                        bitmapImage.EndInit();
                        imgTest.Source = bitmapImage;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            String firstMacAddress = NetworkInterface.GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault();
            MessageBox.Show("MAC " + firstMacAddress);
        }

        private void BtnInsertLMH_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (QLKIOSKEntities1 db = new QLKIOSKEntities1())
                {
                    LoaiMatHang lmh = new LoaiMatHang();
                    lmh.TenLMH = "abc";
                    db.LoaiMatHangs.Add(lmh);
                    db.SaveChanges();
                    MessageBox.Show("success");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void BtnGetLoaiMatHangs_Click(object sender, RoutedEventArgs e)
        {
            //List<LoaiMatHang> list = await Task.Run(() => KIOSKAPI.GetLoaiMatHangsAsync(KIOSKAPI.Token, mako));

            //foreach (LoaiMatHang loaiMatHang in list)
            //{
            //    if (loaiMatHang.ImageLMH == null)
            //    {
            //        continue;
            //    }
            //    try
            //    {
            //        Image img = new Image();
            //        byte[] binaryPhoto = loaiMatHang.ImageLMH;
            //        Stream stream = new MemoryStream(binaryPhoto);
            //        BitmapImage bitmapImage = new BitmapImage();
            //        bitmapImage.BeginInit();
            //        bitmapImage.StreamSource = stream;
            //        bitmapImage.EndInit();
            //        img.Source = bitmapImage;
            //        img.Width = 50;
            //        img.Height = 50;

            //        spnImages.Children.Add(img);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //}

        }

        private void TxbURI_TextChanged(object sender, TextChangedEventArgs e)
        {
            KIOSKAPI.Uri = txbURI.Text;
        }


        private async void BtnLapHoaDon_Click(object sender, RoutedEventArgs e)
        {
            LapHoaDonRequestCollection requestCollection = new LapHoaDonRequestCollection();
            List<CTHDTableType> CTHDTableTypes = new List<CTHDTableType>()
            {
                new CTHDTableType() {MaMH = 1, SoLuong = 12, DonGiaBan = 13000, MangVe = true},
                new CTHDTableType() {MaMH = 2, SoLuong = 5, DonGiaBan = 7000, MangVe = false}
            };

            requestCollection.Token = KIOSKAPI.Token;
            requestCollection.MaKIOSK = mako;
            requestCollection.MaSSC = "1234";
            requestCollection.MaHTTT = 1;
            requestCollection.CTHDTableType = CTHDTableTypes;

            await Task.Run(() => KIOSKAPI.PostLapHoaDonAsync(requestCollection));

            MessageBox.Show("OK");
        }

        private async void BtnNapTien_Click(object sender, RoutedEventArgs e)
        {
            NapTienRequestCollection requestCollection = new NapTienRequestCollection();
            List<ChiTietNapTableType> chiTietNapTableTypes = new List<ChiTietNapTableType>()
            {
                new ChiTietNapTableType() {MaLT = "T5K", SoLuongTo = 2},
                new ChiTietNapTableType() {MaLT = "T50K", SoLuongTo = 3}
            };

            requestCollection.Token = KIOSKAPI.Token;
            requestCollection.MaKIOSK = mako;
            requestCollection.MaSSC = "1234";
            requestCollection.ChiTietNapTableType = chiTietNapTableTypes;

            await Task.Run(() => KIOSKAPI.PostNapTienAsync(requestCollection));

            MessageBox.Show("OK");
        }

        private async void BtnLoaiMatHang_Click(object sender, RoutedEventArgs e)
        {
            List<v_api_LoaiMatHang> list = await Task.Run(() => KIOSKAPI.GetLoaiMatHangsAsync(KIOSKAPI.Token, mako));
            DatagridWindow w = new DatagridWindow(list);
            w.ShowDialog();
        }

        private async void BtnBuoiAn_Click(object sender, RoutedEventArgs e)
        {
            List<v_api_BuoiAn> list = await Task.Run(() => KIOSKAPI.GetBuoiAnsAsync(KIOSKAPI.Token, mako));
            DatagridWindow w = new DatagridWindow(list);
            w.ShowDialog();
        }

        private async void BtnMatHang_Click(object sender, RoutedEventArgs e)
        {
            List<v_api_MatHang> list = await Task.Run(() => KIOSKAPI.GetMatHangsAsync(KIOSKAPI.Token, mako, 2, 1));
            DatagridWindow w = new DatagridWindow(list);
            w.ShowDialog();
        }

        private async void BtnHinhThucThanhToan_Click(object sender, RoutedEventArgs e)
        {
            List<v_api_HinhThucThanhToan> list = await Task.Run(() => KIOSKAPI.GetHinhThucThanhToansAsync(KIOSKAPI.Token, mako));
            DatagridWindow w = new DatagridWindow(list);
            w.ShowDialog();
        }

        private async void BtnDotKhuyenMai_Click(object sender, RoutedEventArgs e)
        {
            List<v_api_DotKhuyenMai> list = await Task.Run(() => KIOSKAPI.GetDotKhuyenMaisAsync(KIOSKAPI.Token, mako));
            DatagridWindow w = new DatagridWindow(list);
            w.ShowDialog();
        }

        private async void BtnChiTietGiamGiaTHeoSL_Click(object sender, RoutedEventArgs e)
        {
            List<v_api_ChiTietGiamGiaTheoSL> list = await Task.Run(() => KIOSKAPI.GetChiTietGiamGiaTheoSLsAsync(KIOSKAPI.Token, mako, 1));
            DatagridWindow w = new DatagridWindow(list);
            w.ShowDialog();
        }
    }
}
