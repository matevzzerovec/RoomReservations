using Microsoft.AspNetCore.Mvc.Rendering;
using RoomReservationsBLL.Mappers;
using RoomReservationsBLL.Modules;
using RoomReservationsDAL.Reservations.Models;
using RoomReservationsDAL.Reservations.Repositories;
using RoomReservationsVM.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public RoomVm GetFirstRoom()
        {
            var firstRoomDb = _roomRepository.GetFirstRoomWithPictures();
            var roomIdList = _roomRepository.GetRoomIdList();

            if (firstRoomDb == null) return new RoomVm();

            var firstRoomVm = RoomMapper.MapToVm(firstRoomDb);
            firstRoomVm.RoomIdList = roomIdList;

            return firstRoomVm;
        }

        public RoomVm GetNextRoom(RoomVm roomVm)
        {
            var nextId = IdLooperModule.GetNextId(roomVm.RoomId.GetValueOrDefault(), roomVm.RoomIdList);

            return AssembleNewRoom(roomVm, nextId);
        }

        public RoomVm GetPrevRoom(RoomVm roomVm)
        {
            var prevId = IdLooperModule.GetPrevId(roomVm.RoomId.GetValueOrDefault(), roomVm.RoomIdList);

            return AssembleNewRoom(roomVm, prevId);
        }

        private RoomVm AssembleNewRoom(RoomVm oldRoom, int nextId)
        {
            var roomDb = _roomRepository.GetRoomWithPictures(nextId);

            if (roomDb == null) return new RoomVm();

            var roomVm = RoomMapper.MapToVm(roomDb);

            // Pass the values to the new view
            roomVm.RoomIdList = oldRoom.RoomIdList;
            roomVm.BookingVm = oldRoom.BookingVm;

            return roomVm;
        }
    }
}
