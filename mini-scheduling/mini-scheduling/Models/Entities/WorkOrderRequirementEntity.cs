using System;

namespace mini_scheduling.Models
{
    public class WorkOrderRequirementEntity
    {
        public int WorkOrderRequirementID { get; set; }
        public int SupplyID { get; set; }
        public bool OpenRequirement { get; set; }
        public int Quantity { get; set; }
        public int PartID { get; set; }

        public SupplyEntity Supply { get; set; }
    }
}