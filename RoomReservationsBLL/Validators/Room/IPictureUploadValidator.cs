using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RoomReservationsVM.ViewModels.RoomView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Validators.Room
{
    public interface IPictureUploadValidator
    {
        bool IsValid(RoomVm roomVm, ModelStateDictionary modelState);
    }
}
