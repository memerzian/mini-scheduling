using System.Collections.Generic;

namespace mini_scheduling.Models
{
    public class DataLoad
    {
        public Dictionary<int, PartEntity> PartDictionary { get; set; }
        public SupplyEntity[] Supplies { get; set; }
        public MasterScheduleEntity[] MasterSchedules { get; set; }
        public BillOfMaterialsRequirementEntity[] BomRequirements { get; set; }
        public WorkOrderRequirementEntity[] WorkOrderRequirements { get; set; }

        public DataLoad(Dictionary<int, PartEntity> partDictionary, SupplyEntity[] supplies, MasterScheduleEntity[] masterSchedules
            , BillOfMaterialsRequirementEntity[] bomRequirements, WorkOrderRequirementEntity[] workOrderRequirements)
        {
            PartDictionary = partDictionary;
            Supplies = supplies;
            MasterSchedules = masterSchedules;
            BomRequirements = bomRequirements;
            WorkOrderRequirements = workOrderRequirements;
        }
    }
}