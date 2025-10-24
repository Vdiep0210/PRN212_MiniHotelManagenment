using BusinessObjects.Models;
using Services;
using System.Windows;
using System.Windows.Input;

namespace HotelManagement.Views;

public partial class MainWindow : Window
{
    private readonly CustomerService _custService = new();
    private readonly RoomService _roomService = new();
    private readonly BookingService _bookingService = new();
    private readonly string _role;
    private readonly int? _customerId;

    public MainWindow(string role, int? customerId = null)
    {
        InitializeComponent();
        _role = role;
        _customerId = customerId;
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        LoadCustomers();
        LoadRooms();
        LoadBookings();
    }

    private void LoadCustomers() => dgCustomers.ItemsSource = _custService.GetAll();
    private void LoadRooms() => dgRooms.ItemsSource = _roomService.GetAll();
    private void LoadBookings() => dgBookings.ItemsSource = _bookingService.GetAll();

    private void BtnSearchCustomer_Click(object sender, RoutedEventArgs e)
    {
        var q = txtSearchCust.Text.Trim();
        dgCustomers.ItemsSource = string.IsNullOrEmpty(q) ? _custService.GetAll() : _custService.Search(q);
    }

    private void BtnSearchRoom_Click(object sender, RoutedEventArgs e)
    {
        var q = txtSearchRoom.Text.Trim();
        dgRooms.ItemsSource = string.IsNullOrEmpty(q) ? _roomService.GetAll() : _roomService.Search(q);
    }

    private void BtnNewCustomer_Click(object sender, RoutedEventArgs e)
    {
        var win = new CustomerEditWindow();
        if (win.ShowDialog() == true) LoadCustomers();
    }

    private void BtnNewRoom_Click(object sender, RoutedEventArgs e)
    {
        var win = new RoomEditWindow();
        if (win.ShowDialog() == true) LoadRooms();
    }

    private void dgRooms_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (dgRooms.SelectedItem is RoomInformation selected)
        {
            var win = new RoomEditWindow(selected);
            if (win.ShowDialog() == true) LoadRooms();
        }
    }

    private void BtnDeleteRoom_Click(object sender, RoutedEventArgs e)
    {
        if (dgRooms.SelectedItem is RoomInformation selected)
        {
            var confirm = MessageBox.Show($"Are you sure you want to delete room #{selected.RoomNumber}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm == MessageBoxResult.Yes) { _roomService.Delete(selected.RoomId); LoadRooms(); }
        }
        else MessageBox.Show("Please select a room to delete.", "No selection", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void dgCustomers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (dgCustomers.SelectedItem is Customer selected)
        {
            var win = new CustomerEditWindow(selected);
            if (win.ShowDialog() == true) LoadCustomers();
        }
    }

    private void BtnNewBooking_Click(object sender, RoutedEventArgs e)
    {
        var win = new BookingEditWindow();
        if (win.ShowDialog() == true) LoadBookings();
    }

    private void BtnReport_Click(object sender, RoutedEventArgs e)
    {
        var win = new ReportWindow();
        win.ShowDialog();
    }
}
