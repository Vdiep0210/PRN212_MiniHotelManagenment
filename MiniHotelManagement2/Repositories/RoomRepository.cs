using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class RoomRepository : IRoomRepository
{
    public List<RoomInformation> GetAll() => RoomDAO.Instance.GetAll();
    public RoomInformation? GetById(int id) => RoomDAO.Instance.GetById(id);
    public void Add(RoomInformation r) => RoomDAO.Instance.Add(r);
    public void Update(RoomInformation r) => RoomDAO.Instance.Update(r);
    public void Delete(int id) => RoomDAO.Instance.Delete(id);
    public List<RoomInformation> Search(string q) => RoomDAO.Instance.Search(q);
}
