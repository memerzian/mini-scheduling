using System;

namespace mini_scheduling.Models
{
    public class PartEntity
    {
        public int PartID { get; set; }
        public string Name { get; set; }
        public int Leadtime { get; set; }
        public int BillOfMaterialsID { get; set; }
    }
}