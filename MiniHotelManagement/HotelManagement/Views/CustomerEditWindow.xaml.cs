using System;
using System.Windows;
using BusinessObjects.Models;
using Services;

namespace HotelManagement.Views
{
    public partial class CustomerEditWindow : Window
    {
        private readonly CustomerService _service = new CustomerService();
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
                txtBirthday.Text = _existing.CustomerBirthday?.ToString("yyyy-MM-dd");
                txtPass.Password = _existing.Password ?? "";
            }
            else
            {
                Title = "Add New Customer";
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string pass = txtPass.Password.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Please fill in all required fields!", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime? dob = null;
            if (DateTime.TryParse(txtBirthday.Text, out var dt)) dob = dt;

            var cust = new Customer
            {
                CustomerFullName = name,
                EmailAddress = email,
                Telephone = phone,
                CustomerBirthday = dob,
                CustomerStatus = 1,
                Password = pass
            };

            if (_existing == null)
            {
                _service.AddCustomer(cust);
                MessageBox.Show("Customer added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                cust.CustomerId = _existing.CustomerId;
                _service.UpdateCustomer(cust);
                MessageBox.Show("Customer updated successfully!", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            DialogResult = true;
            Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_existing == null)
            {
                MessageBox.Show("Cannot delete. This customer has not been saved yet.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete customer '{_existing.CustomerFullName}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm == MessageBoxResult.Yes)
            {
                _service.DeleteCustomer(_existing.CustomerId);
                MessageBox.Show("Customer deleted successfully!", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
