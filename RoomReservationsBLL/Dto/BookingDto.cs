using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Dto
{
    public class BookingDto
    {
        public bool BookingSucesseful { get; set; }
        public bool MailingSucesseful { get; set; }

        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
