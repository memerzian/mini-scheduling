using System;
using System.Collections.Generic;

namespace mini_scheduling.Models
{
    public class PartAllocationDetails
    {
        // Demand
        public string ParentPartNumber { get; set; }
        public string ParentType { get; set; }
        public string ParentObjectName { get; set; }
        public DateTime ParentDueDate { get; set; }
        public DateTime ParentStartDate { get; set; }
        public int RequirementQuantity { get; set; }
        public bool DemandOrderRepeat { get; set; }

        // Allocation
        public int AllocationQuantity { get; set; }

        // Supply
        public string Type { get; set; }
        public string ObjectName { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public int SupplyQuantity { get; set; }
        public bool SupplyOrderRepeat { get; set; }
        
    }
}