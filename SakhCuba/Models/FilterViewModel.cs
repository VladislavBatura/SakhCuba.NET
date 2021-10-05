using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakhCuba.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Decision> decisions, int? decision, string searchString)
        {
            decisions.Insert(0, new Decision { DecisionName = "Все", DecisionId = 0 });
            Decisions = new SelectList(decisions, "DecisionId", "DecisionName", decision);
            SelectedDecision = decision;
            SelectedSearchString = searchString;
        }

        public SelectList Decisions { get; private set; }
        public int? SelectedDecision { get; private set; }
        public string SelectedSearchString { get; private set; }
    }
}
