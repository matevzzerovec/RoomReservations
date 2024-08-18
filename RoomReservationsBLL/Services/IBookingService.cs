﻿using RoomReservationsBLL.Dto;
using RoomReservationsVM.ViewModels.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Services
{
    public interface IBookingService
    {
        public bool BookRoom(BookingVm bookingVm);
    }
}
