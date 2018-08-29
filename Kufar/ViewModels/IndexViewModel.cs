using System.Collections.Generic;
using Kufar.Models;

namespace Kufar.ViewModels
{
    public class IndexViewModel
    {

        public IEnumerable<Advertisement> Advertisements { get; set; }

        public SortViewModel SortViewModel { get; set; }

        public PageViewModel PageViewModel { get; set; }
        public int ViewType { get; set; }
    }
}
