using RoomReservationsBLL.Mappers;
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

            if (firstRoomDb == null)
            {
                return new RoomVm();
            }

            return RoomMapper.MapToVm(firstRoomDb);
        }
    }
}
