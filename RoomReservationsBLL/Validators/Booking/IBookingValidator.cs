using Microsoft.AspNetCore.Mvc.ModelBinding;
using RoomReservationsVM.ViewModels.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Validators.Booking
{
    public interface IBookingValidator
    {
        bool IsValid(BookingVm bookingVm, ModelStateDictionary modelState);
    }
}
