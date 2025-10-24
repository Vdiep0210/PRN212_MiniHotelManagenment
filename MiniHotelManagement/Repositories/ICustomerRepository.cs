using BusinessObjects.Models;
using System.Collections.Generic;

namespace Repositories
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
        Customer? GetCustomerById(int id);
        Customer? GetCustomerByEmail(string email);
        void AddCustomer(Customer c);
        void UpdateCustomer(Customer c);
        void DeleteCustomer(int id);
        List<Customer> SearchByName(string q);
    }
}
