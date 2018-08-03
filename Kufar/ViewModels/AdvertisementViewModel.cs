using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Kufar.Models;

namespace Kufar.ViewModels
{
    public class AdvertisementViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "incorrect information")]


        public string Title { get; set; }

        public string Description { get; set; }


        public string Photo { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }
    }
}
