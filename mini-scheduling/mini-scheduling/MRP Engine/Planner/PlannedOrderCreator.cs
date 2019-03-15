using min_scheduling.Models.Enums;
using mini_scheduling.Models;
using System.Collections.Generic;
using System.Linq;

namespace min_scheduling.MRP_Engine
{
    public class PlannedOrderCreator
    {
        public Supply CreatePlannedOrder(int quantity, int partID, List<ScheduledObject> scheduledObjects)
        {
            int? currentSequence = scheduledObjects
                .Where(s => s.PartID == partID)
                .Where(s => s.TypeID == (int)ObjectType.PlannedOrder)
                .Max(s => s.Sequence);

            var scheduledObject = new ScheduledObject
            {
                SupplyID = null,
                TypeID = (int)ObjectType.PlannedOrder,
                PartID = partID,
                Quantity = quantity,
                Sequence = currentSequence != null ? currentSequence + 1 : 1
            };

            scheduledObjects.Add(scheduledObject);

            var supplyObject = new Supply()
            {
                PartID = partID,
                Quantity = quantity,
                QuantityAllocated = 0,
                ScheduledObject = scheduledObject,
                SortOrder = SortOrder.sortDictionary[scheduledObject.TypeID]
            };

            return supplyObject;
        }
    }
}