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

namespace WPFTestAPI
{
    /// <summary>
    /// Interaction logic for DatagridWindow.xaml
    /// </summary>
    public partial class DatagridWindow : Window
    {
        public DatagridWindow(List<v_api_LoaiMatHang> list)
        {
            InitializeComponent();
            dgData.ItemsSource = list;
        }

        public DatagridWindow(List<v_api_BuoiAn> list)
        {
            InitializeComponent();
            dgData.ItemsSource = list;
        }

        public DatagridWindow(List<v_api_MatHang> list)
        {
            InitializeComponent();
            dgData.ItemsSource = list;
        }

        public DatagridWindow(List<v_api_HinhThucThanhToan> list)
        {
            InitializeComponent();
            dgData.ItemsSource = list;
        }

        public DatagridWindow(List<v_api_DotKhuyenMai> list)
        {
            InitializeComponent();
            dgData.ItemsSource = list;
        }

        public DatagridWindow(List<v_api_ChiTietGiamGiaTheoSL> list)
        {
            InitializeComponent();
            dgData.ItemsSource = list;
        }

        public DatagridWindow(List<v_api_QuangCao> list)
        {
            InitializeComponent();
            dgData.ItemsSource = list;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
