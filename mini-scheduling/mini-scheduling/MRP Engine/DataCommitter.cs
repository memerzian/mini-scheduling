using mini_scheduling.DAL;
using mini_scheduling.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
                        RunID = runID
                    };

                    db.ScheduledObjects.Add(scheduledObjectEntity);

                    db.SaveChanges();

                    hashcodeMap.Add(so.Hashcode, scheduledObjectEntity.ScheduledObjectID);
                }

                return;
            }
        }
    }
}