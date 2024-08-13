namespace RoomReservationsUI.Models.Shared
{
    public class Picture
    {
        public int? RoomPictureId { get; set; }
        public int? RoomId { get; set; }
        public byte[] PictureData { get; set; }
    }
}
