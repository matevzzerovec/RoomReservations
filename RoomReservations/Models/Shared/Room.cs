namespace RoomReservationsUI.Models.Shared
{
    public class Room
    {
        public int? RoomId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }

        public List<Picture> PictureList { get; set; } = new List<Picture>();
    }
}
