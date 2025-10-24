using BusinessObjects.Models;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class RoomRepository : IRoomRepository
    {
        public List<RoomInformation> GetRooms()
            => RoomDAO.Instance.GetRooms();

        public RoomInformation? GetRoomById(int id)
            => RoomDAO.Instance.GetRoomById(id);

        public void AddRoom(RoomInformation room)
            => RoomDAO.Instance.AddRoom(room);

        public void UpdateRoom(RoomInformation room)
            => RoomDAO.Instance.UpdateRoom(room);

        public void DeleteRoom(int id)
            => RoomDAO.Instance.DeleteRoom(id);

        public List<RoomInformation> SearchByNumberOrType(string query)
            => RoomDAO.Instance.SearchByNumberOrType(query);
    }
}
