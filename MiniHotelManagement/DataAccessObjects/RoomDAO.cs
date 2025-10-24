using BusinessObjects.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public sealed class RoomDAO
    {
        private static readonly RoomDAO _instance = new RoomDAO();
        public static RoomDAO Instance => _instance;
        private RoomDAO() { }

        public List<RoomInformation> GetRooms()
        {
            using var ctx = new FUMiniHotelContext();
            return ctx.Set<RoomInformation>()
                      .Include(r => r.RoomType)
                      .AsNoTracking()
                      .ToList();
        }

        public RoomInformation? GetRoomById(int id)
        {
            using var ctx = new FUMiniHotelContext();
            return ctx.Set<RoomInformation>().Find(id);
        }

        public void AddRoom(RoomInformation r)
        {
            using var ctx = new FUMiniHotelContext();
            ctx.Set<RoomInformation>().Add(r);
            ctx.SaveChanges();
        }

        public void UpdateRoom(RoomInformation r)
        {
            using var ctx = new FUMiniHotelContext();
            var e = ctx.Set<RoomInformation>().Find(r.RoomId);
            if (e != null)
            {
                ctx.Entry(e).CurrentValues.SetValues(r);
                ctx.SaveChanges();
            }
        }

        public void DeleteRoom(int id)
        {
            using var ctx = new FUMiniHotelContext();
            var e = ctx.Set<RoomInformation>().Find(id);
            if (e != null)
            {
                ctx.Set<RoomInformation>().Remove(e);
                ctx.SaveChanges();
            }
        }

        public List<RoomInformation> SearchByNumberOrType(string q)
        {
            using var ctx = new FUMiniHotelContext();
            return ctx.Set<RoomInformation>()
                      .Include(r => r.RoomType)
                      .Where(r => (r.RoomNumber != null && r.RoomNumber.Contains(q))
                               || (r.RoomType != null && r.RoomType.RoomTypeName != null && r.RoomType.RoomTypeName.Contains(q)))
                      .AsNoTracking()
                      .ToList();
        }
    }
}
