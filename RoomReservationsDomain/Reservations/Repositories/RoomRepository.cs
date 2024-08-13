using Microsoft.EntityFrameworkCore;
using RoomReservationsDAL.Reservations.Models;
using System;
using System.Collections.Generic;
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
            return _context.Room.OrderByDescending(x => x.RoomId).Include(x => x.RoomPictures).FirstOrDefault();
        }
    }
}
