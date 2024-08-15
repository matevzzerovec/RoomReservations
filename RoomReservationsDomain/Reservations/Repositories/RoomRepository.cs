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

        public Room GetFirstRoomWithPictures()
        {
            return _context.Room.Include(x => x.RoomPictures).OrderBy(x => x.RoomId).First();
        }

        public Room GetRoomWithPictures(int roomId)
        {
            return _context.Room.Include(x => x.RoomPictures).Single(x => x.RoomId == roomId);
        }

        public List<int> GetRoomIdList()
        {
            return _context.Room.Select(x => x.RoomId).ToList();
        }
    }
}
