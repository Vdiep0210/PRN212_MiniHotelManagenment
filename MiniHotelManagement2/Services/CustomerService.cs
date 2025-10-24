using BusinessObjects.Models;
using Repositories;

namespace Services;

public class CustomerService
{
    private readonly ICustomerRepository _repo = new CustomerRepository();
    public List<Customer> GetAll() => _repo.GetAll();
    public Customer? GetById(int id) => _repo.GetById(id);
    public Customer? Login(string email, string password) => _repo.Login(email, password);
    public void Add(Customer c) => _repo.Add(c);
    public void Update(Customer c) => _repo.Update(c);
    public void Delete(int id) => _repo.Delete(id);
    public List<Customer> Search(string q) => _repo.Search(q);
}
