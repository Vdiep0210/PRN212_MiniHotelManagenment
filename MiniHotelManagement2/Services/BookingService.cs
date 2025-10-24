using BusinessObjects.Models;
using Repositories;

namespace Services;

public class BookingService
{
    private readonly IBookingRepository _repo = new BookingRepository();
    public List<BookingReservation> GetAll() => _repo.GetAll();
    public void Add(BookingReservation r) => _repo.Add(r);
}
