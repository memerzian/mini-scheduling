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

        // D3 expects this children value to come in as lowercase when performing tree calculations
        // I should probably just find a way to update it on the front end
        public List<BOMObject> children { get; set; }

        public BOMObject()
        {
            children = new List<BOMObject>();
        }
    }
}