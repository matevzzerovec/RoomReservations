using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using RoomReservationsDAL.Reservations;
using RoomReservationsUI.Mappers;
using RoomReservationsUI.Models;
using RoomReservationsUI.Models.Shared;

namespace RoomReservations.Controllers
{
    public class RoomViewController : Controller
    {
        private readonly ILogger<RoomViewController> _logger;
        private readonly ReservationsDbContext _reservationsDb;

        public RoomViewController(ILogger<RoomViewController> logger, ReservationsDbContext reservationsDb)
        {
            _logger = logger;
            _reservationsDb = reservationsDb;
        }

        public IActionResult Index()
        {
            var firstRoomDb = _reservationsDb.Room.OrderByDescending(x => x.RoomId).Include(x => x.RoomPictures).FirstOrDefault();
            var firstRoomVm = RoomMapper.MapToVm(firstRoomDb);

            return View("Index", firstRoomVm);
        }
    }
}
