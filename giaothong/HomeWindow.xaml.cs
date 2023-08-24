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
using LiveCharts;
using LiveCharts.Wpf;

namespace giaothong
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
        }
        // Icon đóng ứng dụng
        private void CloseIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát khỏi ứng dụng?", "Xác nhận thoát", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        
        // Icon ẩn ứng dụng
        private void MinimizeIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // Icon phóng to thu nhỏ
        private void MaximizeIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        //Thay đổi kích thước ô thống kê khi phóng to
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                thirdRow.Height = new GridLength(200); // Set the height to 200 when maximized
                titlesize1.FontSize = 25;
                numbersize1.FontSize = 30;
                iconsize1.Height = 80;
                iconsize1.Width = 80;

                titlesize2.FontSize = 25;
                numbersize2.FontSize = 30;
                iconsize2.Height = 80;
                iconsize2.Width = 80;

                titlesize3.FontSize = 25;
                numbersize3.FontSize = 30;
                iconsize3.Height = 80;
                iconsize3.Width = 80;
            }
            else
            {
                thirdRow.Height = new GridLength(150); // Restore the height to 150 when restored
                titlesize1.FontSize = 16;
                numbersize1.FontSize = 22;
                iconsize1.Height = 50;
                iconsize1.Width = 50;

                titlesize2.FontSize = 16;
                numbersize2.FontSize = 22;
                iconsize2.Height = 50;
                iconsize2.Width = 50;

                titlesize3.FontSize = 16;
                numbersize3.FontSize = 22;
                iconsize3.Height = 50;
                iconsize3.Width = 50;
            }
        }
    }
}
