using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsVM.ViewModels.Booking
{
    public class BookingVm : CommonVm
    {
        [ValidateNever]
        public string ReCaptchaSiteKey { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.mm.yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Polje je obvezno")]
        public DateTime? ArrivalDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.mm.yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Polje je obvezno")]
        public DateTime? DepartureDate { get; set; }

        [ValidateNever]
        public SelectList RoomSelectList { get; set; }

        [Required(ErrorMessage = "Polje je obvezno")]
        public int? SelectedRoomId { get; set; }

        [Required(ErrorMessage = "Polje je obvezno")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Prosimo vnesite veljaven e-mail naslov"), EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"^(\+|00)[1-9][0-9 \-\(\)\.]{7,32}$", ErrorMessage = "Prosimo vnesite veljavno telefonsko številko vključno s kodo države.")]
        [Phone]
        [Required(ErrorMessage = "Polje je obvezno")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Polje je obvezno")]
        public string CustomerNote { get; set; }
    }
}
