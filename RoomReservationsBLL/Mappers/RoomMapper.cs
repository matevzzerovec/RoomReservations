using RRVM = RoomReservationsVM.ViewModels.RoomView;
using RRDAL = RoomReservationsDAL.Reservations.Models;
using RoomReservationsDAL.Reservations.Models;
using RoomReservationsVM.ViewModels.RoomView;

namespace RoomReservationsBLL.Mappers
{
    internal static class RoomMapper
    {
        internal static RRVM.RoomVm MapToVm(RRDAL.Room roomDb)
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

                pictureVm.RoomPictureId = pictureDb.RoomPictureId;
                pictureVm.RoomId = pictureDb.RoomId;
                pictureVm.PictureData = pictureDb.PictureData;

                roomVm.PictureList.Add(pictureVm);
            }

            return roomVm;
        }

        internal static RRDAL.Room MapToDb(RRVM.RoomVm roomVm)
        {
            var roomDb = new RRDAL.Room();

            roomDb.RoomId = roomVm.RoomId.GetValueOrDefault();
            roomDb.Name = roomVm.Name;
            roomDb.Price = roomVm.Price.GetValueOrDefault();
            roomDb.ShortDescription = roomVm.ShortDescription;
            roomDb.LongDescription = roomVm.LongDescription;

            return roomDb;
        }

        internal static void MapChangesToDb(Room roomDb, RoomVm roomVm)
        {
            roomDb.Name = roomVm.Name;
            roomDb.Price = roomVm.Price.GetValueOrDefault();
            roomDb.ShortDescription = roomVm.ShortDescription;
            roomDb.LongDescription = roomVm.LongDescription;
        }
    }
}
