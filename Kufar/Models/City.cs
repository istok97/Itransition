using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Kufar.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Country Country { get; set; }


    }
}
