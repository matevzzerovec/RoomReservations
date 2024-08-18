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
        private readonly IBookingService _bookingService;

        public BookingController(IRegistryService registryService, IBookingValidator bookingValidator, IBookingService bookingService)
        {
            _registryService = registryService;
            _bookingValidator = bookingValidator;
            _bookingService = bookingService;
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

            if (!_bookingService.BookRoom(bookingVm))
            {
                bookingVm.IsBookingError = true;
                bookingVm.ClientFeedback = "Žal je prišlo do napake pri poskusu rezervacije.";

                return View("Index", bookingVm);
            }

            //// TODO mailing
            //if (true)
            //{

            //}

            bookingVm.ClientFeedback = "Rezervacija uspešna! Na e-mail smo vam poslali podrobnosti rezervacije.";

            bookingVm = _registryService.FillRoomSelectList(bookingVm);

            ModelState.Clear();

            return View("Index", bookingVm);
        }
    }
}
