using System;

namespace mini_scheduling.Models
{
    public class SupplyEntity
    {
        public int SupplyID { get; set; }
        public int TypeID { get; set; }
        public int PartID { get; set; }
        public int Quantity { get; set; }
    }
}