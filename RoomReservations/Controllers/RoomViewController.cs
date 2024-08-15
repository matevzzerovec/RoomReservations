using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using RoomReservationsDAL.Reservations;
using RoomReservationsBLL.Mappers;
using RoomReservationsBLL.Services;
using RoomReservationsVM.Models.Shared;

namespace RoomReservations.Controllers
{
    public class RoomViewController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomViewController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var firstRoomVm = _roomService.GetFirstRoom();

            return View("Index", firstRoomVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NextRoom(RoomVm roomVm)
        {
            var nextRoomVm = _roomService.GetNextRoom(roomVm.RoomId.GetValueOrDefault(), roomVm.RoomIdList);

            ModelState.Clear();

            return View("Index", nextRoomVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PreviousRoom(RoomVm roomVm)
        {
            var prevRoomVm = _roomService.GetPrevRoom(roomVm.RoomId.GetValueOrDefault(), roomVm.RoomIdList);

            ModelState.Clear();

            return View("Index", prevRoomVm);
        }
    }
}
