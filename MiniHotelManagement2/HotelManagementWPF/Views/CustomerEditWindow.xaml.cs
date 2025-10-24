using BusinessObjects.Models;
using Services;
using System;
using System.Windows;

namespace HotelManagement.Views
{
    public partial class CustomerEditWindow : Window
    {
        private readonly CustomerService _service = new();
        private readonly Customer? _existing;

        public CustomerEditWindow(Customer? existing = null)
        {
            InitializeComponent();
            _existing = existing;

            if (_existing != null)
            {
                Title = "Edit Customer";
                txtName.Text = _existing.CustomerFullName;
                txtEmail.Text = _existing.EmailAddress;
                txtPhone.Text = _existing.Telephone;
                txtBirthday.Text = _existing.CustomerBirthday?.ToString("yyyy-MM-dd") ?? string.Empty;
            }
            else
            {
                Title = "Add New Customer";
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Name and Email are required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // ✅ Convert từ DateTime? sang DateOnly?
            DateOnly? dob = null;
            if (DateTime.TryParse(txtBirthday.Text, out var dt))
                dob = DateOnly.FromDateTime(dt);

            var cust = new Customer
            {
                CustomerFullName = txtName.Text.Trim(),
                EmailAddress = txtEmail.Text.Trim(),
                Telephone = txtPhone.Text.Trim(),
                CustomerBirthday = dob, // ✅ fix kiểu đúng
                CustomerStatus = 1,
                Password = _existing?.Password ?? "default"
            };

            if (_existing == null)
            {
                _service.Add(cust);
                MessageBox.Show("Customer added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                cust.CustomerId = _existing.CustomerId;
                _service.Update(cust);
                MessageBox.Show("Customer updated successfully!", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            DialogResult = true;
            Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_existing == null)
            {
                MessageBox.Show("Cannot delete unsaved record.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete customer '{_existing.CustomerFullName}'?",
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (confirm == MessageBoxResult.Yes)
            {
                _service.Delete(_existing.CustomerId);
                MessageBox.Show("Customer deleted successfully!", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
