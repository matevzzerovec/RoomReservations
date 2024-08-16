using Microsoft.AspNetCore.Mvc.ModelBinding;
using RoomReservationsBLL.Validators.Modules;
using RoomReservationsDAL.Reservations.Repositories;
using RoomReservationsVM.ViewModels.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Validators.Booking
{
    public class BookingValidator : IBookingValidator
    {
        private readonly IReservationRepository _reservationRepository;

        public BookingValidator(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public bool IsValid(BookingVm bookingVm, ModelStateDictionary modelState)
        {
            if (!EmailValidatorModule.IsValid(bookingVm.Email))
            {
                modelState.AddModelError(nameof(bookingVm.Email), "Prosimo vnesite veljaven e-mail naslov");
            }

            if (bookingVm.ArrivalDate.GetValueOrDefault().Date < DateTime.Now.Date)
            {
                modelState.AddModelError(nameof(bookingVm.ArrivalDate), "Dan prihoda ne sme biti manjši od danes.");
            }

            if (bookingVm.ArrivalDate.GetValueOrDefault().Date >= bookingVm.DepartureDate.GetValueOrDefault().Date)
            {
                modelState.AddModelError(nameof(bookingVm.DepartureDate), "Dan odhoda mora biti večji od dneva prihoda");
            }

            if (!_reservationRepository.IsRoomAvalible(
                bookingVm.SelectedRoomId.GetValueOrDefault(), 
                bookingVm.ArrivalDate.GetValueOrDefault(), 
                bookingVm.DepartureDate.GetValueOrDefault()))
            {
                modelState.AddModelError(nameof(bookingVm.DepartureDate), "Žal je soba v tem terminu že zasedena");
            }

            return modelState.IsValid;
        }
    }
}
