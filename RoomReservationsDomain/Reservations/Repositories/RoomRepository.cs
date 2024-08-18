using Microsoft.EntityFrameworkCore;
using RoomReservationsDAL.Reservations.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsDAL.Reservations.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ReservationsDbContext _context;

        public RoomRepository(ReservationsDbContext context)
        {
            _context = context;
        }
        public List<int> GetRoomIdList()
        {
            return _context.Room.Select(x => x.RoomId).ToList();
        }


        public IQueryable<Room> GetAll()
        {
            return _context.Room;
        }

        public Room GetFirstRoomWithPictures()
        {
            return _context.Room.Include(x => x.RoomPictures).OrderBy(x => x.RoomId).First();
        }

        public decimal GetRoomPrice(int roomId)
        {
            var desiredRoom = _context.Room.SingleOrDefault(x => x.RoomId == roomId);
            if (desiredRoom != null)
            {
                return desiredRoom.Price;
            }
            else
            {
                return 0;
            }
        }

        public Room GetRoom(int roomId)
        {
            return _context.Room.SingleOrDefault(x => x.RoomId == roomId);
        }

        public Room GetRoomWithPictures(int roomId)
        {
            return _context.Room.Include(x => x.RoomPictures).SingleOrDefault(x => x.RoomId == roomId);
        }
    }
}
