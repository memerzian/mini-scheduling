using System;
using System.Collections.Generic;

namespace mini_scheduling.Models
{
    public class PartAllocationStatus
    {
        public string PartNumber { get; set; }
        public int Leadtime { get; set; }
        public PartAllocationDetails[] Details { get; set; }
    }
}