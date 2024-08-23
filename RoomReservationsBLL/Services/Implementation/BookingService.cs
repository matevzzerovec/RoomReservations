using Microsoft.Extensions.Logging;
using RoomReservationsBLL.Services.Interface;
using RoomReservationsDAL.Reservations.Repositories;
using RoomReservationsVM.ViewModels.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Services.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<BookingService> _logger;

        public BookingService(IReservationRepository reservationRepository, IRoomRepository roomRepository, ILogger<BookingService> logger)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _logger = logger;
        }

        public bool BookRoom(BookingVm bookingVm)
        {
            try
            {
                var roomPrice = _roomRepository.GetRoomPrice(bookingVm.SelectedRoomId.GetValueOrDefault());
                var totalPrice = Modules.TotalPriceCalcModule.CalcTotalPrice(bookingVm.ArrivalDate.GetValueOrDefault(), bookingVm.DepartureDate.GetValueOrDefault(), roomPrice);

                var reservationDb = Mappers.ReservationMapper.MapToDb(bookingVm, totalPrice);

                _reservationRepository.Add(reservationDb);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Booking service.");
                return false;
            }

            return true;
        }
    }
}
