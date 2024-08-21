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
            var room = _context.Room.Include(x => x.RoomPictures).SingleOrDefault(x => x.RoomId == roomId);
            if (room == null)
            {
                throw new Exception($"Room with ID {roomId} not found.");
            }

            return room;
        }

        public void Add(Room roomDb)
        {
            _context.Add(roomDb);

            // Set navigation props to unmodified so EF doesn't try to automatically insert/update them
            _context.Entry(roomDb).Collection(r => r.RoomPictures).IsModified = false;
            _context.Entry(roomDb).Collection(r => r.Reservations).IsModified = false;

            _context.SaveChanges();
        }

        public void Update(Room roomDb)
        {
            // Attach the room to the context, so it is tracked by EF
            var existingRoom = _context.Room.Include(r => r.RoomPictures).FirstOrDefault(r => r.RoomId == roomDb.RoomId);
            if (existingRoom == null)
            {
                throw new Exception($"Room with ID {roomDb.RoomId} not found.");
            }

            existingRoom.Name = roomDb.Name;
            existingRoom.Price = roomDb.Price;
            existingRoom.ShortDescription = roomDb.ShortDescription;
            existingRoom.LongDescription = roomDb.LongDescription;
            existingRoom.LastTimestamp = DateTime.Now;
            existingRoom.LastUser = roomDb.LastUser;

            // Update RoomPictures (Handle updating, deleting, and adding pictures)
            existingRoom.RoomPictures = roomDb.RoomPictures;

            _context.SaveChanges();
        }

        public void Delete(int roomId)
        {
            var existingRoom = _context.Room.Include(r => r.RoomPictures).FirstOrDefault(r => r.RoomId == roomId);
            if (existingRoom == null)
            {
                throw new Exception($"Room with ID {roomId} not found.");
            }

            // Delete pictures
            _context.RoomPicture.RemoveRange(existingRoom.RoomPictures);

            // TODO: could add validation for existing reservations
            _context.Reservation.RemoveRange(existingRoom.Reservations);

            _context.Room.Remove(existingRoom);

            _context.SaveChanges();
        }
    }
}
