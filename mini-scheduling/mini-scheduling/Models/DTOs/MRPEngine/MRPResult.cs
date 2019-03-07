using System;
using System.Collections.Generic;

namespace mini_scheduling.Models
{
    public class MRPResult
    {
        public List<ScheduledObject> ScheduledObjects { get; set; }
        public List<Allocation> Allocations { get; set; }
    }
}