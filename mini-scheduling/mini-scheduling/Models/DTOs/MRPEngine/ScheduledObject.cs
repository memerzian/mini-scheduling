using System;

namespace mini_scheduling.Models
{
    public class ScheduledObject
    {
        public int ScheduledObjectID { get; set; }
        public int? ObjectID { get; set; }
        public int TypeID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int Hashcode { get; set; }

        public ScheduledObject()
        {
            Hashcode = this.GetHashCode();
        }
    }
}