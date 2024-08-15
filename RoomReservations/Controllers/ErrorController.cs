using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RoomReservations.Controllers;
using RoomReservationsVM.Models;
using System.Diagnostics;

namespace RoomReservationsUI.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<RoomViewController> _logger;

        public ErrorController(ILogger<RoomViewController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult GenericError()
        {
            // Get exception details
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature != null)
            {
                // Log error
                var exception = exceptionHandlerPathFeature.Error;
                var path = exceptionHandlerPathFeature.Path;
                _logger.LogError(exception, "An unhandled exception occurred while processing the request at path {Path}", path);
                _logger.LogError("Exception Message: {Message}", exception.Message);
                _logger.LogError("Stack Trace: {StackTrace}", exception.StackTrace);
            }

            var vm = new ErrorVm { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View("~/Views/Shared/Error/GenericError.cshtml", vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StautsCodeError(int? statusCode = null)
        {
            if (statusCode.GetValueOrDefault() == 404)
            {
                // Log if needed
                return View("~/Views/Shared/Error/PageNotFoundError.cshtml");
            }
            // Could handle other common status codes
            else
            {
                // Log if needed
                var vm = new ErrorVm { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
                return View("~/Views/Shared/Error/GenericError.cshtml", vm);
            }
        }
    }
}
