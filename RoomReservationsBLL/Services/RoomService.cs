using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoomReservationsBLL.Mappers;
using RoomReservationsBLL.Modules;
using RoomReservationsDAL.Reservations.Models;
using RoomReservationsDAL.Reservations.Repositories;
using RoomReservationsVM.ViewModels.RoomView;
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

        public RoomVm GetRoomById(int roomId)
        {
            var roomDb = _roomRepository.GetRoomWithPictures(roomId);

            var roomVm = RoomMapper.MapToVm(roomDb);

            return roomVm;
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

        public void CreateRoom(RoomVm roomVm)
        {
            var newRoomDb = RoomMapper.MapToDb(roomVm);

            newRoomDb.LastTimestamp = DateTime.Now;

            // Začasno dokler nimam userjev
            newRoomDb.LastUser = "matevz";

            _roomRepository.Add(newRoomDb);
        }

        private RoomVm AssembleNewRoom(RoomVm oldRoom, int nextId)
        {
            var roomDb = _roomRepository.GetRoomWithPictures(nextId);

            if (roomDb == null) return new RoomVm();

            var roomVm = RoomMapper.MapToVm(roomDb);

            roomVm.RoomIdList = oldRoom.RoomIdList;

            return roomVm;
        }

        public void UpdateRoom(RoomVm roomVm, IFormFile[] newPictures)
        {
            throw new NotImplementedException(); // TODO
        }
    }
}
