
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kufar.Models

{
    public class Country
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }

        public ICollection<City> Cities { get; set; }

        public ICollection<Advertisement> Advertisements { get; set; }
    }
}