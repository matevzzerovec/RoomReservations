using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoomReservationsBLL.Mappers;
using RoomReservationsBLL.Modules;
using RoomReservationsBLL.Services.Interface;
using RoomReservationsDAL.Reservations.Models;
using RoomReservationsDAL.Reservations.Repositories;
using RoomReservationsVM.ViewModels.RoomView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Services.Implementation
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public RoomVm GetRoomById(int roomId)
        {
            var roomDb = _roomRepository.GetRoomWithPictures(roomId);

            var roomVm = RoomMapper.MapToVm(roomDb);

            return roomVm;
        }

        public void CreateRoom(RoomVm roomVm)
        {
            var newRoomDb = RoomMapper.MapToDb(roomVm);

            newRoomDb.LastTimestamp = DateTime.Now;

            // Začasno dokler nimam userjev
            newRoomDb.LastUser = "matevz";

            _roomRepository.Add(newRoomDb);
        }

        public void UpdateRoom(RoomVm roomVm)
        {
            throw new NotImplementedException(); // TODO
        }

        public void DeleteRoom(int roomId)
        {
            _roomRepository.Delete(roomId);
        }
    }
}
