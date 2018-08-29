using System.ComponentModel.DataAnnotations;
using Kufar.Models.Enums;

namespace Kufar.ViewModels
{
    public class AdvertisementViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "incorrect information")]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        [Required(ErrorMessage = "Required!")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Required!")]
        public int CityId { get; set; }

        public DisplayAdvertisement DisplayAdvertisement { get; set; }
    }
}
