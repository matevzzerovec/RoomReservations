using RoomReservationsVM.ViewModels.Booking;

namespace RoomReservationsVM.Models.Shared
{
    public class RoomVm
    {
        public List<int> RoomIdList { get; set; } = new List<int>();

        public int? RoomId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public List<PictureVm> PictureList { get; set; } = new List<PictureVm>();

        public BookingVm BookingVm { get; set; } = new BookingVm();
    }
}
