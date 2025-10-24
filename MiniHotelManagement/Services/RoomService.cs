using BusinessObjects.Models;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class RoomService
    {
        private readonly IRoomRepository repo = new RoomRepository();

        public List<RoomInformation> GetRooms() => repo.GetRooms();
        public RoomInformation? GetRoomById(int id) => repo.GetRoomById(id);
        public void AddRoom(RoomInformation r) => repo.AddRoom(r);
        public void UpdateRoom(RoomInformation r) => repo.UpdateRoom(r);
        public void DeleteRoom(int id) => repo.DeleteRoom(id);
        public List<RoomInformation> SearchByNumberOrType(string q) => repo.SearchByNumberOrType(q);
    }
}
