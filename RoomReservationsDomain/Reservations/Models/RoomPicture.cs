using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsDAL.Reservations.Models
{
    public class RoomPicture
    {
        public int RoomPictureId { get; set; }
        public int RoomId { get; set; }
        public byte[] PictureData { get; set; }

        // Navigation property
        public Room Room { get; set; }
    }
}
