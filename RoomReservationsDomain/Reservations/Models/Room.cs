using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsDomain.Reservations.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public DateTime LastTimestamp { get; set; }
        public string LastUser { get; set; }

        // Navigation properties
        public ICollection<RoomPicture> RoomPictures { get; set; } = new List<RoomPicture>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
