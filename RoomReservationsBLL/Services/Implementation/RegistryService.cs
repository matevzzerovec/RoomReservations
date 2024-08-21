using Microsoft.AspNetCore.Mvc.Rendering;
using RoomReservationsBLL.Services.Interface;
using RoomReservationsDAL.Reservations.Repositories;
using RoomReservationsVM.ViewModels.Booking;
using RoomReservationsVM.ViewModels.RoomView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Services.Implementation
{
    public class RegistryService : IRegistryService
    {
        private readonly IRoomRepository _roomRepository;

        public RegistryService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public BookingVm FillRoomSelectList(BookingVm bookingVm)
        {
            var allRooms = _roomRepository.GetAll();

            // Generate select list and prepend "Izberi"/empty element
            var selectList = allRooms.Select(r => new SelectListItem
            {
                Value = r.RoomId.ToString(),
                Text = r.Name
            }).ToList();

            selectList.Insert(0, new SelectListItem { Value = "", Text = "-- Izberi sobo --", Selected = true });

            bookingVm.RoomSelectList = new SelectList(selectList, "Value", "Text");

            return bookingVm;
        }
    }
}
