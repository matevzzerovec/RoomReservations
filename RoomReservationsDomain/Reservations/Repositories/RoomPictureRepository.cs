using RoomReservationsDAL.Reservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsDAL.Reservations.Repositories
{
    public class RoomPictureRepository : IRoomPictureRepository
    {
        private readonly ReservationsDbContext _context;

        public RoomPictureRepository(ReservationsDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RoomPicture> GetList(IEnumerable<int> roomPictureIdList)
        {
            if (roomPictureIdList == null || roomPictureIdList.Count() == 0) return new List<RoomPicture>();

            return _context.RoomPicture.Where(x => roomPictureIdList.Contains(x.RoomPictureId));
        }

        public void AddList(IEnumerable<RoomPicture> roomPictureDbList)
        {
            if (roomPictureDbList == null || roomPictureDbList.Count() == 0) return;

            // Set navigation props to unmodified so EF doesn't try to automatically insert/update them
            foreach (var roomPictureDb in roomPictureDbList)
            {
                _context.Entry(roomPictureDb).Reference(r => r.Room).IsModified = false;
            }

            _context.AddRange(roomPictureDbList);

            _context.SaveChanges();
        }

        public void RemoveList(IEnumerable<RoomPicture> roomPictureDbList)
        {
            if (roomPictureDbList == null || roomPictureDbList.Count() == 0) return;

            _context.RemoveRange(roomPictureDbList);

            _context.SaveChanges();
        }
    }
}
