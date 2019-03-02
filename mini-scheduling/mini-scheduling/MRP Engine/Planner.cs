using min_scheduling.Models.Enums;
using mini_scheduling.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace min_scheduling.MRP_Engine
{
    public class Planner
    {
        public MRPResults Plan(DataLoad dataLoad, List<int> partIDOrder)
        {
            var scheduledObjects = new List<ScheduledObject>();
            var suppliesDictionary = new Dictionary<int, List<Supply>>();
            var demandsDictionary = new Dictionary<int, List<Demand>>();
            var allocations = new List<Allocation>();

            // Create a scheduled object and supply/demand objects for every supply/master schedule
            foreach(SupplyEntity supply in dataLoad.Supplies)
            {
                var scheduledObject = new ScheduledObject
                {
                    ObjectID = supply.SupplyID,
                    TypeID = supply.TypeID
                };

                scheduledObjects.Add(scheduledObject);

                var supplyObject = new Supply()
                {
                    PartID = supply.PartID,
                    Quantity = supply.Quantity,
                    QuantityAllocated = 0,
                    ScheduledObject = scheduledObject,
                    SortOrder = SortOrder.sortDictionary[supply.TypeID]
                };

                if (!suppliesDictionary.ContainsKey(supply.PartID))
                {
                    suppliesDictionary.Add(supply.PartID, new List<Supply>(){ });
                }

                suppliesDictionary[supply.PartID].Add(supplyObject);
            }

            foreach (MasterScheduleEntity masterSchedule in dataLoad.MasterSchedules)
            {
                var scheduledObject = new ScheduledObject
                {
                    DueDate = masterSchedule.Date,
                    StartDate = masterSchedule.Date.AddDays(dataLoad.PartDictionary[masterSchedule.PartID].Leadtime * -1),
                    ObjectID = masterSchedule.MasterScheduleID,
                    TypeID = (int)ObjectType.MasterSchedule
                };

                scheduledObjects.Add(scheduledObject);

                var demandObject = new Demand()
                {
                    PartID = masterSchedule.PartID,
                    Quantity = 1,
                    QuantityAllocatedTo = 0,
                    ScheduledObject = scheduledObject
                };

                if (!demandsDictionary.ContainsKey(masterSchedule.PartID))
                {
                    demandsDictionary.Add(masterSchedule.PartID, new List<Demand>() { });

                }

                demandsDictionary[masterSchedule.PartID].Add(demandObject);
            }

            // Start the allocations and planned order creations
            foreach (int partID in partIDOrder)
            {
                Demand[] partDemands = demandsDictionary.ContainsKey(partID) ?
                    demandsDictionary[partID].OrderBy(d => d.ScheduledObject.DueDate).ToArray() :
                    null;

                Supply[] partSupplies = suppliesDictionary.ContainsKey(partID) ?
                    suppliesDictionary[partID].OrderBy(s => s.SortOrder).ToArray() :
                    null;

                foreach (var partDemand in partDemands)
                {
                    while (partDemand.Quantity > partDemand.QuantityAllocatedTo)
                    {
                        Supply availableSupply = partSupplies?.Where(ps => ps.QuantityAllocated < ps.Quantity).FirstOrDefault();

                        int quantityNeeded = partDemand.Quantity - partDemand.QuantityAllocatedTo;

                        // If there is no existing supply, need to create a planned order to fill in the gaps
                        if (availableSupply == null)
                        {
                            availableSupply = CreatePlannedOrder(quantityNeeded, partID, scheduledObjects);
                        }

                        // Only allocate what you can given this demand and supply combination
                        int quantityAllocated = Math.Min(quantityNeeded, availableSupply.Quantity);

                        // Create allocation
                        var allocation = new Allocation(partDemand, availableSupply, quantityAllocated);
                        allocations.Add(allocation);

                        // Update all quantities
                        availableSupply.QuantityAllocated += quantityAllocated;
                        partDemand.QuantityAllocatedTo += quantityAllocated;

                        // Adjust date of scheduledobject
                        availableSupply.ScheduledObject.DueDate = availableSupply.ScheduledObject.DueDate < partDemand.ScheduledObject.StartDate
                            ? availableSupply.ScheduledObject.DueDate :
                            partDemand.ScheduledObject.StartDate;

                        // Create demands for children of supply
                        if (availableSupply.ScheduledObject.TypeID == (int)ObjectType.PlannedOrder)
                        {
                            BillOfMaterialsRequirementEntity[] requirements = dataLoad.BomRequirements.Where(b => b.Bom.PartID == availableSupply.PartID).ToArray();

                            foreach(var requirement in requirements)
                            {
                                var demandObject = new Demand()
                                {
                                    PartID = requirement.RequiredPartID,
                                    Quantity = quantityAllocated * requirement.Quantity,
                                    QuantityAllocatedTo = 0,
                                    ScheduledObject = availableSupply.ScheduledObject
                                };

                                if (!demandsDictionary.ContainsKey(requirement.RequiredPartID))
                                {
                                    demandsDictionary.Add(requirement.RequiredPartID, new List<Demand>() { });
                                }

                                demandsDictionary[requirement.RequiredPartID].Add(demandObject);
                            }
                        }
                    }
                }
            }

            var mrpResults = new MRPResults()
            {
                ScheduledObjects = scheduledObjects,
                Allocations = allocations
            };

            return mrpResults;
        }

        public Supply CreatePlannedOrder(int quantity, int partID, List<ScheduledObject> scheduledObjects)
        {
            var scheduledObject = new ScheduledObject
            {
                DueDate = DateTime.Today,
                StartDate = DateTime.Today,
                ObjectID = null,
                TypeID = (int)ObjectType.PlannedOrder
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