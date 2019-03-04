using System;

namespace mini_scheduling.Models
{
    public class AllocationEntity
    {
        public int AllocationID { get; set; }
        public int RunID { get; set; }
        public int ParentScheduledObjectID { get; set; }
        public int ChildScheduledObjectID { get; set; }
        public int BillOfMaterialsRequirementID { get; set; }
        public int WorkOrderRequirementID { get; set; }
        public int Quantity { get; set; }
    }
}