﻿using RoomReservationsVM.Models.Shared;
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

        RoomVm GetNextRoom(int roomId, List<int> roomIdList);

        RoomVm GetPrevRoom(int roomId, List<int> roomIdList);
    }
}
