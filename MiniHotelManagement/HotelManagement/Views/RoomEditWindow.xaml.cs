using BusinessObjects.Models;
using Services;
using System;
using System.Linq;
using System.Windows;

namespace HotelManagement.Views
{
    public partial class RoomEditWindow : Window
    {
        private readonly RoomService _roomService = new RoomService();
        private readonly FUMiniHotelContext _context = new FUMiniHotelContext();
        private RoomInformation? _existing;

        public RoomEditWindow(RoomInformation? room = null)
        {
            InitializeComponent();
            _existing = room;
            LoadRoomTypes();

            if (_existing != null)
            {
                Title = "Edit Room";
                txtNumber.Text = _existing.RoomNumber;
                txtDesc.Text = _existing.RoomDetailDescription;
                txtPrice.Text = _existing.RoomPricePerDay?.ToString();
                txtCapacity.Text = _existing.RoomMaxCapacity?.ToString();
                cbRoomType.SelectedValue = _existing.RoomTypeId;
            }
            else
            {
                Title = "Add Room";
            }
        }

        private void LoadRoomTypes()
        {
            var types = _context.RoomTypes.ToList();
            cbRoomType.ItemsSource = types;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumber.Text))
            {
                MessageBox.Show("Room number is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out var price))
            {
                MessageBox.Show("Invalid price format.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtCapacity.Text, out var capacity))
            {
                MessageBox.Show("Invalid capacity.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var room = new RoomInformation
            {
                RoomNumber = txtNumber.Text.Trim(),
                RoomDetailDescription = txtDesc.Text.Trim(),
                RoomPricePerDay = price,
                RoomMaxCapacity = capacity,
                RoomStatus = 1,
                RoomTypeId = (int)cbRoomType.SelectedValue
            };

            if (_existing == null)
            {
                _roomService.AddRoom(room);
            }
            else
            {
                room.RoomId = _existing.RoomId;
                _roomService.UpdateRoom(room);
            }

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
