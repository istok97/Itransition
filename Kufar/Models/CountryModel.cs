using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kufar.Models
{
    public class CountryModel
    {
        public List<State> StateModel { get; set; }
        public SelectList FilteredCity { get; set; }
    }
    public class State
    {
        public int Id { get; set; }
        public string StateName { get; set; }
    }
    public class City
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string CityName { get; set; }
    }
}
