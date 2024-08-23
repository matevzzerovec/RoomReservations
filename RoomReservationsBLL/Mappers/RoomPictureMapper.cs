using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using RoomReservationsBLL.Modules;
using RoomReservationsDAL.Reservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Mappers
{
    internal class RoomPictureMapper
    {
        internal static IEnumerable<RoomPicture> MapToDb(int roomId, IFormFile[] newPictureList)
        {
            var result = new List<RoomPicture>();

            if (newPictureList != null)
            {
                foreach (var picture in newPictureList)
                {
                    result.Add(new RoomPicture()
                    {
                        RoomId = roomId,
                        PictureData = ConverterModule.ConvertToBytes(picture)
                    });
                }
            }

            return result;
        }
    }
}
