using BusinessObjects.Models;
using Services;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HotelManagement.Views
{
    public partial class MainWindow : Window
    {
        private readonly CustomerService _custService = new CustomerService();
        private readonly RoomService _roomService = new RoomService();
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
        }

        private void LoadCustomers()
        {
            dgCustomers.ItemsSource = _custService.GetCustomers();
        }

        private void LoadRooms()
        {
            dgRooms.ItemsSource = _roomService.GetRooms();
        }

        private void BtnSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            var q = txtSearchCust.Text.Trim();
            dgCustomers.ItemsSource = string.IsNullOrEmpty(q)
                ? _custService.GetCustomers()
                : _custService.SearchByName(q);
        }

        private void BtnSearchRoom_Click(object sender, RoutedEventArgs e)
        {
            var q = txtSearchRoom.Text.Trim();
            dgRooms.ItemsSource = string.IsNullOrEmpty(q)
                ? _roomService.GetRooms()
                : _roomService.SearchByNumberOrType(q);
        }

        private void BtnNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            var win = new CustomerEditWindow();
            if (win.ShowDialog() == true)
                LoadCustomers();
        }

        private void BtnNewRoom_Click(object sender, RoutedEventArgs e)
        {
            var win = new RoomEditWindow();
            if (win.ShowDialog() == true)
                LoadRooms();
        }

        private void dgRooms_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgRooms.SelectedItem is RoomInformation selected)
            {
                var editWin = new RoomEditWindow(selected);
                if (editWin.ShowDialog() == true)
                {
                    LoadRooms();
                }
            }
        }

        private void BtnDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem is RoomInformation selected)
            {
                var confirm = MessageBox.Show($"Are you sure you want to delete room #{selected.RoomNumber}?",
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (confirm == MessageBoxResult.Yes)
                {
                    _roomService.DeleteRoom(selected.RoomId);
                    LoadRooms();
                }
            }
            else
            {
                MessageBox.Show("Please select a room to delete.", "No selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void dgCustomers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgCustomers.SelectedItem is Customer selected)
            {
                var win = new CustomerEditWindow(selected);
                if (win.ShowDialog() == true)
                    LoadCustomers();
            }
        }
    }
}
