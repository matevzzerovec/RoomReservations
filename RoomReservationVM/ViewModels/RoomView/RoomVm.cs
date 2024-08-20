using System.ComponentModel.DataAnnotations;

namespace RoomReservationsVM.ViewModels.RoomView
{
    public class RoomVm : CommonVm
    {
        public List<int> RoomIdList { get; set; } = new List<int>();

        public int? RoomId { get; set; }

        [Required(ErrorMessage = "Polje je obvezno")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Polje je obvezno")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Vnesite veljavno ceno")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Polje je obvezno")]
        public string? ShortDescription { get; set; }

        [Required(ErrorMessage = "Polje je obvezno")]
        public string? LongDescription { get; set; }

        public List<PictureVm> PictureList { get; set; } = new List<PictureVm>();
    }
}
