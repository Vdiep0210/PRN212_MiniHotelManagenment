using BusinessObjects.Models;

namespace Repositories;

public interface IBookingRepository
{
    List<BookingReservation> GetAll();
    void Add(BookingReservation r);
}
