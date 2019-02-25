using System;

namespace mini_scheduling.Models
{
    public class ScheduledObjectEntity
    {
        public int ScheduledObjectID { get; set; }
        public int RunID { get; set; }
        public int ObjectID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}