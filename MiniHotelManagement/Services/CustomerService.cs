using BusinessObjects.Models;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repo = new CustomerRepository();

        public List<Customer> GetCustomers() => _repo.GetCustomers();

        public Customer? GetById(int id) => _repo.GetCustomerById(id);

        public Customer? Login(string email, string password)
        {
            var u = _repo.GetCustomerByEmail(email);
            if (u != null && u.Password == password) return u;
            return null;
        }

        public void AddCustomer(Customer c)
        {
            // basic validation
            if (string.IsNullOrWhiteSpace(c.EmailAddress)) throw new System.ArgumentException("Email required");
            _repo.AddCustomer(c);
        }

        public void UpdateCustomer(Customer c) => _repo.UpdateCustomer(c);
        public void DeleteCustomer(int id) => _repo.DeleteCustomer(id);
        public List<Customer> SearchByName(string q) => _repo.SearchByName(q);
    }
}
