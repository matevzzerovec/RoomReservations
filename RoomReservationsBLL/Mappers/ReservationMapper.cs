using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRVM = RoomReservationsVM.ViewModels.Booking;
using RRDAL = RoomReservationsDAL.Reservations.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace RoomReservationsBLL.Mappers
{
    internal static class ReservationMapper
    {
        public static RRDAL.Reservation MapToDb(RRVM.BookingVm bookingVm, decimal totalPrice)
        {
            var reservationDb = new RRDAL.Reservation();

            reservationDb.RoomId = bookingVm.SelectedRoomId.GetValueOrDefault();
            reservationDb.ArrivalDate = bookingVm.ArrivalDate.GetValueOrDefault().Date;
            reservationDb.DepartureDate = bookingVm.DepartureDate.GetValueOrDefault().Date;
            reservationDb.TotalPrice = totalPrice;
            reservationDb.Name = bookingVm.Name;
            reservationDb.Email = bookingVm.Email;
            reservationDb.PhoneNumber = bookingVm.PhoneNumber;
            reservationDb.Note = bookingVm.CustomerNote;
            reservationDb.Timestamp = DateTime.Now;

            return reservationDb;
        }
    }
}
