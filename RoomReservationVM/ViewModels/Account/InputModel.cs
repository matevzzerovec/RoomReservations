using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsVM.ViewModels.Account
{
    public class InputModel
    {
        [Required(ErrorMessage = "Polje je obvezno")]
        [EmailAddress(ErrorMessage = "Neveljavna e-pošta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Polje je obvezno")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
