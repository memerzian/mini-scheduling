using mini_scheduling.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace min_scheduling.MRP_Engine
{
    public class Planner
    {
        public void Plan(DataLoad dataLoad, List<int> partIDOrder)
        {
            var scheduledObjects = new List<ScheduledObjectEntity>();

            // First step is to create a scheduled object for every supply/master schedule
            foreach(SupplyEntity supply in dataLoad.Supplies)
            {
                var scheduledObject = new ScheduledObject
                {
                    DueDate = DateTime.Today,
                    StartDate = DateTime.Today,
                    ObjectID = supply.SupplyID,
                    TypeID = supply.TypeID
                };
            }

            foreach (MasterScheduleEntity masterSchedule in dataLoad.MasterSchedules)
            {
                var scheduledObject = new ScheduledObject
                {
                    DueDate = DateTime.Today,
                    StartDate = DateTime.Today
                };
            }
        }
    }
}