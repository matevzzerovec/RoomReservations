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
        private readonly IRegistryService _registryService;

        public RoomViewController(
            IRoomService roomService, 
            IRegistryService registryService)
        {
            _roomService = roomService;
            _registryService = registryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var firstRoomVm = _roomService.GetFirstRoom();
            
            _registryService.FillRoomSelectList(firstRoomVm);

            return View("Index", firstRoomVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NextRoom(RoomVm roomVm)
        {
            var nextRoomVm = _roomService.GetNextRoom(roomVm);

            _registryService.FillRoomSelectList(nextRoomVm);

            ModelState.Clear();

            return View("Index", nextRoomVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PreviousRoom(RoomVm roomVm)
        {
            var prevRoomVm = _roomService.GetPrevRoom(roomVm);

            _registryService.FillRoomSelectList(prevRoomVm);

            ModelState.Clear();

            return View("Index", prevRoomVm);
        }
    }
}
