using RRVM = RoomReservationsVM.Models.Shared;
using RRDAL = RoomReservationsDAL.Reservations.Models;

namespace RoomReservationsBLL.Mappers
{
    public static class RoomMapper
    {
        public static RRVM.RoomVm MapToVm(RRDAL.Room roomDb)
        {
            var roomVm = new RRVM.RoomVm();

            roomVm.RoomId = roomDb.RoomId;
            roomVm.Name = roomDb.Name;
            roomVm.Price = roomDb.Price;
            roomVm.ShortDescription = roomDb.ShortDescription;
            roomVm.LongDescription = roomDb.LongDescription;

            foreach (var pictureDb in roomDb.RoomPictures)
            {
                var pictureVm = new RRVM.PictureVm();

                pictureVm.RoomId = pictureDb.RoomId;
                pictureVm.PictureData = pictureDb.PictureData;

                roomVm.PictureList.Add(pictureVm);
            }

            return roomVm;
        }
    }
}
