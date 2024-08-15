using Microsoft.AspNetCore.Mvc.Rendering;
using RoomReservationsBLL.Mappers;
using RoomReservationsBLL.Modules;
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

        public RoomVm GetNextRoom(int currentRoomId, List<int> roomIdList)
        {
            var nextId = IdLooperModule.GetNextId(currentRoomId, roomIdList);

            return AssembleNewRoom(roomIdList, nextId);
        }

        public RoomVm GetPrevRoom(int currentRoomId, List<int> roomIdList)
        {
            var nextId = IdLooperModule.GetPrevId(currentRoomId, roomIdList);

            return AssembleNewRoom(roomIdList, nextId);
        }

        private RoomVm AssembleNewRoom(List<int> roomIdList, int nextId)
        {
            var roomDb = _roomRepository.GetRoomWithPictures(nextId);

            if (roomDb == null) return new RoomVm();

            var roomVm = RoomMapper.MapToVm(roomDb);
            roomVm.RoomIdList = roomIdList;

            return roomVm;
        }
    }
}
