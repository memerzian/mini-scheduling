using min_scheduling.Models.Enums;
using mini_scheduling.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace min_scheduling.MRP_Engine
{
    public class SupplyCreator
    {
        public SupplyCreation CreateSupply(SupplyEntity[] supplies)
        {
            var createdSupply = new SupplyCreation()
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

                createdSupply.SupplyScheduledObjects.Add(scheduledObject);

                var supplyObject = new Supply()
                {
                    PartID = supply.PartID,
                    Quantity = supply.Quantity,
                    QuantityAllocated = 0,
                    ScheduledObject = scheduledObject,
                    SortOrder = SortOrder.sortDictionary[supply.TypeID]
                };

                if (!createdSupply.SuppliesDictionary.ContainsKey(supply.PartID))
                {
                    createdSupply.SuppliesDictionary.Add(supply.PartID, new List<Supply>() { });
                }

                createdSupply.SuppliesDictionary[supply.PartID].Add(supplyObject);
            }

            return createdSupply;
        }
    }
}