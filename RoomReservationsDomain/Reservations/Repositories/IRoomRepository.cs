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
        IQueryable<Room> GetAll();

        Room GetFirstRoomWithPictures();

        Room GetRoomWithPictures(int roomId);

        List<int> GetRoomIdList();
    }
}
