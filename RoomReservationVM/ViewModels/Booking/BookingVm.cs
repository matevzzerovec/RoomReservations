using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsVM.ViewModels.Booking
{
    public class BookingVm
    {
        [DisplayFormat(DataFormatString = "{0:dd.mm.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ArrivalDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.mm.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DepartureDate { get; set; }

        public SelectList RoomSelectList { get; set; }
        public int? SelectedRoomId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerNote { get; set; }
    }
}
