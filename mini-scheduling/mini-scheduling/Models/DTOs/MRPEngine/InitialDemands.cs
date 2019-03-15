using System.Collections.Generic;

namespace mini_scheduling.Models
{
    public class InitialDemands
    {
        public Dictionary<int, List<Demand>> DemandsDictionary { get; set; }
        public List<ScheduledObject> DemandScheduledObjects { get; set; }
    }
}