using mini_scheduling.DAL;
using mini_scheduling.Models;
using System;
using System.Collections.Generic;

namespace min_scheduling.MRP_Engine
{
    public class DataCommitter
    {
        public void CommitData(int runID, MRPResult results)
        {
            using (var db = new SchedulingContext())
            {
                // hashcode is key and database ScheduledObjectID is value
                var hashcodeMap = new Dictionary<int, int>() { };

                foreach (var so in results.ScheduledObjects)
                {
                    var scheduledObjectEntity = new ScheduledObjectEntity
                    {
                        DueDate = (DateTime)so.DueDate,
                        StartDate = (DateTime)so.StartDate,
                        RunID = runID,
                        PartID = so.PartID,
                        MasterScheduleID = so.MasterScheduleID,
                        SupplyID = so.SupplyID
                    };

                    db.ScheduledObjects.Add(scheduledObjectEntity);

                    // Put SaveChanges here because I end up using the ID in the hashcode mapping
                    db.SaveChanges();

                    hashcodeMap.Add(so.Hashcode, scheduledObjectEntity.ScheduledObjectID);
                }

                foreach (var alloc in results.Allocations)
                {
                    var allocationEntity = new AllocationEntity
                    {
                        ParentScheduledObjectID = hashcodeMap[alloc.Demand.ScheduledObject.Hashcode],
                        ChildScheduledObjectID = hashcodeMap[alloc.Supply.ScheduledObject.Hashcode],
                        Quantity = alloc.Quantity,
                        RunID = runID
                    };

                    db.Allocations.Add(allocationEntity);
                    db.SaveChanges();
                }

                return;
            }
        }
    }
}