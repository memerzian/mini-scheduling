using System;
using System.Collections.Generic;

namespace mini_scheduling.Models
{
    public class SupplyCreation
    {
        public Dictionary<int, List<Supply>> SuppliesDictionary { get; set; }
        public List<ScheduledObject> SupplyScheduledObjects { get; set; }
    }
}