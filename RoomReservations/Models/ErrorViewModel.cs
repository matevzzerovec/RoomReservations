namespace RoomReservations.Models
{
    public class ErrorViewModel
    {
        public string ErrorDetails { get; set; }
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
