using RoomReservationsDAL.Reservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsDAL.Reservations.Repositories
{
    public interface IRoomRepository
    {
        List<int> GetRoomIdList();

        IQueryable<Room> GetAll();

        Room GetFirstRoomWithPictures();

        decimal GetRoomPrice(int roomId);

        Room GetRoom(int roomId);

        Room GetRoomWithPictures(int roomId);

        Room GetRoomWithPicturesReservations(int roomId);

        void Add(Room roomDb);

        void Update(Room roomDb);

        void Delete(Room roomDb);
    }
}
