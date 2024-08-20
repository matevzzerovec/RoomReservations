using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using RoomReservationsDAL.Reservations;
using RoomReservationsBLL.Mappers;
using RoomReservationsBLL.Services;
using RoomReservationsVM.ViewModels.RoomView;

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
        public IActionResult NextRoom(RoomVm roomVm)
        {
            var nextRoomVm = _roomService.GetNextRoom(roomVm);

            ModelState.Clear();

            return View("Index", nextRoomVm);
        }

        [HttpPost]
        public IActionResult PreviousRoom(RoomVm roomVm)
        {
            var prevRoomVm = _roomService.GetPrevRoom(roomVm);

            ModelState.Clear();

            return View("Index", prevRoomVm);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            return View("CreateRoom", new RoomVm());
        }

        [HttpPost]
        public IActionResult Create(RoomVm roomVm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateRoom", roomVm);
            }

            _roomService.CreateRoom(roomVm);

            roomVm.ClientFeedback = "Soba je uspešno kreirana, za dodana slik pojdite na urejanje sobe.";

            return View("CreateRoom", roomVm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var roomVm = _roomService.GetRoomById(id);

            return View("EditRoom", roomVm);
        }

        [HttpPost]
        public IActionResult Save(RoomVm roomVm, IFormFile[] newPictures)
        {
            if (!ModelState.IsValid)
            {
                return View("EditRoom", roomVm);
            }

            _roomService.UpdateRoom(roomVm, newPictures);

            roomVm.ClientFeedback = "Soba je uspešno posodobljena!";

            return View("EditRoom", roomVm);
        }

        //[HttpPost]
        //public IActionResult Delete(int id)
        //{
        //    _roomService.DeleteRoom(id);
        //    return RedirectToAction("Index");
        //}
    }
}
