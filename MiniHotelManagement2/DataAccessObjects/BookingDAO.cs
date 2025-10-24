using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects;

public sealed class BookingDAO
{
    private static readonly BookingDAO _instance = new();
    public static BookingDAO Instance => _instance;
    private BookingDAO() { }

    public List<BookingReservation> GetAll()
    {
        using var ctx = new FUMiniHotelContext();
        return ctx.BookingReservations.Include(b => b.Customer).Include(b => b.BookingDetails).ToList();
    }

    public void Add(BookingReservation r)
    {
        using var ctx = new FUMiniHotelContext();
        ctx.BookingReservations.Add(r);
        ctx.SaveChanges();
    }
}
