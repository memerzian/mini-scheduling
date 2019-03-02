using System;
using System.Collections.Generic;

namespace mini_scheduling.Models
{
    public class MRPResults
    {
        public List<ScheduledObject> ScheduledObjects { get; set; }
        public List<Allocation> Allocations { get; set; }
    }
}