using Microsoft.AspNetCore.Http;
using RoomReservationsVM.ViewModels.RoomView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Services.Interface
{
    public interface IRoomService
    {
        RoomVm GetRoomById(int roomId);

        void CreateRoom(RoomVm roomVm);

        void UpdateRoom(RoomVm roomVm, IFormFile[] newPictures);
    }
}
