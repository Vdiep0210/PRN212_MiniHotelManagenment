using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public sealed class CustomerDAO
    {
        private static readonly CustomerDAO _instance = new CustomerDAO();
        public static CustomerDAO Instance => _instance;
        private CustomerDAO() { }

        public List<Customer> GetCustomers()
        {
            using var ctx = new FUMiniHotelContext();
            return ctx.Customers.AsNoTracking().ToList();
        }

        public Customer? GetCustomerById(int id)
        {
            using var ctx = new FUMiniHotelContext();
            return ctx.Customers.Find(id);
        }

        public Customer? GetCustomerByEmail(string email)
        {
            using var ctx = new FUMiniHotelContext();
            return ctx.Customers.FirstOrDefault(c => c.EmailAddress == email);
        }

        public void AddCustomer(Customer c)
        {
            using var ctx = new FUMiniHotelContext();
            ctx.Customers.Add(c);
            ctx.SaveChanges();
        }

        public void UpdateCustomer(Customer c)
        {
            using var ctx = new FUMiniHotelContext();
            var e = ctx.Customers.Find(c.CustomerId);
            if (e != null)
            {
                ctx.Entry(e).CurrentValues.SetValues(c);
                ctx.SaveChanges();
            }
        }

        public void DeleteCustomer(int id)
        {
            using var ctx = new FUMiniHotelContext();
            var e = ctx.Customers.Find(id);
            if (e != null)
            {
                ctx.Customers.Remove(e);
                ctx.SaveChanges();
            }
        }

        public List<Customer> SearchByName(string q)
        {
            using var ctx = new FUMiniHotelContext();
            return ctx.Customers
                      .Where(c => c.CustomerFullName != null && c.CustomerFullName.Contains(q))
                      .AsNoTracking()
                      .ToList();
        }
    }
}
