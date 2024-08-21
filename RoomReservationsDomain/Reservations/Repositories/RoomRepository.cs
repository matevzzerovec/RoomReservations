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
            var room = _context.Room.SingleOrDefault(x => x.RoomId == roomId);
            if (room == null)
            {
                throw new Exception($"Room with ID {roomId} not found.");
            }

            return room;
        }

        public Room GetRoomWithPictures(int roomId)
        {
            var room = _context.Room
                .Include(x => x.RoomPictures)
                .SingleOrDefault(x => x.RoomId == roomId);
            
            if (room == null)
            {
                throw new Exception($"Room with ID {roomId} not found.");
            }

            return room;
        }

        public Room GetRoomWithPicturesReservations(int roomId)
        {
            var room = _context.Room
                .Include(x => x.RoomPictures)
                .Include(x => x.Reservations)
                .SingleOrDefault(x => x.RoomId == roomId);

            if (room == null)
            {
                throw new Exception($"Room with ID {roomId} not found.");
            }

            return room;
        }

        public void Add(Room roomDb)
        {
            roomDb.LastTimestamp = DateTime.Now;

            _context.Add(roomDb);

            // Set navigation props to unmodified so EF doesn't try to automatically insert/update them
            _context.Entry(roomDb).Collection(r => r.RoomPictures).IsModified = false;
            _context.Entry(roomDb).Collection(r => r.Reservations).IsModified = false;

            _context.SaveChanges();
        }

        public void Update(Room roomDb)
        {
            roomDb.LastTimestamp = DateTime.Now;

            // Set navigation props to unmodified so EF doesn't try to automatically insert/update them
            _context.Entry(roomDb).Collection(r => r.RoomPictures).IsModified = false;
            _context.Entry(roomDb).Collection(r => r.Reservations).IsModified = false;

            _context.SaveChanges();
        }

        public void Delete(Room roomDb)
        {
            // Delete pictures
            _context.RoomPicture.RemoveRange(roomDb.RoomPictures);

            // TODO: could add validation for existing reservations
            _context.Reservation.RemoveRange(roomDb.Reservations);

            _context.Room.Remove(roomDb);

            _context.SaveChanges();
        }
    }
}
