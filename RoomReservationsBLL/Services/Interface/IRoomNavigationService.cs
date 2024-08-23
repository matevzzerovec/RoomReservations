using RoomReservationsVM.ViewModels.RoomView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Services.Interface
{
    public interface IRoomNavigationService
    {
        RoomVm GetFirstRoom();

        RoomVm GetNextRoom(RoomVm roomVm);

        RoomVm GetPrevRoom(RoomVm roomVm);
    }
}
