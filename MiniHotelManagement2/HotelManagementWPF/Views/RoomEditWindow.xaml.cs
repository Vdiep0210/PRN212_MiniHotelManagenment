using System.Windows;
using BusinessObjects.Models;
using Services;

namespace HotelManagement.Views
{
    public partial class RoomEditWindow : Window
    {
        private readonly RoomService _service = new();
        private readonly RoomService _roomTypeService = new();
        private readonly RoomInformation? _existing;

        public RoomEditWindow(RoomInformation? existing = null)
        {
            InitializeComponent();
            _existing = existing;
            LoadRoomTypes();

            if (_existing != null)
            {
                Title = "Edit Room";
                txtNumber.Text = _existing.RoomNumber;
                txtDescription.Text = _existing.RoomDetailDescription;
                txtCapacity.Text = _existing.RoomMaxCapacity?.ToString();
                txtPrice.Text = _existing.RoomPricePerDay?.ToString("F2");
                cmbRoomType.SelectedValue = _existing.RoomTypeId;
            }
            else
            {
                Title = "Add New Room";
            }
        }

        private void LoadRoomTypes()
        {
            var list = _roomTypeService.GetAll();
            cmbRoomType.ItemsSource = list;
            cmbRoomType.DisplayMemberPath = "RoomTypeName";
            cmbRoomType.SelectedValuePath = "RoomTypeId";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumber.Text) || cmbRoomType.SelectedValue == null)
            {
                MessageBox.Show("Please fill all required fields!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var room = new RoomInformation
            {
                RoomNumber = txtNumber.Text.Trim(),
                RoomDetailDescription = txtDescription.Text.Trim(),
                RoomMaxCapacity = int.TryParse(txtCapacity.Text, out var cap) ? cap : null,
                RoomTypeId = (int)cmbRoomType.SelectedValue,
                RoomPricePerDay = decimal.TryParse(txtPrice.Text, out var price) ? price : null,
                RoomStatus = 1
            };

            if (_existing == null)
            {
                _service.Add(room);
                MessageBox.Show("Room added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                room.RoomId = _existing.RoomId;
                _service.Update(room);
                MessageBox.Show("Room updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            DialogResult = true;
            Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_existing == null)
            {
                MessageBox.Show("This room has not been saved yet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var confirm = MessageBox.Show($"Are you sure you want to delete room '{_existing.RoomNumber}'?",
                                          "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (confirm == MessageBoxResult.Yes)
            {
                _service.Delete(_existing.RoomId);
                MessageBox.Show("Room deleted successfully!", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
