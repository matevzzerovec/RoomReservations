using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using RoomReservationsDAL.Reservations;
using RoomReservationsBLL.Mappers;
using RoomReservationsVM.ViewModels.RoomView;
using RoomReservationsBLL.Services.Interface;
using System.Linq;
using RoomReservationsBLL.Validators.Room;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RoomReservations.Controllers
{
    public class RoomViewController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IRoomNavigationService _roomNavigationService;
        private readonly IPictureUploadValidator _pictureUploadValidator;

        public RoomViewController(IRoomService roomService, IRoomNavigationService roomNavigationService, IPictureUploadValidator pictureUploadValidator)
        {
            _roomService = roomService;
            _roomNavigationService = roomNavigationService;
            _pictureUploadValidator = pictureUploadValidator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var firstRoomVm = _roomNavigationService.GetFirstRoom();

            return View("Index", firstRoomVm);
        }

        [HttpPost]
        public IActionResult NextRoom(RoomVm roomVm)
        {
            var nextRoomVm = _roomNavigationService.GetNextRoom(roomVm);

            ModelState.Clear();

            return View("Index", nextRoomVm);
        }

        [HttpPost]
        public IActionResult PreviousRoom(RoomVm roomVm)
        {
            var prevRoomVm = _roomNavigationService.GetPrevRoom(roomVm);

            ModelState.Clear();

            return View("Index", prevRoomVm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddNew()
        {
            return View("CreateRoom", new RoomVm());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(RoomVm roomVm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateRoom", roomVm);
            }

            _roomService.CreateRoom(roomVm, User.FindFirstValue(ClaimTypes.Email));

            roomVm.ClientFeedback = "Soba je uspešno kreirana, za dodana slik pojdite na urejanje sobe.";

            return View("CreateRoom", roomVm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var roomVm = _roomService.GetRoomById(id);

            return View("EditRoom", roomVm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Save(RoomVm roomVm)
        {
            if (!ModelState.IsValid || !_pictureUploadValidator.IsValid(roomVm, ModelState))
            {
                return View("EditRoom", roomVm);
            }

            _roomService.UpdateRoom(roomVm, User.FindFirstValue(ClaimTypes.Email));

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _roomService.DeleteRoom(id);

            return RedirectToAction("Index");
        }
    }
}
