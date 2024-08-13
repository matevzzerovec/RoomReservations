using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRUI = RoomReservationsUI.Models.Shared;
using RRDAL = RoomReservationsDAL.Reservations.Models;

namespace RoomReservationServices.Mappers
{
    public static class RoomMapper
    {
        public static RRUI.Room MapToVm(RRDAL.Room roomDb)
        {
            var roomVm = new RRUI.Room();

            roomVm.RoomId = roomDb.RoomId;
            roomVm.Name = roomDb.Name;
            roomVm.Price = roomDb.Price;
            roomVm.ShortDescription = roomDb.ShortDescription;
            roomVm.LongDescription = roomDb.LongDescription;

            return roomVm;
        }
    }
}
