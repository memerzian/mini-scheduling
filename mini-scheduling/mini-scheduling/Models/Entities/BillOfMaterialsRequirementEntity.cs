using System;

namespace mini_scheduling.Models
{
    public class BillOfMaterialsRequirementEntity
    {
        public int BillOfMaterialsRequirementID { get; set; }
        public int BillOfMaterialsID { get; set; }
        public int Quantity { get; set; }
        public int RequiredPartID { get; set; }

        public PartEntity Part { get; set; }
        public BillOfMaterialsEntity Bom { get; set; }
    }
}