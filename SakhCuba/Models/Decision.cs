using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SakhCuba.Models
{
    public class Decision
    {
        public int DecisionId { get; set; }
        [Display(Name ="Решение")]
        public string DecisionName { get; set; }
        public List<Application> Applications { get; set; }
    }
}
