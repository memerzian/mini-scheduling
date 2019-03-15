using min_scheduling.Models.Enums;
using mini_scheduling.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace min_scheduling.MRP_Engine
{
    public class Planner
    {
        public MRPResult Plan(DataLoad dataLoad, List<int> partIDOrder)
        {
            var scheduledObjects = new List<ScheduledObject>();
            
            // Create a scheduled object and supply/demand objects for every supply/master schedule
            var supplyCreator = new SupplyCreator();
            SupplyCreation supply = supplyCreator.CreateSupply(dataLoad.Supplies);

            scheduledObjects.AddRange(supply.SupplyScheduledObjects);
            var suppliesDictionary = supply.SuppliesDictionary;

            var demandCreator = new InitialDemandCreator();
            InitialDemands demands = demandCreator.CreateInitialDemand(dataLoad.MasterSchedules);

            scheduledObjects.AddRange(demands.DemandScheduledObjects);
            var demandsDictionary = demands.DemandsDictionary;

            // Allocate supply to demand for each part
            var allocations = new List<Allocation>();
            var plannedOrderCreator = new PlannedOrderCreator();

            foreach (int partID in partIDOrder)
            {
                Demand[] partDemands = demandsDictionary.ContainsKey(partID) ?
                    demandsDictionary[partID].OrderBy(d => d.ScheduledObject.StartDate).ToArray() :
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

                        // If there is no existing supply, need to create a planned order to fill in the gap
                        if (availableSupply == null)
                        {
                            availableSupply = plannedOrderCreator.CreatePlannedOrder(quantityNeeded, partID, scheduledObjects);
                        }

                        // Only allocate what you can given this demand and supply combination
                        int quantityAllocated = Math.Min(quantityNeeded, availableSupply.Quantity - availableSupply.QuantityAllocated);

                        // Create allocation
                        var allocation = new Allocation(partDemand, availableSupply, quantityAllocated);
                        allocations.Add(allocation);

                        // Update all quantities
                        availableSupply.QuantityAllocated += quantityAllocated;
                        partDemand.QuantityAllocatedTo += quantityAllocated;

                        // Adjust date of scheduledobject if it is earlier than a previous due date for the scheduled object
                        availableSupply.ScheduledObject.DueDate = availableSupply.ScheduledObject.DueDate < partDemand.ScheduledObject.StartDate
                            ? availableSupply.ScheduledObject.DueDate :
                            partDemand.ScheduledObject.StartDate;

                        availableSupply.ScheduledObject.StartDate = ((DateTime)availableSupply.ScheduledObject.DueDate)
                            .AddDays(dataLoad.PartDictionary[availableSupply.PartID].Leadtime * -1);

                        // Create demands for children of supply if planned order of work order
                        if (availableSupply.ScheduledObject.TypeID == (int)ObjectType.PlannedOrder)
                        {
                            BillOfMaterialsRequirementEntity[] requirements = dataLoad.BomRequirements.Where(b => b.Bom.PartID == availableSupply.PartID).ToArray();

                            foreach(BillOfMaterialsRequirementEntity requirement in requirements)
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

            var mrpResults = new MRPResult()
            {
                ScheduledObjects = scheduledObjects,
                Allocations = allocations
            };

            return mrpResults;
        }
    }
}