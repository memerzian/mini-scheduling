using min_scheduling.Models.Enums;
using mini_scheduling.Models;
using System.Collections.Generic;
using System.Linq;

namespace min_scheduling.MRP_Engine
{
    public class ChildDemandCreator
    {
        public void CreateChildDemand(BillOfMaterialsRequirementEntity[] bomRequirements, WorkOrderRequirementEntity[] workOrderRequirements
            , Supply availableSupply, int quantityAllocated, Dictionary<int, List<Demand>> demandsDictionary)
        {
            var requirements = new List<Requirement>();
            
            if (availableSupply.ScheduledObject.TypeID == (int)ObjectType.PlannedOrder)
            {
                requirements = bomRequirements
                    .Where(b => b.Bom.PartID == availableSupply.PartID)
                    .Select(b => new Requirement
                    {
                        RequiredPartID = b.RequiredPartID,
                        RequiredQuantity = b.Quantity
                    })
                    .ToList();
            }
            else
            {
                requirements = workOrderRequirements
                    .Where(w => w.SupplyID == availableSupply.ScheduledObject.SupplyID)
                    .Where(w => w.OpenRequirement)
                    .Select(w => new Requirement
                    {
                        RequiredPartID = w.PartID,
                        RequiredQuantity = w.Quantity
                    })
                    .ToList();
            };
                
            foreach (Requirement requirement in requirements)
            {
                var demandObject = new Demand()
                {
                    PartID = requirement.RequiredPartID,
                    Quantity = quantityAllocated * requirement.RequiredQuantity,
                    QuantityAllocatedTo = 0,
                    ScheduledObject = availableSupply.ScheduledObject
                };

                if (!demandsDictionary.ContainsKey(requirement.RequiredPartID))
                {
                    demandsDictionary.Add(requirement.RequiredPartID, new List<Demand>() { });
                }

                demandsDictionary[requirement.RequiredPartID].Add(demandObject);
            }

            return;
        }

        public class Requirement
        {
            public int RequiredPartID { get; set; }
            public int RequiredQuantity { get; set; }
        }
    }
}