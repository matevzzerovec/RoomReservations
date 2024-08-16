namespace RoomReservationsVM.ViewModels
{
    public class ErrorVm
    {
        public string ErrorDetails { get; set; }
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
