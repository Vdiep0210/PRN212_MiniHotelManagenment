using Services;
using System;
using System.Linq;
using System.Windows;

namespace HotelManagement.Views
{
    public partial class ReportWindow : Window
    {
        private readonly BookingService _bookingService = new();

        public ReportWindow()
        {
            InitializeComponent();
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            // Lấy ngày bắt đầu và kết thúc từ DatePicker
            var from = dpFrom.SelectedDate ?? DateTime.MinValue;
            var to = dpTo.SelectedDate ?? DateTime.MaxValue;

            // Lấy danh sách Booking và lọc theo khoảng thời gian
            var list = _bookingService.GetAll()
                .Where(b =>
                    b.BookingDate.HasValue &&
                    b.BookingDate.Value.ToDateTime(TimeOnly.MinValue) >= from &&
                    b.BookingDate.Value.ToDateTime(TimeOnly.MinValue) <= to)
                .ToList();

            // Hiển thị trong DataGrid
            dgReport.ItemsSource = list;

            // Tính tổng doanh thu
            var total = list.Sum(b => b.TotalPrice ?? 0);
            MessageBox.Show($"Total revenue: {total:C}", "Report", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
