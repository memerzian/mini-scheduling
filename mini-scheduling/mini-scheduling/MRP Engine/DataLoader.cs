using mini_scheduling.DAL;
using mini_scheduling.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace min_scheduling.MRP_Engine
{
    public class DataLoader
    {
        public DataLoad LoadData()
        {
            using (var db = new SchedulingContext())
            {
                Dictionary<int, PartEntity> partDictionary = db.Parts
                    .ToDictionary(x => x.PartID);

                SupplyEntity[] supplies = db.Supplies
                    .ToArray();

                MasterScheduleEntity[] masterSchedules = db.MasterSchedules
                    .ToArray();

                // Including the bom data here b/c it is required for the dependency analyzer
                BillOfMaterialsRequirementEntity[] bomRequirements = db.BillOfMaterialsRequirements
                    .Include(b => b.Bom)
                    .ToArray();

                WorkOrderRequirementEntity[] workOrderRequirements = db.WorkOrderRequirements.ToArray();

                var dataLoad = new DataLoad(partDictionary, supplies, masterSchedules, bomRequirements, workOrderRequirements);

                return dataLoad;
            }
        }
    }
}