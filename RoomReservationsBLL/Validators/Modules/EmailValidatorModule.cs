using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Validators.Modules
{
    internal static class EmailValidatorModule
    {
        internal static bool IsValid(string email)
        {
            bool isValid = false;

            try
            {
                MailAddress address = new MailAddress(email);

                isValid = (address.Address == email);
            }
            catch (FormatException)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
