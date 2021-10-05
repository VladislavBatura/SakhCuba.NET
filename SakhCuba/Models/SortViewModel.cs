using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakhCuba.Models
{
    public class SortViewModel
    { 
        public SortState NameSort { get; set; }
        public SortState NicknameSort { get; set; }
        public SortState DiscordNameSort { get; set; }
        public SortState AgeSort { get; set; }
        public SortState DecisionSort { get; set; }
        public SortState Current { get; set; }
        public bool Up { get; set; }

        public SortViewModel(SortState sortOrder)
        {
            NameSort = SortState.NameAsc;
            NicknameSort = SortState.NicknameAsc;
            DiscordNameSort = SortState.DiscordNameAsc;
            AgeSort = SortState.AgeAsc;
            DecisionSort = SortState.DecisionAsc;
            Up = true;

            if(sortOrder == SortState.NameDesc || sortOrder == SortState.NicknameDesc ||
                sortOrder == SortState.DiscordNameDesc || sortOrder == SortState.AgeDesc ||
                sortOrder == SortState.DecisionDesc)
            {
                Up = false;
            }

            switch (sortOrder)
            {
                case SortState.NameAsc:
                    Current = NameSort = SortState.NameDesc;
                    break;
                case SortState.NameDesc:
                    Current = NameSort = SortState.NameAsc;
                    break;
                case SortState.NicknameAsc:
                    Current = NicknameSort = SortState.NicknameDesc;
                    break;
                case SortState.NicknameDesc:
                    Current = NicknameSort = SortState.NicknameAsc;
                    break;
                case SortState.DiscordNameAsc:
                    Current = DiscordNameSort = SortState.DiscordNameDesc;
                    break;
                case SortState.DiscordNameDesc:
                    Current = DiscordNameSort = SortState.DiscordNameAsc;
                    break;
                case SortState.AgeAsc:
                    Current = AgeSort = SortState.AgeDesc;
                    break;
                case SortState.AgeDesc:
                    Current = AgeSort = SortState.AgeAsc;
                    break;
                case SortState.DecisionAsc:
                    Current = DecisionSort = SortState.DecisionDesc;
                    break;
                case SortState.DecisionDesc:
                    Current = DecisionSort = SortState.DecisionAsc;
                    break;
                default:
                    Current = NameSort = SortState.NameDesc;
                    break;
            }
        }

    }
}
