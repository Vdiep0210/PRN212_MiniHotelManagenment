using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class BookingRepository : IBookingRepository
{
    public List<BookingReservation> GetAll() => BookingDAO.Instance.GetAll();
    public void Add(BookingReservation r) => BookingDAO.Instance.Add(r);
}
