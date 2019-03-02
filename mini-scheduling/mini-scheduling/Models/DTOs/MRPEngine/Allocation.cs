using System;
using System.Collections.Generic;

namespace mini_scheduling.Models
{
    public class Allocation
    {
        public Demand Demand { get; set; }
        public Supply Supply { get; set; }
        public int Quantity { get; set; }

        public Allocation(Demand demand, Supply supply, int quantity)
        {
            Demand = demand;
            Supply = supply;
            Quantity = quantity;
        }
    }
}