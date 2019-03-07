using mini_scheduling.Models;
using System.Collections.Generic;
using System.Linq;

namespace min_scheduling.MRP_Engine
{
    public class DependencyAnalyzer
    {
        public List<int> AnalyzeDependencies(BillOfMaterialsRequirementEntity[] bomRequirements, WorkOrderRequirementEntity[] workOrderRequirements, Dictionary<int, PartEntity> partDictionary)
        {
            var partIDOrder = new List<int>();

            BomRelationship[] uniqueRelationships = bomRequirements
                .Select(b => new BomRelationship(b.Bom.PartID, b.RequiredPartID))
                .Union(workOrderRequirements
                    .Select(w => new BomRelationship(w.Supply.PartID, w.PartID))
                    )
                .Distinct()
                .ToArray();

            int[] allPartIDs = partDictionary.Keys.ToArray();

            AddPart(partIDOrder, uniqueRelationships, allPartIDs);

            return partIDOrder;
        }

        private void AddPart(List<int> partIDOrder, BomRelationship[] uniqueRelationships, int[] allPartIDs)
        {
            // Find all children parts and remove relationships for parents that have already been processed
            int[] childrenPartIDs = uniqueRelationships
                .Where(u => !partIDOrder.Contains(u.ParentPartID))
                .Select(u => u.ChildPartID)
                .ToArray();

            int[] noDependencyPartIDs = allPartIDs.Except(childrenPartIDs).ToArray();

            // Remove partIDs that have already been processed because we don't want to add those again to the list
            int[] partIDsToAdd = noDependencyPartIDs.Except(partIDOrder).ToArray();

            foreach (var partID in partIDsToAdd)
            {
                partIDOrder.Add(partID);
            }

            if (allPartIDs.Except(partIDOrder).ToArray().Length > 0)
            {
                AddPart(partIDOrder, uniqueRelationships, allPartIDs);
            }

            return;
        }
    }
}