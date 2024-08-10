using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsDomain.Reservations.Models
{
    public class RoomPicture
    {
        public int RoomPictureId { get; set; }
        public int RoomId { get; set; }
        public byte[] PictureData { get; set; }

        // Reverse navigation
        public Room Room { get; set; } = new Room();
    }
}
