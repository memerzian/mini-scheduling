using System;
using System.Collections.Generic;

namespace mini_scheduling.Models
{
    public class BOMObject
    {
        public string Partnumber { get; set; }
        public int PartID { get; set; }
        public int LeadTime { get; set; }
        public int Quantity { get; set; }
        public List<BOMObject> Children { get; set; }

        public BOMObject()
        {
            Children = new List<BOMObject>();
        }
    }
}