using min_scheduling.Models.Enums;
using mini_scheduling.Models;
using System.Collections.Generic;

namespace min_scheduling.MRP_Engine
{
    public class InitialDemandCreator
    {
        public InitialDemands CreateInitialDemand (MasterScheduleEntity[] masterSchedules)
        {
            var initialDemand = new InitialDemands()
            {
                DemandsDictionary = new Dictionary<int, List<Demand>>(),
                DemandScheduledObjects = new List<ScheduledObject>()
            };

            foreach (MasterScheduleEntity masterSchedule in masterSchedules)
            {
                var scheduledObject = new ScheduledObject
                {
                    DueDate = masterSchedule.Date,
                    StartDate = masterSchedule.Date,
                    MasterScheduleID = masterSchedule.MasterScheduleID,
                    TypeID = (int)ObjectType.MasterSchedule,
                    PartID = masterSchedule.PartID,
                    Quantity = 1
                };

                initialDemand.DemandScheduledObjects.Add(scheduledObject);

                var demandObject = new Demand()
                {
                    PartID = masterSchedule.PartID,
                    Quantity = 1,
                    QuantityAllocatedTo = 0,
                    ScheduledObject = scheduledObject
                };

                if (!initialDemand.DemandsDictionary.ContainsKey(masterSchedule.PartID))
                {
                    initialDemand.DemandsDictionary.Add(masterSchedule.PartID, new List<Demand>() { });
                }

                initialDemand.DemandsDictionary[masterSchedule.PartID].Add(demandObject);
            }

            return initialDemand;
        }
    }
}