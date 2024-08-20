using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsVM.ViewModels
{
    public class CommonVm
    {
        [ValidateNever]
        public string ClientFeedback { get; set; }
    }
}
