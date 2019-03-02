using System;
using System.Collections.Generic;

namespace mini_scheduling.Models
{
    public class Supply
    {
        public int Quantity { get; set; }
        public int QuantityAllocated { get; set; }
        public int PartID { get; set; }
        public ScheduledObject ScheduledObject { get; set; }
        public int SortOrder { get; set; }
    }
}