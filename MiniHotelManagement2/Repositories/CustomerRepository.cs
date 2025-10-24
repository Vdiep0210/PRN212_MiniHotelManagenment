using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class CustomerRepository : ICustomerRepository
{
    public List<Customer> GetAll() => CustomerDAO.Instance.GetAll();
    public Customer? GetById(int id) => CustomerDAO.Instance.GetById(id);
    public Customer? Login(string email, string password) => CustomerDAO.Instance.Login(email, password);
    public void Add(Customer c) => CustomerDAO.Instance.Add(c);
    public void Update(Customer c) => CustomerDAO.Instance.Update(c);
    public void Delete(int id) => CustomerDAO.Instance.Delete(id);
    public List<Customer> Search(string q) => CustomerDAO.Instance.SearchByName(q);
}
