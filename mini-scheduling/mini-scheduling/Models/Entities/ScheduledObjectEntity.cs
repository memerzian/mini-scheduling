using System;

namespace mini_scheduling.Models
{
    public class ScheduledObjectEntity
    {
        public int ScheduledObjectID { get; set; }
        public int RunID { get; set; }
        public int TypeID { get; set; }
        public int? SupplyID { get; set; }
        public int? MasterScheduleID { get; set; }
        public int PartID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }

        public ObjectTypeEntity Type { get; set; }
    }
}