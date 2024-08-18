using Microsoft.Extensions.Logging;
using RoomReservationsVM.ViewModels.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RoomReservationsVM.Configuration;
using Microsoft.Extensions.Options;
using RoomReservationsDAL.Reservations.Repositories;

namespace RoomReservationsBLL.Services
{
    public class MailingService : IMailingService
    {
        private readonly ILogger<MailingService> _logger;
        private readonly IRoomRepository _roomRepository;

        private readonly AppValues _appValues;
        private readonly MailCredentials _mailCredentials;

        public MailingService(
            ILogger<MailingService> logger, IRoomRepository roomRepository,
            IOptions<AppValues> appValues, IOptions<MailCredentials> mailCredentials)
        {
            _logger = logger;
            _roomRepository = roomRepository;

            _appValues = appValues.Value;
            _mailCredentials = mailCredentials.Value;
        }

        public bool SendMailToClient(BookingVm bookingVm)
        {
            try
            {
                var room = _roomRepository.GetRoom(bookingVm.SelectedRoomId.GetValueOrDefault());
                var totalPrice = Modules.TotalPriceCalcModule.CalcTotalPrice(bookingVm.ArrivalDate.GetValueOrDefault(), bookingVm.DepartureDate.GetValueOrDefault(), room.Price);

                var subject = "Potrditev rezervacije";
                var body =
                    $"<b>Hvala za oddano povpraševanje!</b> <br>" +
                    $"Datum rezervacije: {DateTime.Now.ToLocalTime()} <br>" +
                    $"Izbrana soba: {room.Name} <br>" +
                    $"Skupni znesek: {totalPrice:n2}€";

                SendMail(subject, body, bookingVm.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Mailing service.");
                return false;
            }

            return true;
        }

        public bool SendMailToHotel(BookingVm bookingVm)
        {
            try
            {
                var room = _roomRepository.GetRoom(bookingVm.SelectedRoomId.GetValueOrDefault());
                var totalPrice = Modules.TotalPriceCalcModule.CalcTotalPrice(bookingVm.ArrivalDate.GetValueOrDefault(), bookingVm.DepartureDate.GetValueOrDefault(), room.Price);

                var subject = "Obvestilo o novi rezervaciji";
                var body =
                    $"<b>V sistemu je prišlo do nove rezervacije</b> <br>" +
                    $"Termin: Od {bookingVm.ArrivalDate.GetValueOrDefault():dd.MM.yyyy} do {bookingVm.DepartureDate.GetValueOrDefault():dd.MM.yyyy} <br>" +
                    $"Ime in priimek: {bookingVm.Name} <br>" +
                    $"Email: {bookingVm.Email} <br>" +
                    $"Telefon: {bookingVm.PhoneNumber} <br>" +
                    $"Sporočilo: {bookingVm.CustomerNote} <br>" +
                    $"Izbrana soba: {room.Name} <br>" +
                    $"Skupni znesek: {totalPrice:n2}€";

                SendMail(subject, body, _appValues.HotelEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Mailing service.");
                return false;
            }

            return true;
        }

        private void SendMail(string subject, string body, string to)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_mailCredentials.Username, _mailCredentials.Password),
                EnableSsl = true,
                UseDefaultCredentials = false
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_appValues.HotelEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(to);

            smtpClient.Send(mailMessage);
        }
    }
}
