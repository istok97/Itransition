using System.ComponentModel.DataAnnotations;

namespace Kufar.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="incorrect information")]
        
        public string Title { get; set; }

        public string Description { get; set; }


        public string Photo { get; set; }

        public Country Country { get; set; }

        public City City { get; set; }
    }
}
