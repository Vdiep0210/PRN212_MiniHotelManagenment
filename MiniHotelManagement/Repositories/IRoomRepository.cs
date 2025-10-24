using BusinessObjects.Models;
using System.Collections.Generic;

namespace Repositories
{
    public interface IRoomRepository
    {
        List<RoomInformation> GetRooms();
        RoomInformation? GetRoomById(int id);
        void AddRoom(RoomInformation room);
        void UpdateRoom(RoomInformation room);
        void DeleteRoom(int id);
        List<RoomInformation> SearchByNumberOrType(string query);
    }
}
