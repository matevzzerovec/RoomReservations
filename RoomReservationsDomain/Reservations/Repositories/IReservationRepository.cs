using RoomReservationsDAL.Reservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsDAL.Reservations.Repositories
{
    public interface IReservationRepository
    {
        bool IsRoomAvalible(int roomId, DateTime arrivalDate, DateTime departureDate);
        void Add(Reservation reservationDb);
    }
}
