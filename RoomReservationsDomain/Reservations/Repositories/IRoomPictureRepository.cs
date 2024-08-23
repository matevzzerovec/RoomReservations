using RoomReservationsDAL.Reservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsDAL.Reservations.Repositories
{
    public interface IRoomPictureRepository
    {
        IEnumerable<RoomPicture> GetList(IEnumerable<int> roomPictureIdList);

        void AddList(IEnumerable<RoomPicture> roomPictureDbList);

        void RemoveList(IEnumerable<RoomPicture> roomPictureDbList);
    }
}
