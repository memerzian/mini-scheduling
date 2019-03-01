using System;

namespace mini_scheduling.Models
{
    public class MasterScheduleEntity
    {
        public int MasterScheduleID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int PartID { get; set; }
    }
}