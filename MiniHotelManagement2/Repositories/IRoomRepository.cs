using BusinessObjects.Models;

namespace Repositories;

public interface IRoomRepository
{
    List<RoomInformation> GetAll();
    RoomInformation? GetById(int id);
    void Add(RoomInformation r);
    void Update(RoomInformation r);
    void Delete(int id);
    List<RoomInformation> Search(string q);
}
