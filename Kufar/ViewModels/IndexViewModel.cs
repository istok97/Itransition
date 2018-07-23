using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kufar.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Advertisement> Advertisements { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
