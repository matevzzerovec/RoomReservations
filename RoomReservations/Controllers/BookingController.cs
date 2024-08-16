using Microsoft.AspNetCore.Mvc;
using RoomReservationsBLL.Services;
using RoomReservationsBLL.Validators.Booking;
using RoomReservationsVM.ViewModels.Booking;
using RoomReservationsVM.ViewModels.RoomView;

namespace RoomReservationsUI.Controllers
{
    public class BookingController : Controller
    {
        private readonly IRegistryService _registryService;
        private readonly IBookingValidator _bookingValidator;

        public BookingController(IRegistryService registryService, IBookingValidator bookingValidator)
        {
            _registryService = registryService;
            _bookingValidator = bookingValidator;
        }

        public IActionResult Index()
        {
            var bookingVm = new BookingVm();

            _registryService.FillRoomSelectList(bookingVm);

            return View("Index", bookingVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookRoom(BookingVm bookingVm)
        {
            // Default/attribute validation and custom validation
            if (!ModelState.IsValid || !_bookingValidator.IsValid(bookingVm, ModelState))
            {
                _registryService.FillRoomSelectList(bookingVm);

                return View("Index", bookingVm);
            }

            bookingVm.ClientFeedback = "Rezervacija uspešna! Na vaš e-mail smo poslali potrditev rezervacije.";

            _registryService.FillRoomSelectList(bookingVm);

            ModelState.Clear();

            return View("Index", bookingVm);
        }
    }
}
