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
using System.Xml.Linq;

namespace RoomReservationsBLL.Services.Implementation
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomPictureRepository _roomPictureRepository;

        public RoomService(IRoomRepository roomRepository, IRoomPictureRepository roomPictureRepository)
        {
            _roomRepository = roomRepository;
            _roomPictureRepository = roomPictureRepository; 
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

            // TODO: Začasno dokler nimam userjev
            newRoomDb.LastUser = "matevz";

            _roomRepository.Add(newRoomDb);
        }

        public void UpdateRoom(RoomVm roomVm)
        {
            // Delete unwanted pictures
            var unwantedPicuresDbList = _roomPictureRepository.GetList(roomVm.PictureList
                .Where(x => x.IsDeleted == true)
                .Select(x => x.RoomPictureId.GetValueOrDefault()));

            _roomPictureRepository.RemoveList(unwantedPicuresDbList);

            // Add new pictures
            var newPicturesDb = RoomPictureMapper.MapToDb(roomVm.RoomId.GetValueOrDefault(), roomVm.NewPictureList);

            _roomPictureRepository.AddList(newPicturesDb);

            // Update Room data
            var roomDb = _roomRepository.GetRoom(roomVm.RoomId.GetValueOrDefault());

            RoomMapper.MapChangesToDb(roomDb, roomVm);

            // TODO: Začasno dokler nimam userjev
            roomDb.LastUser = "matevz";

            _roomRepository.Update(roomDb);
        }

        public void DeleteRoom(int roomId)
        {
            var roomDb = _roomRepository.GetRoomWithPicturesReservations(roomId);

            _roomRepository.Delete(roomDb);
        }
    }
}
