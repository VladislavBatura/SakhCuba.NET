using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakhCuba.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Application> Applications { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
