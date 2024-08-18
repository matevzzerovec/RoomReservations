using RoomReservationsDAL.Reservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsDAL.Reservations.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationsDbContext _context;

        public ReservationRepository(ReservationsDbContext context)
        {
            _context = context;
        }

        public bool IsRoomAvalible(int roomId, DateTime arrivalDate, DateTime departureDate)
        {
            // Check for room overlaps
            // https://stackoverflow.com/questions/13513932/algorithm-to-detect-overlapping-periods
            var overlap = _context.Reservation.Any(
                x => x.RoomId == roomId &&
                x.ArrivalDate.Date < departureDate.Date &&
                arrivalDate < x.DepartureDate.Date
            );

            return !overlap;
        }

        public void Add(Reservation reservationDb)
        {
            _context.Add(reservationDb);
            _context.SaveChanges();
        }
    }
}
