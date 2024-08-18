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

namespace RoomReservationsBLL.Services
{
    public class MailingService : IMailingService
    {
        private readonly ILogger<MailingService> _logger;

        private readonly AppValues _appValues;
        private readonly MailCredentials _mailCredentials;

        public MailingService(ILogger<MailingService> logger, IOptions<AppValues> appValues, IOptions<MailCredentials> mailCredentials)
        {
            _logger = logger;
            _appValues = appValues.Value;
            _mailCredentials = mailCredentials.Value;
        }

        public bool SendMailToClient(BookingVm bookingVm)
        {
            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(_appValues.HotelEmail);
            mailMessage.To.Add(new MailAddress(bookingVm.Email));

            var pass = _mailCredentials.Password;
            var user = _mailCredentials.Username;

            //mailMessage.Subject = "Potrditev rezervacije";
            //mailMessage.Body = "This is a test email sent using C#.Net";

            //SmtpClient smtpClient = new SmtpClient(smtp.maileroo.com);
            //smtpClient.Host = "smtp.maileroo.com";
            //smtpClient.Port = 587;
            //smtpClient.UseDefaultCredentials = false;
            //smtpClient.Credentials = new NetworkCredential("SenderEmail," "SenderPassword");
            //smtpClient.EnableSsl = true;

            try
			{

			}
			catch (Exception ex)
			{
                _logger.LogError(ex, "Error in Mailing service.");
                return false;
			}

            return true;
        }
    }
}
