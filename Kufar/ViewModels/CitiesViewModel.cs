using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Kufar.Models;

namespace Kufar.ViewModels
{
    public class CitiesViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required!")]
        public string Name { get; set; }
        public int CountryId { get; set; }

        public IEnumerable<City> Cities { get; set; }
        public Country SelectedCountry { get; set; }
        public Country City { get; set; }
        public Country Country { get; set; }
    }
}
