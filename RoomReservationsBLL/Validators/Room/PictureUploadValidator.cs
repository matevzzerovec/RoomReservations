using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RoomReservationsVM.ViewModels.Booking;
using RoomReservationsVM.ViewModels.RoomView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Validators.Room
{
    public class PictureUploadValidator : IPictureUploadValidator
    {
        public bool IsValid(RoomVm roomVm, ModelStateDictionary modelState)
        {
            if (roomVm.NewPictureList != null)
            {
                foreach (var picture in roomVm.NewPictureList)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var extension = Path.GetExtension(picture.FileName).ToLower();

                    if (!allowedExtensions.Contains(extension))
                    {
                        modelState.AddModelError(nameof(roomVm.NewPictureList), $"Slike so lahko samo fomrata: {string.Join(" ", allowedExtensions)}");
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
