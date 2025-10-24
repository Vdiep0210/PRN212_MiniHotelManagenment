using BusinessObjects.Models;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void AddCustomer(Customer c) => CustomerDAO.Instance.AddCustomer(c);
        public void DeleteCustomer(int id) => CustomerDAO.Instance.DeleteCustomer(id);
        public Customer? GetCustomerByEmail(string email) => CustomerDAO.Instance.GetCustomerByEmail(email);
        public Customer? GetCustomerById(int id) => CustomerDAO.Instance.GetCustomerById(id);
        public List<Customer> GetCustomers() => CustomerDAO.Instance.GetCustomers();
        public void UpdateCustomer(Customer c) => CustomerDAO.Instance.UpdateCustomer(c);
        public List<Customer> SearchByName(string q) => CustomerDAO.Instance.SearchByName(q);
    }
}
