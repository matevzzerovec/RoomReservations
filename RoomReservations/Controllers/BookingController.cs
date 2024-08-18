using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RoomReservationsBLL.Services;
using RoomReservationsBLL.Validators.Booking;
using RoomReservationsVM.Configuration;
using RoomReservationsVM.ViewModels.Booking;
using RoomReservationsVM.ViewModels.RoomView;

namespace RoomReservationsUI.Controllers
{
    public class BookingController : Controller
    {
        private readonly IRegistryService _registryService;
        private readonly IBookingValidator _bookingValidator;
        private readonly IBookingService _bookingService;
        private readonly IMailingService _mailingService;

        public BookingController(
            IRegistryService registryService, 
            IBookingValidator bookingValidator, 
            IBookingService bookingService, 
            IMailingService mailingService)
        {
            _registryService = registryService;
            _bookingValidator = bookingValidator;
            _bookingService = bookingService;
            _mailingService = mailingService;
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

            if (!_mailingService.SendMailToClient(bookingVm))
            {
                bookingVm.IsMailingError = true;
                bookingVm.ClientFeedback = "Rezervacija je uspešna, a je prišlo do napake pri pošiljanju e-maila.";

                return View("Index", bookingVm);
            }

            _mailingService.SendMailToHotel(bookingVm);

            bookingVm.ClientFeedback = "Rezervacija uspešna! Na e-mail smo vam poslali podrobnosti rezervacije.";

            bookingVm = _registryService.FillRoomSelectList(bookingVm);

            ModelState.Clear();

            return View("Index", bookingVm);
        }
    }
}
