using System.Collections.Generic;

namespace mini_scheduling.Models
{
    public class BOMCost
    {
        public int MasterScheduleID { get; set; }
        public Dictionary<string, int> CostItems {get; set;}
    }
}