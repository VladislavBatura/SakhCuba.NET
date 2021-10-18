using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakhCuba.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string FirstColumn { get; set; }
        public string SecondColumn { get; set; }
        public string ThirdColumn { get; set; }

        public Images Image { get; set; }
    }
}
