using System;
using System.Collections.Generic;

namespace mini_scheduling.Models
{
    public class Demand
    {
        public int Quantity { get; set; }
        public int QuantityAllocatedTo { get; set; }
        public int PartID { get; set; }
        public ScheduledObject ScheduledObject { get; set; }
        public int? BomRequirementID { get; set; }
        public int? WorkOrderRequirementID { get; set; }
    }
}