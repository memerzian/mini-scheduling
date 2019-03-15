using mini_scheduling.Models;
using System.Collections.Generic;

namespace min_scheduling.MRP_Engine
{
    public class InitialSupplyCreator
    {
        public SupplyCreation CreateInitialSupply(SupplyEntity[] supplies)
        {
            var initialSupply = new SupplyCreation()
            {
                SuppliesDictionary = new Dictionary<int, List<Supply>>(),
                SupplyScheduledObjects = new List<ScheduledObject>()
            };

            foreach (SupplyEntity supply in supplies)
            {
                var scheduledObject = new ScheduledObject
                {
                    SupplyID = supply.SupplyID,
                    TypeID = supply.TypeID,
                    PartID = supply.PartID,
                    Quantity = supply.Quantity
                };

                initialSupply.SupplyScheduledObjects.Add(scheduledObject);

                var supplyObject = new Supply()
                {
                    PartID = supply.PartID,
                    Quantity = supply.Quantity,
                    QuantityAllocated = 0,
                    ScheduledObject = scheduledObject,
                    SortOrder = SortOrder.sortDictionary[supply.TypeID]
                };

                if (!initialSupply.SuppliesDictionary.ContainsKey(supply.PartID))
                {
                    initialSupply.SuppliesDictionary.Add(supply.PartID, new List<Supply>() { });
                }

                initialSupply.SuppliesDictionary[supply.PartID].Add(supplyObject);
            }

            return initialSupply;
        }
    }
}