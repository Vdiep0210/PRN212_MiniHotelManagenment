using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessObjects;
public sealed class RoomDAO
{
    private static readonly RoomDAO _instance = new();
    public static RoomDAO Instance => _instance;
    private RoomDAO() { }

    public List<RoomInformation> GetAll()
    {
        using var ctx = new FUMiniHotelContext();
        return ctx.RoomInformations.Include(r => r.RoomType).ToList();
    }

    public RoomInformation? GetById(int id)
    {
        using var ctx = new FUMiniHotelContext();
        return ctx.RoomInformations.Include(r => r.RoomType).FirstOrDefault(r => r.RoomId == id);
    }

    public void Add(RoomInformation r)
    {
        using var ctx = new FUMiniHotelContext();
        ctx.RoomInformations.Add(r);
        ctx.SaveChanges();
    }

    public void Update(RoomInformation r)
    {
        using var ctx = new FUMiniHotelContext();
        var e = ctx.RoomInformations.Find(r.RoomId);
        if (e != null) { ctx.Entry(e).CurrentValues.SetValues(r); ctx.SaveChanges(); }
    }

    public void Delete(int id)
    {
        using var ctx = new FUMiniHotelContext();
        var e = ctx.RoomInformations.Find(id);
        if (e != null) { ctx.RoomInformations.Remove(e); ctx.SaveChanges(); }
    }

    public List<RoomInformation> Search(string q)
    {
        using var ctx = new FUMiniHotelContext();
        return ctx.RoomInformations.Include(r => r.RoomType)
                   .Where(r => (r.RoomNumber != null && r.RoomNumber.Contains(q))
                            || (r.RoomType != null && r.RoomType.RoomTypeName.Contains(q))).ToList();
    }
}