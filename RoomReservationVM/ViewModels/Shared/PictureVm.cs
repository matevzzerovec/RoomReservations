namespace RoomReservationsVM.Models.Shared
{
    public class PictureVm
    {
        public int? RoomPictureId { get; set; }
        public int? RoomId { get; set; }
        public byte[] PictureData { get; set; }
    }
}
