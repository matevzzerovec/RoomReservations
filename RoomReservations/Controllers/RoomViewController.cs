using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using RoomReservationsDAL.Reservations;
using RoomReservationsBLL.Mappers;
using RoomReservationsBLL.Services;

namespace RoomReservations.Controllers
{
    public class RoomViewController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomViewController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public IActionResult Index()
        {
            var firstRoomVm = _roomService.GetFirstRoom();
            return View("Index", firstRoomVm);
        }
    }
}
