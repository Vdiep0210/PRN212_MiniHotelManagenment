using BusinessObjects.Models;

namespace Repositories;

public interface ICustomerRepository
{
    List<Customer> GetAll();
    Customer? GetById(int id);
    Customer? Login(string email, string password);
    void Add(Customer c);
    void Update(Customer c);
    void Delete(int id);
    List<Customer> Search(string q);
}
