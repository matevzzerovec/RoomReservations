using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using reCAPTCHA.AspNetCore;
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
        private readonly IRecaptchaService _recaptcha;
        private readonly AppValues _appValues;

        public BookingController(
            IRegistryService registryService,
            IBookingValidator bookingValidator,
            IBookingService bookingService,
            IMailingService mailingService,
            IRecaptchaService recaptcha,
            IOptions<AppValues> appValues)
        {
            _registryService = registryService;
            _bookingValidator = bookingValidator;
            _bookingService = bookingService;
            _mailingService = mailingService;
            _recaptcha = recaptcha;
            _appValues = appValues.Value;
        }

        public IActionResult Index()
        {
            var bookingVm = new BookingVm()
            {
                ReCaptchaSiteKey = _appValues.ReCaptchaSiteKey
            };

            return ReturnFilledVm(bookingVm);
        }

        [HttpPost]
        public async Task<IActionResult> BookRoom(BookingVm bookingVm)
        {
            bookingVm.ClientFeedback = "";

            // ReCaptcha validation
            var recaptchaResult = await _recaptcha.Validate(Request);
            if (!recaptchaResult.success)
            {
                bookingVm.FeedbackWarning = true;
                bookingVm.ClientFeedback = "Prosimo izpolnite reCaptcha test.";

                return ReturnFilledVm(bookingVm);
            }

            // Default/attribute validation and custom validation
            if (!ModelState.IsValid || !_bookingValidator.IsValid(bookingVm, ModelState))
            {
                return ReturnFilledVm(bookingVm);
            }

            // Room booking (DB insert)
            if (!_bookingService.BookRoom(bookingVm))
            {
                bookingVm.FeedbackDanger = true;
                bookingVm.ClientFeedback = "Žal je prišlo do napake pri poskusu rezervacije.";

                return ReturnFilledVm(bookingVm);
            }

            // Client mailing
            if (!_mailingService.SendMailToClient(bookingVm))
            {
                bookingVm.FeedbackWarning = true;
                bookingVm.ClientFeedback = "Rezervacija je uspešna, a je prišlo do napake pri pošiljanju e-maila.";

                return ReturnFilledVm(bookingVm);
            }

            // Hotel mailing
            _mailingService.SendMailToHotel(bookingVm);

            bookingVm.ClientFeedback = "Rezervacija uspešna! Na e-mail smo vam poslali podrobnosti rezervacije.";

            ModelState.Clear();

            return ReturnFilledVm(bookingVm);
        }

        private IActionResult ReturnFilledVm(BookingVm bookingVm)
        {
            bookingVm = _registryService.FillRoomSelectList(bookingVm);

            return View("Index", bookingVm);
        }
    }
}
