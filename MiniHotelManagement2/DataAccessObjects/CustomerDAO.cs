using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects;
public sealed class CustomerDAO
{
    private static readonly CustomerDAO _instance = new();
    public static CustomerDAO Instance => _instance;
    private CustomerDAO() { }

    public List<Customer> GetAll()
    {
        using var ctx = new FUMiniHotelContext();
        return ctx.Customers.ToList();
    }

    public Customer? GetById(int id)
    {
        using var ctx = new FUMiniHotelContext();
        return ctx.Customers.Find(id);
    }

    public Customer? Login(string email, string password)
    {
        using var ctx = new FUMiniHotelContext();
        return ctx.Customers.FirstOrDefault(c => c.EmailAddress == email && c.Password == password);
    }

    public void Add(Customer c)
    {
        using var ctx = new FUMiniHotelContext();
        ctx.Customers.Add(c);
        ctx.SaveChanges();
    }

    public void Update(Customer c)
    {
        using var ctx = new FUMiniHotelContext();
        var e = ctx.Customers.Find(c.CustomerId);
        if (e != null) { ctx.Entry(e).CurrentValues.SetValues(c); ctx.SaveChanges(); }
    }

    public void Delete(int id)
    {
        using var ctx = new FUMiniHotelContext();
        var e = ctx.Customers.Find(id);
        if (e != null) { ctx.Customers.Remove(e); ctx.SaveChanges(); }
    }

    public List<Customer> SearchByName(string q)
    {
        using var ctx = new FUMiniHotelContext();
        return ctx.Customers.Where(c => c.CustomerFullName != null && c.CustomerFullName.Contains(q)).ToList();
    }
}
