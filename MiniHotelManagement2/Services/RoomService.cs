using BusinessObjects.Models;
using Repositories;

namespace Services;

public class RoomService
{
    private readonly IRoomRepository _repo = new RoomRepository();
    public List<RoomInformation> GetAll() => _repo.GetAll();
    public RoomInformation? GetById(int id) => _repo.GetById(id);
    public void Add(RoomInformation r) => _repo.Add(r);
    public void Update(RoomInformation r) => _repo.Update(r);
    public void Delete(int id) => _repo.Delete(id);
    public List<RoomInformation> Search(string q) => _repo.Search(q);
}
