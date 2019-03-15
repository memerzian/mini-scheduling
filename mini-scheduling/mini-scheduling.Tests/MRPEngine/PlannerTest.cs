using Microsoft.VisualStudio.TestTools.UnitTesting;
using min_scheduling.Models.Enums;
using min_scheduling.MRP_Engine;
using mini_scheduling.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace min_scheduling.Tests.MRPEngine
{
    [TestClass]
    public class PlannerTest
    {
        [TestMethod]
        public void EnsureEngineCreatesPlannedOrderWithCorrectDate()
        {
            // Arrange
            var planner = new Planner();
            int partID = 1;
            int partLeadtime = 10;

            var partDictionary = new Dictionary<int, PartEntity>()
                {
                    { 1, new PartEntity
                    {
                        PartID = partID,
                        Leadtime = partLeadtime
                    }}
            };

            var supplies = new SupplyEntity[0];

            var masterSchedule = new MasterScheduleEntity
            {
                MasterScheduleID = 1,
                Date = DateTime.Today,
                PartID = 1
            };

            var masterSchedules = new MasterScheduleEntity[1];
            masterSchedules[0] = masterSchedule;

            var bomRequirements = new BillOfMaterialsRequirementEntity[0];
            var workOrderRequirements = new WorkOrderRequirementEntity[0];

            var dataLoad = new DataLoad(partDictionary, supplies, masterSchedules, bomRequirements, workOrderRequirements);

            var partIDOrder = new List<int>();
            partIDOrder.Add(partID);


            // Act
            var result = planner.Plan(dataLoad, partIDOrder);

            // Assert
            var plannedOrder = result.ScheduledObjects
                .Where(s => s.TypeID == (int)ObjectType.PlannedOrder)
                .FirstOrDefault();

            Assert.IsNotNull(plannedOrder);
            Assert.IsTrue(plannedOrder.StartDate == DateTime.Today.AddDays(partLeadtime * -1));
        }
    }
}
