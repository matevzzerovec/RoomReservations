﻿using Microsoft.AspNetCore.Http;
using RoomReservationsVM.ViewModels.RoomView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Services
{
    public interface IRoomService
    {
        RoomVm GetFirstRoom();

        RoomVm GetRoomById(int roomId);

        RoomVm GetNextRoom(RoomVm roomVm);

        RoomVm GetPrevRoom(RoomVm roomVm);

        void CreateRoom(RoomVm roomVm);

        void UpdateRoom(RoomVm roomVm, IFormFile[] newPictures);
    }
}
