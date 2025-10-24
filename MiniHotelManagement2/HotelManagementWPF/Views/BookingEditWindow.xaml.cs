using BusinessObjects.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HotelManagement.Views
{
    public partial class BookingEditWindow : Window
    {
        private readonly CustomerService _custService = new();
        private readonly RoomService _roomService = new();
        private readonly BookingService _bookingService = new();

        public BookingEditWindow()
        {
            InitializeComponent();
            LoadData();
            dpStart.SelectedDateChanged += OnDateChanged;
            dpEnd.SelectedDateChanged += OnDateChanged;
            cbRoom.SelectionChanged += OnSelectionChanged;
        }

        private void LoadData()
        {
            // Load customers
            cbCustomer.ItemsSource = _custService.GetAll();
            cbCustomer.DisplayMemberPath = "CustomerFullName";
            cbCustomer.SelectedValuePath = "CustomerId"; // ✅ đúng property

            // Load rooms
            cbRoom.ItemsSource = _roomService.GetAll();
            cbRoom.DisplayMemberPath = "RoomNumber";
            cbRoom.SelectedValuePath = "RoomId"; // ✅ đúng property
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbCustomer.SelectedValue == null || cbRoom.SelectedValue == null ||
                dpStart.SelectedDate == null || dpEnd.SelectedDate == null)
            {
                MessageBox.Show("Please fill all fields!", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var start = DateOnly.FromDateTime(dpStart.SelectedDate.Value);
            var end = DateOnly.FromDateTime(dpEnd.SelectedDate.Value);

            if (end < start)
            {
                MessageBox.Show("End date must be after start date!", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var days = (end.ToDateTime(TimeOnly.MinValue) - start.ToDateTime(TimeOnly.MinValue)).Days + 1;
            var room = (RoomInformation)cbRoom.SelectedItem;
            var total = (room.RoomPricePerDay ?? 0) * days;

            var reservation = new BookingReservation
            {
                BookingDate = DateOnly.FromDateTime(DateTime.Now),
                CustomerId = (int)cbCustomer.SelectedValue,
                TotalPrice = total,
                BookingStatus = 1,
                BookingDetails = new List<BookingDetail>()
            };

            var detail = new BookingDetail
            {
                RoomId = (int)cbRoom.SelectedValue,
                StartDate = start,
                EndDate = end,
                ActualPrice = room.RoomPricePerDay
            };

            reservation.BookingDetails.Add(detail);
            _bookingService.Add(reservation);

            MessageBox.Show("Booking saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) => Close();

        // ==============================
        // 🧮 AUTO-CALCULATE TOTAL PRICE
        // ==============================
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e) => CalculateTotal();
        private void OnDateChanged(object sender, SelectionChangedEventArgs e) => CalculateTotal();

        private void CalculateTotal()
        {
            if (cbRoom.SelectedItem is not RoomInformation room ||
                dpStart.SelectedDate == null ||
                dpEnd.SelectedDate == null)
            {
                txtTotal.Text = string.Empty;
                return;
            }

            var start = DateOnly.FromDateTime(dpStart.SelectedDate.Value);
            var end = DateOnly.FromDateTime(dpEnd.SelectedDate.Value);

            if (end < start)
            {
                txtTotal.Text = "Invalid date range";
                return;
            }

            var days = (end.ToDateTime(TimeOnly.MinValue) - start.ToDateTime(TimeOnly.MinValue)).Days + 1;
            var total = (room.RoomPricePerDay ?? 0) * days;
            txtTotal.Text = $"{total:C}"; // Hiển thị dạng tiền tệ
        }
    }
}
